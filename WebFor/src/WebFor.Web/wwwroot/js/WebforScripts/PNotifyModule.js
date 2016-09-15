"use strict";
var pNotifyModule = (function () {

    var successNotice = function (titleMessage, bodyMessage) {
        new PNotify({
            title: titleMessage,
            text: bodyMessage,
            type: 'success',
            icon: 'glyphicon glyphicon-ok-sign',
            delay: 3000
        });
    };

    var warningNotice = function (titleMessage, bodyMessage) {
        new PNotify({
            title: titleMessage,
            text: bodyMessage,
            type: 'warning',
            icon: 'glyphicon glyphicon-warning-sign',
            delay: 3000
        });
    };

    var failureNotice = function (titleMessage, bodyMessage) {
        new PNotify({
            title: titleMessage,
            text: bodyMessage,
            type: 'danger',
            icon: 'glyphicon glyphicon-warning-sign',
            delay: 3000
        });
    };

    var confirm = function (callBack, title, text) {
        (new PNotify({
            title: title,
            text: text,
            icon: 'glyphicon glyphicon-question-sign',
            hide: false,
            confirm: {
                confirm: true
            },
            buttons: {
                closer: false,
                sticker: false
            },
            history: {
                history: false
            }
        })).get()
            .on('pnotify.confirm', callBack);
    };

    return {
        successNotice: successNotice,
        warningNotice: warningNotice,
        failureNotice: failureNotice,
        confirm: confirm
    };

})();