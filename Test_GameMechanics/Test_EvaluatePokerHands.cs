namespace Test_PokerSim2022
{
    [TestClass]
    [TestCategory("Evaluate Poker Hands Tests")]
    public class Test_EvaluatePokerHands
    {
        private EvaluatePokerHand eph = new EvaluatePokerHand();

        [TestMethod]
        public void CheckHand_ShouldReturn_RoyalStraightFlush_AndScore()
        {
            List<CardRecord> list = new List<CardRecord>()
            {
                new CardRecord(0, "Hearts", 10),
                new CardRecord(0, "Hearts", 11),
                new CardRecord(0, "Hearts", 12),
                new CardRecord(0, "Hearts", 13),
                new CardRecord(0, "Hearts", 14)
            };
            var result = eph.EvaluateHand(list);
            Assert.IsTrue(result.Message == "Royal Straight Flush");
            Assert.IsTrue(result.Score == 900);
        }

        [TestMethod]
        public void CheckHand_ShouldReturn_StraightFlush_AndScore()
        {
            List<CardRecord> list = new List<CardRecord>()
            {
                new CardRecord(0, "Hearts", 10),
                new CardRecord(0, "Hearts", 11),
                new CardRecord(0, "Hearts", 12),
                new CardRecord(0, "Hearts", 13),
                new CardRecord(0, "Hearts", 9)
            };
            var result = eph.EvaluateHand(list);
            Assert.IsTrue(result.Message == "Straight Flush");
            Assert.IsTrue(result.Score == 800);
        }

        [TestMethod]
        public void CheckHand_ShouldReturn_FourOfAKind_AndScore()
        {
            List<CardRecord> list = new List<CardRecord>()
            {
                new CardRecord(0, "Hearts", 2),
                new CardRecord(0, "Spades", 2),
                new CardRecord(0, "Clovers", 2),
                new CardRecord(0, "Diamonds", 2),
                new CardRecord(0, "Spades", 14)
            };
            var result = eph.EvaluateHand(list);
            Assert.IsTrue(result.Message == "4 of a kind of 2 and with 14 of Spades");
            Assert.IsTrue(result.Score == 702);
        }

        [TestMethod]
        public void CheckHand_ShouldReturn_FullHouse_AndScore()
        {
            List<CardRecord> list = new List<CardRecord>()
            {
                new CardRecord(0, "Spades", 10),
                new CardRecord(0, "Hearts", 10),
                new CardRecord(0, "Hearts", 9),
                new CardRecord(0, "Diamonds", 10),
                new CardRecord(0, "Spades", 9)
            };
            var result = eph.EvaluateHand(list);
            Assert.IsTrue(result.Message == "Full House");
            Assert.IsTrue(result.Score == 610);
        }

        [TestMethod]
        public void CheckHand_ShouldReturn_Flush_AndScore()
        {
            List<CardRecord> list = new List<CardRecord>()
            {
                new CardRecord(0, "Hearts", 3),
                new CardRecord(0, "Hearts", 11),
                new CardRecord(0, "Hearts", 8),
                new CardRecord(0, "Hearts", 13),
                new CardRecord(0, "Hearts", 9)
            };
            var result = eph.EvaluateHand(list);
            Assert.IsTrue(result.Message == "Flush");
            Assert.IsTrue(result.Score == 500);
            Assert.IsTrue(result.Tiebreaker == 13);
        }

        [TestMethod]
        public void CheckHand_ShouldReturn_Straight_AndScore()
        {
            List<CardRecord> list = new List<CardRecord>()
            {
                new CardRecord(0, "Hearts", 10),
                new CardRecord(0, "Clovers", 11),
                new CardRecord(0, "Hearts", 12),
                new CardRecord(0, "Hearts", 13),
                new CardRecord(0, "Spades", 9)
            };
            var result = eph.EvaluateHand(list);
            Assert.IsTrue(result.Message == "Straight");
            Assert.IsTrue(result.Score == 400);
            Assert.IsTrue(result.Tiebreaker == 13);
        }

        [TestMethod]
        public void CheckHand_ShouldReturn_ThreeOfAKind_AndScore()
        {
            List<CardRecord> list = new List<CardRecord>()
            {
                new CardRecord(0, "Hearts", 2),
                new CardRecord(0, "Spades", 2),
                new CardRecord(0, "Clovers", 2),
                new CardRecord(0, "Diamonds", 6),
                new CardRecord(0, "Spades", 14)
            };
            var result = eph.EvaluateHand(list);
            Assert.IsTrue(result.Message == "3 of a kind of 2");
            Assert.IsTrue(result.Score == 302);
        }

        [TestMethod]
        public void CheckHand_ShouldReturn_2Pair_AndScore()
        {
            List<CardRecord> list = new List<CardRecord>()
            {
                new CardRecord(0, "Spades", 10),
                new CardRecord(0, "Hearts", 2),
                new CardRecord(0, "Hearts", 9),
                new CardRecord(0, "Diamonds", 10),
                new CardRecord(0, "Spades", 9)
            };
            var result = eph.EvaluateHand(list);
            Assert.IsTrue(result.Message == "2 pairs of 10 and 9");
            Assert.IsTrue(result.Score == 210);
        }

        [TestMethod]
        public void CheckHand_ShouldReturn_1pair_AndScore()
        {
            List<CardRecord> list = new List<CardRecord>()
            {
                new CardRecord(0, "Spades", 10),
                new CardRecord(0, "Hearts", 2),
                new CardRecord(0, "Hearts", 7),
                new CardRecord(0, "Diamonds", 10),
                new CardRecord(0, "Spades", 3)
            };
            var result = eph.EvaluateHand(list);
            Assert.IsTrue(result.Message == "1 pair of 10");
            Assert.IsTrue(result.Score == 110);
        }
        //High Card
        [TestMethod]
        public void CheckHand_ShouldReturn_HighCard_AndScore()
        {
            List<CardRecord> list = new List<CardRecord>()
            {
                new CardRecord(0, "Spades", 10),
                new CardRecord(0, "Hearts", 2),
                new CardRecord(0, "Hearts", 7),
                new CardRecord(0, "Diamonds", 6),
                new CardRecord(0, "Spades", 3)
            };
            var result = eph.EvaluateHand(list);
            Assert.IsTrue(result.Message == "High Card");
            Assert.IsTrue(result.Score == 10);
        }

        [TestMethod]
        public void CheckHand_ShouldReturn_ErrorAsMessage_AndScore0()
        {
            List<CardRecord> list = new List<CardRecord>()
            {
                //Ogiltig hand
                new CardRecord(0, "Spades", 10),
                new CardRecord(0, "Hearts", 10),
                new CardRecord(0, "Hearts", 10),
                new CardRecord(0, "Diamonds", 10),
                new CardRecord(0, "Spades", 10)
            };
            var result = eph.EvaluateHand(list);
            Assert.IsTrue(result.Message == "Error");
            Assert.IsTrue(result.Score == 0);
        }

    }
}
