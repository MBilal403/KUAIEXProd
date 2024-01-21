var IsEditMode = false;

$(document).ready(function () {
    
    handleStaff();
    LoadData();

    $(document).ajaxStart(function () {
        $(window).scrollTop(0);
        $("#wait").css("display", "block");
    });
    $(document).ajaxComplete(function () {
        $("#wait").css("display", "none");
    });

});

$(".DigitOnly").keypress(function (e) {
    e = e || window.event;
    var charCode = (typeof e.which == "number") ? e.which : e.keyCode;
    // Allow non-printable keys
    if (!charCode || charCode == 8 /* Backspace */) {
        return;
    }

    var typedChar = String.fromCharCode(charCode);

    // Allow the minus sign () if the user enters it first
    if (typedChar != "2" && this.value == "9") {
        return false;
    }
    // Allow the minus sign (9) if the user enters it first
    if (typedChar != "9" && this.value == "") {
        return false;
    }
    // Allow numeric characters
    if (/\d/.test(typedChar)) {
        return;
    }

    // Allow the minus sign (-) if the user enters it first
    if (typedChar == "-" && this.value == "") {
        return;
    }



    // In all other cases, suppress the event
    return false;
});


var LoadData = function () {
    //alert("Testing");
    $.ajax({
        type: "POST",
        cache: false,
        url: "../GeneralSettings/LoadTermsConditions",        
        processData: false,
        contentType: false,
        success: function (Rdata) {
            if (Rdata != 'error') {
               
                var obj = JSON.parse(Rdata);
                //alert(obj[0].Id + "-" + obj[0].Description);
                $('#Id').val(obj[0].Id);
                $('#Title').val(obj[0].Title);
                $('#Description').val(obj[0].Description);
               

                $('#btn-save').html("<i class='fa fa-save'></i> Update");
                IsEditMode = true;
                //if (obj.Content_Type) {
                //    $("#Content_Type").iCheck('check');
                //}
                //else {
                //    $("#Content_Type").iCheck('uncheck');
                //}

                $(window).scrollTop(0);
            }
        }
    });
}

//handle stuff
var handleStaff = function () {
    $('.frmAddUsers').validate({
        errorElement: 'span', //default input error message container
        errorClass: 'help-block', // default input error message class
        focusInvalid: false, // do not focus the last invalid input
        ignore: "",
        rules: {
            Title: {
                required: true,
                maxlength: 50
            },
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
            //alert($(".frmAddUsers").serialize());
            //$('#btn-save').attr('disabled', 'true');
            //$(".frmAddUsers :disabled").removeAttr('disabled');
            if (!IsEditMode) {



            }



            else {
                $.post(
                    "../GeneralSettings/updateTermsConditions",
                    $(".frmAddUsers").serialize(),
                    function (value) {
                        if (value == 'exist') {
                            swal(
                                'Warning',
                                'Terms and condition Name Already Exist!',
                                'warning'
                            )
                            return;
                        }
                        if (value != 'error') {
                            swal(
                                'Success',
                                'Terms and condition Updated Successfully!',
                                'success'
                            )
                            //Reset();
                            //$('#btn-save').removeAttr('disabled');

                            //$('#tblUsers').DataTable().clear().draw;
                            //LoadGridData();
                        }
                        else {
                            swal("Error", "Data not updated!!", "error")
                            //$('#btn-save').removeAttr('disabled');
                        }
                    },
                    "text"
                );
            }
            return false;

        }

    });
    $('.frmAddUsers').keypress(function (e) {
        if (e.which == 13) {
            if ($('.frmAddUsers').validate().form()) {
                $('.frmAddUsers').submit();
            }
            return false;
        }

    });
    jQuery('#btn-save').click(function () {
        if ($('.frmAddUsers').validate().form()) {
            $('.frmAddUsers').submit();
        }
    });
}




//load grid

//reset values





// For number validation

// for loading User
