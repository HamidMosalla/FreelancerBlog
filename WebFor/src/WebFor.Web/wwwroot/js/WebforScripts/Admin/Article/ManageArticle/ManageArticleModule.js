"use strict";

var manageArticleModule = (function () {

    function deleteArticleButtonClickEventHandlerFor() {

        $(".DeleteArticleButton").on("click", function (e) {
            e.preventDefault();

            var $this = $(this);
            var url = $this.attr("href");
            var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

            pNotifyModule.confirm("تایید حذف", "آیا از حذف مقاله مورد نظر اطمینان دارید؟", function () {

                $.ajax({
                    type: "POST",
                    url: url,
                    data: { __RequestVerificationToken: antiForgeryToken },
                    dataType: "json",
                    success: function (response) {

                        if (response.status === "Deleted") {

                            pNotifyModule.successNotice("حذف موفقیت آمیز", "حذف مقاله موفقیت آمیز بود.");

                            $this.closest("tr").fadeOut(2000);
                        }

                        if (response.status === "NotDeletedSomeProblem") {

                            pNotifyModule.failureNotice("حذف ناموفق", "حذف مقاله موفقیت آمیز نبود.");

                        }

                    },
                    error: function (xhr, status, error) {
                        console.log(xhr.responseText);
                        //alert("message : \n" + "An error occurred, for more info check the js console" + "\n status : \n" + status + " \n error : \n" + error);
                    }
                });

            });


        });
    }

    return {
        setupClickEventForDeleteArticleButton: deleteArticleButtonClickEventHandlerFor
    };

})();