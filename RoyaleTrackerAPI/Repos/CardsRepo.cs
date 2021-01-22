using RoyaleTrackerAPI.Models;
using RoyaleTrackerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyaleTrackerAPI.Repos
{
    public class CardsRepo : ICardsRepo
    {
        private TRContext context;

        public CardsRepo(TRContext c)
        {
            context = c;
        }
        public void AddCard(Card card)
        {
            context.Add(card);
        }

        public void DeleteCard(int cardID)
        {
            Card cardToDelete = GetCardByID(cardID);
            context.Cards.Remove(cardToDelete);
        }

        public List<Card> GetAllCards()
        {
            return context.Cards.ToList();
        }

        public Card GetCardByID(int cardID)
        {
            return context.Cards.Find(cardID);
        }
    }
}
