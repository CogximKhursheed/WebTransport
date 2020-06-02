                    


/* This javascript file contains some javascript functions 
    through which a developer can validate text boxes and input boxes very easily. */
    
/* A developer can append more functions according to the requirements. */

/* CODE START --------------*/

/* 1.) This function will not allow ' and ; in the text box. These 2 characters are mostly used 
    for sql injection */

/*function notAllowSpecialCharacters(e)
        {
      
            // Get the ASCII value of the key that the user entered
            var key = (document.all)?window.event.keyCode:e.which;
            alert(key);
            if ((key == 39) || (key == 59) || (key == 32))
                // If it was, then dispose the key and continue with entry
                return false;
            else
                // If it was, then allow the entry to continue
                return true; 
        }*/
        
        function notAllowSpecialCharacters(e) 
        {                        
            var keycode = (document.all)?window.event.keyCode:e.which;   
                             
            //Keys Allowed => HOME, END, DELETE, BACKSPACE, DOT, ALPHABETS, NUMBERS, TAB
            if((keycode >= 47 && keycode <= 57)||  (keycode >= 65 && keycode <= 90) || (keycode >= 97 && keycode <= 122) ||  (keycode == 8) || (keycode == 46) || (keycode == 36) || (keycode == 190) || (keycode == 9)) 
            {
                return true; 
            }
            else
            {
                return false; 
            }

            return true; 
        }
        
       function notAllowSpecialCharacters_Spaceallow(e) 
        {
            
            var keycode = (document.all) ? window.event.keyCode : e.which;
            
            //Keys Allowed => HOME, END, DELETE, BACKSPACE, DOT, ALPHABETS, NUMBERS, TAB,COMMA,And,dass
            if((keycode >= 47 && keycode <= 57)||  (keycode == 32) || (keycode >= 65 && keycode <= 90) || (keycode >= 97 && keycode <= 122) ||  (keycode == 8) || (keycode == 46) || (keycode == 36) || (keycode == 190) || (keycode == 9) || (keycode==0)|| (keycode==44) || (keycode==38)|| (keycode==45)) 
            {
                return true; 
            }
            else
            {
                return false; 
            }

            return true; 
        }


/* 2.) This function will allow only alphabetical characters and spaces. */
        
        function allowOnlyAlphabet(e)
        {
            // Get the ASCII value of the key that the user entered
            var key = (document.all)?window.event.keyCode:e.which;
                   
            if ((key >= 65 && key <= 90 ) || (key >= 97 && key<= 122) || (key == 8) || (key == 32)|| (key == 0))
                // If it was, then allow the entry to continue
                return true;
            else
                // If it was not, then dispose the key and continue with entry
                return false; 
        }
        
/* 3.) This function will allow only numeric values with no spaces */

        function allowOnlyNumber(evt)
        {
            // Get the ASCII value of the key that the user entered
            var charCode = (evt.which) ? evt.which : event.keyCode
           
                if ((charCode >= 48 && charCode <= 57 ) || (charCode == 8) || (charCode == 0))
                    // If it was, then allow the entry to continue
                    return true;
                else
                    // If it was not, then dispose the key and continue with entry
                    return false;      
        }
        
        /* 4.) This function will allow only numeric values with no spaces */

        function allowOnlyNumber_Allowcolon(evt)
        {
            // Get the ASCII value of the key that the user entered
            var charCode = (evt.which) ? evt.which : event.keyCode
           
                if ((charCode >= 48 && charCode <= 57 ) || (charCode == 8) || (charCode == 0) || (charCode == 58) || (charCode == 46))
                    // If it was, then allow the entry to continue and allow :
                    return true;
                else
                    // If it was not, then dispose the key and continue with entry
                    return false;      
        }
        
/* 5.) This function will allow only float/double values with no spaces */

        function allowOnlyFloatNumber(evt)
        {
            // Get the ASCII value of the key that the user entered
            
            var charCode = (evt.which) ? evt.which : evt.keyCode

            //if ((charCode >= 48 && charCode <= 57 ) || (charCode == 8)|| (charCode == 45) || (charCode == 46) || (charCode == 0)|| (charCode == 9))

            if ((charCode >= 48 && charCode <= 57) || (charCode == 8) || (charCode == 46) || (charCode == 0) || (charCode == 9))
                    // If it was, then allow the entry to continue
                    return true;
                else
                    // If it was not, then dispose the key and continue with entry
                    return false;  
        }
        
