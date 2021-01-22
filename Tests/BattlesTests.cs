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
    class BattlesTests
    {
        TRContext fakeContext;
        BattlesRepo repo;

        public BattlesTests()
        {
            //creates sudo options for the fake context
            var options = new DbContextOptionsBuilder<TRContext>()
                .UseInMemoryDatabase(databaseName: "ClashAPI")
                .Options;
            fakeContext = new TRContext(options);

            fakeContext.Battles.Add(new Battle() { BattleId = 001, Team1Win = true });
            fakeContext.Battles.Add(new Battle() { BattleId = 002, Team1Win = false });
            fakeContext.Battles.Add(new Battle() { BattleId = 003, Team1Win = true });
            fakeContext.Battles.Add(new Battle() { BattleId = 004, Team1Win = false });
            fakeContext.SaveChanges();

            repo = new BattlesRepo(fakeContext);
        }

        [Test]
        public void GetAllTest()
        {
            List<Battle> battles = repo.GetAllBattles();
            Assert.IsNotNull(battles);
        }

        [Test]
        public void GetBattleTest()
        {
            Battle battle = repo.GetBattleByID(001);
            Assert.IsTrue(battle.Team1Win == true);
        }

        [Test]
        public void AddBattleTest()
        {
            Battle battle = new Battle() { BattleId = 005, Team1Win = false };
            repo.AddBattle(battle);
            fakeContext.SaveChanges();

            Assert.AreEqual(repo.GetBattleByID(005).Team1Win, false);
        }

        [Test]
        public void DeleteBattleTest()
        {
            Assert.IsNotNull(repo.GetBattleByID(002));

            repo.DeleteBattle(002);
            fakeContext.SaveChanges();

            Assert.IsNull(repo.GetBattleByID(002));
        }

        [Test]
        public void UpdateBattleTest()
        {
            Battle Battle = repo.GetBattleByID(003);
            Battle.Team2Win = true;
            fakeContext.SaveChanges();

            Assert.AreEqual(true, repo.GetBattleByID(003).Team2Win);
        }
    }
}
