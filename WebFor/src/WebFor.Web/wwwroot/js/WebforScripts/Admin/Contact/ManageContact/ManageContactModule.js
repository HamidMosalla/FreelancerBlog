"use strict";

var manageContactModule = (function () {

    function deleteContactButtonClickEventHandler() {

        //code responsible for deleting contact
        $(".DeleteContactButton").on("click", function (e) {
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

        });

    }

    function contactDetailButtonClickEventHandler() {

        //code responsible for showing the contact detail modal
        $(".contactDetail").on("click", function (e) {

            e.preventDefault();

            var $this = $(this);

            var contactBodyTxt = $this.data("contact-body");

            $("#contactBodyModaltxt").text(contactBodyTxt);

            $("#ContactDetailModal").modal("show");

        });
    }

    return {
        wireUpDeleteContactButtonClickEvent: deleteContactButtonClickEventHandler,
        wireUpContactDetailButtonClickEvent: contactDetailButtonClickEventHandler,
        initSpinner: ajaxSpinnerForWholePage
    };

})();