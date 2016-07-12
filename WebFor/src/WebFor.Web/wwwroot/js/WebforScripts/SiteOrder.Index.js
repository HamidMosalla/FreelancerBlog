(function () {
    $(function () {
        "use strict";

        ajaxSpinnerForPartOfPage("#show_form");

        $('a').click(function () { $('html, body').animate( { scrollTop: $($.attr(this, 'href')).offset().top }, 500); return false; });

        // go to the next step
        $(".done").click(function (e) {

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

        });

        //expand and collapse the heading
        $('.payment-wizard li .wizard-heading').click(function () {

            if ($(this).parent().hasClass('completed')) {

                var thisLiInd = $(this).parent("li").index();

                var liInd = $('.payment-wizard li.active').index();

                if (thisLiInd < liInd) {
                    $('.payment-wizard li.active').addClass("jump-here");
                }
                $(this).parent().addClass('active').removeClass('completed');

                $(this).siblings('.wizard-content').slideDown();

            }

        });


        $("#calculateFinalPriceForm").on("submit", function (e) {
            e.preventDefault();

            //var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

            var trueCheckBoxes = $(".slideThree input[type='checkbox']:checked");

            //console.log(trueCheckBoxes.length);

            $.each(trueCheckBoxes, function (index, value) {
                //console.log(value); 
                $(value).val(true);

            });

            var $form = $(this);

            var formParametersArray = $form.serializeArray();

            if ($form.valid()) {

                $.ajax({
                    type: "POST",
                    url: "/SiteOrder/Index",
                    data: formParametersArray,//$form.serialize()
                    dataType: "json",
                    success: function (response) {

                        //console.log(response.price);
                        //console.log(response.priceSheet);
                        //console.log(response.status);

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

        });

    });
})();