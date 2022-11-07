using Database;
using GameEngine.Classes;
using GameEngine.DTO;
using GameEngine.Exception;
namespace Test_PokerSim2022
{
    [TestClass]
    [TestCategory("Deck Tests")]
    public class Test_DeckMechanics
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
        [TestMethod]
        public void FillDeckWithCards_CheckIfCountIs52()
        {
            var deck = GetTestDeck();
            Assert.IsTrue(deck.Count == 52);
        }

        [TestMethod]
        public void FillDeckWithCards_CheckIfCountIs51()
        {
            var deck = GetTestDeck();
            Assert.IsFalse(deck.Count == 51);
        }

        [TestMethod]
        public void FillDeckWithCards_CheckIfCountIs53()
        {
            var deck = GetTestDeck();
            Assert.IsFalse(deck.Count == 53);
        }

        [TestMethod]
        public void FillDeckWithCards_Shuffle_CheckIfListIsNotEqualBefore()
        {
            var deck = GetTestDeck();
            var deck1 = deck.ToArray();
            deck.Shuffle();
            var deck2 = deck.ToArray();
            CollectionAssert.AreNotEqual(deck1, deck2);
        }

        [TestMethod]
        [ExpectedException(typeof(DeckException))]
        public void FillDeckWithCards_TryToGetIndex52_ShouldThrowException()
        {
            var deck = GetTestDeck();
            var item = deck[52];
        }

        [TestMethod]
        public void FillDeckWithCards_Remove2Cards_CheckCount_ShouldBe50()
        {
            var deck = GetTestDeck();
            deck.Burn(2);
            Assert.IsTrue(deck.Count == 50);
        }

        [TestMethod]
        public void FillDeckWithCards_Draw1Card__CardShouldBeTheSameAsIndex0()
        {
            var deck = GetTestDeck();
            var item = deck[0];
            var item2 = deck.Draw();
            Assert.AreEqual(item, item2);
            Console.WriteLine(item);
            Console.WriteLine(item2);
        }

        [TestMethod]
        public void FillDeckWithCards_Draw1Card_DeckShouldHaveCount51()
        {
            var deck = GetTestDeck();
            var item = deck.Draw();
            Assert.IsTrue(deck.Count == 51);
            Assert.IsFalse(item == deck.Draw());
        }

        [TestMethod]
        public void FillDeckWithCards_UseNewMethod_ShouldNotBeSameOrderAsStart()
        {
            var deck = GetTestDeck();
            var arr1 = deck.ToArray();
            deck.New();
            var arr2 = deck.ToArray();
            CollectionAssert.AreNotEqual(arr1, arr2);
        }

        [TestMethod]
        public void FillDeckWithCards_Draw1Card_BackupListShouldStillBeCount52()
        {
            var deck = GetTestDeck();
            deck.Draw();
            Assert.IsTrue(deck.GetCountSeedList() == 52);
        }

        [TestMethod]
        public void GetTestDeck_Burn10Cards_CheckCount()
        {
            var deck = GetTestDeck();
            deck.Burn(10);
            Assert.IsTrue(deck.Count == 42);
        }

        [TestMethod]
        public void MakeADeckWithCustomCards()
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
            Deck<CardRecord> d = new Deck<CardRecord>(list);
            Assert.IsTrue(d.Count == 10);
        }

        [TestMethod]
        public void MakeADeckWithCustomCards_ThenBurnTwo_VerifyThatRightCardsGotBurned()
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
            Deck<CardRecord> d = new Deck<CardRecord>(list);
            var card1 = d[2];
            var card2 = d[3];
            d.Burn(2);
            Assert.IsTrue(card1 == d[0]);
            Assert.IsTrue(card2 == d[1]);
            foreach (var item in d.ToList())
            {
                Console.WriteLine(item.ToString());
            }
        }

        [TestMethod]
        public void MakeADeckWithCustomCards_ThenBurnTwo_VerifyThatRightCardsGotBurned_CutTheDeck_PrintAllCards()
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
            Deck<CardRecord> d = new Deck<CardRecord>(list);
            var card1 = d[2];
            var card2 = d[3];
            d.Burn(2);
            Assert.IsTrue(card1 == d[0]);
            Assert.IsTrue(card2 == d[1]);
            d.CutTheDeck(5);
            foreach (var item in d.ToList())
            {
                Console.WriteLine(item.ToString());
            }
            Assert.IsTrue(d.Count == 8);
        }
    }
}