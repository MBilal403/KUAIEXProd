var IsEditMode = false;

$(document).ready(function () {
    handleStaff();
    LoadGridData();

    LoadCountry();

    $(document).ajaxStart(function () {
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
        url: "../City/Edit",
        data: data,
        processData: false,
        contentType: false,
        success: function (Rdata) {
            if (Rdata != 'error') {
                var obj = JSON.parse(Rdata);
                $('#UID').val(obj.UID);
                $('#Name').val(obj.Name);
                $('#Country_Id').val(obj.Country_Id).trigger("chosen:updated");
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
    $(".frmAddUsers").submit(function (event) {
        event.preventDefault();
        if (validateForm()) {
            $('#btn-save').attr('disabled', 'true');
            $(".frmAddUsers :disabled").removeAttr('disabled');
            if (!IsEditMode) {

                $.post(
                    "../City/AddCity",
                    $(".frmAddUsers").serialize(),
                    function (value) {
                        if (value === "duplicate_value_exist") {
                            swal(
                                'Error',
                                'City Already Exist!',
                                'error'
                            )
                            Reset();
                            $('#btn-save').removeAttr('disabled');
                        }
                        else if (value === "insert_success") {
                            swal(
                                'Success',
                                'City Saved Successfully!',
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
                    "../City/EditCity",
                    $(".frmAddUsers").serialize(),
                    function (value) {
                        if (value == 'duplicate_value_exist') {
                            swal(
                                'Warning',
                                'City Name Already Exist!',
                                'warning'
                            )
                            return;
                        }
                        if (value == 'update_success') {
                            swal(
                                'Success',
                                'City Updated Successfully!',
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
        }

    });

    function validateForm() {
        var isValid = true;

        $('.required-text').text('');
        var fieldsToValidate = ['Country_Id', 'Name'];

        fieldsToValidate.forEach(function (fieldName) {
            var fieldValue = $('#' + fieldName).val();

            // Assuming you have a span element with id 'Val' + fieldName to display validation messages
            var validationMessageElement = $('#Val' + fieldName);

            if (fieldValue === '' || (fieldValue === '0' && $('#' + fieldName).is('select'))) {
                isValid = false;
                validationMessageElement.text(' required ');
            } else {
                validationMessageElement.text(''); // Clear any previous validation message
            }
        });

        return isValid;
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




//load grid
var LoadGridData = function () {
    $('#tblUsers').DataTable({
        "destroy": true,
        "lengthMenu": [5, 25, 50, 75, 100],
        "sAjaxSource": "../City/LoadGrid",
        "bServerSide": true,
        "bProcessing": true,
        "paging": true,
        "order": [[1, 'asc']],
        "language": {
            "emptyTable": "No record found."
        },
        "columns": [
            {
                "data": "Name",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "Country",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "Status",
                "render": function (data, type, row) {

                    return data == 1 ? '<span class="label label-success label-xs">Active</span>' : '<span class="label label-danger label-xs">In Active</span>';
                },
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "UID",
                "render": function (data, type, row) {
                    console.log(data);
                    return '<button id=' + data + ' class="btn btn-warning btn-block btn-xs btn-edit" style="width: 80px;">' +
                        '<i class="fa fa-edit"></i>' +
                        ' Edit' +
                        '</button>';
                },
                "autoWidth": true
            }
        ]
    });
};


//reset values


$('#btn-refresh').click(function () {
    Reset();
});


// For number validation

// for loading User
var LoadCountry = function () {
    $.ajax({
        type: "POST",
        cache: false,
        async: false,
        url: "../City/LoadCountry",
        processData: false,
        contentType: false,
        success: function (data) {
            var sch = JSON.parse(data);

            var $el = $('#Country_Id');
            $el.empty();
            if (sch.length > 0) {
                $el.append('<option value="0">' + "Select Country" + '</option>');
                $.each(sch, function (idx, obj) {
                    $el.append('<option value="' + obj.Id + '">' + obj.Name + '</option>');
                });
            }
            else {
                $el.append('<option value="0">' + "Select Country" + '</option>');
            }
            $el.trigger("liszt:updated");
            $el.chosen();
        }
    });
}
// Ensure that the "Country_Id" is included in the serialized form data


function Reset() {
    IsEditMode = false;
    LoadCountry();
    $('#Name').val('');
    $('#Country_Id').val('');
    $("#Status").iCheck('uncheck');

    $('#btn-save').html("<i class='fa fa-save'></i> Save");
    $('#Country_Id').val("").trigger("chosen:updated");
    $('#btn-save').removeAttr('disabled');
}

$('#btn-sync').on('click', function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../City/SynchronizeRecords",
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
