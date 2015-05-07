
jQuery.fn.jetmenu = function(options){
	var settings = {
		 indicator	 		:true     			// indicator that indicates a submenu
		,speed	 			:300     			// submenu speed
		,hideClickOut		:true     			// hide submenus when click outside menu
	}
	$.extend( settings, options );
	
	var menu = $(".jetmenu");
	var lastScreenWidth = window.innerWidth;
	
	if(settings.indicator == true){
		$(menu).find("a").each(function(){
			if($(this).siblings(".dropdown, .megamenu").length > 0){
				$(this).append("<span class='indicator'>+</span>");
			}
		});
	}
	
	$(menu).prepend("<li class='showhide'><span class='title'>MENU</span><span class='icon'><em></em><em></em><em></em><em></em></span></li>");
	
	screenSize();
	
	$(window).resize(function() {
		if(lastScreenWidth <= 768 && window.innerWidth > 768){
			unbindEvents();
			hideCollapse();
			bindHover();
		}
		if(lastScreenWidth > 768 && window.innerWidth <= 768){
			unbindEvents();
			showCollapse();
			bindClick();
		}
		lastScreenWidth = window.innerWidth;
	});
	
	function screenSize(){
		if(window.innerWidth <= 768){
			showCollapse();
			bindClick();
		}
		else{
			hideCollapse();
			bindHover();
		}
	}
	
	function bindHover(){
		if (navigator.userAgent.match(/Mobi/i) || window.navigator.msMaxTouchPoints > 0){						
			$(menu).find("a").on("click touchstart", function(e){
				e.stopPropagation(); 
				e.preventDefault();
				window.location.href = $(this).attr("href");
				$(this).parent("li").siblings("li").find(".dropdown, .megamenu").stop(true, true).fadeOut(settings.speed);
				if($(this).siblings(".dropdown, .megamenu").css("display") == "none"){
					$(this).siblings(".dropdown, .megamenu").stop(true, true).fadeIn(settings.speed);
				}
				else{
					$(this).siblings(".dropdown, .megamenu").stop(true, true).fadeOut(settings.speed);
					$(this).siblings(".dropdown").find(".dropdown").stop(true, true).fadeOut(settings.speed);
				}
			});
			
			if(settings.hideClickOut == true){
				$(document).bind("click.menu touchstart.menu", function(ev){
					if($(ev.target).closest(menu).length == 0){
						$(menu).find(".dropdown, .megamenu").fadeOut(settings.speed);
					}
				});
			}
		}
		else{
			$(menu).find("li").bind("mouseenter", function(){
				$(this).children(".dropdown, .megamenu").stop(true, true).fadeIn(settings.speed);
			}).bind("mouseleave", function(){
				$(this).children(".dropdown, .megamenu").stop(true, true).fadeOut(settings.speed);
			});
		}
	}
	
	function bindClick(){
		$(menu).find("li:not(.showhide)").each(function(){
			if($(this).children(".dropdown, .megamenu").length > 0){
				$(this).children("a").bind("click", function(){
					if($(this).siblings(".dropdown, .megamenu").hasClass("menu-visible")){
						$(this).siblings(".dropdown, .megamenu").slideUp(settings.speed);
						$(this).siblings(".dropdown, .megamenu").removeClass("menu-visible");
					}
					else{
						$(this).siblings(".dropdown, .megamenu").slideDown(settings.speed);
						$(this).siblings(".dropdown, .megamenu").addClass("menu-visible");
						firstItemClick = 1;
					}
				});
			}
		});
	}
	
	function showCollapse(){
		$(menu).children("li:not(.showhide)").hide(0);
		$(menu).children("li.showhide").show(0);
		$(menu).children("li.showhide").bind("click", function(){
			if($(menu).children("li").is(":hidden")){
				$(menu).children("li").slideDown(settings.speed);
			}
			else{
				$(menu).children("li:not(.showhide)").slideUp(settings.speed);
				$(menu).children("li.showhide").show(0);
			}
		});
	}
	
	function hideCollapse(){
		$(menu).children("li").show(0);
		$(menu).children("li.showhide").hide(0);
	}	
	
	function unbindEvents(){
		$(menu).find("li, a").unbind();
		$(document).unbind("click.menu touchstart.menu");
		$(menu).find(".dropdown, .megamenu").hide(0);
	}

}