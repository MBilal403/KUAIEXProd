$(document).ready(function () {
    LoadGridData();
});


var LoadGridData = function () {
    $.ajax({
        type: "GET",
        cache: false,
        url: "../RemittanceTransaction/LoadGrid",
        processData: false,
        contentType: false,
        success: function (data) {
            data = JSON.parse(data);
            $('#tblRemitterTransaction').DataTable().destroy();
            var html = '';
            for (var i = 0; i < data.length; i++) {
                var obj = data[i];

                html += '<tr>';

                html += '<td class="hidden">' + obj.Remittance_Id  + '</td>';

                if (obj.FullName != null) {
                    html += '<td>' + obj.Remitter_Name  + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }
                if (obj.Customer_Name != null) {
                    html += '<td>' + obj.Country_Name  + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }
                if (obj.Birth_Date != null) {
                    html += '<td>' + obj.Remittance_Date  + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }

                if (obj.Address_Line3 != null) {
                    html += '<td>' + obj.Beneficiary_Name   + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }
                if (obj.Branch_Address_Line3 != null) {
                    html += '<td>' + obj.Identification_Number  + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }
                if (obj.Branch_Address_Line3 != null) {
                    html += '<td>' + obj.DD_Number  + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }

                if (obj.Country_Name != null) {
                    html += '<td>' + obj.Bank_Name  + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }

                if (obj.Currency != null) {
                    html += '<td>' + obj.Customer_Name + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }

                
                html += '</tr>'
            }
            $("#tblbody").append(html);
            $('#tblRemitterTransaction').DataTable().draw();
        }
    });
}