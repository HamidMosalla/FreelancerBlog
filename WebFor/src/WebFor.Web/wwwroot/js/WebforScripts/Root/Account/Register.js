(function () {
    $(function () {
        "use strict";

        // extend range validator method to treat checkboxes differently
        var defaultRangeValidator = $.validator.methods.range;

        $.validator.methods.range = function(value, element, param) {
            if (element.type === 'checkbox') {
                // if it's a checkbox return true if it is checked
                return element.checked;
            } else {
                // otherwise run the default validation function
                return defaultRangeValidator();
            }
        };

    });
})();