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
        public int Team1Id { get; set; }
        public int Team2Id { get; set; }
        public string Type { get; set; }
        public string BattleTime { get; set; }
        public string DeckSelection { get; set; }
        public bool IsLadderTournament { get; set; }
        public int GameModeId { get; set; }
        public GameMode GameMode { get; set; }


        [NotMapped]
        public List<TeamMember> Team { get; set; }

        [NotMapped]
        public List<TeamMember> Opponent { get; set; }


    }
}
