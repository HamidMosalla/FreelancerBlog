"use strict";

var startAjaxSpinner = function (selector, opts = { lines: 10, length: 7, width: 4, radius: 10, corners: 1, rotate: 0, color: '#000', speed: 1, trail: 60, shadow: false, hwaccel: false, className: 'spinner', zIndex: 2e9 }) {

    var element = $(selector);
    var spinner = new Spinner(opts);

    element.block({ message: null });
    spinner.spin(element[0]);

    return { Spinner: spinner, Element: element[0] };
};

var stopAjaxSpinner = function (spinnerElement) {

    spinnerElement.Spinner.stop(spinnerElement.Element);
    $(spinnerElement.Element).unblock();

};