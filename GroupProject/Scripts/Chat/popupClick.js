let chatCallbackThatGetsFollowers = $.Callbacks("once");
let chatMyFollowersUserName = [];
let isOpen = false;
$(document).on("click", ".chat-window-popup", function () {
    

    if (!isOpen) {
        $(this).css("bottom", "400px").css("transition", "0.3s");
        $(".chat-followers-col").css("bottom", "0px").css("transition", "0.3s");

        chatCallbackThatGetsFollowers.add(getFollowers);
        chatCallbackThatGetsFollowers.fire();

        function getFollowers() {

            $.ajax({
                url: "/api/users/chat/follow",
                method: "Get",
            }).done(function (data) {
                let user = $(".chat-followers-col .chat-followers-list");
                data.forEach(function (f) {
                    let templ = `<div data-val-id="${f.id}" class="chat-each-follower">
                        <div class="chat-follower-photo">
                            <img class="img-fluid" src="${f.imageName}" />
                        </div>
                        <div class="chat-follower-details">
                            <span>${f.isDeveloper ? f.developer.fullName : f.company.companyName}</span >
                        </div>
                    </div>`;

                    user.append(templ);
                    chatMyFollowersUserName.push(f.userName);

                });

            })
                .fail(function () {
                    toastr("Try Refreshing!");
                });
        };
    }
    else {
        $(this).css("bottom", "0px").css("transition", "0.3s");
        $(".chat-followers-col").css("bottom", "-400px").css("transition", "0.3s");
        $(".chat-messaging-col").css("bottom", "-400px");
    }

    isOpen = !isOpen;
    
});
