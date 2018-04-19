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
        [RequestSizeLimit(valueCountLimit: 10000)]
        [Route("DrawDeck")]
        public JsonResult DrawDeck(string GM)
        {
            GameMaster GameMaster = JsonConvert.DeserializeObject<GameMaster>(GM);
            System.Console.WriteLine(GameMaster.deck.deck[0]);
            System.Console.WriteLine(GameMaster.deck.DiscardPile[0]);
            GameMaster.players[GameMaster.Turn].hand.Add(GameMaster.deck.deck[0]);
            GameMaster.deck.deck.RemoveAt(0);
            
            return Json(GameMaster);
        }
    }
}