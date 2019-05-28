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
var nIntervId;
var watchID = -1;

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

function clearCompetition() {
    var main = document.getElementById("single_competition_holder");
    var firstChilds = main.getElementsByClassName("jump_holder");
    for (var j = firstChilds.length - 1; j > 0; j--) {

        main.removeChild(firstChilds[j]);
    }
    var firstChildsInner = firstChilds[0].getElementsByClassName("jump_data_holder");
    for (var k = firstChildsInner.length - 1; k > 0; k--) {
        firstChildsInner[k].parentNode.removeChild(firstChildsInner[k]);
    }
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
        clone.innerHTML = "Jump " + (parseInt(i) + 1);
        ghost.appendChild(clone);
    }

    for (var j = 0; j < data.Comp.Users.length; j++) {
        clone = ghost.cloneNode(true);

        clone.style.padding = "5px 0 5px 0";
        clone.style.borderTop = "1px solid black";
        clone.style.borderLeft = "1px solid black";
        clone.style.borderRight = "1px solid black";
        if (j === data.Comp.Users.length - 1) {
            clone.style.borderBottom = "1px solid black";
        }


        clone.getElementsByClassName("jump_data_holder")[0].innerHTML = data.Comp.Users[j].Name;
        var B = 0;
        for (var k = 0; k < data.Jumps.length; k++) {
            if (data.Jumps[k].CUID === data.Comp.Users[j].ID) {
                clone.getElementsByClassName("jump_data_holder")[k + 1].innerHTML = data.Jumps[k].Name;
                clone.getElementsByClassName("jump_data_holder")[k + 1].style.breakInside = "avoid";
                B++;

                var result = 0;
                switch (data.Jumps[k].Results.length) {
                    case 1:
                        result += data.Jumps[k].Results[0].Score;
                        break;
                    case 2:
                        result += data.Jumps[k].Results[0].Score;
                        result += data.Jumps[k].Results[1].Score;
                        result = result / 2;
                        break;
                    case 3:
                        //result += data.Jumps[k].Result[0].Score;
                        result += data.Jumps[k].Results[1].Score;
                        //result += data.Jumps[k].Result[2].Score;
                        break;
                    case 4:
                        //result += data.Jumps[k].Result[0].Score;
                        result += data.Jumps[k].Results[1].Score;
                        result += data.Jumps[k].Results[2].Score;
                        //result += data.Jumps[k].Result[3].Score;
                        result = result / 2;
                        break;
                    case 5:
                        //result += data.Jumps[k].Result[0].Score;
                        result += data.Jumps[k].Results[1].Score;
                        result += data.Jumps[k].Results[2].Score;
                        result += data.Jumps[k].Results[3].Score;
                        //result += data.Jumps[k].Result[4].Score;
                        result = result / 3;
                        break;
                    case 6:
                        //result += data.Jumps[k].Result[0].Score;
                        result += data.Jumps[k].Results[1].Score;
                        result += data.Jumps[k].Results[2].Score;
                        result += data.Jumps[k].Results[3].Score;
                        result += data.Jumps[k].Results[4].Score;
                        //result += data.Jumps[k].Result[5].Score;
                        result = result / 4;
                        break;
                    case 7:
                        //result += data.Jumps[k].Result[0].Score;
                        //result += data.Jumps[k].Result[1].Score;
                        result += data.Jumps[k].Results[2].Score;
                        result += data.Jumps[k].Results[3].Score;
                        result += data.Jumps[k].Results[4].Score;
                        //result += data.Jumps[k].Result[5].Score;
                        //result += data.Jumps[k].Result[6].Score;
                        result = result / 3;
                        break;
                    case 8:
                        //result += data.Jumps[k].Result[0].Score;
                        //result += data.Jumps[k].Result[1].Score;
                        result += data.Jumps[k].Results[2].Score;
                        result += data.Jumps[k].Results[3].Score;
                        result += data.Jumps[k].Results[4].Score;
                        result += data.Jumps[k].Results[5].Score;
                        //result += data.Jumps[k].Result[6].Score;
                        //result += data.Jumps[k].Result[7].Score;
                        result = result / 4;
                        break;
                    case 9:
                        //result += data.Jumps[k].Result[0].Score;
                        //result += data.Jumps[k].Result[1].Score;
                        result += data.Jumps[k].Results[2].Score;
                        result += data.Jumps[k].Results[3].Score;
                        result += data.Jumps[k].Results[4].Score;
                        result += data.Jumps[k].Results[5].Score;
                        result += data.Jumps[k].Results[6].Score;
                        //result += data.Jumps[k].Result[7].Score;
                        //result += data.Jumps[k].Result[8].Score;
                        result = result / 5;
                        break;
                    case 10:
                        //result += data.Jumps[k].Result[0].Score;
                        //result += data.Jumps[k].Result[1].Score;
                        result += data.Jumps[k].Results[2].Score;
                        result += data.Jumps[k].Results[3].Score;
                        result += data.Jumps[k].Results[4].Score;
                        result += data.Jumps[k].Results[5].Score;
                        result += data.Jumps[k].Results[6].Score;
                        result += data.Jumps[k].Results[7].Score;
                        //result += data.Jumps[k].Result[8].Score;
                        //result += data.Jumps[k].Result[9].Score;
                        result = result / 6;
                        break;

                    default:
                }

                clone.getElementsByClassName("jump_data_holder")[k + 1].innerHTML = data.Jumps[k].Name + " [" + result + "]";

            }
        }
        dest.appendChild(clone);
    }
}

function watchComp() {
    if (watchID !== -1) {
        sendTextMessage("GET COMPETITION\r\n" + watchID);
        console.log('Still watching : ' + watchID);
    }
}

function watch(ID) {
    watchID = ID;
    nIntervId = setInterval(watchComp, 5000);
}
function stopWatching() {
    watchID = -1;
    switchWindow("overview_competition_holder");
}

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

        clone.onmousedown = function () { watch(this.value), sendTextMessage("GET COMPETITION\r\n" + this.value); };

        dest.appendChild(clone);
    }
}

document.getElementsByClassName("backButton")[0].onmousedown = function () { stopWatching() };


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
            clearCompetition();
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