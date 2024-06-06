var IsEditMode = false;

$(document).ready(function () {
    handleStaff();
    LoadGridData();
    LoadCountry();
    LoadCurrency();

    $(document).ajaxStart(function () {

        $("#wait").css("display", "block");
    });
    $(document).ajaxComplete(function () {
        $("#wait").css("display", "none");
    });
    /*   $('#CommissionRate1').on('input', function () {
           validateInput($(this));
       });
       $('#CommissionRate2').on('input', function () {
           validateInput($(this));
       });
       $('#AmountLimit').on('input', function () {
           validateInput($(this));
       });*/


});

function validateInput(input) {
    const value = parseFloat(input.val());
    const decimalCount = (value.toString().split('.')[1] || []).length;
    if (decimalCount > 3) {
        swal("Error", "Maximum 3 digits allowed after the decimal point", "error");
    }
}


// Edit method
$(document).on('click', '.btn-edit', function () {
    var UID = $(this).attr('id');
    var data = new FormData();
    data.append("UID", UID);

    $.ajax({
        type: "POST",
        cache: false,
        url: "../CountryCurrency/Edit",
        data: data,
        processData: false,
        contentType: false,
        success: function (Rdata) {
            if (Rdata != 'error') {
                var obj = JSON.parse(Rdata);
                  $('.required-text').text('');
                $('#Id').val(obj.Id);
                $('#Country_Id').val(obj.Country_Id).prop('Enable', 'true').trigger("chosen:updated");
                $('#Currency_Id').val(obj.Currency_Id).prop('Enable', 'true').trigger("chosen:updated");
                $('#CommissionRate1').val(obj.CommissionRate1);
                $('#CommissionRate2').val(obj.CommissionRate2);
                $('#AmountLimit').val(obj.AmountLimit);
                $('#DisplayOrder').val(obj.DisplayOrder).prop('Enable', 'true').trigger("chosen:updated");
                $('#btn-save').html("<i class='fa fa-save'></i> Update");
                IsEditMode = true;
                $('#Country_Id').prop('disabled', true).trigger('chosen:updated');
                $('#Currency_Id').prop('disabled', true).trigger('chosen:updated');
                $(window).scrollTop(0);
            } else {
                ShowErrorAlert("Error", "Some Error Occurred!");
                $('#btn-validate').removeAttr('disabled');
            }
        },
        error: function (e) {
            // Handle error
        }
    });
});

// Handle stuff
var handleStaff = function () {
    $(".frmAddCountryCurrency").submit(function (event) {
        event.preventDefault();
        if (validateForm()) {
            $('#btn-save').attr('disabled', 'true');
            $(".frmAddCountryCurrency :disabled").removeAttr('disabled');
            if (!IsEditMode) {
                $.post(
                    "../CountryCurrency/AddCountryCurrency",
                    $(".frmAddCountryCurrency").serialize(),
                    function (value) {
                        if (value == 'duplicate_value_exist') {
                            swal("Error", "Country Already Exist", "error");
                            $('#btn-save').removeAttr('disabled');
                        }

                        else if (value != 'error') {
                            swal(
                                'Success',
                                'Country Saved Successfully!',
                                'success'
                            );
                            resetForm();
                            $('#btn-save').removeAttr('disabled');
                            $('#tblUsers').DataTable().clear().draw;
                            LoadGridData();
                        }
                        else {
                            swal("Error", "Data Not Saved. Please Refresh & Try Again", "error");
                            $('#btn-save').removeAttr('disabled');
                        }
                    },
                    "text"
                );
            } else {
                $.post(
                    "../CountryCurrency/EditCountryCurrency",
                    $(".frmAddCountryCurrency").serialize(),
                    function (value) {
                        if (value == 'duplicate_value_exist') {
                            swal(
                                'Warning',
                                'Country Currency Name Already Exist!',
                                'warning'
                            );
                            return;
                        }
                        if (value != 'error') {
                            swal(
                                'Success',
                                'Country Currency Updated Successfully!',
                                'success'
                            );
                            resetForm();
                            $('#btn-save').removeAttr('disabled');
                            $('#tblUsers').DataTable().clear().draw;
                            LoadGridData();
                        } else {
                            swal("Error", "Data not updated!!", "error");
                            $('#btn-save').removeAttr('disabled');
                        }
                    },
                    "text"
                );
            }
        }
    });

function validateForm() {
    var isValid = true;

    $('.required-text').text('');

    var fieldsToValidate = ['Country_Id', 'Currency_Id', 'CommissionRate1', 'AmountLimit', 'CommissionRate2', 'DisplayOrder'];

    fieldsToValidate.forEach(function (fieldName) {
        var fieldValue = $('#' + fieldName).val().trim();

        // Assuming you have a span element with id 'Val' + fieldName to display validation messages
        var validationMessageElement = $('#Val' + fieldName);

        if (fieldValue === '' || (fieldValue === '0' && $('#' + fieldName).is('select'))) {
            isValid = false;
            validationMessageElement.text(' required ');
        } else {
            validationMessageElement.text(''); // Clear any previous validation message
        }
    });

    if (isValid) {
        // Check if CommissionRate2 is not greater than CommissionRate1
        var commissionRate1 = parseFloat($('#CommissionRate1').val());
        var commissionRate2 = parseFloat($('#CommissionRate2').val());

        if (isNaN(commissionRate1) || isNaN(commissionRate2) || commissionRate2 > commissionRate1) {
            isValid = false;
            swal("Error", "Commission Rate 2 must be less than or equal to Commission Rate 1", "error");
        }
    }

    return isValid;
}

    $('.frmAddCountryCurrency').keypress(function (e) {
        if (e.which == 13) {
            if ($('.frmAddCountryCurrency').validate().form()) {
                $('.frmAddCountryCurrency').submit();
            }
            return false;
        }
    });

    jQuery('#btn-save').click(function () {
        if ($('.frmAddCountryCurrency').validate().form()) {
            $('.frmAddCountryCurrency').submit();
        }
    });
}

