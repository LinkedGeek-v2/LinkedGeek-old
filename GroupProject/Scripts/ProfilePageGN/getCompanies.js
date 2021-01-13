(function () {

        $("#company").on("change", function () {

            let chosenCompanyID = $("#companies option[value='" + this.value + "']").attr("data-company-id");
            if (chosenCompanyID)
                $("input[name=CompanyWorkingID]").val(chosenCompanyID);
            else {
                $("input[name=CompanyWorkingID]").val("");
            }
        });   

})();