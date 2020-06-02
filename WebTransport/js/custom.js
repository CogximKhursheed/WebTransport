jQuery(function($) {
  // function cb(start, end) {
  //       $('#reportrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
  //   }
  //   cb(moment().subtract(29, 'days'), moment());

  //   $('#reportrange').daterangepicker({
  //       ranges: {
  //          'Today': [moment(), moment()],
  //          'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
  //          'Last 7 Days': [moment().subtract(6, 'days'), moment()],
  //          'Last 30 Days': [moment().subtract(29, 'days'), moment()],
  //          'This Month': [moment().startOf('month'), moment().endOf('month')],
  //          'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
  //       }
  // }, cb);

  // var dt = new Date();
  // var time = dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
  // $('#time_in').val(time);


  // $('.datepicker-input').datepicker().on('changeDate', function (ev) {
  //   $(this).datepicker('hide');
  // });


    $(".datepicker").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd-mm-yy',
        minDate: null,
        maxDate: null
    });

  $( ".datepicker-dob" ).datepicker({
    changeMonth: true,
    changeYear: true
  });

  $('#item_master_list').DataTable( {
    // "scrollY": 170,
    "scrollX": 500,
    "lengthMenu": [[50], [50]]
  } );

  $('#rate_master').DataTable( {
    // "scrollY": 170,
    "scrollX": 500,
    "lengthMenu": [[50], [50]]
  } );

  $('#ledger_list').DataTable( {
    // "scrollY": 170,
    "scrollX": 500,
    "lengthMenu": [[50], [50]]
  } );

  $('#challan_confirm').DataTable( {
    // "scrollY": 170,
    "scrollX": 500,
    "lengthMenu": [[50], [50]]
  } );

  $('#employee_master_list').DataTable( {
    // "scrollY": 170,
    "scrollX": 500,
    "lengthMenu": [[50], [50]]
  } );

  $('#challan_confirmation').DataTable( {
    // "scrollY": 170,
    "scrollX": 500,
    "lengthMenu": [[50], [50]]
  } );

  $('#challan_confirmation_grdetails').DataTable( {
    // "scrollY": 170,
    "scrollX": 500,
    "lengthMenu": [[50], [50]]
  } );

  $('#challan_confirmation_searchform').DataTable( {
    // "scrollY": 170,
    "scrollX": 500,
    "lengthMenu": [[50], [50]]
  } );

  $('#material_issue').DataTable( {
    "scrollY": 101,
    "scrollX": 500,
    "lengthMenu": [[50], [50]]
  } );

  $('#trip_sheet_popup_form').DataTable( {
    // "scrollY": 101,
    "scrollX": 350,
    "lengthMenu": [[50], [50]]
  } );

  $('#trip_sheet_challan_details').DataTable( {
    "scrollY": 158,
    "scrollX": 400,
    "lengthMenu": [[50], [50]]
  } );

  $('#trip_sheet_fuel_details').DataTable( {
    "scrollY": 158,
    "scrollX": 400,
    "lengthMenu": [[50], [50]]
  } );

  $('#trip_sheet_expense_details').DataTable( {
    "scrollY": 100,
    "scrollX": 400,
    "lengthMenu": [[50], [50]]
  } );

  $('#trip_sheet_toll_details').DataTable( {
    "scrollY": 158,
    "scrollX": 400,
    "lengthMenu": [[50], [50]]
  } );


  var current = window.location.pathname.split("/").pop();  
  var target = $( '.sidebar-nav a[href="'+current+'"]' );
  target.addClass('active');

  var fp = $('a.active').parent().parent().parent();
  var sp = $('a.active').parent().parent().parent().parent().parent();

  $(fp).addClass("active");
  $(sp).addClass("active");

   $('.sidebar-nav-menu, .sidebar-nav-submenu').on('click', function() { 
    $(this).parent().toggleClass('open').removeClass('active');  
  });

  $("#checkAll").click(function () {
    $(".check").prop('checked', $(this).prop('checked'));
  });

  $('header.navbar>ul:first-child').on('click', function() { 
   $('#page-container').toggleClass('custommenu');  
  });

  $('input[type="radio"]').click(function() {
    if($(this).attr('class') == 'by_receipt') {
        $('.on_radio_select_btn').css('display','block');           
    }
    else {
        $('.on_radio_select_btn').hide();   
    }
  });

});

