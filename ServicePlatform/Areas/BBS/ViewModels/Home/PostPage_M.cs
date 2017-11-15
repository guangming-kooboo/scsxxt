using System.Collections.Generic;
using Qx.Community.Interfaces;
using Qx.Community.Models;

namespace ServicePlatform.Areas.BBS.ViewModels.Home
{
   
    public class PostPage_M
    {
        public static PostPage_M ToViewModel(Post post, List<Reply> replys, Reply bestreply)
        {
            return new PostPage_M() { bestReply = bestreply, post = post, reply = replys};
        }
        public Post post;
        public List<Reply> reply;
        public Reply bestReply;
        public Column column;
    }
}