let skillAddTemplate = function () {

    let showSkill = function (skill) {
        let template = `
    <div class="Skill-${skill.SkillID}">
        <div class="devSkill">
            <div class="dev-parent" id='Skill-${skill.SkillID}'>

                <div class="dev-child"> <span>${skill.SkillName}</span></div>

                <div class="dev-buttons">
                    <button class="fa fa-trash delete-btn delSkill" value="${skill.SkillID}"></button>
                </div>
                            </div>
        </div>
        <hr class="rowHr" />
    </div>`;
        return template;
    };




    return {
        showSkill: showSkill
    };


}();
