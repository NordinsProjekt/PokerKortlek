using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_PokerSim2022
{
    [TestClass]
    [TestCategory("Game Save")]
    public class Test_GameSave
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
        public void GenerateGame_SaveGame_RestoreGame_CheckIfEverythingMatch()
        {
            var gm = GetTestGame(4);
            gm.Save("TestGame5");
            Console.WriteLine(gm.ToString());
            gm = GetTestGame(4);
            gm.Load("TestGame5");
            Console.WriteLine(gm.ToString());
            Assert.IsTrue(gm.GetDeck.Count == 32);
            Assert.IsTrue(gm.GetDeck.GetCountSeedList() == 52);
            IGameState state = new GameStateToDB();
            state.RemoveGameSave("TestGame5");
        }

        [TestMethod]
        public void RestoreGame_HandsShouldBeCount3()
        {
            var gm = GetTestGame(4); //Startar ett game för 4 spelare
            Console.WriteLine(gm);
            gm.Load("SeedData2");//Har bara 3 spelare
            Assert.IsTrue(gm.Winner().Count == 3);
            Console.WriteLine(gm);
        }

        [TestMethod]
        public void RestoreGameFromConstructor_ShouldReturn4Hands()
        {
            GamePoker gm = new GamePoker("SeedData1");
            Assert.IsTrue(gm.Winner().Count() == 4);
            gm.Winner().ForEach(x=>Console.WriteLine(x.ToString()));
        }

        [TestMethod]
        public void RestoreGame_AndCheckDeckOrder_ViaCSVFile()
        {
            GamePoker gm = new GamePoker("SeedData1");
            var deck = gm.GetDeck;
            var text = File.ReadAllLines("deck.csv");
            for (int i = 1; i < text.Length; i++)
            {
                var card = text[i].Split(',');
                if (card.Count() !=3)
                    break;
                var drawedCard = deck.Draw();
                Assert.IsTrue(drawedCard.CardId == int.Parse(card[0]) 
                    && drawedCard.Color == card[1]
                    && drawedCard.Value == int.Parse(card[2]));
            }
        }
        
    }
}