/***REPORT-FILTER****/
function ShowFilterValue() {
    $('.filter-field').remove();
    $('.filter .control-holder select').each(function () {
        var ele = $(this);
        if (parseInt($('#' + ele.attr('id') + ' option:selected').val()) > 0) {
            var fieldId = ele.attr('id');
            var fieldName = ele.parent().prev().children().text();
            var fieldText = $('#' + ele.attr('id') + ' option:selected').text();
            var isRequired = ele.hasClass('required') ? 'disabled' : '';
            var editType = ele.hasClass('required') ? 'edit' : 'close';
            $('.selected-filetrs').append('<div class="filter-field " onclick="openFilter();"><div class="field-name">' + fieldName + '</div><div class="field-text">' + fieldText + '</div><span class="delete"  onclick="RemoveFilter($(this))" data-id="' + fieldId + '"><i class="fa fa-' + editType + '"></i></span></div>')
        }
    });
    $('.filter .control-holder input[type=text]').each(function () {
        var ele = $(this);
        if (ele.val().length > 0) {
            var fieldId = ele.attr('id');
            var fieldName = ele.parent().prev().children().text();
            var fieldText = ele.val();
            var isRequired = ele.hasClass('required') ? 'disabled' : '';
            var editType = ele.hasClass('required') ? 'edit' : 'close';
            $('.selected-filetrs').append('<div class="filter-field "  onclick="openFilter();"><div class="field-name">' + fieldName + '</div><div class="field-text">' + fieldText + '</div><span class="delete" onclick="RemoveFilter($(this))" data-id="' + fieldId + '"><i class="fa fa-' + editType + '"></i></span></div>')
        }
    });
}

function openFilter() {
    $('.filter').slideDown();
    if (!$(this).hasClass('active')) {
        $('.fa-filter').addClass('active');
    }
}

function RemoveFilter(ele) {
    var id = ele.data('id');
    if (ele.children('i').hasClass('fa-close')) {
        $('#' + id + ' option').val(0);
        $('#' + id).val('');
        ShowFilterValue();
        //$('form').submit();
    }
    $('#' + id).focus();
    $('#' + id).active();
}
$('.filter input, .filter select, .filter checkbox, .filter radio').change(function () {
    ShowFilterValue();
});
$(document).ready(function () {
    AnimatedIcon();
    LoadControls();
    setTimeout(function () {
        $('.fa.fa-filter').removeClass('blink');
    }, 5000);
});
function LoadControls() {
    //AutoHeight();
    ShowFilterValue();
    //AnimatedIcon();
    setTimeout(function () {
        $('.fa.fa-filter').removeClass('blink');
    }, 5000);
}

/*DOWNLOAD-OPTION*/
$(".title-action .fa.fa-download").click(function () {
    $(this).next('.download-option-box').addClass("active");
    $(".download-option-container").removeClass("empty");
    var downloadOPtionExist = $(this).next(".download-option-box").children(".download-option-container").children("ul").children("li").children("input").length;
    if (downloadOPtionExist == 0 || downloadOPtionExist == undefined) {
        $(".download-option-container").addClass("empty");
        $(".download-option-container li").hide();
    }
});
$(".close-download-box").click(function () {
    $(this).parent().removeClass("active");
});
var prm = Sys.WebForms.PageRequestManager.getInstance();
prm.add_endRequest(function () {
    ShowFilterValue();
    /*DOWNLOAD-OPTION*/
    $(".title-action .fa.fa-download").click(function () {
        $(this).next('.download-option-box').addClass("active");
        $(".download-option-container").removeClass("empty");
        var downloadOPtionExist = $(this).next(".download-option-box").children(".download-option-container").children("ul").children("li").children("input").length;
        if (downloadOPtionExist == 0 || downloadOPtionExist == undefined) {
            $(".download-option-container").addClass("empty");
            $(".download-option-container li").hide();
        }
    });
    $(".close-download-box").click(function () {
        $(this).parent().removeClass("active");
    });
});
/*PRINTER-ANIMATION*/
/*ANIMATED-ICON*/
function AnimatedIcon() {
    if ($('.printing-animation').length > 0) {
        $('.printing-animation').append('<i class="printer"></i><i class="report"></i><i class="blank-paper"></i>');
    }
}
/*AUTOCOMPLETE-SEARCH*/
$(document).ready(function () {
    if ($("#TopMenuSearchBox select option").length == 0) {
        var count = 0;
        $("ul.sidebar-nav > li").each(function () { count++; $(this).addClass("dir-li"); $(this).data("seq", count); });
        $(".sidebar-nav  a[href*='aspx']").each(function () {
            var link = $(this).attr('href');
            var text = $(this).text();
            var parText = $(this).closest('.dir-li').children("a").children("span:last-child").text().trim();
            if (parText.length > 0) {
                text = text + " - " + parText;
            }
            $("#TopMenuSearchBox select").append("<option data-search-keyword='" + $(this).attr('data-search-keyword') + "' title='" + text + "' onclick=\"opentargetLink($(this))\" value='" + link + "'>" + text + "</option>");
            sortDropdownAsc($("#TopMenuSearchBox select"));
        });
    }
});
$('input.autocomplete-search').focus(function () {
    $(this).parent().addClass('drop-active');
});
$('input.autocomplete-search').on("blur", function (event) {
    $(this).parent().removeClass('drop-active');
});
$('.autocomplete-search').click(function () {
    var searchInput = $(this);
    var results = $(this).next('.trends');
    if ((searchInput.val() || '').length > 0) {
        //    $("#trends").slideDown();
        //    $('#search').select();
    }
    else {
        results.hide();
        $("option:selected", results).removeAttr("selected");
    }
});

