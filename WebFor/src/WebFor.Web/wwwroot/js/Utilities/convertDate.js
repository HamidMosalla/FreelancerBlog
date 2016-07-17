(function () {
    $(function () {
        "use strict";

        //convert .net date to short persian date
        $(".persianDate").each(function (index, element) { $(element).text(new Date($(element).text()).toLocaleDateString("fa-IR").replace(' ه‍.ش.', '')) });
        
    });
})();