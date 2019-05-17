function generatePastel() {
    var c = hsvToRgb(Math.random(), 0.3, 1);
    var color = 'rgb(' + c[0] + ',' + c[1] + ',' + c[2] + ')';

    return color;
}

function hsvToRgb(h, s, v) {
    var r, g, b;

    var i = Math.floor(h * 6);
    var f = h * 6 - i;
    var p = v * (1 - s);
    var q = v * (1 - f * s);
    var t = v * (1 - (1 - f) * s);

    switch (i % 6) {
        case 0: r = v, g = t, b = p; break;
        case 1: r = q, g = v, b = p; break;
        case 2: r = p, g = v, b = t; break;
        case 3: r = p, g = q, b = v; break;
        case 4: r = t, g = p, b = v; break;
        case 5: r = v, g = p, b = q; break;
    }

    return [r * 255, g * 255, b * 255];
}



var host = window.location.origin.replace("http", "ws");
var socket = new WebSocket("ws://127.0.0.1:80");

socket.onopen = function (openEvent) {
    console.log("Socket connection is open.");
    sendTextMessage();
    getAllCompetitions();
};

socket.onmessage = function (e) {
    //console.log("Recieved: " + JSON.parse(e.data));
    //"[{\"ID\":1,\"Name\":\"Första Tävlingen\",\"Start\":\"2019-04-15T21:51:24\",\"Finished\":\"0001-01-01T00:00:00\""

    str = "";
    for (var i = 0; i < e.data.length; i += 8) {
        val = e.data[i + 4] + e.data[i + 6] + e.data[i] + e.data[i + 2] + "";
        str += String.fromCharCode(parseInt(val, 16));
    }
    //console.log(str);
    var obj = JSON.parse(str);
    decodeMessage(obj);
};

socket.onerror = function (err) {
    console.error(err);
};

function switchWindow(window) {

    var switchTo = document.getElementById(window);

    var open = document.getElementsByClassName("center_holder");
    for (var i = 0; i < open.length; i++) {
        if (open[i].classList.contains("active")) {
            open[i].classList.remove("active");
            open[i].classList.add("hidden");
        }
    }
    switchTo.classList.remove("hidden");
    switchTo.classList.add("active");
}

function viewCompetition(data) {
    console.log(data);
    switchWindow("single_competition_holder");

    document.getElementsByClassName("title")[0].innerHTML = data.Comp.Name;




    var ghost = document.getElementsByClassName("jump_holder")[0];
    var dest = document.getElementById("single_competition_holder");
    var ghost_item = ghost.getElementsByClassName("jump_data_holder")[0];

    ghost.style.columnCount = data.Comp.Jumps + 1;

    var clone;
    for (var i = 0; i < data.Comp.Jumps; i++) {
        clone = ghost_item.cloneNode(true);
        clone.innerHTML = "Jump " + i + 1;
        ghost.appendChild(clone);
    }

    for (var j = 0; j < data.Comp.Users.length; j++) {
        clone = ghost.cloneNode(true);

        clone.style.padding = "5px 0 5px 0";
        clone.style.borderTop = "1px solid black";
        clone.style.borderLeft = "1px solid black";
        clone.style.borderRight = "1px solid black";
        if (j == data.Comp.Users.length - 1) {
            clone.style.borderBottom = "1px solid black";
        }


        clone.getElementsByClassName("jump_data_holder")[0].innerHTML = data.Comp.Users[j].Name;
        for (var k = 0; k < data.Comp.Jumps; k++) {
            //clone.getElementsByClassName("jump_data_holder")[k + 1].innerHTML = getScoreByID(data.Comp.Users[j].ID, k , data.Results, data.Comp.Jumps);
            //vi behöver hoppnummer också
        }
        dest.appendChild(clone);
    }
}

//function getScoreByID(userID, jump, results, jumps) {
//    var scores;

//    for (var i = 0; i < jumps.length; i++) {
//        if (jumps[i].CUID == userID) {

//        }
//    }
//}

function generateCompetitions(num, data) {
    switchWindow("overview_competition_holder");

    var ghost = document.getElementsByClassName("comp_item")[0];
    var dest = document.getElementById("overview_competition_holder");
    for (var i = 0; i < num; i++) {
        var clone = ghost.cloneNode(true);
        clone.classList.remove("hidden");
        var t = clone.children[0];
        t.innerHTML = data[i].Name;

        if (i % 2 === 0) {
            clone.onmouseleave = function () { this.style.backgroundColor = "rgb(225, 250, 255)"; };
            clone.style.backgroundColor = "rgb(225, 250, 255)";
        } else {
            clone.onmouseleave = function () { this.style.backgroundColor = "rgb(193, 236, 245)"; };
            clone.style.backgroundColor = "rgb(193, 236, 245)";
        }
        clone.onmouseover = function () { this.style.backgroundColor = "#C1F5D0" };


        clone.value = data[i].ID;
        clone.onmousedown = function () { sendTextMessage("GET COMPETITION\r\n" + this.value); };

        dest.appendChild(clone);
    }
}

function hexDecode(hex) {
    var j;
    var hexes = hex.match(/.{1,8}/g) || [];
    var back = "";
    for (j = 0; j < hexes.length; j++) {
        back += String.fromCharCode(parseInt(hexes[j], 16));
    }

    return back;
}

function unicodeToChar(text) {
    return text.replace(/[\dA-F]{4}/gi,
        function (match) {
            return String.fromCharCode(parseInt(match.replace(/\\u/g, ''), 16));
        });
}

function hexToAscii(hexx) {
    var hex = hexx.toString();//force conversion
    var str = '';
    for (var i = 0; (i < hex.length && hex.substr(i, 2) !== '00'); i += 2)
        str += String.fromCharCode(parseInt(hex.substr(i, 2), 16));
    return str;
}

function retardToHex(msg) {
    msg = msg.replace(/(..)/g, '$1-').slice(0, -1);
    return msg;
    //(0011000) (ff) (a)
}

function decodeMessage(messageObj) {


    switch (messageObj.Type) {
        case "CompetitionWithUser":
            generateCompetitions(messageObj.Num, messageObj.Data);
            break;
        case "SingleCompetition":
            viewCompetition(messageObj.Data);
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