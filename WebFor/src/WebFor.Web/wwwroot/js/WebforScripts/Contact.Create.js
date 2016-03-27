(function () {
	$(function () {
	    "use strict";

	    ajaxSpinnerForPartOfPage("#contact-page");

	    $("#YesScript").show();

	    var successfulCreateNotice = function () {
	        new PNotify({
	            title: 'ثبت موفق',
	            text: 'پیغام شما با موفقیت ثبت شد، هم اکنون به صفحه اصلی هدایت میشوید.',
	            type: 'success',
	            icon: 'glyphicon glyphicon-ok',
	            delay: 1000
	        });
	    }

	    var ProblematicCreateNotice = function () {
	        new PNotify({
	            title: 'ثبت ناموفق',
	            text: 'مشکلی در ثبت پیغام شما پیش آمده، لطفا دوباره تلاش کنید، اگر موفق به ثبت نشدید، با مدیریت سایت تماس بگیرید.',
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


		$("#ContactForm").on("submit", function (e) {
			e.preventDefault();

			//var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

			var $form = $(this);

			var formParametersArray = $form.serializeArray();

			formParametersArray.push({ name: 'isJavascriptEnabled', value: true });

			if ($form.valid()) {

				$.ajax({
					type: "POST",
					url: "/Contact/Create",
					data: formParametersArray,//$form.serialize().concat("&isJavascriptEnabled=true"),
					dataType: "json",
					success: function (response) {

					    if (response.Status === "Success") {
					        successfulCreateNotice();
					        setTimeout(function () { window.location.replace("/Home/Index"); }, 3000);
					    }

					    if (response.Status === "ProblematicSubmit") {
					        ProblematicCreateNotice();
					    }

					},
					error: function (xhr, status, error) {
						console.log(xhr.responseText);
						alert("message : \n" + "An error occurred, for more info check the js console" + "\n status : \n" + status + " \n error : \n" + error);
					}
				});


			}

		});


	});
})();