using Database;
using DataLayer.DTO;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class PokerCards : ICardCommunicate
    {
        /// <summary>
        /// Gives a List of 52 standard pokercards
        /// </summary>
        /// <returns></returns>
        public List<CardRecord> GetCards()
        {
            List<CardRecord> cards = new List<CardRecord>();
            Communicate com = new Communicate();
            var cardList = com.GetCardList();
            foreach (var item in cardList)
                cards.Add(new CardRecord(item.Id, item.Color, item.Number));
            return cards;
        }
    }
}
