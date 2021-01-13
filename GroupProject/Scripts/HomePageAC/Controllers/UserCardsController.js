class UserCardsController {
    constructor(Container, FeedController, UserService) {
        console.log("in the constructor");
        this.Container = Container;
        this.UserService = UserService;
        this.FeedController = FeedController;
        this.Bootstart();
    }

    Bootstart() {
        console.log("booting user cards");
        this.LoadMiniNetwork()
            .done(this.AddMiniNetworkHandlers);
    }


    LoadMiniNetwork() {
        
        return $.get("/api/users/followees", function (data) {
            data.forEach(addUserToDOM);
        });
    }
    AddMiniNetworkHandlers(data) {
        console.log("in here");
        data.forEach(addHandler);
    }



}