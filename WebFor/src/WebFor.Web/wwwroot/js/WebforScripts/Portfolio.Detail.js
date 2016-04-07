(function () {
    $(function () {
        "use strict";

        //remove the style of image so that img-responsive class will work as expected
        $("img").addClass("img-responsive").removeAttr("style");

        //convert .net date to persian date
        $(".persianDate").each(function (index, element) { $(element).text(new Date($(element).text()).toLocaleDateString("fa-IR").replace(' ه‍.ش.', '')) });

        //wrap all images in an anchor tag
        $("img").wrap(function(index) {
            return '<a href="' + $(this).attr("src") + '" target="_blank"></a>';
        });


    });
})();