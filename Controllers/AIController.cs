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
    public class AIController : Controller
    {
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
            Card min = ComputerTurn.hand[0];
            
            // if (ComputerTurn.num_suits.Max() == 4)
            // {
            //     foreach(Card c in ComputerTurn.hand)
            //     {
            //         if(c.value < min.value)
            //             min = c;
            //     }
            //     GameMaster.deck.DiscardPile.Insert(0, min);
            //     ComputerTurn.hand.Remove(min);
            //     System.Console.WriteLine($"{GameMaster.players[GameMaster.turn].name} discarded {min}"); 
            // }
            // else if (ComputerTurn.num_suits.Max() == 3)
            // {
            //     for(int idx = 0; idx < ComputerTurn.hand.Count; idx++)
            //     {
            //         if(ComputerTurn.hand[idx].suit != ComputerTurn.best_suit)
            //         {
            //             if(ComputerTurn.hand[idx].value < min.value)
            //             {
            //                 min = ComputerTurn.hand[idx];
            //             }
            //         }
            //     }
            //     GameMaster.deck.DiscardPile.Insert(0, min);
            //     ComputerTurn.hand.Remove(min);
            //     System.Console.WriteLine($"{GameMaster.players[GameMaster.turn].name} discarded {GameMaster.deck.DiscardPile[0]}.");  
            // }
            // else
            // {
            //     if(ComputerTurn.worst_value == ComputerTurn.hand_value)
            //     {
            //         foreach(Card c in ComputerTurn.hand)
            //             {
            //                 if(c.value < min.value)
            //                     min = c;
            //             }
            //     }
            //     else
            //     {
            //         for(int idx = 0; idx < ComputerTurn.hand.Count; idx++)
            //         {
            //             if(ComputerTurn.hand[idx].suit != ComputerTurn.best_suit)
            //             {
            //                 if(ComputerTurn.hand[idx].value < min.value)
            //                 {
            //                     min = ComputerTurn.hand[idx];
            //                 }
            //             }
            //         }
            //     } 
            //     GameMaster.deck.DiscardPile.Insert(0, min);
            //     ComputerTurn.hand.Remove(min);
            //     System.Console.WriteLine($"{GameMaster.players[GameMaster.turn].name} discarded {min}"); 

            //     //knock check
            // }

            int MaxHandValue;
            int BadCardIndex;
            for (int idx = )
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