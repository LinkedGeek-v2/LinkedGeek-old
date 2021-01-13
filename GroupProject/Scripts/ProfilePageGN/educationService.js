let EducationService = function () {
    let createOrEditEducation = function (educationDataForm, done, fail) {
        $.ajax({
            url: "/educations/add-edit",
            method: "Post",
            data: educationDataForm
        })
            .done(done)
            .fail(fail);
    };

    let deleteEducation = function (whoClicked) {
        DeleteDialog.deleteAction("/educations/delete/", whoClicked.val(), "Education", whoClicked);
    };

    return {
        createOrEditEducation: createOrEditEducation,
        deleteEducation: deleteEducation
    }
}();