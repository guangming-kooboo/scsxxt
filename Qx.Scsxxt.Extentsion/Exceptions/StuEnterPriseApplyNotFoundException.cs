
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qx.Scsxxt.Extentsion.Exceptions
{
   public class StuEnterPriseApplyNotFoundException : Exception
    {
        public StuEnterPriseApplyNotFoundException()
        {
        }

        public StuEnterPriseApplyNotFoundException(string message) : base(message)
        {
        }
    }
}
