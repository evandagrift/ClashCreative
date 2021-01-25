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
    public class BattlesController : ControllerBase
    {
        //Authentication Manager for handling Bearer Token
        private readonly ICustomAuthenticationManager customAuthenticationManager;

        //context to DB and Repo for handling
        private TRContext context;
        private BattlesRepo repo;

        //loading in injected dependancies
        public BattlesController(ICustomAuthenticationManager m, TRContext c)
        {
            customAuthenticationManager = m;
            context = c;
            repo = new BattlesRepo(context);
        }

        // POST api/Battles
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public void Post([FromBody] Battle battle)
        {
            repo.AddBattle(battle);
            context.SaveChanges();
        }

        [Authorize(Policy = "AdminOnly")]
        // GET: api/Battles
        [HttpGet]
        public string Get()
        {
            List<Battle> battles = repo.GetAllBattles();

            return JsonConvert.SerializeObject(battles, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

        }

        [Authorize(Policy = "AdminOnly")]
        // GET api/Battles/battleTID
        [HttpGet("{battleID}", Name = "GetBattle")]
        public string Get(int battleID)
        {
            Battle battle = repo.GetBattleByID(battleID);
            return JsonConvert.SerializeObject(battle, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }


        [Authorize(Policy = "AdminOnly")]
        // DELETE: api/Battles/{battleID}
        [HttpDelete("{battleID}")]
        public void Delete(int battleID)
        {
            repo.DeleteBattle(battleID);
            context.SaveChanges();
        }

        [Authorize(Policy = "AdminOnly")]
        // Update: api/Battles
        [HttpPut]
        public void Update([FromBody] Battle battle)
        {
            repo.UpdateBattle(battle);
            context.SaveChanges();
        }

    }
}
