using System.Collections.Generic;
using System.Linq;
using Qx.Community.Interfaces;
using Qx.Community.Models;

namespace Qx.Community.Services
{
    public class DiaryService : BaseService, IDiaryService
    {
        //获取日志  返回值为Diary
        public Diary GetDiary(string id)
        {
            var ShowDiary = Db.BBS_Diary.Where(a => a.DiaryID == id).Select(b => new Diary()
            {
                AuthorId=b.UserID,
                ID=b.DiaryID,
                DiaryTitle = b.DiaryTitle,
                Time = b.Time,
                Content = b.Contents,
                ReplyCount=b.BBS_DiaryReply.Count(c=>c.DiaryID==b.DiaryID)
            }).FirstOrDefault();
           return ShowDiary;
        }
         //获取日志列表  返回值为Diary
        public List<Diary> GetDiaryList(string id, int count)
        {
            var diary = Db.BBS_Diary.Where(a => a.UserID == id && a.StateID == "1").Select(b => new Diary()
            {
                ID = b.DiaryID,
                Time = b.Time,
                DiaryTitle = b.DiaryTitle
            }).Take(count).ToList();
            return diary;
        }
        public List<Diary> GetDiaryList(string id)
        {
            var diary = Db.BBS_Diary.Where(a => a.UserID == id&&a.StateID=="1").Select(b => new Diary()
            {
                ID = b.DiaryID,
                Time = b.Time,
                DiaryTitle = b.DiaryTitle,
                ReplyCount=b.BBS_DiaryReply.Count
            }).ToList();
            return diary;
         
         }

        public List<Diary> GetDiaryDrafts(string id)
        {
            var diary = Db.BBS_Diary.Where(a => a.UserID == id && a.StateID == "0").Select(b => new Diary()
                {
                    DiaryTitle = b.DiaryTitle,
                    Time = b.Time,
                    ReplyCount = b.BBS_DiaryReply.Count,
                    ID = b.DiaryID
                }).ToList();
            return diary;
        }

        public List<ReplyDiary> GetReplyDiarys(string id )
        {
            var replydiarys = Db.BBS_DiaryReply.Where(a => a.DiaryID == id).Select(b => new ReplyDiary()
            {
                Author = b.BBS_Users.T_User.NickName,
                Time = b.Time,
                AuthorID = b.UserID,
                AuthorHeadPicture =b.BBS_Users.HeadImg,
                Content = b.Contents
            }).ToList();
            return replydiarys;
        }
    }
}