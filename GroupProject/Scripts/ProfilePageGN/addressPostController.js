let AddressController = function (addressService) {

    let form;

    let createOrEditAddress = function (formData) {
        form = formData;
        addressService.createOrEditAddress(formData, done, fail);
    }



    let done = function (dataReturned) {     
        try {
            $("#dev-profile-page-form-replace").modal('hide');
        }
        catch {

        }

        try {
            $("#comp-profile-page-form-replace").modal('hide');
        }
        catch {

        }
  

        let chosenCity = $("#city").val();
        let chosenCountry = $('#country').val();
        $('#adrCity').text(chosenCity +",");
        $('#adrCountry').text(chosenCountry);
        toastr.success("Address has been changed!");

    };

    let fail = function (dataReturned) {
        let errors = dataReturned.responseJSON.message.split(',');

        for (var i of errors) {
            if (i) {
                toastr.error(i);
            }
        }
    }


    return {
        createOrEditAddress: createOrEditAddress
    };


}(AddressService);