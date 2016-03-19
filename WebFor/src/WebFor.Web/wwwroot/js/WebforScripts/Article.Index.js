(function () {
	$(function () {
		"use strict";

	    var $articleDateSpans = $(".ArticleDateCreated");

	    $($articleDateSpans).each(function (index, element) {

	        var dateArray = element.innerHTML.split('/');
	    	//console.log(dateArray);
	        //console.log(element.innerHTML);
	        //console.log(JalaliDate.gregorianToJalali(2016, 3, 6));

	        element.innerHTML = JalaliDate.gregorianToJalali(dateArray[2], dateArray[0], dateArray[1]).toString().replace(/,/g, '/');
	    });


	});
})();