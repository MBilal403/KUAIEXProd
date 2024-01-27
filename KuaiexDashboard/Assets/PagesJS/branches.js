var IsEditMode = false;

$(document).ready(function () {
    $('#tblbank').DataTable({ responsive: true });
    handleStaff();
    LoadGridData();
    LoadCountry();
    LoadCurrency();

    $(document).ajaxStart(function () {
        $(window).scrollTop(0);
        $("#wait").css("display", "block");
    });
    $(document).ajaxComplete(function () {
        $("#wait").css("display", "none");
    });

    $('#Bank_Id').chosen();
});

$(".DigitOnly").keypress(function (e) {
    e = e || window.event;
    var charCode = (typeof e.which === "number") ? e.which : e.keyCode;
    // Allow non-printable keys
    if (!charCode || charCode === 8 /* Backspace */) {
        return;
    }

    var typedChar = String.fromCharCode(charCode);

    // Allow the minus sign () if the user enters it first
    if (typedChar !== "2" && this.value === "9") {
        return false;
    }
    // Allow the minus sign (9) if the user enters it first
    if (typedChar !== "9" && this.value === "") {
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
        url: "../Banks/Edit",
        data: data,
        processData: false,
        contentType: false,
        success: function (Rdata) {
            if (Rdata != 'error') {
                var obj = JSON.parse(Rdata);

                $('#UID').val(obj.UID);
                $('#English_Name').val(obj.English_Name);
                $('#Arabic_Name').val(obj.Arabic_Name);
                $('#Address_Line3').val(obj.Address_Line3);

                $('#Country_Id').val(obj.Country_Id);
                $('#Currency_Id').val(obj.Currency_Id).prop('disabled', 'true').trigger("chosen:updated");
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

$("#Record_Status").on('ifChecked', function (event) {
    $(this).closest("input").attr('value', 1);
});
$("#Record_Status").on('ifUnchecked', function (event) {
    $(this).closest("input").attr('value', 0);
});

//handle stuff
var handleStaff = function () {
    $('.frmAddbank').validate({
        errorElement: 'span', //default input error message container
        errorClass: 'help-block', // default input error message class
        focusInvalid: false, // do not focus the last invalid input
        ignore: "",
        rules: {
            English_Name: {
                required: true,
                maxlength: 50
            },
            Arabic_Name: {
                required: true,
                maxlength: 50
            },
            Country_Id: {
                required: true,
                maxlength: 50
            },
            Currency_Id: {
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
            $(".frmAddbank :disabled").removeAttr('disabled');
            if (!IsEditMode) {
                // alert($(".frmAddbank").serialize());
                $.post(
                    "../Banks/AddBank",
                    $(".frmAddbank").serialize(),
                    function (value) {
                        if (value == 'exist') {
                            swal(
                                'Warning',
                                'English Name Already Exist!',
                                'warning'
                            )
                            return;
                        }
                        if (value == 'noallowbank') {
                            swal('Warning', 'Your Are Not Allow To Add More bank ', 'warning')
                            Reset();
                            return;
                        }
                        if (value != 'error') {
                            swal(
                                'Success',
                                'Bank Saved Successfully!',
                                'success'
                            )
                            Reset();
                            $('#btn-save').removeAttr('disabled');
                            $('#tblbank').DataTable().clear().draw;
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
                    "../Banks/EditBank",
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

                            $('#tblbank').DataTable().clear().draw;
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

    jQuery('#btn-save').click(function () {

        if ($('.frmAddbank').validate().form()) {
            $('.frmAddbank').submit();
        }
    });
}

$(document).on('click', '.btnsearch', function () {

    var Country = $("#Country_Id").val();
    var Bank = $("#Bank_Id").val();

    //alert($("#Country_Id").val());
    $('#tblbank').DataTable().destroy();
    $("#tblbody").html('');
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Branches/LoadGrid?Country=" + Country + "&Bank=" + Bank,
        processData: false,
        contentType: false,
        success: function (data) {
            data = JSON.parse(data);
            $('#tblbank').DataTable().destroy();
            var html = '';
            for (var i = 0; i < data.length; i++) {
                var obj = data[i];



                html += '<tr>';

                html += '<td class="hidden">' + obj.UID + '</td>';

                if (obj.English_Name != null) {
                    html += '<td>' + obj.English_Name + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }

                if (obj.Bank_Name != null) {
                    html += '<td>' + obj.Bank_Name + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }
                if (obj.Address_Line1 != null || obj.Address_Line2 != null || obj.Address_Line3 != null) {
                    html += '<td>' + obj.Address_Line1 + ' ' + obj.Address_Line2 + ' ' + obj.Address_Line3 + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }


                if (obj.Country_Name != null) {
                    html += '<td>' + obj.Country_Name + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }
                if (obj.Currency != null) {
                    html += '<td>' + obj.Currency + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }
                if (obj.City_Name != null) {
                    html += '<td>' + obj.City_Name + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }

                if (obj.EMail_Address1 != null) {
                    html += '<td>' + obj.EMail_Address1 + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }

                //if (obj.Record_Status == 1) {
                //    html += '<td><span class="label label-success label-xs">Active</span></td>';
                //}
                //else {
                //    html += '<td><span class="label label-danger label-xs">In Active</span></td>';
                //}


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
            $('#tblbank').DataTable().draw();
        }
    });
})



$("#Country_Id").on('change', function () {
    var CountryId = this.value;
    console.log(CountryId);
    LoadBanks(CountryId);
});

//load grid
var LoadGridData = function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Branches/LoadGrid1",
        processData: false,
        contentType: false,
        success: function (data) {
            data = JSON.parse(data);
            $('#tblbank').DataTable().destroy();
            var html = '';
            for (var i = 0; i < data.length; i++) {
                var obj = data[i];

                html += '<tr>';

                html += '<td class="hidden">' + obj.UID + '</td>';

                if (obj.English_Name != null) {
                    html += '<td>' + obj.English_Name + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }

                if (obj.Bank_Name != null) {
                    html += '<td>' + obj.Bank_Name + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }
                if (obj.Address_Line1 != null || obj.Address_Line2 != null || obj.Address_Line3 != null) {
                    html += '<td>' + obj.Address_Line1 + ' ' + obj.Address_Line2 + ' ' + obj.Address_Line3 + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }


                if (obj.Country_Name != null) {
                    html += '<td>' + obj.Country_Name + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }
                if (obj.Currency != null) {
                    html += '<td>' + obj.Currency + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }
                if (obj.City_Name != null) {
                    html += '<td>' + obj.City_Name + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }

                if (obj.EMail_Address1 != null) {
                    html += '<td>' + obj.EMail_Address1 + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }

                //if (obj.Record_Status == 1) {
                //    html += '<td><span class="label label-success label-xs">Active</span></td>';
                //}
                //else {
                //    html += '<td><span class="label label-danger label-xs">In Active</span></td>';
                //}


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
            $('#tblbank').DataTable().draw();
        }
    });
}

//reset values
function Reset() {
    IsEditMode = false;
    LoadUserType();
    $('#English_Name').val('');
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
                $el.append('<option value="">' + "Select Country" + '</option>');
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
                $el.append('<option value="">' + "Select Bank" + '</option>');
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

var LoadCurrency = function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Banks/LoadCurrency",
        processData: false,
        contentType: false,
        success: function (data) {
            var sch = JSON.parse(data);

            var $el = $('#Currency_Id');
            $el.empty();
            if (sch.length > 0) {
                $el.append('<option value="">' + "Select Currency" + '</option>');
                $.each(sch, function (idx, obj) {
                    $el.append('<option value="' + obj.Currency_Id + '">' + obj.Name + '</option>');
                });
            }
            else {
                $el.append('<option value="">' + "Select Currency" + '</option>');
            }
            $el.trigger("liszt:updated");
            $el.chosen();
        }
    });
}