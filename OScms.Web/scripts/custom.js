// JavaScript Document
$(function(){
	  /* $("#navigation ul li:has(ul)").hover(function(){
			$(this).children("ul").stop(true,true).slideDown("slow");
        },function(){
		    $(this).children("ul").stop(true,true).slideUp("fast");
		});	  */ 
 /*$(".menu_content>li").eq(0).children().css("background","url(images/mBackground.gif)")*/
 
 
 /*$(".menu_content>li").eq(0).mouseover(
	function(){
		$(this).children().css("background","url(images/mBackground.gif)").parent().siblings().children().css("background","none");
		$(".menu_AboutUs").siblings().hide();
		$(".menu_AboutUs").show();								   
		});*/
 $(".menu_content>li").eq(0).mouseover(
	function(){
		$(this).children().css("background","url(images/mBackground1.gif)").parent().siblings().children().css("background","none");
		$(".menu_index").siblings().hide();
		$(".menu_index").show();								   
		});
		
 $(".menu_content>li").eq(1).mouseover(
	function(){
		$(this).children().css("background","url(images/mBackground.gif)").parent().siblings().children().css("background","none");
		$(".menu_AboutUs").siblings().hide();
		$(".menu_AboutUs").show();								   
		});
	  
	  
$(".menu_content>li").eq(2).mouseover(
	function(){
			$(this).children().css("background","url(images/mBackground.gif)").parent().siblings().children().css("background","none");
		$(".menu_class").siblings().hide();
		$(".menu_class").show();								   
		});

$(".menu_content>li").eq(3).mouseover(
	function(){
			$(this).children().css("background","url(images/mBackground.gif)").parent().siblings().children().css("background","none");
		$(".menu_teacher").siblings().hide();
		$(".menu_teacher").show();								   
		});
$(".menu_content>li").eq(4).mouseover(
	function(){
			$(this).children().css("background","url(images/mBackground.gif)").parent().siblings().children().css("background","none");
		$(".menu_works").siblings().hide();
		$(".menu_works").show();								   
		});
$(".menu_content>li").eq(5).mouseover(
	function(){
			$(this).children().css("background","url(images/mBackground.gif)").parent().siblings().children().css("background","none");
		$(".menu_item").siblings().hide();
		$(".menu_item").show();								   
		});

$(".menu_content>li").eq(6).mouseover(
	function(){
			$(this).children().css("background","url(images/mBackground.gif)").parent().siblings().children().css("background","none");
		$(".menu_action").siblings().hide();
		$(".menu_action").show();								   
		});

$(".menu_content>li").eq(7).mouseover(
	function(){
			$(this).children().css("background","url(images/mBackground.gif)").parent().siblings().children().css("background","none");
		$(".menu_balcony").siblings().hide();
		$(".menu_balcony").show();								   
		});
$(".menu_content>li").eq(8).mouseover(
	function(){
			$(this).children().css("background","url(images/mBackground.gif)").parent().siblings().children().css("background","none");
		$(".menu_center").siblings().hide();
		$(".menu_center").show();								   
		});

$(".menu_content>li").eq(9).mouseover(
	function(){
			$(this).children().css("background","url(images/mBackground.gif)").parent().siblings().children().css("background","none");
		$(".menu_demand").siblings().hide();
		$(".menu_demand").show();								   
		});


  $(".leftmenu1 ul li").bind("click",function(){
		$(this).siblings().children().slideUp("fast");									  
		$(this).children().slideDown("slow");
	});


})

// ²ÍÌü½éÉÜ