/* 6.) This function will allow alphabetical characters, numeric values and spaces. */
        
        function allowAlphabetAndNumer(e)
        {
            // Get the ASCII value of the key that the user entered
            var key = (document.all)?window.event.keyCode:e.which;
            
            if ((key >= 65 && key <= 90 ) || (key >= 97 && key<= 122) || (key >= 48 && key<= 57) || (key == 8) || (key == 32)|| (key == 0) || (key == 45))
                // If it was, then allow the entry to continue
                return true;
            else
                // If it was not, then dispose the key and continue with entry
                return false; 
        }
        
/* 7.) This function will not allow anything. 
    It will be used in the date text boxes where no input will be taken from the user. 
    Here the user will click on the calenadr control only */

        function notAllowAnything(e)
                {
                    // Get the ASCII value of the key that the user entered
                    var key = (document.all)?window.event.keyCode:e.which;
                   
                    if(key==0)
                     {return true;}
                    else
                    {
                       return false;
                       }
                }
                
        function AllowOnlyEnter()
        {
           var key = (document.all)?window.event.keyCode:e.which;
           if(key==13)
           { return true;}
           else
           { return false;}
        }          
        
/* 8.) This function will not allow ' and ; and space in the text box. These 2 characters are mostly used 
    for sql injection */

        function ntalw(e)
                {
                    
                    // Get the ASCII value of the key that the user entered
                    var key = (document.all)?window.event.keyCode:e.which;
                    //alert(key);
                    if ((key == 39) || (key == 32) || (key == 59))
                    {
                        // If it was, then dispose the key and continue with entry
                        return false;
                        }
                    else
                        // If it was, then allow the entry to continue
                        return true; 
                }

/* 9.) This function will allow alphabetical characters, numeric values with no spaces. */
        
        function allowAlphabetAndNumerN(e)
        {
        
            // Get the ASCII value of the key that the user entered
            var key = (document.all)?window.event.keyCode:e.which;
            
           
            if ((key >= 65 && key <= 90 ) || (key >= 97 && key<= 122) || (key >= 48 && key<= 57) || (key == 8) || (key == 0) || (key == 45))
                // If it was, then allow the entry to continue
                return true;
            else
                // If it was not, then dispose the key and continue with entry
                return false; 
        }
/* 10.) This function will allow alphabetical characters,float numeric values. */

        function allowAlphabetAndFloatNumber(e) {
            // Get the ASCII value  of the key that the user entered
            var key = (document.all) ? window.event.keyCode : e.which;

            if ((key >= 65 && key <= 90) || (key >= 97 && key <= 122) || (key >= 48 && key <= 57) || (key == 8) || (key == 32) || (key == 0) || (key == 45)
                || (key >= 48 && key <= 57) || (key == 8) || (key == 45) || (key == 46) || (key == 0) || (key == 9))
            // If it was, then allow the entry to continue
                return true;
            else
            // If it was not, then dispose the key and continue with entry
                return false;
        }



        /* 11.) This function will allow alphabetical characters, numeric values and spaces and Dot. */

        function allowAlphabetAndNumerAndDotAndSlash(e) {
            // Get the ASCII value of the key that the user entered
            var key = (document.all) ? window.event.keyCode : e.which;

            if ((key >= 65 && key <= 90) || (key >= 97 && key <= 122) || (key >= 48 && key <= 57) || (key == 8) || (key == 32) || (key == 0) || (key == 45) ||
                    (key == 47) || (key == 46))
            // If it was, then allow the entry to continue
                return true;
            else
            // If it was not, then dispose the key and continue with entry
                return false;
        }

      /* 12.) This function will allow numeric values. */
        function allowOnlyNumber(e)
        {
            // Get the ASCII value of the key that the user entered
            //var charCode = (evt.which) ? evt.which : event.keyCode
            var charCode = (document.all)?window.event.keyCode:e.which;
           
                if ((charCode >= 48 && charCode <= 57 ) || (charCode == 8) || (charCode == 0))
                    // If it was, then allow the entry to continue
                    return true;
                else
                    // If it was not, then dispose the key and continue with entry
                    return false;      
        }

        /*    This function is for Email. */

        function allowEmail(e) {
            // Get the ASCII value of the key that the user entered
            var key = (document.all) ? window.event.keyCode : e.which;

            if ((key >= 65 && key <= 90) || (key >= 97 && key <= 122) || (key >= 48 && key <= 57) || (key == 8) || (key == 32) || (key == 0) || (key == 45) || (key == 46) || (key == 64))
            // If it was, then allow the entry to continue
                return true;
            else
            // If it was not, then dispose the key and continue with entry
                return false;
        }
         
