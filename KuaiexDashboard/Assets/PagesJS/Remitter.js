var IsEditMode = false;

$(document).ready(function () {
    $('#tblUsers').DataTable({ responsive: true });
    handleStaff();
    KYCCheckBoxes();
    LoadGridData();
    LoadIdentificationType();
    LoadResidencyType();
    LoadCountry();
    LoadNationality();
    LoadQuestions();
    LoadCity();
    LoadExpectTrancationsCount();
    $('#data_2 .input-group.date').datepicker({
        startView: 1,
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        autoclose: true,
        format: "dd/mm/yyyy"
    });


    $(document).ajaxStart(function () {
        $(window).scrollTop(0);
        $("#wait").css("display", "block");
    });
    $(document).ajaxComplete(function () {
        $("#wait").css("display", "none");
    });
});

function KYCCheckBoxes() {
    // Hide additional checkboxes initially
    $("#additionalCheckboxes").hide();

    // Show additional checkboxes when any of the first three checkboxes is checked
    $("[id^='pepcheckbox']").change(function () {
        if ($(this).prop("checked")) {
            $("#additionalCheckboxes").show();
        } else {
            $("#additionalCheckboxes").hide();
        }
    });
}



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
        url: "../Remitter/Edit",
        data: data,
        processData: false,
        contentType: false,
        success: function (Rdata) {
            if (Rdata != 'error') {
                var obj = JSON.parse(Rdata);

                $('#UID').val(obj.UID);
                $('#Name').val(obj.Name);
                $('#Identification_Type').val(obj.Identification_Type).prop('Enable', 'true').trigger("chosen:updated");
                $('#Identification_Number').val(obj.Identification_Number);
                $('#Nationality').val(obj.Nationality).prop('Enable', 'true').trigger("chosen:updated");
                $('#Date_Of_Birth').val(obj.Date_Of_Birth);
                $('#Identification_Expiry_Date').val(obj.Identification_Expiry_Date);
                $('#Occupation').val(obj.Occupation);
                $('#Email_Address').val(obj.Email_Address);
                $('#Mobile_No').val(obj.Mobile_No);
                $('#Gender').val(obj.Gender).prop('Enable', 'true').trigger("chosen:updated");
                $('#Area').val(obj.Area).prop('Enable', 'true').trigger("chosen:updated");
                $('#Block').val(obj.Block);
                $('#Street').val(obj.Street);
                $('#Building').val(obj.Building);
                $('#Floor').val(obj.Floor);
                $('#Flat').val(obj.Flat);
                $('#Identification_Additional_Detail').val(obj.Identification_Additional_Detail);
                $('#Residence_Type').val(obj.Residence_Type).prop('Enable', 'true').trigger("chosen:updated");
                $('#Telephone_No').val(obj.Telephone_No);
                $('#Birth_Place').val(obj.Birth_Place);
                $('#Birth_Country').val(obj.Birth_Country).prop('Enable', 'true').trigger("chosen:updated");
                $('#Expected_Monthly_Trans_Count').val(obj.Expected_Monthly_Trans_Count).prop('Enable', 'true').trigger("chosen:updated");
                $('#Other_Income').val(obj.Other_Income);
                $('#Other_Income_Detail').val(obj.Other_Income_Detail);
                $('#Login_Id').val(obj.Login_Id);
                $('#Password').val(obj.Password);
                $('#Pep_Description').val(obj.Pep_Description);
                $('#Monthly_Income').val(obj.Monthly_Income);
                $('#Monthly_Trans_Limit').val(obj.Monthly_Trans_Limit);
                $('#Yearly_Trans_Limit').val(obj.Yearly_Trans_Limit);
                $('#Compliance_Limit').val(obj.Compliance_Limit);
                $('#Compliance_Trans_Count').val(obj.Compliance_Trans_Count);
                $('#Compliance_Comments').val(obj.Compliance_Comments);
                $('#Compliance_Limit_Expiry').val(obj.Compliance_Limit_Expiry);


                //$('#City_Id').val(obj.City_Id).prop('disabled', 'true').trigger("chosen:updated");
                //$('#City_Id').val(obj.City_Id).prop('disabled', 'true').trigger("chosen:updated");
                //$('#City_Id').val(obj.City_Id).prop('disabled', 'true').trigger("chosen:updated");

                $('#btn-save').html("<i class='fa fa-save'></i> Update");
                IsEditMode = true;
                if (obj.IsReviwed) {
                    $("#IsReviwed").iCheck('check');
                }
                else {
                    $("#IsReviwed").iCheck('uncheck');
                }
                if (obj.IsVerified) {
                    $("#IsVerified").iCheck('check');
                }
                else {
                    $("#IsVerified").iCheck('uncheck');
                }
                $(window).scrollTop(0);

                LoadSecurityQuestions(obj.Customer_Id);

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

//edit method
$(document).on('click', '.btn-bene', function () {
    var uid = $(this).attr('id');    
    window.location = "../Beneficiary/Index?UID=" + uid;
});

$("#IsReviwed").on('ifChecked', function (event) {
    $(this).closest("input").attr('value', true);
});
$("#IsReviwed").on('ifUnchecked', function (event) {
    $(this).closest("input").attr('value', false);
});


$("#IsVerified").on('ifChecked', function (event) {
    $(this).closest("input").attr('value', 1);
});
$("#IsVerified").on('ifUnchecked', function (event) {
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
            other_Detail: {
                required: true,
                maxlength: 50
            },
            Name: {
                required: true,
                maxlength: 50
            },
            Identification_Type: {
                required: true,
                maxlength: 50
            },
            Identification_Expiry_Date: {
                required: true,
                maxlength: 50
            },
            Nationality: {
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

            $('#btn-save').attr('disabled', 'true');
            $(".frmAddUsers :disabled").removeAttr('disabled');
            if (!IsEditMode) {

                $.post(
                    "../Remitter/AddCustomer?QId1=" + $('#Security_Question_Id_1').val() + "&QId2=" + $('#Security_Question_Id_2').val() + "&QId3=" + $('#Security_Question_Id_3').val() + "&Answer1=" + $('#Answer1').val() + "&Answer2=" + $('#Answer2').val() + "&Answer3=" + $('#Answer3').val(),
                    $(".frmAddUsers").serialize(),
                    function (value) {
                        if (value == 'exist') {
                            swal(
                                'Warning',
                                'Customer Name Already Exist!',
                                'warning'
                            );
                            return;
                        }
                        if (value === 'noallowuser') {
                            swal('Warning', 'Your Are Not Allow To Add More Customer ', 'warning')
                            Reset();
                            return;
                        }
                        if (value !== 'error') {
                            swal(
                                'Success',
                                'Customer Saved Successfully!',
                                'success'
                            );
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
                    "../Remitter/EditCustomer",
                    $(".frmAddUsers").serialize(),
                    function (value) {
                        if (value == 'exist') {
                            swal(
                                'Warning',
                                'Customer Name Already Exist!',
                                'warning'
                            )
                            return;
                        }
                        if (value != 'error') {
                            swal(
                                'Success',
                                'Customer Updated Successfully!',
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
        url: "../Remitter/LoadGrid",
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
                if (obj.Description != null) {
                    html += '<td>' + obj.Description + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }

                if (obj.Identification_Number != null) {
                    html += '<td>' + obj.Identification_Number + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }
                if (obj.Email_Address != null) {
                    html += '<td>' + obj.Email_Address + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }

                if (obj.Identification_Expiry_Date != null) {
                    html += '<td>' + obj.Identification_Expiry_Date + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }

                //if (obj.Occupation != null) {
                //    html += '<td>' + obj.Occupation + '</td>';
                //}
                //else {
                //    html += '<td>-</td>';
                //}
                //alert(obj.IsReviwed);
                if (obj.IsReviwed == 1) {
                    html += '<td><span class="label label-success label-xs">Reviewed</span></td>';
                }
                else {
                    html += '<td><span class="label label-danger label-xs">Under Review</span></td>';
                }

                html += '<td>';
                html += '<button id=' + obj.UID + ' class="btn btn-warning btn-block btn-xs btn-edit" style="width: 80px;">';
                html += '<i class="fa fa-edit"></i>';
                html += ' Edit';
                html += ' </button>';

                if (obj.IsBlocked === 1) {
                    html += '<button id=' + obj.UID + ' class="btn btn-info btn-block btn-xs btn-unblock" style="width: 80px;">';
                    html += '<i class="fa fa-users"></i>';
                    html += ' Un Block';
                    html += ' </button>';
                }

                html += '<button id=' + obj.UID + ' class="btn btn-warning btn-block btn-xs btn-bene" style="width: 80px;">';
                html += '<i class="fa fa-edit"></i>';
                html += ' Beneficiaries';
                html += ' </button>';

                html += ' </td>';
                html += '</tr>';
            }
            $("#tblbody").append(html);
            $('#tblUsers').DataTable().draw();
        }
    });
}


$(document).on('click', '.btn-unblock', function () {
    //alert("Testing");
    swal({
        title: 'Are you sure?',
        text: "You want to Unblock this Customer",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, UnBlock it!'
    })
        .then(function () {
            $.ajax({
                type: "POST",
                cache: false,
                url: "../Remitter/UnBlockCustomer?UID=" + $('.btn-unblock').attr('id'),
                processData: false,
                contentType: false,
                success: function (data) {

                    var html = '';
                    {
                        html += '<tr>';
                        if (obj.IsBlocked === 0 || obj.InvalidTryCount === 0) {
                            swal(
                                'Success',
                                'Customer Unblock Successfully!',
                                'success'
                            );
                        }
                        else {
                            status = 'Error';
                        }
                        html += '</tr>';
                    }
                }
            });
        });
});

//reset values
function Reset() {
    IsEditMode = false;
    LoadIdentificationType();
    LoadResidencyType();
    LoadCountry();
    LoadQuestions();
    LoadCity();
    LoadExpectTrancationsCount();

    $('#Name').val('');
    $('#Identification_Type').val('');
    $('#Identification_Number').val('');
    $('#Nationality').val('');
    $('#Date_Of_Birth').val('');
    $('#Identification_Expiry_Date').val('');
    $('#Occupation').val('');
    $('#Mobile_No').val('');
    $('#Area').val('');
    $('#Block').val('');
    $('#Street').val('');
    $('#Building').val('');
    $('#Floor').val('');
    $('#Flat').val('');
    $('#Identification_Additional_Detail').val('');
    $('#Residence_Type').val('');
    $('#Telephone_No').val('');
    $('#Birth_Place').val('');
    $('#Birth_Country').val('');
    $('#Expected_Monthly_Trans_Count').val('');
    $('#Other_Income').val('');
    $('#Email_Address').val('');

    $('#Login_Id').val('');
    $('#Password').val('');
    $('#Pep_Description').val('');
    $('#Monthly_Income').val('');
    $('#Monthly_Trans_Limit').val('');
    $('#Yearly_Trans_Limit').val('');
    $('#Compliance_Limit').val('');
    $('#Compliance_Trans_Count').val('');
    $('#Compliance_Comments').val('');
    $('#Compliance_Limit_Expiry').val('');

    $('#Security_Question_Id_1').val('');
    $('#Answer1').val('');
    $('#Security_Question_Id_2').val('');
    $('#Answer2').val('');
    $('#Security_Question_Id_3').val('');
    $('#Answer3').val('');


    $("#IsVerified").iCheck('uncheck');
    $("#IsReviwed").iCheck('uncheck');

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


function LoadSecurityQuestions(Customer_Id) {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Remitter/LoadSecurityQuestions?Customer_Id=" + Customer_Id,
        processData: false,
        contentType: false,
        success: function (data) {
            var obj = JSON.parse(data);

            $('#Security_Question_Id_1').val(obj[0].Question_Id).trigger("chosen:updated");
            $('#Answer1').val(obj[0].Answer);
            $('#Security_Question_Id_2').val(obj[1].Question_Id).trigger("chosen:updated");
            $('#Answer2').val(obj[1].Answer);
            $('#Security_Question_Id_3').val(obj[2].Question_Id).trigger("chosen:updated");
            $('#Answer3').val(obj[2].Answer);
        }
    });
}

// for loading User
var LoadIdentificationType = function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Remitter/LoadIdentificationType",
        processData: false,
        contentType: false,
        success: function (data) {
            var sch = JSON.parse(data);

            var $el = $('#Identification_Type');
            $el.empty();
            if (sch.length > 0) {
                $el.append('<option value="">' + "Select Identification Type" + '</option>');
                $.each(sch, function (idx, obj) {
                    $el.append('<option value="' + obj.Id + '">' + obj.Description + '</option>');
                });
            }
            else {
                $el.append('<option value="">' + "Select  Identification Type" + '</option>');
            }
            $el.trigger("liszt:updated");
            $el.chosen();
        }
    });
}

var LoadResidencyType = function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Remitter/LoadResidencyType",
        processData: false,
        contentType: false,
        success: function (data) {
            var sch = JSON.parse(data);

            var $el = $('#Residence_Type');
            $el.empty();
            if (sch.length > 0) {
                $el.append('<option value="">' + "Select Residency Type" + '</option>');
                $.each(sch, function (idx, obj) {
                    $el.append('<option value="' + obj.Id + '">' + obj.Name + '</option>');
                });
            }
            else {
                $el.append('<option value="">' + "Select  Residency Type" + '</option>');
            }
            $el.trigger("liszt:updated");
            $el.chosen();
        }
    });
}

