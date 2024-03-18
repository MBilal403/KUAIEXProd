var IsEditMode = false;
var RemittancePurpose;
var RemitterRelation;
var SourceOfIncome;

$(document).ready(function () {

    var urlParams = new URLSearchParams(window.location.search);
    var uid = urlParams.get('UID');
    LoadGridData(uid);
    $(document).on('click', '.btn-edit', function () {
        var id = $(this).attr('id');
        window.location = "../Beneficiary/EditBene?UID=" + id + "&CUID=" + uid;
    });


    $('#addBeneficiaryBtn').on('click', function () {
        // Extract UID from the URL
        // Redirect to the appropriate URL with UID as a parameter
        if (uid !== null) {
            window.location.href = '/Beneficiary/Add?UID=' + uid;
        } else {
            window.location.href = "../Remitter/Index";
        }
    });
    
});
//load grid
var LoadGridData = function (uid) {
    $.ajax({
        type: "GET",
        cache: false,
        async: false,
        url: "../Beneficiary/LoadGrid?CUID= "+ uid,
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

                if (obj.Currency_Name != null) {
                    html += '<td>' + obj.Currency_Name + '</td>';
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
    resetForm();
});

// Getting Value From Query String By Param Name
function getParameterByName(name) {
    var match = RegExp('[?&]' + name + '=([^&]*)').exec(window.location.href);
    return match && decodeURIComponent(match[1].replace(/\+/g, ' '));
}