/* CODE END ---------------*/


/*----------------------------------------------*/
function showmsg(value)
{
    jAlert(value,"Lead Graph");
}




function AddDesignation(desig, stat, cid) {

    try {
        var errorMessage = "Sorry, Please try again later.";
        $.ajax(
    {
        type: "POST",
        url: "Json.aspx",
        dataType: "text",
        data: { designation: desig, status: stat, compidno: cid, action: "adddesignation" },
        success: function(response) {
            $("#PopupLoaderImage").fadeOut("fast"); // hiding the loading image
            $('#SpnMessage').empty();
            $("#SpnMessage").fadeIn("fast");
            //alert('desig' + response);
            if (response.indexOf("!") >= 0) { // failure
                $('#SpnMessage').empty();
                // alert('failure' + response);
            }
            else { // success
                if (response > 0) {
                    document.getElementById("ctl00_contentmaster_hiddesid").value = response;
                    AddItem(desig, response, "ctl00_contentmaster_ddlDesig");
                    ToggleShowHide("DivDesignation", "designation");
                }
                else {
                    $('#SpnMessage').text("Designation already exists");
                }
            }
        },
        failure: function(msg) {
            $("#PopupLoaderImage").fadeOut("fast"); // hiding the loading image
            $('#SpnMessage').empty();
        }
    });
    }
    catch (err) {
        $("#PopupLoaderImage").fadeOut("fast"); // hiding the loading image
        $('#SpnMessage').empty();
        alert(err);
        return false;
    }
}
function AddLeadSource(SorcName, stat, cid) {

    try {
        var errorMessage = "Sorry, Please try again later.";
        $.ajax(
    {
        type: "POST",
        url: "Json.aspx",
        data: { LeadSource: SorcName, status: stat, compidno: cid, action: "addLeadSource" },
        success: function(response) {
            $("#PopupLoaderImage").fadeOut("fast"); // hiding the loading image
            $('#SpnMessage').empty();
            $("#SpnMessage").fadeIn("fast");
            if (response.indexOf("!") >= 0) { // failure
                $('#SpnMessage').empty();
                // alert('failure' + response);
            }
            else { // success
                if (response > 0) {
                    AddItem(SorcName, response, "ctl00_contentmaster_ddlLeadSource");
                    ToggleShowHide("DivLeadSource", "LeadSource");
                }
                else {
                    $('#SpnMessage').text("Lead Source already exists");
                }
            }
        },
        failure: function(msg) {
            $("#PopupLoaderImage").fadeOut("fast"); // hiding the loading image
            $('#SpnMessage').empty();
        }
    });
    }
    catch (err) {
        $("#PopupLoaderImage").fadeOut("fast"); // hiding the loading image
        $('#SpnMessage').empty();
        alert(err);
        return false;
    }
}

function AddLocation(LocName, stat, cid) {
//    alert(LocName + "   " + stat + "   " + cid);
    try {
        var errorMessage = "Sorry, Please try again later.";
        $.ajax(
    {
        type: "POST",
        url: "Json.aspx",
        dataType:"text",
        data: { Location: LocName, status: stat, compidno: cid, action: "addLocation" },
        success: function(response) {
            $("#PopupLoaderImage").fadeOut("fast"); // hiding the loading image
            $('#SpnMessageLoc').empty();
            $("#SpnMessageLoc").fadeIn("fast");
            //            if (response.indexOf("!") >= 0) { // failure
            //                $('#SpnMessageLoc').empty();
            //                // alert('failure' + response);
            //            }
            //            else { // success
           // alert(response);
            if (response > 0) {
                document.getElementById("ctl00_contentmaster_hidlocid").value = response;
                AddItem(LocName, response, "ctl00_contentmaster_ddlLocation");
                ToggleShowHide("DivLocation", "Location");
            }
            else {
                $('#SpnMessageLoc').text("Location already exists.");
            }
            //}
        },
        failure: function(msg) {
            $("#PopupLoaderImage").fadeOut("fast"); // hiding the loading image
            $('#SpnMessageLoc').empty();
        }
    });
    }
    catch (err) {
        $("#PopupLoaderImage").fadeOut("fast"); // hiding the loading image
        $('#SpnMessageLoc').empty();
        alert(err);
        return false;
    }
}


