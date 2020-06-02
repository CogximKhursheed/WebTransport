        $('.fa.fa-info').click(function () {
            $(this).toggleClass('show-static');
            if ($(this).hasClass('show-static')) {
                $('.fa.fa-info').removeClass('show-static')
                $(this).addClass('show-static');
                $('.hint-detail').slideUp(300);
                $(this).next('.hint-detail').slideDown(300);
            }
            else {
                $('.hint-detail').slideUp(300);
            }
        });
        $('html').click(function (e) {
            if (!$(e.target).hasClass('show-static')) {
                $('.fa.fa-info').removeClass('show-static')
                $('.hint-detail').slideUp(300);
            }
        }); 