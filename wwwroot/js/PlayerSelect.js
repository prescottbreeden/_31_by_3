$(document).ready(function(){
    console.log("Hookup is working!")
    var playerNumber = 2;
    $("#PlayerForm").append('<input type="hidden" id="PlayerCounter" name="PlayerCounter" value="' + playerNumber +'">');

    $("#AddPlayer").click(function(){
        if(playerNumber < 6)
        {
            document.getElementById("PlayerForm").innerHTML += 
            `<div class="form-group" id="player${playerNumber + 1}">
                <p>Player ${playerNumber + 1} Name:</p>
                <input type="text" class="form-control ml-2" name="player${playerNumber + 1}"  placeholder="..">
            </div>`
            playerNumber ++;
            $("#PlayerCounter").remove();
            $("#PlayerForm").append(`<input type="hidden" id="PlayerCounter" name="PlayerCounter" value="${playerNumber}">`);
        }

    });

    $("#RemovePlayer").click(function(){
        if(playerNumber > 2)
        {
            $("#player" + playerNumber).remove()
            playerNumber --;
            $("#PlayerCounter").remove();
            $("#PlayerForm").append(`<input type="hidden" id="PlayerCounter" name="PlayerCounter" value="${playerNumber}">`);
        }
    })
})