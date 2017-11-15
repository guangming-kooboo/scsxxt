using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class EditPracticeCase_M
    {
        public string Pic { get; set; }

        public List<PracticeCase> Case { get; set; }

        public static EditPracticeCase_M ToViewModel(List<PracticeCase> Case,int CaseNo,string CaseName,int CaseCount)
        {
            return new EditPracticeCase_M()
            {
                Case= Case,
                CaseNo = CaseNo,
                CaseName= CaseName,
                CaseCount= CaseCount
            };
        }

        public int CaseCount { get; set; }

        public PracticeCase ToModel()
        {
            return new PracticeCase()
            {
                PracticeNo = PracticeNo,
                CaseNo = CaseNo,
                ItemNo = ItemNo,
                //  ItemName=ItemName,
                CaseName = CaseName,
                ItemContent = ItemContent
            };
        }
        public string PracticeNo { get; set; }
        public int CaseNo { get; set; }
        public string ItemNo { get; set; }
        public string CaseName { get; set; }
        public string ItemContent { get; set; }
        public string ItemName { get; set; }
    }
}