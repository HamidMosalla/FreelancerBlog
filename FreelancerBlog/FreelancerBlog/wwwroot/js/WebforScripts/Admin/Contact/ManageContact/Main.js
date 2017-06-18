$(function () {

    requirejs(["../../../../utilities/pnotifymodule", "ManageContactModule"], function () {

        manageContactModule.wireUpDeleteContactButtonClickEvent();
        manageContactModule.wireUpContactDetailButtonClickEvent();
        manageContactModule.initSpinner();
    });

});