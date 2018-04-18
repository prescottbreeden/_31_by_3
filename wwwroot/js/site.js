
$(document).ready(function(){
    var GameMaster;
    $("#PlayGame").click(function(){
        $.get("/start",function(res){
            GameMaster = res;
            console.log(GameMaster);
            var player_hands = document.getElementById("player_hands");
            var img = document.createElement("img")
            for(let player = 0; player < GameMaster.players.length; player ++)
            {
                console.log("IN THE FIRST LOOP");
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
                                        <h3 class="player_name">player.name</h3>
                                    </div>
                                    <div class="col-12 col-md-6">
                                        <h3 class="player_tokens">player.tokens</h3>
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
            <!-- END OF HAND  --><!-- START OF ONE HAND -->               
                
                
                `)
                
            }
            
            for(let player = 0; player < 4; player ++)
            {
                console.log("for loop outer")
                // for(card in GameMaster.players[player].hand)
                for(var i = 0; i < 4; i ++)
                    {
                        console.log("for loop inner")
                        console.log("IN THE SECOND LOOP")
                        $(".HandTarget" + player).append(
                            `<div class="player-card col-12 col-md-6 col-lg-3">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="card-anchor">
                                            <div class="m0a w100">
                                                <img alt="WHY" class="fucking-work-please${player}${i}">
                                            </div>
                                            <!-- a card should go here -->
                                        </div>
                                    </div>
                                </div>
                            </div>`)
                            $('.fucking-work-please' + player + i).setAttribute("src", "http://localhost:8000/img/c" + i)
                            Prescott sucks at git
                            actually lawyer sucks
                        }
                        
                }
        })
    })

})
