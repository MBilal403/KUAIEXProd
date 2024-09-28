var IsEditMode = false;

var apiUrls = {
    identificationType: '../Remitter/LoadIdentificationType',
    residencyType: '../Remitter/LoadResidencyType',
    country: '../Remitter/LoadCountry',
    nationality: '../Remitter/LoadCountry',
    area: '../Remitter/GetKuwaitActiveCities',
    expectedMonthlyTransCount: '../Remitter/LoadExpectTrancationsCount',
    expectTrancationsAmount: '../Remitter/LoadExpectTrancationsAmount',
    genderTypes: '../Remitter/LoadGenderTypes',
};

var utilities = {
    // Generalized function to load dropdown data
    loadDropdownData: function (url, fieldSelector, optionText, valueField, textField) {
        $.ajax({
            type: "POST",
            cache: false,
            async: false,
            url: url,
            processData: false,
            contentType: false,
            success: function (data) {
                var items = JSON.parse(data);
                var $dropdown = $(fieldSelector);

                // Clear current options
                $dropdown.empty();

                // Add new options if data exists
                if (items.length > 0) {
                    $dropdown.append('<option value="">' + optionText + '</option>');
                    $.each(items, function (idx, obj) {
                        $dropdown.append('<option value="' + obj[valueField] + '">' + obj[textField] + '</option>');
                    });
                } else {
                    $dropdown.append('<option value="">' + optionText + '</option>');
                }

                // Update UI elements (for Chosen, Select2, etc.)
                $dropdown.trigger("liszt:updated");
                $dropdown.chosen();
            },
            error: function (xhr, status, error) {
                console.error("Error loading data from: " + url, error);
            }
        });
    },
    // Generalized function to ajaxRequest
    ajaxRequest: function (url, method = 'GET', data = null, onSuccess = null, onError = null, retries = 3, isFormData = false) {
        $.ajax({
            url: url,
            type: method,
            contentType: isFormData ? false : "application/json; charset=utf-8",
            processData: !isFormData,
            dataType: "json",
            data: isFormData ? data : method === 'GET' ? data : JSON.stringify(data),
            success: function (response) {
                if (onSuccess && typeof onSuccess === 'function') {
                    onSuccess(response);
                }
            },
            error: function (xhr, status, error) {
                if (retries > 0) {
                    console.warn(`Retrying... Attempts left: ${retries}`);
                    ajaxRequest(url, method, data, onSuccess, onError, retries - 1, isFormData);
                } else {
                    console.error("Error occurred at " + url + ": ", {
                        status: status,
                        error: error,
                        xhr: xhr.responseText
                    });
                    if (onError && typeof onError === 'function') {
                        onError(xhr, status, error);
                    } else {
                        swal("Error", "AJAX Error", "error");
                    }
                }
            }
        });
    }

};
$(document).ready(function () {

    initDropdowns();
    handleStaff();
    KYCCheckBoxes();


    var urlParams = new URLSearchParams(window.location.search);
    var uidParam = urlParams.get('UID');

    if (uidParam !== null) {

        editUserById(uidParam);

    }
    $('#btn-back').on('click', function () {
        window.history.back();  // Go back to the previous page
    });

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

    // Civil Id Front
    $('#Civil_Id_Front').change(function () {
        imagePreviewCivil_Id_Front(this);
    });

    $('#removeCivil_Id_Front').click(function () {
        removeCivilIdFront();
    });

    function imagePreviewCivil_Id_Front(input) {
        const imagePreviewCivil_Id_Front = $('#imagePreviewCivil_Id_Front');
        const removeButton = $('#removeCivil_Id_Front');

        if (input.files && input.files[0]) {
            const reader = new FileReader();

            reader.onload = function (e) {
                imagePreviewCivil_Id_Front.html(`<img src="${e.target.result}" alt="Selected Image" width="200">`);
                removeButton.show();
            };

            reader.readAsDataURL(input.files[0]);
        } else {
            imagePreviewCivil_Id_Front.html('');

            removeButton.hide(); // Hide the remove button if no image is selected
        }
    }

    function removeCivilIdFront() {
        const inputFile = $('#Civil_Id_Front');
        const imagePreviewCivil_Id_Front = $('#imagePreviewCivil_Id_Front');
        const removeButton = $('#removeCivil_Id_Front');

        inputFile.val(''); // Clear the file input value
        imagePreviewCivil_Id_Front.html('<p>No image</p>'); // Show "No image"
        removeButton.hide(); // Hide the remove button
    }

    // Civil Id Back
    $('#Civil_Id_Back').change(function () {
        imagePreviewCivil_Id_Back(this);
    });

    $('#removeCivil_Id_Back').click(function () {
        removeCivilIdBack();
    });

    function imagePreviewCivil_Id_Back(input) {
        const imagePreviewCivil_Id_Back = $('#imagePreviewCivil_Id_Back');
        const removeButton = $('#removeCivil_Id_Back');

        if (input.files && input.files[0]) {
            const reader = new FileReader();

            reader.onload = function (e) {

                imagePreviewCivil_Id_Back.html(`<img src="${e.target.result}" alt="Selected Image" width="200">`);

                console.log(e.target.result);
                removeButton.show(); // Show the remove button when an image is selected
            };

            reader.readAsDataURL(input.files[0]);
        } else {
            imagePreviewCivil_Id_Back.html('');
            removeButton.hide(); // Hide the remove button if no image is selected
        }
    }

    function removeCivilIdBack() {
        const inputFile = $('#Civil_Id_Back');
        const imagePreviewCivil_Id_Back = $('#imagePreviewCivil_Id_Back');
        const removeButton = $('#removeCivil_Id_Back');

        inputFile.val(''); // Clear the file input value
        imagePreviewCivil_Id_Back.html('<p>No image</p>'); // Show "No image"
        removeButton.hide(); // Hide the remove button
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
function formatDate(dateString) {
    // Extract the date part by splitting from 'T' (ISO format)
    const parts = dateString.split('T')[0].split('-'); // Extract [yyyy, MM, dd]

    // Create a formatted date in MM/dd/yyyy format
    return `${parts[1]}/${parts[2]}/${parts[0]}`; // MM/dd/yyyy
}
function editUserById(uid) {
    var data = new FormData();
    data.append("UID", uid);

    utilities.ajaxRequest(
        "../Remitter/Edit",
        "POST",
        data = data,
        onSuccess = function (obj) {
            if (obj != 'error') {
                $('#UID').val(obj.UID);
                $('#Name').val(obj.Name);
                $('#Employer').val(obj.Employer);
                $('#Identification_Type').val(obj.Identification_Type).prop('Enable', 'true').trigger("chosen:updated");
                $('#Identification_Type').prop('disabled', true).trigger('chosen:updated');
                $('#Identification_Number').val(obj.Identification_Number);
                $('#Identification_Number').prop('disabled', true);
                /*  var nationalityValue = $('#Nationality option').filter(function () {
                      console.log($(this).text());
                      return $(this).text().toLowerCase() === obj.Nationality.toLowerCase();
                  }).val();*/

                $('#Nationality').val(obj.Nationality).prop('disabled', false).trigger("chosen:updated");

                if (obj.Date_Of_Birth !== null) {
                    $('#Date_Of_Birth').val(
                        formatDate(obj.Date_Of_Birth)
                    );
                }

                if (obj.Identification_Expiry_Date !== null) {
                    $('#Identification_Expiry_Date').val(
                        formatDate(obj.Identification_Expiry_Date)
                    );
                }
                $('#Occupation').val(obj.Occupation);
                $('#Email_Address').val(obj.Email_Address);
                $('#Email_Address').prop('disabled', true);
                $('#Mobile_No').val(obj.Mobile_No);
                $('#Mobile_No').prop('disabled', true);
                $('#Gender').val(obj.Gender).prop('Enable', 'true').trigger("chosen:updated");

                $('#Area').val(obj.Area).prop('Enable', 'true').trigger("chosen:updated");

                $('#Block').val(obj.Block);
                $('#Street').val(obj.Street);
                //$('#Building').val(obj.Building);
                $('#Source_Of_Income').val(obj.Source_Of_Income);
                //$('#Floor').val(obj.Floor);
                $('#Flat').val(obj.Flat);
                $('#Identification_Additional_Detail').val(obj.Identification_Additional_Detail);
                $('#Residence_Type').val(obj.Residence_Type).prop('Enable', 'true').trigger("chosen:updated");
                $('#Telephone_No').val(obj.Telephone_No);

                $('#Birth_Place').val(obj.Birth_Place).prop('disabled', false).trigger("chosen:updated");
                //$('#Birth_Country').val(obj.Birth_Country).prop('Enable', 'true').trigger("chosen:updated");
                $('#Expected_Monthly_Trans_Count').val(obj.Expected_Monthly_Trans_Count).prop('Enable', 'true').trigger("chosen:updated");
                $('#Monthly_Trans_Limit').val(obj.Monthly_Trans_Limit).prop('Enable', 'true').trigger("chosen:updated");
                $('#Other_Income').val(obj.Other_Income);
                $('#Other_Income_Detail').val(obj.Other_Income_Detail);
                //$('#Pep_Description').val(obj.Pep_Description);
                $('#Monthly_Income').val(obj.Monthly_Income);
                //$('#Yearly_Trans_Limit').val(obj.Yearly_Trans_Limit);
                //$('#Compliance_Limit').val(obj.Compliance_Limit);
                //$('#Compliance_Trans_Count').val(obj.Compliance_Trans_Count);
                //$('#Compliance_Comments').val(obj.Compliance_Comments);
                //if (obj.Compliance_Limit_Expiry !== null) {
                //    $('#Compliance_Limit_Expiry').val(obj.Compliance_Limit_Expiry.split('T')[0]);
                //}

                if (obj.Civil_Id_Front !== null && obj.Civil_Id_Front !== "") {
                    $('#imagePreviewCivil_Id_Front').html(`<img src="${obj.Civil_Id_Front}" alt="Image">`);
                    $('#removeCivil_Id_Front').show();
                    $('#existingImage').val(obj.Civil_Id_Front);
                }
                else {
                    $('#imagePreviewCivil_Id_Front').html('<p>No image</p>');
                    $('#removeCivil_Id_Front').hide();

                }
                if (obj.Civil_Id_Back !== null && obj.Civil_Id_Back !== "") {
                    $('#imagePreviewCivil_Id_Back').html(`<img src="${obj.Civil_Id_Back}" alt="Image" >`);
                    $('#removeCivil_Id_Back').show();
                } else {
                    $('#imagePreviewCivil_Id_Back').html('<p>No image</p>');
                    $('#removeCivil_Id_Back').hide();
                }

                //  $("#btn-refresh").removeClass("disabled").removeAttr("disabled");
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

                //  LoadSecurityQuestions(obj.Customer_Id);
                //LoadKYCIndividuals(obj.Customer_Id);

            }
            else {
                ShowErrorAlert("Error", "Some Error Occured!");
                $('#btn-validate').removeAttr('disabled');
            }
        },
        null,
        3,
        true
    );

}
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
var handleStaff = function () {

    $(".frmAddUsers").submit(function (event) {
        event.preventDefault();
        let formData = new FormData();

        if (validateForm()) {
            var file1 = $('#Civil_Id_Front')[0].files[0];
            var file2 = $('#Civil_Id_Back')[0].files[0];

            if (file1 === undefined) {
                file1 = null;
            }
            if (file2 === undefined) {
                file2 = null;
            }

            let civilIdFrontExists = $('#imagePreviewCivil_Id_Front').find('img').length > 0;
            let civilIdBackExists = $('#imagePreviewCivil_Id_Back').find('img').length > 0;

            let RequestOBj = {
                UID: $('#UID').val(),
                Name: $('#Name').val(),
                Email_Address: $('#Email_Address').val(),
                Occupation: $('#Occupation').val(),
                Employer: $('#Employer').val(),
                Gender: $('#Gender').val(),
                Is_Profile_Completed: $('#Is_Profile_Completed').prop('checked'),
                IsReviwed: $('#IsReviwed').prop('checked'),
                Area: $('#Area').val(),
                Block: $('#Block').val(),
                Street: $('#Street').val(),
                Flat: $('#Flat').val(),
                Identification_Type: $('#Identification_Type').val(),
                Identification_Number: $('#Identification_Number').val(),
                Identification_Expiry_Date: $('#Identification_Expiry_Date').val(),
                Identification_Additional_Detail: $('#Identification_Additional_Detail').val(),
                Residence_Type: $('#Residence_Type').val(),
                Mobile_No: $('#Mobile_No').val(),
                Telephone_No: $('#Telephone_No').val(),
                Nationality: $('#Nationality').val(),
                Date_Of_Birth: $('#Date_Of_Birth').val(),
                Birth_Place: $('#Birth_Place').val(),
                Expected_Monthly_Trans_Count: $('#Expected_Monthly_Trans_Count').val(),
                Monthly_Trans_Limit: $('#Monthly_Trans_Limit').val(),
                Monthly_Income: $('#Monthly_Income').val(),
                Source_Of_Income: $('#Source_Of_Income').val(),
                Other_Income: $('#Other_Income').val(),
                Other_Income_Detail: $('#Other_Income_Detail').val(),
                CivilIdFrontImage: file1,
                CivilIdBackImage: file2,
                Civil_Id_Front: civilIdFrontExists ? "Present" : "",
                Civil_Id_Back: civilIdBackExists ? "Present" : ""
            }
            console.log(RequestOBj);

            for (let key in RequestOBj) {

                formData.append(key, RequestOBj[key]); 

            }

            var url = 'https://stagingapi.creamerz.com/Remittance/UploadCivilid';
            var urlLocal = 'http://localhost:31821/Remittance/UploadCivilId';
            var url1 = '../Remitter/AddCustomerFiles';
            AddCustomer(formData);
          
        }

        function AddCustomer(formData) {
            $('#btn-save').attr('disabled', 'true');
            $(".frmAddUsers :disabled").removeAttr('disabled');
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
                        if (value === 'insert_success') {

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

                $.ajax({
                    url: "../Remitter/EditCustomer",
                    type: 'POST',
                    data: formData,  
                    contentType: false, 
                    processData: false,  
                    dataType: 'text',  
                    success: function (value) {

                        if (value === 'duplicate_value_exist') {
                            swal('Warning', 'Customer Name Already Exist!', 'warning');
                            $('#btn-save').removeAttr('disabled');
                            return;
                        }
                        if (value === 'not_allowed') {
                            swal('Warning', 'Valid Format ".jpeg", ".jpg", ".png" and Max Size is 10 MB ', 'warning');
                            $('#btn-save').removeAttr('disabled');
                            return;
                        }
                        if (value === 'update_success') {
                            swal('Success', 'Customer Updated Successfully!', 'success');
                            resetForm();
                            $('#btn-save').removeAttr('disabled');
                            window.location = "../Remitter/Index";
                        } else {
                            swal('Error', 'Data not updated!!', 'error');
                            $('#btn-save').removeAttr('disabled');
                        }
                    },
                    error: function (xhr, status, error) {
                        // Handle error scenario
                        swal('Error', 'An error occurred while processing your request.', 'error');
                        $('#btn-save').removeAttr('disabled');
                    }
                });
            }
            return false;
        }


    });

    function validateForm() {

        var requiredFields = [
            { field: "Name" },
            { field: "Identification_Type" },
            { field: "Gender" },
            { field: "Identification_Number" },
            { field: "Identification_Expiry_Date" },
            { field: "Mobile_No" },
            { field: "Source_Of_Income" },
            { field: "Monthly_Income" },
            { field: "Residence_Type" },
            { field: "Nationality" },
            { field: "Email_Address" },
            { field: "Monthly_Trans_Limit" },
            { field: "Area" }
        ];
        let IsValid = true;

        for (var i = 0; i < requiredFields.length; i++) {
            var fieldId = requiredFields[i].field;
            var fieldValue = $("#" + fieldId).val().trim();

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
function resetForm() {
    IsEditMode = false;
    initDropdowns();
    LoadQuestions();

    $('#Name').val('');
    $('#Employer').val('');
    $('#Identification_Type').val('').trigger("chosen:updated");
    $('#Identification_Number').val('');
    $('#Nationality').val('').trigger("chosen:updated");
    $('#Date_Of_Birth').val('');
    $('#Identification_Expiry_Date').val('');
    $('#Occupation').val('');
    $('#Mobile_No').val('');
    $('#Area').val('').trigger("chosen:updated");

    $('#Block').val('');
    $('#Street').val('');
    $('#Building').val('');
    $('#Floor').val('');
    $('#Flat').val('');
    $('#Identification_Additional_Detail').val('');
    $('#Residence_Type').val('').trigger("chosen:updated");
    $('#Telephone_No').val('');
    $('#Birth_Place').val('');
    $('#Birth_Country').val('').trigger("chosen:updated");
    $('#Expected_Monthly_Trans_Count').val('').trigger("chosen:updated");
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

    $('#Security_Question_Id_1').val('').trigger("chosen:updated");
    $('#Answer1').val('');
    $('#Security_Question_Id_2').val('').trigger("chosen:updated");
    $('#Answer2').val('');
    $('#Security_Question_Id_3').val('').trigger("chosen:updated");
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
            if (obj[6].Answer) {
                $("#checkbox7").iCheck('check');
            }
            else {
                $("#checkbox7").iCheck('uncheck');
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
function initDropdowns() {

    // Load Identification Type dropdown
    utilities.loadDropdownData(
        apiUrls.identificationType,
        '#Identification_Type',
        'Select Identification Type',
        'Id',
        'Description'
    );
    // Load Residency Type dropdown
    utilities.loadDropdownData(
        apiUrls.residencyType,
        '#Residence_Type',
        'Select Residency Type',
        'Id',
        'Name'
    );
    // Load Country dropdown
    utilities.loadDropdownData(
        apiUrls.country,
        '#Birth_Place',
        'Select Country',
        'Name',
        'Name'
    );
    // Load Nationality dropdown
    utilities.loadDropdownData(
        apiUrls.nationality,
        '#Nationality',
        'Select Nationality',
        'Nationality',
        'Nationality'
    );
    // Load Area dropdown Used as a City
    utilities.loadDropdownData(
        apiUrls.area,
        '#Area',
        'Select Area',
        'Name',
        'Name'
    );
    // Load Expected Monthly Trans Count dropdown
    utilities.loadDropdownData(
        apiUrls.expectedMonthlyTransCount,
        '#Expected_Monthly_Trans_Count',
        'Select Expected Monthly Trans',
        'Id',
        'Name'
    );
    // Load Expected Monthly Trans Amount dropdown
    utilities.loadDropdownData(
        apiUrls.expectTrancationsAmount,
        '#Monthly_Trans_Limit',
        'Select Expected Monthly Trans',
        'Id',
        'Name'
    );
    // Load Gender types dropdown
    utilities.loadDropdownData(
        apiUrls.genderTypes,
        '#Gender',
        'Select Gender',
        'Id',
        'Value'
    );



    //LoadQuestions();
}


