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
        public async Task<RedirectToActionResult> Index(HomePageModel model)
        {
            if (model.Player != null || model.Clan !=null)
            {
                if (model.Player.Tag != null)
                {
                    ClashJson clashJson = new ClashJson(_clientFactory);

                    Player playerUpdated = await clashJson.GetPlayerData(model.Player.Tag);
                    if (playerUpdated != null)
                    {
                        return RedirectToAction("PlayerData", model.Player);
                    }
                    else
                    {
                        model.Warning = "The Tag you entered is not valid";
                        return RedirectToAction("Index", "Home", model);
                    }
                }
                else if (model.Clan.Tag != null)
                {
                    return RedirectToAction("ClanData", model.Clan);
                }
                else
                {
                    model.Warning = "The Tag you entered is not valid";
                    return RedirectToAction("Index", "Home", model);
                }
            }
            else
            {
                return RedirectToAction("AllPlayers", model);
            }
        }

        public async Task<IActionResult> AllPlayers()
        {
            return View();
        }
        public async Task<IActionResult> Clans(Player player)
        {
            return View();
        }
        public async Task<IActionResult> PlayerData(Player player)
        {
            ClashJson clashJson = new ClashJson(_clientFactory);

            Player playerUpdated = await clashJson.GetPlayerData(player.Tag);
            Deck deck = new Deck(playerUpdated.CurrentDeck);
            PlayerDataModel model = new PlayerDataModel();
            model.CardsInGame = context.Cards.Count();
            model.Player = playerUpdated;
            model.Deck = deck;
            model.Deck.SetCards(context);
            return View(model);
        }

    }
}
