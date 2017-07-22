var manageArticleModule = (function ($) {
    'use strict';

    function deleteArticleHandler(e) {
        e.preventDefault();

        var $this = $(this);
        var url = $this.attr("href");
        var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

        pNotifyModule.confirm("تایید حذف",
            "آیا از حذف مقاله مورد نظر اطمینان دارید؟",
            function () {

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


    }

    /*
     ArticleId = a.ArticleId,
     ArticleDateModified = a.ArticleDateModified,
     ArticleDateCreated = a.ArticleDateCreated,
     ArticleStatus = a.ArticleStatus,
     ArticleTitle = a.ArticleTitle
    */

    var Run = function () {
        $(".DeleteArticleButton").on("click", deleteArticleHandler);

        $("#article-table").DataTable({
            "bServerSide": true,
            "sAjaxSource": "/Admin/Article/GetArticles",
            "bProcessing": true,
            "columnDefs": [
                {
                    "targets": 0,
                    "data": "ArticleTitle",
                    "render": function (data, type, full, meta) {
                        return full[1];
                    }
                },
                {
                    "targets": 1,
                    "data": "ArticleStatus",
                    "render": function (data, type, full, meta) {
                        return full[6];
                    }
                },
                {
                    "targets": 2,
                    "data": "ArticleDateCreated",
                    "render": function (data, type, full, meta) {
                        return full[3];
                    }
                },
                {
                    "targets": 3,
                    "data": "ArticleDateModified",
                    "render": function (data, type, full, meta) {
                        return full[4];
                    }
                },
                {
                    "targets": 4,
                    "data": "ArticleId",
                    "render": function (data, type, full, meta) {
                        return `<a href="/Admin/Article/Edit/${full[0]}" class="btn btn-warning">ویرایش</a> &nbsp;` +
                               `<a href="/Article/Details/${full[0]}/${full[1]}" class="btn btn-default">جزئیات</a> &nbsp;` +
                               `<a href="/Admin/Article/Delete/${full[0]}" class="btn btn-danger DeleteArticleButton">حذف</a>`;
                    }
                }
            ]
        });
    };

    return {
        Run: Run
    };

})(jQuery);