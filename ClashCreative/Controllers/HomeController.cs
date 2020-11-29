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
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<HomeController> _logger;
        ClashContext context;
        public HomeController(ClashContext c, IHttpClientFactory f)
        {
            context = c;
            _clientFactory = f;
        }
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
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
      

        private async Task<Player> GetPlayerData(string playerID)
        {
            string connectionString = "/v1/players/%23" + playerID.Substring(1);
            var client = _clientFactory.CreateClient("API Client");
            var result = await client.GetAsync(connectionString);
            Console.WriteLine();

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Player>(content); 
            }
            return null;
        }
        private async Task<Clan> GetClanData(string clanID)
        {
            string connectionString = "/v1/clans/%23" + clanID.Substring(1);
            var client = _clientFactory.CreateClient("API Client");

            var result = await client.GetAsync(connectionString);

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Clan>(content);
            }
            return null;
        }



        private async Task<List<Battle>> GetBattleData(string playerID)
        {
            string connectionString = "/v1/players/%23" + playerID.Substring(1) + "/battlelog/";

            var client = _clientFactory.CreateClient("API Client");

            var result = await client.GetAsync(connectionString);

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                Console.WriteLine();
                return JsonConvert.DeserializeObject<List<Battle>>(content);
            }
            return null;
        }



        private async Task<List<Card>> GetCards()
        {
            string connectionString = "/v1/cards?limit=&after=0&before=102";
            // Get an instance of HttpClient from the factpry that we registered
            // in Startup.cs
            var client = _clientFactory.CreateClient("API Client");

            // Call the API & wait for response. 
            // If the API call fails, call it again according to the re-try policy
            // specified in Startup.cs
            var result = await client.GetAsync(connectionString);
            Console.WriteLine();

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                Console.WriteLine();
                //removing the base layer from called Json
                content = content.Substring(9, content.Length - 10);
                return JsonConvert.DeserializeObject<List<Card>>(content);
            }
            return null;
        }
        public async Task<int> GetDeckId(Deck d)
        {
            var decks = context.Decks.OrderBy(p => p.DeckId).ToList();

            int deckId = -1;

            decks.ForEach(a => {
                d.SortCards();
                if(a.Card1Id == d.Card1Id &
                a.Card2Id == d.Card2Id &
                a.Card3Id == d.Card3Id &
                a.Card4Id == d.Card4Id &
                a.Card5Id == d.Card5Id &
                a.Card6Id == d.Card6Id &
                a.Card7Id == d.Card7Id &
                a.Card8Id == d.Card8Id)
                { 
                    deckId = a.DeckId; 
                }
            });

            if (deckId == -1)
            {
                context.Decks.Add(d);
                context.SaveChanges();
                return context.Decks.Count();

            }
            else { return deckId; }

        }


            public async Task<bool> GetClanMembersData(string tag)
        {
            DateTime now = DateTime.Now;
            var clan = await GetClanData(tag);
            List<Player> playersToAdd = new List<Player>();

            for(int i = 0; i < clan.Members; i++)
            {
                var p = await GetPlayerData(clan.MemberList[i].Tag);
                Deck d = p.CurrentDeck;
                p.ClanTag = clan.Tag;
                p.LastSeen = clan.MemberList[i].LastSeen;

                p.UpdateTime = now;
                Console.WriteLine();

                //create function to Get/Set Deck ID
                //function will return DeckId If the deck doesn't exist it adds it and saves it and returns the created ID

                var deckId = await GetDeckId((Deck)d);
                p.DeckId = deckId;
                p.CurrentDeck = null;
                if(p.DeckId==-1)
                {
                    Console.WriteLine();
                }
                playersToAdd.Add(p);
            }

            Console.WriteLine();


            clan.UpdateTime = now;
            await context.Clans.AddAsync(clan);
            await context.Players.AddRangeAsync(playersToAdd);
            context.SaveChanges();
            return true;
        }
        public async Task<IActionResult> Index()
        {
            string myPlayerTag = "#29PGJURQL";
            string myClanTag = "#8CYPL8R";
            string sorenClanTag = "#L2JUGRVR"; 
            string aPlayerTag = "#9QGPC82Y0";
            string dangerFrog = "#8UUQVLP0";

            //await GetClanMembersData(sorenClanTag);
           //await GetClanMembersData(myClanTag);

            var me = await GetPlayerData(myPlayerTag);
            var me2 = await GetPlayerData(myPlayerTag);

            me.CurrentDeck.SortCards();
            me2.CurrentDeck.SortCards();
            context.Decks.Add(me.CurrentDeck);
            context.Decks.Add(me2.CurrentDeck);
            context.SaveChanges();

            // Deck myDeck = me.CurrentDeck;
            // myDeck.DeckId = await GetDeckId(myDeck);

            //var frog = await GetPlayerData(myPlayerTag);
            //Deck frogDeck = frog.CurrentDeck;
            // frogDeck.DeckId = await GetDeckId(frogDeck);

            // bool v = (frogDeck == myDeck);
            Console.WriteLine();
          //  player.UpdateTime = DateTime.UtcNow;

          ////  var cards = await GetCards();

          // // await context.AddRangeAsync(cards);
          //  await context.AddAsync(player.CurrentDeck);
          // // await context.Players.AddAsync(player);
          //  context.SaveChanges();


          //  //var battle = await GetBattleData(myPlayerTag);
          //  Console.WriteLine();

          //  // Pass the data into the View
            return View();
        }
    }
}
