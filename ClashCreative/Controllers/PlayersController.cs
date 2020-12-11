using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ClashCreative.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClashCreative.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private ClashContext context;

        //string myPlayerTag = "#29PGJURQL";
        //string myClanTag = "#8CYPL8R";
        public PlayersController(ClashContext c, IHttpClientFactory f)
        {
            context = c;
            _clientFactory = f;
        }

        [HttpPost]
        public async Task<IActionResult> Clans(Clan clan)
        {

            ClashJson clashJson = new ClashJson(_clientFactory);
            ClansModel model = new ClansModel(context);
            Clan returnClan = new Clan();
            try
            {
                returnClan = await clashJson.GetClanData(clan.Tag);
            }
            catch
            {
                returnClan = new Clan();
                returnClan.Tag = "invalid";
            }
            if(returnClan== null)
            {
                returnClan = new Clan();
                returnClan.Tag = "invalid";
            }
            model.SearchedClan = returnClan;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Players(Player player)
        {

            ClashJson clashJson = new ClashJson(_clientFactory);
            ClashDB clashDB = new ClashDB(context);
            int cardsInGame = context.Cards.Count();

            PlayersModel model = new PlayersModel(context);
            Player returnPlayer = new Player();
            try
            {
                returnPlayer = await clashJson.GetPlayerData(player.Tag);
            }
            catch
            {
                returnPlayer = new Player();
                returnPlayer.Tag = "invalid";
            }
            if (returnPlayer == null)
            {
                returnPlayer = new Player();
                returnPlayer.Tag = "invalid";
            }
            for (int p = 0; p < model.Players.Count(); p++)
            {
                if (model.Players[p].Tag != null && model.Players[p].Tag != "invalid")
                {
                    model.Players[p] = await clashJson.GetPlayerData(model.Players[p].Tag);
                    model.Players[p] = await clashDB.FillPlayerDBData(model.Players[p]);

                    model.Players[p].CurrentDeck.ForEach((d => { d.SetUrl(); }));
                    model.Players[p].Deck = new Deck(model.Players[p].CurrentDeck);
                    model.Players[p].Deck.SetCards(context);
                    model.Players[p].CardsInGame = cardsInGame;

                    if (model.Players[p].Clan != null)
                    {
                        model.Players[p].ClanTag = model.Players[p].Clan.Tag;
                    }
                }

            }

            if(returnPlayer.Tag != null && returnPlayer.Tag != "invalid")
            { 
                returnPlayer = await clashDB.FillPlayerDBData(returnPlayer);
                returnPlayer.ClanTag = returnPlayer.Clan.Tag;
                returnPlayer.CardsInGame = cardsInGame;

                returnPlayer.Deck.Card1.SetUrl();
                returnPlayer.Deck.Card2.SetUrl();
                returnPlayer.Deck.Card3.SetUrl();
                returnPlayer.Deck.Card4.SetUrl();
                returnPlayer.Deck.Card5.SetUrl();
                returnPlayer.Deck.Card6.SetUrl();
                returnPlayer.Deck.Card7.SetUrl();
                returnPlayer.Deck.Card8.SetUrl();
            }

            model.SearchedPlayer = returnPlayer;

            return View(model);
        }
        public async Task<IActionResult> Clans()
        {
            ClansModel model = new ClansModel(context);
            return View(model);
        }
        public async Task<IActionResult> Players()
        {
            ClashJson clashJson = new ClashJson(_clientFactory);
            ClashDB clashDB = new ClashDB(context);
            int cardsInGame = context.Cards.Count();

            PlayersModel model = new PlayersModel(context);
            for (int p = 0; p < model.Players.Count(); p++)
            {
                if (model.Players[p].Tag != null && model.Players[p].Tag != "invalid")
                {
                    model.Players[p] = await clashJson.GetPlayerData(model.Players[p].Tag);
                    model.Players[p] = await clashDB.FillPlayerDBData(model.Players[p]);

                    model.Players[p].CurrentDeck.ForEach((d => { d.SetUrl(); }));
                    model.Players[p].Deck = new Deck(model.Players[p].CurrentDeck);
                    model.Players[p].Deck.SetCards(context);
                    model.Players[p].CardsInGame = cardsInGame;

                    if (model.Players[p].Clan != null)
                    {
                        model.Players[p].ClanTag = model.Players[p].Clan.Tag;
                    }
                }

            }
            return View(model);
        }


        public async Task<IActionResult> PlayerData(Player player)
        {
            ClashDB clashDB = new ClashDB(context);
            ClashJson clashJson = new ClashJson(_clientFactory);

            PlayerDataModel model = new PlayerDataModel();
            Player existingPlayer = context.Players.Where(p => p.Tag == player.Tag).FirstOrDefault();

            Player playerUpdated = await clashJson.GetPlayerData(player.Tag);
            playerUpdated.CurrentFavouriteCard.SetUrl();
            playerUpdated = await clashDB.FillPlayerDBData(playerUpdated);
            playerUpdated.ClanTag = playerUpdated.Clan.Tag;

            playerUpdated.Deck = new Deck(playerUpdated.CurrentDeck);
            playerUpdated.Deck.SetCards(context);
            playerUpdated.CardsInGame = context.Cards.Count();
            model.Player = playerUpdated;




            //if this player hasn't been logged add the to the DB
            if (existingPlayer == null)
            {
                context.Players.Add(playerUpdated);
                context.SaveChanges();
            }
            return View(model);
        }

    }
}
