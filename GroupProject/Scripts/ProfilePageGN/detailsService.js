let DetailsService = function () {


    // to be fixed, doesnt post on dev action
    let addDetails = function (formData, done, fail,value) {

        if (value) {
            $.ajax({
                url: "/details/dev",
                method: "Patch",
                data: formData
            })
                .done(done)
                .fail(fail);
        }
        else {
            $.ajax({
                url: "/details/comp",
                method: "Patch",
                data: formData
            })
                .done(done)
                .fail(fail);
        }
    

    };


    return {
        addDetails: addDetails
    };


}();