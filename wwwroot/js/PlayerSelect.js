$(document).ready(function(){
    console.log("Hookup is working!")
    var playerNumber = 2;
    $("#AddPlayer").click(function(){
        document.getElementById("PlayerForm").innerHTML += 
        `<div class="form-group">
            <p>Player ${playerNumber + 1} Name:</p>
            <input type="text" class="form-control ml-2" name="player${playerNumber + 1}" id="player${playerNumber + 1}" placeholder="..">
        </div>`
        playerNumber ++;
    });

    $("#RemovePlayer").click(function(){
        document.removeChild(document.getElementById("player" + playerNumber));
        playerNumber --;
    })
})