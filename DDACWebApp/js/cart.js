function saveToDatabase()
    {
        simpleCart.bind('beforeCheckout', function (data) {
            alert("coming to it");
            PageMethods.insert("", onSuccess, onFailure);
            return true;
        });
    

}
function tryAtMost( maxRetries, promise) {

                promise = promise || new Promise();
                // try to insert into the database
                //can check from server explorer
               

                if (success) {
                    promise.resolve(result);

                    return true;
                } else if (maxRetries > 0) {
                    // Try again if we haven't reached maxRetries yet
                    setTimeout(function () {
                        tryAtMost( maxRetries - 1, promise);
                    }, retryInterval);
                } else {
                    promise.reject(error);
                    return false;
                }
            }

            
function onSuccess(resultstring) {
    alert("success at page methods");
    alert(resultstring)
}

function onFailure() {
    alert("failed at page methods");
}