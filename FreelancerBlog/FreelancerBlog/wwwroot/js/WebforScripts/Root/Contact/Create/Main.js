$(function () {

    requirejs(["../../../../utilities/pnotifymodule", "CreateContctModule"], function () {

        createContctModule.makeYesScriptVisible();
        createContctModule.wireUpcontactFormSubmitEvent();
        createContctModule.initSpinner();
    });

});