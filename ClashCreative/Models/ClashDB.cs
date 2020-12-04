using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polly;


namespace ClashCreative.Models
{
    public class ClashDB
    {
        private ClashContext context;

        public ClashDB(ClashContext c)
        {
            context = c;
            //Player p = await GetPlayerData("#29PGJURQL");
        }


        //public async Task Update()
        //{
        //    Console.WriteLine();
        //}



        public async Task<bool> AddClanMembersDataToDB(string clanTag, ClashJson cJson)
        {
            DateTime now = DateTime.Now;
            var clan = await cJson.GetClanData(clanTag);
            List<Player> playersToAdd = new List<Player>();
            List<Battle> battlesToAdd = new List<Battle>();
            for (int i = 0; i < clan.Members; i++)
            {
                var p = await cJson.GetPlayerData(clan.MemberList[i].Tag);
                Deck d = new Deck(p.CurrentDeck);


                p.LastSeen = clan.MemberList[i].LastSeen; p.UpdateTime = now;

                Console.WriteLine();

                //create function to Get/Set Deck ID
                //function will return DeckId If the deck doesn't exist it adds it and saves it and returns the created ID

                var deckId = GetSetDeckID(d);
                p.CurrentDeckId = deckId;


                if (p.CurrentDeckId == -1)
                {
                    Console.WriteLine();
                }
                p.TeamId = GetSetTeamId(p);
                playersToAdd.Add(p);
                List<Battle> playerBattles = await cJson.GetListOfBattles(p.Tag);
                battlesToAdd.AddRange(playerBattles);
            }

            Console.WriteLine();


            clan.UpdateTime = now;
            await context.Clans.AddAsync(clan);
            await context.Players.AddRangeAsync(playersToAdd);
            int f = await SaveBattles(battlesToAdd);

            //context.SaveChanges();
            return true;
        }

        //returns number of saved battles
        public async Task<int> SaveBattles(List<Battle> battles)
        {
            int linesSaved = 0;
            //The last battle saved in the DB
            //Note:need to apply a where so it aligns per user
            var lastSavedBattle = context.Battles.OrderByDescending(b => b.BattleId).FirstOrDefault();

            //if there are items in the db it'll remove up until the most recent game 
            if (lastSavedBattle != null)
            {
                for(int i = 0; i < battles.Count; i++)
                {
                    if(lastSavedBattle.BattleTime == battles[i].BattleTime)
                    {
                        //this is the item before the items you want
                        battles.RemoveRange(0, i+1);
                    }
                }
            }

          battles.ForEach( b =>
            {
                //plop in all those variables
                b.Team1Name = b.Team[0].Name;
                if (b.Team.Count > 1) { b.Team1Name += " " + b.Team[1].Name; }

                b.Team1Id = GetSetTeamId(b.Team);

                if (b.Team[0].Crowns > b.Opponent[0].Crowns)
                { b.Team1Win = true; }
                else { b.Team1Win = false; }

                b.Team1StartingTrophies = b.Team[0].StartingTrophies;
                b.Team1TrophyChange = b.Team[0].TrophyChange;

                Deck d = new Deck(b.Team[0].Cards);
                b.Team1DeckA = GetSetDeckID(d);

                if (b.Team.Count > 1)
                {
                    d = new Deck(b.Team[1].Cards);
                    b.Team1DeckB = GetSetDeckID(d);
                }

                b.Team1Crowns = b.Team[0].Crowns;

                b.Team1KingTowerHp = b.Team[0].KingTowerHitPoints;
                b.Team1PrincessTowerHpA = b.Team[0].PrincessTowerA;
                b.Team1PrincessTowerHpB = b.Team[0].PrincessTowerB;


                //Team 2
                b.Team2Name = b.Opponent[0].Name;
                if (b.Opponent.Count > 1) { b.Team2Name += " " + b.Opponent[1].Name; }

                b.Team2Id = GetSetTeamId(b.Opponent);

                if (b.Opponent[0].Crowns > b.Team[0].Crowns)
                { b.Team2Win = true; }
                else { b.Team2Win = false; }

                b.Team2StartingTrophies = b.Opponent[0].StartingTrophies;
                b.Team2TrophyChange = b.Opponent[0].TrophyChange;

                d = new Deck(b.Opponent[0].Cards);
                b.Team2DeckA = GetSetDeckID(d);

                if (b.Opponent.Count > 1)
                {
                    d = new Deck(b.Opponent[1].Cards);
                    b.Team2DeckB = GetSetDeckID(d);
                }

                b.Team2Crowns = b.Opponent[0].Crowns;

                b.Team2KingTowerHp = b.Opponent[0].KingTowerHitPoints;
                b.Team2PrincessTowerHpA = b.Opponent[0].PrincessTowerA;
                b.Team2PrincessTowerHpB = b.Opponent[0].PrincessTowerB;


                b.GameModeId = b.GameMode.Id;

                    context.Battles.Add(b);
                    context.SaveChanges();
                    linesSaved++;
                //var team = b.Team;
                //var opponent = b.Opponent;

                //clashDB.GetSetTeamId(team);
                //clashDB.GetSetTeamId(opponent);
            });
            Console.WriteLine();


            return linesSaved;
        }


        #region Completed

        //any unique team of players, a, b, a+b... is saved and given a team Id by the database
        public int GetSetTeamId(Player player)
        {
            List<TeamMember> t = new List<TeamMember>();
            TeamMember p = new TeamMember();
            p.Tag = player.Tag;
            p.Name = player.Name;
            t.Add(p);
            return GetSetTeamId(t);
        }
            public int GetSetTeamId(List<TeamMember> teamMembers)
        {
            //return variable, if it remains at -1 flags if statement to create new team Id
            int teamId = -1;

            //defaults to 1 player
            bool twoVtwo = false;

            //if there are two players in the team members list, sets to 2v2
            if (teamMembers.Count == 2) twoVtwo = true;

            //searches db for all teams and focuses that search based on wether on not it's 2v2
            var teams = context.Team.Where(t => t.TwoVTwo == twoVtwo).ToList();

            //if it's
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
            else
            {
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
                teamToAdd.TeamName = teamMembers[0].Name;

                if (twoVtwo)
                {
                    teamToAdd.TwoVTwo = true;
                    teamToAdd.Tag2 = teamMembers[1].Tag;
                    teamToAdd.Name2 = teamMembers[1].Name;
                    teamToAdd.TeamName += " " + teamMembers[1].Name;
                }
                context.Team.Add(teamToAdd);
                context.SaveChanges();

                //sets return Id to the newly set Id within the Team model
                teamId = teamToAdd.TeamId;
            }
            return teamId;
        }

        //sorts the cards so no duplicates will be logged
        //looks for the deck to get the decks Id
        //if it doesn't exist creates it in the DB and hands back the DB generated Id
        //Note:this will need designing once db gets bigger
        public int GetSetDeckID(Deck d)
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
       

        #endregion
    }
}

