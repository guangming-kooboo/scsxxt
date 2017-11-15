using System.Collections.Generic;
using Qx.Community.Interfaces;
using Qx.Community.Models;

namespace ServicePlatform.Areas.BBS.ViewModels.Home
{
    public class PostList_M
    {

        public static PostList_M ToViewModel(List<Theme> Themes, List<Post> posts, Column column, string id, string filter, bool IsLogin)
        {
            return new PostList_M() { theme=Themes,post=posts,column=column,themeid=id, filter = filter,IsLogin=IsLogin };
        }
        public static PostList_M ToViewModel(List<Theme> Themes, List<Post> posts, Column column, string filter, bool IsLogin)
        {
            return new PostList_M() { theme = Themes, post = posts, column = column, filter = filter ,IsLogin=IsLogin};
        }
        public List<Theme> theme;
        public List<Post> post;
        public Column column;
        public string themeid;
        public string filter;
        public bool IsLogin;
    }
}