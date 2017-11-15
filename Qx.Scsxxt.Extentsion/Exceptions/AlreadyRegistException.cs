
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qx.Scsxxt.Extentsion.Exceptions
{
   public class AlreadyRegistException : Exception
    {
        public AlreadyRegistException()
        {
        }

        public AlreadyRegistException(string message) : base(message)
        {
           
        }
    }
}
