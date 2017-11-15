
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qx.Scsxxt.Extentsion.Exceptions
{
   public class EntPracBatchNotFoundException : Exception
    {
        public EntPracBatchNotFoundException()
        {
        }

        public EntPracBatchNotFoundException(string message) : base(message)
        {
        }
    }
}
