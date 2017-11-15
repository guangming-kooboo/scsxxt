
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qx.Scsxxt.Extentsion.Exceptions
{
   public class PracticeVolunteerAlreadyExsitException : Exception
    {
        public PracticeVolunteerAlreadyExsitException()
        {
        }

        public PracticeVolunteerAlreadyExsitException(string message) : base(message)
        {
        }
    }
}
