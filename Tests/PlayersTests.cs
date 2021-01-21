using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RoyaleTrackerAPI.Models;
using RoyaleTrackerAPI.Repos;
using RoyaleTrackerClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestFixture]
    class PlayersTests
    {
        TRContext fakeContext;
        PlayersRepo repo;

        public PlayersTests()
        {
            //creates sudo options for the fake context
            var options = new DbContextOptionsBuilder<TRContext>()
                .UseInMemoryDatabase(databaseName: "ClashAPI")
                .Options;
            fakeContext = new TRContext(options);

            fakeContext.Players.Add(new Player() { Tag = "tag1", Name = "name1" });
            fakeContext.Players.Add(new Player() { Tag = "tag2", Name = "name2" });
            fakeContext.Players.Add(new Player() { Tag = "tag3", Name = "name3" });


            fakeContext.SaveChanges();

            repo = new PlayersRepo(fakeContext);

        }

        [Test]
        public void GetPlayerTest()
        {
            Player player = repo.GetPlayerByTag("tag1");
            Assert.AreEqual(player.Name, "name1");
        }
        
        [Test]
        public void GetAllPlayersTest()
        {
            List<Player> players = repo.GetAllPlayers();
            Assert.IsNotNull(players);
        }

        [Test]
        public void AddPlayerTest()
        {
            Player player = new Player() { Tag = "tag4", Name = "name4" };
            repo.AddPlayer(player);
            fakeContext.SaveChanges();
            Assert.IsNotNull(repo.GetPlayerByTag("tag4"));
        }

        [Test]
        public void DeletePlayerTest()
        {
            repo.DeletePlayer("tag2");
            fakeContext.SaveChanges();

        }
        [Test]
        public void UpdatePlayerTest()
        {
            Player player = repo.GetPlayerByTag("tag3");
            player.Name = "UPDATED";

            string name = repo.GetPlayerByTag("tag3").Name;

            Assert.AreEqual(name, "UPDATED");

        }

    }
}
