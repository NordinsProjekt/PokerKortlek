var playerX = 240;
var playerY = 540;
var score = 0;
var cars = [];
var myCar = new Image();
myCar.src = "/Frogger/car.jpg";
var myPlayer = new Image();
var AwaitEnterKey = false;
var playerDead = false;


function car(x, y, speed) {
    this.x = x;
    this.y = y;
    this.speed = speed;

    this.Move = function () {
        this.x += speed;
        if (this.x > 420)
            this.x = 0;
    }
    this.Collision = function ()
    {
        //Collision works on the right edge of the car
        var diff = this.x -playerX;
        //if (this.x <= playerX && this.x + 60 >= playerX && playerY == this.y && diff > 0 && diff < 60)
        if (playerY == this.y && diff >= 0 && diff <= 60)
            return true;
        if (playerY == this.y && diff >= -60 && diff <= 0)
            return true;
        return false;
    }
}

function floatingObject(x, y, speed) {
    this.x = x;
    this.y = y;
    this.speed = speed;
}

function StartGame() {
    cars.push(new car(0, 240, 1));
    cars.push(new car(-60, 300, 1.5));
    cars.push(new car(-180, 360, 2));
    cars.push(new car(-60, 420, 0.5));
    cars.push(new car(-240, 480, 1));
    gameLoop();
}
function gameLoop() {
    CreateGameWindow();
    CheckCollisions();
    window.requestAnimationFrame(gameLoop);
}
function CreateGameWindow()
{
    var canvas = document.getElementById("gamewindow");
    var ctx = canvas.getContext("2d");
    //Start
    ctx.fillStyle = "white";
    ctx.fillRect(0, 0, 600, 560);
    ctx.font = '48px serif';
    ctx.strokeText('Frogger', 0, 50);


    //GamePart Start and Road
    ctx.fillStyle = "green";
    ctx.fillRect(0, 60, 500, 60);
    ctx.fillStyle = "blue";
    ctx.fillRect(0, 120, 500, 120);
    ctx.fillStyle = "black"; //Road
    ctx.fillRect(0, 240 , 500, 540);
    ctx.fillStyle = "green"; //Start
    ctx.fillRect(0, 540, 500, 60);

    //Player
    if (playerDead) {
        myPlayer.src = "/Frogger/death.jpg";
    } else {
        myPlayer.src = "/Frogger/frog.jpg";
    }
    ctx.drawImage(myPlayer, playerX, playerY, 60, 60);

    //Cars
    for (var i = 0; i < cars.length; i++) {
        cars[i].Move();
        ctx.drawImage(myCar, cars[i].x, cars[i].y, 60, 60);
    }

    //Check GameState
    if (AwaitEnterKey == true && playerDead == false) {
        ctx.font = '38px serif';
        ctx.strokeStyle = "white";
        ctx.strokeText('Winner is you', 50, 200);
        ctx.strokeText('Press ENTER to continue', 50, 300)
    }
    ctx.font = '48px serif';
    ctx.strokeStyle = "black";
    ctx.strokeText('X:' + playerX + ' Y:' + playerY + " Score: " + score, 0, 650);
}

function AddListeners() {
    document.addEventListener('keyup', (event) => {
        var name = event.key;
        var code = event.code;
        let move = 60;
        console.log(name);
        if (AwaitEnterKey == true) {
            if (name == 'Enter') {
                AwaitEnterKey = false;
                Reset();
            }
        }
        else {
            if (name == 'a')
                playerX -= move;
            if (name == 'd')
                playerX += move;
            if (name == 'w')
                playerY -= move;
            if (name == 's')
                playerY += move;
            CheckPlayerPlacement();
            PlayerFinish();
        }
        // Alert the key name and key code on keydown

    }, false);
}
//Ser till att spelaren inte hamnar på utsidan.
function CheckPlayerPlacement()
{
    if (playerY >= 540)
        playerY = 540;
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
        score++;
        AwaitEnterKey = true;
    }
}
function PlayerKilled() {
    score = 0;
    playerDead = true;
    AwaitEnterKey = true;
}

function PlayerWin() {

}

function Reset()
{
    playerX = 240;
    playerY = 540;
    playerDead = false;
}
function CheckCollisions() {
    for (var i = 0; i < cars.length; i++) {
        if (cars[i].Collision() == true)
            PlayerKilled();
    }
}