(function () {
    $.validator.setDefaults({ ignore: '' });
    $(function() {
        "use strict";

        $("#ArticleForm").validate({
            ignore: []
        });

        var editor = CKEDITOR.replace('ArticleBody');
        

    });
})();