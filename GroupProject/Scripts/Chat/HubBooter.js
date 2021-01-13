let HubBooter = function () {
    let conn = $.connection;    

    //conn.feed.client.receivePost = function (post) {
    //    post = JSON.parse(post);
    //    FeedController.AddPostToDOM(post, true);
    //};


    //let starter = function () {
    //        conn.hub.start()
    //            .done(function () {
    //                toastr.success("Connected!");
    //                console.log("Mpika");
    //            })
    //            .fail(function () {
    //                toastr.error("Chat isnt working Currently!");
    //            });
    //};
    return {
        Connection: conn,
        //Start: starter,
    }
}();







