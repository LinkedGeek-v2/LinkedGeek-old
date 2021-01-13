let DetailsController = function (DetailsService) {

    let form;

    let saveDetails = function (formData,value) {
        form = formData;
        DetailsService.addDetails(formData, done, fail,value);
    }



    let done = function (dataReturned) {
        try {
            $("#dev-profile-page-form-replace").modal('hide');
            $('#fullName').text(form.FirstName + " " + form.LastName);
            if (dataReturned) {
                $('#devAge').text(dataReturned + " years old");
            }
            else
                $('#devAge').text("");
            toastr.success("Details have been saved!");
        }
        catch {

        }

        try {
            $("#comp-profile-page-form-replace").modal('hide');
            toastr.success("Details have been saved! Please refresh your Page!");
        }
        catch {

        }
        

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
        saveDetails: saveDetails
    };


}(DetailsService);

