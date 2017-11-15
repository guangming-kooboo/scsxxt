using System;
using System.Collections.Generic;
using Qx.Community.Interfaces;
using Qx.Community.Models;


namespace ServicePlatform.Areas.BBS.ViewModels.UserCenter
{
    public class Diarys_M
    {
        public static Diarys_M ToViewModel(int diaryCount,int draftCount, List<Diary> diary,string userid)
        {
            return new Diarys_M(){diary=diary,diaryCount=diaryCount,draftCount=draftCount,userid=userid};
        }
        public List<Diary> diary;
        public int diaryCount;
        public int draftCount;
        public string userid;
    }
}