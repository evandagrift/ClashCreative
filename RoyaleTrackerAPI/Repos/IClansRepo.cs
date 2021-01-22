using RoyaleTrackerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyaleTrackerAPI.Repos
{
    interface IClansRepo
    {
        Clan GetClanByTag(string clanTag);
        List<Clan> GetAllClans();
        void AddClan(Clan clan);
        void DeleteClan(string clanTag);
    }
}
