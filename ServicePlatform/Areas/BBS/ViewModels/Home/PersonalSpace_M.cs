using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qx.Community.Interfaces;
using Qx.Community.Models;

namespace ServicePlatform.Areas.BBS.ViewModels.Home
{
    public class PersonalSpace_M
    {
        public static PersonalSpace_M ToViewModel(UserInformation userInfo,
            int diary,int friend,int photo,int replycount,int share,int visitor,
            List<Photo> photos, 
            List<Person> friends,
            List<RecentVisitor> visitors,
            List<Diary> diarys,
            List<ShareAndDiary> shares,
            string userid)
        {
            return new PersonalSpace_M()
            {
                diarys = diarys,
                friend = friends,
                recentVisitor = visitors,
                shares = shares,
                photos = photos,
                statistics=new Statistics(){
                 diary=diary,
                 Friend=friend,
                 Photo=photo,
                 Reply=replycount,
                 Share=share,
                 Visitor=visitor
                },
                userinformation = userInfo,
                UserId=userid
            };
        }
        public List<RecentVisitor> recentVisitor;
        public List<Person> friend;
        public List<Diary> diarys;
        public List<ShareAndDiary> shares;
        public List<Photo> photos;
        public Statistics statistics;
        public UserInformation userinformation;
        public string UserId;

    }
}