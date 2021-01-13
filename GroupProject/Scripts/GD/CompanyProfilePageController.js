let CompanyController = function () {
    function deleteJob() {
        $(document).on("click", ".delete-btn", function () {
            let button = $(this);
            let buttonId = button.attr("id");
            $.ajax({
                url: `/Job/DeleteJob/${buttonId}`,
                method: "DELETE"
            })
                .done(function () {
                    toastr.success("Job Deleted!");
                    button.parent().remove();
                })
                .fail(function () {
                    toastr.error("Something went wrong!");
                });
        });
    };

    function postJob(CompanyID) {
        $(document).on("click", ".post-btn", function () {
            $(document).off("submit", "#jobsPosted-form").on("submit", "#jobsPosted-form", function (e) {
                e.preventDefault();

                let formData = {
                    "companyID": CompanyID,
                    "jobTitle": $("input[name=jobTitle]").val(),
                    "jobDescription": $("input[name=jobDescription]").val(),
                    "jobType": $("input[name=jobType]").val()
                };

                $.ajax({
                    url: "/Job/CreateJob",
                    method: "POST",
                    data: formData
                })
                    .done(function () {
                        toastr.success("Job Created!");
                        $("#jobModal").modal("hide");
                        $.get("/CompanyProfilePage/Jobs", function (data) {
                            $("#replace").html(data);
                        });
                    })
                    .fail(function () {
                        toastr.error("Something went wrong!");
                        $("#jobModal").modal("hide");
                    });
            });
        });
    };

    function manageApplicant(btnClass, message) {
        $(document).on("click", btnClass, function () {
            let button = $(this);
            NotificationService.notify(message, button.attr("id"));
            toastr.success("Success!");
            button.closest("tr").remove();
        });
    }

    return {
        deleteJob: deleteJob,
        postJob: postJob,
        manageApplicant: manageApplicant
    }
}();