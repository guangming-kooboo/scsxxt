using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qx.Scsxxt.Extentsion.Exceptions
{
   public class EnterpriseNotFoundException : Exception
    {
        public EnterpriseNotFoundException()
        {
        }

        public EnterpriseNotFoundException(string message) : base(message)
        {
        }
    }
}
