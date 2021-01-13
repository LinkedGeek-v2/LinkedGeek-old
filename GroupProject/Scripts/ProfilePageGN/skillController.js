let SkillController = function () {

    let form;

    let addSkill = function (formData) {
        form = formData;
        $.ajax({
            url: "/developerSkills/add/" + form.SkillID,
            method: "POST",

        }).done(done).fail(fail);
    }

    let done = function () {
        let skillName = $("#SkillForm option[value=" + form.SkillID + "]").text();

        $(".devSkillBody").prepend(skillAddTemplate.showSkill({ SkillID: form.SkillID, SkillName: skillName }));
        $("#dev-profile-page-form-replace").modal("hide");
        toastr.success("Skill was added succesfully!");

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
        addSkill: addSkill
    };






}();
