let ChatService = function () {
    let conn = $.connection;
    

    //let connection = HubBooter.Connection;
    let chat = conn.chat;

    let id;
    let timesRequested = 0;
    console.log("chat service running");


    function findSelectedUserDetails(whoClicked) {
        let img = whoClicked.find("img").attr("src");
        let name = whoClicked.find("span").text();
        name1 = name;
        let details = { img: img, name: name };

        return details;
    };


    function UpdateScrollbar() {
        let b = document.getElementById("show-messages");
        b.scrollTop = b.scrollHeight;
    }

    function showDetailsToChatWindow(details) {
        $(".chat-user-info .chat-follower-photo").attr("style", "background: url('" + details.img + "')");
        $(".chat-follower-details .chat-user-name").text(details.name);

    };


    function ScrollingBind() {
        if ($(".chat-message-window").scrollTop() == 0) {
            timesRequested = timesRequested + 1;
            chat.server.getSelectedUserChatHistory(id, timesRequested);
        }
    };


    let showSentMessage = function (message) {
        let cont = $("#show-messages");
        let template = `<div class="showing-messages">
            <small>${moment().format("HH:mm")}</small>
            <div class="sent-messages">
                <div class="message-content">
                    <div class="message-text">${message}</div>
                </div>
            </div>
        </div>`;
        cont.append(template);
    };


    let showReceivedMessage = function (message) {
        let cont = $("#show-messages");
        let template = `<div class="showing-messages">
            <small>${moment(message.timeSent).format("HH:mm")}</small>
            <div class="received-messages">
                <div class="message-content">
                    <div class="message-text">${message.messageContent}</div>
                </div>
            </div>
        </div>`;
        timesRequested == 0 ? cont.append(template) : cont.prepend(template);

    };


    let showHistoryMessages = function (message) {
        let cont = $("#show-messages");
        let template;
        if (chatMyFollowersUserName.includes(message.fromUserName))
            showReceivedMessage(message);
        else {
            template = `<div class="showing-messages">
            <small>${moment(message.timeSent).format("HH:mm")}</small>
            <div class="sent-messages">
                <div class="message-content">
                    <div class="message-text">${message.messageContent}</div>
                </div>
            </div>
        </div>`;
        }
        timesRequested == 0 ? cont.append(template) : cont.prepend(template);

    }

    $("#msg-form").submit(function (e) {
        e.preventDefault();
        let message = this.children[0].value;
        showSentMessage(message);
        chat.server.sendMsg(id, message);
        UpdateScrollbar();
    });

    $(document).on("click", ".chat-each-follower", function () {
        let clicked = $(this);
        let a = clicked.attr("data-val-id");
        timesRequested = 0;
        $("#show-messages").html(" ");
        if (typeof (id) === "undefined" || id != a) {
            id = a;
            let details = findSelectedUserDetails(clicked);
            showDetailsToChatWindow(details);

            $('.chat-messaging-col').css("bottom", "0px").css("transition", "0.3s");
            conn.chat.server.getSelectedUserChatHistory(id, timesRequested);
            conn.chat.server.checkUserStatus(id);
            $(".chat-message-window").bind('scroll', ScrollingBind);
        }

    });


    conn.chat.client.userStatus = function (online) {
        if (online) {
            $(".chat-messaging-col .chat-current-chatting-user .chat-follower-photo.fa-circle").css("color:rgb(37, 163, 0);");
        }
        else {
            $(".chat-messaging-col .chat-current-chatting-user .chat-follower-photo.fa-circle").css("color:gray;");
        }
    };


    conn.chat.client.receive = function (message) {
        let message1 = JSON.parse(message);
        showReceivedMessage(message1);
        UpdateScrollbar();
    };



    conn.chat.client.getHistory = function (dataReturned) {
        console.log(dataReturned);
        if (dataReturned === "You got all Chat History!") {
            $(".chat-message-window").unbind('scroll', ScrollingBind);
            let cont = $("#show-messages");
            let tmpl = `<div class="showing-messages all-chat-history"><strong>${dataReturned}</strong><div>`;
            cont.prepend(tmpl);
        }
        else {
            let incomingMessages = JSON.parse(dataReturned);

            incomingMessages.forEach(x => showHistoryMessages(x));
        }
        if (timesRequested == 0) {
            UpdateScrollbar();
        }
    };

    //console.log("hello");
    //conn.feed.client.receivePost = function (post) {
    //    console.log("hello2");
    //    post = JSON.parse(post);
    //    FeedController.AddPostToDOM(post, true);
    //};


    conn.hub.start().done(() => console.log("twra mpike"));


    //HubBooter.Start();


}();