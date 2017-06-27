"use strict";

var portfolioCreateModule = (function () {

    function setupCkEditorSettings() {

        $.validator.setDefaults({ ignore: '' });

        $("#PortfolioForm").validate({
            ignore: []
        });

        var editor = CKEDITOR.replace('PortfolioBody', { customConfig: '../../lib/ckeditor/config.portfolio.js' });

    }

    function initCalendarDatePicker() {

        $('#PortfolioDateBuilt').pikaday({ firstDay: 1, format: 'MM/DD/YYYY' });

    }

    return {
        setupCkEditorSettings: setupCkEditorSettings,
        initCalendarDatePicker: initCalendarDatePicker
    };

})();