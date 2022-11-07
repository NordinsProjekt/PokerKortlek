using GameEngine.Classes;

namespace Test_PokerSim2022
{
    [TestClass]
    [TestCategory("Game Tests")]
    public class Test_GameMechanics
    {
        private Deck<CardRecord> GetTestDeck()
        {
            Communicate com = new Communicate();
            List<CardRecord> cards = new List<CardRecord>();
            foreach (var item in com.GetCardList())
                cards.Add(new CardRecord(item.Id, item.Color, item.Number));
            Deck<CardRecord> deck = new Deck<CardRecord>(cards);
            return deck;
        }
        private GamePoker GetTestGame(int numOfPlayers)
        {
            var d = GetTestDeck();
            d.Shuffle();
            var deck = d.ToList();
            GamePoker game = new GamePoker(deck, numOfPlayers);
            return game;
        }

        [TestMethod]
        public void GenerateNewGame_With4Players_CheckIfAllHandsHave5Cards()
        {
            var gm = GetTestGame(4);
            var hand1 = gm.GetHand(0);
            var hand2 = gm.GetHand(1);
            var hand3 = gm.GetHand(2);
            var hand4 = gm.GetHand(3);

            Assert.IsTrue(hand1.Count == 5);
            Assert.IsTrue(hand2.Count == 5);
            Assert.IsTrue(hand3.Count == 5);
            Assert.IsTrue(hand4.Count == 5);
        }

        [TestMethod]
        [ExpectedException(typeof(GameException))]
        public void GenerateNewGame_With5Players_ShouldThrowException()
        {
            var gm = GetTestGame(5);
        }

        [TestMethod]
        public void GenerateNewGame_With4Players_ShowGameState()
        {
            var gm = GetTestGame(4);
            Console.WriteLine(gm.ToString());
        }

        [TestMethod]
        public void GenerateNewGame_MakeAllPlayersThrowCards_AndDrawNewOnes_ShowGameState()
        {
            var gm = GetTestGame(4);
            var deck = gm.GetDeck;
            var hand1 = gm.GetHand(0);
            var hand2 = gm.GetHand(1);
            var hand3 = gm.GetHand(2);
            var hand4 = gm.GetHand(3);

            hand1.Throw(4);
            hand1.Throw(1);

            hand2.Throw(2);
            hand2.Throw(2);

            hand3.Throw(1);
            hand3.Throw(1);

            hand4.Throw(0);
            hand4.Throw(0);
            hand4.Throw(0);

            hand1.Draw(deck.Draw());
            hand1.Draw(deck.Draw());

            hand2.Draw(deck.Draw());
            hand2.Draw(deck.Draw());

            hand3.Draw(deck.Draw());
            hand3.Draw(deck.Draw());

            hand4.Draw(deck.Draw());
            hand4.Draw(deck.Draw());
            hand4.Draw(deck.Draw());
            Console.WriteLine(gm.ToString());
        }
        [TestMethod]
        public void GenerateNewGame_PrintWinningHandsList()
        {
            var gm = GetTestGame(4);
            var winnerList = gm.Winner();

            foreach (var item in winnerList)
            {
                Console.WriteLine(item.ToString());
            }
        }
        [TestMethod]
        public void GenerateTestGame_PrintIt_UseNewToInitGame_PrintAgain()
        {
            var gm = GetTestGame(4);
            Console.WriteLine(gm);
            gm.RestartGame();
            Console.WriteLine(gm);
        }
        [TestMethod]
        public void MakeAGameWithCustomCardList_ShouldHave2HandsWithFlush()
        {
            List<CardRecord> list = new List<CardRecord>()
            {
                new CardRecord(1,"Hearts",2),
                new CardRecord(2,"Hearts",3),
                new CardRecord(3,"Hearts",4),
                new CardRecord(4,"Hearts",5),
                new CardRecord(5,"Hearts",6),
                new CardRecord(6,"Hearts",7),
                new CardRecord(7,"Hearts",8),
                new CardRecord(8,"Hearts",9),
                new CardRecord(9,"Hearts",10),
                new CardRecord(10,"Hearts",11)
            };

            GamePoker gm = new GamePoker(list, 2);
            var deck = gm.GetDeck;
            Console.WriteLine(gm);
        }
    }
}
