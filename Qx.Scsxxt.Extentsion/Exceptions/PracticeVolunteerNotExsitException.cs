using System;
using System.Runtime.Serialization;

namespace Qx.Scsxxt.Extentsion.Services
{
    [Serializable]
    internal class PracticeVolunteerNotExsitException : Exception
    {
        public PracticeVolunteerNotExsitException()
        {
        }

        public PracticeVolunteerNotExsitException(string message) : base(message)
        {
        }

        public PracticeVolunteerNotExsitException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PracticeVolunteerNotExsitException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}