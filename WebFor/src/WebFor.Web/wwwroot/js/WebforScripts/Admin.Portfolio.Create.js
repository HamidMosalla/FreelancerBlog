(function () {
    $.validator.setDefaults({ ignore: '' });
    $(function() {
        "use strict";

        $("#PortfolioForm").validate({
            ignore: []
        });

        var editor = CKEDITOR.replace('PortfolioBody', { customConfig: '../../lib/ckeditor/config.portfolio.js' });
        
        $('#PortfolioDateBuilt').pikaday({ firstDay: 1, format: 'MM/DD/YYYY' });

    });
})();