(function () {
    $(function () {
        "use strict";

        ajaxSpinnerForPartOfPage("#ArticleTagEditModalContainer");

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
                text: 'مشکلی در حذف مقاله پیش آمده، لطفا دوباره تلاش کنید.',
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

        //code responsible for deleting the tags
        $(".DeleteArticleTagButton").on("click", function (e) {
            e.preventDefault();

            var $this = $(this);
            var url = $this.attr("href");
            var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

            (new PNotify({
                title: 'تایید حذف',
                text: 'آیا از حذف تگ مورد نظر اطمینان دارید؟',
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

                        if (response.Status === "Deleted") {
                            successfulDeleteNotice();
                            $this.closest("tr").fadeOut(2000);
                        }

                        if (response.Status === "NotDeletedSomeProblem") {
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

        // #region code responsible for showing and saving the article tag edit modal

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

                    if (response.Status === "Success") {

                        GeneralSuccessNotice();

                        $("#" + articleTagId).text(articleTagNewTagNameTxt).hide().fadeIn(850);

                        $("#EditArticleTagModal").modal("hide");

                    }

                    if (response.Status === "NotDeletedSomeProblem") {
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