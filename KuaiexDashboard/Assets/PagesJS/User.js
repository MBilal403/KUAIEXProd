var IsEditMode = false;

$(document).ready(function () {
    $('#tblUsers').DataTable({ responsive: true });
    handleStaff();
    LoadGridData();
    LoadUserType();
    LoadLocation();

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

//edit method
$(document).on('click', '.btn-edit', function () {
    var uid = $(this).attr('id');
    var data = new FormData();
    data.append("UID", uid);
    $.ajax({
        type: "POST",
        cache: false,
        url: "../UserManagement/Edit",
        data: data,
        processData: false,
        contentType: false,
        success: function (Rdata) {
            if (Rdata != 'error') {
                var obj = JSON.parse(Rdata);

                $('#UID').val(obj.UID);
                $('#Name').val(obj.Name);
                $('#UserName').val(obj.UserName);
                $('#UserName').prop('Enable', 'true');
                $('#Email').val(obj.Email);
                $('#Password').val(obj.Password);
                $('#ContactNo').val(obj.ContactNo);
                $('#UserTypeId').val(obj.UserTypeId).prop('Enable', 'true').trigger("chosen:updated");
                $('#btn-save').html("<i class='fa fa-save'></i> Update");
                IsEditMode = true;
                if (obj.Status) {
                    $("#Status").iCheck('check');
                }
                else {
                    $("#Status").iCheck('uncheck');
                }
                $(window).scrollTop(0);
            }
            else {
                ShowErrorAlert("Error", "Some Error Occured!");
                $('#btn-validate').removeAttr('disabled');
            }
        },
        error: function (e) {
        }
    });
});

$("#Status").on('ifChecked', function (event) {
    $(this).closest("input").attr('value', 1);
});
$("#Status").on('ifUnchecked', function (event) {
    $(this).closest("input").attr('value', 0);
});

//handle stuff
var handleStaff = function () {
    $('.frmAddUsers').validate({
        errorElement: 'span', //default input error message container
        errorClass: 'help-block', // default input error message class
        focusInvalid: false, // do not focus the last invalid input
        ignore: "",
        rules: {
            Name: {
                required: true,
                maxlength: 50
            },
            UserName: {
                required: true,
                maxlength: 50
            },
            Password: {
                required: true,
                maxlength: 50
            },
            UserType: {
                required: true,
                maxlength: 20
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
            $('#btn-save').attr('disabled', 'true');
            $(".frmAddUsers :disabled").removeAttr('disabled');
            if (!IsEditMode) {
                //alert($(".frmAddUsers").serialize());
                $.post(
                    "../UserManagement/AddUser",
                    $(".frmAddUsers").serialize(),
                    function (value) {
                        if (value == 'exist') {
                            swal(
                                'Warning',
                                'User Name Already Exist!',
                                'warning'
                            )
                            return;
                        }
                        if (value == 'noallowuser') {
                            swal('Warning', 'Your Are Not Allow To Add More User ', 'warning')
                            Reset();
                            return;
                        }
                        if (value != 'error') {
                            swal(
                                'Success',
                                'User Saved Successfully!',
                                'success'
                            )
                            Reset();
                            $('#btn-save').removeAttr('disabled');
                            $('#tblUsers').DataTable().clear().draw;
                            LoadGridData();

                        }
                        else {
                            swal("Error", "Data Not Saved. Please Refresh & Try Again", "error")
                            $('#btn-save').removeAttr('disabled');
                        }
                    },
                    "text"
                );
            }
            else {
                $.post(
                    "../UserManagement/EditUser",
                    $(".frmAddUsers").serialize(),
                    function (value) {
                        if (value == 'exist') {
                            swal(
                                'Warning',
                                'User Name Already Exist!',
                                'warning'
                            )
                            return;
                        }
                        if (value != 'error') {
                            swal(
                                'Success',
                                'User Updated Successfully!',
                                'success'
                            )
                            Reset();
                            $('#btn-save').removeAttr('disabled');

                            $('#tblUsers').DataTable().clear().draw;
                            LoadGridData();
                        }
                        else {
                            swal("Error", "Data not updated!!", "error")
                            $('#btn-save').removeAttr('disabled');
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
            var CellNo = $("#ContactNo").val();
            if (CellNo.length > 0) {
                if (!CellNumberValidator(CellNo)) {
                    swal("Please provide valid Cell #.");
                    return;
                }
            }

            if ($('.frmAddUsers').validate().form()) {
                $('.frmAddUsers').submit();
            }
            return false;
        }
    });
    jQuery('#btn-save').click(function () {
        var CellNo = $("#ContactNo").val();
        if (CellNo.length > 0) {
            if (!CellNumberValidator(CellNo)) {
                swal("Please provide valid Cell #.");
                return;
            }
        }
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
        url: "../UserManagement/LoadGrid",
        processData: false,
        contentType: false,
        success: function (data) {
            data = JSON.parse(data);
            $('#tblUsers').DataTable().destroy();
            var html = '';
            for (var i = 0; i < data.length; i++) {
                var obj = data[i];



                html += '<tr>';

                html += '<td class="hidden">' + obj.UID + '</td>';

                if (obj.Name != null) {
                    html += '<td>' + obj.Name + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }
                if (obj.UserName != null) {
                    html += '<td>' + obj.UserName + '</td>';
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
                if (obj.ContactNo != null) {
                    html += '<td>' + obj.ContactNo + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }

                if (obj.UserType != null) {
                    html += '<td>' + obj.UserType + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }

                if (obj.Status == 1) {
                    html += '<td><span class="label label-success label-xs">Active</span></td>';
                }
                else {
                    html += '<td><span class="label label-danger label-xs">In Active</span></td>';
                }


                html += '<td>';
                html += '<button id=' + obj.UID + ' class="btn btn-warning btn-block btn-xs btn-edit" style="width: 80px;">';
                html += '<i class="fa fa-edit"></i>';
                html += ' Edit';
                html += ' </button>';
                html += ' </td>';

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
function Reset() {
    IsEditMode = false;
    LoadUserType();
    $('#Name').val('');
    $('#UserTypeId').val('');
    $('#ContactNo').val('');
    $('#UserName').val('');
    $('#Password').val('');
    $('#Email').val('');

    $("#IsActive").iCheck('uncheck');

    $('#btn-save').html("<i class='fa fa-save'></i> Save");
    $('#UserTypeId').val("").trigger("chosen:updated");
    $('#btn-save').removeAttr('disabled');
}

$('#btn-refresh').click(function () {
    Reset();
});


// For number validation
function CellNumberValidator(CellNo) {
    var phoneno = /^[9]{1}[2]{1}[0-9]{10}$/;
    if (CellNo.match(phoneno)) {
        return true;
    }
    else {
        return false;
    }
}
// for loading User
var LoadUserType = function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../UserManagement/LoadUserType",
        processData: false,
        contentType: false,
        success: function (data) {
            var sch = JSON.parse(data);

            var $el = $('#UserTypeId');
            $el.empty();
            if (sch.length > 0) {
                $el.append('<option value="">' + "Select User" + '</option>');
                $.each(sch, function (idx, obj) {
                    $el.append('<option value="' + obj.UserTypeId + '">' + obj.Name + '</option>');
                });
            }
            else {
                $el.append('<option value="">' + "Select User" + '</option>');
            }
            $el.trigger("liszt:updated");
            $el.chosen();
        }
    });
}

