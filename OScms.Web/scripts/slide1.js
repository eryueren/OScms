
$(function(){
		   
  var page = 0;
  //var i = 6;//ÿ���6��ͼƬ
	//var v_width = 140 ;//��ʾͼƬ���		
	 var len=$(".move_tu").find("li").length;//18��li
	//var page_count = Math.floor(len) ;   //3��
    var $v_show = $("div.move_tu");          //�ƶ���ͼƬ

var adTimer;
	
		$(".buttom_1").click(function(){  
									
				
				if(!$v_show.is(":animated"))
				{
					 if(page>=(len-6))
					   {
						  return false;
						}
						
						else{$v_show.animate({ left :'-='+140 }, 1000);page++;}
				}
				
         });

		    $(".buttom_r").click(function(){
				 if(!$v_show.is(":animated"))
				 {
					  if(page<=0)
					   {
						  return false;
						}
					   
						
						else{$v_show.animate({ left : '+='+140 }, 1000);page--;}
				}
					  
								  
		});
			
		/*	 
		function showImg_0(){
				
						
				
				};
		
			
			function showImg_count(page){
				
				
				
				};
			function showImg_count_1(page){
				
				
				
				};
				$("#ztc_h2").hover(function(){
								adTimer = setInterval(function(){
											alert(0)
											
											
											
											
											
										}, 2000);	},	   
					
					function(){
						clearInterval(adTimer);
						})
				
				
		 $('.move_slider').hover(function(){
			 clearInterval(adTimer);
		 },function(){
			 adTimer = setInterval(function(){
			    if(!$v_show.is(":animated"))
				{
					  if(page==page_count)
					  {
						 $v_show.animate({ left : 0 }, "fast"); 
						  page=0;
						}
						
					  else if(page==(page_count-1))
					  {
						 var all_width=len*140-840;//  �Ƶ����һ�� 
						
						 $v_show.animate({ left : -all_width }, 1000); 
						  page++;
						}
					  else if(page==0)
					   {
						  var num=len-page_count*i;
						   var num_width=num*140;
							$v_show.animate({ left : '-='+num_width }, 1000); page++;
						  
							
						}
						
						else{$v_show.animate({ left : '-='+v_width }, 1000); page++;}
				}
			 
			 } , 15000);
	 }).trigger("mouseleave");;*/
			
		 
})



	 
		
		
		
		
		
