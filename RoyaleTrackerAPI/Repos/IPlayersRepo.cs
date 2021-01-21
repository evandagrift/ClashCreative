using RoyaleTrackerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyaleTrackerAPI.Repos
{
    interface IPlayersRepo
    {
        void AddPlayer(Player player);
        List<Player> GetAllPlayers();
        Player GetPlayerByTag(string playerTag);
        void DeletePlayer(string playerTag);
    }
}
