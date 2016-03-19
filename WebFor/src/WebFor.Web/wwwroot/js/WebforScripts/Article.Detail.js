(function () {
    $(function () {
        "use strict";

        ajaxSpinnerForPartOfPage("#articleRatingContainer");

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


        //code responsible for submitting the rating and updating it and showing proper notification
        $('#ArticleRating').on("change", function() {

            var $this = $(this);

            $.ajax({
                type: "POST",
                url: "/Article/RateArticle",
                data: { id: $("#ArticleId").val(), rating: $this.val() },
                dataType: "json",
                success: function (response) {

                    if (response.Status === "YouMustLogin") {

                        $this.rating('rate', 0);

                        new PNotify({
                            title: 'ثبت ناموفق',
                            text: 'برای امتیاز دادن باید به سایت وارد شوید.',
                            type: 'warning',
                            icon: 'glyphicon glyphicon-warning-sign',
                            delay: 5000
                        });

                    }

                    if (response.Status === "Success") {

                        new PNotify({
                            title: 'عملیات موفق',
                            text: 'امتیاز شما با موفقیت ثبت شد.',
                            type: 'success',
                            icon: 'glyphicon glyphicon-ok',
                            delay: 5000
                        });

                    }

                    if (response.Status === "SomeProblemWithSubmit") {

                        new PNotify({
                            title: 'ثبت ناموفق',
                            text: 'مشکلی در ثبت امتیاز شما پیش آمده، لطفا دوباره تلاش کنید.',
                            type: 'daner',
                            icon: 'glyphicon glyphicon-warning-sign',
                            delay: 5000
                        });

                    }

                    if (response.Status === "UpdatedSuccessfully") {

                        new PNotify({
                            title: 'عملیات موفق',
                            text: 'امتیاز شما با موفقیت به روز رسانی شد.',
                            type: 'success',
                            icon: 'glyphicon glyphicon-ok',
                            delay: 5000
                        });

                    }

                },
                error: function (xhr, status, error) {
                    console.log(xhr.responseText);
                    alert("message : \n" + "An error occurred, for more info check the js console" + "\n status : \n" + status + " \n error : \n" + error);
                }
            });

        });

    });
})();