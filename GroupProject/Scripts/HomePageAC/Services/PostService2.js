
//Post Service is an abstract class. Only classes extening it
//can be initiated if and only if they implement its methods.
class PostService {
    constructor() {
        if (this.constructor === PostService) 
            throw new TypeError('Abstract class "Widget" cannot be instantiated directly.');

        if (this.CreatePost == undefined) 
            throw new TypeError('Classes extending the widget abstract class need to Implement createPost');

        if (this.GetPosts == undefined) 
            throw new TypeError('Classes extending the widget abstract class need to Implement getPosts');

        if (this.GetPostsRange == undefined) 
            throw new TypeError('Classes extending the widget abstract class need to Implement getPostsRange');
    }
}

class HomePagePostService extends PostService{

    constructor() {
        super();
    }
    CreatePost(post, done, fail) {
        $.post("/api/posts/createpost", post)

            .done((data) => {
                done(data);
            })

            .fail(fail);
    }

    GetPosts(done, fail,devId) {
        $.get(`/api/posts/getuserfolloweesposts/${devId}`) // same query in two funcs
            .done(done)
            .fail(fail);
    }


    //Returns the Post View Models given the specified range

    GetPostsRange(done, fail, startIndex, endIndex) {
        $.get(`/api/posts/getuserfolloweesposts/${startIndex}/${endIndex}`)
            .done(done)
            .fail(fail);
    }

}

class OtherHomePagePostService extends PostService {
    constructor(UserId) {
        super();
        this.userId = UserId;

    }
    CreatePost(post, done, fail) {
        $.post("/api/posts/createpost", post)

            .done((data) => {
                done(data);
            })

            .fail(fail);
    }

    GetPosts(done, fail) {
        $.get(`/api/posts/getuserposts/${this.userId}`) // same query in two funcs
            .done(done)
            .fail(fail);
    }


    //Returns the Post View Models given the specified range

    GetPostsRange(done, fail, startIndex, endIndex) {
        console.log("inside post service, id:" + this.userId);

        $.get(`/api/posts/getuserposts/${startIndex}/${endIndex}/${this.userId}`)
            .done(done)
            .fail(fail);
    }

    //get the posts of my followees and myself

   
}