using System;
using System.Collections.Generic;
using System.Web.Mvc;
namespace Qx.Community.Models
{
    public class ShareAndDiary
    {
        public string PostID;
        public string State;
        public string ID;
        public string Title;
        public DateTime  Time;
        public string Author;//分享者或写日志的作者
        public  List<SelectListItem> items;
    }
}