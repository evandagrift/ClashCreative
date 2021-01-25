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
    class DecksTests
    {
        TRContext fakeContext;
        DecksRepo repo;
        public DecksTests()
        {
            //creates sudo options for the fake context
            var options = new DbContextOptionsBuilder<TRContext>()
                .UseInMemoryDatabase(databaseName: "ClashAPI")
                .Options;
            fakeContext = new TRContext(options);

            fakeContext.Decks.Add(new Deck() { DeckId = 001, Card1Id = 001 });

            fakeContext.Decks.Add(new Deck() { DeckId = 002, Card1Id = 002 });

            fakeContext.Decks.Add(new Deck() { DeckId = 003, Card1Id = 003 });
            fakeContext.SaveChanges();
            repo = new DecksRepo(fakeContext);
        }

        [Test]
        public void GetDeckTest()
        {
            Deck deck = repo.GetDeckByID(001);
            Assert.AreEqual(deck.Card1Id, 001);
        }

        [Test]
        public void GetAllDecksTest()
        {
            List<Deck> decks = repo.GetAllDecks();
            Assert.IsNotNull(decks);
        }

        [Test]
        public void AddDeckTest()
        {
            Deck deck = new Deck() { DeckId = 004, Card1Id = 004 };
            repo.AddDeck(deck);
            fakeContext.SaveChanges();

            Deck returnedDeck = repo.GetDeckByID(004);
            Assert.AreEqual(returnedDeck.Card1Id, 004);
        }
        [Test]
        public void DeleteDeckTest()
        {
            repo.DeleteDeck(002);
            fakeContext.SaveChanges();

            Deck deletedDeck = repo.GetDeckByID(002);
            Assert.IsNull(deletedDeck);
        }

        [Test]
        public void UpdateDeckTest()
        {
            Deck deck = repo.GetDeckByID(003);
            deck.Card2Id = 003;

            fakeContext.SaveChanges();

            Deck updatedDeck = repo.GetDeckByID(003);
        }


    }
}
