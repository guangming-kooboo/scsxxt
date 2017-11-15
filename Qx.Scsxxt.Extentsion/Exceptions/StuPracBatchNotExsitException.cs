using System;
using System.Runtime.Serialization;

namespace Qx.Scsxxt.Extentsion.Services
{
    [Serializable]
    internal class StuPracBatchNotExsitException : Exception
    {
        public StuPracBatchNotExsitException()
        {
        }

        public StuPracBatchNotExsitException(string message) : base(message)
        {
        }

        public StuPracBatchNotExsitException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StuPracBatchNotExsitException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}