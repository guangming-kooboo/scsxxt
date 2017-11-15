using System.Collections.Generic;
using Qx.Community.Models;

namespace Qx.Community.Interfaces
{
    public interface IMessageService
    {
        List<Messages> GetMessage(string id );
        List<Messages> GetMessage(string id,int count);
    }
}