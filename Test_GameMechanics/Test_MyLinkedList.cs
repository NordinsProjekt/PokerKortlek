namespace Test_PokerSim2022
{
    [TestClass]
    [TestCategory("MyLinked List")]

    public class Test_MyLinkedList
    {
        private MyLinkedList<CardRecord> linky = new MyLinkedList<CardRecord>();
        [TestMethod]
        public void Test_AddOneItemToEmptyLinkedList_CountShouldBe1()
        {
            linky.AddFirst(new CardRecord(1, "Hearts", 2));
            Assert.IsTrue(linky.Count == 1);
        }

        [TestMethod]
        public void Test_AddTweoItemToEmptyLinkedList_CountShouldBe2()
        {
            linky.AddFirst(new CardRecord(1, "Hearts", 2));
            linky.AddFirst(new CardRecord(2, "Hearts", 3));
            Assert.IsTrue(linky.Count == 2);
        }

        [TestMethod]
        public void Test_AddTwoItemsLast_CheckOrderOnThose_Item2ShouldBeLast()
        {
            linky.AddFirst(new CardRecord(1, "Hearts", 2));
            linky.AddLast(new CardRecord(2, "Hearts", 3));
            Assert.IsTrue(linky[1].CardId == 2);
            Assert.IsTrue(linky[0].CardId == 1);
        }

        [TestMethod]
        public void Test_AddTwoItemsFirstAndLast_CheckOrderOnThose_ForEachAndCheckObjects()
        {
            linky.AddFirst(new CardRecord(1, "Hearts", 2));
            linky.AddLast(new CardRecord(2, "Hearts", 3));
            List<CardRecord> list = new List<CardRecord>()
            { new CardRecord(1, "Hearts", 2),
                new CardRecord(2, "Hearts", 3)};
            int i = 0;
            foreach (var item in linky)
                Assert.IsTrue(item == list[i++]);
        }

        [TestMethod]
        public void Test_AddThreeItemsFirstLastFirst_CheckOrderOnThose_ForEachAndCheckObjects()
        {
            linky.AddFirst(new CardRecord(1, "Hearts", 2));
            linky.AddLast(new CardRecord(2, "Hearts", 3));
            linky.AddFirst(new CardRecord(3, "Hearts", 4));
            List<CardRecord> list = new List<CardRecord>()
            {   new CardRecord(3, "Hearts", 4),
                new CardRecord(1, "Hearts", 2),
                new CardRecord(2, "Hearts", 3)};
            int i = 0;
            foreach (var item in linky)
                Assert.IsTrue(item == list[i++]);
        }

        [TestMethod]
        public void Test_AddThreeItemsFirstLastFirst_RemoveLast_CheckOrderOnThose_ForEachAndCheckObjects()
        {
            linky.AddFirst(new CardRecord(1, "Hearts", 2));
            linky.AddLast(new CardRecord(2, "Hearts", 3));
            linky.AddFirst(new CardRecord(3, "Hearts", 4));
            linky.RemoveLast();
            Assert.IsTrue(linky.Count == 2);
            List<CardRecord> list = new List<CardRecord>()
            {   new CardRecord(3, "Hearts", 4),
                new CardRecord(1, "Hearts", 2),
            };
            int i = 0;
            foreach (var item in linky)
                Assert.IsTrue(item == list[i++]);
        }

        [TestMethod]
        public void Test_AddThreeItemsFirstLastFirst_RemoveFirst_CheckOrderOnThose_ForEachAndCheckObjects()
        {
            linky.AddFirst(new CardRecord(1, "Hearts", 2));
            linky.AddLast(new CardRecord(2, "Hearts", 3));
            linky.AddFirst(new CardRecord(3, "Hearts", 4));
            linky.RemoveFirst();
            Assert.IsTrue(linky.Count == 2);
            List<CardRecord> list = new List<CardRecord>()
            {   
                new CardRecord(1, "Hearts", 2),
                new CardRecord(2, "Hearts", 3)
            };
            int i = 0;
            foreach (var item in linky)
                Assert.IsTrue(item == list[i++]);
        }

        [TestMethod]
        public void Test_AddFourItemLast_SwapHalves_CheckOrderOnThose_ForEachAndCheckObjects()
        {
            linky.AddLast(new CardRecord(1, "Hearts", 2));
            linky.AddLast(new CardRecord(2, "Hearts", 3));
            linky.AddLast(new CardRecord(3, "Hearts", 4));
            linky.AddLast(new CardRecord(4, "Hearts", 5));
            linky.SplitAndSwap(2);
            List<CardRecord> list = new List<CardRecord>()
            {   new CardRecord(3, "Hearts", 4),
                new CardRecord(4, "Hearts", 5),
                new CardRecord(1, "Hearts", 2),
                new CardRecord(2, "Hearts", 3)
            };
            int i = 0;
            foreach (var item in linky)
                Assert.IsTrue(item == list[i++]);
        }

        [TestMethod]
        public void Test_AddSixItemLast_SwapHalves_CheckOrderOnThose_ForEachAndCheckObjects()
        {
            linky.AddLast(new CardRecord(1, "Hearts", 2));
            linky.AddLast(new CardRecord(2, "Hearts", 3));
            linky.AddLast(new CardRecord(3, "Hearts", 4));
            linky.AddLast(new CardRecord(4, "Hearts", 5));
            linky.AddLast(new CardRecord(5, "Hearts", 6));
            linky.AddLast(new CardRecord(6, "Hearts", 7));
            linky.SplitAndSwap(3);
            List<CardRecord> list = new List<CardRecord>()
            {   
                new CardRecord(4, "Hearts", 5),
                new CardRecord(5, "Hearts", 6),
                new CardRecord(6, "Hearts", 7),
                new CardRecord(1, "Hearts", 2),
                new CardRecord(2, "Hearts", 3),
                new CardRecord(3, "Hearts", 4)
            };
            int i = 0;
            foreach (var item in linky)
                Assert.IsTrue(item == list[i++]);
        }
    }
}
