var IsEditMode = false;
var RemittancePurpose;
var RemitterRelation;
var SourceOfIncome;

$(document).ready(function () {

    $('#CUID').val(getParameterByName('UID'));

    $('.dvRemittance_Purpose_Detail').hide();
    $('.dvSource_Of_Income_Detail').hide();
    $('.dvRemitter_Relation_Detail').hide();

    LoadCountry();
    $('#Currency_Id').chosen();
    LoadNationality();

    $('#tblUsers').DataTable({ responsive: true });

    RemittancePurpose = new SlimSelect({
        select: '#Remittance_Purpose',
        placeholder: 'Select Remittance Purpose',
        showSearch: true, // shows search field
        searchText: 'Sorry No Record Found.',
        hideSelectedOption: false,
        closeOnSelect: false,
        onChange: function () {
            RemittancePurpose_OnChange();
        }
    });
    SourceOfIncome = new SlimSelect({
        select: '#Source_Of_Income',
        placeholder: 'Select Source Of Income',
        showSearch: true, // shows search field
        searchText: 'Sorry No Record Found.',
        hideSelectedOption: false,
        closeOnSelect: true,
        onChange: function () {
            SourceOfIncome_OnChange();
        }
    });
    RemitterRelation = new SlimSelect({
        select: '#Remitter_Relation',
        placeholder: 'Select Relation With Remitter',
        showSearch: true, // shows search field
        searchText: 'Sorry No Record Found.',
        hideSelectedOption: false,
        closeOnSelect: false,
        limit: 1,
        onChange: function () {
            RemitterRelation_OnChange();
        }
    });

    LoadRemittancePurpose();
    LoadRemitterRelation();
    LoadSourceOfIncome();

    LoadRemittanceType();
    $('#Bank_Id').chosen();
    $('#Branch_Id').chosen();
    $('#Remittance_Subtype_Id').chosen();

    handleStaff();
    LoadGridData();
       
    //LoadRoutingBanksDefault();
    //LoadRoutingBankBranchesDefault();

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
                    $el.append('<option value="' + obj.Id + '">' + obj.CurrencyName + ' ' + obj.CurrencyCode + '</option>');
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
        }
    });
};

var RemittancePurpose_OnChange = function () {
    var rem_selected = RemittancePurpose.selected();
    if ($.inArray("3", rem_selected) !== -1) {
        $('.dvRemittance_Purpose_Detail').show();
    }
    else {
        $('.dvRemittance_Purpose_Detail').hide();
    }
};

