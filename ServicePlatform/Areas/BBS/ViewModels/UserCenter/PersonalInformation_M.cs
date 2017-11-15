using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qx.Community.Interfaces;
using Qx.Community.Models;

namespace ServicePlatform.Areas.BBS.ViewModels.UserCenter
{
    public class PersonalInformation_M
    {
        public static PersonalInformation_M ToViewModel(UserInformation userinfo,int diary,int friend,int photo,int reply,int share,int visitor,string id)
        {
            return new PersonalInformation_M()
            {
                statistics = new Statistics()
                {
                    diary = diary,
                    Friend = friend,
                    Photo = photo,
                    Reply = reply,
                    Share = share,
                    Visitor = visitor
                },
                userinformation = userinfo,
                userid=id
            };
        }
        public Statistics statistics;
        public UserInformation userinformation;
        public string userid;
    }
}