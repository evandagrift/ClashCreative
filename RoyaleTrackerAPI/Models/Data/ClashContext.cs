using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoyaleTrackerClasses;

namespace RoyaleTrackerAPI
{
    public partial class ClashContext : DbContext
    {
        public ClashContext() { }
        public  ClashContext(DbContextOptions<ClashContext> options) : base(options) { }

        public DbSet<Player> Players { get; set; }
        public DbSet<Clan> Clans { get; set; }
        public DbSet<Deck> Decks { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
