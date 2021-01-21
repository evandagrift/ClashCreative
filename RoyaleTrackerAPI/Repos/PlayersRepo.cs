using RoyaleTrackerAPI.Models;
using RoyaleTrackerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyaleTrackerAPI.Repos
{
    public class PlayersRepo : IPlayersRepo
    {
        private TRContext context;
        public PlayersRepo(TRContext c)
        {
            context = c;
        }

        public void AddPlayer(Player player)
        {
            context.Players.Add(player);
        }

        public void DeletePlayer(string playerTag)
        {
            Player playerToDelete = GetPlayerByTag(playerTag);
            context.Players.Remove(playerToDelete);
        }

        public List<Player> GetAllPlayers()
        {
            return context.Players.ToList();
        }

        public Player GetPlayerByTag(string playerTag)
        {
            return context.Players.Find(playerTag);
        }
    }
}
