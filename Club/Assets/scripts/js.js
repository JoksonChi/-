// JavaScript Document
$(function(){
		$(".chi_m_lnav a").click(
		function(){
			var _s=$(this).index();
			$(this).addClass('on').siblings().removeClass('on');
			$(".chi_m_licon ul").eq(_s).show().siblings().hide();
			}
	);

	$(".chi_left_2c .top a").click(
		function(){
			var _s=$(this).index();
			$(this).addClass('on').siblings().removeClass('on');
			$(".chi_left_2c .display .con").eq(_s).show().siblings().hide();
			}
	)
});