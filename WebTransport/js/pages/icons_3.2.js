/*
 *  Document   : icons.js
 *  Author     : pixelcave
 */
var Icons=function(){var t,n;return{init:function(){var i;$("#page-content .btn").click(function(){return i=$(this).attr("data-original-title"),$("#icon-gen-input").val('<i class="'+i+'"></i>').select(),$("html,body").animate({scrollTop:$("#icon-gen").offset().top-15}),!1}),t=$(".js-icon-list a"),$(".js-icon-search").on("keyup",function(){n=$(this).val().toLowerCase(),n.length>2?(t.hide(),t.each(function(){$("i",this).attr("class").match(n)&&$(this).show()})):0===n.length&&t.show()})}}}();