function autoScroll() {
    var objDiv = document.getElementById("scrollbox");
    objDiv.scrollTop = objDiv.scrollHeight;
}

function make_paragraph(text) {
    var objP = document.getElementsByTagName("p")[0];
    objP.innerHTML = text;
}

function make_heading(tag, text) {
    var heading = "<" + tag + ">" + text + "</" + tag + ">";
    return heading;
}

function CalculateSum(arr) {
    var sum = 0;
    while (arr.length > 0) {
        sum += arr.pop();
    }
    return sum;
}

function GenerateListOfNumbers(start, end) {
    var text = "";
    for (let i = start; i <= end; i++) {
        text += i + "<br />";
    }
    return text;
}
function DrawRedBox() {
    var canvas = document.getElementById("myCanvas");
    var ctx = canvas.getContext("2d");
    ctx.fillStyle = "#FF0000";
    ctx.fillRect(0, 0, 150, 150);
}

function DrawRedCircle() {
    var canvas = document.getElementById("myCircle");
    var ctx = canvas.getContext("2d");
    ctx.strokeStyle = "#FF0000";
    ctx.arc(75, 75, 35, 0, Math.PI * 2);
    ctx.stroke();
}

function DrawTwoBoxes() {
    let canvas = document.getElementById("myTwoBoxes");
    let ctx = canvas.getContext("2d");
    ctx.fillStyle = "#FF0000";
    ctx.fillRect(0, 0, 100, 100);
    ctx.globalAlpha = 0.5;
    ctx.fillStyle = "#0066FF";
    ctx.fillRect(40, 40, 100, 100);
}

function DrawTriangle() {
    let canvas = document.getElementById("myTriangle");
    let ctx = canvas.getContext("2d");
    ctx.beginPath();
    ctx.moveTo(10, 0);
    ctx.lineTo(10, 100);
    ctx.lineTo(100, 100);
    ctx.fill();
    ctx.stroke();
}

function DrawArcs() {
    var canvas = document.getElementById("myArcCanvas");
    var ctx = canvas.getContext("2d");
    let x = 0;
    let y = 0;
    for (let i = 0; i < 6; i++) {
        x += 50;
        y += 50;
        ctx.beginPath();
        ctx.fillStyle = `rgb(${Math.floor(255 - 42.5 * i)}, ${Math.floor(255 - 42.5 * i)}, ${Math.floor(255 - 42.5 * i)})`;
        ctx.strokeStyle = "black";
        ctx.arc(25 + x, 25 + y, 15, 0, Math.PI * 2);
        ctx.stroke();
        ctx.fill();
    }
}
function DrawRectangle() {
    var canvas = document.getElementById("myRectangle");
    var ctx = canvas.getContext("2d");
    ctx.beginPath();
    ctx.fillStyle = "black";
    ctx.fillRect(0, 0, 150, 150);
    ctx.fillStyle = "white";
    ctx.strokeStyle = "black";
    ctx.fillRect(20, 20, 100, 100);
    ctx.stroke();
    ctx.fill();
    ctx.lineWidth = 5;
    ctx.strokeStyle = "black";
    ctx.rect(30, 30, 80, 80);
    ctx.stroke();
    ctx.fill();
}