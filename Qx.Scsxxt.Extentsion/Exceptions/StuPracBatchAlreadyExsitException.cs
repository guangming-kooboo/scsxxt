
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qx.Scsxxt.Extentsion.Exceptions
{
   public class StuPracBatchAlreadyExsitException : Exception
    {
        public StuPracBatchAlreadyExsitException()
        {
        }

        public StuPracBatchAlreadyExsitException(string message) : base(message)
        {
        }
    }
}
