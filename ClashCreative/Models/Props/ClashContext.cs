using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClashCreative.Models
{
    public class ClashContext : DbContext
    {
        public  ClashContext(DbContextOptions<ClashContext> options) : base(options) { }

        public DbSet<Player> Players { get; set; }
        public DbSet<Clan> Clans { get; set; }
        public DbSet<Deck> Decks { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<Team> Team { get; set; }


    }
}
