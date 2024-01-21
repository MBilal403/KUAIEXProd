var IsEditMode = false;

$(document).ready(function () {
    $('#tblUsers').DataTable({ responsive: true });
    handleStaff();
    LoadGridData();
    LoadCity();

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
    if (!charCode || charCode == 8 /* Backspace */) {
        return;
    }

    var typedChar = String.fromCharCode(charCode);

    if (typedChar != "2" && this.value == "9") {
        return false;
    }

    if (typedChar != "9" && this.value == "") {
        return false;
    }

    if (/\d/.test(typedChar)) {
        return;
    }

    if (typedChar == "-" && this.value == "") {
        return;
    }

    return false;
});

// Edit method
$(document).on('click', '.btn-edit', function () {
    var uid = $(this).attr('id');
    var data = new FormData();
    data.append("UID", uid);

    $.ajax({
        type: "POST",
        cache: false,
        url: "../country/Edit",
        data: data,
        processData: false,
        contentType: false,
        success: function (Rdata) {
            if (Rdata != 'error') {
                var obj = JSON.parse(Rdata);

                $('#UID').val(obj.UID);
                $('#Name').val(obj.Name);
                $('#Nationality').val(obj.Nationality);
                $('#Alpha_2_Code').val(obj.Alpha_2_Code);
                $('#Alpha_3_Code').val(obj.Alpha_3_Code);
                $('#Country_Dialing_Code').val(obj.Country_Dialing_Code);
                $('#Comission').val(obj.Comission);
                $('#City_Id').val(obj.City_Id).prop('Enable', 'true').trigger("chosen:updated");
                $('#btn-save').html("<i class='fa fa-save'></i> Update");
                IsEditMode = true;

                if (obj.Status) {
                    $("#Status").iCheck('check');
                } else {
                    $("#Status").iCheck('uncheck');
                }

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

$("#Status").on('ifChecked', function (event) {
    $(this).closest("input").attr('value', 1);
});

$("#Status").on('ifUnchecked', function (event) {
    $(this).closest("input").attr('value', 0);
});

// Handle stuff
var handleStaff = function () {
    $('.frmAddUsers').validate({
        errorElement: 'span',
        errorClass: 'help-block',
        focusInvalid: false,
        ignore: "",
        rules: {
            Name: {
                required: true,
                maxlength: 50
            },
            Nationality: {
                required: true,
                maxlength: 50
            },
            CityId: {
                required: true,
                maxlength: 50
            },
        },
        invalidHandler: function (event, validator) { },
        highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
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
            $(".frmAddUsers :disabled").removeAttr('disabled');
            if (!IsEditMode) {
                $.post(
                    "../country/AddCountry",
                    $(".frmAddUsers").serialize(),
                    function (value) {
                        if (value != 'error') {
                            swal(
                                'Success',
                                'User Saved Successfully!',
                                'success'
                            );
                            Reset();
                            $('#btn-save').removeAttr('disabled');
                            $('#tblUsers').DataTable().clear().draw;
                            LoadGridData();
                        } else {
                            swal("Error", "Data Not Saved. Please Refresh & Try Again", "error");
                            $('#btn-save').removeAttr('disabled');
                        }
                    },
                    "text"
                );
            } else {
                $.post(
                    "../country/EditCountry",
                    $(".frmAddUsers").serialize(),
                    function (value) {
                        if (value == 'exist') {
                            swal(
                                'Warning',
                                'User Name Already Exist!',
                                'warning'
                            );
                            return;
                        }
                        if (value != 'error') {
                            swal(
                                'Success',
                                'User Updated Successfully!',
                                'success'
                            );
                            Reset();
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

// Load grid
var LoadGridData = function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../country/LoadGrid",
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
                } else {
                    html += '<td>-</td>';
                }
                if (obj.Nationality != null) {
                    html += '<td>' + obj.Nationality + '</td>';
                } else {
                    html += '<td>-</td>';
                }
                if (obj.Alpha_2_Code != null) {
                    html += '<td>' + obj.Alpha_2_Code + '</td>';
                } else {
                    html += '<td>-</td>';
                }
                if (obj.City != null) {
                    html += '<td>' + obj.City + '</td>';
                } else {
                    html += '<td>-</td>';
                }
                if (obj.Status == 'A') {
                    html += '<td><span class="label label-success label-xs">Active</span></td>';
                } else {
                    html += '<td><span class="label label-danger label-xs">In Active</span></td>';
                }
                html += '<td>';
                html += '<button id=' + obj.UID + ' class="btn btn-warning btn-block btn-xs btn-edit" style="width: 80px;">';
                html += '<i class="fa fa-edit"></i>';
                html += ' Edit';
                html += ' </button>';
                html += ' </td>';
                html += '</tr>';
            }
            $("#tblbody").append(html);
            $('#tblUsers').DataTable().draw();
        }
    });
}

// Reset values
function Reset() {
    IsEditMode = false;
    LoadCity();
    $('#Name').val('');
    $('#City_Id').val('');
    $('#Country_Dialing_Code').val('');
    $('#Alpha_3_Code').val('');
    $('#Alpha_2_Code').val('');
    $('#Nationality').val('');
    $('#Comission').val('');
    $("#Status").iCheck('uncheck');
    $('#btn-save').html("<i class='fa fa-save'></i> Save");
    $('#City_Id').val("").trigger("chosen:updated");
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
    } else {
        return false;
    }
}

// Load City
var LoadCity = function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Country/LoadCity",
        processData: false,
        contentType: false,
        success: function (data) {
            var sch = JSON.parse(data);
            var $el = $('#City_Id');
            $el.empty();
            if (sch.length > 0) {
                $el.append('<option value="">' + "Select City" + '</option>');
                $.each(sch, function (idx, obj) {
                    $el.append('<option value="' + obj.Id + '">' + obj.Name + '</option>');
                });
            } else {
                $el.append('<option value="">' + "Select City" + '</option>');
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
        url: "../Country/SynchronizeRecords",
        processData: false,
        contentType: false,
        success: function (data) {
            swal(
                'Success',
                data + ' Records Synchronized Successfully.',
                'success'
            );
        }
    });
});
