(function () {
    $(function () {
        "use strict";

        var successfulDeleteNotice = function () {
            new PNotify({
                title: 'حذف موفق',
                text: 'آیتم مورد نظر با موفقیت حذف شد.',
                type: 'success',
                icon: 'glyphicon glyphicon-ok',
                delay: 1000
            });
        }

        var ProblematicDeleteNotice = function() {
            new PNotify({
                title: 'حذف ناموفق',
                text: 'مشکلی در حذف مقاله پیش آمده، لطفا دوباره تلاش کنید.',
                type: 'warning',
                icon: 'glyphicon glyphicon-warning-sign',
                delay: 1000
            });
        }

        $(".DeletePortfolioButton").on("click", function (e) {
                e.preventDefault();

                var $this = $(this);
                var url = $this.attr("href");
                var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

                (new PNotify({
                    title: 'تایید حذف',
                    text: 'آیا از حذف پورتفولیو مورد نظر اطمینان دارید؟',
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

        });
    })();