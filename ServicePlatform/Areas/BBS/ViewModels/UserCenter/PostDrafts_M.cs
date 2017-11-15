using System;
using System.Collections.Generic;
using Qx.Community.Interfaces;
using Qx.Community.Models;


namespace ServicePlatform.Areas.BBS.ViewModels.UserCenter
{
   
    public class PostDrafts_M
    {
        public static PostDrafts_M ToViewModel(UserInformation userinformation, List<Post> drafts,
            List<Forum> forums, List<Column> columns,List<Theme> themes,int postCount,int draftCount,string id)
        {
            return new PostDrafts_M() { userinformation = userinformation, drafts = drafts, forums = forums, columns = columns, themes = themes, postCount = postCount, draftCount = draftCount,userid=id };
        }
        public UserInformation userinformation;
        public List<Post> drafts;
        public List<Forum> forums;
        public List<Column> columns;
        public List<Theme> themes;
        public int postCount;
        public int draftCount;
        public string userid;
    }
}