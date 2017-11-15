using System;
namespace Qx.Community.Models
{
    public class Diary
    {
        public DateTime? Time;
        public string ID;
        public string DiaryTitle;
        public string Author;
        public string AuthorId;
        public string Content;
        public int ReplyCount;
        public int PraiseCount;
    }
}