$('.autocomplete-search').on("keydown", function (event) {
    var searchInput = $(this);
    var results = $(this).next('.trends');
    var key = event.keyCode;
    if (key == 9) {
        $(this).parent().toggleClass('drop-active');
        results.slideUp();
    }
});

$('.autocomplete-search').on("keyup", function (event) {
    var searchInput = $(this);
    var results = $(this).next('.trends');
    var key = event.keyCode;
    if (key == 40) {
        results.slideDown();
        var inputs = $(this).parents().eq(0).find(":input");
        var idx = inputs.index(this);
        results.focus();
        results.select();
        results.val($("option:first", results).val());
        //$("#target option:first").attr('selected','selected');
        var e = jQuery.Event("keydown");
        e.keyCode = 40;
        searchInput.trigger(e);
    }
    if (key == 27) {
        searchInput.focus();
        searchInput.val('');
        $("option:selected", results).removeAttr("selected");
    }
    if (searchInput.val() != "") {
        if (key == 9) {
            results.slideUp();
        }
        results = $(this).next('.trends');
        results.slideDown();
        var userInput = searchInput.val();
        showOnlyOptionsSimilarToText(results, userInput);
        SetSearchListHeight(results, results.children('option').length);
    }
    else {
        if (key == 9) {
            results.slideUp();
        }
    }
});

/*KEYUP*/
$("body").on('keydown', function (e) {
    if ((e.ctrlKey || e.metaKey) && (e.metaKey || e.shiftKey) && (e.which == 70)) {
        $("input.autocomplete-search.form-control.ele-hanger.input-cross").focus();
    }
});

