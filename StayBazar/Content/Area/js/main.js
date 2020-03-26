function isINLetters(event) {
    var charCode = (event.which) ? event.which : event.charCode;
    if ((charCode >= 48) && (charCode <= 57) || (charCode >= 58 && charCode <= 64)|| (charCode >= 33 && charCode <= 43) || (charCode >= 91 && charCode <= 96) || (charCode >= 123 && charCode <= 126)) {
        return false;
    }
    return true;
}

function isINNumbers(event) {
    var charCode = (event.which) ? event.which : event.charCode;
    if ((charCode >= 90) && (charCode <= 126) || (charCode >= 58) && (charCode <= 90) || (charCode >= 33 && charCode <= 42)) {
        return false;
    }
    return true;
}

function isINNumbersletters(event) {
    var charCode = (event.which) ? event.which : event.charCode;
    if ((charCode >= 33 && charCode <= 42) || (charCode >= 58 && charCode <= 64)|| (charCode >= 43 && charCode <= 47) || charCode == 59 || charCode == 61 || (charCode >= 91 && charCode <= 96) || (charCode >= 123 && charCode <= 126)) {
        return false;
    }
    return true;
}

function validateRq()
{
    $.get("/Common/IsValid",function(data)
    {
        if(data != "true")
        {
            window.location = "/Account/Login";
        }
    })
}