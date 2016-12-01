function saveToDatabase()
{






   
    simpleCart.each(function (item, x) {
        alert("in the each loop")
           alert( PageMethods.insert("", onSuccess, onFailure));
    });
    
    
}

function loginalert() {
    alert("Redirecting to login page.");
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
    alert(resultstring);
}

function onFailure(resultString) {
    alert("failed at page methods");
    alert(resultString)
}