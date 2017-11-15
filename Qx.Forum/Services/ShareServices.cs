using System.Collections.Generic;
using Qx.Community.Interfaces;
using Qx.Community.Models;

namespace Qx.Community.Services
{
    public class ShareServices : BaseService, IShareService
    {
       
        //获取分享列表  返回值为Share
        public List<ShareAndDiary> GetShareList(string userid)
        {
            var share = new List<ShareAndDiary>();
           return share;
        }
        public List<ShareAndDiary> GetShareList(string userid,int count)
        {
            var share = new List<ShareAndDiary>();
            return share;
        }
        public List<ShareAndDiary> GetAllShare(string userid)
        {
            var share = new List<ShareAndDiary>();
            return share;
        }
    }
}