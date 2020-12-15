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
        private ClashContext context;
        public HomeController(ClashContext c, IHttpClientFactory f)
        {
            context = c;
            _clientFactory = f;
        }

        public async Task<IActionResult> Index()
        {
            if (context.Cards.Count() == 0)
            {
                await LazySeed();
            }

            //creates an instance of the model because the constructor holds crucial formatting variables
            HomePageModel model = new HomePageModel();

            return View(model);
        }

        public async Task<bool> LazySeed()
        {
            ClashJson clashJson = new ClashJson(_clientFactory);
            ClashDB clashDB = new ClashDB(context);
            DateTime now = DateTime.Now;

            //Gets all cards in game from Clash API
            List<Card> cards = await clashJson.GetAllCards();

            //sets the img URL Strings foar all cards
            //this is done because URL is called from API via IDictionary
            cards.ForEach(c => { c.SetUrl(); });

            //adds cards to context to be saved Async because there are many results
            await context.Cards.AddRangeAsync(cards);

            var clan = await clashJson.GetClanData("#8CYPL8R");

            clan.UpdateTime = now;
            context.Clans.Add(clan);

            List<Player> playersToAdd = new List<Player>();
            List<Battle> battlesToAdd = new List<Battle>();
            int savedBattles = 0;

            //fills the list of player data to add to the DB
            // for (int m = 0; m < clan.Members; m++)

            for (int m = 0; m < 25; m++)
            {
                //gets basic player
                var player = await clashJson.GetPlayerData(clan.MemberList[m].Tag);
                player = await clashDB.FillPlayerDBData(player);
                playersToAdd.Add(player);
            }


            context.Players.AddRange(playersToAdd);

            for (int p = 0; p < playersToAdd.Count(); p++)
            {
                var battles = await clashJson.GetListOfBattles(playersToAdd[p].Tag);
                battles = battles.GetRange(0, 10);

                savedBattles += await clashDB.SaveBattles(battles);
            }

            context.SaveChanges();

            var teamID = context.Team.OrderBy(t => t.TeamName).ToList();

            int indexTo = 10;
            for (int i = 0; i < indexTo; i++)
            {
                var player = await clashJson.GetPlayerData(teamID[i].Tag);
                if (player.Clan != null)
                {
                    player.Clan = await clashJson.GetClanData(player.Clan.Tag);
                    context.Clans.Add(player.Clan);
                }
                else indexTo++;
            }
            context.SaveChanges();
            return true;
        }

    }
}


//saving in case I want later
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

