$(document).ready(function(){
						
	$('#featured-info').jCarouselLite({ //slider for homepage featured section info text
		btnPrev:   '#home-featured-nav .prev', 
		btnNext:   '#home-featured-nav .next', 
		easing:'easeInOutExpo',
		speed:900,
		vertical:true,
		visible:1
	});
	
	
	$('#images').jCarouselLite({ //slider for homepage featured images
		btnPrev:   '#home-featured-nav .prev', 
		btnNext:   '#home-featured-nav .next', 
		easing:'easeInOutExpo',
		speed:900,
		visible:1
	});


});	//end of page functions
