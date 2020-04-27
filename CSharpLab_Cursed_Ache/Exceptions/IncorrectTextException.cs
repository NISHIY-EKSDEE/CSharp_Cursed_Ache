using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpLab_Cursed_Ache.Exceptions
{
    public class IncorrectTextException : Exception
    {
        public IncorrectTextException(string message) : base(message)
        {
        }
    }
}
