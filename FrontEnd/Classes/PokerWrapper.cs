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
        public List<Player> playerList = new List<Player>();
        public string roomName = "";
        public PokerWrapper()
        {
            ICardCommunicate ipc = new PokerCards();
            poker = new GamePoker(ipc.GetCards(), 2);
        }
        public int JoinRoom(string room)
        {
            if (playerList.Count > 2) { return -1; }
            if (roomName == "")
            {
                roomName = room;
                playerList.Add(new Player() { Id = 0, Name = "Player 1", Waiting = true});
                return 0;
            }
            else
            {
                playerList.Add(new Player() { Id = 1, Name = "Player 2", Waiting = true });
                return 1;
            }       
        }
        public bool Save()
        {
            if (playerList.Count() == 2)
            {
                try
                {
                    poker.Save(roomName);
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
            return false;

            
        }
        public bool Update()
        {
            try
            {
                poker.Update(roomName);
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

        public void RestorePlayer(int id)
        {
            playerList[id].DrawnCards = false;
            playerList[id].Waiting = false;
        }

        public void ShuffleAndDeal()
        {
            poker.RestartGame();
        }

        public void LeaveGame(int id)
        {
            Player player = playerList.Where(x=>x.Id== id).FirstOrDefault();
            if (player != null)
            {
                playerList.Remove(player);
            }
            else
            {
                throw new Exception("Player doesnt exist");
            }
        }
    }
}
