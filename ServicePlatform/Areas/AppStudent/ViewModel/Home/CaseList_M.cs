using System.Collections.Generic;
using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class CaseList_M : BaseViewModel
    {
        public int _careerstatus;
        public string CaseName { get; set; }
        public int CaseNo { get; set; }
        public int CaseCount { get; set; }

        public int DiaryCount { get; set; }

        public string Uid { get; set; }

        public List<PracticeCase> PracticeCase { get; set; }

        public static CaseList_M ToViewModel(int careerstatus,List<PracticeCase> practicecase, int  CaseCount)
        {
            return new CaseList_M()
            {
                PracticeCase = practicecase,
               // DiaryCount= DiaryCount,
                CaseCount= CaseCount,
                _careerstatus= careerstatus
            };
        }
    }
}