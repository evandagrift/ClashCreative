using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RoyaleTrackerAPI;
using RoyaleTrackerAPI.Repos;
using RoyaleTrackerClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestFixture]
    class UsersTests
    {
        ClashContext fakeContext;
        UserRepo repo;
        List<User> users;
        public UsersTests()
        {

            //creates sudo options for the fake context
            var options = new DbContextOptionsBuilder<ClashContext>()
                .UseInMemoryDatabase(databaseName: "ClashAPI")
                .Options;

            fakeContext = new ClashContext(options);
            //seed data for fake DB
            fakeContext.Users.Add(new User() { Username = "admin1", Password = "password1", Role = "Admin", Token = Guid.NewGuid().ToString() });
            fakeContext.Users.Add(new User() { Username = "user2", Password = "password2", Role = "User" });
            fakeContext.Users.Add(new User() { Username = "user1", Password = "password1", Role = "User" });
            fakeContext.SaveChanges();
            //fake context for testing

            //init the repo for testing`
            repo = new UserRepo(fakeContext);

        }

        [SetUp]
        public void Setup()
        {

        }

        //read all
        [Test]
        public void GetAllTest()
        {
            users = repo.GetAllUsers();
            Assert.IsTrue(users.Count > 0);
        }

        //Read By Id
        [Test]
        public void GetByIDTest()
        {
            User user = repo.GetUserByUsername("user2");
            Assert.AreEqual(user.Password, "password2");
        }

        [Test]
        public void AddUserTest()
        {
            User user = new User() { Username = "new1", Password = "new1", Role = "User" };
            repo.AddUser(user);
            fakeContext.SaveChanges();
            User fetchedUser = repo.GetUserByUsername("new1");
            Assert.NotNull(fetchedUser);

            User nonexistentUser = repo.GetUserByUsername("new172");
            Assert.IsNull(nonexistentUser);
        }

        [Test]
        public void DeleteUserTest()
        {
            repo.DeleteUser("user1");
            fakeContext.SaveChanges();
            Assert.IsNull(repo.GetUserByUsername("user1"));
        }

        [Test]
        public void GetUncreatedToken()
        {
            string token = repo.GetUserToken("user2");
            Assert.IsNotNull(token);
        }

        [Test]
        public void GetExistingToken()
        {
            string token = repo.GetUserToken("admin1");
            Assert.IsNotNull(token);
        }
    }
}