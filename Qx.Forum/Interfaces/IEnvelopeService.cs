using System.Collections.Generic;
using Qx.Community.Models;

namespace Qx.Community.Interfaces
{
    public interface IEnvelopeService
    {
        List<Envelope> GetPersonalMessage();
        List<Envelope> GetPublicMessage();
    }
}