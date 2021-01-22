using RoyaleTrackerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyaleTrackerAPI.Repos
{
    interface IBattlesRepo
    {
        void AddBattle(Battle Battle);
        List<Battle> GetAllBattles();
        Battle GetBattleByID(int BattleID);
        void DeleteBattle(int BattleID);
    }
}
