using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ClashCreative.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClashCreative.Controllers
{
    public class CardsController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private ClashContext context;
        private ClashDB clashDB;
        private ClashJson clashJson;

        //string myPlayerTag = "#29PGJURQL";
        //string myClanTag = "#8CYPL8R";
        public CardsController(ClashContext c, IHttpClientFactory f)
        {
            context = c;
            _clientFactory = f;
        }

        public async Task<IActionResult> Cards()
        {

            var cardsFrmDB = context.Cards.ToList();
            return View(cardsFrmDB);
        }

        public async Task<IActionResult> Decks()
        {
            ClashJson clashJson = new ClashJson(_clientFactory);
            ClashDB clashDB = new ClashDB(context);

            Deck d = new Deck();
            d = context.Decks.FirstOrDefault();
            d.Card1 = context.Cards.Find(d.Card1Id);
            d.Card2 = context.Cards.Find(d.Card2Id);
            d.Card3 = context.Cards.Find(d.Card3Id);
            d.Card4 = context.Cards.Find(d.Card4Id);
            d.Card5 = context.Cards.Find(d.Card5Id);
            d.Card6 = context.Cards.Find(d.Card6Id);
            d.Card7 = context.Cards.Find(d.Card7Id);
            d.Card8 = context.Cards.Find(d.Card8Id);
            List<Deck> decks = new List<Deck>();
            decks.Add(d);
            return View(decks);
        }
    }
}
