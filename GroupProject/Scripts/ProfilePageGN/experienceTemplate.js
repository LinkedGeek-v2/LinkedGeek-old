let createExperienceTemplate = function () {

    let showExperience = function (experience) {
        let template = `<div class="Exp-${experience.ExperienceID}">
<div class="devExperience">
            <div class="dev-parent" id='Exp-${experience.ExperienceID}'>
<div class="dev-child">
                <label>JobTitle:</label>
                <span>${experience.JobTitle}</span>
                <label>Company:</label>` +
            CreateCompanyNameLink(experience) + `
                <br />
                <label>Started In:</label>
                <span>${moment(new Date(experience.StartYear)).format("DD-MMM-yyyy")}</span>
                <br />
                <label>Until:</label>
                <span>${experience.EndYear != "" ? moment(new Date(experience.EndYear)).format("DD-MMM-yyyy") : "Present"}</span> ` +
            GetExperienceType(experience) + `

</div>
<div class="dev-buttons">` +
            AjaxButton("DeveloperProfile", "Experience", jQuery.param(experience)) + `
                <button class="fa fa-trash delete-btn delExp" value="${experience.ExperienceID}"></button>

            </div>
        </div>
</div>
 <hr class="rowHr" />
</div>`;
        return template;
    };


    function GetExperienceType(experience) {
        let expType = $("#ExperienceForm option[value='" + experience.ExperienceType + "']").text();
        return `<label>Job Type:</label>
                <span>${expType}</span>`;
    }


    function CreateCompanyNameLink(experience) {
        if (experience.CompanyWorkingID) {
            return `<a href="/OtherCompanyProfilePage/OtherCompProfilePage/${experience.CompanyWorkingID}">${experience.CompanyName}</a>`;
        }
        else return `<span>${experience.CompanyName}</span>`;
    }

    return {
        showExperience: showExperience
    };


}();
