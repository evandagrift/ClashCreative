using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClashCreative.Models
{
    public class ClashJson
    {
        private readonly IHttpClientFactory _clientFactory;

        public ClashJson(IHttpClientFactory f)
        {
            _clientFactory = f;
            //Player p = await GetPlayerData("#29PGJURQL");
        }

        //returns the Player Object from deserielized JSON
        public async Task<Player> GetPlayerData(string playerID)
        {
            string connectionString = "/v1/players/%23" + playerID.Substring(1);
            var client = _clientFactory.CreateClient("API Client");
            var result = await client.GetAsync(connectionString);
            Console.WriteLine();

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var player = JsonConvert.DeserializeObject<Player>(content);
                player.ClanTag = player.Clan.Tag;
                return player;
            }
            return null;
        }

        public async Task<List<Card>> GetAllCards() 
        { 
            string connectionString = "/v1/cards?";

            var client = _clientFactory.CreateClient("API Client");
            var result = await client.GetAsync(connectionString);

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();

                //removing the base layer from called Json
                content = content.Substring(9, content.Length - 10);
                return JsonConvert.DeserializeObject<List<Card>>(content); ;
            }
            return null;
        }


        //returns Clan Object from deserielized JSON
        public async Task<Clan> GetClanData(string clanID)
        {
            string connectionString = "/v1/clans/%23" + clanID.Substring(1);
            var client = _clientFactory.CreateClient("API Client");

            var result = await client.GetAsync(connectionString);

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Clan>(content);
            }
            return null;
        }

        //gets battle data from JSON/api
        public async Task<List<Battle>> GetListOfBattles(string playerID)
        {
            string connectionString = "/v1/players/%23" + playerID.Substring(1) + "/battlelog/";

            var client = _clientFactory.CreateClient("API Client");

            var result = await client.GetAsync(connectionString);

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var battles = JsonConvert.DeserializeObject<List<Battle>>(content);
               // battles.ForEach(b => { b.Opponent.})
                return battles;
            }
            return null;
        }
    }
}
