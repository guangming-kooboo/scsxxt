using System;
using System.Collections.Generic;
using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class DiaryList_M : BaseViewModel
    {
        public int CaseCount;
        public int _careerstatus;
        public int RecordNo { get; set; }
        public List<Diary> Diarys { get;set; }
        public string Uid { get;set; }

        public static DiaryList_M ToViewModel(int careerstatus,List<Diary> diarys,int DiaryCount)
        {
            return new DiaryList_M()
            {
                Diarys = diarys,
                _careerstatus= careerstatus,
                DiaryCount = DiaryCount,
               // CaseCount= CaseCount,
                //RecordTime=diarys[]
            };
        }

        public int DiaryCount { get; set; }
        public DateTime RecordTime { get; set; }
    }
}