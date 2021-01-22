using RoyaleTrackerAPI.Models;
using RoyaleTrackerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyaleTrackerAPI.Repos
{
    public class ClansRepo : IClansRepo
    {
        private TRContext context;

        public ClansRepo(TRContext c)
        {
            context = c;
        }
        public void AddClan(Clan clan)
        {
            context.Clans.Add(clan);
        }

        public void DeleteClan(string clanTag)
        {
            Clan clanToDelete = context.Clans.Find(clanTag);
            context.Clans.Remove(clanToDelete);
        }

        public List<Clan> GetAllClans()
        {
            return context.Clans.ToList();
        }

        public Clan GetClanByTag(string clanTag)
        {
            return context.Clans.Find(clanTag);
        }
    }
}
