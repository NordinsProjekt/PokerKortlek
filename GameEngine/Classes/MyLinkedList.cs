using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Classes
{
    public class MyLinkedList<T> : IEnumerable<T>
    {
        private MyLinkedItem<T>? FirstElement { get; set; }
        private MyLinkedItem<T>? LastElement { get; set; }
        private int count;
        public int Count => count;
        public T this[int index] => GetListItem(index).Value;

        private MyLinkedItem<T> GetListItem(int index)
        {
            if (index < 0) return null;
            var current = FirstElement;
            for (int i = 0; i < index; i++)
            {
                current = current.NextItem;
            }
            return current;
        }
        public void AddFirst(T value)
        {
            //Lägg till ett element först i listan.
            //Utan att tappa resten av listan
            if (FirstElement == null)
            {
                FirstElement = new MyLinkedItem<T>(value);
                LastElement = FirstElement;
                count++;
            }
            else
            {
                var item = FirstElement;
                FirstElement = new MyLinkedItem<T>(value);
                FirstElement.NextItem = item;
                FirstElement.NextItem.PreviousItem = item;
                count++;
            }
        }
        public void AddLast(T value)
        {
            //Lägg till element i slutet av listan.
            if (FirstElement == null)
            {
                FirstElement = new MyLinkedItem<T>(value);
                LastElement = FirstElement;
                count++;
            }
            else
            {
                var last = LastElement;
                LastElement = new MyLinkedItem<T>(value);
                LastElement.PreviousItem = last;
                LastElement.PreviousItem.NextItem = LastElement;
                count++;
            }

        }
        public void RemoveFirst()
        {
            //Tar bort första elementet
            if (FirstElement == null)
                return;
            var item = FirstElement.NextItem;
            if (item != null)
                item.PreviousItem = null;
            FirstElement = item;
            count--;
            if (count == 1)
                LastElement = FirstElement;
        }
        public void RemoveLast()
        {
            //Tar bort sista elementet
            if (Count == 0)
                return;
            if (LastElement == null)
                return;
            var prevlast = GetListItem(Count - 2);
            prevlast.NextItem = default;
            LastElement = prevlast;
            count--;
        }

        public void RemoveAll()
        {
            FirstElement = null;
            LastElement = null;
            count = 0;
        }

        public void SplitAndSwap(int index)
        {
            if (index <= 0 || index >= Count)
                throw new IndexOutOfRangeException();

            var requested = GetListItem(index); //Nya FirstItem
            var requestedPrev = requested.PreviousItem;  //Nya LastItem
            //Klipper av länkarna
            requested.PreviousItem = null;
            requestedPrev.NextItem = null;

            //Flyttar pekarna
            FirstElement = requested;
            LastElement = requestedPrev;

            //Letar upp början och slutet i de olika delarna
            var current = FirstElement;
            while(current.NextItem != null)
                current = current.NextItem;

            var currentPrev = LastElement;
            while (currentPrev.PreviousItem != null)
                currentPrev = currentPrev.PreviousItem;

            //Binder ihop dem så next item och prev items kan skapa listor i asc/desc
            current.NextItem = currentPrev;
            currentPrev.PreviousItem = current;
        }

        public T[] ToArray()
        {
            T[] array = new T[count];
            var current = FirstElement;
            int x = 0;
            while (current != null)
            {
                array[x++] = current.Value;
                current = current.NextItem;
            }
            return array;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var nextitem = FirstElement;
            while (nextitem != null)
            {
                yield return nextitem.Value;
                nextitem = nextitem.NextItem;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
