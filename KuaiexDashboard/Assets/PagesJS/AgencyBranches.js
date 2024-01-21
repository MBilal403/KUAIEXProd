var IsEditMode = false;

$(document).ready(function () {
    $('#tblUsers').DataTable({ responsive: true });
    handleStaff();
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

//edit method
$(document).on('click', '.btn-edit', function () {
    var uid = $(this).attr('id');
    var data = new FormData();
    data.append("Id", uid);
    $.ajax({
        type: "POST",
        cache: false,
        url: "../AgencyBranches/Edit",
        data: data,
        processData: false,
        contentType: false,
        success: function (Rdata) {
            if (Rdata != 'error') {
                var obj = JSON.parse(Rdata);

                $('#Id').val(obj.Id);
                $('#Name').val(obj.Name);
                $('#ContactNo').val(obj.ContactNo);
                $('#Address').val(obj.Address);
                $('#Longitude').val(obj.Longitude);
                $('#Latitude').val(obj.Latitude);
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

                $.post(
                    "../AgencyBranches/AddAgencyBarnches",
                    $(".frmAddUsers").serialize(),
                    function (value) {
                        if (value == 'exist') {
                            swal(
                                'Warning',
                                'AgencyBranches Name Already Exist!',
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
                                'AgencyBranches Saved Successfully!',
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
                    "../AgencyBranches/EditAgencyBarnches",
                    $(".frmAddUsers").serialize(),
                    function (value) {
                        if (value == 'exist') {
                            swal(
                                'Warning',
                                'AgencyBranches  Already Exist!',
                                'warning'
                            )
                            return;
                        }
                        if (value != 'error') {
                            swal(
                                'Success',
                                'AgencyBranches Updated Successfully!',
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
        url: "../AgencyBranches/LoadGrid",
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
                if (obj.ContactNo != null) {
                    html += '<td>' + obj.ContactNo + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }

                if (obj.Address != null) {
                    html += '<td>' + obj.Address + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }
                if (obj.Longitude != null) {
                    html += '<td>' + obj.Longitude + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }
                if (obj.Latitude != null) {
                    html += '<td>' + obj.Latitude + '</td>';
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
                html += '<button id=' + obj.Id + ' class="btn btn-warning btn-block btn-xs btn-edit" style="width: 80px;">';
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


$('#btn-refresh').click(function () {
    Reset();
});


// For number validation

// for loading User


function Reset() {
    IsEditMode = false;
   
    $('#Name').val('');
    $('#ContactNo').val('');
    $('#Address').val('');
    $('#Longitude').val('');
    $('#Latitude').val('');
    $("#Status").iCheck('uncheck');

    $('#btn-save').html("<i class='fa fa-save'></i> Save");
    $('#btn-save').removeAttr('disabled');
}