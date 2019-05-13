//var c = hsvToRgb(Math.random(), 0.3, 1);
//temp.color = 'rgb(' + c[0] + ',' + c[1] + ',' + c[2] + ')';
//temp.x = i; //is this required?
//temp.y = j;
//BLOCKS[i][j] = temp;
//        }
//    }
//}

//function hsvToRgb(h, s, v) {
//    var r, g, b;

//    var i = Math.floor(h * 6);
//    var f = h * 6 - i;
//    var p = v * (1 - s);
//    var q = v * (1 - f * s);
//    var t = v * (1 - (1 - f) * s);

//    switch (i % 6) {
//        case 0: r = v, g = t, b = p; break;
//        case 1: r = q, g = v, b = p; break;
//        case 2: r = p, g = v, b = t; break;
//        case 3: r = p, g = q, b = v; break;
//        case 4: r = t, g = p, b = v; break;
//        case 5: r = v, g = p, b = q; break;
//    }

//    return [r * 255, g * 255, b * 255];
//}



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
    var parsed = e.data.replace(/\\"/g, '"');
    parsed = parsed.replace('"[', '[');
    //parsed = parsed.replace(']"', ']');
    //parsed += '}';
    console.log(parsed);
    var obj = JSON.parse(parsed);
    console.log(obj);


    //Object { Type: 0, Num: 1, Data: "[{\"ID\":1,\"Name\":\"Första Tävlingen\",\"Start\":\"2019-04-15T21:51:24\",\"Finished\":\"0001-011T00:00:00\"}]" }

    decodeMessage(obj);

};

function generateCompetitions(num, data) {
    var ghost = document.getElementsByClassName("comp_item")[0];
    var dest = document.getElementsByClassName("center_holder")[0];
    for (var i = 0; i < num; i++) {
        var clone = ghost.cloneNode(true);
        clone.classList.remove("hidden");
        var t = clone.children[0];
        t.innerHTML = data[i].Name;

        dest.appendChild(clone);
    }
}

function decodeMessage(messageObj) {


    switch (messageObj.Type) {
        case 0:
            generateCompetitions(messageObj.Num, messageObj.Data);
            break;

        default:
            break;
    }
}

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