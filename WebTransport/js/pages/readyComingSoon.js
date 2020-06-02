/*
 *  Document   : readyComingSoon.js
 *  Author     : pixelcave
 */
var ComingSoon=function(){return{init:function(){$(".js-countdown").countdown((new Date).getFullYear()+1+"/01/15",function(n){$(this).html(n.strftime('<div class="row"><div class="col-xs-6 col-sm-3 countdown-con"><div class="countdown-num">%-D</div><div class="countdown-info">DAYS</div></div><div class="col-xs-6 col-sm-3 countdown-con"><div class="countdown-num">%H</div><div class="countdown-info">HOURS</div></div><div class="col-xs-6 col-sm-3 countdown-con"><div class="countdown-num">%M</div><div class="countdown-info">MINUTES</div></div><div class="col-xs-6 col-sm-3 countdown-con"><div class="countdown-num">%S</div><div class="countdown-info">SECONDS</div></div></div>'))})}}}();