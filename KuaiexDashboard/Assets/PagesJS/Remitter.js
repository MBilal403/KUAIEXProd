
$(document).ready(function () {

    LoadGridData();
    $(document).ajaxStart(function () {
        $(window).scrollTop(0);
        $("#wait").css("display", "block");
    });
    $(document).ajaxComplete(function () {
        $("#wait").css("display", "none");
    });
});


//edit method
$(document).on('click', '.btn-bene', function () {
    var uid = $(this).attr('id');
    window.location = "../Beneficiary/Index?UID=" + uid;
});
$(document).on('click', '.btn-edit', function () {
    var uid = $(this).attr('id');
    window.location = "../Remitter/Add?UID=" + uid;
});


var LoadGridData = function () {
    $('#tblUsers').DataTable({
        "destroy": true,
        "lengthMenu": [5, 25, 50, 75, 100],
        "sAjaxSource": "../Remitter/LoadGrid",
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
                "data": "Description",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "Identification_Number",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "Email_Address",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "Identification_Expiry_Date",
                "render": function (data, type, row) {
                    var timestamp = parseInt(data.match(/\d+/)[0]); 
                    var date = new Date(timestamp);
                    return date.toLocaleDateString();
                },
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "IsReviwed",
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
                    return '<button id=' + data + ' class="btn btn-warning btn-block btn-xs btn-edit" style="width: 60px;">' +
                        '<i class="fa fa-edit"></i>' +
                        ' Edit' +
                        '</button>';
                },

                "autoWidth": true
            },
            {
                "data": "UID",
                "render": function (data, type, row) {
                    console.log(data);
                    return '<button id=' + data + ' class="btn btn-warning btn-block btn-xs btn-bene" style="width: 60px;">' +
                        '<i class="fa fa-edit"></i>' +
                        ' Beneficiaries' +
                        '</button>';
                },

                "autoWidth": true
            }
           

        ]
    });
};



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
