"use strict";
var pNotifyModule = (function () {

    var successNotice = function (titleMessage="موفق", bodyMessage="عملیات مورد نظر موفقیت آمیز بود.") {
        new PNotify({
            title: titleMessage,
            text: bodyMessage,
            type: 'success',
            icon: 'glyphicon glyphicon-ok-sign',
            delay: 3000
        });
    };

    var warningNotice = function (titleMessage="مشکل عملیات", bodyMessage="مشکلی در اجرای عملیات مورد نظر پیش آمده.") {
        new PNotify({
            title: titleMessage,
            text: bodyMessage,
            type: 'warning',
            icon: 'glyphicon glyphicon-warning-sign',
            delay: 3000
        });
    };

    var failureNotice = function (titleMessage="ناموفق", bodyMessage="عملیات مورد نظر موفقیت آمیز نبود.") {
        new PNotify({
            title: titleMessage,
            text: bodyMessage,
            type: 'danger',
            icon: 'glyphicon glyphicon-warning-sign',
            delay: 3000
        });
    };

    var confirm = function (title, text, callBack) {
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