using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class PracticeCaseList_M
    {
        public int CaseCount { get; set; }

        public int DiaryCount { get; set; }

        public string Uid { get; set; }

        public List<PracticeCase> PracticeCase { get; set; }

        public static PracticeCaseList_M ToViewModel(List<PracticeCase> practicecase,string uid, int  CaseCount)
        {
            return new PracticeCaseList_M()
            {
                PracticeCase = practicecase,
                Uid = uid,
               // DiaryCount= DiaryCount,
                CaseCount= CaseCount
            };
        }
    }
}