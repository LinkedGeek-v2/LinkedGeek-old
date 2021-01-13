let EducationController = function (educationService) {

    let formData;

    let createOrEditEducation = function (educationDataForm) {
        formData = educationDataForm;
        educationService.createOrEditEducation(educationDataForm, done, fail)
    };

    let deleteEducation = function (whoClicked) {
        educationService.deleteEducation(whoClicked);
    };

    let done = function (idReturned) {
        $("#dev-profile-page-form-replace").modal('hide');
        let div = $(".eduHeader");
        let div2 = $(".devEduBody");
        if (div.hasClass("hidden")) {
            div.removeClass("hidden");
        }

        if (formData.EducationID == "0") {
            formData.EducationID = idReturned;
            div2.prepend(createEducationTemplate.showEducation(formData));
            toastr.success("Education added succesfully!");
        }
        else {
            $(".Edu-" + formData.EducationID).html(createEducationTemplate.showEducation(formData));
            toastr.success("Education edited succesfully!");

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
        createOrEditEducation: createOrEditEducation,
        deleteEducation: deleteEducation
    }
}(EducationService);
