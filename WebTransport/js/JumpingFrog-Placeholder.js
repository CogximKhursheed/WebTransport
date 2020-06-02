/*UPADHYAY PUNEET*/
/*JUMPING FROG STYLE HIDE AND SEEK PLACEHOLDER*/
$('.virtual-placeholder').each(function () {
    var placeText = $(this).data('placeholder-text');
    var placeWidth = $(this).children('.placeholder').innerWidth();
    var placeHeight = $(this).children('.placeholder').innerHeight();
    $(this).append('<placeholder class="" style="width:' + parseInt(placeWidth - 5) + 'px; height:' + placeHeight + 'px;padding:8px 12px;" >' + placeText + '</placeholder>');
    $(this).children('.placeholder').css("cssText", "z-index: 10 !important;");
    $(this).children('.placeholder').css("background-color", "transparent");
    if ($(this).children('.placeholder').val().length > 0) {
        $(this).children('placeholder').addClass('placeholder-heading');
    }
});

function SetDefaultPlaceholder(ele) {
    if (ele.children('.placeholder').val().length == 0) {
        $(this).siblings('placeholder').addClass('placeholder-heading');
    }
}

$('.placeholder').focus(function () {
    if ($(this).val().length == 0) {
        $(this).siblings('placeholder').addClass('placeholder-heading');
    }
	$(this).siblings('placeholder').addClass('active');
});

$('.placeholder').blur(function () {
    if ($(this).val().length == 0) {
        $(this).siblings('placeholder').removeClass('placeholder-heading');
    }
	$(this).siblings('placeholder').removeClass('active');
});
