using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClashCreative.Models
{
    public class Deck : List<Card>
    {
        public Deck()
        {
            this.Capacity = NotMappedAttribute;
        }

        public int DeckId { get; set; }

        //lol I couldn't figure out how to stop Capacity from showing up so I'm tryin this

        [NotMapped]
        public int Capacity { get; set; }

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

            sortedList.Add(this[0].Id);
            sortedList.Add(this[1].Id);
            sortedList.Add(this[2].Id);
            sortedList.Add(this[3].Id);
            sortedList.Add(this[4].Id);
            sortedList.Add(this[5].Id);
            sortedList.Add(this[6].Id);
            sortedList.Add(this[7].Id);

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