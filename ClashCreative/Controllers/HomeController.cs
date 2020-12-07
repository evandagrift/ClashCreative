using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClashCreative.Models;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace ClashCreative.Controllers
{
    public class HomeController : Controller
    { 
        private readonly ILogger<HomeController> _logger;

        private readonly IHttpClientFactory _clientFactory;
        private ClashContext context;
        public HomeController(ClashContext c, IHttpClientFactory f)
        {
            context = c;
            _clientFactory = f;
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }





        public async Task<IActionResult> Index()
        {
            // var player = await GetPlayerData("#29PGJURQL");
            //var clan = await GetClanData(player.ClanTag);
            ClashJson clashJson = new ClashJson(_clientFactory);
            ClashDB clashDB = new ClashDB(context);


            if (context.Cards.Count() < 1)
            {
                clashJson = new ClashJson(_clientFactory);
                List<Card> cards = await clashJson.GetAllCards();
                cards.ForEach(c => { c.SetUrl(); });
                await context.Cards.AddRangeAsync(cards);
                context.SaveChanges();
            }

            //battles.ForEach(b =>
            //{
            //    var team = b.Team;
            //    var opponent = b.Opponent;

            //    clashDB.GetSetTeamId(team);
            //    clashDB.GetSetTeamId(opponent); 
            //List<Battle> battles = await clashJson.GetListOfBattles("#9V88U2CG2");
            //await clashDB.SaveBattles("#9V88U2CG2", battles);
            return View();
        }




    }
}
//string myPlayerTag = "#29PGJURQL";
//string myClanTag = "#8CYPL8R";
//string sorenClanTag = "#L2JUGRVR";
//string aPlayerTag = "#9QGPC82Y0";
//string dangerFrog = "#8UUQVLP0";

//await GetClanMembersData(sorenClanTag);
//await GetClanMembersData(myClanTag);

//var me = await GetPlayerData(myPlayerTag);
//var frog = await GetPlayerData(dangerFrog);
//var rando = await GetPlayerData(aPlayerTag);

//Console.WriteLine();



//Deck myDeck = new Deck(me.CurrentDeck);
//int myID =  GetDeckID(myDeck);

//Deck frogDeck = new Deck(frog.CurrentDeck);
//int frogID = GetDeckID(frogDeck);

//bool v = (frogDeck == myDeck);

//  var cards = await GetCards();

// // await context.AddRangeAsync(cards);
//  await context.AddAsync(player.CurrentDeck);
// // await context.Players.AddAsync(player);
//  context.SaveChanges();


//  //var battle = await GetBattleData(myPlayerTag);
//  Console.WriteLine();

//  // Pass the data into the View
/*
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
*/