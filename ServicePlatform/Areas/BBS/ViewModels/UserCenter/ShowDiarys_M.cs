using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qx.Community.Interfaces;
using Qx.Community.Models;



namespace ServicePlatform.Areas.BBS.ViewModels.UserCenter
{
    public class ShowDiarys_M
    {

        public static ShowDiarys_M ToViewModel(Diary showDiarys, List<ReplyDiary> replydiarys,string id)
        {
            return new ShowDiarys_M() { showDiarys = showDiarys, replydiarys = replydiarys,userid=id};
        }
        public Diary showDiarys;
        public List<ReplyDiary> replydiarys;
        public string userid;
    }
}