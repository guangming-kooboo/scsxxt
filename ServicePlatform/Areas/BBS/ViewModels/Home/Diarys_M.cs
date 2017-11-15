using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qx.Community.Interfaces;
using Qx.Community.Models;

namespace ServicePlatform.Areas.BBS.ViewModels.Home
{
    public class Diarys_M
    {
        public static Diarys_M ToViewModel(List<Diary> diary,int diaryreplycount,string id)
        {
            return new Diarys_M() { diary=diary, diaryreplycount=diaryreplycount, UserId=id};
        }
        public List<Diary> diary;
        int diaryreplycount;
        public string UserId;
    }
}