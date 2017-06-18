$(function () {

    requirejs(["../../../../utilities/pnotifymodule", "IndexModule"], function () {

        indexModule.initSpinner();
        indexModule.initSmoothScroll();
        indexModule.wireUpnextStepButtonClickEvent();
        indexModule.wireUpexpandCollapseEvent();
        indexModule.wireUpsubmitFormEvent();
    });

});