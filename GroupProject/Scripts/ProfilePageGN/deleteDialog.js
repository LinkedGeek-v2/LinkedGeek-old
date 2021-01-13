let DeleteDialog = function () {

    let deleteAction = function (url, value, name, divToRemove) {
        bootbox.dialog({
            title: "Confirm Deletion",
            message: "<p>Are you sure you want to delete this " + name + " ?</p>",
            size: "sm",
            buttons: {
                no: {
                    label: "Cancel",
                    className: "btn-default",
                    callback: function () {
                        bootbox.hideAll();
                    }
                },
                yes: {
                    label: "Yes",
                    className: "btn-danger",
                    callback: function () {
                        $.ajax({
                            url: url + value,
                            method: "DELETE"
                        })
                            .done(function () {
                                divToRemove.fadeOut(function () {
                                    divToRemove.remove();
                                    toastr.success(name + " deleted successfully!");
                                });
                            })
                            .fail(function () {
                                toastr.error("Please refresh!");
                            });
                    }
                }
            }
        });
    };
        return {
            deleteAction: deleteAction
        };
}();