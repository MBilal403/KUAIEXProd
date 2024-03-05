$(document).ready(function () {
    LoadGridData();
});


var LoadGridData = function () {
    $('#tblRemitterTransaction').DataTable({
        "destroy": true,
        "lengthMenu": [10, 25, 50, 75],
        "sAjaxSource": "../RemittanceTransaction/LoadGrid",
        "bServerSide": true,
        "bProcessing": true,
        "paging": true,
        "order": [[1, 'asc']],
        "language": {
            "emptyTable": "No record found."
        },
        "columns": [
            {
                "data": "Remitter_Name",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "Address_Line3",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "Identification_Number",    
                "autoWidth": true,
                "searchable": true
            }
            ,
            {
                "data": "Beneficiary_Name",
                "autoWidth": true,
                "searchable": true
            }
            ,
             {
                 "data": "Remittance_Date",
                "render": function (data, type, row) {
                    var timestamp = parseInt(data.match(/\d+/)[0]);
                    var date = new Date(timestamp);
                    return date.toLocaleDateString();
                },
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "DD_Number",
                "autoWidth": true,
                "searchable": true
            }
            ,
            {
                "data": "Bank_Name",
                "autoWidth": true,
                "searchable": true
            }
            ,
            {
                "data": "Amount_FC",
                "autoWidth": true,
                "searchable": true
            }
            ,
            {
                "data": "Amount_LC",
                "autoWidth": true,
                "searchable": true
            }
        ]
    });
};