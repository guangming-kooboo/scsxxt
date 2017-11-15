using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qx.Community.Interfaces;
using Qx.Community.Models;

namespace ServicePlatform.Areas.BBS.ViewModels.Home
{
    public class MyPost_M
    {
        public static MyPost_M ToViewModel(List<Post> posts, UserInformation userinformation,int postcount,string id)
        {
            return new MyPost_M() { myPost = posts, userinformation = userinformation, postCount = postcount,userid=id };
        }
        public List<Post> myPost;
        public UserInformation userinformation;
        public int postCount;
        public string userid;
    }
}