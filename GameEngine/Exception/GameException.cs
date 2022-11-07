using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Exception
{
    public class GameException : ApplicationException
    {
        public GameException(string message) : base(message)
        {

        }
    }
}