var LoadCountry = function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Remitter/LoadCountry",
        processData: false,
        contentType: false,
        success: function (data) {
            var sch = JSON.parse(data);

            var $el = $('#Birth_Country');
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

var LoadNationality = function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Remitter/LoadCountry",
        processData: false,
        contentType: false,
        success: function (data) {
            var sch = JSON.parse(data);

            var $el = $('#Nationality');
            $el.empty();
            if (sch.length > 0) {
                $el.append('<option value="">' + "Select Nationality" + '</option>');
                $.each(sch, function (idx, obj) {
                    $el.append('<option value="' + obj.Nationality + '">' + obj.Nationality + '</option>');
                });
            }
            else {
                $el.append('<option value="">' + "Select Nationality" + '</option>');
            }
            $el.trigger("liszt:updated");
            $el.chosen();
        }
    });
}

var LoadCity = function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Remitter/LoadCity",
        processData: false,
        contentType: false,
        success: function (data) {
            var sch = JSON.parse(data);

            var $el = $('#Area');
            $el.empty();
            if (sch.length > 0) {
                $el.append('<option value="">' + "Select Area" + '</option>');
                $.each(sch, function (idx, obj) {
                    $el.append('<option value="' + obj.Name + '">' + obj.Name + '</option>');
                });
            }
            else {
                $el.append('<option value="">' + "Select Area" + '</option>');
            }
            $el.trigger("liszt:updated");
            $el.chosen();
        }
    });
}

