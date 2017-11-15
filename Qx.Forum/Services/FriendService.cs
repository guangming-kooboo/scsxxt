using System.Collections.Generic;
using System.Linq;
using Qx.Community.Interfaces;
using Qx.Community.Models;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Community.Services
{
    public class FriendService : BaseService, IFriendService
    {
        private const string DefaultHeadImg = "/Areas/BBS/Content/Image/默认头像.png";
        //获取好友，返回值是Person
        public List<Person> GetFriend(string userid)
        {
            var friend = Db.BBS_Friend.Where(a => a.UserIDA == userid  && a.StatusID == "1").Select(b => new Person()
            {
                ID=b.UserIDB,
                HeadImg=b.BBS_Users1.HeadImg,
                Name=b.BBS_Users1.T_User.NickName
            }).ToList();
            friend.ForEach(a =>
            {
                a.HeadImg = a.HeadImg.HasValue() ? a.HeadImg : DefaultHeadImg;
            });
           return friend;
        }
        //获取加好友的请求 返回值是 Envelope
        public List<Envelope> GetFriendRequest()
        {
            return new List<Envelope>();
        }
    }
}