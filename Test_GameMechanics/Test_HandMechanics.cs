namespace Test_PokerSim2022
{
    [TestClass]
    [TestCategory("Hand Tests")]
    public class Test_HandMechanics
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

        private Hand<CardRecord> GetTestHand(List<CardRecord> cardList)
        {
            Hand<CardRecord> player1 = new Hand<CardRecord>(cardList,1);
            return player1;
        }

        [TestMethod]
        public void StartHandWith5Cards_CheckIfHandCountIs5()
        {
            var deck = GetTestDeck();
            deck.Shuffle();
            List<CardRecord> cardList = new List<CardRecord>();
            for (int i = 0; i < 5; i++)
                cardList.Add(deck.Draw());
            var player1 = GetTestHand(cardList);
            Assert.IsTrue(player1.Count == 5);
        }

        [TestMethod]
        public void StartHandWith5Cards_Remove1Card_CountShouldBe4()
        {
            var deck = GetTestDeck();
            deck.Shuffle();

            List<CardRecord> cardList = new List<CardRecord>();
            for (int i = 0; i < 5; i++)
                cardList.Add(deck.Draw());
            var player1 = GetTestHand(cardList);

            player1.Throw(0);

            Assert.IsTrue(player1.Count == 4);
        }

        [TestMethod]
        public void StartHandWith5Cards_Throw1Add1()
        {
            var deck = GetTestDeck();
            deck.Shuffle();

            List<CardRecord> cardList = new List<CardRecord>();
            for (int i = 0; i < 5; i++)
                cardList.Add(deck.Draw());
            var player1 = GetTestHand(cardList);

            player1.Throw(0);
            player1.Draw(deck.Draw());
            Assert.IsTrue(player1.Count == 5);
        }

        [TestMethod]
        [ExpectedException(typeof(HandException))]
        public void StartHandWith5Cards_TryToRemoveIndex5_ShouldThrowException()
        {
            var deck = GetTestDeck();
            deck.Shuffle();
            var arr = deck.ToArray();
            List<CardRecord> cardList = new List<CardRecord>();
            for (int i = 0; i < 5; i++)
                cardList.Add(arr[i]);
            var player1 = GetTestHand(cardList);
            player1.Throw(5);
        }

        [TestMethod]
        [ExpectedException(typeof(HandException))]
        public void StartHandWith5Cards_TryToRemove6Cards_ShouldThrowException()
        {
            var deck = GetTestDeck();
            deck.Shuffle();
            var arr = deck.ToArray();
            List<CardRecord> cardList = new List<CardRecord>();
            for (int i = 0; i < 5; i++)
                cardList.Add(arr[i]);
            var player1 = GetTestHand(cardList);
            for (int i = 0; i < 7; i++)
                player1.Throw(0);
        }

        [TestMethod]
        public void StartHandWith5Cards_TryToRemove5Cards_CountShouldBe0()
        {
            var deck = GetTestDeck();
            deck.Shuffle();
            var arr = deck.ToArray();
            List<CardRecord> cardList = new List<CardRecord>();
            for (int i = 0; i < 5; i++)
                cardList.Add(arr[i]);
            var player1 = GetTestHand(cardList);
            for (int i = 0; i < 5; i++)
                player1.Throw(0);
            Assert.IsTrue(player1.Count == 0);
        }

        [TestMethod]
        public void StartHandWith5Cards_PrintHandArray()
        {
            var deck = GetTestDeck();
            deck.Shuffle();
            List<CardRecord> cardList = new List<CardRecord>();
            for (int i = 0; i < 5; i++)
                cardList.Add(deck.Draw());
            var player1 = GetTestHand(cardList);
            foreach (var item in player1.ToList())
                Console.WriteLine(item);
        }

        [TestMethod]
        public void StartHandWith5Cards_PrintHandToList_ThrowOne_PrintHandToList()
        {
            var deck = GetTestDeck();
            deck.Shuffle();
            List<CardRecord> cardList = new List<CardRecord>();
            for (int i = 0; i < 5; i++)
                cardList.Add(deck.Draw());
            var player1 = GetTestHand(cardList);
            player1.ToList().ForEach(x => Console.WriteLine(x.ToString()));
            player1.Throw(0);
            Console.WriteLine("\nDropped index 0\n");
            player1.ToList().ForEach(x => Console.WriteLine(x.ToString()));
        }
    }
}