/**GET-Querystring-value**/
function getQueryStringVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}
function sortDropdownAsc(ddl) {
    ddl.html(ddl.find('option').sort(function (x, y) {
        // to change to descending order switch "<" for ">"
        return $(x).text() > $(y).text() ? 1 : -1;
    }));
}
function sortDropdownDesc(ddl) {
    ddl.html(ddl.find('option').sort(function (x, y) {
        // to change to descending order switch "<" for ">"
        return $(x).text() < $(y).text() ? 1 : -1;
    }));
}
function opentargetLink(ele) {
    var link = ele.attr("value");
    if (link != "undefined") {
        window.location.href = link;
        var text = ele.attr("title");
        showLoadingMessageStatus("<p>Redirecting to <b>" + text + "</b><p>", "primary", 0);
    }
}
//SEARCH DROPDRWN
var showOnlyOptionsSimilarToText = function (selectionEl, str) {
    str = str.toLowerCase();
    // cache the jQuery object of the <select> element
    var $el = $(selectionEl);

    if (!$el.data("options")) {
        // cache all the options inside the <select> element for easy recover
        $el.data("options", $el.find("option").clone());
    }

    var newOptions = $el.data("options").filter(function () {
        var text = $(this).text();
        text = text.toLowerCase() + " " + $(this).attr("data-search-keyword").toLowerCase();
        return text.match(str);
    });
    $el.empty().append(newOptions);
    SetSearchListHeight(selectionEl, newOptions.length);
};
$('.trends').keydown(function (e) {
    var results = $(this);
    var searchInput = $(this).prev('.autocomplete-search');
    var key = e.keyCode;
    if (key == 13) {
        if ($(searchInput).parent("#TopMenuSearchBox").length === 1) {
            var link = results.children("option:selected").val();
            if (link !== "undefined") {
                window.location.href = link;
                var text = results.children("option:selected").attr("title");
                showLoadingMessageStatus("<p>Redirecting to <b>" + text + "</b><p>", "primary", 0);
            }
            event.preventDefault();
            return false;
        }
        var ele = $(this);
        searchInput.val($("option:selected", results).text());
        searchInput.focus();
        ele.slideUp();
        $("option:selected", results).removeAttr("selected");
        event.preventDefault();
        return false;
    }
    if (key == 8 || key == 27) {
        searchInput.focus();
        searchInput.val('');
        $("option:selected", results).removeAttr("selected");
    }
    if (key == 38) {
        if ($("option:selected:first-child", results).is(":selected")) {
            searchInput.focus();
            searchInput.select();
            $("option:selected", results).removeAttr("selected");
        }
    }
});
$('.trends').click(function (e) {
    var results = $(this);
    var searchInput = $(this).prev('.autocomplete-search');
    searchInput.val($("option:selected", results).text());
    searchInput.focus();
    results.slideUp();
    $("option:selected", results).removeAttr("selected");
});
function SetSearchListHeight(List, itms) {
    if (itms > 0) {
        var minHeight = 24;
        var maxHeight = 240;
        var height = parseInt(itms) * parseInt(minHeight);
        if (parseInt(height) <= maxHeight) {
            List.css("height", height + "px");
            List.css("min-height", height + "px");
        }
        else {
            List.css("height", 240 + "px");
            List.css("min-height", 240 + "px");
        }
    }
    else {
        List.hide();
        $("option:selected", List).removeAttr("selected");
    }
}
$("body").on('click', function (e) {
    $(".trends").slideUp();
    $(".trends option:selected").removeAttr("selected");
});

$("#TopMenuSearchBox").on('click', function (e) {
    e.stopPropagation();
});
function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}
//////**LOADING-QUICK-MESSAGE**//////
//hideLoadingMessage();
//showLoadingMessage("Network error occurred");
//showLoadingMessageStatus("Internet connection is offline", "failure", 5);
/*LOADING-MESSAGE*/
function showLoadingMessage(msg) {
    $('.loading-message-img .spinner').removeClass("success");
    $('.loading-message-img .spinner').removeClass("failure");
    $('.loading-message .loading-message-info').html(msg);
    $('.loading-message').addClass('show');
}
function hideLoadingMessage() {
    $('.loading-message').removeClass('show');
    $('.loading-message').removeClass("no-load");
}
function hideLoadingMessageWithDelay(delay) {
    setTimeout(function () {
        $('.loading-message').removeClass('show');
        $('.loading-message').removeClass("no-load");
    }, delay * 1000);
}
function showLoadingMessageStatus(msg, type, delay) {
    $('.loading-message-img .spinner').removeClass("success");
    $('.loading-message-img .spinner').removeClass("failure");
    if (type === "success") {
        $('.loading-message-img .spinner').addClass("success");
    }
    else if (type === "failure") {
        $('.loading-message-img .spinner').addClass("failure");
    }
    else if (type === "infoWait") {
        $('.loading-message').addClass("no-load");
    }
    else if (type === "infoSuccess") {
        $('.loading-message-img .spinner').addClass("success");
        $('.loading-message').addClass("no-load");
    }
    else if (type === "infoFailure") {
        $('.loading-message-img .spinner').addClass("failure");
        $('.loading-message').addClass("no-load");
    }
    if ($('.loading-message').hasClass("show")) {
        $('.loading-message .loading-message-info').html(msg);

    }
    else {
        $('.loading-message .loading-message-info').html(msg);
        $('.loading-message').addClass('show');
    }
    if (delay > 0) {
        setTimeout(function () {
            $(".loading-message-box").css("top", "-70px");
        }, (parseFloat(delay) - parseFloat(.5)) * 1000);
        setTimeout(function () {
            $('.loading-message').removeClass('show');
            $(".loading-message-box").removeAttr("style");
        }, delay * 1000);
    }
}
//window.addEventListener("keydown", function (e) {
//    if ((e.ctrlKey && e.keyCode === 70)) {
//        e.preventDefault();
//    }
//})