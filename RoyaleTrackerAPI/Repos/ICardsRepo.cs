using RoyaleTrackerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyaleTrackerAPI.Repos
{
    interface ICardsRepo
    {
        void AddCard(Card card);
        List<Card> GetAllCards();
        Card GetCardByID(int cardID);
        void DeleteCard(int cardID);
    }
}
