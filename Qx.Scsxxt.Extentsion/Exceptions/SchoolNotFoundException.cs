using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qx.Scsxxt.Extentsion.Exceptions
{
    public class SchoolNotFoundException : Exception
    {
        public SchoolNotFoundException()
        {
        }

        public SchoolNotFoundException(string message) : base(message)
        {
        }
    }
}
