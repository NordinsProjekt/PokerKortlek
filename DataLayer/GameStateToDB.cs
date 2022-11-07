using Database;
using DataLayer.DTO;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Xml.Linq;

namespace DataLayer
{
    public class GameStateToDB : IGameState
    {
        public void SaveGameState(GameStateRecord gsr, string nameOfSave)
        {
            PokerSimulator2022Context ps = new PokerSimulator2022Context();
            if (ps.Game.Where(x => x.Name == nameOfSave).Any())
                throw new Exception("Savefile already exist");
            //Händer
            Game g = new Game() { Name = nameOfSave ,Start = DateTime.Now};
            for (int i = 0; i < gsr.Hands.Count; i++)
            {
                var hand = ps.Location.Where(x => x.Name == $"Hand{i + 1}").First();
                for (int j = 0; j < gsr.Hands[i].Count; j++)
                {
                    var card = gsr.Hands[i].Skip(j).Take(1).First();
                    var c = ps.Cards.Where(x => x.Id == card.CardId).First();
                    Save row = new Save() { Card = c, Location = hand, Game = g, CustomOrder = j+1 };
                    ps.Save.Add(row);
                }
            }
            //MainDeck
            var deck = ps.Location.Where(x => x.Name == "MainDeck").First();
            for (int i = 0; i < gsr.Deck.Count; i++)
            {
                var card = gsr.Deck.Skip(i).Take(1).First();
                var c = ps.Cards.Where(x => x.Id == card.CardId).First();
                Save row = new Save() { Card = c, Location = deck, Game = g, CustomOrder = i + 1 };
                ps.Save.Add(row);
            }

            //SeedDeck
            var deckStored = ps.Location.Where(x => x.Name == "StoredDeck").First();
            for (int i = 0; i < gsr.DeckOrgState.Count; i++)
            {
                var card = gsr.DeckOrgState.Skip(i).Take(1).First();
                var c = ps.Cards.Where(x => x.Id == card.CardId).First();
                Save row = new Save() { Card = c, Location = deckStored, Game = g, CustomOrder = i + 1 };
                ps.Save.Add(row);
            }
            ps.SaveChanges();
        }

        public GameStateRecord RestoreGameState(string nameOfSave)
        {
            PokerSimulator2022Context ps = new PokerSimulator2022Context();
            var gameId = ps.Game.Where(x => x.Name.Equals(nameOfSave)).Select(x => x.Id).FirstOrDefault();
            if (gameId == 0)
                throw new Exception("Save doesnt exist");
            var save = ps.Save.Include(c=>c.Card)
                .Include(c=>c.Location)
                .Include(c=>c.Game)
                .Where(x => x.GameId == gameId).ToList();
            List<List<CardRecord>> hands = GetHands(save);
            List<CardRecord> deck = GetDeck(save,"MainDeck");
            List<CardRecord> deckOrgState = GetDeck(save, "StoredDeck");
            return new GameStateRecord(hands,deck,deckOrgState);
        }

        public void RemoveGameSave(string nameOfSave)
        {
            PokerSimulator2022Context ps = new PokerSimulator2022Context();
            Game g = ps.Game.Where(x => x.Name.Equals(nameOfSave)).FirstOrDefault();
            if (g != null)
            {
                var s = ps.Save.Where(x => x.GameId == g.Id).ToList();
                foreach (var item in s)
                {
                    ps.Save.Remove(item);
                }
                ps.Game.Remove(g);
            }
            ps.SaveChanges();
        }

        private List<List<CardRecord>> GetHands(List<Save> dbHands)
        {
            List<List<CardRecord>> hands = new List<List<CardRecord>>();
            int numOfHands = dbHands.Where(x => x.Location.Name.StartsWith("Hand")).GroupBy(x=>x.Location.Name).Count();
            for (int i = 0; i < numOfHands; i++)
            {
                int handNum = i + 1;
                var hand = dbHands.Where(x => x.Location.Name.Equals($"Hand{handNum}")).OrderBy(y => y.CustomOrder).ToList();
                hands.Add(new List<CardRecord>());
                for (int j = 0; j < hand.Count; j++)
                {
                    hands[i].Add(GetCard(hand[j].Card));
                }
            }
            return hands;
        }

        private List<CardRecord> GetDeck(List<Save> dbDeck,string nameOfDeck)
        {
            List<CardRecord> deck = new List<CardRecord>();
            var savedDeck = dbDeck.Where(x => x.Location.Name.Equals($"{nameOfDeck}")).OrderBy(y => y.CustomOrder).ToList();
            foreach (var item in savedDeck)
            {
                deck.Add(GetCard(item.Card));
            }
            return deck;
        }
        private CardRecord GetCard(Cards c) => new CardRecord(c.Id, c.Color, c.Number);
    }
}