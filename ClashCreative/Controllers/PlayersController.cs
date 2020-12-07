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
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PlayerData(Player player)
        {
            ClashJson clashJson = new ClashJson(_clientFactory);

            Player playerUpdated = await clashJson.GetPlayerData(player.Tag);
            Deck deck = new Deck(playerUpdated.CurrentDeck);
            PlayerDataModel model = new PlayerDataModel();
            model.Player = playerUpdated;
            model.Deck = deck;
            model.Deck.SetCards(context);
            return View(model);
        }

    }
}
