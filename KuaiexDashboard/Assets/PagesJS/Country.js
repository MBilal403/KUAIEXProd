var IsEditMode = false;

$(document).ready(function () {
    handleStaff();
    LoadGridData();
    $("#btn-save").addClass("disabled").attr("disabled", true);
    $(document).ajaxStart(function () {

        $("#wait").css("display", "block");
    });

    $(document).ajaxComplete(function () {
        $("#wait").css("display", "none");
    });
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
                $('#Name').prop('disabled', true);
                $('#Nationality').val(obj.Nationality);
                $('#Nationality').prop('disabled', true);
                $('#Alpha_2_Code').val(obj.Alpha_2_Code);
                $('#Alpha_2_Code').prop('disabled', true);
                $('#Alpha_3_Code').val(obj.Alpha_3_Code);
                $('#Country_Dialing_Code').val(obj.Country_Dialing_Code);
                IsEditMode = true;
                $("#btn-save").removeClass("disabled").removeAttr("disabled");

                if (obj.Under_Review_Status === "A") {
                    $("#Under_Review_Status").iCheck('check');
                } else {
                    $("#Under_Review_Status").iCheck('uncheck');
                }
                if (obj.High_Risk_Status === "A") {
                    $("#High_Risk_Status").iCheck('check');
                } else {
                    $("#Status").iCheck('uncheck');
                }
                if (obj.Status === "A") {
                    $("#Status").iCheck('check');
                } else {
                    $("#Status").iCheck('uncheck');
                }

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

$("#Status").on('ifChecked', function (event) {
    $(this).closest("input").attr('value', 1);
});

$("#Status").on('ifUnchecked', function (event) {
    $(this).closest("input").attr('value', 0);
});

// Handle stuff
var handleStaff = function () {
    $(".frmAddUsers").submit(function (event) {
        event.preventDefault();
        if (validateForm()) {
         
            $(".frmAddUsers :disabled").removeAttr('disabled');
            if (!IsEditMode) {
                $.post(
                    "../country/AddCountry",
                    $(".frmAddUsers").serialize(),
                    function (value) {
                        if (value == 'duplicate_value_exist') {
                            swal("Error", "Country Already Exist", "error");
                            $('#btn-save').removeAttr('disabled');
                        }

                        else if (value != 'error') {
                            swal(
                                'Success',
                                'Country Saved Successfully!',
                                'success'
                            );
                            resetForm();
                            $('#btn-save').removeAttr('disabled');
                            $('#tblCountry').DataTable().clear().draw;
                            LoadGridData();
                        }
                        else {
                            swal("Error", "Data Not Saved. Please Refresh & Try Again", "error");
                            $("#btn-save").removeClass("disabled").removeAttr("disabled");
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
                                'Country Updated Successfully!',
                                'success'
                            );
                            resetForm();
                       
                            $('#tblCountry').DataTable().clear().draw;
                            LoadGridData();
                        } else {
                            swal("Error", "Data not updated!!", "error");
                            $("#btn-save").removeClass("disabled").removeAttr("disabled");
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
        var fieldsToValidate = ['Alpha_2_Code', 'Nationality', 'Name'];

        fieldsToValidate.forEach(function (fieldName) {
            var fieldValue = $('#' + fieldName).val().trim();

            if (fieldValue === '') {
                isValid = false;
                $('#Val' + fieldName).text(' required ');
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

    // Load grid
    var LoadGridData = function () {
        $('#tblCountry').DataTable({
            "destroy": true,
            "lengthMenu": [10, 25, 50, 100],
            "sAjaxSource": "../country/LoadGrid",
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
                    "data": "Nationality",
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "Alpha_2_Code",
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "City",
                    "autoWidth": true,
                    "searchable": true,
                    "render": function (data, type, row) {
                        
                        if (!data) {
                            return "<span style='display: block; text-align: center;'>-</span>"; 
                        }
                        return data; 
                    }
                },
                {
                    "data": "High_Risk_Status",
                    "render": function (data, type, row) {
                        return data === 'A' ? '<span class="label label-success label-xs">Active</span>' : '<span class="label label-danger label-xs">In Active</span>';
                    },
                    "autoWidth": true,
                    "searchable": true
                },
                {
                    "data": "Under_Review_Status",
                    "render": function (data, type, row) {
                        return data === 'A' ? '<span class="label label-success label-xs">Active</span>' : '<span class="label label-danger label-xs">In Active</span>';
                    },
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

    // Reset values
    function resetForm() {
        IsEditMode = false;
        $('#Name').val('');
        $('#Country_Dialing_Code').val('');
        $('#Alpha_3_Code').val('');
        $('#Alpha_2_Code').val('');
        $('#Nationality').val('');
        $("#High_Risk_Status").iCheck('uncheck');
        $("#Under_Review_Status").iCheck('uncheck');
        $("#Status").iCheck('uncheck');
        $("#btn-save").addClass("disabled").attr("disabled", true);
        $('#Name').prop('disabled', false);
        $('#Nationality').prop('disabled', false);
        $('#Alpha_2_Code').prop('disabled', false);

    }

    $('#btn-refresh').click(function () {
        resetForm();
    });


    $('#btn-sync').on('click', function () {
        $.ajax({
            type: "POST",
            cache: false,
            url: "../Country/SynchronizeRecords",
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