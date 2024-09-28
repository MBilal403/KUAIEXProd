var jsonString;
$(document).ready(function () {

    LoadGridData();
    $(document).ajaxStart(function () {
        $(window).scrollTop(0);
        $("#wait").css("display", "block");
    });
    $(document).ajaxComplete(function () {
        $("#wait").css("display", "none");
    });
    $('#btnsearch').on('click', function (event) {
        event.preventDefault();

        var name = $('#Name').val().trim();
        var identificationNumber = $('#Identification_Number').val().trim();

        var searchCriteria = {
            Name: name !== "" ? name : null,
            Identification_Number: identificationNumber !== "" ? identificationNumber : null
        };
        if (searchCriteria.Name === null && searchCriteria.Identification_Number === null) {
            alert("Both fields are empty. Please enter at least one search criterion.");
            jsonString = null;
            return;
        }

        jsonString = JSON.stringify(searchCriteria);

        console.log(jsonString);
        $('#tblUsers').DataTable().draw();

    });

    $('#btnclear').on('click', function () {
        // Clear the input fields
        $('#Name').val('');
        $('#Identification_Number').val('');

        // Set jsonString to null
        jsonString = null;

        $('#tblUsers').DataTable().draw();
    });

});

$(document).on('click', '.btn-bene', function () {
    var uid = $(this).attr('id');
    window.location = "../Beneficiary/Index?UID=" + uid;
});
$(document).on('click', '.btn-edit', function () {
    var uid = $(this).attr('id');
    window.location = "../Remitter/Add?UID=" + uid;
});

var fetchGridData = function (callback, settings) {
    //console.log(settings)
    var requestData = {
        start: settings._iDisplayStart,
        length: settings._iDisplayLength,
        //search: settings.oPreviousSearch.sSearch,
        search: jsonString,
        order: settings.aaSorting,
        columns: settings.aoColumns.map(function (col) {
            return {
                data: col.data,
                searchable: col.bSearchable,
                orderable: col.bSortable,
                search: col.sSearch
            };
        })
    };
    console.log(requestData)

    $.ajax({
        url: "../Remitter/LoadGrid",
        type: "POST",
        data: requestData,
        success: function (response) {
            callback({
                draw: settings.iDraw,
                recordsTotal: response.recordsTotal,
                recordsFiltered: response.recordsFiltered,
                data: response.data
            });
        },
        error: function (xhr, status, error) {
            console.log("Error fetching data: ", error);
        }
    });
};
var LoadGridData = function () {
    $('#tblUsers').DataTable({
        "destroy": true,
        "lengthMenu": [10, 25, 50, 100],
        "processing": true,
        "serverSide": true,
        "responsive": true,
        "dom": '<"row"<"col-sm-6"l><"col-sm-6 text-right">>rtip',
        "paging": true,
        "order": [[1, 'asc']],
        "language": {
            "emptyTable": "No record found."
        },
        "ajax": function (data, callback, settings) {
            fetchGridData(callback, settings);
        },
        "columns": [
            { "data": "Name", "autoWidth": true, "searchable": true },
            { "data": "Description", "autoWidth": true, "searchable": true },
            { "data": "Identification_Number", "autoWidth": true, "searchable": true },
            { "data": "Email_Address", "autoWidth": true, "searchable": true },
            {
                "data": "Identification_Expiry_Date",
                "render": function (data) {
                    var timestamp = parseInt(data.match(/\d+/)[0]);
                    var date = new Date(timestamp);
                    return date.toLocaleDateString();
                },
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "IsReviwed",
                "render": function (data) {
                    return data == 1 ?
                        '<span class="label label-success label-xs">Active</span>' :
                        '<span class="label label-danger label-xs">Inactive</span>';
                },
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "UID",
                "render": function (data) {
                    return '<button id=' + data + ' class="btn btn-warning btn-xs btn-edit"><i class="fa fa-edit"></i> Edit</button>';
                },
                "autoWidth": true, "orderable": false 
            },
            {
                "data": "UID",
                "render": function (data) {
                    return '<button id=' + data + ' class="btn btn-warning btn-xs btn-bene"><i class="fa fa-edit"></i> Beneficiaries</button>';
                },
                "autoWidth": true, "orderable": false 
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
        },
        error: function (xhr, status, error) {
            swal("Error", "Records Synchronized Failed !!", "error");
        }
    });
});
