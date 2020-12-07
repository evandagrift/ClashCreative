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

        #region Completed

        public async Task<int> SaveBattles(List<Battle> battles)
        {
            battles.OrderByDescending(b => b.BattleTime);
            int linesSaved = 0;


            List<Battle> battlesToSave = new List<Battle>();

            battles.ForEach((b) =>
            {
                b.Team1Id = GetSetTeamId(b.Team);
                b.Team2Id = GetSetTeamId(b.Opponent);

                var playerBattles = context.Battles.Where(t => t.Team1Id == b.Team1Id && t.BattleTime == b.BattleTime || t.Team2Id == b.Team1Id && t.BattleTime == b.BattleTime).FirstOrDefault();

                if (playerBattles == null)
                {
                    battlesToSave.Add(b);
                }
            });


            battlesToSave.ForEach(b =>
            {


                //plop in all those variables
                b.Team1Name = b.Team[0].Name;
                if (b.Team.Count > 1) { b.Team1Name += " " + b.Team[1].Name; }


                if (b.Team[0].Crowns > b.Opponent[0].Crowns)
                { b.Team1Win = true; }
                else { b.Team1Win = false; }

                b.Team1StartingTrophies = b.Team[0].StartingTrophies;
                b.Team1TrophyChange = b.Team[0].TrophyChange;

                Deck d = new Deck(b.Team[0].Cards);
                b.Team1DeckAId = GetSetDeckID(d);

                if (b.Team.Count > 1)
                {
                    d = new Deck(b.Team[1].Cards);
                    b.Team1DeckBId = GetSetDeckID(d);
                }

                b.Team1Crowns = b.Team[0].Crowns;

                b.Team1KingTowerHp = b.Team[0].KingTowerHitPoints;
                b.Team1PrincessTowerHpA = b.Team[0].PrincessTowerA;
                b.Team1PrincessTowerHpB = b.Team[0].PrincessTowerB;


                //Team 2
                b.Team2Name = b.Opponent[0].Name;
                if (b.Opponent.Count > 1) { b.Team2Name += " " + b.Opponent[1].Name; }


                if (b.Opponent[0].Crowns > b.Team[0].Crowns)
                { b.Team2Win = true; }
                else { b.Team2Win = false; }

                b.Team2StartingTrophies = b.Opponent[0].StartingTrophies;
                b.Team2TrophyChange = b.Opponent[0].TrophyChange;

                d = new Deck(b.Opponent[0].Cards);
                b.Team2DeckAId = GetSetDeckID(d);

                if (b.Opponent.Count > 1)
                {
                    d = new Deck(b.Opponent[1].Cards);
                    b.Team2DeckBId = GetSetDeckID(d);
                }

                b.Team2Crowns = b.Opponent[0].Crowns;

                b.Team2KingTowerHp = b.Opponent[0].KingTowerHitPoints;
                b.Team2PrincessTowerHpA = b.Opponent[0].PrincessTowerA;
                b.Team2PrincessTowerHpB = b.Opponent[0].PrincessTowerB;


                b.GameModeId = b.GameMode.Id;

                context.Battles.Add(b);
                linesSaved++;
                //var team = b.Team;
                //var opponent = b.Opponent;

                //clashDB.GetSetTeamId(team);
                //clashDB.GetSetTeamId(opponent);
            });
            Console.WriteLine();

            return linesSaved;
        }


        public async Task<Player> FillPlayerDBData(Player player)
        {
            DateTime now = DateTime.Now;

            player.TeamId = GetSetTeamId(player);

            Deck d = new Deck(player.CurrentDeck);
            player.CurrentDeckId = GetSetDeckID(d);
            player.UpdateTime = now;
            return player;
        }



       // any unique team of players, a, b, a+b... is saved and given a team Id by the database
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
            var teams = context.Team.Where(t => t.TwoVTwo == twoVtwo);
            Team myTeam = new Team();
            //if it's
            if (!twoVtwo)
            {
                myTeam = teams.Where(t => t.Tag == teamMembers[0].Tag || t.Tag2 == teamMembers[0].Tag).FirstOrDefault(); 
                if(myTeam != null) { teamId = myTeam.TeamId; }
            }
            else
            {
                myTeam = teams.Where(t => ((t.Tag == teamMembers[0].Tag && t.Tag2 == teamMembers[1].Tag) || (t.Tag == teamMembers[1].Tag && t.Tag2 == teamMembers[0].Tag))).FirstOrDefault();


                if (myTeam != null) 
                { teamId = myTeam.TeamId;
                if(myTeam.Tag == "#9V88U2CG2"|| myTeam.Tag == "#29PGJURQL")
                        {
                        Console.WriteLine();
                    }
                }
            }

            if (teamId == -1)
            { Console.WriteLine(); }

            if (teamId == -1)
            {
                myTeam = new Team();
                myTeam.Tag = teamMembers[0].Tag;
                myTeam.Name = teamMembers[0].Name;
                myTeam.TeamName = teamMembers[0].Name;

                if (twoVtwo)
                {
                    myTeam.TwoVTwo = true;
                    myTeam.Tag2 = teamMembers[1].Tag;
                    myTeam.Name2 = teamMembers[1].Name;
                    myTeam.TeamName += " " + teamMembers[1].Name;
                }

                //sets return Id to the newly set Id within the Team model
                if (myTeam == null)
                {
                    teamId = 1;
                }
                else { teamId = context.Team.Count() + 1; }



                myTeam.TeamId = teamId;
                context.Team.Add(myTeam);
                context.SaveChanges();
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

