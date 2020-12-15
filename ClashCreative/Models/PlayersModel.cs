using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClashCreative.Models
{
    public class PlayersModel
    {
        public PlayersModel(ClashContext c)
        {
            PopulatePlayers(c);
        }
        public Player SearchedPlayer { get; set; }
        public List<Player> Players { get; set; }



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

        public void PopulatePlayers(ClashContext context)
        {
            //if the db is empty it does nothing
            //I intend on populating the production DB Manually so nothing gets out of hand
            if (context.Players.Count() > 0)
            {
                //Returns all clans in DB ordered by Clan Tag
                var players = context.Players.OrderByDescending(p => p.UpdateTime).ToList();
                //cycles through all clans in the DB ordered by Clan Tag
                players.ForEach((p =>
                {
                    //if there's nothing in the List it immediately adds a value
                    if (Players == null)
                    {
                        Players = new List<Player>();
                        Players.Add(p);
                    }

                    //the recieved list is ordered by tag, it will only enter the first entry per tag by clan
                    if (Players[Players.Count() - 1].Tag != p.Tag) { Players.Add(p); }

                }));
            }
        }
    }
}
