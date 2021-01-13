let ExperienceService = function () {

    let createEditExperience = function (experienceDataForm , done, fail) {
        $.ajax({
            url: "/experiences/add-edit",
            method: "POST",
            data: experienceDataForm
        })
            .done(done)
            .fail(fail);    
    };

    let deleteExperience = function (whoClicked) {
        DeleteDialog.deleteAction("/experiences/delete/", whoClicked.val(), "Experience", whoClicked);      
    };

    return {
        createEditExperience: createEditExperience,
        deleteExperience: deleteExperience
    };

}();