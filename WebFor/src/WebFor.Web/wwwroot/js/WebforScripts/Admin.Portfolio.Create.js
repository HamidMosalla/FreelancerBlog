(function () {
    $.validator.setDefaults({ ignore: '' });
    $(function() {
        "use strict";

        $("#PortfolioForm").validate({
            ignore: []
        });

        var editor = CKEDITOR.replace('PortfolioBody');
        
        $('#PortfolioDateBuilt').pikaday({ firstDay: 1, format: 'MM/DD/YYYY' });

    });
})();