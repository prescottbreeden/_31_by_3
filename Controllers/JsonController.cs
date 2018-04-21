using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using _31_by_3.Models;


namespace _31_by_3.Controllers
{
    public class JsonController : Controller
    {
        [HttpGet]
        [Route("start")]
        public JsonResult StartGame()
        {
            string player1 = HttpContext.Session.GetString("player1");
            string player2 = HttpContext.Session.GetString("player2");
            string player3 = HttpContext.Session.GetString("player3");
            string player4 = HttpContext.Session.GetString("player4");
            List<string> PlayerSelect = new List<string>{player1, player2, player3, player4};
            List<Player> Players = GamePlay.CreatePlayers(PlayerSelect);
            Deck CurrentDeck = GamePlay.BuildAndShuffle();
            GamePlay.Deal(Players, CurrentDeck);
            for(var i = 0; i < Players.Count; i++)
            {
                Players[i].hand_value = HandValue.Calculate(Players[i]);
            }
            var GameMaster = new { Players = Players, Deck = CurrentDeck, Turn = 0};
            return Json(GameMaster);
        }

        [HttpPost]
        [RequestSizeLimit(valueCountLimit: 1000000000)]
        [Route("DrawDeck")]
        public JsonResult DrawDeck(string GM)
        {
            GameMaster GameMaster = JsonConvert.DeserializeObject<GameMaster>(GM);
            GameMaster.players[GameMaster.turn].hand.Add(GameMaster.deck.deck[0]);
            GameMaster.deck.deck.RemoveAt(0);

            return Json(GameMaster);
        }

        [HttpPost]
        [RequestSizeLimit(valueCountLimit: 1000000000)]
        [Route("DrawDiscard")]
        public JsonResult DrawDiscard(string GM)
        {
            GameMaster GameMaster = JsonConvert.DeserializeObject<GameMaster>(GM);
            GameMaster.players[GameMaster.turn].hand.Add(GameMaster.deck.DiscardPile[0]);
            GameMaster.deck.DiscardPile.RemoveAt(0);

            return Json(GameMaster);
        }

        [HttpPost]
        [RequestSizeLimit(valueCountLimit: 1000000000)]
        [Route("DiscardCard")]
        public JsonResult DiscardCard(string GM)
        {
            GameMaster GameMaster = JsonConvert.DeserializeObject<GameMaster>(GM);
            for(int i = 0; i < GameMaster.players[GameMaster.turn].hand.Count; i ++)
            {
                if(GameMaster.players[GameMaster.turn].hand[i].selected)
                {  
                    GameMaster.players[GameMaster.turn].hand[i].selected = false; // remove discard attribute
                    GameMaster.deck.DiscardPile.Insert(0, GameMaster.players[GameMaster.turn].hand[i]);
                    GameMaster.players[GameMaster.turn].hand.RemoveAt(i);
                }
            }
            if(GameMaster.deck.deck.Count == 0)
            {
                foreach(var card in GameMaster.deck.DiscardPile)
                {
                    GameMaster.deck.deck.Add(card);
                }
                GameMaster.deck.DiscardPile.Clear();
                GameMaster.deck.Shuffle(GameMaster.deck.deck);
                GameMaster.deck.MoveTopCardToDiscardPile();
            }
            GameMaster.players[GameMaster.turn].hand_value = HandValue.Calculate(GameMaster.players[GameMaster.turn]);
            if(GameMaster.players[GameMaster.turn].hand_value == 31)
            {
                //call gameover
            }
            else
            {
                GameMaster.turn++;
                if (GameMaster.turn == 4)
                {
                    GameMaster.turn = 0;
                }
                if(GameMaster.players[GameMaster.turn].knocked == true)
                {
                    //call evaluate winner
                }
            }
            return Json(GameMaster);
        }

