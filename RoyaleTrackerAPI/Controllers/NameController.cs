using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoyaleTrackerClasses;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoyaleTrackerAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NameController : ControllerBase
    {
        private readonly ICustomAuthenticationManager customAuthenticationManager;
        ClashContext context;

        public NameController(ICustomAuthenticationManager customAuthenticationManager, ClashContext context)
        {
            this.customAuthenticationManager = customAuthenticationManager;
            this.context = context;
        }

                                      
        [Authorize(Policy = "All")]
        // GET: api/<NameController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<NameController>/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CardController>
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public void Post([FromBody] User value)
        {
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User userCred)
        {
            var token = customAuthenticationManager.Authenticate(userCred.Username, userCred.Password, context);

            if(token == null)
                return Unauthorized();
            

            return Ok(token);
        }

    }
}
