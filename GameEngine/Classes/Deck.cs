using GameEngine.DTO;
using GameEngine.Exception;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Classes
{
    public class Deck<T> : IEnumerable<T> where T : class
    {
        //Klassen borde ha en kopia av kortleken som skickas in,
        //Så samma kortlek används av New metoden.
        private MyLinkedList<T> decklist = new MyLinkedList<T>();
        private MyLinkedList<T> seedDeckList = new MyLinkedList<T>();
        
        public Deck(List<T> deck,List<T> seedDeck)
        {
            foreach (var item in deck)
            {
                decklist.AddLast(item);
            }
            foreach (var item in seedDeck)
            {
                seedDeckList.AddLast(item);
            }
        }
        public Deck(List<T> list)
        {
            foreach (var item in list)
            {
                decklist.AddLast(item);
                seedDeckList.AddLast(item);
            }
        }
        public T this[int index]
        { 
            get 
            {
                if (index >= Count || index < 0)
                    throw new DeckException("Index out of range");
                return decklist[index]; 
            } 
        }
        public int Count { get { return decklist.Count; } }

        /// <summary>
        /// Blandar korten som finns kvar i kortleken
        /// </summary>
        public void Shuffle()
        {
            Random rnd = new Random();
            var temp = decklist.ToList();
            MyLinkedList<T> newList = new MyLinkedList<T>();
            for (int i = 0; i < temp.Count;)
            {
                int x = rnd.Next(temp.Count);
                newList.AddLast(temp[x]);
                temp.RemoveAt(x);
            }
            decklist = newList;
        }
        /// <summary>
        /// Tar korten från seedBackupListan och bygger en ny lek.
        /// Blandar leken också.
        /// </summary>
        public void New()
        {
            MyLinkedList<T> newList = new MyLinkedList<T>();
            decklist.RemoveAll();
            foreach (var item in seedDeckList)
                decklist.AddLast(item);
            Shuffle();
        }

        public T Draw()
        {
            var item = decklist[0];
            decklist.RemoveFirst();
            return item;
        }

        public void CutTheDeck(int index)
        {
            if (index >= Count || index < 0)
                throw new DeckException("Index out of range");
            decklist.SplitAndSwap(index);
        }

        public void Burn(int number)
        {
            if (number >= Count || number < 0)
                throw new DeckException("Index out of range");
            for (int i = 0; i < number; i++)
                decklist.RemoveFirst();
        }
        public int GetCountSeedList()
        {
            return seedDeckList.Count;
        }

        public List<T> GetSeedList()
        {
            return seedDeckList.ToList();
        }
        public T[] ToArray()
        {
            return decklist.ToArray();
        }

        public List<T> ToList()
        {
            List<T> list = new List<T>();
            foreach (var item in decklist)
                list.Add(item);
            return list;
        }
        public IEnumerator<T> GetEnumerator()
        {
            return decklist.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
