using RoyaleTrackerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyaleTrackerAPI.Repos
{
    interface IDecksRepo
    {
        Deck GetDeckByID(int deckID);
        List<Deck> GetAllDecks();
        void AddDeck(Deck deck);
        void DeleteDeck(int deckID);
        void UpdateDeck(Deck deck);
    }
}