var LoadQuestions = function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Remitter/LoadQuestions",
        processData: false,
        contentType: false,
        success: function (data) {
            var sch = JSON.parse(data);

            var $el1 = $('#Security_Question_Id_1');
            var $el2 = $('#Security_Question_Id_2');
            var $el3 = $('#Security_Question_Id_3');
            $el1.empty();
            $el2.empty();
            $el3.empty();
            if (sch.length > 0) {
                $el1.append('<option value="">' + "Select Questions" + '</option>');
                $el2.append('<option value="">' + "Select Questions" + '</option>');
                $el3.append('<option value="">' + "Select Questions" + '</option>');
                $.each(sch, function (idx, obj) {
                    $el1.append('<option value="' + obj.Id + '">' + obj.Question + '</option>');
                    $el2.append('<option value="' + obj.Id + '">' + obj.Question + '</option>');
                    $el3.append('<option value="' + obj.Id + '">' + obj.Question + '</option>');
                });
            }
            else {
                $el1.append('<option value="">' + "Select Questions" + '</option>');
                $el2.append('<option value="">' + "Select Questions" + '</option>');
                $el3.append('<option value="">' + "Select Questions" + '</option>');
            }
            $el1.trigger("liszt:updated");
            $el2.trigger("liszt:updated");
            $el3.trigger("liszt:updated");
            $el1.chosen();
            $el2.chosen();
            $el3.chosen();
        }
    });
}

var LoadExpectTrancationsCount = function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Remitter/LoadExpectTrancationsCount",
        processData: false,
        contentType: false,
        success: function (data) {
            var sch = JSON.parse(data);

            var $el = $('#Expected_Monthly_Trans_Count');
            $el.empty();
            if (sch.length > 0) {
                $el.append('<option value="">' + "Select Expected Monthly Trans" + '</option>');
                $.each(sch, function (idx, obj) {
                    $el.append('<option value="' + obj.Id + '">' + obj.Name + '</option>');
                });
            }
            else {
                $el.append('<option value="">' + "Select  Expected Monthly Trans" + '</option>');
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
        url: "../Remitter/SynchronizeRecords",
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
