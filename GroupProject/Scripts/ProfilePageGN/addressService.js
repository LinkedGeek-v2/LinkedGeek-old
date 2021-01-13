let AddressService = function () {

    let createOrEditAddress = function (formData,done,fail) {

        $.ajax({
            url: "/address/add-edit",
            method: "POST",
            data: formData
        })
            .done(done)
            .fail(fail);

    };


    return {
        createOrEditAddress: createOrEditAddress
    };


}();