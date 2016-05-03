(function () {
	$(function () {
	    "use strict";

	    //ajaxSpinnerForPartOfPage("#contact-page");


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


	    //$('#wizard').smartWizard();


	    $(".done").click(function () {
	        var this_li_ind = $(this).parent().parent("li").index();
	        if ($('.payment-wizard li').hasClass("jump-here")) {
	            $(this).parent().parent("li").removeClass("active").addClass("completed");
	            $(this).parent(".wizard-content").slideUp();
	            $('.payment-wizard li.jump-here').removeClass("jump-here");
	        } else {
	            $(this).parent().parent("li").removeClass("active").addClass("completed");
	            $(this).parent(".wizard-content").slideUp();
	            $(this).parent().parent("li").next("li:not('.completed')").addClass('active').children('.wizard-content').slideDown();
	        }
	    });

	    $('.payment-wizard li .wizard-heading').click(function () {
	        if ($(this).parent().hasClass('completed')) {
	            var this_li_ind = $(this).parent("li").index();
	            var li_ind = $('.payment-wizard li.active').index();
	            if (this_li_ind < li_ind) {
	                $('.payment-wizard li.active').addClass("jump-here");
	            }
	            $(this).parent().addClass('active').removeClass('completed');
	            $(this).siblings('.wizard-content').slideDown();
	        }
	    });


	});
})();