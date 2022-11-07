using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Exception;

namespace GameEngine.Classes
{
    public class Hand<T>
    {
        private int id;
        private int score;
        private int tiebreaker;
        private List<T> list;
        public Hand(List<T> list, int id)
        {
            if (list.Count > 5)
                throw new HandException("You cant have more than 5 items");
            this.list = list;
            this.id = id;
        }
        public T this[int index]
        {
            get
            {
                if (index >= Count || index < 0)
                    throw new HandException("Index out of range");
                return list[index];
            }
        }
        public int Count { get { return list.Count(); } }
        public int Score { get { return score; } set { score = value; } }
        public int TieBreaker { get { return tiebreaker; } set { tiebreaker = value; } }
        public int ID { get { return id; } }

        public void Draw(T item)
        {
            if (Count >= 5)
                throw new HandException("You cant have more than 5 items");
            list.Add(item);
        }

        public void Throw(int index)
        {
            if (index >= Count || index < 0)
                throw new HandException("Index out of range");
            list.RemoveAt(index);
        }

        public void RemoveAllItems()
        {
            list.Clear();
            score = 0;
        }
        public List<T> ToList()
        {
            List<T> newlist = new List<T>();
            foreach (var item in list)
                newlist.Add(item);
            return newlist;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Hand ID: {ID}");
            foreach (var item in list)
                sb.AppendLine(item.ToString());

            sb.AppendLine($"Score: {Score}");
            return sb.ToString();
        }

    }
}