function AddOccupation(OcupName, stat, cid) {

    try {
        var errorMessage = "Sorry, Please try again later.";
        $.ajax(
    {
        type: "POST",
        url: "Json.aspx",
        data: { Occupation: OcupName, status: stat, compidno: cid, action: "addOccupation" },
        success: function(response) {
            $("#PopupLoaderImage").fadeOut("fast"); // hiding the loading image
            $('#SpnMessageOcup').empty();
            $("#SpnMessageOcup").fadeIn("fast");
            if (response.indexOf("!") >= 0) { // failure
                $('#SpnMessageOcup').empty();
                // alert('failure' + response);
            }
            else { // success
                if (response > 0) {
                    document.getElementById("ctl00_contentmaster_hidocupid").value = response; 
                    AddItem(OcupName, response, "ctl00_contentmaster_ddlOccupation");
                    ToggleShowHide("DivOccupation", "Occupation");
                }
                else {
                    $('#SpnMessageOcup').text("Occupation already exists.");
                }
            }
        },
        failure: function(msg) {
            $("#PopupLoaderImage").fadeOut("fast"); // hiding the loading image
            $('#SpnMessageOcup').empty();
        }
    });
    }
    catch (err) {
        $("#PopupLoaderImage").fadeOut("fast"); // hiding the loading image
        $('#SpnMessageOcup').empty();
        alert(err);
        return false;
    }
}


/* This function is used to show / hide pop up div's on Cases.aspx */
function ToggleShowHide(name, type) 
{
    // Local variables
    var isNew = 0;
    if(type=="client")
    {
       $("#SpnMessageClient").empty();
       var clientvalue=$('#ctl00_contentmaster_ddlclient').val();
       if(clientvalue=="-1")
       {
         isNew=1;
       }
    }
    if(type=="city")
    {
       $("#SpnMessageCity").empty();
       var cityvalue=$('#ctl00_contentmaster_ddlState').val();
       if(cityvalue=="-1")
       {
         isNew=1;
       }
    }
    if(isNew==1)
    {
       $("#dvClient").fadeIn(300);
    }
  
    return false;
}

function AddCity(cityname,stateid)
{
   try 
   {
     var errorMessage = "Sorry, Please try again later.";
    $.ajax(
     {
        type: "POST",
        url: "Json.aspx",
        data: { City: cityName, state: stateid,action: "addcity" },
        success: function(response) {
            $("#PopupLoaderImageCity").fadeOut("fast"); // hiding the loading image
            $('#SpnMessageCity').empty();
            $("#SpnMessageCity").fadeIn("fast");
            if (response.indexOf("!") >= 0) { // failure
                $('#SpnMessageCity').empty();
            }
            else { // success
                if (response > 0) {
                    $('#<%=hidcityid.ClientID%>').val(response);
                    AddItem(OcupName, response, "ctl00_contentmaster_ddlCity");
                    ToggleShowHide("dvcity", "city");
                }
                else {
                    $('#SpnMessageCity').text("City already exists.");
                }
            }
        },
        failure: function(msg) {
            $("#PopupLoaderImageCity").fadeOut("fast"); // hiding the loading image
            $('#SpnMessageCity').empty();
        }
    });
    }
    catch (err) {
        $("#PopupLoaderImage").fadeOut("fast"); // hiding the loading image
        $('#SpnMessageOcup').empty();
        alert(err);
        return false;
    }
}


// This function is used to Add a new item in Drop Down List dynamically
function AddItem(Text, Value, control) {
    // Create an Option object        
    var opt = document.createElement("option");
    var obj = document.getElementById(control); // Drop Down control object

    // Assign text and value to Option object
    opt.text = Text;
    opt.value = Value;
    
    // Add an Option object to Drop Down/List Box
    obj.options.add(opt);
    

    obj.selectedIndex = obj.options.length - 1; // focusing again to new value added in drop down control
    obj.options[obj.options.length - 1].selected=true;
    obj.value=Value;
    
    //$(obj).addClass("focus");
  
}


function CenterDiv(divname) {
    $('#' + divname).css('position', 'absolute');
    $('#' + divname).css("left", ($(window).width() / 2 - $('#' + divname).width() / 2) + "px");
    $('#'+divname).css("top", ($(window).height()/2-$('#'+divname).height()/2) + "px");
//    $('#' + divname).css("top", (($(window).height() - $('#' + divname).outerHeight()) / 2) + 
//                                                $(window).scrollTop() + "px");
//    $('#' + divname).css("left", (($(window).width() - $('#' + divname).outerWidth()) / 2) + 
//                                                $(window).scrollLeft() + "px");

    //resize(divname);
}

