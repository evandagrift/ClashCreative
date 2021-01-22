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
    class ClansTests
    {
        TRContext fakeContext;
        ClansRepo repo;
        public ClansTests()
        {

            //creates sudo options for the fake context
            var options = new DbContextOptionsBuilder<TRContext>()
                .UseInMemoryDatabase(databaseName: "ClashAPI")
                .Options;
            fakeContext = new TRContext(options);

            fakeContext.Clans.Add(new Clan() { Tag = "AAA", Name = "test1" });
            fakeContext.Clans.Add(new Clan() { Tag = "BBB", Name = "test2" });
            fakeContext.Clans.Add(new Clan() { Tag = "CCC", Name = "test3" });
            fakeContext.SaveChanges();

            repo = new ClansRepo(fakeContext);
        }

        [Test]
        public void GetClanTest()
        {
            Clan clan = repo.GetClanByTag("AAA");
            Assert.AreEqual("test1", clan.Name);
        }


        [Test]
        public void GetAllClansTest()
        {
            List<Clan> clans = repo.GetAllClans();
            Assert.IsNotNull(clans);
        }

        [Test]
        public void AddClanTest()
        {
            Clan clan = new Clan() { Tag = "DDD", Name = "test4" };
            repo.AddClan(clan);
            fakeContext.SaveChanges();

            Clan fetchedClan = repo.GetClanByTag("DDD");

            Assert.IsNotNull(fetchedClan);
        }

        [Test]
        public void DeleteUserTest()
        {
            Clan clanToDelete = repo.GetClanByTag("CCC");

            Assert.IsNotNull(clanToDelete);

            repo.DeleteClan(clanToDelete.Tag);
            fakeContext.SaveChanges();
            Clan deletedClan = repo.GetClanByTag("CCC");
            Assert.IsNull(deletedClan);
        }

        [Test]
        public void UpdateClanTest()
        {
            Clan clanToUpdate = repo.GetClanByTag("BBB");
            clanToUpdate.Name = "CHANGED";
            fakeContext.SaveChanges();

            Clan updatedClan = repo.GetClanByTag("BBB");

            Assert.AreEqual(updatedClan.Name, "CHANGED");
        }


    }
}
