using System.Collections.Generic;
using System;

namespace Qx.Community.Models
{
    public class Forum
    {
        public string ID;
        public DateTime Time;
        public string ForumName;
        public string ForumExplain;
        public List<Person> ForumManager;
        public string Publisher;//版块发布人
        public int PostCount;//版块里帖子的数量
    }
}