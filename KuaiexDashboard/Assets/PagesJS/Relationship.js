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
});


//edit method
$(document).on('click', '.btn-edit', function () {
    var uid = $(this).attr('id');
    var data = new FormData();
    data.append("Id", uid);
    $.ajax({
        type: "POST",
        cache: false,
        url: "../GeneralSettings/GetRelationship",
        data: data,
        processData: false,
        contentType: false,
        success: function (Rdata) {
            if (Rdata != 'error') {
                var obj = JSON.parse(Rdata);
                $('#Relationship_Id').val(obj.Relationship_Id);
                $('#Name').val(obj.Name);
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
                    "../GeneralSettings/AddRelationship",
                    $(".frmAddUsers").serialize(),
                    function (value) {
                        if (value === "duplicate_value_exist") {
                            swal(
                                'Error',
                                'Relationship Already Exist!',
                                'error'
                            )
                            Reset();
                            $('#btn-save').removeAttr('disabled');
                        }
                        else if (value === "insert_success") {
                            swal(
                                'Success',
                                'Relationship Saved Successfully!',
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
                    "../GeneralSettings/EditRelationship",
                    $(".frmAddUsers").serialize(),
                    function (value) {
                        if (value == 'duplicate_value_exist') {
                            swal(
                                'Warning',
                                'Relationship Name Already Exist!',
                                'warning'
                            )
                            return;
                        }
                        if (value == 'update_success') {
                            swal(
                                'Success',
                                'Relationship Updated Successfully!',
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
        var fieldsToValidate = ['Name'];

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

var LoadGridData = function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Banks/LoadGrid1",
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

                if (obj.Address_Line1 != null || obj.Address_Line2 != null || obj.Address_Line3 != null) {
                    html += '<td>' + obj.Address_Line1 + ' ' + obj.Address_Line2 + ' ' + obj.Address_Line3 + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }

                html += '</tr>'
            }
            $("#tblbody").append(html);
            $('#tblbank').DataTable().draw();
        }
    });
}

var LoadGridData = function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../GeneralSettings/LoadGridRelationship",
        processData: false,
        contentType: false,
        success: function (data) {
            data = JSON.parse(data);
            $('#tblUsers').DataTable().destroy();
            var html = '';
            for (var i = 0; i < data.length; i++) {
                var obj = data[i];
            
                html += '<tr>';

                html += '<td class="hidden">' + obj.Relationship_Id + '</td>';

                if (obj.Name != null) {
                    html += '<td>' + obj.Name + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }
             
                if (obj.Status == 1) {
                    html += '<td><span class="label label-success label-xs">Active</span></td>';
                }
                else {
                    html += '<td><span class="label label-danger label-xs">In Active</span></td>';
                }
                
                html += '<td>';
                html += '<button id=' + obj.Relationship_Id + ' class="btn btn-warning btn-block btn-xs btn-edit" style="width: 80px;">';
                html += '<i class="fa fa-edit"></i>';
                html += ' Edit';
                html += ' </button>';
                html += ' </td>';

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




    function Reset() {
        IsEditMode = false;
        $('#Name').val('');
        $("#Status").iCheck('uncheck');

        $('#btn-save').html("<i class='fa fa-save'></i> Save");
        $('#btn-save').removeAttr('disabled');
    }
    