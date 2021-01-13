class SubmitPostController {
    service;
    constructor(formContainer, errorContainer, user, imageSelectionController, textAreaController, feedController) {

        this.service = feedController.postService;
        this.textArea = textAreaController.GetTextArea();
        this.image = imageSelectionController.GetImageUploaded();
        this.formContainer = formContainer;
        this.errorContainer = errorContainer;
        this.user = user;
        this.imageSelectionController = imageSelectionController;
        this.textAreaController = textAreaController;
        this.feedController = feedController;

        this.done = this.done.bind(this);
        this.fail = this.fail.bind(this);
        this.SetSubmitPostHandler();
    }


    ConstructPostViewModel(post) {
        //Constructs a HomePageViewModel type object
        let postViewModel = {
            userId: this.user.Id,
            userImageUrl: this.user.UserImageUrl,
            name: this.user.Name,
            description: this.user.Description,
            post: {
                datePosted: post.datePosted,
                text :post.text,
                //imageName:  post.postImageBase64
                imageName: post.imageName
            }
        }

        return postViewModel;
    }



    done(post) {
        //After a post is submitted to the back end
        //   1. wrap it in a ViewModel
        //   2. send it via Signal R to other homepages
        //   3. add it to my homepage
        post = this.ConstructPostViewModel(post);


        //this.feedController.signalR.feed.server.send(post);

        this.feedController.FeedHubSubscriber.HubConnection.Connection.feed.server.send(post);

        this.feedController.AddPostToDOM(post, true);
        this.imageSelectionController.ClearImageUploaded();
        this.textAreaController.ClearTextArea();

        $(this.errorContainer).css("visibility", "hidden");

    }
    fail(response) {
        //After a post failed to submit in the back end, display the appropriate
        //model errors that were received from the back end
        console.log("FAILED");
        let errors = response.responseJSON.modelState;
        delete errors["$id"];

        let errorMsg = "";
        for (var i in errors)
            errorMsg += `${errors[i]}\n`

        this.displayErrorMessages(errorMsg);
    }

    validateInput() {
        //Front end validation - before sending a post to the back end
        let text = $(this.textArea).val();
        let imagebase64 = $(this.image).attr("src");
        const emptytext = "What do you want to say?";
        if (text != emptytext || imagebase64)
            return true;
        else
            return false;
    }

    displayErrorMessages(errorMsg) {
        //Displays the error message on the error container
        $(this.errorContainer).text(errorMsg);
        $(this.errorContainer).css("visibility", "visible");
    }

    SetSubmitPostHandler() {


        $(this.formContainer).submit((event) => {
            event.preventDefault();
            this.feedController.RemoveNoPostsDisplay();
            
            //collect the image and the text from their respective containers
            let text = $(this.textArea).val();
            text = text != "What do you want to say?" ? text : undefined;
            let imagebase64 = $(this.image).attr("src");

            //front end validation and send to backend
            if (this.validateInput()) {
                let post = { ImageBase64: imagebase64, Text: text };

                this.service.CreatePost(post, this.done, this.fail);
            }
            else
                this.displayErrorMessages("Please enter some text or add a picture to create a post.")
        });
    }





}
