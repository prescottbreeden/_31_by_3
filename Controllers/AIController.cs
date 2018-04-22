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
            Player player = GameMaster.players[GameMaster.turn];
            AI Computer = new AI(player);
            
            if(Computer.EvaluateDiscardCard(Computer, GameMaster.deck.DiscardPile[0]))
            {
                GameMaster.deck.DrawFromDiscard(Computer);
            }
            else
            {
                GameMaster.deck.DrawFromDeck(Computer);
            }

            return Json(GameMaster);
        }

        [HttpPost]
        [Route("ComputerTurnDiscard")]
        public JsonResult ComputerTurnDiscard(string GM)
        {
            System.Threading.Thread.Sleep(3000);

            GameMaster GameMaster = JsonConvert.DeserializeObject<GameMaster>(GM);
            Player player = GameMaster.players[GameMaster.turn];
            AI Computer = new AI(player);
            Card min = Computer.ChooseDiscard(Computer);
            
            GameMaster.deck.DiscardPile.Insert(0, min);
            player.hand.Remove(min);
            player.hand_value = HandValue.Calculate(player);
            //knock check here

            if(player.hand_value == 31)
            {
                GameOver endGame = new GameOver(player, GameMaster.players);
                GameMaster.endGame = endGame;
            }
            else
            {
                GameMaster.turn++;
                if (GameMaster.turn == 4)
                {
                    GameMaster.turn = 0;
                }
                if(player.knocked == true)
                {
                    GameOver endGame = new GameOver(GameMaster.players);
                    GameMaster.endGame = endGame;
                }
            }
            return Json(GameMaster);
        }
    }
}