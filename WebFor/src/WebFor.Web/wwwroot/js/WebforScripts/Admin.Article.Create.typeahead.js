(function() {
    $(function() {
        "use strict";

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


    });
})();