

$(window).scroll(function() {
	if ($(this).scrollTop()>50) 
	{
		$("header").addClass("top1");
	}
	else
	{
		$("header").removeClass("top1");
	}
});