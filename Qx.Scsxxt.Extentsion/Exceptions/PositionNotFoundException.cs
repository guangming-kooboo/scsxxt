
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qx.Scsxxt.Extentsion.Exceptions
{
   public class PositionNotFoundException : Exception
    {
        public PositionNotFoundException()
        {
        }

        public PositionNotFoundException(string message) : base(message)
        {
        }
    }
}
