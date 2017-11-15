using System;
using System.Collections.Generic;

namespace Qx.Community.Models
{
    public class Column
    {
        public int? TodayPost;//今日帖子数
        public string ID;
        public DateTime Time;//先把所有models 文件夹下的所有time 类型改成 DateTime 然后提交（不用理会下方的错误） 有空吧vs安装成2013 或2015   
        public string ColumnName;
        public string ImageUrl;
        public string ForumName;//所属版块名
        public string ColumnExplain;
        public int? ReplyCount;
        public int? ThemeCount;
        public string Publisher;//栏目发布人
        public int? PostCount;
        public Post NewHotPost;
        public List<Person> ForumManager;
        public string ForumID;
    }
}