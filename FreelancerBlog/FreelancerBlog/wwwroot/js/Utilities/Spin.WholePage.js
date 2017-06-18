"use strict";

var ajaxSpinnerForWholePage = function () {

    var element = $('<div id="spinDiv"></div>').css({
        position: "fixed",
        top: "50%",
        left: "50%",
        zIndex: "1"
    }).appendTo('body');

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

    var spinner = new Spinner(opts);

    $(document).on({

        ajaxStart: function () {
            $.blockUI({ message: null });
            //note that document.getElementById is not the same as $(selector), so, element[0] because we need to DOM element not jquery object
            spinner.spin(element[0]);
        },
        ajaxStop: function () {
            $.unblockUI();
            spinner.stop(element[0]);
        }
    });

};