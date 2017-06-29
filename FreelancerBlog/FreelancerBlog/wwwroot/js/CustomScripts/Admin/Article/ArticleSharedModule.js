define("ArticleSharedModule", function () {
    "use strict";

    function setupTypeAheadForTafInput() {
        $('#ArticleTags').tagsinput({
            typeahead: {
                minLength: 1,
                showHintOnFocus: true,
                autoSelect: true,
                afterSelect: function (val) { this.$element.val(""); },
                source: function (query) {
                    return $.get('/Admin/Article/TagLookup');
                }
            }
        });
    }

    function setupCkEditorSettings() {
        $.validator.setDefaults({ ignore: '' });

        $("#ArticleForm").validate({
            ignore: []
        });

        var editor = CKEDITOR.replace('ArticleBody');
    }

    var Run = function () {
        setupTypeAheadForTafInput();
        setupCkEditorSettings();
    };

    return {
        Run: Run
    };

});