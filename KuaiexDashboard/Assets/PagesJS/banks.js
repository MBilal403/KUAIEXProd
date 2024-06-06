var IsEditMode = false;

$(document).ready(function () {
    handleStaff();
    LoadGridData();
    LoadCountry();

    var MAX_LIMIT = 1e15; // Maximum limit set to quadrillion

    $('#AmountLimit').on('input', function () {
        NumberToWords();
    });


    function NumberToWords() {
        var amountLimit = $('#AmountLimit').val();
        if (amountLimit !== '' && amountLimit <= MAX_LIMIT) {
            var amountInWords = convertToWords(amountLimit);
            $('#AmountInWords').text(amountInWords.toUpperCase());
        } else if (amountLimit === '') {
            $('#AmountInWords').text('empty');
        } else {
            $('#AmountInWords').text('Value exceeds maximum limit (' + MAX_LIMIT + ')');
        }

    }
    function TxnKWDToWords() {
        var amountLimit = $('#TransactionAmountFC').val();
        if (amountLimit !== '' && amountLimit <= MAX_LIMIT) {
            var amountInWords = convertToWords(amountLimit);
            $('#TransactionAmountFCInWords').text(amountInWords.toUpperCase());
        } else if (amountLimit === '') {
            $('#TransactionAmountFCInWords').text('empty');
        } else {
            $('#TransactionAmountFCInWords').text('Value exceeds maximum limit (' + MAX_LIMIT + ')');
        }
    }


    $('#TransactionAmountFC').on('input', function () {
        TxnKWDToWords();
    });

    function TxnFCToWords() {
        var amountLimit = $('#TransactionAmountKWD').val();
        if (amountLimit !== '' && amountLimit <= MAX_LIMIT) {
            var amountInWords = convertToWords(amountLimit);
            $('#TransactionAmountKWDInWords').text(amountInWords.toUpperCase());
        } else if (amountLimit === '') {
            $('#TransactionAmountKWDInWords').text('empty');
        } else {
            $('#TransactionAmountKWDInWords').text('Value exceeds maximum limit (' + MAX_LIMIT + ')');
        }
    }
    $('#TransactionAmountKWD').on('input', function () {
        TxnFCToWords();
    });




    function convertToWords(number) {
        var data = numberToWords.toWords(number);
        return data;
    }
    $('#setBankLimitsModal').on('hidden.bs.modal', function () {
        $('#modalTitle').text('');
        $('#AmountLimit').val('');
        $('#NumberofTransaction').val('');
        $('#TransactionAmountFC').val('');
        $('#TransactionAmountKWD').val('');
        $('#NumberOfTransactionMonthly').val('');
        $('#TransactionAmountKWDInWords').text('');
        $('#TransactionAmountFCInWords').text('');
        $('#UID').val('');
    });

    $(document).on('click', '.btn-detail', function () {
        var uid = $(this).attr('id');
        $.ajax({
            url: '../Banks/Detail?UID=' + uid,
            method: 'GET',
            success: function (bankDetails) {
                var response = JSON.parse(bankDetails);

                $('#bankId').text(response.Bank_Id);
                $('#englishName').text(response.English_Name);
                $('#arabicName').text(response.Arabic_Name);
                $('#shortEnglishName').text(response.Short_English_Name);
                $('#shortArabicName').text(response.Short_Arabic_Name);
                $('#countryId').text(response.CountryName);
                $('#currencyId').text(response.CurrencyName);
                $('#addressLine1').text(response.Full_Address);
                $('#telNumber').text(response.Tel_Number1);
                $('#faxNumber').text(response.Fax_Number1);
                $('#emailAddress').text(response.EMail_Address1);
                $('#contactPerson1').text(response.Contact_Person1);
                $('#contactTitle1').text(response.Contact_Title1);
                $('#mobileNumber1').text(response.Mob_Number1);
                $('#remarks').text(response.Remarks);
                $('#hoBranch1Id').text(response.HO_Branch1_Id);
                $('#bankCode').text(response.Bank_Code);
                // Show the modal
                $('#bankDetailsModal').modal('show');
            },
            error: function (xhr, status, error) {
                // Handle error
                console.error(error);
            }
        });


    });

    $(document).ajaxStart(function () {
        $(window).scrollTop(0);
        $("#wait").css("display", "block");
    });
    $(document).ajaxComplete(function () {
        $("#wait").css("display", "none");
    });
    $(document).on('click', '.btn-setLimit', function () {
        var uid = $(this).attr('id');
        $.ajax({
            url: '../Banks/GetLimitDetail?UID=' + uid,
            method: 'GET',
            success: function (response) {
                var data = JSON.parse(response);
                $('#modalTitle').text(data.English_Name);
                $('#AmountLimit').val(data.AmountLimit);
                $('#NumberOfTransaction').val(data.NumberOfTransaction);
                $('#NumberOfTransactionMonthly').val(data.NumberOfTransactionMonthly);
                $('#TransactionAmountKWD').val(data.TransactionAmountKWD);
                $('#TransactionAmountFC').val(data.TransactionAmountFC);
                $('#UID').val(data.UID);
                NumberToWords();
                TxnFCToWords();
                TxnKWDToWords();
                $('#setBankLimitsModal').modal('show');
            },
            error: function (xhr, status, error) {
                // Handle error
                console.error(error);
            }
        });


    });
    $(document).on('click', '#setLimitAllBanks', function () {
        $('#modalTitle').text("Set limit for all banks ");
        $('#AmountLimit').val(0);
        $('#NumberOfTransaction').val(0);
        $('#NumberOfTransactionMonthly').val(0);
        $('#TransactionAmountFC').val(0);
        $('#TransactionAmountKWD').val(0);

        NumberToWords();
        TxnFCToWords();
        TxnKWDToWords();
        $('#setBankLimitsModal').modal('show');
    });

    $(document).on('click', '#setPriority', function () {
        window.location = "../Banks/Priority";
    });


    $(document).on('change', '.toggle input[type="checkbox"]', function () {
        var uid = $(this).closest('.toggle').attr('id').split('_')[1];
        var action = $(this).prop('checked') ? 'Activated' : 'Blocked';

        var confirmationMessage = 'Are you sure you want to ' + action.toLowerCase() + '?';
        if (confirm(confirmationMessage)) {
            $.ajax({
                url: '../Banks/ChangeState',
                method: 'POST',
                data: { uid: uid },
                success: function (response) {
                    LoadGridData();
                },
                error: function (xhr, status, error) {
                    console.error('AJAX request failed');
                }
            });
        } else {
          
            $(this).prop('checked', !$(this).prop('checked'));
        }
    });

});

