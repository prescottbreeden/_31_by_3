
$(document).ready(function(){
    var GameMaster;

    function ShowDiscardPile(){
        document.getElementById("discard_pile_top_card").setAttribute("src", "http://localhost:8000/img/" + GameMaster.deck.discardPile[0]["suit"][0] + GameMaster.deck.discardPile[0]["face"] )
    }
    $(document).on("click", ".clickable", function(){
        if($(this).parents('.HandTarget' + GameMaster.turn).length)
        {
            console.log("FOUND THE RIGHT HANDTARGET")
            $(".hand").find(".player-selected").removeClass("player-selected");
            $(this).addClass("player-selected");
        }
    });

    $("#PlayGame").click(function(){
        $.get("/start",function(res){
            GameMaster = res;
            console.log(GameMaster);
            var player_hands = document.getElementById("player_hands");
            var img = document.createElement("img")
            ShowDiscardPile()
            for(let player = 0; player < GameMaster.players.length; player ++)
            {
                player_hands.innerHTML += (`
                
                <!-- START OF ONE HAND -->
                                
                <div class="hand">
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
                                        <h3 class="player_tokens">Tokens: ${GameMaster.players[player].tokens}</h3>
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
                                <button>Knock</button>
                                <!-- knock button -->
                                <!-- <button class="hide">Lay Down!</button> -->
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <!-- Show Hide Hand button -->
                                <button id="reveal_hand">Show</button>
                                <button id="hide_hand" class="hidden">Hide</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END OF HAND  -->              
                `)   
            }    
            for(let player = 0; player < 4; player ++)
            {
                for(card in GameMaster.players[player].hand)
                    {
                        $(".HandTarget" + player).append(
                            `<div class="player-card col-12 col-md-6 col-lg-3">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="card-anchor">
                                            <div class="m0a w100">
                                                <img alt="WHY" id="player_card${player}${card}" class="clickable">
                                            </div>
                                            <!-- a card should go here -->
                                        </div>
                                    </div>
                                </div>
                            </div>
                            `)
                            document.getElementById("player_card" + player + card).setAttribute("src", "http://localhost:8000/img/" + GameMaster.players[player].hand[card]["suit"][0] + GameMaster.players[player].hand[card]["face"] )
                        }
                        
                }
            });
            $("#PlayGame").remove()
        })

        $("#DrawCard").on("click", function(){
            $.ajax({
                type: "POST",
                data: {"GM" :JSON.stringify(GameMaster)},
                url: "/DrawDeck",
                dataType: "json",
                success: function(res){
                    console.log(res);
                    GameMaster = res;

                    $(".HandTarget" + GameMaster.turn).append(
                        `<div class="remove-me player-card col-12 col-md-6 col-lg-3">
                            <div class="row">
                                <div class="col-12">
                                    <div class="card-anchor">
                                        <div class="m0a w100">
                                             <img class="clickable" id="draw_card${GameMaster.turn}"></img>
                                        </div>
                                        <!-- a card should go here -->
                                    </div>
                                </div>
                            </div>
                        </div>
                        `)
                    document.getElementById("draw_card" + GameMaster.turn).setAttribute("src", "http://localhost:8000/img/" + GameMaster.players[GameMaster.turn].hand[GameMaster.players[GameMaster.turn].hand.length-1]["suit"][0] + GameMaster.players[GameMaster.turn].hand[GameMaster.players[GameMaster.turn].hand.length-1]["face"] )
                }
            });
            // DC.done(function(res){})
            // document.getElementById("draw_card" + "0").setAttribute("src", "http://localhost:8000/img/" + GameMaster.players[0].hand[2]["suit"][0] + GameMaster.players[0].hand[2]["face"] )
        })




}) // document ready
