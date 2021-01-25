using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RoyaleTrackerAPI.Models;
using RoyaleTrackerAPI.Repos;
using RoyaleTrackerClasses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoyaleTrackerAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DecksController : ControllerBase
    {
        //Authentication Manager for handling Bearer Token
        private readonly ICustomAuthenticationManager customAuthenticationManager;

        //context to DB and Repo for handling
        private TRContext context;
        private DecksRepo repo;

        //loading in injected dependancies
        public DecksController(ICustomAuthenticationManager m, TRContext c)
        {
            customAuthenticationManager = m;
            // commented out while testing 
            context = c;

            //init the repo with DB context
            repo = new DecksRepo(context);
        }

        // POST api/Decks
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public void Post([FromBody] Deck deck)
        {
            repo.AddDeck(deck);
            context.SaveChanges();
        }

        [Authorize(Policy = "AdminOnly")]
        // GET: api/Decks
        [HttpGet]
        public string Get()
        {
            List<Deck> decks = repo.GetAllDecks();

            return JsonConvert.SerializeObject(decks, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

        }

        [Authorize(Policy = "AdminOnly")]
        // GET api/Decks/deckTag
        [HttpGet("{deckID}", Name = "GetDeck")]
        public string Get(int deckID)
        {
            Deck deck = repo.GetDeckByID(deckID);
            return JsonConvert.SerializeObject(deck, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }


        [Authorize(Policy = "AdminOnly")]
        // DELETE: api/Decks/{deckTag}
        [HttpDelete("{deckID}")]
        public void Delete(int deckID)
        {
            repo.DeleteDeck(deckID);
            context.SaveChanges();
        }

        [Authorize(Policy = "AdminOnly")]
        // Update: api/Decks
        [HttpPut]
        public void Update([FromBody] Deck deck)
        {
            repo.UpdateDeck(deck);
            context.SaveChanges();
        }

    }
}
