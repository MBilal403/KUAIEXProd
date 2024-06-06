var IsEditMode = false;

$(document).ready(function () {


    LoadCountry();
    fetchDataAndFillTable("0");
    $("tbody").sortable({
        axis: "y",
        cursor: "move",
        containment: "parent",
        update: function (event, ui) {

            var sortedIds = $(this).sortable("toArray");

            $.each(sortedIds, function (index, value) {
                var row = $('#' + value);
                row.appendTo($(this));
            });

            renumberRows();
        },
        start: function (event, ui) {
            ui.item.css("background-color", "#f7f7f7");
        },
        stop: function (event, ui) {
            ui.item.css("background-color", "");
        }
    });
    function renumberRows() {
        $("tbody tr").each(function (index) {
            $(this).find("td:first").text(index + 1);
        });
    }

    $("tbody tr").on("click", function () {
        $("tbody tr").removeClass("selected");
        $(this).addClass("selected");
    });


    $(document).on('change', '.toggle input[type="checkbox"]', function () {
        var id = $(this).closest('.toggle').attr('id').split('_')[1];
        var action = $(this).prop('checked') ? 'Active' : 'InActive';

        var confirmationMessage = 'Are you sure you want to ' + action.toLowerCase() + '?';
        if (confirm(confirmationMessage)) {
            $.ajax({
                url: '../Banks/ChangeDirectTransaction',
                method: 'POST',
                data: { id: id },
                success: function (response) {
                    fetchDataAndFillTable($("#Country_Id").val());
                },
                error: function (xhr, status, error) {
                    console.error('AJAX request failed');
                    fetchDataAndFillTable($("#Country_Id").val());
                }
            });
        } else {

            $(this).prop('checked', !$(this).prop('checked'));
        }
    });


    $("#btn-Priority").click(function () {
        var tableData = [];
        $("#sortable-table tbody tr").each(function () {
            var rowData = {};
            $(this).find("td").each(function (index, cell) {
                rowData["column" + index] = $(cell).text();
            });
            tableData.push(rowData);
        });

        if (tableData.length === 1 && tableData[0]["column0"] === "No records found") {
            // Alert if table has no records
            swal("Warning", "There are no records in the table.", "warning");
            return; // Exit function
        }

        var bankList = [];

        tableData.forEach(function (rowData) {
            if (rowData["column0"] === "No records found") {
                return; // Skip row with "No records found" message
            }
            var column0Value = parseInt(rowData["column0"]);
            var column1Value = parseInt(rowData["column1"]);

            // Create an object to store the extracted values
            var banks = {
                PriorityValue: column0Value,
                Bank_Id: column1Value
            };

            bankList.push(banks);
        });

        console.log(bankList);

        $.ajax({
            url: "../Banks/SetBankPriority",
            method: "POST",
            data: { bank_MstList: bankList },
            success: function (response) {
                if (response != 'error') {
                    swal(
                        'Success',
                        'Bank Priority Updated Successfully!',
                        'success'
                    );
                }
                else {
                    swal("Error", "Data not updated!!", "error");
                }
            },
            error: function (xhr, status, error) {
                swal("Error", "Data not updated!!", "error");
            }
        });
    });

});

$("#Country_Id").on('change', function () {
    var CountryId = this.value;
    fetchDataAndFillTable(CountryId);
});

$('#btnAdd').on('click', function () {
    var CountryId = $("#Country_Id").val();

});

var LoadCountry = function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Banks/LoadCountry",
        processData: false,
        contentType: false,
        success: function (data) {
            var sch = JSON.parse(data);

            var $el = $('#Country_Id');
            $el.empty();
            if (sch.length > 0) {
                $el.append('<option value="0">' + "Select Country" + '</option>');
                $.each(sch, function (idx, obj) {
                    $el.append('<option value="' + obj.Id + '">' + obj.Name + '</option>');
                });
            }
            else {
                $el.append('<option value="">' + "Select Country" + '</option>');
            }
            $el.trigger("liszt:updated");
            $el.chosen();
        }
    });
}

function fetchDataAndFillTable(CountryId) {

    var apiUrl = "../Banks/LoadBanks?countryId=" + CountryId;

    $.get(apiUrl, function (data) {
        $("#sortable-table tbody").empty();
        var serializedData = JSON.parse(data);

        if (Array.isArray(serializedData) && serializedData.length === 0) {
            var emptyRow = $("<tr>").append($("<td colspan='4'>").text("No records found"));
            $("#sortable-table tbody").append(emptyRow);
        } else {
            console.log(serializedData);

            var tableBody = $("#sortable-table tbody");

            $.each(serializedData, function (index, item) {
                var toggleHtml = item.DirectTransaction === true ? '<label id="label_' + item.Bank_Id + '" class="toggle"><input type="checkbox" checked><span class="slider"></span></label>' : '<label id="label_' + item.Bank_Id + '" class="toggle"><input type="checkbox"><span class="slider"></span></label>';
                var labelHtml = item.Record_Status === 'A' ? '<span class="label label-success label-xs">Active</span>' : '<span class="label label-danger label-xs">Blocked</span>';
                
                var row = $("<tr>").append(
                    $("<td>").text(index + 1),
                    $("<td>").text(item.Bank_Id).css("display", "none"),
                    $("<td>").text(item.English_Name),
                    $("<td>").html(labelHtml),
                     $("<td>").html(toggleHtml)
                );
                tableBody.append(row);
            });

        }
    });


}
