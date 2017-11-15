using System.Collections.Generic;
using Qx.Community.Models;

namespace Qx.Community.Interfaces
{
    public interface IPhotoService
    {
        List<Photo> GetPicture(string userid);
        List<Photo> GetPicture(string userid,int count);
    }
}