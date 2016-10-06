$(function () {

    requirejs(["../../../../utilities/pnotifymodule", "ManageArticleModule"], function () {

        manageArticleModule.setupClickEventForDeleteArticleButton();
    });

});