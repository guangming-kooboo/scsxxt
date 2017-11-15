using System.Collections.Generic;
using System.Linq;
using Qx.Community.Interfaces;
using Qx.Community.Models;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Community.Services
{
    public class VisitorService : BaseService, IVisitorService
    {
        private const string DefaultHeadImg = "/Areas/BBS/Content/Image/默认头像.png";
        //获取访客的列表，返回值是recentvisitor
        public List<RecentVisitor> GetVisitor(string userid)
        {

            var recentVisitor = Db.BBS_Visitor.Where(a => a.UserIDA == userid).Select(b => new RecentVisitor()
            {
                HeadImg = b.BBS_Users1.HeadImg,
                Name = b.BBS_Users.T_User.NickName,
                ID = b.UserIDB,
                Time = b.Time
            }).ToList();
            recentVisitor.ForEach(a =>
            {
                a.HeadImg = a.HeadImg.HasValue() ? a.HeadImg : DefaultHeadImg;
            });
           return recentVisitor;
        }
    }
}