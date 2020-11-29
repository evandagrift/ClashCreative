using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClashCreative.Models
{
    public class Battle
    {
        [Key]
        public int BattleId { get;set; }
        //public string 
        public string Type { get; set; }
        public string BattleTime { get; set; }
        public bool IsLadderTournament { get; set; }
        public int GameModeId { get; set; }
        public GameMode GameMode { get; set; }

        public string DeckSelection { get; set; }

        [NotMapped]
        public List<TeamMember> TeamMembers { get; set; }

        [NotMapped]
        public List<TeamMember> Opponent { get; set; }


    }
}
