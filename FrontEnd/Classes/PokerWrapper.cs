using GameEngine.Classes;
using DataLayer.Interfaces;
using DataLayer.DTO;
using DataLayer;
using System.Linq.Expressions;

namespace FrontEnd.Classes
{
    public class PokerWrapper
    {
        private GamePoker poker;
        public List<string> playerList = new List<string>();
        public PokerWrapper()
        {
            ICardCommunicate ipc = new PokerCards();
            poker = new GamePoker(ipc.GetCards(), 2);
        }
        public bool Save(string name)
        {
            try
            {
                poker.Save(name);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public bool Update(string name)
        {
            try
            {
                poker.Update(name);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public Hand<CardRecord> GetHand(int index) => poker.GetHand(index);
        public Deck<CardRecord> GetDeck() => poker.GetDeck;
        public int WinningHand() => poker.Winner().First().ID;
    }
}
