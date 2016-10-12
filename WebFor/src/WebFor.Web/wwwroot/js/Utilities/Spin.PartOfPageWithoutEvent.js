"use strict";

var startAjaxSpinner = function (selector, spinnerColor) {

    var opts = {
        lines: 17 // The number of lines to draw
        , length: 12 // The length of each line
        , width: 21 // The line thickness
        , radius: 6 // The radius of the inner circle
        , scale: 1 // Scales overall size of the spinner
        , corners: 1 // Corner roundness (0..1)
        , color: spinnerColor // #rgb or #rrggbb or array of colors
        , opacity: 0.45 // Opacity of the lines
        , rotate: 0 // The rotation offset
        , direction: 1 // 1: clockwise, -1: counterclockwise
        , speed: 0.9 // Rounds per second
        , trail: 66 // Afterglow percentage
        , fps: 20 // Frames per second when using setTimeout() as a fallback for CSS
        , zIndex: 2e9 // The z-index (defaults to 2000000000)
        , className: 'spinner' // The CSS class to assign to the spinner
        , top: '50%' // Top position relative to parent
        , left: '50%' // Left position relative to parent
        , shadow: false // Whether to render a shadow
        , hwaccel: false // Whether to use hardware acceleration
        , position: 'absolute' // Element positioning
    };

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