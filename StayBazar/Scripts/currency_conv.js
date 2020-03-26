/**
        Currency converting 
        "http://www.freecurrencyconverterapi.com/api/convert?q=INR-USD&compact=y";
        {"INR-USD":{"val":0.0163}}
        **/
var cx_global_curtype = -1;
var cx_global_old_curtype = -1;

function cx_LoadCurrency() {
    $(".cxcurele").each(function () {
        $(this).data("cxcurval", $(this).html());
    });
    $(".cxcurinput").each(function () {
        $(this).data("cxcurval", $(this).val());
    });
    $(".cxcurrateele").each(function () {

        $(this).data("cxcurval", $(this).val());
    });
}

function cx_ChangeCurRate(curval) {
    if (curarray.length == 0) {
        //use default rate here
        cx_defaultRate();
        return;
    }
    var found = false;
    for (i = 0; i < curarray.length; i++) {
        if (curarray[i][0] == curval) {
            cx_ChangeCurrency(i, curarray[i][1], parseFloat(curarray[i][2]));
            found = true;
            break;
        }
    }
    if (!found) {
        // use default
        cx_defaultRate();
    }
}
function cx_ChangeCurrency(cx_id, cx_class, cx_rate) {
    cx_global_old_curtype = cx_global_curtype;
    cx_global_curtype = cx_id;
    ChgCurrencies(cx_rate);
    cx_ChgSymbol(cx_class);
}
function ChgCurrencies(cxval) {

    $(".cxcurele").each(function () {
        var val = $(this).data("cxcurval");
        val = Math.round(val * cxval);
        $(this).html(val);
    });
    $(".cxcurinput").each(function () {
        var val = $(this).data("cxcurval");
        val = Math.round(val * cxval);
        $(this).val(val);
    });
}
function cx_ChgSymbol(newSymbol) {
    var csschg = "fa-rupee";
    if (cx_global_old_curtype>= 0 && cx_global_old_curtype < curarray.length) {
        csschg = curarray[cx_global_old_curtype][1];
    }

    // if (cx_global_curtype != cx_global_old_curtype)
    $(".cxprice").removeClass(csschg);
    $(".cxprice").addClass(newSymbol);

}
function cx_defaultRate()
{
    cx_global_old_curtype = -1;
    cx_global_curtype = -1;
    ChgCurrencies(1);
    cx_ChgSymbol("fa-rupee");
}
function cx_reloadRates()
{
    $(".cxcurele").each(function () {
        if(!$(this).data("cxcurval")) $(this).data("cxcurval", $(this).html());
    });
}
function cx_removeDefault()
{
    $(".cxprice").removeClass('fa-rupee');
}
function cx_changeAllRates()
{
    var v = $("#curPick").val();
    cx_ChangeCurRate(v);
}
