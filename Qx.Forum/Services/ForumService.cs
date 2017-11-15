using System;
using System.Collections.Generic;
using System.Linq;
using Qx.Community.Interfaces;
using Qx.Community.Models;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Community.Services
{

    public class ForumService : BaseService, IForumService
    {
        private const string DefaultColumnImg = "/Areas/BBS/Content/Image/栏目默认图片.png";
        //这里是获取热门板块   返回值类型为 Forum
        public List<Forum> GetPopularForum()
        {
            return new List<Forum>();
        }
        public Forum GetForum(string id)
        {
            var forum = Db.BBS_Forum.Where(a => a.ForumID == id).Select(b => new Forum()
            {
                ForumName = b.ForumName,
                ID = b.ForumID,
                ForumExplain = b.ForumExplain
            }).FirstOrDefault();
            return forum;
        }
        //这里是获取父版块  返回值为Forum
        public List<Forum> GetForums()
        {
            var forums = Db.BBS_Forum.Select(b => new Forum()
            {
                ID = b.ForumID,
                ForumName = b.ForumName
            }).ToList();
            return forums;
        }
        //这里是获取子版块 返回值为Column
        public Column GetColumn(string columId)
        {
            var column = Db.BBS_Columns.Where(a => a.ColumnID == columId).Select(b => new Column()
            {
                //ImageUrl = b.ColumnImg.HasValue() ? b.ColumnImg : DefaultColumnImg,
                ForumID=b.BBS_Forum.ForumID,
                ID = b.ColumnID,
                ColumnName = b.ColumnName,
                ForumName = b.BBS_Forum.ForumName,
                PostCount=b.BBS_Theme.Sum(c=>c.BBS_Post.Count(d=>d.StateID=="1")),
                ReplyCount=b.BBS_Theme.Sum(c=>c.BBS_Post.Sum(d=>d.BBS_ReplyPosts.Count)),
                ColumnExplain=b.ColumnExplain,
            }
                ).FirstOrDefault();
            return column;
        }

        //获取多个子版块  
        public List<Column> GetColumnsByForumID(string id)
        {
            var columns = Db.BBS_Columns.Where(a => a.ForumID == id).Select(b => new Column()
            {
                ImageUrl = b.ColumnImg,
                ID = b.ColumnID,
                ColumnName = b.ColumnName
            }).ToList();
            columns.ForEach(a =>
            {
                a.ImageUrl = a.ImageUrl.HasValue() ? a.ImageUrl : DefaultColumnImg;
            });
            return columns;
        }
        public List<Column> GetColumns(string keyword)
        {
            var now=DateTime.Now;
            var todayBegin=new DateTime(now.Year,now.Month,now.Day,0,0,0);
            var columns = Db.BBS_Columns.Where(a => a.ColumnName.Contains(keyword)).
                Select(b => new Column()
            {
                ImageUrl = b.ColumnImg,
                ThemeCount=b.BBS_Theme.Count,
                PostCount=b.BBS_Theme.Sum(c=>c.BBS_Post.Count),
                TodayPost = b.BBS_Theme.Sum(c => c.BBS_Post.Count(d => d.PostTime > todayBegin&&d.StateID=="1")),
                ID=b.ColumnID,
                ColumnName = b.ColumnName,
                NewHotPost=b.BBS_Theme.
                SelectMany(c=>c.BBS_Post).
                Select(d=>new Post{
                     ClickCount=d.PClickCount,
                     PostTitle=d.PostTitle,
                     Time=d.PostTime,
                     ID=d.PostID
                }).
                OrderBy(e =>e.ClickCount).FirstOrDefault(),       
                ForumManager = b.BBS_ForumManager.Where(c => c.ForumID == b.ColumnID).Select(d => new Person()
                {        
                    ID = d.UserID,
                    Name = d.BBS_Users.T_User.NickName
                }).ToList(),
            }).ToList();
            columns.ForEach(a =>
            {
                a.ImageUrl = a.ImageUrl.HasValue() ? a.ImageUrl : DefaultColumnImg;
            });
            return columns;
        }
        //这里是获取所有板块 返回值为Zone
        public List<Zone> GetZone()
        {
            var now = DateTime.Now;
            var todayBegin = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            var zones = Db.BBS_Forum.Select(a => new Zone()
            {
                Forum = new Forum()
                {
                    ForumName = a.ForumName,
                    ForumManager = a.BBS_Columns.
                    SelectMany(m=>m.BBS_ForumManager).
                    Select(b =>
                        new Person()
                        {
                            ID = b.BBS_Users.T_User.UserID,
                            Name = b.BBS_Users.T_User.NickName
                        }
                         ).ToList()
                },
                Columns = a.BBS_Columns.Select(c => new Column()
                {
                    ImageUrl=c.ColumnImg,
                    TodayPost = c.BBS_Theme.Sum(d => d.BBS_Post.Count(e => e.PostTime > todayBegin&&e.StateID=="1")),
                    ThemeCount=c.BBS_Theme.Count,
                    PostCount=c.BBS_Theme.Sum(d=>d.BBS_Post.Count(e=>e.StateID=="1")),
                    ColumnExplain = c.ColumnExplain,
                    ColumnName = c.ColumnName,
                    ForumName = c.BBS_Forum.ForumName,
                    ForumManager=c.BBS_ForumManager.Select(m=> new Person()
                        {
                            ID = m.BBS_Users.T_User.UserID,
                            Name = m.BBS_Users.T_User.NickName
                        }).ToList(),
                    ID = c.ColumnID.ToString()
                }).ToList()
            }).ToList();
            zones.ForEach(a => a.Columns.ForEach(b => { b.ImageUrl = b.ImageUrl.HasValue() ? b.ImageUrl : DefaultColumnImg; }));
            return zones;
        }
    }
}