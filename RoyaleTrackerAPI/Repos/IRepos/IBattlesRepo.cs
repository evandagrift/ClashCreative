using RoyaleTrackerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyaleTrackerAPI.Repos
{
    interface IBattlesRepo
    {
        void AddBattle(Battle battle);
        List<Battle> GetAllBattles();
        Battle GetBattleByID(int battleID);
        void DeleteBattle(int battleID);
        void UpdateBattle(Battle battle);
    }
}
