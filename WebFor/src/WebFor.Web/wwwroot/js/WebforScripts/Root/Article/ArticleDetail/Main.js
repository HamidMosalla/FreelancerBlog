$(function () {

    requirejs(["../../../../utilities/pnotifymodule", "ArticleDetailModule"], function () {

        articleDetailModule.setUpratingComponent();
        articleDetailModule.wireUprateComponentClickEvent();
        articleDetailModule.wireUpcommentFormSubmitButtonEvent();
        articleDetailModule.wireUpreplyButtonClickEvent();
        articleDetailModule.wireUpcencelReplyButtonClickEvent();
        articleDetailModule.setupCommentCharacterCounter();
        articleDetailModule.setUpSpinner();
    });

});