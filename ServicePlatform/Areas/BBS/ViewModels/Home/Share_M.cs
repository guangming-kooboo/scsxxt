using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qx.Community.Interfaces;
using Qx.Community.Models;

namespace ServicePlatform.Areas.BBS.ViewModels.Home
{
   public class Share_M
    {

       public static Share_M ToViewModel(List<ShareAndDiary> Shares,int SharingCount,string id)
       {
           return new Share_M(){Shares=Shares,SharingCount=SharingCount,userid=id};
        }
        public List<ShareAndDiary> Shares;
        public int SharingCount;  //进行中的分享数量
        public string userid;
    }
}
