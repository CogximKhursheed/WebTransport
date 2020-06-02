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



