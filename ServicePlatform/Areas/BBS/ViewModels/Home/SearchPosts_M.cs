using System.Collections.Generic;
using Qx.Community.Interfaces;
using Qx.Community.Models;


namespace ServicePlatform.Areas.BBS.ViewModels.Home
{
    public class SearchPosts_M
    {
        public static SearchPosts_M ToViewModel(List<Post> posts, string keyword)
        {
            return new SearchPosts_M() {posts=posts,keyword=keyword};
        }

        public List<Post> posts;
        public string keyword;
    }
}