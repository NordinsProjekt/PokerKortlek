using DataLayer.Interfaces;
using GameEngine.DTO;
using GameEngine.Exception;
using DataLayer;
using DataLayer.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace GameEngine.Classes
{
    public class GamePoker
    {
        private Deck<CardRecord> carddeck;
        private Hand<CardRecord>[] hands;
        public GamePoker(string nameOfSave)
        {
            Load(nameOfSave);
        }
        public GamePoker(List<CardRecord> list,int numOfHands)
        {
            if (numOfHands > 4 || numOfHands <= 1)
                throw new GameException("Number of Players can only be between 2-4");
            carddeck = new Deck<CardRecord>(list);
            DealCardsToHands(numOfHands);
        }
        private List<CardRecord>[] MakeCardPilesForHands(int numOfHands)
        {
            List<CardRecord>[] listOfCards = new List<CardRecord>[numOfHands];
            for (int k = 0; k < numOfHands; k++)
                listOfCards[k] = new List<CardRecord>();
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < numOfHands; j++)
                    listOfCards[j].Add(carddeck.Draw());
            return listOfCards;
        }

        public void DealCardsToHands(int numOfHands)
        {
            carddeck.Shuffle();
            hands = new Hand<CardRecord>[numOfHands];
            var arr = MakeCardPilesForHands(numOfHands);
            for (int i = 0; i < numOfHands; i++)
                hands[i] = new Hand<CardRecord>(arr[i], i + 1);
        }
        public void Update(string nameOfSave)
        {
            IGameState gameState = new GameStateToDB(); //Method level scope
            //Lägg till username och vilken hand de ska ha. Det sparas i GameUser
            //Glöm inte att reverse db också.
        }
        public void Save(string nameOfSave)
        {
            IGameState gameState = new GameStateToDB(); //Method level scope
            List<HandRecord> handList = new List<HandRecord>();
            for (int i = 0; i < hands.Length; i++)
            {
                HandRecord hand = new HandRecord(hands[i].ToList(), hands[i].PlayerName, hands[i].ID);
                handList.Add(hand);
            }
            //List<List<CardRecord>> handList = new List<List<CardRecord>>();
            //for (int i = 0; i < hands.Length; i++)
            //    handList.Add(hands[i].ToList());
            List<CardRecord> deckList = carddeck.ToList();
            List<CardRecord> orgDeckList = carddeck.GetSeedList();
            GameStateRecord gsr = new GameStateRecord(handList,deckList,orgDeckList);
            try
            {
                gameState.SaveGameState(gsr, nameOfSave);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
                throw new GameException("Något gick fel med sparningen av GameState");
            }

        }

        public void Load(string nameOfSave)
        {
            IGameState gameState = new GameStateToDB(); //Method level scope
            GameStateRecord state;
            try
            {
                state = gameState.RestoreGameState(nameOfSave);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
                throw new GameException("Något gick fel med hämtningen av GameState");
            } 
            FillHands(state.Hands);
            FillDeck(state.Deck,state.DeckOrgState);
        }

        //private void FillHands(List<List<CardRecord>> list)
        //{
        //    hands = new Hand<CardRecord>[list.Count];
        //    for (int i = 0; i < list.Count; i++)
        //        hands[i] = new Hand<CardRecord>(list[i], i + 1);
        //}
        private void FillHands(List<HandRecord> list)
        {
            hands = new Hand<CardRecord>[list.Count];
            for (int i = 0; i < list.Count; i++)
                hands[i] = new Hand<CardRecord>(list[i].CardList, i + 1);
        }
        private void FillDeck(List<CardRecord> deck,List<CardRecord> seedDeck)
        {
            carddeck = new Deck<CardRecord>(deck, seedDeck);
        }

        public void RestartGame()
        {
            int numOfHands = hands.Length;
            carddeck.New();
            DealCardsToHands(numOfHands);
        }

        public void DrawCardToHand(Hand<CardRecord> hand) => hand.Draw(carddeck.Draw());

        public void ThrowCardFromHand(Hand<CardRecord> hand, int indexToThrow) => hand.Throw(indexToThrow);

        public Hand<CardRecord> GetHand(int index)
        {
            if (index < 0 || index >= hands.Length)
                throw new GameException("Index out of range");
            return hands[index];
        }

        public Hand<CardRecord>[] GetHands { get { return hands; } }
        public Deck<CardRecord> GetDeck { get { return carddeck; } }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Poker Simulator 2022");
            sb.AppendLine($"Number of Players {hands.Length}");
            var winnerList = Winner();

            for (int i = 0; i < hands.Length; i++)
            {
                sb.AppendLine($"\nPlayer {i + 1} hand");
                foreach (CardRecord hand in hands[i].ToList().OrderByDescending(x => x.Value))
                    sb.AppendLine($"\t{hand}");
                sb.AppendLine(EvaluateCardHand(hands[i]).Message);
            }
            sb.AppendLine($"Cards left in deck: {carddeck.Count}");
            sb.AppendLine($"Winner: Player {winnerList.First().ID.ToString()}");
            return sb.ToString();
        }

        public List<Hand<CardRecord>> Winner()
        {
            List<Hand<CardRecord>> list = new List<Hand<CardRecord>>();
            for (int i = 0; i < hands.Length; i++)
            {
                var result = EvaluateCardHand(hands[i]);
                hands[i].Score = result.Score;
                hands[i].TieBreaker = result.Tiebreaker;
                list.Add(hands[i]);
            }
            return list.OrderByDescending(hands => hands.Score).ThenByDescending(hands=>hands.TieBreaker).ToList();
        }
        private EvaluateCardResult EvaluateCardHand(Hand<CardRecord> h)
        {
            EvaluatePokerHand eph = new EvaluatePokerHand();
            return eph.EvaluateHand(h.ToList());
        }
    }
}
