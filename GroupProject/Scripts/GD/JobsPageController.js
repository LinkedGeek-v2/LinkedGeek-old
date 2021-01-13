

let JobsController = function () {
    //Return this function
    function jobFormSubmit(table) {
        $("#jobForm").submit(function (e) {
            e.preventDefault();

            jobSubmit(table);
        });
    }

    function postFormData(formData, table) {
        $.post("/api/jobs/search", formData, function (data) {
            if (typeof table.attr("hidden") !== typeof undefined && table.attr("hidden") !== false) {
                table.removeAttr("hidden");
            }

            if (data.length != 0) {
                drawTable(data);
                setApplyBtn();
            }
            else {
                toastr.error("No jobs found.")
                table.attr("hidden", true);
            }
        });
    };

    function drawTable(data) {
        $.each(data, function (key, entry) {
            let title = $(`<td>${entry.jobTitle}</td>`);
            let description = $(`<td>${entry.jobDescription}</td>`);
            let date = $(`<td>${entry.datePosted}</td>`);
            let type = $(`<td>${entry.jobType}</td>`);
            let company = $(`<td id="${entry.company.companyID}"><a href="/OtherCompanyProfilePage/OtherCompProfilePage?id=${entry.company.companyID}">${entry.company.companyName}</a></td>`)
            let button = $(`<td><button id="${entry.jobID}" type="submit" class="btn btn-sm btn-primary apply-btn">Apply</button></td>`);
            let row = $("<tr>").append(title, description, date, type, company, button);
            $("#job-table-data").append(row);
        });
    };

    function setApplyBtn() {
        $(".apply-btn").on("click", function (e) {
            e.preventDefault();

            let button = $(this);
            let companyId = button.closest("tr").find("td").eq(-2).attr("id"); //get the id of this row's company
            $.ajax({
                url: "/api/jobs/" + button.attr("id"),
                method: "POST"
            })
                .done(function () {
                    button
                        .removeClass("btn-primary")
                        .addClass("btn-success")
                        .text("Applied")
                        .attr("disabled", true);

                    NotificationService.notify("We have a new applicant!", companyId);

                    toastr.success("Your application is accepted!");
                })
                .fail(function () {
                    toastr.error("Something went wrong!");
                });
        });
    }

    function jobSubmit(table) {
        let formData = {
            "jobTitle": $("input[name=jobTitle]").val().trim(),
            "cityName": $("input[name=city]").val()
        };
        $("#job-table-data").children().remove();
        table.attr("hidden", true);

        if (formData.jobTitle || formData.cityName) {
            postFormData(formData, table);
        }
        else {
            toastr.error("Please fill at least one field");
        }
    }

    return {jobFormSubmit: jobFormSubmit}
}();