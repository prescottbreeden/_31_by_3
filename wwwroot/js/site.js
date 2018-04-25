$(document).ready(function()
{
    // Game Master Data
    var GameMaster;

    var TrashTalk = [];
    var Shakespearian = ["Thou art a crooked bog!", "Thou art a thin faced plague!", "Thou art a slothful dog!", "Thou art a deformed coward!", "Thou art a foolish ape!", "Thou art an ordinary double villain!", "Thou art an unnecessary carbuncle!", "Thou art a crusty nit!", "Thou art a whining maltworm!"];

    var MontyPython = ["It's only a flesh wound...", "My sister was bit by a moose", "Moose bites can be pretti nasti", "I'll do you for that!", "Bring out the holy hand grenade of Antioch!", "Ni!!", "Help help! I'm being oppressed!", "I'll bite your legs off!", "I fart in your general direction!", "Anyone in the mood for a farcical aquatic ceremony?", "Go and boil your bottoms, you sons of silly persons!", "I'll turn you into a newt!", "Your mother was a hamster and your father smelt of elderberries!", "Quit or I'll taunt you a second time!"];

    // ----------------------- //
    // ---- ALL FUNCTIONS ---- //
    // ----------------------- //
    function createPlayerSlots()
    {
        var player_hands = document.getElementById("player_hands");
        player_hands.innerHTML = "";
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
                                <button class="assist-btn">Help</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- END OF HAND  -->
                `)
            }
            for(let i = 0; i < GameMaster.players.length; i ++)
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
    }

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
                    if((GameMaster.players[idx].isHuman && (GameMaster.players[idx].player_seat == GameMaster.turn || GameMaster.singlePlayer)) || GameMaster.endRound != null || GameMaster.allAI == true)
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
    function HumanTurnChange(nextplayer)
    {
        document.getElementById("change_turn").innerHTML = "";
        $("#change_turn").append(`
        <div class="inner-shadow">
            <div class="bubble-border">
                <div class="row">
                    <div class="col-12">
                        <h2 class="tac">${nextplayer}'s turn is next:</h2>
                        <ul>
                            <li>Click "Ready!" to show your cards and begin your turn.</li>
                        </ul>
                        <button id="shadowbox_confirm">Ready!</button>
                    </div>
                </div>
            </div>
        </div>`)
        $("#change_turn").toggle();
        $("#change_turn_shadow_box").toggle();
    }
    function EndRoundResults()
    {
        var htmlResults = ""
        for(var idx = 0; idx < GameMaster.players.length; idx++)
        {
            htmlResults += `<li>${GameMaster.players[idx].name}: ${GameMaster.players[idx].hand_value} points</li>`
        }

        document.getElementById("change_turn").innerHTML = "";
        $("#change_turn").append(`
        <div class="inner-shadow">
            <div class="bubble-border">
                <div class="row">
                    <div class="col-12">
                        <h2 class="tac">${GameMaster.endRound.winner.name} won the round!</h2>
                        <ul>
                            ${htmlResults} 
                        </ul>
                        <button id="shadowbox_end_round">See Hands</button>
                    </div>
                </div>
            </div>
        </div>`)
        $("#change_turn").toggle();
        $("#change_turn_shadow_box").toggle();
    }
    function CallNextRound()
    {
        if(GameMaster.endGame != null)
        {
            $.ajax({
                type: "POST",
                data: {"GM" :JSON.stringify(GameMaster)},
                url: "/NextRound",
                dataType: "json",
                success: function(res)
                {
                    console.log(res);
                    GameMaster = res;
                    ShowDiscardPile()
                    createPlayerSlots();
                    if(!GameMaster.players[GameMaster.turn].isHuman)
                    {
                        CompDraw();
                    }
                }
            })
        }
    }

    function NextTurn()
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
                if(GameMaster.endRound != null)
                {
                    replacePlayerHands();
                    EndRoundResults();                                
                }
                else if(!GameMaster.players[GameMaster.turn].isHuman)
                {
                    CompDraw();
                }
            }
        })
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
        var nextplayer = player+1;
        if(nextplayer == GameMaster.players.length)
        {
            nextplayer = 0;
        }
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
                if(GameMaster.players[player].knocked)
                {
                    new Audio("../Knocking.mp3").play();
                    // alert(GameMaster.players[player].name + " has just knocked! ruh roh!")
                }
                if(GameMaster.endRound != null)
                {
                    replacePlayerHands();
                    EndRoundResults();
                }
                else if((GameMaster.players[nextplayer].isHuman && !GameMaster.players[nextplayer].knocked) && !GameMaster.singlePlayer)
                {
                    HumanTurnChange(GameMaster.players[nextplayer].name);
                }
                else
                {
                    NextTurn();
                }
            }
        })
    }

    // ------------------------ //
    // ------ AJAX STUFF ------ //
    // ------------------------ //

    // Initialize Game
    $("#PlayGame").click(function()
    {
        $.get("/start",function(res)
        {
            // new Audio("../MidnightPianoBar.mp3").play();  // music on or off
            GameMaster = res;
            console.log(GameMaster);
            ShowDiscardPile()
            createPlayerSlots();
                if(!GameMaster.players[GameMaster.turn].isHuman)
                {
                    CompDraw();
                }
        });
        // $("#PlayGame").remove()
    })

    $(document).on("click", "#shadowbox_end_round", function(){
        $("#change_turn").toggle();
        $("#change_turn_shadow_box").toggle();
    })

    // Next Round
    $("#NextRound").click(function()
    {
        CallNextRound(GameMaster);
    })
    

    // Select a Card
    $(document).on("click", ".clickable", function()
    {
        if($(this).parents('.HandTarget' + GameMaster.turn).length)
        {
            $(".hand").find(".player-selected").removeClass("player-selected");
            $(this).addClass("player-selected");
        }
    });

    // Help Player
    $(document).on("click", ".assist-btn", function()
    {
        var player = GameMaster.turn;
        $(".hand").find(".player-selected").removeClass("player-selected");
        if(GameMaster.players[player].hand.length ==4)
        {
            $.ajax({
                type: "POST",
                data: {"GM" :JSON.stringify(GameMaster)},
                url: "/AssistPlayer",
                dataType: "json",
                success: function(res)
                {
                    console.log(res);
                    GameMaster = res;
                    
                    for(var i = 0; i < GameMaster.players[player].hand.length; i++)
                    {
                        if(GameMaster.players[player].hand[i].selected == true)
                        {
                            $("#player_card"+player+i).addClass("player-selected")
                        }
                    }
                }
            })
        }
        else
        {
            console.log("First tip: Draw a Card you dingus...")
        }
        
        
    })

    // Human Draw Deck
    $("#DrawCard").on("click", function()
    {
        if(GameMaster.endGame == null)
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
                console.log("You may only draw once per turn");
            }
        }
        else
        {
            console.log("Please select 'Next Round' to start the next round.");
        }
    });

    // Human Draw From Discard
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

    // Human Discard a Card
    $(document).on("click", ".discard-btn", function()
    {
        var player = GameMaster.turn;
        var nextplayer = GameMaster.turn+1;
        if(nextplayer == GameMaster.players.length)
        {
            nextplayer = 0;
        }
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
                        GameMaster.players[player].hand[i].selected = true
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
                                if(GameMaster.players[player].knocked)
                                {
                                    new Audio("../Knocking.mp3").play();
                                    // alert(GameMaster.players[player].name + " has just knocked! ruh roh!")
                                }                  
                                if((GameMaster.players[nextplayer].isHuman && !GameMaster.players[nextplayer].knocked) && !GameMaster.singlePlayer)
                                {
                                    HumanTurnChange(GameMaster.players[nextplayer].name);
                                }
                                else
                                {
                                    NextTurn();
                                }
                            }
                        }) // end of discard ajax
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
    // Next human player ready
    $(document).on("click", "#shadowbox_confirm", function()
    {
        $("#change_turn").toggle();
        $("#change_turn_shadow_box").toggle();
        NextTurn();
    })

    // Human Knock
    $(document).on("click", ".knock-btn", function()
    {
        var player = GameMaster.turn;
        var nextplayer = GameMaster.turn+1;
        if(nextplayer == GameMaster.players.length)
        {
            nextplayer = 0;
        }
        if(GameMaster.players[player].hand.length == 3 && !GameMaster.knocked)
        {
            // change gamemaster and current player status to knocked
            GameMaster.players[player].knocked = true;
            GameMaster.knocked = true;
            new Audio("../Knocking.mp3").play();
            //---------------------------------//
            //--- Insert Knock Notification ---//
            //---------------------------------//
            if(!GameMaster.players[nextplayer].isHuman)
            {
                NextTurn();
            }
            else
            {
                hidePlayerHands();
                // next player is human, and they haven't knocked, and it is not in single player mode --> call next human turn prompt
                if((GameMaster.players[nextplayer].isHuman && !GameMaster.players[nextplayer].knocked) && !GameMaster.singlePlayer)
                {
                    HumanTurnChange(GameMaster.players[nextplayer].name);
                }
                // else if((GameMaster.players[nextplayer].isHuman && GameMaster.players[nextplayer].knocked) && !GameMaster.singlePlayer)
                // {
                //     NextTurn();
                // }
            }
        }
    })

}) // document ready
