using System.Collections.Generic;
using Qx.Community.Models;

namespace Qx.Community.Interfaces
{
    public interface IUserService
    {
        List<Person> GetUsers(string keyword);
        UserInformation GetPersonalData(string id);
        string GetHeadPicture(string id );
    }
}