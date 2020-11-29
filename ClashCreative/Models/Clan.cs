using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClashCreative.Models
{
    public class Clan
    {
        [Key]
        public int Id { get;set; }
        public string Tag { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int RequiredTrophies { get; set; }
        public int DonationsPerWeek { get; set; }
        public int Members { get; set; }

        [NotMapped]
        public List<Player> MemberList { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
