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


            var allDecks = context.Decks.ToList();
            var allBattles = context.Battles.ToList();
            List<Deck> notableDecks = new List<Deck>();

            //for each deck
            allDecks.ForEach(d =>
            {

                var deckBattles = allBattles.Where(a =>a.Type=="PvP").Where(a => (a.Team1DeckAId == d.DeckId || a.Team1DeckBId == d.DeckId)).ToList();
                deckBattles.ForEach(b =>
                {
                        if (b.Team1Win) { d.Wins++; }
                        else { d.Loss++; }
                });

            });


            allDecks.ForEach(d =>
            {
                if (d.Wins > 0)
                {
                    if (d.Loss == 0)
                    {
                        d.WinLossRate = d.Wins / 1;
                    }
                    else { d.WinLossRate = d.Wins / d.Loss; }
                    
                }
            });

            notableDecks = allDecks.Where(d => d.Wins > 5).OrderByDescending(d => d.WinLossRate).ToList().GetRange(0,10);
            notableDecks.ForEach(d =>
            {
                d.Card1 = context.Cards.Find(d.Card1Id);
                d.Card2 = context.Cards.Find(d.Card2Id);
                d.Card3 = context.Cards.Find(d.Card3Id);
                d.Card4 = context.Cards.Find(d.Card4Id);
                d.Card5 = context.Cards.Find(d.Card5Id);
                d.Card6 = context.Cards.Find(d.Card6Id);
                d.Card7 = context.Cards.Find(d.Card7Id);
                d.Card8 = context.Cards.Find(d.Card8Id);
            });
            
            return View(notableDecks);
        }
    }
}
