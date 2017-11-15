using System;
using System.Collections.Generic;
using Qx.Community.Interfaces;
using Qx.Community.Models;


namespace ServicePlatform.Areas.BBS.ViewModels.UserCenter
{
    public class MyPost_M
    {
        public static MyPost_M ToViewModel(List<Forum> forums, List<Column> columns,List<Theme> themes,List<Post> myPosts, UserInformation userinfo,int postcount,int draftcount,string id)
        {
            return new MyPost_M() { myPost=myPosts,forums=forums,columns=columns,themes=themes, userinformation=userinfo,postCount=postcount,draftCount=draftcount,userid=id};
        }
        public List<Post> myPost;
        public UserInformation userinformation;
        public List<Forum> forums;
        public List<Column> columns;
        public List<Theme> themes;
        public int postCount;
        public int draftCount;
        public string userid;
    }
    
}