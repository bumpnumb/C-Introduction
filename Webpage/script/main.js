



var host = window.location.origin.replace("http", "ws");
var socket = new WebSocket("ws://127.0.0.1:80");

socket.onopen = function (openEvent) {
    console.log("Socket connection is open.");
    sendTextMessage();
};

socket.onmessage = function (e) {
    //console.log("Recieved: " + JSON.parse(e.data));
    //"[{\"ID\":1,\"Name\":\"Första Tävlingen\",\"Start\":\"2019-04-15T21:51:24\",\"Finished\":\"0001-01-01T00:00:00\""

    console.log(e.data);

    //var obj = JSON.parse(e.data);

};

function getAllCompetitions() {
    sendTextMessage("GET ALL COMPETITIONS");
}


function sendTextMessage(message) {
    if (socket.readyState !== WebSocket.OPEN) {
        console.log("Socket is not open for connection.");
        return;
    }
    socket.send(message);
    console.log("Sent: " + message);
}

window.onbeforeunload = function () {
    socket.onclose = function () { }; // disable onclose handler first
    socket.send("Exit<00>");
    socket.close();
};