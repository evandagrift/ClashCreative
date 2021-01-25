using RoyaleTrackerAPI.Models;
using RoyaleTrackerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyaleTrackerAPI.Repos
{
    public class BattlesRepo : IBattlesRepo
    {
        //DB Access
        private TRContext context;

        //constructor, connects Connect argumented context
        public BattlesRepo(TRContext c) { context = c; }

        //adds given battle to the context
        public void AddBattle(Battle battle) { context.Add(battle); }

        //deletes battle at given ID
        public void DeleteBattle(int battleID)
        {
            //fetches the battle at given ID
            Battle battleToDelete = GetBattleByID(battleID);

            //if a valid battle is fetched from the DB it is removed from the context
            if(battleToDelete != null)
                context.Battles.Remove(battleToDelete);
        }

        //returns a list of all battles from DB
        public List<Battle> GetAllBattles() { return context.Battles.ToList(); }

        //gets battle with given battleID
        public Battle GetBattleByID(int battleID) { return context.Battles.Find(battleID); }

        //updates battle at given ID
        public void UpdateBattle(Battle battle)
        {
            //fetches battle with given ID
            Battle battleToUpdate = GetBattleByID(battle.BattleId);

            //if a valid battle is fetched it updates it
            if (battleToUpdate != null)
            {
                battleToUpdate.BattleTime = battle.BattleTime;

                battleToUpdate.Team1Name = battle.Team1Name;
                battleToUpdate.Team1Id = battle.Team1Id;
                battleToUpdate.Team1Win = battle.Team1Win;
                battleToUpdate.Team1StartingTrophies = battle.Team1StartingTrophies;
                battleToUpdate.Team1TrophyChange = battle.Team1TrophyChange;
                battleToUpdate.Team1DeckAId = battle.Team1DeckAId;
                battleToUpdate.Team1DeckBId = battle.Team1DeckBId;
                battleToUpdate.Team1Crowns = battle.Team1Crowns;
                battleToUpdate.Team1KingTowerHp = battle.Team1KingTowerHp;
                battleToUpdate.Team1PrincessTowerHpA = battle.Team1PrincessTowerHpA;
                battleToUpdate.Team1PrincessTowerHpB = battle.Team1PrincessTowerHpB;

                battleToUpdate.Team2Name = battle.Team2Name;
                battleToUpdate.Team2Id = battle.Team2Id;
                battleToUpdate.Team2Win = battle.Team2Win;
                battleToUpdate.Team2StartingTrophies = battle.Team2StartingTrophies;
                battleToUpdate.Team2TrophyChange = battle.Team2TrophyChange;
                battleToUpdate.Team2DeckAId = battle.Team2DeckAId;
                battleToUpdate.Team2DeckBId = battle.Team2DeckBId;
                battleToUpdate.Team2Crowns = battle.Team2Crowns;
                battleToUpdate.Team2KingTowerHp = battle.Team2KingTowerHp;
                battleToUpdate.Team2PrincessTowerHpA = battle.Team2PrincessTowerHpA;
                battleToUpdate.Team2PrincessTowerHpB = battle.Team2PrincessTowerHpB;

                battleToUpdate.Type = battle.Type;
                battleToUpdate.DeckSelection = battle.DeckSelection;
                battleToUpdate.IsLadderTournament = battle.IsLadderTournament;
                battleToUpdate.GameModeId = battle.GameModeId;
            }
        }

    
    }
}
