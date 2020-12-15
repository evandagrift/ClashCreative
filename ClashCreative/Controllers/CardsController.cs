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

        //ClientFactory is for calling Json Data from Clash Api
        public CardsController(ClashContext c, IHttpClientFactory f)
        {
            context = c;
            _clientFactory = f;
        }

        //page showing all cards directing to the clicked cards wiki
        public async Task<IActionResult> Cards()
        {

            var cardsFrmDB = context.Cards.ToList();
            return View(cardsFrmDB);
        }

        //the top 10 ranking decks out of the collection in the DB
        //This probably should be delegated to it's own function somewhere but alas
        public async Task<IActionResult> Decks()
        {
            //My Json and DB handling classes
            ClashJson clashJson = new ClashJson(_clientFactory);
            ClashDB clashDB = new ClashDB(context);

            //gets all logged decks(every deck configuraiton is assigned a unique ID)
            var allDecks = context.Decks.ToList();

            //all of the logged battles
            var allBattles = context.Battles.ToList();
            //list to hold highest ranking decks
            List<Deck> notableDecks = new List<Deck>();

            if (allDecks.Count() > 0)
            {
                //goes throug each logged deck
                allDecks.ForEach(d =>
                {
                //grabs all logged 1v1 battles using the selected deck from DB (not doing 2v2 yet because it's more complex)
                var deckBattles = allBattles.Where(a => a.Type == "PvP").Where(a => (a.Team1DeckAId == d.DeckId)).ToList();

                //for each of those battles
                deckBattles.ForEach(b =>
                    {
                    //if the deck won or lost that battle it's score is increased or decreased
                        if (b.Team1Win) { d.Wins++; }
                        else { d.Loss++; }
                    });

                });
                //now that win/loss data is added to allDecks


                //we cycle back through all the decks again and assign them a rough win loss rate
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

                //lists decks with wins more than five(so that it only pulls decks that have sufficient-ish data)
                //orders the decks by winLoss rate and grabs the highest 10
                notableDecks = allDecks.OrderByDescending(d => d.WinLossRate).ToList().GetRange(0, 10);

                //the selected 10 decks are cycled through and the associated card classes are filled in with all their data from the DB
                //(cards have static ID's and they are used to access additional details about the cards played other than Id)
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
            }
            //passes the list of top 10 decks to the View
            return View(notableDecks);
        }
    }
}
