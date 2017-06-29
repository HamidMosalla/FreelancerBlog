var createContctModule = (function ($) {
    'use strict';

    function makeYesScriptVisible() {
        $("#YesScript").show();
    }

    function contactSubmitHandler(e) {
        e.preventDefault();

        //var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

        var $form = $(this);

        var formParametersArray = $form.serializeArray();

        formParametersArray.push({ name: 'isJavascriptEnabled', value: true });

        if ($form.valid()) {

            $.ajax({
                type: "POST",
                url: "/Contact/Create",
                data: formParametersArray,//$form.serialize().concat("&isJavascriptEnabled=true"),
                dataType: "json",
                success: function (response) {

                    //console.log(response);

                    if (response.status === "Success") {

                        pNotifyModule.successNotice("ثبت موفق", "نظرات شما به موفقیت ثبت شد.");

                        setTimeout(function () { window.location.replace("/Home/Index"); }, 3000);
                    }

                    if (response.status === "FailedTheCaptchaValidation") {
                        pNotifyModule.failureNotice("عملیات ناموفق", "لطفا قسمت کپچا را تکمیل نماییدد.");
                    }

                    if (response.status === "ProblematicSubmit") {
                        pNotifyModule.warningNotice("ثبت ناموفق", "مشکلی در ثبت پیش آمده، لطفا بعد از چند دقیقه دوباره تلاش کنید.");
                    }

                },
                error: function (xhr, status, error) {
                    console.log(xhr.responseText);
                    alert("message : \n" + "An error occurred, for more info check the js console" + "\n status : \n" + status + " \n error : \n" + error);
                }
            });


        }

    }

    function initSpinner() {
        ajaxSpinnerForPartOfPage("#contact-page");
    }

    var Run = function () {
        makeYesScriptVisible();
        $("#ContactForm").on("submit", contactSubmitHandler);
        initSpinner();
    };

    return {
        Run: Run
    };

})(jQuery);