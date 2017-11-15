using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qx.Scsxxt.Extentsion.Exceptions
{
   public class PracBatchNotFoundException : Exception
    {
        public PracBatchNotFoundException()
        {
        }

        public PracBatchNotFoundException(string message) : base(message)
        {
        }
    }
}
