using GameEngine.Classes;
using DataLayer.Interfaces;
using DataLayer.DTO;
using DataLayer;

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
        public Hand<CardRecord> GetHand(int index) => poker.GetHand(index);
        public Deck<CardRecord> GetDeck() => poker.GetDeck;
        public int WinningHand() => poker.Winner().First().ID;
    }
}
