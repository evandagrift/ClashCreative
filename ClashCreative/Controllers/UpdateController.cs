using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClashCreative.Models;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace ClashCreative.Controllers
{
    public class UpdateController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private ClashContext context;

        //string myPlayerTag = "#29PGJURQL";
        //string myClanTag = "#8CYPL8R";
        public UpdateController(ClashContext c, IHttpClientFactory f)
        {
            context = c;
            _clientFactory = f;
        }
        public async Task<IActionResult> Index()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UpdateModel model)
        {

            ClashDB clashDB = new ClashDB(context);
            model.ClashDB = clashDB;
            ClashJson clashJson = new ClashJson(_clientFactory);
            model.ClashJson = clashJson;
            await model.SaveToDBByClan(context);
            return View(model);
        }


    }
}
