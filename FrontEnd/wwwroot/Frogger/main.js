
var playerX = 240;
var playerY = 420;
var score = 0;
var cars = [];
var myCar = new Image();
myCar.src = "/Frogger/car.jpg";
var myPlayer = new Image();
myPlayer.src = "/Frogger/frog.jpg";
function car(x, y, speed) {
    this.x = x;
    this.y = y;
    this.speed = speed;

    this.Move = function () {
        this.x += speed;
        if (this.x > 420)
            this.x = 0;s
    }
    this.Collision = function () {
        if ((playerY - this.y) >= 0 && (playerY - this.y) < 60 &&
            (playerX - this.x) >= 0 && (playerX - this.x) < 60)
            return true;
        }
        console.log("miss");
    }
function StartGame() {
    cars.push(new car(0, 120, 1));
    cars.push(new car(-60, 180, 3));
    cars.push(new car(-180, 240, 2));
    cars.push(new car(-60, 300, 4));
    cars.push(new car(-240, 360, 1));
    gameLoop();
}
function gameLoop() {
    CreateGameWindow();
    PlayerFinish();
    CheckCollisions();
    window.requestAnimationFrame(gameLoop);
}
function CreateGameWindow()
{
    var canvas = document.getElementById("gamewindow");
    var ctx = canvas.getContext("2d");
    ctx.fillStyle = "white";
    ctx.fillRect(0, 0, 500, 560);
    ctx.font = '48px serif';
    ctx.strokeText('Frogger', 0, 50);
    ctx.fillStyle = "green";
    ctx.fillRect(0, 60, 500, 60);
    ctx.fillStyle = "black";
    ctx.fillRect(0, 120, 500, 300);
    ctx.fillStyle = "green";
    ctx.fillRect(0, 420, 500, 60);
    ctx.strokeText('X:' + playerX + ' Y:' + playerY + " Score: " +score, 0, 520);
    //Obsticle
    ctx.fillStyle = "#935116";
    for (var i = 0; i < cars.length; i++) {
        cars[i].Move();
        ctx.drawImage(myCar, cars[i].x, cars[i].y, 60, 60);
    }
    ctx.drawImage(myPlayer, playerX, playerY, 60, 60);
}

function AddListeners() {
    document.addEventListener('keydown', (event) => {
        var name = event.key;
        var code = event.code;
        let move = 60;
        // Alert the key name and key code on keydown
        if (name == 'a')
            playerX -= move;
        if (name == 'd')
            playerX += move;
        if (name == 'w')
            playerY -= move;
        if (name == 's')
            playerY += move;
        CheckPlayerPlacement();
    }, false);
}
//Ser till att spelaren inte hamnar på utsidan.
function CheckPlayerPlacement()
{
    if (playerY >= 420)
        playerY = 420;
    if (playerY < 60)
        playerY = 60;
    if (playerX >= 420)
        playerX = 420;
    if (playerX < 0)
        playerX = 0;
}

function PlayerFinish()
{
    if (playerY == 60)
    {
        alert("Du vann");
        score++;
        Reset();
    }
}
function PlayerKilled() {
    alert("Du dog");
    score = 0;
    Reset();
}

function Reset()
{
    playerX = 240;
    playerY = 420;
}
function CheckCollisions() {
    for (var i = 0; i < cars.length; i++) {
        if (cars[i].Collision() == true)
            PlayerKilled();
    }
}