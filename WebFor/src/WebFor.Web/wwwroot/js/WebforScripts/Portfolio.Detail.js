(function () {
    $(function () {
        "use strict";

        //remove the style of image so that img-responsive class will work as expected
        $("img").addClass("img-responsive").removeAttr("style");

        //wrap all images in an anchor tag
        $("img").wrap(function(index) {
            return '<a href="' + $(this).attr("src") + '" target="_blank"></a>';
        });


    });
})();