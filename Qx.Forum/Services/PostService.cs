using System.Collections.Generic;
using System.Linq;
using Qx.Community.Interfaces;
using Qx.Community.Models;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Community.Services
{
    public class PostService : BaseService, IPostService
    {
        private const string DefaultHeadImg = "/Areas/BBS/Content/Image/默认头像.png";
        //获取热门帖子  返回值为Post
        public List<Post> GetPopularPost()
        {
            return new List<Post>();
        }

        //获取帖子  返回值为Post
        public Post GetPost(string postID)
        {
             //增加阅读量
            var old= Db.BBS_Post.Find(postID);
            old.PClickCount++;
            Db.SaveModified(old);

            var post = Db.BBS_Post.Where(a => a.PostID == postID).Select(b => new Post()
            {
                ThemeId=b.ThemeID,
                AuthorID=b.UserID,
                Author = b.BBS_Users.T_User.NickName,
                PraiseCount = b.BBS_StepPraise.Count,
                ID = b.PostID,
                PostTitle = b.PostTitle,
                PostContent = b.PostContent,
                Time = b.PostTime,
                Reply = b.BBS_ReplyPosts.Count,
                ClickCount = b.PClickCount,
                Theme=b.BBS_Theme.ThemeName,
                Column=b.BBS_Theme.BBS_Columns.ColumnName,
                ColumnID=b.BBS_Theme.ColumnID,
                 file=b.Files
            }).FirstOrDefault();  
            return post;
        }

        public List<Post> GetPostDrafts(string UserID)
        {
            var drafts = Db.BBS_Post.Where(a => a.UserID == UserID && a.StateID == "0").Select(b => new Post()
            {
                ForumName = b.BBS_Theme.BBS_Columns.BBS_Forum.ForumName,
                PostTitle = b.PostTitle,
                ID = b.PostID,
                Time=b.PostTime
            }).ToList();
            return drafts;
        }

        //获取帖子列表  
        public List<Post> GetPostsByKeyWord(string keyword)
        {
            var posts = Db.BBS_Post.Where(a => a.PostTitle.Contains(keyword)&&a.StateID=="1").Select(b => new Post()
            {
                 
                AuthorID=b.UserID,
                AuthorHeadPictureUrl=b.BBS_Users.HeadImg,
                Theme = b.BBS_Theme.ThemeName,
                PostTitle = b.PostTitle,
                Time = b.PostTime,
                LastTime = b.BBS_ReplyPosts.OrderBy(c => c.Time).FirstOrDefault().Time,
                Author = b.BBS_Users.T_User.NickName
            }).ToList();
            //设置默认头像
            posts.ForEach(a =>
            {
                a.AuthorHeadPictureUrl = a.AuthorHeadPictureUrl.HasValue() ? a.AuthorHeadPictureUrl : DefaultHeadImg;
            });
            return posts;
        }
        public List<Post> GetPostsByThemeID(string themeID)
        {
            var posts = Db.BBS_Post.Where(a => a.BBS_Theme.ThemeID == themeID && a.StateID == "1").Select(b => new Post()
            {
                AuthorHeadPictureUrl = b.BBS_Users.HeadImg,
                AuthorID=b.UserID,
                ID = b.PostID.ToString(),
                Theme = b.BBS_Theme.ThemeName,
                PostTitle = b.PostTitle,
                Author = b.BBS_Users.T_User.NickName,
                ClickCount = b.PClickCount,
                LastTime = b.BBS_ReplyPosts.OrderBy(c => c.Time).FirstOrDefault().Time,
                Reply = b.BBS_ReplyPosts.Count,
                Time = b.PostTime,
            }).ToList();
            //设置默认头像
            posts.ForEach(a=>{
                a.AuthorHeadPictureUrl = a.AuthorHeadPictureUrl.HasValue() ? a.AuthorHeadPictureUrl : DefaultHeadImg;
            });
            return posts;
        }
        public List<Post> GetPostsByUserID(string userid)
        {
            var posts = Db.BBS_Post.Where(a => a.UserID==userid&&a.StateID=="1").Select(b => new Post()
            {
                 AuthorHeadPictureUrl=b.BBS_Users.HeadImg,
                AuthorID = b.UserID,
                Author=b.BBS_Users.T_User.NickName,
                ID = b.PostID,
                PostTitle = b.PostTitle,
                Reply = b.BBS_ReplyPosts.Count,
                ForumName=b.BBS_Theme.BBS_Columns.BBS_Forum.ForumName,
                Time=b.PostTime,
                ColumnID=b.BBS_Theme.ColumnID
            }).ToList();
            //设置默认头像
            posts.ForEach(a =>
            {
                a.AuthorHeadPictureUrl = a.AuthorHeadPictureUrl.HasValue() ? a.AuthorHeadPictureUrl : DefaultHeadImg;
            });
            return posts;
        }
        public List<Post> GetPostsByColumnID(string columnID)
        {
            var posts = Db.BBS_Post.Where(a => a.BBS_Theme.BBS_Columns.ColumnID == columnID && a.StateID == "1").Select(b => new Post()
            {
                AuthorHeadPictureUrl=b.BBS_Users.HeadImg,
                AuthorID = b.UserID,
                ID = b.PostID.ToString(),
                Theme = b.BBS_Theme.ThemeName,
                PostTitle = b.PostTitle,
                Author = b.BBS_Users.T_User.NickName,
                ClickCount = b.PClickCount,
                LastTime = b.BBS_ReplyPosts.OrderBy(c => c.Time).FirstOrDefault().Time,
                Reply = b.BBS_ReplyPosts.Count,
                Time = b.PostTime,
            }).ToList();
            //设置默认头像
            posts.ForEach(a =>
            {
                a.AuthorHeadPictureUrl = a.AuthorHeadPictureUrl.HasValue() ? a.AuthorHeadPictureUrl : DefaultHeadImg;
            });
            return posts;
        }
        public List<Post> GetCreamPostsByThemeID(string themeid)
        {
            var posts = Db.BBS_Post.Where(a => a.ThemeID == themeid && a.StateID == "1").Select(b => new Post()
            {
                AuthorHeadPictureUrl = b.BBS_Users.HeadImg,
                AuthorID = b.UserID,
                ID = b.PostID.ToString(),
                Theme = b.BBS_Theme.ThemeName,
                PostTitle = b.PostTitle,
                Author = b.BBS_Users.T_User.NickName,
                ClickCount = b.PClickCount,
                LastTime = b.BBS_ReplyPosts.OrderBy(c => c.Time).FirstOrDefault().Time,
                Reply = b.BBS_ReplyPosts.Count,
                Time = b.PostTime,
            }).OrderByDescending(c => c.ClickCount).ToList();
           //设置默认头像
           posts.ForEach(a =>
           {
               a.AuthorHeadPictureUrl = a.AuthorHeadPictureUrl.HasValue() ? a.AuthorHeadPictureUrl : DefaultHeadImg;
           });
           return posts;
        }
        public List<Post> GetCreamPostsByColumnID(string columnid)
        {
            var posts = Db.BBS_Post.Where(a => a.BBS_Theme.BBS_Columns.ColumnID == columnid && a.StateID == "1").Select(b => new Post()
            {
                 AuthorHeadPictureUrl=b.BBS_Users.HeadImg,
                AuthorID = b.UserID,
                ID = b.PostID.ToString(),
                Theme = b.BBS_Theme.ThemeName,
                PostTitle = b.PostTitle,
                Author = b.BBS_Users.T_User.NickName,
                ClickCount = b.PClickCount,
                LastTime = b.BBS_ReplyPosts.OrderBy(c => c.Time).FirstOrDefault().Time,
                Reply = b.BBS_ReplyPosts.Count,
                Time = b.PostTime,
            }).OrderByDescending(c=>c.ClickCount).ToList();
            //设置默认头像
            posts.ForEach(a =>
            {
                a.AuthorHeadPictureUrl = a.AuthorHeadPictureUrl.HasValue() ? a.AuthorHeadPictureUrl : DefaultHeadImg;
            });
            return posts;
        }
       public List<Post> GetMostNewPostsByThemeID(string themeid)
        {
            var posts = Db.BBS_Post.Where(a => a.BBS_Theme.ThemeID == themeid && a.StateID == "1").Select(b => new Post()
            {
                 AuthorHeadPictureUrl=b.BBS_Users.HeadImg,
                AuthorID = b.UserID,
                ID = b.PostID.ToString(),
                Theme = b.BBS_Theme.ThemeName,
                PostTitle = b.PostTitle,
                Author = b.BBS_Users.T_User.NickName,
                ClickCount = b.PClickCount,
                LastTime = b.BBS_ReplyPosts.OrderBy(c => c.Time).FirstOrDefault().Time,
                Reply = b.BBS_ReplyPosts.Count,
                Time = b.PostTime,
            }).OrderBy(c=>c.Time).ToList();
            //设置默认头像
            posts.ForEach(a =>
            {
                a.AuthorHeadPictureUrl = a.AuthorHeadPictureUrl.HasValue() ? a.AuthorHeadPictureUrl : DefaultHeadImg;
            });
            return posts;
        }
       public  List<Post> GetMostNewPostsByColumnID(string columnid)
        {
            var posts = Db.BBS_Post.Where(a => a.BBS_Theme.BBS_Columns.ColumnID == columnid && a.StateID == "1").Select(b => new Post()
            {
                 AuthorHeadPictureUrl=b.BBS_Users.HeadImg,
                AuthorID = b.UserID,
                ID = b.PostID.ToString(),
                Theme = b.BBS_Theme.ThemeName,
                PostTitle = b.PostTitle,
                Author = b.BBS_Users.T_User.NickName,
                ClickCount = b.PClickCount,
                LastTime = b.BBS_ReplyPosts.OrderBy(c => c.Time).FirstOrDefault().Time,
                Reply = b.BBS_ReplyPosts.Count,
                Time = b.PostTime,
            }).OrderBy(c => c.Time).ToList();
            //设置默认头像
            posts.ForEach(a =>
            {
                a.AuthorHeadPictureUrl = a.AuthorHeadPictureUrl.HasValue() ? a.AuthorHeadPictureUrl : DefaultHeadImg;
            });
            return posts;
        }
        //获取回复  返回值为Reply
        public List<Reply> GetReply(string postID)
        {
            var post = Db.BBS_Post.Find(postID);
            var reply = Db.BBS_ReplyPost.Where(a => a.PostID == postID&&a.ReplyPostID!=post.BestReplyID ).Select(b => new Reply()
                {
                    FatherID=b.ParentReplyID,
                    ID=b.ReplyPostID,
                    HeadPictureUrl=b.BBS_Users.HeadImg,
                    AuthorID=b.UserID,
                    Author = b.BBS_Users.T_User.NickName,
                    Time = b.Time,
                    Content = b.Contents,
                    PraiseCount = b.BBS_ReplyParise.Count(c => c.ReplyPostID == b.ReplyPostID),
                    file=b.Files
                }).OrderByDescending(c=>c.Time).ToList();
            //设置默认头像
            reply.ForEach(a =>
            {
                a.HeadPictureUrl = a.HeadPictureUrl.HasValue() ? a.HeadPictureUrl : DefaultHeadImg;
            });
            return reply;
        }
        //只看楼主
        public List<Reply> GetReply(string postID,string id)
        {
            var post = Db.BBS_Post.Find(postID);
            var reply = Db.BBS_ReplyPost.Where(a => a.PostID == postID && a.UserID == id && a.ReplyPostID != post.BestReplyID).Select(b => new Reply()
            {
                FatherID = b.ParentReplyID,
                ID=b.ReplyPostID,
                HeadPictureUrl = b.BBS_Users.HeadImg,
                AuthorID = b.UserID,
                Author = b.BBS_Users.T_User.NickName,
                Time = b.Time,
                Content = b.Contents,
                PraiseCount=b.BBS_ReplyParise.Count(c=>c.ReplyPostID==b.ReplyPostID),
                file=b.Files
            }).OrderByDescending(c=>c.Time).ToList();
            //设置默认头像
            reply.ForEach(a =>
            {
                a.HeadPictureUrl = a.HeadPictureUrl.HasValue() ? a.HeadPictureUrl : DefaultHeadImg;
            });
            return reply;
        }
        public Reply GetBestReply(string postID)
        {
            var post = Db.BBS_Post.Find(postID);
            var bestReply = Db.BBS_ReplyPost.Where(a => a.PostID == post.BestReplyID ).Select(b => new Reply()
            {
                FatherID = b.ParentReplyID,
                HeadPictureUrl=b.BBS_Users.HeadImg,
                AuthorID = b.UserID,
                Author = b.BBS_Users.T_User.NickName,
                Time = b.Time,
                Content = b.Contents,
                PraiseCount = b.BBS_ReplyParise.Count(c=>c.ReplyPostID==b.ReplyPostID),
                file=b.Files
                
            }).FirstOrDefault();
            //设置默认头像
            if(bestReply!=null)
              bestReply.HeadPictureUrl=bestReply.HeadPictureUrl.HasValue()?bestReply.HeadPictureUrl:DefaultHeadImg;
            return bestReply;
        }
    }
}