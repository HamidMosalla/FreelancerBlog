var manageContactModule = (function ($) {
    'use strict';

    function deleteContactHandler(e) {
        e.preventDefault();

        var $this = $(this);
        var url = $this.attr("href");
        var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

        pNotifyModule.confirm("تایید حذف", "آیا از حذف تماس مورد نظر اطمینان دارید؟", function () {

            $.ajax({
                type: "POST",
                url: url,
                data: { __RequestVerificationToken: antiForgeryToken },
                dataType: "json",
                success: function (response) {

                    if (response.status === "Deleted") {
                        pNotifyModule.successNotice("حذف موفق", "حذف موفق");
                        $this.closest("tr").fadeOut(2000);
                    }

                    if (response.status === "NotDeletedSomeProblem") {
                        pNotifyModule.failureNotice("ناموفق", "ناموفق");
                    }

                },
                error: function (xhr, status, error) {
                    console.log(xhr.responseText);
                    alert("message : \n" + "An error occurred, for more info check the js console" + "\n status : \n" + status + " \n error : \n" + error);
                }
            });

        });

    }

    function showContactDetailHandler(e) {

        e.preventDefault();

        var $this = $(this);

        var contactBodyTxt = $this.data("contact-body");

        $("#contactBodyModaltxt").text(contactBodyTxt);

        $("#ContactDetailModal").modal("show");

    }

    var Run = function () {
        $(".DeleteContactButton").on("click", deleteContactHandler);
        $(".contactDetail").on("click", showContactDetailHandler);
    };

    return {
        Run: Run
    };

})(jQuery);