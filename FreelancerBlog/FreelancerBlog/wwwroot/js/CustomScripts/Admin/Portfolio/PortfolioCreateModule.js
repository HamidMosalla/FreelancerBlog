var portfolioCreateModule = (function ($) {
    'use strict';

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

    var Run = function () {
        setupCkEditorSettings();
        initCalendarDatePicker();
    };

    return {
        Run: Run
    };

})(jQuery);