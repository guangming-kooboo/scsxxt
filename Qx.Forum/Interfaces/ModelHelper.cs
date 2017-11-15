
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qx.Community.Entity;
using Qx.Community.Models;

namespace Qx.Community.Interfaces
{
    public static class ModelHelper
    {
        

        public static Person Convert(this BBS_Users table)
        {
            throw new NotImplementedException();
            return new Person()
            {
                ID = table.T_User.UserID,
                Name = table.T_User.NickName,
                Grade = "---",
                Sex = "---",
              //  RegisterDate = "---",
              //  LastLogin = "---",
            };
        }

        public static Forum Convert(this BBS_Forum table)
        {
            throw new NotImplementedException();
            //return new Forum()
            //{
            //    Time = table.BBS_Columns.Max(a=>a.BBS_Theme.Max(b=>b.BBS_Post.Max(c=>c.PostTime))).Value,
            //    ForumName = table.ForumName,
            //    ForumExplain = table.ForumExplain,
            //    //ForumManager = table.BBS_ForumManager.Select(a => a.BBS_Users.Convert()).ToList(),
            //    Publisher = "---",
            //    PostCount = table.BBS_Columns.Sum(a => a.BBS_Theme.Sum(b => b.BBS_Post.Count))
            //};
        }

        public static Column Convert(this BBS_Columns table)
        {
            throw new NotImplementedException();
            //return new Column()
            //{
            //    TodayPost = table.BBS_Theme.Sum(b => b.BBS_Post.Count), //今日帖子数
            //    ID = table.ColumnID,
            //    Time = table.BBS_Theme.Max(a => a.BBS_Post.Max(b => b.PostTime.Value)), //最后回复时间
            //    ColumnName = table.ColumnName,
            //    ImageUrl = table.ColumnImg,
            //    ForumName = table.BBS_Forum.ForumName, //所属版块名
            //    ColumnExplain = table.ColumnExplain,
            //    ReplyCount = table.BBS_Theme.Sum(a => a.BBS_Post.Sum(b => b.),
            //    ThemeCount = table.BBS_Theme.Count,
            //    Publisher = "---", //栏目发布人
            //    PostCount = table.BBS_Theme.Sum(a => a.BBS_Post.Count),
            //   // NewHotPost = "---",
            //    //NewHotPostUrl = "#",
            //    ForumManager = table.BBS_ForumManager.Select(a => a.BBS_Users.Convert()).ToList()
            //};

        }


        public static Diary Convert(this BBS_Diary table)
        {
            throw new NotImplementedException();
            //return new Diary()
            //{
            //    Time = table.Time.Value,
            //    ID = table.DiaryID,
            //    DiaryTitle = table.DiaryTitle,
            //    AuthorId = table.UserID,
            //    Author = table.BBS_Users.T_User.NickName,
            //    Content = table.Contents,
            //    ReplyCount = table.BBS_DiaryReply.Count,
            //    PraiseCount = -1
            //};
        }
        public static Post Convert(this BBS_Post table)
        {
            throw new NotImplementedException();
            //return new Post()
            //{
            //    ID=table.PostID,
            //    Time = table.PostTime.Value,
            //    PostTitle = table.PostTitle,
            //    AuthorHeadPictureUrl = "#",
            //    AuthorID = table.UserID,
            //    Author = table.BBS_Users.T_User.NickName,
            //    ClickCount = table.PClickCount,
            //    LastTime = table.BBS_Post_Report.BBS_Post.Max(a => a.PostTime).Value,
            //    Reply = table.BBS_ReplyPost.Count,
            //    PostContent = table.PostContent,
            //    StepCount = table.BBS_StepPraise.Count,
            //    PraiseCount = table.BBS_StepPraise.Count,
            //    Status = table.BBS_C_PostState.StateName,
            //    Theme = table.BBS_Theme.ThemeName,//所属主题
            //    ThemeId = table.ThemeID,//所属主题
            //    ForumName = table.BBS_Theme.BBS_Columns.BBS_Forum.ForumName,
            //};
        }
        public static Theme Convert(this BBS_Theme table)
        {
            throw new NotImplementedException();
            //return new Theme()
            //{
            //    ID = table.ThemeID,
            //    ThemeName = table.ThemeName,
            //    ThemeExplain = table.ThemeExplain,
            //    ColumnID = table.BBS_Columns.ColumnName,//所属栏目
            //};
        }
        
    }
}