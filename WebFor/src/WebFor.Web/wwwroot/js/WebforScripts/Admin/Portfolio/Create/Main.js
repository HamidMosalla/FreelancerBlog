$(function () {

    requirejs(["PortfolioCreateModule"], function () {

        portfolioCreateModule.setupCkEditorSettings();
        portfolioCreateModule.initCalendarDatePicker();

    });

});