var IsEditMode = false;

$(document).ready(function () {
    LoadGridData();
    LoadCountry();

    $(document).ajaxStart(function () {
        $(window).scrollTop(0);
        $("#wait").css("display", "block");
    });
    $(document).ajaxComplete(function () {
        $("#wait").css("display", "none");
    });

    $('#Bank_Id').chosen();
});



var LoadGridData = function () {
    var dataTable = $('#tblbank').DataTable({
        "destroy": true,
        "lengthMenu": [10, 25, 50, 100],
        "sAjaxSource": "../Branches/LoadGrid?CountryId=" + 0 + "&BankId=" + 0,
        "bServerSide": true,
        "bProcessing": true,
        "paging": true,
        "order": [[1, 'asc']],
        "language": {
            "emptyTable": "No record found."
        },
        "columns": [
            {
                "data": "English_Name",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "BankName",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "FullAddress",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "CountryName",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "EMail_Address1",
                "autoWidth": true,
                "searchable": true,
                "render": function (data, type, row) {

                    if (!data) {
                        return "<span style='display: block; text-align: center;'>-</span>";
                    }
                    return data;
                }
            }
        ]
    });
    $('#btnsearch').on('click', function () {
        var CountryId = $("#Country_Id").val();
        if ($("#Country_Id").val() == "0") {
            $("#Bank_Id").val("0");
        }
        var BankId = $("#Bank_Id").val();
        dataTable.ajax.url("../Branches/LoadGrid?CountryId=" + CountryId + "&BankId=" + BankId).load();
    });
};




$("#Country_Id").on('change', function () {
    var CountryId = this.value;
    if ($("#Country_Id").val() == "0") {
        $("#Bank_Id").val("0");
    }
    LoadBanks(CountryId);
});


$('#btn-refresh').click(function () {
    Reset();
});

// for loading User
var LoadCountry = function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Branches/LoadCountry",
        processData: false,
        contentType: false,
        success: function (data) {
            var sch = JSON.parse(data);

            var $el = $('#Country_Id');
            $el.empty();
            if (sch.length > 0) {
                $el.append('<option value= 0 >' + "Select Country" + '</option>');
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

var LoadBanks = function (Country_Id) {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Branches/LoadBanks?countryId=" + Country_Id,
        processData: false,
        contentType: false,
        success: function (data) {
            // alert(data);
            var sch = JSON.parse(data);

            var $el = $('#Bank_Id');
            $el.chosen('destroy');
            $el.empty();
            if (sch.length > 0) {
                $el.append('<option value= 0 >' + "Select Bank" + '</option>');
                $.each(sch, function (idx, obj) {
                    $el.append('<option value="' + obj.Bank_Id + '">' + obj.English_Name + '</option>');
                });
            }
            else {
                $el.append('<option value="">' + "Select Bank" + '</option>');
            }
            $el.trigger("liszt:updated");
            $el.chosen();
        }
    });
}

$('#btn-sync').on('click', function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Branches/SynchronizeRecords",
        processData: false,
        contentType: false,
        success: function (data) {
            LoadGridData();
            swal(
                'Success',
                data + ' Records Synchronized Successfully.',
                'success'
            );
        },
        error: function (xhr, status, error) {
            swal("Error", "Records Synchronized Failed !!", "error");
        }
    });
});