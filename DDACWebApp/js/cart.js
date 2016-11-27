function saveToDatabase()
{
    simpleCart.bind('beforeCheckout', function (data) {
        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                document.getElementById("txtHint").innerHTML = this.responseText;
            }
        };
        xhttp.open("GET", "gethint.asp?q=" + str, true);
        xhttp.send();
        
        simpleCart.each(function (item) {
            alert(item.get('name'));
        });
        
    }
    );

}

function showHint(str) {
    var xhttp;
    if (str.length == 0) {
        document.getElementById("txtHint").innerHTML = "";
        return;
    }
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            document.getElementById("txtHint").innerHTML = this.responseText;
        }
    };
    xhttp.open("GET", "gethint.asp?q=" + str, true);
    xhttp.send();
}