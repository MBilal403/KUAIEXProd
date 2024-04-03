var IsEditMode = false;

$(document).ready(function () {
    handleStaff();
    LoadGridData();

    $(document).ajaxStart(function () {
        $("#wait").css("display", "block");
    });
    $(document).ajaxComplete(function () {
        $("#wait").css("display", "none");
    });
    $(document).on('click', '.btn-setLimits', function () {
        var uid = $(this).attr('id');
        window.location = "../Currency/Limits?UID=" + uid;
    });
});


// Edit method
$(document).on('click', '.btn-edit', function () {
    var UID = $(this).attr('id');
    var data = new FormData();
    data.append("UID", UID);

    $.ajax({
        type: "POST",
        cache: false,
        url: "../Currency/Edit",
        data: data,
        processData: false,
        contentType: false,
        success: function (Rdata) {
            if (Rdata != 'error') {
                var obj = JSON.parse(Rdata);
                $('.required-text').text('');
                $('#Id').val(obj.Id);
                $('#Name').val(obj.Name);
                $('#Code').val(obj.Code);
                $('#DD_Rate').val(obj.DD_Rate);
                if (obj.Status == "A") {
                    $("#Status").iCheck('check');
                } else {
                    $("#Status").iCheck('uncheck');
                }
                $('#btn-save').html("<i class='fa fa-save'></i> Update");
                IsEditMode = true;
                $('#Name').prop('disabled', true);
                $('#Code').prop('disabled', true);
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

// Handle stuff
var handleStaff = function () {
    $(".frmAddCurrency").submit(function (event) {
        event.preventDefault();
        if (validateForm()) {
            $('#btn-save').attr('disabled', 'true');
            $(".frmAddCurrency :disabled").removeAttr('disabled');
            if (!IsEditMode) {
                $.post(
                    "../Currency/AddCurrency",
                    $(".frmAddCurrency").serialize(),
                    function (value) {
                        if (value == 'duplicate_value_exist') {
                            swal("Error", "Country Already Exist", "error");
                            $('#btn-save').removeAttr('disabled');
                        }

                        else if (value == 'insert_success') {
                            swal(
                                'Success',
                                'Country Saved Successfully!',
                                'success'
                            );
                            resetForm();
                            $('#btn-save').removeAttr('disabled');
                            $('#tblUsers').DataTable().clear().draw;
                            LoadGridData();
                        }
                        else {
                            swal("Error", "Data Not Saved. Please Refresh & Try Again", "error");
                            $('#btn-save').removeAttr('disabled');
                        }
                    },
                    "text"
                );
            } else {
                $.post(
                    "../Currency/EditCurrency",
                    $(".frmAddCurrency").serialize(),
                    function (value) {
                        if (value == 'duplicate_value_exist') {
                            swal(
                                'Warning',
                                'Currency Name Already Exist!',
                                'warning'
                            );
                            return;
                        }
                        if (value == 'update_success') {
                            swal(
                                'Success',
                                'Currency Updated Successfully!',
                                'success'
                            );
                            resetForm();
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
        }

    });

    function validateForm() {
        var isValid = true;

        // Reset previous validation messages
        $('.required-text').text('');

        // Define the fields to be validated
        var fieldsToValidate = ['Name', 'Code', 'DD_Rate'];

        // Check each field
        fieldsToValidate.forEach(function (fieldName) {
            var fieldValue = $('#' + fieldName).val().trim();

            if (fieldValue === '') {
                isValid = false;
                $('#Val' + fieldName).text(' required.');
            }
        });

        return isValid;
    }


    $('.frmAddCurrency').keypress(function (e) {
        if (e.which == 13) {
            if ($('.frmAddCurrency').validate().form()) {
                $('.frmAddCurrency').submit();
            }
            return false;
        }
    });

    jQuery('#btn-save').click(function () {
        if ($('.frmAddCurrency').validate().form()) {
            $('.frmAddCurrency').submit();
        }
    });
}

// Load grid
var LoadGridData = function () {
    $('#tblUsers').DataTable({
        "destroy": true,
        "lengthMenu": [10, 25, 50, 100],
        "sAjaxSource": "../Currency/LoadGrid",
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
                "searchable": true,
                "render": function (data, type, row) {
                    if (row.IsBaseCurrency === 1) {
                        return data + '    <span  class="label label-info label-sm">Is Based</span>';
                    } else {
                        return data;
                    }
                }
            },
            {
                "data": "Code",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "DD_Rate",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "Status",
                "render": function (data, type, row) {

                    return data === 'A' ? '<span class="label label-success label-xs">Active</span>' : '<span class="label label-danger label-xs">In Active</span>';
                },
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "UID",
                "render": function (data, type, row) {
                    return '<button id="' + data + '" class="btn btn-warning btn-block btn-xs btn-edit" style="width: 80px;">' +
                        '<i class="fa fa-edit"></i>' +
                        ' Edit' +
                        '</button>' +
                        '<button id="' + data + '" class="btn btn-primary btn-block btn-xs btn-setLimits" style="width: 80px; margin-top: 5px;">' +
                        '<i class="fa fa-edit"></i>' +
                        ' Set Limits ' +
                        '</button>';
                },
                "autoWidth": true
            }

        ]
    });
};

// Reset values
function resetForm() {
    // Reset input values
    $('#Name').prop('disabled', false);
    $('#Code').prop('disabled', false);
    $('.frmAddCurrency input[type="text"]').val('');
    $('.frmAddCurrency input[type="number"]').val('');

    $("#Status").iCheck('uncheck');
    $('#btn-save').html("<i class='fa fa-save'></i> Save");
}

$('#btn-refresh').click(function () {
    resetForm();
});


$('#btn-sync').on('click', function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Currency/SynchronizeRecords",
        processData: false,
        contentType: false,
        success: function (data) {
            LoadGridData();
            swal(
                'Success',
                data + ' Records Synchronized Successfully.',
                'success'
            );

        }
    });
});
