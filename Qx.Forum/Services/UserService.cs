using System.Collections.Generic;
using System.Linq;
using Qx.Community.Interfaces;
using Qx.Community.Models;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Community.Services
{
    public class UserService : BaseService, IUserService
    {
        private const string DefaultHeadImg = "/Areas/BBS/Content/Image/默认头像.png";
        //获取用户 返回值为UserInformation
        public List<Person> GetUsers(string keyword)
        {
            var searchUsers =Db.BBS_Users.Where(a=>a.T_User.NickName.Contains(keyword)).Select(b=>new Person()
            {
               ID=b.UserID,
               HeadImg = b.HeadImg,
                LastLogin=b.LastLogin,
                RegisterDate=b.RegisterDate,
                Sex="男",
                Name=b.T_User.NickName,
                Grade=b.BBS_C_UserGrade.UserGradeName
            }).ToList();
            //设置默认头像
            searchUsers.ForEach(a =>
            {
                a.HeadImg = a.HeadImg.HasValue() ? a.HeadImg : DefaultHeadImg;
            });
            return searchUsers;
        }
        //获取个人资料  返回值为UserInformation
        public UserInformation GetPersonalData(string id )
        {
           var userInformation =Db.BBS_Users.Where(a=>a.UserID==id).Select(b=>new UserInformation()
               {
                  ID=b.UserID,
                  Birthday = "1996-09-26",
                  Email = "673658226@qq.com",
                  HeadPictureUrl = b.HeadImg,
                  Name=b.T_User.NickName,
                  Sex="男",
                  LastLogin=b.LastLogin,
                  RecentActivity=b.RecentActivite,
                  Register=b.RegisterDate
               }).FirstOrDefault();
           userInformation.HeadPictureUrl = userInformation.HeadPictureUrl.HasValue() ? userInformation.HeadPictureUrl : DefaultHeadImg;
           return userInformation;
        }
        //获取用户头像  返回值为string
        public string GetHeadPicture(string id)
        {
            var HeadImg=Db.BBS_Users.Where(a => a.UserID == id).FirstOrDefault().HeadImg;
            string HeadPicture = HeadImg.HasValue()?HeadImg:DefaultHeadImg;
            return HeadPicture;
        }



    }
}