var IsEditMode = false;

$(document).ready(function () {
    $('#tblCurrencyLimits').DataTable({ responsive: true });

    var urlParams = new URLSearchParams(window.location.search);
    var uid = urlParams.get('UID');
    $('#UID').val(uid);

    LoadGridData(uid);
    LoadComparisons();
    handleStaff();
    $(document).ajaxStart(function () {
        $("#wait").css("display", "block");
    });
    $(document).ajaxComplete(function () {
        $("#wait").css("display", "none");
    });
});

var LoadComparisons = function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Currency/LoadComparisons",
        processData: false,
        contentType: false,
        success: function (data) {
            var sch = JSON.parse(data);
            var $el = $('#Comparison_Id');
            $el.empty();
            if (sch.length > 0) {
                $el.append('<option value="">' + "Select Comparison" + '</option>');
                $.each(sch, function (idx, obj) {
                    $el.append('<option value="' + obj.Id + '">' + obj.Message + '  (' + obj.Operator + ')' + '</option>');
                });
            } else {
                $el.append('<option value="">' + "Select Comparison" + '</option>');
            }
            $el.trigger("liszt:updated");
            $el.chosen();
        }
    });
}


// Edit method
$(document).on('click', '.btn-delete', function () {
    var UID = $(this).attr('id');
    var data = new FormData();
    data.append("UID", UID);

    $.ajax({
        type: "POST",
        cache: false,
        url: "../Currency/Delete",
        data: data,
        processData: false,
        contentType: false,
        success: function (Rdata) {
            if (Rdata == 'delete_success') {
                $('#tblCurrencyLimits').DataTable().clear().draw;
                var urlParams = new URLSearchParams(window.location.search);
                var uid = urlParams.get('UID');
                $('#UID').val(uid);
                LoadGridData(uid);
                swal(
                    'Success',
                    'Limit Deleted Successfully!',
                    'success'
                );
         
                $(window).scrollTop(0);
            } else {
                ShowErrorAlert("Error", "Some Error Occurred!");
            }
        },
        error: function (e) {
            // Handle error
        }
    });
});

// Handle stuff
var handleStaff = function () {
    $(".frmAddCurrencyLimit").submit(function (event) {
        event.preventDefault();
        if (validateForm()) {
            $('#btn-save').attr('disabled', 'true');
          //  $(".frmAddCurrencyLimit :disabled").removeAttr('disabled');

            var urlParams = new URLSearchParams(window.location.search);
            var uid = urlParams.get('UID');
            $('#UID').val(uid);
            if (!IsEditMode) {
                $.post(
                    "../Currency/AddLimits?UID=" + urlParams.get('UID'),
                    $(".frmAddCurrencyLimit").serialize(),
                    function (value) {
                        if (value == 'duplicate_value_exist') {
                            swal("Warning", "Limit Already Exist", "warning");
                            $('#btn-save').removeAttr('disabled');
                        }

                        else if (value == 'insert_success') {
                            swal(
                                'Success',
                                'Limit Saved Successfully!',
                                'success'
                            );
                            resetForm();
                            $('#btn-save').removeAttr('disabled');
                            $('#tblCurrencyLimits').DataTable().clear().draw;
                            var urlParams = new URLSearchParams(window.location.search);
                            var uid = urlParams.get('UID');
                            LoadGridData(uid);
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
                    $(".frmAddCurrencyLimit").serialize(),
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
                            $('#tblCurrencyLimits').DataTable().clear().draw;
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
        var fieldsToValidate = ['Amount', 'DD_Rate','Comparison_Id'];

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


    jQuery('#btn-save').click(function () {
        if ($('.frmAddCurrencyLimit').validate().form()) {
            $('.frmAddCurrencyLimit').submit();
        }
    });
}

// Load grid


var LoadGridData = function (uid) {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Currency/LoadCurrencyLimits?UID=" + uid,
        processData: false,
        contentType: false,
        success: function (data) {
            data = JSON.parse(data);
            $('#tblCurrencyLimits').DataTable().destroy();
            var html = '';
            for (var i = 0; i < data.length; i++) {
                var obj = data[i];
                html += '<tr>';
                html += '<td class="hidden">' + obj.UID + '</td>';
                if (obj.Amount != null) {
                    html += '<td>' + obj.Amount + '</td>';
                } else {
                    html += '<td>-</td>';
                }
                if (obj.DD_Rate != null) {
                    html += '<td>' + obj.DD_Rate + '</td>';
                } else {
                    html += '<td>-</td>';
                }
                if (obj.ComparisonName != null) {
                    html += '<td>' + obj.ComparisonName + '</td>';
                } else {
                    html += '<td>-</td>';
                }
                html += '<td>';
                html += '<button id=' + obj.UID + ' class="btn btn-danger btn-block btn-xs btn-delete" style="width: 80px;">';
                html += '<i class="fa fa-delete"></i>';
                html += ' Delete';
                html += ' </button>';
                html += ' </td>';
                html += '</tr>';
            }
            $("#tblbody").append(html);
            $('#tblCurrencyLimits').DataTable().draw();
        }
    });
}

// Reset values
function resetForm() {

    $('.frmAddCurrencyLimit input[type="text"]').val('');
    $('.frmAddCurrencyLimit input[type="number"]').val('');
}

$('#btn-refresh').click(function () {
    resetForm();
});

