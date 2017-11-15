
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qx.Scsxxt.Extentsion.Exceptions
{
   public class PracticePositionNotFoundException : Exception
    {
        public PracticePositionNotFoundException()
        {
        }

        public PracticePositionNotFoundException(string message) : base(message)
        {
        }
    }
}
