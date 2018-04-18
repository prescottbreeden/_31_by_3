using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using _31_by_3.Models;

namespace _31_by_3.Controllers
{
    public class CardsController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult PlayerSelect()
        {
            return View();
        }

        [HttpPost]
        [Route("players")]
        public IActionResult Players(string player1, string player2, string player3, string player4)
        {
            if(player1 == null)
            {
                player1 = "zxc";
            }
            if(player2 == null)
            {
                player2 = "zxc";
            }
            if(player3 == null)
            {
                player3 = "zxc";
            }
            if(player4 == null)
            {
                player4 = "zxc";
            }
            HttpContext.Session.SetString("player1", player1);
            HttpContext.Session.SetString("player2", player2);
            HttpContext.Session.SetString("player3", player3);
            HttpContext.Session.SetString("player4", player4);
            System.Console.WriteLine(player1);
            System.Console.WriteLine(player2);
            System.Console.WriteLine(player3);
            System.Console.WriteLine(player4);
            return RedirectToAction("Main");
        }

        [HttpGet]
        [Route("main")]
        public IActionResult Main()
        {
            ViewBag.please = "~";
            return View();
        }
    }
}
