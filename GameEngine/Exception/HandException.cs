using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Exception
{
    public class HandException : ApplicationException
    {
        public HandException(string message) :base(message)
        {

        }
    }
}
