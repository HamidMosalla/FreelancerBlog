(function () {
    $(function () {
        "use strict";

        ajaxSpinnerForPartOfPage("#ArticleCommentEditModalContainer");

        var successfulDeleteNotice = function () {
            new PNotify({
                title: 'حذف موفق',
                text: 'آیتم مورد نظر با موفقیت حذف شد.',
                type: 'success',
                icon: 'glyphicon glyphicon-ok',
                delay: 1000
            });
        }

        var ProblematicDeleteNotice = function () {
            new PNotify({
                title: 'حذف ناموفق',
                text: 'مشکلی در حذف کامنت مورد نظر پیش آمده، لطفا دوباره تلاش کنید.',
                type: 'warning',
                icon: 'glyphicon glyphicon-warning-sign',
                delay: 1000
            });
        }

        var GeneralSuccessNotice = function () {
            new PNotify({
                title: 'عملیات موفق',
                text: 'عملیات مورد نظر موفقیت آمیز بود.',
                type: 'success',
                icon: 'glyphicon glyphicon-ok',
                delay: 1000
            });
        }

        var GeneralFailureNotice = function () {
            new PNotify({
                title: 'عملیات ناموفق',
                text: 'مشکلی در انجام عملیات مورد نظر پیش آمده.',
                type: 'success',
                icon: 'glyphicon glyphicon-ok',
                delay: 1000
            });
        }

        // #region code resposible for persian date
        var $articleDateSpans = $(".ArticleDateCreated");

        $($articleDateSpans).each(function (index, element) {

            var dateArray = element.innerHTML.split('/');
            //console.log(dateArray);
            //console.log(element.innerHTML);
            //console.log(JalaliDate.gregorianToJalali(2016, 3, 6));

            element.innerHTML = JalaliDate.gregorianToJalali(dateArray[2], dateArray[0], dateArray[1]).toString().replace(/,/g, '/');
        });

        // #endregion

        //code responsible for deleting comments
        $(".DeleteArticleCommentButton").on("click", function (e) {
            e.preventDefault();

            var $this = $(this);
            var url = $this.attr("href");
            var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

            (new PNotify({
                title: 'تایید حذف',
                text: 'آیا از حذف کامنت مورد نظر اطمینان دارید؟',
                icon: 'glyphicon glyphicon-question-sign',
                hide: false,
                confirm: {
                    confirm: true
                },
                buttons: {
                    closer: false,
                    sticker: false
                },
                history: {
                    history: false
                }
            })).get().on('pnotify.confirm', function () {


                $.ajax({
                    type: "POST",
                    url: url,
                    data: { __RequestVerificationToken: antiForgeryToken },
                    dataType: "json",
                    success: function (response) {

                        if (response.status === "Deleted") {
                            successfulDeleteNotice();
                            $this.closest("tr").fadeOut(2000);
                        }

                        if (response.status === "NotDeletedSomeProblem") {
                            ProblematicDeleteNotice();
                        }

                    },
                    error: function (xhr, status, error) {
                        console.log(xhr.responseText);
                        alert("message : \n" + "An error occurred, for more info check the js console" + "\n status : \n" + status + " \n error : \n" + error);
                    }
                });


            });

        });

        //code responsible for changing the approval status of article comment
        $(".commentApprovalCheckBox").on("change", function (e) {

            //if (this.checked)

            var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
            var commentId = $(this).val();

            $.ajax({
                type: "POST",
                url: "/Admin/Article/ChangeArticleCommentApprovalStatus",
                data: { __RequestVerificationToken: antiForgeryToken, commentId: commentId },
                dataType: "json",
                success: function (response) {

                    if (response.status === "Success") {
                        GeneralSuccessNotice();
                    }

                    if (response.status === "NotDeletedSomeProblem") {
                        GeneralFailureNotice();
                    }

                },
                error: function (xhr, status, error) {
                    console.log(xhr.responseText);
                    alert("message : \n" + "An error occurred, for more info check the js console" + "\n status : \n" + status + " \n error : \n" + error);
                }
            });


        });

        //code responsible for showing the article comment detail modal
        $(".articleCommentDetail").on("click", function (e) {

            e.preventDefault();

            var $this = $(this);

            var articleCommentBodyTxt = $this.data("article-comment-body");

            $("#articleCommentBodyModal").text(articleCommentBodyTxt);

            $("#ArticleCommentDetailModal").modal("show");


        });

        // #region code responsible for showing and saving the article comment edit modal

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
                        GeneralSuccessNotice();
                        $("#ArticleCommentEditModal").modal("hide");
                    }

                    if (response.status === "NotDeletedSomeProblem") {
                        GeneralFailureNotice();
                    }

                },
                error: function (xhr, status, error) {
                    console.log(xhr.responseText);
                    alert("message : \n" + "An error occurred, for more info check the js console" + "\n status : \n" + status + " \n error : \n" + error);
                }
            });

        });

        // #endregion
        

    });
})();