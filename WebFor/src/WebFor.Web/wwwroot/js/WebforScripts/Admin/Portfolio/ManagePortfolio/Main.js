$(function () {

    requirejs(["../../../../utilities/pnotifymodule", "ManagePortfolioModule"], function () {

        managePortfolioModule.wireUpDeletePortfolioButtonClickEvent();
       
    });

});