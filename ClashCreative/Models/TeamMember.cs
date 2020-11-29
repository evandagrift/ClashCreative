using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClashCreative.Models
{
    public class TeamMember
    {
        public int TeamMemberId { get; set; }
        public string Tag { get; set; }
        public string Name { get; set; }
        public int StartingTrophies { get; set; }
        public int Crowns { get; set; }
        public int KingTowerHitPoints { get; set; }
        public int PrincessTowerA { get { return !(PrincessTowersHitPoints == null) ? PrincessTowersHitPoints[0] : 0; } set { } }
        public int PrincessTowerB { get
            {
                if (PrincessTowersHitPoints != null && PrincessTowersHitPoints.Count > 1) { return PrincessTowersHitPoints[1]; }
                else return 0;

            } set { }
        }
        [NotMapped]
        public List<int> PrincessTowersHitPoints { get; set; }


        [NotMapped]
        public Clan Clan { get; set; }


        [NotMapped]
        public Deck Cards { get; set; }
    }
}
