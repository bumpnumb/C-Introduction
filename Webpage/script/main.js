


// Create WebSocket connection.
const socket = new WebSocket('ws://localhost:80/service');

// Connection opened
socket.addEventListener('open', function (event) {
    socket.send('Hello Server!');
});

// Listen for messages
socket.addEventListener('message', function (event) {
    console.log('Message from server ', event.data);
});

//exampleSocket.onopen = function (event) {
//    exampleSocket.send("Here's some text that the server is urgently awaiting!");
//};

//// Send text to all users through the server
//function sendText() {
//    // Construct a msg object containing the data the server needs to process the message from the chat client.
//    var msg = {
//        type: "message",
//        text: document.getElementById("text").value,
//        id: clientID,
//        date: Date.now()
//    };

//    // Send the msg object as a JSON-formatted string.
//    exampleSocket.send(JSON.stringify(msg));

//    // Blank the text input element, ready to receive the next line of text from the user.
//    document.getElementById("text").value = "";

//}

//    exampleSocket.onmessage = function (event) {
//        console.log(event.data);
//}

//exampleSocket.close();