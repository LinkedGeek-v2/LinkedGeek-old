class FeedHubSubscriber {
    constructor() {
        if (this.constructor === PostService)
            throw new TypeError('Abstract class "FeedHubSubscriber" cannot be instantiated directly.');

        if (this.InitFeedHub == undefined)
            throw new TypeError('Classes extending the FeedHubSubscriber abstract class need to Implement InitFeedHub');
    }
}

class HomePageSubscriber extends FeedHubSubscriber {
    constructor(HubConnection) {
        super();
        this.HubConnection = HubConnection;
    }


    InitFeedHub(DomManipulationMethod) {
        setTimeout(()=> {
            this.HubConnection.Connection.hub.stop();
            console.log(this.HubConnection.Connection);

            this.HubConnection.Connection.feed.client.receivePost = function (post) {
                post = JSON.parse(post);
                DomManipulationMethod(post, true);
            };
            this.HubConnection.Connection.hub.start()
                .done(() => {
                    console.log("feedhub started");
                })
                .fail(function () {
                    console.log("feedhub didnt start");
                });

        }, 2000); 



        
    }
}


class OtherHomePageSubscriber extends FeedHubSubscriber {
    constructor(HubConnection, visiteeId) {
        super();
        this.HubConnection = HubConnection;
        this.visiteeId = visiteeId;
    }

    InitFeedHub(DomManipulationMethod) {

        //stop the hub server and add a client handler so the client gets registered
        //this.HubConnection.hub.stop();
        setTimeout(() => {
            this.HubConnection.Connection.hub.stop();
            this.HubConnection.Connection.feed.client.receivePost = function (post) {
                post = JSON.parse(post);
                DomManipulationMethod(post, true);
            };
            this.HubConnection.Connection.hub.start()
                .done(() => {
                    console.log("feedhub started");
                    this.HubConnection.Connection.feed.server.addToVisitorMap(this.visiteeId);
                })
                .fail(function () {
                    console.log("feedhub ended");
                });

        }, 2000); 
        


        //start the hub 
        


    }

}