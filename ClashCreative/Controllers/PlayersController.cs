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

        public PlayersController(ClashContext c, IHttpClientFactory f)
        {
            context = c;
            _clientFactory = f;
        }

        //clan page from searched clan
        [HttpPost]
        public async Task<IActionResult> Clans(Clan clan)
        {
            //json handler and page model
            ClashJson clashJson = new ClashJson(_clientFactory);
            ClansModel model = new ClansModel(context);

            Clan returnClan = new Clan();
            //tries to get the clan data, if it fails or remains null it will be tagged "invalid"
            try
            {
                returnClan = await clashJson.GetClanData(clan.Tag);
            }
            catch
            {
                returnClan = new Clan();
                returnClan.Tag = "invalid";
            }
            if (returnClan == null)
            {
                returnClan = new Clan();
                returnClan.Tag = "invalid";
            }

            //loads the searched clan into the model
            model.SearchedClan = returnClan;

            return View(model);
        }

        //players page from searched player
        [HttpPost]
        public async Task<IActionResult> Players(Player player)
        {

            ClashJson clashJson = new ClashJson(_clientFactory);
            ClashDB clashDB = new ClashDB(context);

            //int counting cards of game so player can have x/cards
            int cardsInGame = context.Cards.Count();


            PlayersModel model = new PlayersModel(context);
            Player returnPlayer = new Player();

            //tries to get the player from Clash Api if not retrieved sets tags to "invalid"
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

            //for all the players in the list it populates their data 
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
                        //sets the clan tag
                        model.Players[p].ClanTag = model.Players[p].Clan.Tag;
                    }
                }

            }

            //if the selected tag is searchable it fill's the player's data
            if (returnPlayer.Tag != null && returnPlayer.Tag != "invalid")
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

            //model's searched player is set
            model.SearchedPlayer = returnPlayer;

            //returns the model to the view
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

            if (model.Players != null)
            {
                for (int p = 0; p < model.Players.Count(); p++)
                {
                    //if the player is a valid player it fills the model
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




            //if this player hasn't been logged add them to the DB
            if (existingPlayer == null)
            {
                context.Players.Add(playerUpdated);
                context.SaveChanges();
            }
            return View(model);
        }

    }
}

//string myPlayerTag = "#29PGJURQL";
//string myClanTag = "#8CYPL8R";