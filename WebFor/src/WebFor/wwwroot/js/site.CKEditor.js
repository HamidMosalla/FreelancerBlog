(function() {
    $(function() {
        "use strict";

        var editor = CKEDITOR.replace('ArticleBody');

        if ($('.text-editor')) {
            $('.text-editor').ckeditor();
            $('input[type=submit]').bind('click', function () {
                $('.text-editor').ckeditorGet().updateElement();
            });
        }









    });
})();