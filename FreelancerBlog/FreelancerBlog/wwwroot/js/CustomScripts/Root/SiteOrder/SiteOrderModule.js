var siteOrderModule = (function ($) {
    'use strict';

    function initSpinner() {
        ajaxSpinnerForPartOfPage("#show_form");
    }

    function wizardChangeStepHandler(e) {

        var thisLiInd = $(this).parent().parent("li").index();

        if ($('.payment-wizard li').hasClass("jump-here")) {
            $(this).parent().parent("li").removeClass("active").addClass("completed");
            $(this).parent(".wizard-content").slideUp();
            $('.payment-wizard li.jump-here').removeClass("jump-here");
        }
        else {
            $(this).parent().parent("li").removeClass("active").addClass("completed");
            $(this).parent(".wizard-content").slideUp();
            $(this).parent().parent("li").next("li:not('.completed')").addClass('active').children('.wizard-content').slideDown();
        }
    }

    function expandCollapseWizardHandler() {

        if ($(this).parent().hasClass('completed')) {

            var thisLiInd = $(this).parent("li").index();

            var liInd = $('.payment-wizard li.active').index();

            if (thisLiInd < liInd) {
                $('.payment-wizard li.active').addClass("jump-here");
            }
            $(this).parent().addClass('active').removeClass('completed');

            $(this).siblings('.wizard-content').slideDown();
        }
    }

    function calculatePriceHandler(e) {
        e.preventDefault();

        var trueCheckBoxes = $(".slideThree input[type='checkbox']:checked");

        $.each(trueCheckBoxes, function (index, value) {
            $(value).val(true);

        });

        var $form = $(this);

        var formParametersArray = $form.serializeArray();

        if ($form.valid()) {

            $.ajax({
                type: "POST",
                url: "/SiteOrder/Index",
                data: formParametersArray,
                dataType: "json",
                success: function (response) {

                    if (response.status === "FailedTheCaptchaValidation") {

                        pNotifyModule.failureNotice("عملیات ناموفق", "لطفا قسمت کپچا را تکمیل نمایید.");
                        return;
                    }

                    $("#PriceSheetTable").empty();

                    var tableBodyString;

                    $.each(response.priceSheet, function (index, value) {
                        tableBodyString += '<tr><td>' + value.faName + '</td><td>' + (value.value === true ? "بلی" : value.value) + '</td><td>' + Number(value.price).toLocaleString("en") + ' تومان</td></tr>';
                    });

                    $("#PriceSheetTable").append(tableBodyString);

                    $("#FinalPriceModalSpan").text(Number(response.price).toLocaleString("en") + " تومان");

                    response.status === "Success" ? $("#successAlert").show() : $("#faileAlert").show();

                    $("#FinalPriceModal").modal("show");

                },
                error: function (xhr, status, error) {
                    console.log(xhr.responseText);
                    alert("message : \n" + "An error occurred, for more info check the js console" + "\n status : \n" + status + " \n error : \n" + error);
                }
            });
        }
    }

    var Run = function () {
        initSpinner();
        $('.smooth-scroll').click(function () { $('html, body').animate({ scrollTop: $($.attr(this, 'href')).offset().top }, 500); return false; });
        $(".done").on("click", wizardChangeStepHandler);
        $('.payment-wizard li .wizard-heading').on("click", expandCollapseWizardHandler);
        $("#calculateFinalPriceForm").on("submit", calculatePriceHandler);
    };

    return {
        Run: Run
    };

})(jQuery);