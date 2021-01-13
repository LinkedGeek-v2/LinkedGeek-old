let NotificationService = function () {
    let notificationHub = HubBooter.Connection.notificationHub;


    function notify(content, userId) {
        notificationHub.server.notify(content, userId);
    }


    $(".noti").click(function (e) {
        e.stopPropagation();

        $(".noti-content").show();
        let count = 0;
        count = parseInt($(".count").html()) || 0;
        $(".count").css("background", "#222222");
        if (count > 0) {
            updateNotifications();
        }

        $(".count", this).html("&nbsp;");
    });

    $("html").click(function () {
        $(".noti-content").hide();
    });

    function updateNotifications() {
        $("#noticontent").empty();
        $("#noticontent").append(`<li>Loading...</li>`);

        $.get("/api/notifications", function (notifications) {
            console.log(notifications);
            $("#noticontent").empty();
            if (notifications.length == 0) {
                $("#noticontent").append(`<li>You don't have new notifications</li>`);
            }
            else {
                $.each(notifications, function (i, n) {
                    $("#noticontent").append(`<li>${n.content}​​<span style="color: orange; display: block;">${calculateTime(n.dateTimeSent)}​​</span></li>`);
                });
            }
        }).fail(function (e) {
            toastr.error(e.statusText);
        });
    }

    function calculateTime(dateTimeSent) {
        let now = new Date(Date.now());
        let date = new Date(dateTimeSent);

        let yearDiff = now.getUTCFullYear() - date.getUTCFullYear();
        let monthDiff = now.getUTCMonth() - date.getUTCMonth();
        let dayDiff = now.getUTCDate() - date.getUTCDate();
        let hourDiff = now.getUTCHours() - date.getUTCHours();
        let minuteDiff = now.getUTCMinutes() - date.getUTCMinutes();

        if (yearDiff > 0) return yearDiff + (yearDiff > 1 ? " years ago" : " year ago") + date.getUTCMonth();
        else if (monthDiff > 0) return monthDiff + (monthDiff > 1 ? " months ago" : " month ago");
        else if (dayDiff > 0) return dayDiff + (dayDiff > 1 ? " days ago" : " day ago");
        else if (hourDiff > 0) return hourDiff + (hourDiff > 1 ? " hours ago" : " hour ago");
        else if (minuteDiff > 0) return minuteDiff + (minuteDiff > 1 ? " minutes ago" : " minute ago");
        else return "a few moments ago";
    }

    function updateNotificationCount() {
        let count = 0;
        count = parseInt($(".count").html()) || 0;
        count++;
        $(".count").css("background", "red");
        $(".count").html(count);
    }

    notificationHub.client.notifyUser = function (notification) {
        if (notification) {
            updateNotificationCount();
        }
    }

    //HubBooter.Start();

    return {
        notify: notify
    }
}();