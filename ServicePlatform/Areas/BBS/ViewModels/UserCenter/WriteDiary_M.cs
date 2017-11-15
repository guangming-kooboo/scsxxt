using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.BBS.ViewModels.UserCenter
{
    public class WriteDiary_M
    {
        public static WriteDiary_M ToViewModel(string id, string title, string content,string diaryid)
        {
            return new WriteDiary_M() { userid = id,title=title,content=content,diaryid=diaryid };
        }
        public static WriteDiary_M ToViewModel(string id)
        {
            return new WriteDiary_M() { userid = id };
        }
       public string diaryid;
       public  string userid;
       public string content;
       public string title;
    }
}