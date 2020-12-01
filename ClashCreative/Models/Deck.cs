using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClashCreative.Models
{
    public class Deck 
    {
        public Deck() { }
        public Deck(List<Card> c)
        {
            Card1Id = c[0].Id;
            Card2Id = c[1].Id;
            Card3Id = c[2].Id;
            Card4Id = c[3].Id;
            Card5Id = c[4].Id;
            Card6Id = c[5].Id;
            Card7Id = c[6].Id;
            Card8Id = c[7].Id;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DeckId { get; set;}
        public int Card1Id { get; set; } 
        public int Card2Id { get; set; }
        public int Card3Id { get; set; }
        public int Card4Id { get; set; }
        public int Card5Id { get; set; }
        public int Card6Id { get; set; }
        public int Card7Id { get; set; }
        public int Card8Id { get; set; }


        public void SortCards()
        {
            List<int> sortedList = new List<int>();

            sortedList.Add(Card1Id);
            sortedList.Add(Card2Id);
            sortedList.Add(Card3Id);
            sortedList.Add(Card4Id);
            sortedList.Add(Card5Id);
            sortedList.Add(Card6Id);
            sortedList.Add(Card7Id);
            sortedList.Add(Card8Id);

            sortedList.Sort();

            Card1Id = sortedList[0];
            Card2Id = sortedList[1];
            Card3Id = sortedList[2];
            Card4Id = sortedList[3];
            Card5Id = sortedList[4];
            Card6Id = sortedList[5];
            Card7Id = sortedList[6];
            Card8Id = sortedList[7];
        }
    }

}



//public List<int> GetAllCardId ()
//{
//    if (this.Count == 8)
//    {
//        List<int> returnList = new List<int>();
//        returnList.Add(this[0].Id);
//        returnList.Add(this[1].Id);
//        returnList.Add(this[2].Id);
//        returnList.Add(this[3].Id);
//        returnList.Add(this[4].Id);
//        returnList.Add(this[5].Id);
//        returnList.Add(this[6].Id);
//        returnList.Add(this[7].Id);
//        return returnList;
//    }
//    else { return null; }
//}