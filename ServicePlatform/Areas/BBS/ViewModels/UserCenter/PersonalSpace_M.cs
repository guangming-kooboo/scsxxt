using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qx.Community.Interfaces;
using Qx.Community.Models;


namespace ServicePlatform.Areas.BBS.ViewModels.UserCenter
{

    public class PersonalSpace_M
    {
        public static PersonalSpace_M ToViewModel(List<Messages> message,
            UserInformation userInfo,
            int diary, int friend, int photo, int replycount, int share, int visitor,
            List<Photo> photos,
            List<Person> friends,
            List<RecentVisitor> recentVisitor,
            List<Diary> diarys,
            List<ShareAndDiary> shares,
            string userid)
        {
            return new PersonalSpace_M()
            {
                diarys = diarys,
                friend = friends,
                photos = photos,
                message = message,
                userinformation = userInfo,
                recentVisitor = recentVisitor,
                shares = shares,
                userid = userid,
                statistics = new Statistics()
                 {
                     diary = diary,
                     Friend = friend,
                     Photo = photo,
                     Reply = replycount,
                     Share = share,
                     Visitor = visitor
                 }
            };
         

        }
        public List<RecentVisitor> recentVisitor;
        public List<Person> friend;
        public List<Diary> diarys;
        public List<ShareAndDiary> shares;
        public List<Photo> photos;
        public Statistics statistics;
        public UserInformation userinformation;
        public List<Messages> message;
        public string userid;
    }
}