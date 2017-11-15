using System.Collections.Generic;
using Qx.Community.Models;

namespace Qx.Community.Interfaces
{
    public interface IShareService
    {
        List<ShareAndDiary> GetShareList(string userid);
        List<ShareAndDiary> GetShareList(string userid,int count);
        List<ShareAndDiary> GetAllShare(string userid);
    }
}