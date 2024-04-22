var IsEditMode = false;
var RemittancePurpose;
var RemitterRelation;
var SourceOfIncome;

$(document).ready(function () {

    $('.dvRemittance_Purpose_Detail').hide();
    $('.dvSource_Of_Income_Detail').hide();
    $('.dvRemitter_Relation_Detail').hide();
    $('#Currency_Id').chosen();
    LoadNationality();


    RemittancePurpose = new SlimSelect({
        select: '#Remittance_Purpose',
        settings: {
            placeholderText: 'Select Remittance Purpose',
        },
        showSearch: true, // shows search field
        searchText: 'Sorry No Record Found.',
        hideSelectedOption: false,
        closeOnSelect: false
    });
    $('#Remittance_Purpose').on('change', function () {
        RemittancePurpose_OnChange();
    });

    SourceOfIncome = new SlimSelect({
        select: '#Source_Of_Income',
        settings: {
            placeholderText: 'Select Source Of Income',
        },
        showSearch: true, // shows search field
        searchText: 'Sorry No Record Found.',
        hideSelectedOption: false,
        closeOnSelect: true
    });
    RemitterRelation = new SlimSelect({
        select: '#Remitter_Relation',
        settings: {
            placeholderText: 'Select Remitter Relation',
        },
        showSearch: true, // shows search field
        searchText: 'Sorry No Record Found.',
        hideSelectedOption: false,
        closeOnSelect: false,
        limit: 1
    });
    $('#Source_Of_Income').on('change', function () {
        SourceOfIncome_OnChange();
    });
    $('#Remitter_Relation').on('change', function () {
        RemitterRelation_OnChange();
    });

    LoadRemittancePurpose();
    LoadRemitterRelation();
    LoadSourceOfIncome();
    LoadRemittanceType();
    LoadCountry();

    $('#Bank_Id').chosen();
    $('#Branch_Id').chosen();
    $('#Remittance_Subtype_Id').chosen();

    var urlParams = new URLSearchParams(window.location.search);
    var uid = urlParams.get('UID');
    var cuid = urlParams.get('CUID');
    $('#UID').val(uid);
    if (uid !== null && cuid !== null) {
        $('#CUID').val(cuid);

        editUserByUId(uid);
    }


    handleStaff();

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

var LoadCountry = function () {
    $("#wait").css("display", "block");
    $.ajax({
        type: "POST",
        cache: false,
        async: false,
        url: "../Beneficiary/LoadCountry",
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

$('#Country_Id').on('change', function () {
    var Id = $('#Country_Id').val();
    LoadCurrency(Id);
    LoadBank(Id);
});

var LoadCurrency = function (Id) {
    $("#wait").css("display", "block");
    $.ajax({
        type: "POST",
        cache: false,
        async: false,
        url: "../Beneficiary/LoadCurrency?CountryId=" + Id,
        processData: false,
        contentType: false,
        async: false,
        success: function (data) {
            var sch = JSON.parse(data);

            var $el = $('#Currency_Id');
            $el.chosen('destroy');
            $el.empty();
            if (sch.length > 0) {
                $el.append('<option value="">' + "Select Currency" + '</option>');
                $.each(sch, function (idx, obj) {
                    $el.append('<option value="' + obj.CurrencyId + '">' + obj.CurrencyName + ' ' + obj.CurrencyCode + '</option>');
                });
            }
            else {
                $el.append('<option value="">' + "Select Currency" + '</option>');
            }
            $el.trigger("liszt:updated");
            $el.chosen();
        },
        complete: function () {
            $("#wait").css("display", "none");
        }
    });
};

var LoadNationality = function () {
    $("#wait").css("display", "block");
    $.ajax({
        type: "POST",
        cache: false,
        async: false,
        url: "../Beneficiary/LoadNationality",
        processData: false,
        contentType: false,
        async: false,
        success: function (data) {
            var sch = JSON.parse(data);
            var $el1 = $('#Nationality_Id');
            $el1.empty();
            if (sch.length > 0) {
                $el1.append('<option value="">' + "Select Nationality" + '</option>');
                $.each(sch, function (idx, obj) {
                    $el1.append('<option value="' + obj.Id + '">' + obj.Nationality + '</option>');
                });
            }
            else {
                $el1.append('<option value="">' + "Select Nationality" + '</option>');
            }
            $el1.trigger("liszt:updated");
            $el1.chosen();
        },
        complete: function () {
            $("#wait").css("display", "none");
        }
    });
};

var LoadRemittancePurpose = function () {
    $.ajax({
        type: "POST",
        cache: false,
        async: false,
        url: "../Beneficiary/LoadRemittancePurpose",
        processData: false,
        contentType: false,
        success: function (rdata) {
            var sch = JSON.parse(rdata);

            var data = [];
            var d;

            if (sch.length > 0) {
                $.each(sch, function (idx, obj) {
                    d = { 'value': obj.Id, 'text': obj.Name };
                    data.push(d);
                });
            }

            RemittancePurpose.setData(data);
            Console.log(RemittancePurpose.getSelected());
        }
    });
};


function loadEdit() {
    $.ajax({
        type: "POST",
        cache: false,

        url: "../Beneficiary/LoadRemittancePurpose",
        processData: false,
        contentType: false,
        success: function (rdata) {
            var sch = JSON.parse(rdata);

            var data = [];
            var d;

            if (sch.length > 0) {
                $.each(sch, function (idx, obj) {
                    d = { 'value': obj.Id, 'text': obj.Name };
                    data.push(d);
                });
            }


            var targetValue = 3;
            data = data.map(function (option) {
                return {
                    text: option.text,
                    value: option.value,
                    selected: option.value === targetValue  // Set selected to true if the condition is met
                };
            });

            RemittancePurpose.setData(data);
        }
    });
};


var RemittancePurpose_OnChange = function () {
    var rem_selected = RemittancePurpose.getSelected();
    if ($.inArray(3, rem_selected) !== -1) {
        $('.dvRemittance_Purpose_Detail').show();
        $('#Remittance_Purpose_Detail').val('');

    }
    else {
        $('.dvRemittance_Purpose_Detail').hide();
        $('#Remittance_Purpose_Detail').val('');

    }
};

var LoadSourceOfIncome = function () {
    $.ajax({
        type: "POST",
        cache: false,
        async: false,
        url: "../Beneficiary/LoadSourceOfIncome",
        processData: false,
        contentType: false,
        success: function (rdata) {
            var sch = JSON.parse(rdata);
            var data = [];
            var d;
            if (sch.length > 0) {
                $.each(sch, function (idx, obj) {
                    d = { 'value': obj.Id, 'text': obj.Name };
                    data.push(d);
                });
            }
            SourceOfIncome.setData(data);
        }
    });
};

var SourceOfIncome_OnChange = function () {
    var rem_selected = SourceOfIncome.getSelected();
    if ($.inArray(3, rem_selected) !== -1) {
        $('.dvSource_Of_Income_Detail').show();
        $('#Source_Of_Income_Detail').val('');
    }
    else {
        $('.dvSource_Of_Income_Detail').hide();
        $('#Source_Of_Income_Detail').val('');

    }
};

var LoadRemitterRelation = function () {
    $.ajax({
        type: "POST",
        cache: false,
        async: false,
        url: "../Beneficiary/LoadRemitterRelation",
        processData: false,
        contentType: false,
        success: function (rdata) {
            var sch = JSON.parse(rdata);
            var data = [];
            var d;

            if (sch.length > 0) {
                $.each(sch, function (idx, obj) {
                    d = { 'value': obj.Relationship_Id, 'text': obj.Name };
                    data.push(d);
                });
            }
            RemitterRelation.setData(data);
        }
    });
};

var RemitterRelation_OnChange = function () {
    var rem_selected = RemitterRelation.getSelected();
    //console.log(rem_selected);
    if ($.inArray(7, rem_selected) !== -1) {
        $('.dvRemitter_Relation_Detail').show();
        $('#Remitter_Relation_Detail').val('');
    }
    else {
        $('.dvRemitter_Relation_Detail').hide();
        $('#Remitter_Relation_Detail').val('');
    }
};

var LoadRemittanceType = function () {
    $("#wait").css("display", "block");
    $.ajax({
        type: "POST",
        cache: false,
        async: false,
        url: "../Beneficiary/LoadRemittanceType",
        processData: false,
        contentType: false,
        async: false,
        success: function (data) {
            var sch = JSON.parse(data);

            var $el = $('#Remittance_Type_Id');
            $el.empty();
            if (sch.length > 0) {
                $el.append('<option value="">' + "Select Remittance Type" + '</option>');
                $.each(sch, function (idx, obj) {
                    $el.append('<option value="' + obj.Remittance_Type_Id + '">' + obj.English_Name + '</option>');
                });
            }
            else {
                $el.append('<option value="">' + "Select Remittance Type" + '</option>');
            }
            $el.trigger("liszt:updated");
            $el.chosen();
        },
        complete: function () {
            $("#wait").css("display", "none");
        }
    });
};

$('#Remittance_Type_Id').on('change', function () {
    //alert($('#Remittance_Type_Id').val());
    if ($('#Remittance_Type_Id').val() == 6) { // Cash Pickup
        $('.dvBank_Code').hide();
        $('.dvBranch_Id').hide();
        $('.dvBranch_Number').hide();
        $('.dvBank_Account_No').hide();
    }
    else if ($('#Remittance_Type_Id').val() == 5 || $('#Remittance_Type_Id').val() == 1 || $('#Remittance_Type_Id').val() == 4 ) { // Deposit To Account
        $('.dvBank_Code').show();
        $('.dvBranch_Id').show();
        $('.dvBranch_Number').show();
        $('.dvBank_Account_No').show();
    }
});

var LoadBank = function (CountryId) {
    $("#wait").css("display", "block");
    $.ajax({
        type: "POST",
        cache: false,
        async: false,
        url: "../Beneficiary/LoadBank?CountryId=" + CountryId,
        processData: false,
        contentType: false,
        async: false,
        success: function (data) {
            var sch = JSON.parse(data);

            var $el = $('#Bank_Id');
            $el.chosen('destroy');
            $el.empty();
            if (sch.length > 0) {
                $el.append('<option value="">' + "Select Bank" + '</option>');
                $.each(sch, function (idx, obj) {
                    $el.append('<option value="' + obj.Bank_Id + '" code="' + obj.Bank_Code + '">' + obj.English_Name + '</option>');
                });
            }
            else {
                $el.append('<option value="">' + "Select Bank" + '</option>');
            }
            $el.trigger("liszt:updated");
            $el.chosen();
        },
        complete: function () {
            $("#wait").css("display", "none");
        }
    });
};

$('#Bank_Id').on('change', function () {
    var BankId = $('#Bank_Id').val();
    var Bank_Code = $('#Bank_Id option:selected').attr('code');
    var Remittance_Type_Id = $('#Remittance_Type_Id').val();

    if (Bank_Code !== 'null') {
        $('#Bank_Code').val(Bank_Code);
    }
    else {
        $('#Bank_Code').val('');
    }
    LoadBranch(BankId);
    LoadRemintanceSubType(Remittance_Type_Id, BankId);
});

var LoadBranch = function (BankId) {
    $("#wait").css("display", "block");
    $.ajax({
        type: "POST",
        cache: false,
        async: false,
        url: "../Beneficiary/LoadBranch?BankId=" + BankId,
        processData: false,
        contentType: false,
        success: function (data) {
            var sch = JSON.parse(data);

            var $el = $('#Branch_Id');
            $el.chosen('destroy');
            $el.empty();
            if (sch.length > 0) {
                $el.append('<option value="">' + "Select Branch" + '</option>');
                $.each(sch, function (idx, obj) {
                    $el.append('<option value="' + obj.Bank_Branch_Id + '" code="' + obj.Branch_Code + '">' + obj.English_Name + '</option>');
                });
            }
            else {
                $el.append('<option value="">' + "Select Branch" + '</option>');
            }
            $el.trigger("liszt:updated");
            $el.chosen();
        },
        complete: function () {
            $("#wait").css("display", "none");
        }
    });
};

$('#Branch_Id').on('change', function () {
    var Branch_Code = $('#Branch_Id option:selected').attr('code');
    if (Branch_Code !== 'null') {
        $('#Branch_Number').val(Branch_Code);
    }
    else {
        $('#Branch_Number').val('');
    }
});

var LoadRemintanceSubType = function (Remittance_Type_Id, Bank_Id) {
    $("#wait").css("display", "block");
    $.ajax({
        type: "Get",
        cache: false,
        async: false,
        url: "../Beneficiary/LoadRemittanceSubType?Remittance_Type_Id=" + Remittance_Type_Id + "&Bank_Id=" + Bank_Id,
        processData: false,
        contentType: false,
        async: false,
        success: function (data) {
            var sch = JSON.parse(data);

            var $el = $('#Remittance_Subtype_Id');
            if (sch.length > 0) {

                $el.chosen('destroy');
                $el.empty();

                $el.append('<option value="">' + "Select Remittance Subtype " + '</option>');
                $.each(sch, function (idx, obj) {
                    $el.append('<option value="' + obj.Remittance_SubType_Id + '">' + obj.Remittance_SubType + '</option>');
                });

                $el.trigger("liszt:updated");
                $el.chosen();
            }
            else {
                $('.dvRemittance_Subtype_Id').hide();
            }
        },
        complete: function () {
            $("#wait").css("display", "none");
        }
    });
};

$("#Status").on('ifChecked', function (event) {
    $(this).closest("input").attr('value', 1);
});
$("#Status").on('ifUnchecked', function (event) {
    $(this).closest("input").attr('value', 0);
});

//handle stuff
var handleStaff = function () {
    $(".frmAddUsers").submit(function (event) {
        event.preventDefault();

        if (validateForm()) {
            $('#btn-save').attr('disabled', 'true');
            $(".frmAddUsers :disabled").removeAttr('disabled');
            if (!IsEditMode) {
                $.post(
                    "../Beneficiary/AddBeneficiary",
                    $(".frmAddUsers").serialize(),
                    function (value) {
                        if (value === 'duplicate_value_exist') {
                            swal(
                                'Warning',
                                'Beneficiary Name Already Exist!',
                                'warning'
                            )
                            return;
                        }
                        if (value == 'noallowuser') {
                            swal('Warning', 'Your Are Not Allow To Add More Customer ', 'warning')
                            resetForm();
                            return;
                        }
                        if (value == 'insert_success') {
                            swal(
                                'Success',
                                'Beneficiary Saved Successfully!',
                                'success'
                            )
                            resetForm();
                            $('#btn-save').removeAttr('disabled');

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
                    "../Beneficiary/EditBeneficiary",
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
                        if (value === 'update_success') {
                            swal(
                                'Success',
                                'Beneficiary Updated Successfully!',
                                'success'
                            )
                            resetForm();
                            $('#btn-save').removeAttr('disabled');

                            window.location.href = "../Remitter/Index";
                        }
                        else {
                            swal("Error", "Data not updated!!", "error")
                            $('#btn-save').removeAttr('disabled');
                        }
                    },
                    "text"
                );
            }

        }

    });

    function validateForm() {

        var requiredFields = [
            { field: "FullName" },
            { field: "Country_Id" },
            { field: "Currency_Id" },
            { field: "Currency_Id" },
            { field: "Nationality_Id" },
            { field: "Remittance_Type_Id" },
            { field: "Mobile_No" },
            { field: "Bank_Id" }

            // Add more required fields as needed
        ];
        let IsValid = true;
        // Loop through the required fields and check for validation
        for (var i = 0; i < requiredFields.length; i++) {
            var fieldId = requiredFields[i].field;
            var fieldValue = $("#" + fieldId).val();

            // Check if the field is empty
            if (fieldValue === '' || fieldValue === undefined || (fieldValue === '0' && $('#' + fieldName).is('select'))) {
                $("#Val" + fieldId).text("required");
                IsValid = false;
            }
            else {
                $("#Val" + fieldId).text("");
            }
        }
        var rem_selected = RemittancePurpose.getSelected();
        if ($.inArray(3, rem_selected) !== -1) {
            fieldId = 'Remittance_Purpose_Detail';
            fieldValue = $('Remittance_Purpose_Detail').val();
            if (fieldValue === '' || fieldValue === undefined || (fieldValue === '0' && $('#' + fieldName).is('select'))) {
                $("#Val" + fieldId).text("required");
                IsValid = false;
            }
            else {
                $("#Val" + fieldId).text("");
            }

        }


        return IsValid;

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
}

//edit method
function editUserByUId(uid) {
    var data = new FormData();
    data.append("UID", uid);
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Beneficiary/Edit",
        data: data,
        processData: false,
        contentType: false,
        success: function (Rdata) {
            if (Rdata != 'error') {
                var obj = JSON.parse(Rdata);
                console.log(obj);
                $('#UID').val(obj.UID);
                $('#DD_Beneficiary_Name').val(obj.DD_Beneficiary_Name);
                $('#Customer_Id').val(obj.Customer_Id).prop('Enable', 'true').trigger("chosen:updated");
                $('#FullName').val(obj.FullName);
                $('#Gender').val(obj.Gender).prop('Enable', 'true').trigger("chosen:updated");
                $('#Address_Line1').val(obj.Address_Line1);
                $('#Address_Line2').val(obj.Address_Line2);
                $('#Address_Line3').val(obj.Address_Line3);
                $('#Country_Id').val(obj.Country_Id).prop('Enable', 'true').trigger("chosen:updated");
                LoadCurrency(obj.Country_Id);
                LoadBank(obj.Country_Id);
                $('#Nationality_Id').val(obj.Nationality_Id).prop('Enable', 'true').trigger("chosen:updated");
                $('#Birth_Date').val(obj.Birth_Date);
                $('#Currency_Id').val(obj.Currency_Id).prop('Enable', 'true').trigger("chosen:updated");
                $('#Remittance_Purpose').val(obj.Remittance_Purpose);
                $('#Remittance_Type_Id').val(obj.Remittance_Type_Id).prop('Enable', 'true').trigger("chosen:updated");
                $('#Remittance_Instruction').val(obj.Remittance_Instruction);
                $('#Phone_No').val(obj.Phone_No);
                $('#Mobile_No').val(obj.Mobile_No);
                $('#Fax_No').val(obj.Fax_No);
                $('#Email_Address1').val(obj.Email_Address1);
                $('#Email_Address2').val(obj.Email_Address2);
                $('#Identification_Type').val(obj.Identification_Type).prop('Enable', 'true').trigger("chosen:updated");
                $('#Identification_No').val(obj.Identification_No);
                $('#Identification_Remarks').val(obj.Identification_Remarks);
                $('#Identification_Issue_Date').val(obj.Identification_Issue_Date);
                $('#Identification_Expiry_Date').val(obj.Identification_Expiry_Date);
                $('#Bank_Account_type').val(obj.Bank_Account_type).prop('Enable', 'true').trigger("chosen:updated");
                $('#Bank_Id').val(obj.Bank_Id).prop('Enable', 'true').trigger("chosen:updated");
                LoadBranch(obj.Bank_Id);
                LoadRemintanceSubType(obj.Remittance_Type_Id, obj.Bank_Id);
                $('#Branch_Id').val(obj.Branch_Id).prop('Enable', 'true').trigger("chosen:updated");
                $('#Bank_Name').val(obj.Bank_Name);
                $('#Branch_Name').val(obj.Branch_Name);
                $('#Branch_City_Id').val(obj.Branch_City_Id).prop('Enable', 'true').trigger("chosen:updated");
                $('#Branch_City_Name').val(obj.Branch_City_Name);
                $('#Branch_Address_Line1').val(obj.Branch_Address_Line1);
                $('#Branch_Address_Line2').val(obj.Branch_Address_Line2);
                $('#Branch_Address_Line3').val(obj.Branch_Address_Line3);
                $('#Branch_Number').val(obj.Branch_Number);
                $('#Branch_Phone_Number').val(obj.Branch_Phone_Number);
                $('#Branch_Fax_Number').val(obj.Branch_Fax_Number);
                $('#Destination_Country_Id').val(obj.Destination_Country_Id).prop('Enable', 'true').trigger("chosen:updated");
                $('#Destination_Country_Id').trigger("change");
                $('#Bank_Account_No').val(obj.Bank_Account_No);
                $('#Remittance_Remarks').val(obj.Remittance_Remarks);
                $('#Bank_Code').val(obj.Bank_Code);
                $('#Routing_Bank_Id').val(obj.Routing_Bank_Id).prop('Enable', 'true').trigger("chosen:updated");
                $('#Routing_Bank_Id').trigger("change");
                $('#Routing_Bank_Branch_Id').val(obj.Routing_Bank_Branch_Id).prop('Enable', 'true').trigger("chosen:updated");
                if (obj.Remittance_Subtype_Id !== null) {
                    $('.dvRemittance_Subtype_Id').show();
                    $('#Remittance_Subtype_Id').val(obj.Remittance_Subtype_Id).prop('Enable', 'true').trigger("chosen:updated");
                }

                $('#Birth_Place').val(obj.Birth_Place);
                $('#TransFastInfo').val(obj.TransFastInfo);
                RemittancePurpose.setSelected(obj.Remittance_Purpose);
                SourceOfIncome.setSelected(obj.Source_Of_Income);
                RemitterRelation.setSelected(obj.Remitter_Relation);
                $('#Source_Of_Income_Detail').val(obj.Source_Of_Income_Detail);
                $('#Remittance_Purpose_Detail').val(obj.Remittance_Purpose_Detail);
                $('#Remitter_Relation_Detail').val(obj.Remitter_Relation_Detail);
                $('#btn-save').html("<i class='fa fa-save'></i> Update");
                IsEditMode = true;
            }
            else {
                ShowErrorAlert("Error", "Some Error Occured!");
                $('#btn-validate').removeAttr('disabled');
            }
        },
        error: function (e) {
        }
    });
};


$('#btn-refresh').click(function () {
    resetForm();
});

//reset values
function resetForm() {
    IsEditMode = false;
    LoadRemittanceType();

    LoadCountry();
    $('#Customer_Id').val('');
    $('#FullName').val('');
    $('#Country_Id').val('').trigger("chosen:updated");
    $('#Currency_Id').val('').trigger("chosen:updated");
    $('#Mobile_No').val('');
    $('#Bank_Id').val('');
    $('#Branch_Id').val('');
    $('#Branch_Number').val('');
    $('#Bank_Account_No').val('');
    $('#Bank_Code').val('');
    $('#Remittance_Subtype_Id').val('').trigger("chosen:updated");
    $('#btn-save').html("<i class='fa fa-save'></i> Save");
    $('#Nationality_Id').val("").trigger("chosen:updated");
    $('#Remittance_Type_Id').val("").trigger("chosen:updated");
    $('#Bank_Id').val("").trigger("chosen:updated");
    $('#Remittance_Type_Id').val("").trigger("chosen:updated");
    $('#Remittance_Subtype_Id').val("").trigger("chosen:updated");
    $('#btn-save').removeAttr('disabled');


    var requiredFields = [
        { field: "FullName" },
        { field: "Country_Id" },
        { field: "Currency_Id" },
        { field: "Currency_Id" },
        { field: "Nationality_Id" },
        { field: "Remittance_Type_Id" },
        { field: "Mobile_No" },
        { field: "Bank_Id" }

        // Add more required fields as needed
    ];
    // Loop through the required fields and check for validation
    for (var i = 0; i < requiredFields.length; i++) {
        var fieldId = requiredFields[i].field;
        $("#Val" + fieldId).text("");

    }
}

var LoadRoutingBanksDefault = function () {

    var $el = $("#Routing_Bank_Id");
    $el.empty();

    $el.append('<option value="0">' + "Select Routing Bank" + '</option>');

    $el.trigger("liszt:updated");
    $el.chosen();
}

var LoadRoutingBankBranchesDefault = function () {

    var $el = $("#Routing_Bank_Branch_Id");
    $el.empty();

    $el.append('<option value="0">' + "Select Routing Bank Branch" + '</option>');

    $el.trigger("liszt:updated");
    $el.chosen();
}

// Getting Value From Query String By Param Name
function getParameterByName(name) {
    var match = RegExp('[?&]' + name + '=([^&]*)').exec(window.location.href);
    return match && decodeURIComponent(match[1].replace(/\+/g, ' '));
}

$('#btn-sync').on('click', function () {
    $.ajax({
        type: "POST",
        cache: false,
        async: false,
        url: "../Beneficiary/SynchronizeRecords",
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

