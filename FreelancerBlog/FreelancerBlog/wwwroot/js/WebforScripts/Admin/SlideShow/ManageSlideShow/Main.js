$(function () {

    requirejs(["../../../../utilities/pnotifymodule", "ManageSlideShowModule"], function () {

        manageSlideShowModule.wireUpDeleteSlideShowButtonClickEvent();
        manageSlideShowModule.wireUpSlideshowDetailButtonClickEvent();
    });

});