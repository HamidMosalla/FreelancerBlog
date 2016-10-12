"use strict";

var managePortfolioModule = (function () {


    function deletePortfolioButtonClickEventHanlder() {

        $(".DeletePortfolioButton")
            .on("click",
                function (e) {
                    e.preventDefault();

                    var $this = $(this);
                    var url = $this.attr("href");
                    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

                    pNotifyModule.confirm("تایید حذف", "آیا از حذف پورتفولیو مورد نظر اطمینان دارید؟", function () {

                        $.ajax({
                            type: "POST",
                            url: url,
                            data: { __RequestVerificationToken: antiForgeryToken },
                            dataType: "json",
                            success: function (response) {

                                if (response.status === "Deleted") {
                                    pNotifyModule.successNotice("حذف موفق", "فایل مربوط به این پورتفولیو با موفقیت حذف شد");
                                    $this.closest("tr").fadeOut(2000);
                                }

                                if (response.fileStatus === "FileDeleteSuccess") {
                                    pNotifyModule.failureNotice("ناموفق", "ناموفق");
                                }

                                if (response.status === "NotDeletedSomeProblem") {
                                    pNotifyModule.failureNotice("ناموفق", "ناموفق");
                                }

                            },
                            error: function (xhr, status, error) {
                                console.log(xhr.responseText);
                                alert("message : \n" +
                                    "An error occurred, for more info check the js console" +
                                    "\n status : \n" +
                                    status +
                                    " \n error : \n" +
                                    error);
                            }
                        });

                    });

                });

    }

    return {
        wireUpDeletePortfolioButtonClickEvent: deletePortfolioButtonClickEventHanlder
    };

})();