        [HttpPost]
        [RequestSizeLimit(valueCountLimit: 1000000000)]
        [Route("ComputerTurnDraw")]
        public JsonResult ComputerTurnDraw(string GM)
        {
            GameMaster GameMaster = JsonConvert.DeserializeObject<GameMaster>(GM);
            AI ComputerTurn = new AI(GameMaster.players[GameMaster.turn]);
            if(ComputerTurn.num_suits.Max() == 3)
            {
                int MaxSuitIndex = ComputerTurn.num_suits.ToList().IndexOf(3);
                switch(MaxSuitIndex)
                {
                    case 0:
                        ComputerTurn.max_suit_type = "hearts";
                        break;
                    case 1:
                        ComputerTurn.max_suit_type = "diamonds";
                        break;
                    case 2:
                        ComputerTurn.max_suit_type = "spades";
                        break;
                    case 3:
                        ComputerTurn.max_suit_type = "clubs";
                        break;
                }
                if(GameMaster.deck.DiscardPile[0].suit == ComputerTurn.max_suit_type)
                    {
                        Card min = ComputerTurn.hand[0];
                        foreach(Card c in ComputerTurn.hand)
                        {
                            if(c.value < min.value)
                                min = c;
                        }
                        if (GameMaster.deck.DiscardPile[0].value > min.value)
                        {
                            GameMaster.deck.DrawFromDiscard(ComputerTurn);
                            System.Console.WriteLine($"{GameMaster.players[GameMaster.turn].name} drew from discard pile");
                        }
                    }
                    else
                    {
                        GameMaster.deck.DrawFromDeck(ComputerTurn);
                    }
            }
                // 2 cards of same suit logic
                // else
                // {
                //     GameMaster.deck.DrawFromDeck(ComputerTurn);
                //     System.Console.WriteLine($"{GameMaster.players[GameMaster.turn]} drew from deck");
                // }
            else
                {
                    GameMaster.deck.DrawFromDeck(ComputerTurn);
                    System.Console.WriteLine($"{GameMaster.players[GameMaster.turn].name} drew from deck");
                }
            return Json(GameMaster);
        }

        [HttpPost]
        [Route("ComputerTurnDiscard")]
        public JsonResult ComputerTurnDiscard(string GM)
        {
            System.Threading.Thread.Sleep(3000);

                //-------------------------//
                // Hand size is now 4 cards//
                //-------------------------//

            GameMaster GameMaster = JsonConvert.DeserializeObject<GameMaster>(GM);
            AI ComputerTurn = new AI(GameMaster.players[GameMaster.turn]);
            
            if (ComputerTurn.num_suits.Max() == 4)
            {
                Card min = ComputerTurn.hand[0];
                foreach(Card c in ComputerTurn.hand)
                {
                    if(c.value < min.value)
                        min = c;
                }
                GameMaster.deck.DiscardPile.Insert(0, min);
                ComputerTurn.hand.Remove(min);
                System.Console.WriteLine($"{GameMaster.players[GameMaster.turn].name} discarded {min}"); 
            }
            else if (ComputerTurn.num_suits.Max() == 3)
            {
                int min_Suit_Index = ComputerTurn.num_suits.ToList().IndexOf(1);
                GameMaster.deck.DiscardPile.Insert(0, ComputerTurn.hand[min_Suit_Index]);
                ComputerTurn.hand.RemoveAt(min_Suit_Index);
                System.Console.WriteLine($"{GameMaster.players[GameMaster.turn].name} discarded {GameMaster.deck.DiscardPile[0]}.");  
            }
            else
            {
                Card min = new Card();
                min = ComputerTurn.hand[0];
                for(int idx = 0; idx < ComputerTurn.suit_values.Length; idx ++)
                {
                    if(ComputerTurn.suit_values[idx] == ComputerTurn.worst_value)
                    {
                        // 0 = hearts
                        // 1 = diamonds
                        // 2 = spades
                        // 3 = clubs
                        switch(idx)
                        {
                            case 0:
                                ComputerTurn.min_suit_type = ComputerTurn.hearts;
                                break;
                            case 1:
                                ComputerTurn.min_suit_type = ComputerTurn.diamonds;
                                break;
                            case 2:
                                ComputerTurn.min_suit_type = ComputerTurn.spades;
                                break;
                            case 3:
                                ComputerTurn.min_suit_type = ComputerTurn.clubs;
                                break;
                        }
                        foreach(Card c in ComputerTurn.min_suit_type)
                        {
                            if(c.value < min.value)
                                min = c;
                        }
                    }
                }
                GameMaster.deck.DiscardPile.Insert(0, min);
                ComputerTurn.hand.Remove(min);
                System.Console.WriteLine($"{GameMaster.players[GameMaster.turn].name} discarded {min}"); 

                //knock check
            }
            if (ComputerTurn.hand_value > 25)
            {
                // Knock(ComputerTurn);
            }
        
            if(GameMaster.players[GameMaster.turn].hand_value == 31)
            {
                //call gameover
            }
            else
            {
                GameMaster.turn++;
                if (GameMaster.turn == 4)
                {
                    GameMaster.turn = 0;
                }
                if(GameMaster.players[GameMaster.turn].knocked == true)
                {
                    //call evaluate winner
                }
            }
            return Json(GameMaster);
        }
    }
}