
var IsEditMode = false;

$(document).ready(function () {

    handleStaff();

    $(document).ajaxStart(function () {
        $(window).scrollTop(0);
        $("#wait").css("display", "block");
    });
    $(document).ajaxComplete(function () {
        $("#wait").css("display", "none");
    });
});

//handle stuff
var handleStaff = function () {
    $('.frmAddBeneValidation').validate({
        errorElement: 'span', //default input error message container
        errorClass: 'help-block', // default input error message class
        focusInvalid: false, // do not focus the last invalid input
        ignore: "",
        rules: {
            FieldId0: {
                required: true,
                minlength: 1,
                maxlength: 2
            },
            FieldId1: {
                required: true,
                minlength: 1,
                maxlength: 2
            },
            FieldId2: {
                required: true,
                minlength: 1,
                maxlength: 2
            },
            FieldId3: {
                required: true,
                minlength: 1,
                maxlength: 2
            },
            FieldId4: {
                required: true,
                minlength: 1,
                maxlength: 2
            },
            FieldId5: {
                required: true,
                minlength: 1,
                maxlength: 2
            },
            FieldId6: {
                required: true,
                minlength: 1,
                maxlength: 2
            },
            FieldId7: {
                required: true,
                minlength: 1,
                maxlength: 2
            },
            FieldId8: {
                required: true,
                minlength: 1,
                maxlength: 2
            },
            FieldId9: {
                required: true,
                minlength: 1,
                maxlength: 2
            },
            FieldId10: {
                required: true,
                minlength: 1,
                maxlength: 2
            },
            FieldId11: {
                required: true,
                minlength: 1,
                maxlength: 2
            },
            FieldId12: {
                required: true,
                minlength: 1,
                maxlength: 2
            },
            FieldId13: {
                required: true,
                minlength: 1,
                maxlength: 2
            },
            FieldId14: {
                required: true,
                minlength: 1,
                maxlength: 2
            },
            FieldName0: {
                required: true,                
                maxlength: 200
            },
            FieldName1: {
                required: true,
                maxlength: 200
            },
            FieldName2: {
                required: true,
                maxlength: 200
            },
            FieldName3: {
                required: true,
                maxlength: 200
            },
            FieldName4: {
                required: true,
                maxlength: 200
            },
            FieldName5: {
                required: true,
                maxlength: 200
            },
            FieldName6: {
                required: true,
                maxlength: 200
            },
            FieldName7: {
                required: true,
                maxlength: 200
            },
            FieldName8: {
                required: true,
                maxlength: 200
            },
            FieldName9: {
                required: true,
                maxlength: 200
            },
            FieldName10: {
                required: true,
                maxlength: 200
            },
            FieldName11: {
                required: true,
                maxlength: 200
            },
            FieldName12: {
                required: true,
                maxlength: 200
            },
            FieldName13: {
                required: true,
                maxlength: 200
            },
            FieldName14: {
                required: true,
                maxlength: 200
            },
            FieldValidation0: {
                required: true                
            },
            FieldValidation1: {
                required: true
            },
            FieldValidation2: {
                required: true
            },
            FieldValidation3: {
                required: true
            },
            FieldValidation4: {
                required: true
            },
            FieldValidation5: {
                required: true
            },
            FieldValidation6: {
                required: true
            },
            FieldValidation7: {
                required: true
            },
            FieldValidation8: {
                required: true
            },
            FieldValidation9: {
                required: true
            },
            FieldValidation10: {
                required: true
            },
            FieldValidation11: {
                required: true
            },
            FieldValidation12: {
                required: true
            },
            FieldValidation13: {
                required: true
            },
            FieldValidation14: {
                required: true
            }
        },

        invalidHandler: function (event, validator) { //display error alert on form submit

        },

        highlight: function (element) { // hightlight error inputs            
            $(element)
                .closest('.form-group').addClass('has-error'); // set error class to the control group
        },

        success: function (label) {
            label.closest('.form-group').removeClass('has-error');
            label.remove();
        },

        errorPlacement: function (error, element) {
            if (element.closest('.input-icon').size() === 1) {
                error.insertAfter(element.closest('.input-icon'));
            } else {
                error.insertAfter(element);
            }
        },

        submitHandler: function (form) {
            
            $('#btn-save').attr('disabled', 'true');
            $(".frmAddBeneValidation :disabled").removeAttr('disabled');
            if (IsEditMode) {
                $.post(
                    "../BeneficiaryValidation/UpdateBeneValidation",
                    $(".frmAddBeneValidation").serialize(),
                    function (value) {
                        if (value == 'exist') {
                            swal(
                                'Warning',
                                'Customer Name Already Exist!',
                                'warning'
                            );
                            return;
                        }
                        if (value != 'error') {
                            swal(
                                'Success',
                                'Beneficiary Updated Successfully!',
                                'success'
                            );
                            Reset();
                            $('#btn-save').removeAttr('disabled');

                            $('#tblUsers').DataTable().clear().draw;
                            LoadGridData();
                        }
                        else {
                            swal("Error", "Data not updated!!", "error");
                            $('#btn-save').removeAttr('disabled');
                        }
                    },
                    "text"
                );
            }
            return false;

        }

    });
    $('.frmAddBeneValidation').keypress(function (e) {
        if (e.which == 13) {


            if ($('.frmAddBeneValidation').validate().form()) {
                $('.frmAddBeneValidation').submit();
            }
            return false;
        }
    });
    jQuery('#btn-save').click(function () {


        if ($('.frmfrmAddBeneValidationAddUsers').validate().form()) {
            $('.frmAddBeneValidation').submit();
        }
    });
}

$('#RemittanceTypeId').on('change', function () {
    var RemittanceTypeId = $('#RemittanceTypeId').val();

    if (RemittanceTypeId == 1) { // Cash Pickup

    }
    else if (RemittanceTypeId == 2) { // Deposit To Account

    }
});

var LoadValidationData = function (RemittanceTypeId) {
    $("#wait").css("display", "block");
    $.ajax({
        type: "POST",
        cache: false,
        url: "../BeneficiaryValidation/LoadValidationData?RemittanceTypeId=" + RemittanceTypeId,
        processData: false,
        contentType: false,
        async: false,
        success: function (data) {
            var sch = JSON.parse(data);
            var $el1 = $('#Country_Id');
            $el1.empty();
            if (sch.length > 0) {
                $el1.append('<option value="">' + "Select Country" + '</option>');
                $.each(sch, function (idx, obj) {
                    $el1.append('<option value="' + obj.Id + '">' + obj.Name + '</option>');
                });
            }
            else {
                $el1.append('<option value="">' + "Select Country" + '</option>');
            }
            $el1.trigger("liszt:updated");
            $el1.chosen();
        },
        complete: function () {
            $("#wait").css("display", "none");
        }
    });
};
