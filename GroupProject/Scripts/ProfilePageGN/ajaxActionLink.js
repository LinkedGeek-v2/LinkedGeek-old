let AjaxButton = function (controller,name, url) {

    let completeUrlSource = "/" + controller + "/" + name + "Form?" + url;
    return `<a class="fa fa-cog" data-ajax="true" data-ajax-method="Get"
        data-ajax-mode="replace" data-ajax-update="#dev-profile-page-form-replace"
        data-target="#dev-profile-page-form-replace" data-toggle="modal"
        href=${completeUrlSource}></a>`;

};
