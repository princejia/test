// JavaScript Document for Nevermind Template
// Powered by Designing Media Team

// Mega Menu
(function($) {
 "use strict";
 	$(".panel a").click(function(e){
		e.preventDefault();
			var style = $(this).attr("class");
			$(".jetmenu").removeAttr("class").addClass("jetmenu").addClass(style);
		});
	$().jetmenu();

// Tooltips
    $("[data-toggle=tooltip]").tooltip();

    $("[data-toggle=popover]")
      .popover()
	  
// Count Script
	function count($this){
		var current = parseInt($this.html(), 10);
		current = current + 1; /* Where 50 is increment */
	
		$this.html(++current);
		if(current > $this.data('count')){
			$this.html($this.data('count'));
		} else {    
			setTimeout(function(){count($this)}, 50);
		}
	}        
	
	$(".stat-count").each(function() {
	  $(this).data('count', parseInt($(this).html(), 10));
	  $(this).html('0');
	  count($(this));
	});

// Accordion Toggle Items
   var iconOpen = 'icon-minus',
        iconClose = 'icon-plus';

    $(document).on('show.bs.collapse hide.bs.collapse', '.accordion', function (e) {
        var $target = $(e.target)
          $target.siblings('.accordion-heading')
          .find('em').toggleClass(iconOpen + ' ' + iconClose);
          if(e.type == 'show')
              $target.prev('.accordion-heading').find('.accordion-toggle').addClass('active');
          if(e.type == 'hide')
              $(this).find('.accordion-toggle').not($target).removeClass('active');
    });

// Tooltip
	$('.dm-social, .client, .buddy-members').tooltip({
		selector: "a[data-toggle=tooltip]"
	})
	
	$('.dm-social, .client, .buddy-members').tooltip()

// Back to Top
 jQuery(window).scroll(function(){
	if (jQuery(this).scrollTop() > 1) {
			jQuery('.dmtop').css({bottom:"25px"});
		} else {
			jQuery('.dmtop').css({bottom:"-100px"});
		}
	});
	jQuery('.dmtop').click(function(){
		jQuery('html, body').animate({scrollTop: '0px'}, 800);
		return false;
});

// Carousel Widgets
	$('#myCarousel').carousel();
	$('#myCarouselV').carousel();

// Preloader
	$(window).load(function() {
		$('#status').delay(300).fadeOut('slow');
		$('#preloader').delay(300).fadeOut('slow');
		$('body').delay(300).css({'overflow':'visible'});
	})

})(jQuery);