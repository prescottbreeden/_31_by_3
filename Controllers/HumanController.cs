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
            List<Player> Players = GamePlay.CreatePlayers(PlayerSelect);
            Deck CurrentDeck = GamePlay.BuildAndShuffle();
            GamePlay.Deal(Players, CurrentDeck);
            for(var i = 0; i < Players.Count; i++)
            {
                Players[i].hand_value = HandValue.Calculate(Players[i]);
            }
            var GameMaster = new { Players = Players, Deck = CurrentDeck, Turn = 0, knocked = false};
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
    }
}