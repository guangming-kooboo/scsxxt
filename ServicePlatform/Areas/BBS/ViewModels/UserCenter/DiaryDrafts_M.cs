using Qx.Community.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qx.Community.Models;

namespace ServicePlatform.Areas.BBS.ViewModels.UserCenter
{
    public class DiaryDrafts_M
    {
        public static DiaryDrafts_M ToViewModel(List<Diary> drafts, int diaryCount,int draftCount, string id)
        {
            return new DiaryDrafts_M() { drafts = drafts, diaryCount = diaryCount, draftCount = draftCount, userid = id };
        }
        public List<Diary> drafts;
        public int diaryCount;
        public int draftCount;
        public string userid;
    }
}