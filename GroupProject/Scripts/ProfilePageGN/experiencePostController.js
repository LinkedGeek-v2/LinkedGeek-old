let ExperienceController = function (experienceService) {

    let form;


    let createOrEditExp = function (experienceDataForm) {
        form = experienceDataForm;
        experienceService.createEditExperience(experienceDataForm, done, fail);

    };

    let deleteExp = function (whoClicked) {
        experienceService.deleteExperience(whoClicked);
    };


    let done = function (idReturned) {
        $("#dev-profile-page-form-replace").modal('hide');
        let div = $(".expHeader");
        let div2 = $(".devExpBody");
        if (div.hasClass("hidden")) {
            div.removeClass("hidden");
        }

        if (!form.EndYear) {
            $("#currentlyWorkingJobTitle").text(form.JobTitle + " at:");
            $('#currentlyWorkingCompanyName').text(form.CompanyName);
        }




        if (form.ExperienceID == "0") {
            form.ExperienceID = idReturned;
            div2.prepend(createExperienceTemplate.showExperience(form));
            toastr.success("Experience added succesfully!");
        }
        else {
            $(".Exp-" + form.ExperienceID).html(createExperienceTemplate.showExperience(form));
            toastr.success("Experience edited succesfully!");

        };
    };

    let fail = function (dataReturned) {
        let errors = dataReturned.responseJSON.message.split(',');

        for (var i of errors) {
            if (i) {
                toastr.error(i);
            }
        }
    };


    return {
        createOrEditExp: createOrEditExp,
        deleteExp: deleteExp
    };

}(ExperienceService);
