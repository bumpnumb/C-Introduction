



var host = window.location.origin.replace("http", "ws");
var socket = new WebSocket("ws://127.0.0.1:80");

socket.onopen = function (openEvent) {
    console.log("Socket connection is open.");
    sendTextMessage();
};

function sendTextMessage() {
    if (socket.readyState !== WebSocket.OPEN) {
        console.log("Socket is not open for connection.");
        return;
    }
    socket.send("Hello");
}

window.onbeforeunload = function () {
    socket.onclose = function () { }; // disable onclose handler first
    socket.send("Exit<00>");
    socket.close();
};