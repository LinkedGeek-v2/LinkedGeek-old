class NetworkServiceSingleton {

    //Singleton:
    //  _instance  - which holds the actual networkService class - is static and can only be
    //instantiated once.
    //   networkService is an anonymous class stored in NetworkServiceSingleton's private field #networkService
    //making it accessible only by NetworkServiceSingleton
    constructor() {
        if (NetworkServiceSingleton.#instance == null) {
            NetworkServiceSingleton.#instance = new NetworkServiceSingleton.#networkService;
            console.log("singleton inside instance doesnt exist--> construct");
        }
        return NetworkServiceSingleton._instance;
    }

    static #instance;

    static #networkService = class {
        getFollowees(devId, done, fail) {
            $.get("/api/users/followees/" + devId)
                .done(done)
                .fail(fail);
        }

        getFollowers(devId, done, fail) {
            $.get("/api/users/followers/" + devId)
                .done(done)
                .fail(fail);
        }

        unfollow(unfollowId, done, fail) {
            $.ajax({
                url: "/api/users/unfollow/" + unfollowId,
                method: "delete",
            })
                .done(done)
                .fail(fail);

        }
        follow(followId, done, fail) {
            $.post("/api/users/follow/" + followId)
                .done(done)
                .fail(fail);
        }


    }

}
