"use strict";

var manageArticleCommentModule = (function () {

    function deleteArticleCommentButtonClickEventHandler() {

        $(".DeleteArticleCommentButton").on("click", function (e) {
            e.preventDefault();

            var $this = $(this);
            var url = $this.attr("href");
            var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

            pNotifyModule.confirm("تایید حذف", "آیا از حذف کامنت مورد نظر اطمینان دارید؟", function () {

                $.ajax({
                    type: "POST",
                    url: url,
                    data: { __RequestVerificationToken: antiForgeryToken },
                    dataType: "json",
                    success: function (response) {

                        if (response.status === "Deleted") {

                            pNotifyModule.successNotice("حذف موفق", "حذف کامنت موفقیت آمیز بود.");

                            $this.closest("tr").fadeOut(2000);
                        }

                        if (response.status === "NotDeletedSomeProblem") {

                            pNotifyModule.failureNotice("حذف ناموفق", "حذف کامنت موفقیت آمیز نبود.");

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

    function approvalCheckBoxChangeEventHandler() {

        $(".commentApprovalCheckBox").on("change", function (e) {

            var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
            var commentId = $(this).val();

            $.ajax({
                type: "POST",
                url: "/Admin/Article/ChangeArticleCommentApprovalStatus",
                data: { __RequestVerificationToken: antiForgeryToken, commentId: commentId },
                dataType: "json",
                success: function (response) {

                    if (response.status === "Success") {
                        pNotifyModule.successNotice("تایید موفق", "تایید کامنت مورد نظر موفقیت آمیز بود.");
                    }

                    if (response.status === "NotDeletedSomeProblem") {
                        pNotifyModule.failureNotice("تایید موفق", "تایید کامنت مورد نظر موفقیت آمیز نبود.");
                    }

                },
                error: function (xhr, status, error) {
                    console.log(xhr.responseText);
                    //alert("message : \n" + "An error occurred, for more info check the js console" + "\n status : \n" + status + " \n error : \n" + error);
                }
            });

        });
    }

    function articleDetailModalButtonClickEvent() {
        //code responsible for showing the article comment detail modal
        $(".articleCommentDetail").on("click", function (e) {

            e.preventDefault();

            var $this = $(this);

            var articleCommentBodyTxt = $this.data("article-comment-body");

            $("#articleCommentBodyModal").text(articleCommentBodyTxt);

            $("#ArticleCommentDetailModal").modal("show");

        });
    }

    function articleEditButtonClickEvent() {
        ajaxSpinnerForPartOfPage("#ArticleCommentEditModalContainer");

        var articleCommentId;

        $(".articleCommentEdit").on("click", function (e) {

            e.preventDefault();

            var $this = $(this);

            articleCommentId = $this.data("id");

            var articleCommentBodyTxt = $this.data("article-comment-body");

            $("#ArticleCommentEditModalTxt").val(articleCommentBodyTxt);

            $("#ArticleCommentEditModal").modal("show");

        });

        $("#saveEditedComment").on("click", function () {

            var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
            var articleCommentNewBodyTxt = $("#ArticleCommentEditModalTxt").val();

            var eventOriginatorElement = $(".articleCommentEdit[data-id='" + articleCommentId + "']");
            //console.log(eventOriginatorElement.length);
            //console.log(articleCommentId);
            eventOriginatorElement.data("article-comment-body", articleCommentNewBodyTxt);

            $.ajax({
                type: "POST",
                url: "/Admin/Article/EditArticleComment",
                data: { __RequestVerificationToken: antiForgeryToken, commentId: articleCommentId, newCommentBody: articleCommentNewBodyTxt },
                dataType: "json",
                success: function (response) {

                    if (response.status === "Success") {
                        pNotifyModule.successNotice();
                        $("#ArticleCommentEditModal").modal("hide");
                    }

                    if (response.status === "NotDeletedSomeProblem") {
                        pNotifyModule.failureNotice();
                    }

                },
                error: function (xhr, status, error) {
                    console.log(xhr.responseText);
                    alert("message : \n" + "An error occurred, for more info check the js console" + "\n status : \n" + status + " \n error : \n" + error);
                }
            });

        });
    }

    return {
        wireUpDeleteArticleCommentButtonClickEvent: deleteArticleCommentButtonClickEventHandler,
        wireUpApprovalCheckBoxChangeEvent: approvalCheckBoxChangeEventHandler,
        wireUpArticleDetailModalButtonClickEvent: articleDetailModalButtonClickEvent,
        wireUpArticleEditButtonClickEvent: articleEditButtonClickEvent
    };

})();