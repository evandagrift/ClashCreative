using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClashCreative.Models
{
    public class ClansModel
    {
        public ClansModel(ClashContext c)
        {
            PopulateClans(c);
        }
        public Clan SearchedClan { get; set; }
        public List<Clan> Clans { get; set; }



        public string GetHrefID(string s)
        {
            string returnString = "T" + s.Substring(1, s.Length - 1);
            return returnString;
        }
        public string GetSearchHrefId(string s)
        {
            string returnString = "S" + s.Substring(1, s.Length - 1);
            return returnString;
        }
        public string SetHrefId(string s)
        {
            string returnString = "#T" + s.Substring(1, s.Length - 1);
            return returnString;
        }
        public string SetSearchHrefId(string s)
        {
            string returnString = "#S" + s.Substring(1, s.Length - 1);
            return returnString;
        }

        public void PopulateClans(ClashContext context)
        {
            //if the db is empty it does nothing
            //I intend on populating the production DB Manually so nothing gets out of hand
            if (context.Clans.Count() > 0)
            {
                //Returns all clans in DB ordered by Clan Tag
                var clans = context.Clans.OrderByDescending(c => c.ClanScore).ToList();

                //cycles through all clans in the DB ordered by Clan Tag
                clans.ForEach((c =>
                {
                    //if there's nothing in the List it immediately adds a value
                    if (Clans == null) {
                        Clans = new List<Clan>();
                        Clans.Add(c);
                    }

                    //the recieved list is ordered by tag, it will only enter the first entry per tag by clan
                    if (Clans[Clans.Count() - 1].Tag != c.Tag) { Clans.Add(c); }

                }));
            }
        }
    }
}
