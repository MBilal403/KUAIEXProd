
//const messages = document.getElementById('messages');

$(document).ready(function () {

    //handleStaff();
    LoadMainData();

    $(document).on('click', "#chatContactTab li a", function () {
        //console.log("Testing...");
        $("#chatContactTab li").removeClass('active');
        $(this).parent().addClass('active');
        //alert($(this).find('input').val());
        LoadTitleData($(this).find('input').val());
        LoadCustomerChat($(this).find('input').val());

    });

    $('#btn-save').click(function () {
        $("#chatContactTab li").each(function () {
            if ($(this).hasClass("active")) {
                var uid = $(this).find('input').val();

                SendMessage(uid, $('#messageInput').val())                
            }
        });

    });

    setInterval(function () {
        $("#chatContactTab li").each(function () {
            if ($(this).hasClass("active")) {
                var uid = $(this).find('input').val();
                LoadCustomerChat(uid);
            }
        });
    }, 10000); // the "3000" 
    

    //$(document).ajaxStart(function () {
    //    $(window).scrollTop(0);
    //    $("#wait").css("display", "block");
    //});
    //$(document).ajaxComplete(function () {
    //    $("#wait").css("display", "none");
    //});

});

var ScrollToBottom = function () {
    var elmnt = document.getElementById("chat-finished");
    elmnt.scrollIntoView({ block: 'end', behavior: 'auto' });
}
   


var LoadMainData = function () {
    //alert("Testing");
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Chat/CustomerChatMain",
        processData: false,
        contentType: false,
        async: false,
        success: function (Rdata) {

            var data = JSON.parse(Rdata);

            var html = "";

            for (var i = 0; i < data.length; i++) {
                var obj = data[i];
                //alert(obj);
                html += '<li class="contacts-item friends ">';
                html += '<a class="contacts-link" href="#">';
                html += '<div class="avatar ">';
                html += '<span style="radius:10px; background-color:#E8E8E8; border:1px #4b0082">' + obj.NickName + '</span>';
                html += '</div >';
                html += '<div class="contacts-content">';
                html += '<div class="contacts-info">';
                html += '<h6 class="chat-name text-truncate">' + obj.Name + '</h6>';
                html += '<div class="chat-time">' + obj.ChatOn + '</div>';
                html += '</div>';
                html += '<div class="contacts-texts" style="display:none;">';
                html += '<input type="text" id="UID" value="' + obj.UID + '" />';
                //html += '<p class="text-truncate">I’m sorry, I didn’t catch that. Could you please repeat?</p>';
                html += '</div>';
                html += '</div>';
                html += '</a >';
                html += '</li >';

            }

            $('#chatContactTab').append(html);

        }
    });
}




function LoadTitleData(UID) {
    //alert("Testing");
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Chat/CustomerChatTitle?UID=" + UID,
        processData: false,
        contentType: false,
        async: false,
        success: function (Rdata) {

            var data = JSON.parse(Rdata);

            var html = "";
            for (var i = 0; i < data.length; i++) {
                var obj = data[i];
                console.log(obj);
                html += '<div class="avatar d-none d-sm-inline-block mr-3">';
                html += '<span style="radius:10px; background-color:#E8E8E8; border:1px #4b0082">' + obj.NickName + '</span>';
                html += '</div>';

                html += '<div class="media-body align-self-center ">';
                html += '<h6 class="text-truncate mb-0">' + obj.Name + '</h6>';
                html += '</div>';
            }
            $('#ChatTitle').empty();
            $('#ChatTitle').append(html);


        }
    });
}



function LoadCustomerChat(UID) {
    //alert("Testing");
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Chat/CustomerChatDetail?UID=" + UID,
        processData: false,
        contentType: false,
        success: function (Rdata) {

            var data = JSON.parse(Rdata);

            var html = "";
            for (var i = 0; i < data.length; i++) {
                var obj = data[i];
                console.log(obj);

                if (obj.Message_Type == 1) {
                    //    //Self Message Start
                    html += '<div class="message self">';
                    html += '<div class="message-wrapper">';
                    html += '<div class="message-content">';
                    html += '<span>' + obj.Message + '</span>';
                    html += '</div>';
                    html += ' </div>';
                    html += '</div>';
                    //    //Self Message End

                }
                else if (obj.Message_Type == 2) {
                    //  //Received Message Start
                    html += '<div class="message">';
                    html += '<div class="message-wrapper">';
                    html += '<div class="message-content">';
                    html += '<span>' + obj.Message + '</span>';
                    html += '</div>';
                    html += '</div>';
                    html += '</div>';
                    //    //Received Message End
                }
            }
            $('.message-day').empty();
            $('.message-day').append(html);

            ScrollToBottom();
          
        }
    });
}



function SendMessage(UID, Msg) {
    //alert("Testing");
    $.ajax({
        type: "POST",
        cache: false,
        url: "../Chat/SaveChat?UID=" + UID + "&Message=" + Msg,
        processData: false,
        contentType: false,
        success: function (data) {

            if (data != "error") {
                var html = "";
                //    //Self Message Start
                html += '<div class="message self">';
                html += '<div class="message-wrapper">';
                html += '<div class="message-content">';
                html += '<span>' + $('#messageInput').val() + '</span>';
                html += '</div>';
                html += ' </div>';
                html += '</div>';
                //    //Self Message End

                $('.message-day').append(html);
                $('#messageInput').html('');
                ScrollToBottom();
                
                


                
            }
        }
    });
}

