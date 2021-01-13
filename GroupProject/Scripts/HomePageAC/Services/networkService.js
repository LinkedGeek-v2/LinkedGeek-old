let NetworkService = function () {
    let getFollowees = function (devId, done, fail) {
        $.get("/api/users/followees/" + devId)
            .done(done)
            .fail(fail);
    }

    let getFollowers = function (devId, done, fail) {
        $.get("/api/users/followers/" + devId)
            .done(done)
            .fail(fail);
    }

    let unfollow = function (unfollowId, done, fail) {
        $.ajax({
            url: "/api/users/unfollow/" + unfollowId,
            method: "delete",
        })
            .done(done)
            .fail(fail);

    }
    let follow = function (followId, done, fail) {
        $.post("/api/users/follow/" + followId)
            .done(done)
            .fail(fail);
    }

    let savePic = function (imageBase64, done, fail) {
        $.ajax({
            type: "POST",
            url: "/api/users/savepic",
            data: JSON.stringify(imageBase64),
            contentType: "application/json",
        })
            .done(done)
            .fail(fail);
    }

    return {
        getFollowees: getFollowees,
        getFollowers: getFollowers,
        follow: follow,
        unfollow: unfollow,
        savePic: savePic
    }
}();
