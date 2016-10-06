"use strict";

var manageArticleTagModule = (function () {

    function deleteTagButtonClickEventHandler() {

        //code responsible for deleting the tags
        $(".DeleteArticleTagButton").on("click", function (e) {
            e.preventDefault();

            var $this = $(this);
            var url = $this.attr("href");
            var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

            pNotifyModule.confirm("تایید حذف", "آیا از حذف تگ مورد نظر اطمینان دارید؟", function () {

                $.ajax({
                    type: "POST",
                    url: url,
                    data: { __RequestVerificationToken: antiForgeryToken },
                    dataType: "json",
                    success: function (response) {

                        if (response.status === "Deleted") {
                            pNotifyModule.successNotice("حذف موفق", "حذف تگ موفقیت آمیز بود.");
                            $this.closest("tr").fadeOut(2000);
                        }

                        if (response.status === "NotDeletedSomeProblem") {
                            pNotifyModule.failureNotice("حذف ناموفق", "حذف تگ موفقیت آمیز نبود.");
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

    function editTagButtonClickEventHandler() {

        var articleTagId;

        $(".EditArticleTagButton").on("click", function (e) {

            e.preventDefault();

            var $this = $(this);

            articleTagId = $this.data("id");

            var articleTagNameTxt = $this.data("tag-name");

            $("#ArticleTagEditTxt").val(articleTagNameTxt);

            $("#EditArticleTagModal").modal("show");

        });

        $("#saveEditedTag").on("click", function () {

            var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
            var articleTagNewTagNameTxt = $("#ArticleTagEditTxt").val();

            var eventOriginatorElement = $(".EditArticleTagButton[data-id='" + articleTagId + "']");
            //console.log(eventOriginatorElement.length);
            //console.log(articleCommentId);
            eventOriginatorElement.data("tag-name", articleTagNewTagNameTxt);

            $.ajax({
                type: "POST",
                url: "/Admin/Article/EditArticleTag",
                data: { __RequestVerificationToken: antiForgeryToken, tagId: articleTagId, newTagName: articleTagNewTagNameTxt },
                dataType: "json",
                success: function (response) {

                    if (response.status === "Success") {

                        pNotifyModule.successNotice("موفق", "ویرایش موفقیت آمیز بود.");

                        $("#" + articleTagId).text(articleTagNewTagNameTxt).hide().fadeIn(850);

                        $("#EditArticleTagModal").modal("hide");

                    }

                    if (response.status === "NotDeletedSomeProblem") {
                        pNotifyModule.failureNotice("ناموفق", "ویرایش موفقیت آمیز بود.");
                    }

                },
                error: function (xhr, status, error) {
                    console.log(xhr.responseText);
                    alert("message : \n" + "An error occurred, for more info check the js console" + "\n status : \n" + status + " \n error : \n" + error);
                }
            });

        });
    }

    function initSpinner() {
        ajaxSpinnerForPartOfPage("#ArticleTagEditModalContainer");
    }

    return {
        wireUpDeleteTagButtonClickEvent: deleteTagButtonClickEventHandler,
        editTagButtonClickEventHandler: editTagButtonClickEventHandler,
        initSpinner: initSpinner
    };

})();