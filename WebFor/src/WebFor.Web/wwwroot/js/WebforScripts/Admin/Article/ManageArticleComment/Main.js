$(function () {

    requirejs(["../../../../utilities/pnotifymodule", "ManageArticleCommentModule"], function () {

        manageArticleCommentModule.wireUpDeleteArticleCommentButtonClickEvent();
        manageArticleCommentModule.wireUpApprovalCheckBoxChangeEvent();
        manageArticleCommentModule.wireUpArticleDetailModalButtonClickEvent();
        manageArticleCommentModule.wireUpArticleEditButtonClickEvent();

    });

});