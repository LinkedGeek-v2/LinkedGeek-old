let createEducationTemplate = function () {

    let showEducation = function (education) {
        let template = `<div class="Edu-${education.EducationID}">
<div class="devEducation">
            <div class="dev-parent" id='Edu-${education.EducationID}'>
<div class="dev-child">
                <label>School:</label>
                <span>${education.School}</span>
                <label>Degree:</label>
                <span>${education.Degree}</span>
                <br />
                <label>Field:</label>
                <span>${education.Field}</span> ` +
            Grade(education) + `
                <br />
                <label>Started In:</label>
                <span>${moment(new Date(education.StartYear)).format("DD-MMM-yyyy")}</span>
                <br />
                <label>Until:</label>
                <span>${education.EndYear != "" ? moment(new Date(education.EndYear)).format("DD-MMM-yyyy") : "Present"}</span>
</div>
<div class = "dev-buttons">` +
            AjaxButton("DeveloperProfile","Education", jQuery.param(education)) + `
                <button class="fa fa-trash delete-btn delEdu" value="${education.EducationID}"></button>

            </div>
</div>
        </div>
                 <hr class="rowHr" />
</div>`;
        return template;
    };

   
    function Grade(education) {
        if (education.Grade) {
            return `<label>Grade:</label>
              <span>${education.Grade}</span>`;
        }
        else return ``;

    }

    return {
        showEducation: showEducation
    };



}();
