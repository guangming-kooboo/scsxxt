using System;
using System.Collections.Generic;
using System.Linq;
using Qx.Community.Interfaces;

namespace Qx.Community.Services
{
    public class DashboardService : BaseService,IDashboardService
    {
        //计算好友数
        public int CalculateFriend(string userid)
        {
            int CalculateFriend=Db.BBS_Friend.Count(a=>(a.UserIDA==userid||a.UserIDB==userid)&&a.StatusID=="1");
            return CalculateFriend;
            
        }
        //计算问答数
        public int CalculateAnser(string userid)
        {
            int CalculateAnser=new Random().Next(1,1000);
            return CalculateAnser;
            
        }
        //计算日志数
        public int CalculateDiary(string userid)
        {
            var CalculateDiary = Db.BBS_Diary.Count(a => a.UserID == userid&&a.StateID=="1");
            return CalculateDiary;
            
        }
        //计算照片数量
        public int CalculatePhoto(string userid)
        {
            int CalculatePhoto = Db.BBS_Photo.Count(a => a.UserID == userid);
            return CalculatePhoto;
            
        }
        //计算分享数
        public int CalculateShare(string userid)
        {
            int CalculateShare = Db.BBS_Share.Count(a => a.UserID == userid);
            return CalculateShare;
            
        }
        public int CalculatePostCount(string userid)
        {
            int postcount = Db.BBS_Post.Count(a => a.UserID == userid&&a.StateID=="1");
            return postcount;
        }
        public int GetDiaryReplyCount(string userid)
        {
            int GetReplyCount = Db.BBS_ReplyPost.Count(a => a.UserID == userid);
            return GetReplyCount;
        }
        //计算帖子回复数
        public int GetPostReplyCount(string userid)
        {
            int GetReplyCount = Db.BBS_ReplyPost.Count(a => a.UserID == userid);
            return GetReplyCount;
        }
        //计算访客数量
        public int CalculateVisitor(string userid)
        {
            int GetVisitor = Db.BBS_Visitor.Count(a => a.UserIDA == userid);
            return GetVisitor;
        }


        public int CalculatePostDraft(string userid)
        {
            int GetDraftCount = Db.BBS_Post.Count(a => a.UserID == userid && a.StateID == "-1");
            return GetDraftCount;
        }

        public int CalculateDiaryDraft(string userid)
        {
            int GetReplyCount = Db.BBS_Diary.Count(a => a.UserID == userid && a.StateID == "0");
            return GetReplyCount;
        }

        public int CalculateMessage(string userid)
        {
            int CalculateMessage = Db.BBS_Note.Count(a => a.ReceiverUserID == userid);
            return CalculateMessage;
        }

        public int CalculateSharing(string userid)
        {
            int GetReplyCount = Db.BBS_Share.Count(a => a.UserID == userid && a.StatusID == "1");
            return GetReplyCount;
        }

        public int CalculateCancelShare(string userid)
        {
            int GetReplyCount = Db.BBS_Share.Count(a=>a.UserID==userid&&a.StatusID=="-1");
            return GetReplyCount;
        }

        public List<string> GetSearchKeyword()
        {
            return new List<string>();
        }
    }
}