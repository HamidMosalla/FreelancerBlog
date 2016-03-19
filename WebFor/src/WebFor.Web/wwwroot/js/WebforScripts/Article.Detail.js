(function () {
    $(function () {
        "use strict";

        ajaxSpinnerForPartOfPage();

        var $articleDateSpans = $(".ArticleDateCreated");

        $($articleDateSpans).each(function (index, element) {

            var dateArray = element.innerHTML.split('/');
            //console.log(dateArray);
            //console.log(element.innerHTML);
            //console.log(JalaliDate.gregorianToJalali(2016, 3, 6));

            element.innerHTML = JalaliDate.gregorianToJalali(dateArray[2], dateArray[0], dateArray[1]).toString().replace(/,/g, '/');
        });


        //simple rating without tooltip fraction

        //$('#ArticleRating').rating({
        //    extendSymbol: function (rate) {
        //        //console.log($(this));
        //        $(this).tooltip({
        //            container: 'body',
        //            placement: 'bottom',
        //            title: 'Rate ' + rate
        //        });
        //    }
        //});

        //rating with tooltip fraction
        $('#ArticleRating').rating({
            extendSymbol: function () {
                var title;
                $(this).tooltip({
                    container: 'body',
                    placement: 'bottom',
                    trigger: 'manual',
                    title: function () {
                        return title;
                    }
                });
                $(this).on('rating.rateenter', function (e, rate) {
                    title = rate;
                    $(this).tooltip('show');
                })
                .on('rating.rateleave', function () {
                    $(this).tooltip('hide');
                });
            }
        });


        $('#ArticleRating').on("change", function() {
            
            alert('Rating: ' + $(this).val());

        });

    });
})();