using RoyaleTrackerAPI.Models;
using RoyaleTrackerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyaleTrackerAPI.Repos
{
    public class DecksRepo : IDecksRepo
    {
        //DB Access
        private TRContext context;

        //constructor assigning argumented context
        public DecksRepo(TRContext c) { context = c; }

        //adds given deck to context
        public void AddDeck(Deck deck) { context.Add(deck); }

        //deletes deck with given ID
        public void DeleteDeck(int deckID)
        {
            //fetches deck with given ID
            Deck deckToRemove = GetDeckByID(deckID);
            
            //if a valid deck is fetched from DB it removes it from context
            if(deckToRemove != null)
                context.Decks.Remove(deckToRemove);
        }

        //returns all decks in the DB
        public List<Deck> GetAllDecks() { return context.Decks.ToList(); }

        //returns Deck with given ID from DB
        public Deck GetDeckByID(int deckID) { return context.Decks.Find(deckID); }

        //updates deck at given ID with argumented Deck Fields
        public void UpdateDeck(Deck deck)
        {
            //fetches deck at given DeckID
            Deck deckToUpdate = GetDeckByID(deck.DeckId);

            //if a valid deck is fetched it updates all of the fields of that deck
            if(deckToUpdate!=null)
            {
                deckToUpdate.Card1Id = deck.Card1Id;
                deckToUpdate.Card2Id = deck.Card2Id;
                deckToUpdate.Card3Id = deck.Card3Id;
                deckToUpdate.Card4Id = deck.Card4Id;
                deckToUpdate.Card5Id = deck.Card5Id;
                deckToUpdate.Card6Id = deck.Card6Id;
                deckToUpdate.Card7Id = deck.Card7Id;
                deckToUpdate.Card8Id = deck.Card8Id;
            }
        }
    }
}
