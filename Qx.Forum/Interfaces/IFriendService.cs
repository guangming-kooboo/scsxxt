using System.Collections.Generic;
using Qx.Community.Models;

namespace Qx.Community.Interfaces
{
    public interface IFriendService
    {
        List<Person> GetFriend(string userid);
        List<Envelope> GetFriendRequest();
    }
}