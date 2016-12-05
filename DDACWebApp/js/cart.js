function saveToDatabase() {
    simpleCart.bind( 'beforeCheckout' , function( data ){
    var i = 0;
    var array = [];
        
    simpleCart.each(function (item) {

        array[i] = item.get('name') + "|" + item.get("price") + "|" + item.get("quantity") + "|" + item.get("date");
        i++;
    });
    alert(array);

    XMLHttpRequest.prototype.original_open = XMLHttpRequest.prototype.open;

    XMLHttpRequest.prototype.open = function (method, url, async, user, password) 
    {

        async = false;

        var eventArgs = Array.prototype.slice.call(arguments);

        return this.original_open.apply(this, eventArgs);
    }
    
    PageMethods.insert(array, onSuccess, onFailure);
    return true;
    });
}


function loginalert() {
    alert("Redirecting to login page.");
}


            
function onSuccess(resultstring) {
    alert("Your data is saved in the database");
    for (var i = 0, len = resultstring.length; i < len; i++) {
        arr[i]="Your Order ID Is:"+arr[i]
    }
    alert(resultstring);
}

function onFailure(resultString) {
    alert("failed at page methods");
    alert(resultString);
}