var IsEditMode = false;

$(document).ready(function () {

    handleStaff();
    LoadData();
    LoadGridData();

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
        url: "../GeneralSettings/LoadContactUs",
        processData: false,
        contentType: false,
        success: function (Rdata) {
            if (Rdata != 'error') {

                var obj = JSON.parse(Rdata);
                //alert(obj[0].Id + "-" + obj[0].Description);
                $('#Id').val(obj[0].Id);
                $('#ContactNo').val(obj[0].ContactNo);
                $('#Email').val(obj[0].Email);
                $('#Address').val(obj[0].Address);
                $('#CustomerService').val(obj[0].CustomerService);

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
                    "../GeneralSettings/updateContactUs",
                    $(".frmAddUsers").serialize(),
                    function (value) {
                        if (value == 'exist') {
                            swal(
                                'Warning',
                                'Contact  Name Already Exist!',
                                'warning'
                            )
                            return;
                        }
                        if (value != 'error') {
                            swal(
                                'Success',
                                'Contact Us Updated Successfully!',
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
var LoadGridData = function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../GeneralSettings/LoadGridCustomerQuries",
        processData: false,
        contentType: false,
        success: function (data) {
            data = JSON.parse(data);
            $('#tblUsers').DataTable().destroy();
            var html = '';
            for (var i = 0; i < data.length; i++) {
                var obj = data[i];



                html += '<tr>';

                html += '<td class="hidden">' + obj.Id + '</td>';

                if (obj.Name != null) {
                    html += '<td>' + obj.Name + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }
                if (obj.Email != null) {
                    html += '<td>' + obj.Email + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }
                if (obj.PhoneNo != null) {
                    html += '<td>' + obj.PhoneNo + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }

                if (obj.Message != null) {
                    html += '<td>' + obj.Message + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }
               
                

               

                //html += '<td>';
                //html += '<button id=' + obj.UID + ' class="btn btn-warning btn-block btn-xs btn-edit" style="width: 80px;">';
                //html += '<i class="fa fa-edit"></i>';
                //html += ' Edit';
                //html += ' </button>';
                //html += ' </td>';

                //html += ' <td>';
                //html += '<a href="../UserRights/Index/?UID=' + obj.UID + '" class="btn btn-info btn-block btn-xs" style="width: 80px;">';
                //html += '<i class="fa fa-users"></i>';
                //html += ' Rights';
                //html += ' </a>';
                //html += ' </td>';

                html += '</tr>'
            }
            $("#tblbody").append(html);
            $('#tblUsers').DataTable().draw();
        }
    });
}
//reset values





// For number validation

// for loading User
