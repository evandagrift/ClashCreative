using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClashCreative.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public string Tag { get; set; }
        public string Name { get; set; }

        public string ClanTag { get; set; }

        [NotMapped]
        public Clan Clan { get; set; }

        public string Role { get; set; }
        public string LastSeen { get; set; }

        //change this to Current Deck Id
        public int CurrentDeckId { get ; set; }

        [NotMapped]
        public List<Card> CurrentDeck { get; set; }

        public int ExpLevel { get; set; }
        public int Trophies { get; set; }
        public int BestTrophies { get; set; }

        public int Wins { get; set; }
        public int Losses { get; set; }

        public int Donations { get; set; }
        public int DonationsReceived { get; set; }
        public int TotalDonations { get; set; }
        public int ClanCardsCollected { get; set; }
        public DateTime UpdateTime { get; set; }

    }
}
