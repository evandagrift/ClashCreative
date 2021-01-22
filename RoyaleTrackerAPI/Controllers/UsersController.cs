using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RoyaleTrackerAPI.Models;
using RoyaleTrackerClasses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoyaleTrackerAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ICustomAuthenticationManager customAuthenticationManager;
        TRContext context;
        public UsersController(ICustomAuthenticationManager customAuthenticationManager, TRContext context)
        {
            this.customAuthenticationManager = customAuthenticationManager;
            // commented out while testing 
            //this.context = context;

            // plug in fake context
            //seed an Admin User
            


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
