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
        private TRContext context;

        public BattlesRepo(TRContext c)
        {
            context = c;
        }
        public void AddBattle(Battle Battle)
        {
            context.Add(Battle);
        }

        public void DeleteBattle(int BattleID)
        {
            Battle BattleToDelete = GetBattleByID(BattleID);
            context.Battles.Remove(BattleToDelete);
        }

        public List<Battle> GetAllBattles()
        {
            return context.Battles.ToList();
        }

        public Battle GetBattleByID(int BattleID)
        {
            return context.Battles.Find(BattleID);
        }
    }
}
