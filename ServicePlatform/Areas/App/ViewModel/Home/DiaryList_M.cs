using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class DiaryList_M
    {
        public int CaseCount;

        public List<Diary> Diarys { get;set; }
        public string Uid { get;set; }

        public static DiaryList_M ToViewModel(List<Diary> diarys,string uid,int DiaryCount)
        {
            return new DiaryList_M()
            {
                Diarys = diarys,
                Uid =uid,
                DiaryCount= DiaryCount,
               // CaseCount= CaseCount,
                //RecordTime=diarys[]
            };
        }

        public int DiaryCount { get; set; }
        public DateTime RecordTime { get; set; }
    }
}