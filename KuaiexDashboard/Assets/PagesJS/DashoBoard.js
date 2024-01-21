$(document).ready(function () {
    //$('#tblUsers').DataTable({ responsive: true });
   
   LoadGridData();
   LoadTodayCustomers();
   LoadTotalCustomers();
   LoadTodayReviwed();
   LoadTotalReviwed();

    $(document).ajaxStart(function () {
        $(window).scrollTop(0);
        $("#wait").css("display", "block");
    });
    $(document).ajaxComplete(function () {
        $("#wait").css("display", "none");
    });
});

var LoadGridData = function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Dashboard/LoadGrid",
        processData: false,
        contentType: false,
        success: function (data) {
            data = JSON.parse(data);
            //$('#tblUsers').DataTable().destroy();
            var html = '';
            for (var i = 0; i < data.length; i++) {
                var obj = data[i];



                html += '<tr>';

                html += '<td class="hidden">' + obj.Id + '</td>';

                if (obj.Name != null) {
                    html += '<td>' + obj.Name + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }
                if (obj.Code != null) {
                    html += '<td>' + obj.Code + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }
                if (obj.DD_Rate != null) {
                    html += '<td>' + obj.DD_Rate + '</td>';
                }
                else {
                    html += '<td>-</td>';
                }


               


                //html += '<td>';
                //html += '<button id=' + obj.UID + ' class="btn btn-warning btn-block btn-xs btn-edit" style="width: 80px;">';
                //html += '<i class="fa fa-edit"></i>';
                //html += ' Edit';
                //html += ' </button>';
                //html += ' </td>';

                //html += ' <td>';
                //html += '<a href="../UserRights/Index/?UID=' + obj.UID + '" class="btn btn-info btn-block btn-xs" style="width: 80px;">';
                //html += '<i class="fa fa-users"></i>';
                //html += ' Rights';
                //html += ' </a>';
                //html += ' </td>';

                html += '</tr>';
            }
            $("#tblbody").append(html);
            //$('#tblUsers').DataTable().draw();
        }
    });
}

var LoadTodayCustomers = function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Dashboard/TodayCustomers",
        processData: false,
        contentType: false,
        success: function (data) {
            

            $("#Tday").html(data);
           // $('#tblUsers').DataTable().draw();
        }
    });
}

var LoadTotalCustomers = function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Dashboard/TotalCustomers",
        processData: false,
        contentType: false,
        success: function (data) {

            $("#Tlday").html(data);
            // $('#tblUsers').DataTable().draw();
        }
    });
}

var LoadTodayReviwed = function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Dashboard/TodayReviwed",
        processData: false,
        contentType: false,
        success: function (data) {

            $("#Appday").html(data);
            // $('#tblUsers').DataTable().draw();
        }
    });
}

var LoadTotalReviwed = function () {
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Dashboard/TotalReviwed",
        processData: false,
        contentType: false,
        success: function (data) {

            $("#TAppday").html(data);
            // $('#tblUsers').DataTable().draw();
        }
    });
}