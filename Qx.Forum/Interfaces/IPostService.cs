using System.Collections.Generic;
using Qx.Community.Models;

namespace Qx.Community.Interfaces
{
    public interface IPostService
    {
        List<Post> GetPopularPost();
        Post GetPost(string postID);
        List<Post> GetPostDrafts(string UserID);
        List<Post> GetPostsByKeyWord(string keyword);
        List<Post> GetPostsByUserID(string userid);
        List<Post> GetPostsByThemeID(string themeid);
        List<Post> GetPostsByColumnID(string columnid);
        List<Post> GetCreamPostsByThemeID(string themeid);
        List<Post> GetCreamPostsByColumnID(string columnid);
        List<Post> GetMostNewPostsByThemeID(string themeid);
        List<Post> GetMostNewPostsByColumnID(string columnid);
        List<Reply> GetReply(string postID);
        List<Reply> GetReply(string postID,string userid);
        Reply GetBestReply(string postID);
    }
}