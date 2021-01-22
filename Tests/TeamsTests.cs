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
    class TeamsTests
    {
        TRContext fakeContext;
        TeamsRepo repo;

        public TeamsTests()
        {
            //creates sudo options for the fake context
            var options = new DbContextOptionsBuilder<TRContext>()
                .UseInMemoryDatabase(databaseName: "ClashAPI")
                .Options;
            fakeContext = new TRContext(options);

            fakeContext.Teams.Add(new Team() { TeamId = 001, Name = "test1" });
            fakeContext.Teams.Add(new Team() { TeamId = 002, Name = "test2" });
            fakeContext.Teams.Add(new Team() { TeamId = 003, Name = "test3" });
            fakeContext.Teams.Add(new Team() { TeamId = 004, Name = "test4" });
            fakeContext.SaveChanges();

            repo = new TeamsRepo(fakeContext);
        }

        //test get all
        [Test]
        public void GetAllTest()
        {
            List<Team> Teams = repo.GetAllTeams();
            Assert.IsNotNull(Teams);
        }

        [Test]
        public void GetTeamTest()
        {
            Team Team = repo.GetTeamByID(001);
            Assert.IsTrue(Team.Name == "test1");
        }

        [Test]
        public void AddTeamTest()
        {
            Team Team = new Team() { TeamId = 005, Name = "test5" };
            repo.AddTeam(Team);
            fakeContext.SaveChanges();

            Assert.AreEqual(repo.GetTeamByID(005).Name, "test5");
        }

        [Test]
        public void DeleteTeamTest()
        {
            Assert.IsNotNull(repo.GetTeamByID(002));

            repo.DeleteTeam(002);
            fakeContext.SaveChanges();

            Assert.IsNull(repo.GetTeamByID(002));
        }

        [Test]
        public void UpdateTeamTest()
        {
            Team team = repo.GetTeamByID(003);
            team.Name = "UPDATED";
            fakeContext.SaveChanges();

            Assert.AreEqual("UPDATED", repo.GetTeamByID(003).Name);
        }




    }
}
