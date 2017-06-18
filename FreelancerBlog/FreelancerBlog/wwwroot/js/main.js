jQuery(function ($) {


    //center the main slider navigation buttons
    $('.centered').each(function (e) {
        $(this).css('margin-top', ($('#main-slider').height() - $(this).height()) / 2);
    });

    //got to do with index slider
    $(window).resize(function () {
        $('.centered').each(function (e) {
            $(this).css('margin-top', ($('#main-slider').height() - $(this).height()) / 2);
        });
    });

    //portfolio filter => isotope
    $(window).load(function () {
        var $portfolioSelectors = $('.portfolio-filter >li>a');

        if ($portfolioSelectors != 'undefined') {
            var $portfolio = $('.portfolio-items');
            $portfolio.isotope({
                itemSelector: 'li',
                layoutMode: 'fitRows'
            });

            $portfolioSelectors.on('click', function () {
                $portfolioSelectors.removeClass('active');
                $(this).addClass('active');
                var selector = $(this).attr('data-filter');
                $portfolio.isotope({ filter: selector });
                return false;
            });

        }
    });

    //goto top
    $('.gototop').click(function (event) {
        event.preventDefault();
        $('html, body').animate({
            scrollTop: $("body").offset().top
        }, 500);
    });

    //responsible for faking the submit of newsletter!
    $("#news-letter-form").on("submit", function (e) {
        e.preventDefault();

        $("#news-letter-alert").slideDown();
        $("#notification-modal").modal("show");
        this.reset();

    });

    $("#copyright-anchor").on("click", function(e) {
        e.preventDefault();

        $("#news-letter-alert").slideUp();
        $("#copyright-alert").slideDown();
        $("#notification-modal").modal("show");

    });

});