using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClashCreative.Models
{
    public class HomePageModel
    {
        //objects to fill and pass as Model to next view
        public Player Player { get; set; }
        public Clan Clan { get; set; }

      

        ////empty string when not active, "active" when active
        ////these strings are appended to the html class so when not active it is empty string, if selected it == active
        //public string PlayerActive { get; set; }
        //public string ClanActive { get; set; }

        ////these will be called from onlclick
        //public string SetPlayerActive() { PlayerActive = "active"; ClanActive = ""; return ""; }

        //public string SetClanActive() { PlayerActive = ""; ClanActive = "active"; return ""; }

    }
}
