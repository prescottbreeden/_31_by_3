
$(document).ready(function()
{
    var GameMaster;

    function replacePlayerHands()
    {
        for(var idx = 0; idx < GameMaster.players.length; idx++)
        {
            $(".HandTarget" + idx).empty();
            for(card in GameMaster.players[idx].hand)
            {
                $(".HandTarget" + idx).append(
                    `    <div class="player-card cardNumber${card} col-12 col-md-6 col-lg-3">
                            <div class="row">
                                <div class="col-12">
                                    <div class="card-anchor">
                                        <div class="m0a w100">
                                            <img alt="WHY" id="player_card${idx}${card}" class="clickable">
                                            <input type="hidden" class="value" value="${card}">
                                            </div>
                                        <!-- a card should go here -->
                                        </div>
                                    </div>
                                </div>
                            </div>
                    `)
                    if((GameMaster.players[idx].isHuman && (GameMaster.players[idx].player_seat == GameMaster.turn || GameMaster.singlePlayer)) || GameMaster.endGame != null || GameMaster.allAI == true)
                    {
                        document.getElementById("player_card" + idx + card).setAttribute("src", "http://localhost:8000/img/" + GameMaster.players[idx].hand[card]["suit"][0] + GameMaster.players[idx].hand[card]["face"] )
                    }
                    else
                    {
                        document.getElementById("player_card" + idx + card).setAttribute("src", "http://localhost:8000/img/cardback" )
                    }
            }
        }       
    }

    function hidePlayerHands()
    {
        for(var idx = 0; idx < GameMaster.players.length; idx++)
        {
            $(".HandTarget" + idx).empty();
            for(card in GameMaster.players[idx].hand)
            {
                $(".HandTarget" + idx).append(
                    `    <div class="player-card cardNumber${card} col-12 col-md-6 col-lg-3">
                            <div class="row">
                                <div class="col-12">
                                    <div class="card-anchor">
                                        <div class="m0a w100">
                                            <img alt="WHY" id="player_card${idx}${card}" class="clickable">
                                            <input type="hidden" class="value" value="${card}">
                                            </div>
                                        <!-- a card should go here -->
                                        </div>
                                    </div>
                                </div>
                            </div>
                    `)
                document.getElementById("player_card" + idx + card).setAttribute("src", "http://localhost:8000/img/cardback" )
            }
        }       
    }



    function ShowDiscardPile()
    {
        if(GameMaster.deck.discardPile.length > 0)
        {
            document.getElementById("discard_pile_top_card").setAttribute("src", "http://localhost:8000/img/" + GameMaster.deck.discardPile[0]["suit"][0] + GameMaster.deck.discardPile[0]["face"] )
        }
        else
        {
            document.getElementById("discard_pile_top_card").setAttribute("src", "http://localhost:8000/img/cardback")
        }
    }

    function CompDraw()
    {
        var player = GameMaster.turn;
        $.ajax({
            type: "POST",
            data: {"GM" :JSON.stringify(GameMaster)},
            url: "/ComputerTurnDraw",
            dataType: "json",
            success: function(res){
                console.log(res);
                GameMaster = res;
                ShowDiscardPile();
                replacePlayerHands();
                CompDiscard();
            }
        })
    }
    
    function CompDiscard()
    {
        var player = GameMaster.turn;
        $.ajax({
            type: "POST",
            data: {"GM" :JSON.stringify(GameMaster)},
            url: "/ComputerTurnDiscard",
            dataType: "json",
            success: function(res){
                console.log(res);
                GameMaster = res;
                ShowDiscardPile();
                replacePlayerHands();
                if(GameMaster.players[player].knocked == true)
                {
                    alert(GameMaster.players[player].name + " has just knocked! ruh roh!")
                }
                if(GameMaster.endGame != null)
                {
                    replacePlayerHands();
                    if(confirm(GameMaster.endGame.winner.name +" won the game! ... Sorry if you weren't them... : would you like to play again?"))
                    {
                        window.location.replace("localhost:5000")
                    }
                    else
                    {
                        window.location.replace("localhost:5000")
                    }
                }
                else if(GameMaster.players[GameMaster.turn].isHuman == false)
                {
                    CompDraw();
                }
                else
                {
                    alert(GameMaster.players[GameMaster.turn].name + " will draw next.")
                }
            }
        })

    }

    $(document).on("click", ".clickable", function()
    {
        if($(this).parents('.HandTarget' + GameMaster.turn).length)
        {
            $(".hand").find(".player-selected").removeClass("player-selected");
            $(this).addClass("player-selected");
        }
    });

    $("#PlayGame").click(function()
    {
        $.get("/start",function(res)
        {
            GameMaster = res;
            console.log(GameMaster);
            var player_hands = document.getElementById("player_hands");
            var img = document.createElement("img")
            ShowDiscardPile()
            // if(GameMaster.players[0].isHuman && !GameMaster.singlePlayer)
            // {
            //     alert(GameMaster.players[0].name + " will draw first.")
            // }
            for(let player = 0; player < GameMaster.players.length; player ++)
            {
                player_hands.innerHTML += (`
                
                <!-- START OF ONE HAND -->
                                
                <div class="hand hand${player}">
                <div class="tl-arrow"></div>
                <div class="tr-arrow"></div>
                <div class="bl-arrow"></div>
                <div class="br-arrow"></div>
                <div class="row">
                    <div class="col-10">
                        <div class="row">
                            <div class="col-12">
                                <div class="row hand-labels">
                                    <div class="col-12 col-md-6">
                                        <h3 class="player_name">${GameMaster.players[player].name}</h3>
                                    </div>
                                    <div class="col-12 col-md-6">
                                        <h3 class="player_tokens">Chips: ${GameMaster.players[player].chips}</h3>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12" class="">
                                <div class="row HandTarget${player}">
                                <!-- HERE IS WHERE THE HAND GOES -->
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-2 hand-buttons">
                        <div class="row">
                            <div class="col-4"></div>
                            <div class="col-4">
                                <div class="turn-indicator">
                                    <i class="fas fa-child"></i>
                                </div>
                            </div>
                            <div class="col-4"></div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <!-- feel free to put these inside forms if easier/required. Make sure the form is instantiated inside of the col-12 -->
                                <!-- discard card button -->
                                <button class="discard-btn">Discard</button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <!-- laydown button -->
                                <button class ="knock-btn">Knock</button>
                                <!-- knock button -->
                                <!-- <button class="hide">Lay Down!</button> -->
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <!-- Show Hide Hand button -->
                                <button id="reveal_hand">Show</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END OF HAND  -->              
                `)   
            }    
            for(let i = 0; i < 4; i ++)
            {
                for(card in GameMaster.players[i].hand)
                    {
                        $(".HandTarget" + i).append(
                        `    <div class="player-card cardNumber${card} col-12 col-md-6 col-lg-3">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="card-anchor">
                                            <div class="m0a w100">
                                                <img alt="WHY" id="player_card${i}${card}" class="clickable">
                                                <input type="hidden" class="value" value="${card}">
                                            </div>
                                            <!-- a card should go here -->
                                        </div>
                                    </div>
                                </div>
                            </div>
                            `)
                            if(GameMaster.players[i].isHuman && GameMaster.players[i].player_seat == GameMaster.turn)
                            {
                                document.getElementById("player_card" + i + card).setAttribute("src", "http://localhost:8000/img/" + GameMaster.players[i].hand[card]["suit"][0] + GameMaster.players[i].hand[card]["face"] )
                            }
                            else
                            {
                                document.getElementById("player_card" + i + card).setAttribute("src", "http://localhost:8000/img/cardback" )
                            }
                        }
                        
                }
                if(GameMaster.players[GameMaster.turn].isHuman == false)
                {
                    CompDraw();
                }
            });
            $("#PlayGame").remove()
        })

        $("#DrawCard").on("click", function()
        {
            var player = GameMaster.turn
            if(GameMaster.players[player].hand.length == 3)
            {    
                $.ajax({
                    type: "POST",
                    data: {"GM" :JSON.stringify(GameMaster)},
                    url: "/DrawDeck",
                    dataType: "json",
                    success: function(res)
                    {
                        console.log(res);
                        GameMaster = res;
                        replacePlayerHands();
                    }
                })
            }
            else
            {
                console.log("You may only draw once per turn")
            }
        });

        $("#DiscardCard").on("click", function(){
            if(GameMaster.players[GameMaster.turn].hand.length == 3)
            {

                $.ajax({
                    type: "POST",
                    data: {"GM" :JSON.stringify(GameMaster)},
                    url: "/DrawDiscard",
                    dataType: "json",
                    success: function(res){
                        console.log(res);
                        GameMaster = res;
                        ShowDiscardPile();
                        replacePlayerHands();
                    }
                })
            }
            else
            {
                console.log("You may only draw once per turn")
            }
        });

        $(document).on("click", ".discard-btn", function()
        {
            var player = GameMaster.turn;
            if($(this).parents('.hand' + player).length)
            {
                if(GameMaster.players[player].hand.length == 4)
                {
                    //flag that card with a selected property
                    for(let i = 0; i < GameMaster.players[player].hand.length; i ++)
                    {
                        var isSelected = $(".cardNumber" + i).find(".player-selected");
                        if(isSelected.length)
                        {
                            GameMaster.players[player].hand[i].selected=true
                            $.ajax({
                                type: "POST",
                                data: {"GM" :JSON.stringify(GameMaster)},
                                url: "/DiscardCard",
                                dataType: "json",
                                success: function(res){
                                    console.log(res);
                                    GameMaster = res;
                                    ShowDiscardPile();
                                    hidePlayerHands();
                                    if(GameMaster.players[player].knocked == true)
                                    {
                                        alert(GameMaster.players[player].name + " has just knocked! ruh roh!")
                                    }
                                    if(GameMaster.endGame != null)
                                    {
                                        replacePlayerHands();
                                        if(confirm(GameMaster.endGame.winner.name +" won the game! ... Sorry if you weren't them... : would you like to play again?"))
                                        {
                                            window.location.replace("localhost:5000")
                                        }
                                        else
                                        {
                                            window.location.replace("localhost:5000")
                                        }
                                    }
                                    else
                                    {
                                        $.ajax({
                                            type: "POST",
                                            data: {"GM" :JSON.stringify(GameMaster)},
                                            url: "/NextTurn",
                                            dataType: "json",
                                            success: function(res){
                                                console.log(res);
                                                GameMaster = res;
                                                replacePlayerHands();
                                                if(GameMaster.players[GameMaster.turn].isHuman)
                                                {
                                                    alert(GameMaster.players[GameMaster.turn].name + " will draw next.")
                                                }
                                                if(!GameMaster.players[GameMaster.turn].isHuman)
                                                {
                                                    CompDraw();
                                                }
                                            }
                                        })    
                                    }
                                }
                            })
                        }
                    } 
                }
                else
                {
                    console.log("Please draw a card first")
                    return;
                }
            }
            return;
        });

    $(document).on("click", ".knock-btn", function()
    {
        if(GameMaster.players[GameMaster.turn].hand.length == 3 && GameMaster.knocked == false)
        {
            GameMaster.players[GameMaster.turn].knocked = true;
            GameMaster.knocked = true;
            alert(GameMaster.players[GameMaster.turn].name + " has just knocked... ruh roh!")
            if(GameMaster.players[GameMaster.turn].isHuman == false)
            {
                CompDraw();
            }
            else
            {
                $.ajax({
                    type: "POST",
                    data: {"GM" :JSON.stringify(GameMaster)},
                    url: "/NextTurn",
                    dataType: "json",
                    success: function(res){
                        console.log(res);
                        GameMaster = res;
                        if(GameMaster.players[GameMaster.turn].isHuman)
                        {
                            alert(GameMaster.players[GameMaster.turn].name + " will draw next.")
                        }
                        else
                        {
                            CompDraw();
                        }
                    }
                })
            }
        }
    })



})
    



 // document ready