//load grid
var LoadGridData = function () {
    var dataTable = $('#tblbank').DataTable({
        "destroy": true,
        "lengthMenu": [10, 25, 50, 100],
        "sAjaxSource": "../banks/LoadGrid?CountryId=" + 0,
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
                "data": "CountryName",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "CurrencyName",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "AmountLimit",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "NumberOfTransaction",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "Record_Status",
                "render": function (data, type, row) {
                    var toggleHtml = data == 'A' ? '<label id="label_' + row.UID + '" class="toggle"><input type="checkbox" checked><span class="slider"></span></label>' : '<label id="label_' + row.UID + '" class="toggle"><input type="checkbox"><span class="slider"></span></label>';
                    var labelHtml = data == 'A' ? '<span class="label label-success label-xs">Activated</span>' : '<span class="label label-danger label-xs">Blocked</span>';
                    return toggleHtml + '<br>' + labelHtml;
                },
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "UID",
                "render": function (data, type, row) {
                    return '<button id="' + data + '" class="btn btn-warning btn-block btn-xs btn-setLimit" style="width: 80px;">' +
                        '<i class="fa fa-edit"></i>' +
                        ' Set Limit' +
                        '</button>' +
                        '<button id="' + data + '" class="btn btn-primary btn-block btn-xs btn-detail" style="width: 80px; margin-top: 5px;">' +
                        '<i class="fa fa-edit"></i>' +
                        ' Detail ' +
                        '</button>';
                },
                "autoWidth": true
            }
        ]
    });
    $('#btnsearch').on('click', function () {
        var CountryId = $("#Country_Id").val();
        dataTable.ajax.url("../Banks/LoadGrid?CountryId=" + CountryId).load();
    });
};




var handleStaff = function () {
    $('#btn-save').click(function () {
        var uid = $('#UID').val();
        if (validateForm()) {
            $.ajax({
                type: "POST",
                cache: false,
                url: "../banks/UpdateLimits?UID=" + uid + "&AmountLimit=" + $('#AmountLimit').val() + "&NumberOfTransaction=" + $('#NumberOfTransaction').val() + "&NumberOfTransactionMonthly=" + $('#NumberOfTransactionMonthly').val() + "&TxnAmountKWD=" + $('#TransactionAmountKWD').val() + "&TxnAmountFC=" + $('#TransactionAmountFC').val(),
                processData: false,
                contentType: false,
                success: function (value) {
                    $('#setBankLimitsModal').modal('hide');
                    $('#modalTitle').text('');
                    $('#AmountLimit').val('');
                    $('#NumberOfTransaction').val('');
                    $('#NumberOfTransactionMonthly').val('');
                    $('#TransactionAmountKWD').val('');
                    $('#TransactionAmountFC').val('');
                    $('#UID').val('');
                    LoadGridData();
                    if (value === 'update_success') {
                        swal(
                            'Success',
                            'Limits Updated Successfully.',
                            'success'
                        );
                    }
                    else {
                        swal("Error", "Data Not Saved. Please Refresh & Try Again", "error");
                    }

                },
                error: function (xhr, status, error) {
                    $('#setBankLimitsModal').modal('hide');
                    swal(
                        'Warning',
                        data + ' Limits Updated failed.',
                        'warning'
                    );
                }
            });
        }

    });

    function validateForm() {
        var isValid = true;
        $('.required-text').text('');
        var fieldsToValidate = ['AmountLimit', 'NumberOfTransaction', 'NumberOfTransactionMonthly', 'TransactionAmountFC', 'TransactionAmountKWD'];

        var numberOfTransaction = parseFloat($('#NumberOfTransaction').val());
        var numberOfTransactionMonthly = parseFloat($('#NumberOfTransactionMonthly').val());

        if (numberOfTransaction > numberOfTransactionMonthly) {
            isValid = false;
            $('#ValNumberOfTransaction').text('Number of transactions cannot be greater than monthly limit');
        } else {
            fieldsToValidate.forEach(function (fieldName) {
                var fieldValue = $('#' + fieldName).val();
                if (fieldValue === '') {
                    isValid = false;
                    $('#Val' + fieldName).text('Required');
                }
            });
        }

        return isValid;
    }

}






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

$('#btn-sync').on('click', function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../banks/SynchronizeRecords",
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