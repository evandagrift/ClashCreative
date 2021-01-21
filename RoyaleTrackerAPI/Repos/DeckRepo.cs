using RoyaleTrackerAPI.Models;
using RoyaleTrackerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyaleTrackerAPI.Repos
{
    public class DeckRepo : IDeckRepo
    {
        private TRContext context;
        public DeckRepo(TRContext c)
        {
            context = c;
        }

        public void AddDeck(Deck deck)
        {
            context.Add(deck);
        }

        public void DeleteDeck(int deckID)
        {
            Deck deckToRemove = GetDeckById(deckID);
            context.Decks.Remove(deckToRemove);
        }

        public List<Deck> GetAllDecks()
        {
            return context.Decks.ToList();
        }

        public Deck GetDeckById(int deckID)
        {
            return context.Decks.Find(deckID);
        }
    }
}