function CenterDiv1(divname) {
    $('#' + divname).css('position', 'absolute');
    $('#' + divname).css("left", ($(window).width() / 2 - $('#' + divname).width() / 2)-150 + "px");
    $('#'+divname).css("top", ($(window).height()/2-$('#'+divname).height()/2) + "px");
   // resize(divname);
}

//function CenterDiv(divname) {
//    var winWidth=$(window).width();
//    var winHeight=$(window).height();
//    var windowCenter=winWidth/2;
//    var itemCenter=$(theItem).width()/2;
//    var theCenter=windowCenter-itemCenter;
//    var windowMiddle=winHeight/2;
//    var itemMiddle=$(theItem).height()/2;
//    var theMiddle=windowMiddle-itemMiddle;
//    if(winWidth>$(theItem).width()){ //horizontal
//        $(theItem).css('left',theCenter);
//    } else {
//        $(theItem).css('left','0');
//    }
//    if(winHeight>$(theItem).height()){ //vertical
//        $(theItem).css('top',theMiddle);
//    } else {
//        $(theItem).css('top','0');
//    }
//}

function resize(divname)
{
$(window).resize(function() {
    $('#' + divname).dialog("option", "position", "center");
});
}




/*get country code*/
function GetCountryCode(countryId,ctrl) {

    try {
        var errorMessage = "Sorry, Please try again later.";
        $.ajax(
    {
        type: "POST",
        url: "Json.aspx",
        dataType: "text",
        data: { countryId: countryId, action: "getCountryCode" },
        success: function(response) {
          
            if (response.indexOf("!") >= 0) { // failure

            }
            else { // success

                document.getElementById("txtCode").value = response;
            }
        },
        failure: function(msg) {

        }
    });
    }
    catch (err) {
        alert(err);
        return false;
    }
}



/*------------------------*/
 function SetFocus() {
            $('input[type="text"]').focus(function() {
                $(this).addClass("focus");
            });
            $('input[type="text"]').blur(function() {
                $(this).removeClass("focus");
            });
            $("select").focus(function() {
                $(this).addClass("focus");
            });
            $("select").blur(function() {
                $(this).removeClass("focus");
            });
             $("textarea").blur(function() {
                $(this).removeClass("focus");
            });
             $("textarea").focus(function() {
                $(this).addClass("focus");
            });
        }
        
/*------------------------------*/
function textCounter(e, field, countfield, maxlimit) {
    var txtid = document.getElementById(field);
    var lblid = document.getElementById(countfield);
    if (txtid.value.length > maxlimit) // if too long...trim it!
    {
        txtid.value = txtid.value.substring(0, maxlimit);
    }
    else {

    }
}

function CalculateChars(Obj, obj1, maxchar) {

    var txtCountChar = document.getElementById(obj1);
    var max = parseInt(maxchar);
    var count = document.getElementById(Obj).value.length;
    var left = max - count;
    // alert(left+'  '+txtCountChar.value );
    if (left >= 0) {
        newvalue = Obj.value;
        txtCountChar.innerText = left + " characters left";
        txtCountChar.textContent = left + " characters left";
    }
    else {

        Obj.value = newvalue;
    }
}

//To allow float,dot,left and right arrow
function validateQty(event) {
    var key = window.event ? event.keyCode : event.which;
    if (event.keyCode == 8 || event.keyCode == 37 || event.keyCode == 39) {
        return true;
    }
    else if (key < 46 || key > 57) {
        return false;
    }
    else if (key == 47)
    { return false; }
    else return true;
}

function checkfloat(evt, obj) {
    var dp = 0;
    var charCode = (evt.which) ? evt.which : evt.keyCode
    if ((charCode >= 48 && charCode <= 57) || (charCode == 8) || (charCode == 45) || (charCode == 46) || (charCode == 0) || (charCode == 9) || (charCode == 37) || (charCode == 39)) {
        if ((dp = obj.value.indexOf(".")) > -1) {
            if (charCode == 46)
                return false;  // only one allowed  
            else {
                //                // room for more after decimal point?
                return true;
            }

        }
        return true;
    }
    else
    // If it was not, then dispose the key and continue with entry
        return false;
}

