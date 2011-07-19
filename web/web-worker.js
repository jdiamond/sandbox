onmessage = function(e) {
    postMessage('you said: ' + e.data);
};

var counter = 1;

setInterval(function() {
    postMessage(counter);
    counter += 1;
}, 1000);
