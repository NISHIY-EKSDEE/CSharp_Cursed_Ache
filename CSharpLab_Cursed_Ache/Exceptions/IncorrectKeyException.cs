using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpLab_Cursed_Ache.Exceptions
{
    public class IncorrectKeyException : Exception
    {
        public IncorrectKeyException(string msg) : base(msg)
        {
        }
    }
}
