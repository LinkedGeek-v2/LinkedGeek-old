class FeedController {
    feedContainer;
    postService;
    lastPostPosition;

    constructor(feedContainer, FeedHubSubscriber, PostService) {
        this.feedContainer = feedContainer;

        this.FeedHubSubscriber = FeedHubSubscriber;
        this.postService = PostService;
        this.ScrollableLoad();

        this.lastPostPosition = 0;
        this.AppendNextPosts = this.AppendNextPosts.bind(this);


        this.AddPostToDOM = this.AddPostToDOM.bind(this);
        this.FeedHubSubscriber.InitFeedHub(this.AddPostToDOM);
        this.NoPostsDisplay();
    }

    ScrollableLoad() {
        let _this = this;//window
        let element = $(this.feedContainer);

        $(_this.feedContainer).scroll(function () {
            let diff = $(element)[0].scrollTop - ($(element)[0].scrollHeight - $(element)[0].offsetHeight);
            if (diff > -1 && diff < 1) {
                _this.AppendNextPosts();
            }
        });
    }

    AppendNextPosts() {
        let done = (data) => {
            if (data.length == 0) {
                this.lastPostPosition -= 5;
            }
            else
                this.RemoveNoPostsDisplay();

            for (var iter of data)
                this.AddPostToDOM(iter, false);
        }
        let fail = () => toastr.error("Couldn't fetch next posts");
        //use the post service to get posts and pass the DOM manipulation in the done,fail functions
        //if (devId)
        this.postService.GetPostsRange(done, fail, this.lastPostPosition, this.lastPostPosition += 5/*, devId*/);
        //else
        //    this.postService.GetPostsRange(done, fail, this.lastPostPosition, this.lastPostPosition += 5);
    }

    RefreshPosts(devId) {
        $(this.feedContainer).empty(); // empty the feed

        let done = (data) => data.forEach(this.AddPostToDOM);
        let fail = () => alert("Something went wrong");

        //use the post service to get posts and pass the DOM manipulation in the done,fail functions
        if (devId)
            this.postService.GetPosts(done, fail, devId);
        else
            this.postService.GetPosts(done, fail);
    }

    NoPostsDisplay() {
        $(this.feedContainer).append(`<h2 id="nopostsyet">No posts have been made yet</h2>`);
    }
    RemoveNoPostsDisplay() {
        $("#nopostsyet").remove();
    }


    AddPostToDOM(viewModel, isAtTop) {

        let profilePagelink = viewModel.isDeveloper ? `/OtherDeveloperProfile/OtherDeveloperProfilePage/${viewModel.userId}` : `/OtherCompanyProfilePage/OtherCompProfilePage/${viewModel.userId}`;
        let feed = $(this.feedContainer);
        //class="img-circle"
        let postDate = FeedController.TimePassed(new Date(viewModel.post.datePosted));
        let postTemplate = `
                                <div class="post">
                                    <section class="post-header">

                                        <div class="post-profilepic">
                                            <a href="${profilePagelink}">
                                                <img userId="${viewModel.userId}" class="img-circle"  src="${viewModel.userImageUrl}" />
                                            </a>
                                        </div>

                                        <div class="post-details">
                                            <div class="user-details">

                                                <a href="${profilePagelink}">
                                                    <h4 userId="${viewModel.userId}" class="user-name"><bold>${viewModel.name}</bold></h4>
                                                </a>
                                                <h6 class="user-otherdetails">`;
        if (viewModel.description != null) {
            postTemplate += `${viewModel.description}</h6>`;
        }

        postTemplate +=
            `</div>
                                            <span class="date">${postDate}</span>
                                        </div>

                                    </section>
                                    <section class="post-body">
                                        <div class="uploaded-text">
                                            `;

        if (viewModel.post.text != null) {
            postTemplate += `<p>
                                                ${viewModel.post.text}
                                            </p>`;
        }


        postTemplate +=
            `</div>
                                        <div class="uploaded-image">`;
        if (viewModel.post.imageName != null) {
            postTemplate +=
                `<img src="${viewModel.post.imageName}" />`;
        }
        postTemplate +=
            `</div>

                                    </section>
                                </div>`;
        if (isAtTop == true)
            feed.prepend(postTemplate);
        else
            feed.append(postTemplate);
    }


    static TimePassed(date) {
        let now = new Date(Date.now());

        let yearDiff = now.getUTCFullYear() - date.getUTCFullYear();
        let monthDiff = now.getUTCMonth() - date.getUTCMonth();
        let dayDiff = now.getUTCDate() - date.getUTCDate();
        let hourDiff = now.getUTCHours() - date.getUTCHours();
        let minuteDiff = now.getUTCMinutes() - date.getUTCMinutes();


        if (yearDiff > 0)
            return yearDiff + (yearDiff > 1 ? " years ago" : " year ago") + date.getUTCMonth();

        else if (monthDiff > 0)
            return monthDiff + (monthDiff > 1 ? " months ago" : " month ago");

        else if (dayDiff > 0)
            return dayDiff + (dayDiff > 1 ? " days ago" : " day ago");

        else if (hourDiff > 0)
            return hourDiff + (hourDiff > 1 ? " hours ago" : " hour ago");

        else if (minuteDiff > 0)
            return minuteDiff + (minuteDiff > 1 ? " minutes ago" : " minute ago");
        else
            return "a few moments ago";


    }

}
