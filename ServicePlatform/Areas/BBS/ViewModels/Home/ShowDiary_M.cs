using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qx.Community.Models;

namespace ServicePlatform.Areas.BBS.ViewModels.Home
{
    public class ShowDiary_M
    {
        public static ShowDiary_M ToViewModel(Diary diary, List<ReplyDiary> replydiarys,string id)
        {
            return new ShowDiary_M() { diary = diary, replydiarys=replydiarys,userid=id };
        }
        public Diary diary;
        public List<ReplyDiary> replydiarys;
        public string userid;
    }
}