var LoadSourceOfIncome = function () {
    $.ajax({
        type: "POST",
        cache: false,
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
    var rem_selected = SourceOfIncome.selected();
    console.log(rem_selected);
    if ($.inArray("3", rem_selected) !== -1) {
        $('.dvSource_Of_Income_Detail').show();
    }
    else {
        $('.dvSource_Of_Income_Detail').hide();
    }
};

var LoadRemitterRelation = function () {
    $.ajax({
        type: "POST",
        cache: false,
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
    var rem_selected = RemitterRelation.selected();
    //console.log(rem_selected);
    if ($.inArray("7", rem_selected) !== -1) {
        $('.dvRemitter_Relation_Detail').show();
    }
    else {
        $('.dvRemitter_Relation_Detail').hide();
    }
};

var LoadRemittanceType = function () {
    $("#wait").css("display", "block");
    $.ajax({
        type: "POST",
        cache: false,
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
    else if ($('#Remittance_Type_Id').val() == 5) { // Deposit To Account
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
    $('.frmAddUsers').validate({
        errorElement: 'span', //default input error message container
        errorClass: 'help-block', // default input error message class
        focusInvalid: false, // do not focus the last invalid input
        ignore: "",
        rules: {
            Name: {
                required: true,
                maxlength: 200
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
                    "../Beneficiary/AddBeneficiary",
                    $(".frmAddUsers").serialize(),
                    function (value) {
                        if (value === 'exist') {
                            swal(
                                'Warning',
                                'Beneficiary Name Already Exist!',
                                'warning'
                            )
                            return;
                        }
                        if (value == 'noallowuser') {
                            swal('Warning', 'Your Are Not Allow To Add More Customer ', 'warning')
                            Reset();
                            return;
                        }
                        if (value != 'error') {
                            swal(
                                'Success',
                                'Beneficiary Saved Successfully!',
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
                    "../Beneficiary/EditBeneficiary",
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
                                'Beneficiary Updated Successfully!',
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

//edit method
$(document).on('click', '.btn-edit', function () {
    var uid = $(this).attr('id');
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

                $('#UID').val(obj.UID);
                $('#DD_Beneficiary_Name').val(obj.DD_Beneficiary_Name);
                $('#Customer_Id').val(obj.Customer_Id).prop('Enable', 'true').trigger("chosen:updated");
                $('#FullName').val(obj.FullName);
                $('#Gender').val(obj.Gender).prop('Enable', 'true').trigger("chosen:updated");
                $('#Address_Line1').val(obj.Address_Line1);
                $('#Address_Line2').val(obj.Address_Line2);
                $('#Address_Line3').val(obj.Address_Line3);
                $('#Country_Id').val(obj.Country_Id).prop('Enable', 'true').trigger("chosen:updated");
                $('#Currency_Id').val(obj.Currency_Id).prop('Enable', 'true').trigger("chosen:updated");
                $('#Birth_Date').val(obj.Birth_Date);
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

                $('#Remittance_Subtype_Id').val(obj.Remittance_Subtype_Id).prop('Enable', 'true').trigger("chosen:updated");
                $('#Birth_Place').val(obj.Birth_Place);
                $('#TransFastInfo').val(obj.TransFastInfo);

                $('#btn-save').html("<i class='fa fa-save'></i> Update");
                IsEditMode = true;
                //if (obj.Status) {
                //    $("#Status").iCheck('check');
                //}
                //else {
                //    $("#Status").iCheck('uncheck');
                //}
                //$(window).scrollTop(0);
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

//load grid
var LoadGridData = function () {
    //alert(uid);
    $.ajax({
        type: "GET",
        cache: false,
        url: "../Beneficiary/LoadGrid",
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

                if (obj.FullName != null) {
                    html += '<td>' + obj.FullName + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }
                if (obj.Customer_Name != null) {
                    html += '<td>' + obj.Customer_Name + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }
                if (obj.Birth_Date != null) {
                    html += '<td>' + obj.Birth_Date + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }

                if (obj.Address_Line3 != null) {
                    html += '<td>' + obj.Address_Line3 + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }
                if (obj.Branch_Address_Line3 != null) {
                    html += '<td>' + obj.Branch_Address_Line3 + '</td>';
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

                if (obj.Bank_Name != null) {
                    html += '<td>' + obj.Bank_Name + '</td>';
                }
                else {
                    html += '<td>-</td>';
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

$('#btn-refresh').click(function () {
    Reset();
});

//reset values
function Reset() {
    IsEditMode = false;
    LoadRemittanceType();
    
    LoadCountry();
    
    
    
    
    
    $('#Customer_Id').val('');
    $('#FullName').val('');
    
    $('#Country_Id').val('');
    $('#Currency_Id').val('');
    
    $('#Remittance_Purpose').val('');
    $('#Remittance_Type_Id').val('');
    
    
    $('#Mobile_No').val('');
    
    
    
    $('#Bank_Id').val('');
    $('#Branch_Id').val('');
    
    $('#Branch_Number').val('');
    
    $('#Bank_Account_No').val('');
    
    $('#Bank_Code').val('');
    
    
    $('#Remittance_Subtype_Id').val('');
    

    $("#IsActive").iCheck('uncheck');

    $('#btn-save').html("<i class='fa fa-save'></i> Save");
    $('#Customer_Id').val("").trigger("chosen:updated");
    $('#Country_Id').val("").trigger("chosen:updated");
    $('#Currency_Id').val("").trigger("chosen:updated");
    $('#Remittance_Type_Id').val("").trigger("chosen:updated");
    
    $('#Bank_Id').val("").trigger("chosen:updated");

    
    $('#Remittance_Type_Id').val("").trigger("chosen:updated");
    $('#Remittance_Subtype_Id').val("").trigger("chosen:updated");
    $('#btn-save').removeAttr('disabled');
}

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
    if (typedChar === "-" && this.value === "") {
        return;
    }



    // In all other cases, suppress the event
    return false;
});

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

