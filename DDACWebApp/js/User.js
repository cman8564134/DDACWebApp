function submitform(x, y) {
    var d = new Date();
    var d = document.getElementById(x).value;

    var n = d.toString();
    document.getElementById(y).value = n;
    simpleCart.bind('beforeAdd', function (item) {
        if (!item.get('date')) {

            return false; // prevents item from being added to cart
        }
    });
    if (!(document.getElementById(y).value)) {
        alert("The date have not been set");
    }
    document.getElementById(x).value = ""
}
