var IsEditMode = false;

$(document).ready(function () {
    handleStaff();
    KYCCheckBoxes();
    LoadIdentificationType();
    LoadResidencyType();
    LoadCountry();
    LoadNationality();
    LoadQuestions();
    LoadCity();
    LoadExpectTrancationsCount();

    var urlParams = new URLSearchParams(window.location.search);
    var uidParam = urlParams.get('UID');

    if (uidParam !== null) {

        editUserById(uidParam);

    }



    $('#data_2 .input-group.date').datepicker({
        startView: 1,
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        autoclose: true,
        format: "dd/mm/yyyy"
    });
    $('#checkbox14').change(function () {
        // If the checkbox is checked, enable the text field; otherwise, disable it
        if (this.checked) {
            $("#other_Detail").prop('disabled', false);
        } else {
            $("#other_Detail").prop('disabled', true).val('');
        }
    });
    $('#Civil_Id_Front').change(function () {
        imagePreviewCivil_Id_Front(this);
    });
    $('#Civil_Id_Back').change(function () {
        imagePreviewCivil_Id_Back(this);
    });
    function imagePreviewCivil_Id_Front(input) {
        const imagePreviewCivil_Id_Front = $('#imagePreviewCivil_Id_Front');


        if (input.files && input.files[0]) {
            const reader = new FileReader();

            reader.onload = function (e) {

                imagePreviewCivil_Id_Front.html(`<img src="${e.target.result}" alt="Selected Image">`);
            };


            reader.readAsDataURL(input.files[0]);
        } else {

            imagePreviewCivil_Id_Front.html('');
        }
    }

    function imagePreviewCivil_Id_Back(input) {
        const imagePreviewCivil_Id_Back = $('#imagePreviewCivil_Id_Back');

        // Ensure that a file is selected
        if (input.files && input.files[0]) {
            const reader = new FileReader();

            reader.onload = function (e) {
                // Display the selected image in the preview div
                imagePreviewCivil_Id_Back.html(`<img src="${e.target.result}" alt="Selected Image">`);
            };

            // Read the selected file as a data URL
            reader.readAsDataURL(input.files[0]);
        } else {
            // Clear the preview if no file is selected
            imagePreviewCivil_Id_Back.html('');
        }
    }

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
        // Check if any of the checkboxes is checked
        if ($("[id^='pepcheckbox']:checked").length > 0) {
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
function editUserById(uid) {
    var data = new FormData();
    data.append("UID", uid);
    $.ajax({
        type: "POST",
        cache: false,
        async: false,
        url: "../Remitter/Edit",
        data: data,
        processData: false,
        contentType: false,
        success: function (Rdata) {
            if (Rdata != 'error') {
                var obj = JSON.parse(Rdata);
                console.log(obj);
                $('#UID').val(obj.UID);
                $('#Name').val(obj.Name);
                $('#Employer').val(obj.Employer);
                $('#Identification_Type').val(obj.Identification_Type).prop('Enable', 'true').trigger("chosen:updated");
                $('#Identification_Number').val(obj.Identification_Number);
                $('#Nationality').val(obj.Nationality).prop('Enable', 'true').trigger("chosen:updated");
                if (obj.Date_Of_Birth !== null) {
                    $('#Date_Of_Birth').val(obj.Date_Of_Birth.split('T')[0]);
                }
                if (obj.Identification_Expiry_Date !== null) {
                    $('#Identification_Expiry_Date').val(obj.Identification_Expiry_Date.split('T')[0]);
                }
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
                $('#Pep_Description').val(obj.Pep_Description);
                $('#Monthly_Income').val(obj.Monthly_Income);
                $('#Monthly_Trans_Limit').val(obj.Monthly_Trans_Limit);
                $('#Yearly_Trans_Limit').val(obj.Yearly_Trans_Limit);
                $('#Compliance_Limit').val(obj.Compliance_Limit);
                $('#Compliance_Trans_Count').val(obj.Compliance_Trans_Count);
                $('#Compliance_Comments').val(obj.Compliance_Comments);
                if (obj.Compliance_Limit_Expiry !== null) {
                    $('#Compliance_Limit_Expiry').val(obj.Compliance_Limit_Expiry.split('T')[0]);
                }

                if (obj.Civil_Id_Front !== null && obj.Civil_Id_Front !== "") {
                    $('#imagePreviewCivil_Id_Front').html(`<img src="${obj.Civil_Id_Front}" alt="Image">`);
                }
                if (obj.Civil_Id_Back !== null && obj.Civil_Id_Back !== "") {
                    $('#imagePreviewCivil_Id_Back').html(`<img src="${obj.Civil_Id_Back}" alt="Image" >`);
                }

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
                if (obj.Is_Profile_Completed) {
                    $("#Is_Profile_Completed").iCheck('check');
                }
                else {
                    $("#Is_Profile_Completed").iCheck('uncheck');
                }

                $(window).scrollTop(0);

                LoadSecurityQuestions(obj.Customer_Id);
                LoadKYCIndividuals(obj.Customer_Id);

            }
            else {
                ShowErrorAlert("Error", "Some Error Occured!");
                $('#btn-validate').removeAttr('disabled');
            }
        },
        error: function (e) {
        }
    });
}

//edit method

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

    $(".frmAddUsers").submit(function (event) {
        event.preventDefault();

        if (validateForm()) {

            var file1 = $('#Civil_Id_Front')[0].files[0];
            var file2 = $('#Civil_Id_Back')[0].files[0];
            var civilid = $('#Identification_Number').val().trim();

            var formData = new FormData();
            if (file1 !== undefined) {
                formData.append('Civil_Id_Front', file1);
            }
            if (file2 !== undefined) {
                formData.append('Civil_Id_Back', file2);
            }
            formData.append('Civil_Id', civilid);

            var url = 'https://stagingapi.creamerz.com/Remittance/UploadCivilid';
            var url1 = '../Remitter/AddCustomerFiles';

            if (file1 !== undefined || file2 !== undefined) {

                $.ajax({
                    url: url1,
                    type: 'POST',
                    data: formData,
                    async: false,
                    contentType: false,
                    processData: false,
                    success: function (response) {
                        debugger;
                        console.log();
                        $('#btn-save').attr('disabled', 'true');
                        $(".frmAddUsers :disabled").removeAttr('disabled');
                        return AddCustomer(response.civil_Id_Back, response.civil_Id_Front);
                    },
                    error: function (error) {
                        console.error('Error uploading files:', error);
                    }
                });
            } else {
                return AddCustomer(null, null);
            }

        }

        function AddCustomer(Civil_Id_Back, Civil_Id_Front) {
            if (!IsEditMode) {
                $.post(
                    "../Remitter/AddCustomer?civil_Id_Back=" + Civil_Id_Back + "&civil_Id_Front=" + Civil_Id_Front,
                    $(".frmAddUsers").serialize(),
                    function (value) {
                        if (value == 'duplicate_value_exist') {
                            swal(
                                'Warning',
                                'Customer Name Already Exist!',
                                'warning'
                            );
                            return;
                        }
                        if (value === 'noallowuser') {
                            swal('Warning', 'Your Are Not Allow To Add More Customer ', 'warning')
                            resetForm();
                            return;
                        }
                        if (value !== 'error') {

                            swal(
                                'Success',
                                'Customer Saved Successfully!',
                                'success'
                            );
                            resetForm();
                            $('#btn-save').removeAttr('disabled');
                            window.location = "../Remitter/Index";

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
                    "../Remitter/EditCustomer?Civil_Id_Back=" + Civil_Id_Back + "&Civil_Id_Front=" + Civil_Id_Front,
                    $(".frmAddUsers").serialize(),
                    function (value) {
                        if (value == 'duplicate_value_exist') {
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
                            resetForm();
                            $('#btn-save').removeAttr('disabled');
                            window.location = "../Remitter/Index";

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

    function validateForm() {

        var requiredFields = [
            { field: "Name" },
            { field: "Occupation" },
            { field: "Employer" },
            { field: "Identification_Type" },
            { field: "Identification_Number" },
            { field: "Identification_Expiry_Date" },
            { field: "Mobile_No" },
            { field: "Date_Of_Birth" },
            { field: "Nationality" },
            { field: "Security_Question_Id_1" },
            { field: "Security_Answer_1" },
            { field: "Security_Question_Id_2" },
            { field: "Security_Question_Id_3" },
            { field: "Security_Answer_2" },
            { field: "Security_Answer_3" },
            { field: "Area" }
            // Add more required fields as needed
        ];
        let IsValid = true;
        // Loop through the required fields and check for validation
        for (var i = 0; i < requiredFields.length; i++) {
            var fieldId = requiredFields[i].field;
            var fieldValue = $("#" + fieldId).val().trim();

            // Check if the field is empty
            if (fieldValue === "") {
                $("#Val" + fieldId).text("required");
                IsValid = false;
            }
            else {
                $("#Val" + fieldId).text("");
            }
        }
        // Validate Files
        /*    var fileInput = $("#Civil_Id_Front")[0];
            var fileInput1 = $("#Civil_Id_Back")[0];
    
            if (!fileInput.files.length > 0) {
                IsValid = false;
                $("#ValCivil_Id_Front").text("Please select a file");
            }
            else {
                $("#ValCivil_Id_Front").text("");
            }
            if (!fileInput1.files.length > 0) {
                IsValid = false;
                $("#ValCivil_Id_Back").text("Please select a file");
            } else {
                $("#ValCivil_Id_Back").text("");
            }*/
        // Validation for the first 3 checkboxes

        var isChecked = $(".group1:checked").length > 0;
        if (isChecked) {

            if ($(".group2:checked").length == 0) {

                $("#ValadditionalCheckboxes").text("Please check any one");
                IsValid = false;

            } else {

                $("#ValadditionalCheckboxes").text("");
                if ($("#checkbox14:checked").length > 0) {
                    if ($("#other_Detail").val() === '') {
                        $("#Valother_Detail").text("required");
                        IsValid = false;
                    } else {
                        $("#Valother_Detail").text("");
                    }
                }
            }


        }

        return IsValid;

    }

}

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





//reset values
function resetForm() {
    IsEditMode = false;
    LoadIdentificationType();
    LoadResidencyType();
    LoadCountry();
    LoadQuestions();
    LoadCity();
    LoadExpectTrancationsCount();

    $('#Name').val('');
    $('#Employer').val('');
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
    $('#Civil_Id_Front').val('');
    $('#Civil_Id_Back').val('');

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


    $("#checkbox1").iCheck('uncheck');
    $("#checkbox2").iCheck('uncheck');
    $("#checkbox3").iCheck('uncheck');
    $("#checkbox4").iCheck('uncheck');
    $("#checkbox5").iCheck('uncheck');
    $("#checkbox6").iCheck('uncheck');
    $("#checkbox7").iCheck('uncheck');
    $("#checkbox8").iCheck('uncheck');
    $("#checkbox9").iCheck('uncheck');
    $("#checkbox10").iCheck('uncheck');
    $("#checkbox11").iCheck('uncheck');
    $("#checkbox12").iCheck('uncheck');
    $("#checkbox13").iCheck('uncheck');
    $("#checkbox14").iCheck('uncheck');
    $("#pepcheckbox15").iCheck('uncheck');
    $("#pepcheckbox16").iCheck('uncheck');
    $("#pepcheckbox17").iCheck('uncheck');
    $("#IsReviwed").iCheck('uncheck');
    $("#IsVerified").iCheck('uncheck');
    $("#additionalCheckboxes").hide();

    $('#btn-save').html("<i class='fa fa-save'></i> Save");
    $('#UserTypeId').val("").trigger("chosen:updated");
    $('#btn-save').removeAttr('disabled');
    $('#imagePreviewCivil_Id_Back').html('');
    $('#imagePreviewCivil_Id_Front').html('');
}

$('#btn-refresh').click(function () {
    resetForm();
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
            //console.log(obj);
            $('#Security_Question_Id_1').val(obj[0].Question_Id).trigger("chosen:updated");
            $('#Security_Answer_1').val(obj[0].Answer);
            $('#Security_Question_Id_2').val(obj[1].Question_Id).trigger("chosen:updated");
            $('#Security_Answer_2').val(obj[1].Answer);
            $('#Security_Question_Id_3').val(obj[2].Question_Id).trigger("chosen:updated");
            $('#Security_Answer_3').val(obj[2].Answer);
        }
    });
}

function LoadKYCIndividuals(Customer_Id) {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Remitter/LoadKYCIndividuals?Customer_Id=" + Customer_Id,
        processData: false,
        contentType: false,
        success: function (data) {
            var obj = JSON.parse(data);
            //console.log(obj);

            if (obj[14].Answer || obj[15].Answer || obj[16].Answer) {
                $("#additionalCheckboxes").show();
            } else {
                $("#additionalCheckboxes").hide();
            }

            if (obj[0].Answer) {
                $("#checkbox1").iCheck('check');
            }
            else {
                $("#checkbox1").iCheck('uncheck');
            }
            if (obj[1].Answer) {
                $("#checkbox2").iCheck('check');
            }
            else {
                $("#checkbox2").iCheck('uncheck');
            }
            if (obj[2].Answer) {
                $("#checkbox3").iCheck('check');
            }
            else {
                $("#checkbox3").iCheck('uncheck');
            }
            if (obj[3].Answer) {
                $("#checkbox4").iCheck('check');
            }
            else {
                $("#checkbox4").iCheck('uncheck');
            }
            if (obj[4].Answer) {
                $("#checkbox5").iCheck('check');
            }
            else {
                $("#checkbox5").iCheck('uncheck');
            }
            if (obj[5].Answer) {
                $("#checkbox6").iCheck('check');
            }
            else {
                $("#checkbox6").iCheck('uncheck');
            }
            if (obj[7].Answer) {
                $("#checkbox8").iCheck('check');
            }
            else {
                $("#checkbox8").iCheck('uncheck');
            }
            if (obj[8].Answer) {
                $("#checkbox9").iCheck('check');
            }
            else {
                $("#checkbox9").iCheck('uncheck');
            }
            if (obj[9].Answer) {
                $("#checkbox10").iCheck('check');
            }
            else {
                $("#checkbox10").iCheck('uncheck');
            }
            if (obj[10].Answer) {
                $("#checkbox11").iCheck('check');
            }
            else {
                $("#checkbox11").iCheck('uncheck');
            }
            if (obj[11].Answer) {
                $("#checkbox12").iCheck('check');
            }
            else {
                $("#checkbox12").iCheck('uncheck');
            }
            if (obj[12].Answer) {
                $("#checkbox13").iCheck('check');
            }
            else {
                $("#checkbox13").iCheck('uncheck');
            }
            if (obj[13].Answer) {
                $("#checkbox14").iCheck('check');
                $("#other_Detail").prop('disabled', false);
                $("#other_Detail").val(obj[13].Details);

            }
            else {
                $("#checkbox14").iCheck('uncheck');
            }
            if (obj[14].Answer) {
                $("#pepcheckbox15").iCheck('check');
            }
            else {
                $("#pepcheckbox15").iCheck('uncheck');
            }
            if (obj[15].Answer) {
                $("#pepcheckbox16").iCheck('check');
            }
            else {
                $("#pepcheckbox16").iCheck('uncheck');
            }
            if (obj[16].Answer) {
                $("#pepcheckbox17").iCheck('check');
            }
            else {
                $("#pepcheckbox17").iCheck('uncheck');
            }


        }
    });
}

// for loading User
var LoadIdentificationType = function () {
    $.ajax({
        type: "POST",
        cache: false,
        async: false,
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
        async: false,
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
        async: false,
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
        async: false,
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
        async: false,
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
        async: false,
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
        async: false,
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