// Load grid
var LoadGridData = function () {
    $('#tblUsers').DataTable({
        "destroy": true,
        "lengthMenu": [10, 25, 50, 100],
        "sAjaxSource": "../CountryCurrency/LoadGrid",
        "bServerSide": true,
        "bProcessing": true,
        "paging": true,
        "order": [[1, 'asc']],
        "language": {
            "emptyTable": "No record found."
        },
        "columns": [
            {
                "data": "Country_Name",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "Currency_Name",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "CommissionRate1",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "CommissionRate2",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "AmountLimit",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "DisplayOrder",
                "autoWidth": true,
                "searchable": true
            },

            {
                "data": "UID",
                "render": function (data, type, row) {
                    console.log(data);
                    return '<button id=' + data + ' class="btn btn-warning btn-block btn-xs btn-edit" style="width: 80px;">' +
                        '<i class="fa fa-edit"></i>' +
                        ' Edit' +
                        '</button>';
                },
                "autoWidth": true
            }
        ]
    });
};

// Reset values
function resetForm() {
    // Reset input values
    $('.required-text').text('');
    $('#Country_Id').prop('disabled', false).trigger('chosen:updated');
    $('#Currency_Id').prop('disabled', false).trigger('chosen:updated');
    $('.frmAddCountryCurrency input[type="text"]').val('');
    $('.frmAddCountryCurrency input[type="number"]').val('');
    $('.frmAddCountryCurrency select').each(function () {
        // Manually set the selected option to the one with value '1'
        $(this).find('option[value="1"]').prop('selected', true);
    });
    // Reset select values
    $('.frmAddCountryCurrency select:not(#DisplayOrder)').val('');
    $('.chzn').trigger('chosen:updated');
    $('#Currency_Id').chosen();

    var $el = $('#Currency_Id');
    $el.empty();


    $el.trigger("liszt:updated");
    $el.chosen();
    $('#btn-save').html("<i class='fa fa-save'></i> Save");
}

$('#btn-refresh').click(function () {
    resetForm();
});

// Load Country
var LoadCountry = function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../CountryCurrency/LoadCountry",
        processData: false,
        contentType: false,
        success: function (data) {
            var sch = JSON.parse(data);
            var $el = $('#Country_Id');
            $el.empty();
            if (sch.length > 0) {
                $el.append('<option value="">' + "Select Country" + '</option>');
                $.each(sch, function (idx, obj) {
                    $el.append('<option value="' + obj.Id + '">' + obj.Name + '</option>');
                });
            } else {
                $el.append('<option value="">' + "Select Country" + '</option>');
            }
            $el.trigger("liszt:updated");
            $el.chosen();
        }
    });
}

var LoadCurrency = function () {
    $("#wait").css("display", "block");
    $.ajax({
        type: "POST",
        cache: false,
        async: false,
        url: "../CountryCurrency/LoadCurrency",
        processData: false,
        contentType: false,
        async: false,
        success: function (data) {
            var sch = JSON.parse(data);
            console.log(sch);
            var $el = $('#Currency_Id');
            $el.empty();
            if (sch.length > 0) {
                $el.append('<option value="">' + "Select Currency" + '</option>');
                $.each(sch, function (idx, obj) {
                    $el.append('<option value="' + obj.Id + '">' + obj.Name + ' ' + obj.Code + '</option>');
                });
            }
            else {
                $el.append('<option value="">' + "Select Currency" + '</option>');
            }
            $el.trigger("liszt:updated");
            $el.chosen();
        },
        complete: function () {
            $("#wait").css("display", "none");
        }
    });
};

$('#btn-sync').on('click', function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../CountryCurrency/SynchronizeRecords",
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


