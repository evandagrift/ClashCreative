using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polly;


namespace ClashCreative.Models
{
    public class UpdateDB
    {
        private ClashContext context;
        private readonly IHttpClientFactory _clientFactory;

        public UpdateDB(ClashContext c, IHttpClientFactory f)
        {
            context = c;
            _clientFactory = f;
            //Player p = await GetPlayerData("#29PGJURQL");
        }


        public async Task Update()
        {
           // var player = await GetPlayerData("#29PGJURQL");
           //var clan = await GetClanData(player.ClanTag);

            var battles = await GetListOfBattles("#88CPPQC0V");
            Temp(battles);
            Console.WriteLine();
        }


        public void Temp(List<Battle> battles)
        {
            battles.ForEach(b => {
                b.GameModeId = b.GameMode.Id;

                int id = GetTeamId(b.Team);
                if (b.Team.Count == 2)
                {
                    Console.WriteLine();
                }

            });

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
                return JsonConvert.DeserializeObject<List<Battle>>(content);
            }
            return null;
        }


        public async Task<bool> AddClanMembersDataToDB(string tag)
        {
            DateTime now = DateTime.Now;
            var clan = await GetClanData(tag);
            List<Player> playersToAdd = new List<Player>();

            for (int i = 0; i < clan.Members; i++)
            {
                var p = await GetPlayerData(clan.MemberList[i].Tag);
                Deck d = new Deck(p.CurrentDeck);

                
                p.LastSeen = clan.MemberList[i].LastSeen; p.UpdateTime = now;

                Console.WriteLine();

                //create function to Get/Set Deck ID
                //function will return DeckId If the deck doesn't exist it adds it and saves it and returns the created ID

                var deckId = GetDeckID(d);
                p.CurrentDeckId = deckId;


                if (p.CurrentDeckId == -1)
                {
                    Console.WriteLine();
                }
                playersToAdd.Add(p);
            }

            Console.WriteLine();


            clan.UpdateTime = now;
            await context.Clans.AddAsync(clan);
            await context.Players.AddRangeAsync(playersToAdd);
            context.SaveChanges();
            return true;
        }



        public int GetTeamId(List<TeamMember> teamMembers)
        {
            int teamId = -1;

            bool twoVtwo = false;
            if (teamMembers.Count == 2) twoVtwo = true;

            var teams = context.Team.Where(t => t.TwoVTwo == twoVtwo).ToList();

            if (!twoVtwo)
            {
                teams.ForEach(a =>
                {
                    if (a.Tag == teamMembers[0].Tag)
                    {
                        teamId = a.TeamId;
                    }
                });
            }
            else { 
                teams.ForEach(a =>
                {
                    if (a.Tag == teamMembers[0].Tag && a.Tag2 == teamMembers[1].Tag || 
                    a.Tag == teamMembers[1].Tag && a.Tag2 == teamMembers[0].Tag)
                    {
                        teamId = a.TeamId;
                    }
                });
            }

            if (teamId == -1)
            {
                Team teamToAdd = new Team();
                teamToAdd.Tag = teamMembers[0].Tag;
                teamToAdd.Name = teamMembers[0].Name;

                if (twoVtwo)
                {
                    teamToAdd.TwoVTwo = true;
                    teamToAdd.Tag2 = teamMembers[1].Tag;
                    teamToAdd.Name2 = teamMembers[1].Name;
                }
                context.Team.Add(teamToAdd);
                context.SaveChanges();
            }
            return teamId;
        }
        #region Completed

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
                player.CurrentDeckId = GetDeckID(new Deck(player.CurrentDeck));
                return player;
            }
            return null;
        }

        public int GetDeckID(Deck d)
        {
            var decks = context.Decks.OrderBy(p => p.DeckId).ToList();
            int deckId = -1;
            d.SortCards();
            decks.ForEach(a => {
                a.SortCards();
                if (
                a.Card1Id == d.Card1Id &&
                a.Card2Id == d.Card2Id &&
                a.Card3Id == d.Card3Id &&
                a.Card4Id == d.Card4Id &&
                a.Card5Id == d.Card5Id &&
                a.Card6Id == d.Card6Id &&
                a.Card7Id == d.Card7Id &&
                a.Card8Id == d.Card8Id)
                {
                    deckId = a.DeckId;
                }
            });
            if (deckId == -1)
            {
                if (decks.Count() > 0)
                {
                    int count = decks.Count();
                    deckId = decks[count - 1].DeckId + 1;
                }
                else { deckId = 1; }
                d.DeckId = deckId;
                context.Decks.Add(d);
                context.SaveChanges();
            }
            return deckId;
        }
        //calls the API and puts all cards in the game in DB
        public async Task<bool> PutCardsInDB()
        {
            string connectionString = "/v1/cards?";

            var client = _clientFactory.CreateClient("API Client");
            var result = await client.GetAsync(connectionString);

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();

                //removing the base layer from called Json
                content = content.Substring(9, content.Length - 10);
                var cards = JsonConvert.DeserializeObject<List<Card>>(content);
                context.Cards.AddRange(cards);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        #endregion
    }
}

