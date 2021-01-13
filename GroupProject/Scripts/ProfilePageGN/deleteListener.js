let deleteListener = function () {

    let Delete = function (idName, name, url) {
        let url2 = "/" + url + "/delete/";
        $(document).on("click", (".del" + idName), function () {
            let divToRemove = $("." + idName + "-" + $(this).val());
            DeleteDialog.deleteAction(url2, $(this).val(), name, divToRemove);

        });
    }

    return {
        Delete: Delete
    };
}();
