﻿using System;
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
            //Class that accesses Clash Royale Api
            ClashJson clashJson = new ClashJson(_clientFactory);

            //Class for DB interacting functions
            ClashDB clashDB = new ClashDB(context);

            //Note:Do this better at later date
            //couldn't figure out how to seed if empty w/ out repos in a timely matter
            if (context.Cards.Count() < 1)
            {
                //accesses json functions via IHttpClientFactory _clientFactory to make the API calls
                clashJson = new ClashJson(_clientFactory);
                
                //Gets all cards in game from Clash API
                List<Card> cards = await clashJson.GetAllCards();

                //sets the img URL Strings foar all cards
                //this is done because URL is called from API via IDictionary
                cards.ForEach(c => { c.SetUrl(); });

                //adds cards to context to be saved Async because there are many results
                await context.Cards.AddRangeAsync(cards);


                //saves cards to DB
                context.SaveChanges();
            }
            
                //creates an instance of the model because the constructor holds crucial formatting variables
                HomePageModel model = new HomePageModel();

            return View(model);
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

