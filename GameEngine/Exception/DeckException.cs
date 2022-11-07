using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Exception
{
    public class DeckException : ApplicationException
    {
        public DeckException(string message) : base(message)
        {

        }
    }
}
