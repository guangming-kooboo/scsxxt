using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qx.Community.Interfaces;
using Qx.Community.Models;



namespace ServicePlatform.Areas.BBS.ViewModels.UserCenter
{
    public class Share_M
    {


        public static Share_M ToViewModel(List<ShareAndDiary> Shares, int SharingCount, int CancelShare,string id)
        {
            return new Share_M() { Shares = Shares, SharingCount = SharingCount, CancelShare = CancelShare,userid=id };
         }
        public List<ShareAndDiary> Shares;
        public int SharingCount;//进行中的分享数量
        public int CancelShare;//取消分享的数量
        public string userid;
    }
}