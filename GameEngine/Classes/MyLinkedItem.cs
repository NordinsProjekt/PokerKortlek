using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Classes
{
    public class MyLinkedItem<T>
    {
        private T value;
        public MyLinkedItem<T> ?NextItem { get; set; }
        public MyLinkedItem<T> ?PreviousItem { get; set; }
        public MyLinkedItem(T value)
        {
            this.value = value;
        }
        public T Value
        {
            get { return value; }
            set { this.value = value; }
        }
    }
}
