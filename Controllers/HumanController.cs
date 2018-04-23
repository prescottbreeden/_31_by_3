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
    public class HumanController : Controller
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
            GameMaster newGame = new GameMaster(PlayerSelect);
            var GameMaster = newGame;
            return Json(GameMaster);
        }

        [HttpPost]
        [RequestSizeLimit(valueCountLimit: 1000000000)]
        [Route("NextTurn")]
        public JsonResult NextTurn(string GM)
        {
            GameMaster GameMaster = JsonConvert.DeserializeObject<GameMaster>(GM);
            GameMaster.turn++;
            if (GameMaster.turn == 4)
            {
                GameMaster.turn = 0;
            }
            if(GameMaster.players[GameMaster.turn].knocked == true)
            {
                GameOver endGame = new GameOver(GameMaster.players);
                GameMaster.endGame = endGame;
            }
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
            Player player = GameMaster.players[GameMaster.turn];
            player.hand.Add(GameMaster.deck.DiscardPile[0]);
            GameMaster.deck.DiscardPile.RemoveAt(0);

            return Json(GameMaster);
        }

        [HttpPost]
        [RequestSizeLimit(valueCountLimit: 1000000000)]
        [Route("DiscardCard")]
        public JsonResult DiscardCard(string GM)
        {
            GameMaster GameMaster = JsonConvert.DeserializeObject<GameMaster>(GM);
            Player player = GameMaster.players[GameMaster.turn];
            for(int i = 0; i < player.hand.Count; i ++)
            {
                if(player.hand[i].selected)
                {  
                    player.hand[i].selected = false; // remove discard attribute
                    GameMaster.deck.DiscardPile.Insert(0, player.hand[i]);
                    player.hand.RemoveAt(i);
                }
            }
            player.hand_value = HandValue.Calculate(player);
            if(GameMaster.deck.deck.Count == 0)
            {
                GameOver endGame = new GameOver(GameMaster.players);
                GameMaster.endGame = endGame;
            }
            if(player.hand_value == 31 || player.hand_value == 32)
            {
                GameOver endGame = new GameOver(player, GameMaster.players);
                GameMaster.endGame = endGame;
            }
            return Json(GameMaster);
        }
    }
}