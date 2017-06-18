"use strict";

var ajaxSpinnerForPartOfPage = function (selector) {

    var opts = {
        lines: 10, // The number of lines to draw
        length: 7, // The length of each line
        width: 4, // The line thickness
        radius: 10, // The radius of the inner circle
        corners: 1, // Corner roundness (0..1)
        rotate: 0, // The rotation offset
        color: '#000', // #rgb or #rrggbb
        speed: 1, // Rounds per second
        trail: 60, // Afterglow percentage
        shadow: false, // Whether to render a shadow
        hwaccel: false, // Whether to use hardware acceleration
        className: 'spinner', // The CSS class to assign to the spinner
        zIndex: 2e9, // The z-index (defaults to 2000000000)
        //top: 25, // Top position relative to parent in px
        //left: 25 // Left position relative to parent in px
    };

    var elements = $(selector);

    var spinner = [];

    for (var i = 0; i < elements.length; i++) {
        spinner[i] = new Spinner(opts);
    }

    $(document).on({

        ajaxStart: function () {
            for (var j = 0; j < elements.length; j++) {
                $(elements[j]).block({ message: null });
                spinner[j].spin(elements[j]);
            }
        },
        ajaxStop: function () {
            for (var j = 0; j < elements.length; j++) {
                $(elements[j]).unblock();
                spinner[j].stop(elements[j]);
            }
        }
    });

};