$(function () {

    requirejs(["../articlesharedmodule"], function () {

        articleSharedModule.setupTypeAheadForTafInput();
        articleSharedModule.setupCkEditorSettings();

    });

});