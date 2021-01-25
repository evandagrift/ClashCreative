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
        //DB Access
        private TRContext context;

        //assigns argumented context
        public ClansRepo(TRContext c) { context = c; }

        //adds given clan to context
        public void AddClan(Clan clan) { context.Clans.Add(clan); }
        
        //Deletes clan with given clanTag
        public void DeleteClan(string clanTag)
        {
            //fetches clan with given clan tag
            Clan clanToDelete = context.Clans.Find(clanTag);

            //if a valid clan is fetched from the database it removes it from the context
            if(clanToDelete != null)
                context.Clans.Remove(clanToDelete);
        }

        //Returns a List of all Clans in DB
        public List<Clan> GetAllClans() { return context.Clans.ToList(); }

        //gets clan with given clanTag
        public Clan GetClanByTag(string clanTag) { return context.Clans.Find(clanTag); }

        //updates clan at given clantag
        public void UpdateClan(Clan clan)
        {
            //fetches clan at given Tag
            Clan clanToUpdate = GetClanByTag(clan.Tag);

            //if a valid clan is fetched it updates the fields to those of the argumented clan
            if(clanToUpdate != null)
            {
                clanToUpdate.Name = clan.Name;
                clanToUpdate.Type = clan.Type;
                clanToUpdate.Description = clan.Description;
                clanToUpdate.BadgeId = clan.BadgeId;
                clanToUpdate.LocationCode = clan.LocationCode;
                clanToUpdate.RequiredTrophies = clan.RequiredTrophies;
                clanToUpdate.DonationsPerWeek = clan.DonationsPerWeek;
                clanToUpdate.ClanChestStatus = clan.ClanChestStatus;
                clanToUpdate.ClanChestLevel = clan.ClanChestLevel;
                clanToUpdate.ClanScore = clan.ClanScore;
                clanToUpdate.ClanWarTrophies = clan.ClanWarTrophies;
                clanToUpdate.Members = clan.Members;
                clanToUpdate.UpdateTime = clan.UpdateTime;
            }
        }
    }
}
