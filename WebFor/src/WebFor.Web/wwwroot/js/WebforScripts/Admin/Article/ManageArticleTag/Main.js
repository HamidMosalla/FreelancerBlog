$(function () {

    requirejs(["../../../../utilities/pnotifymodule", "ManageArticleTagModule"], function () {

        manageArticleTagModule.wireUpDeleteTagButtonClickEvent();
        manageArticleTagModule.editTagButtonClickEventHandler();
        manageArticleTagModule.initSpinner();
    });

});