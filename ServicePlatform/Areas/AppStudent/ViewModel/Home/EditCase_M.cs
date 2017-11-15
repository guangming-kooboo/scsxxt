using System.Collections.Generic;
using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class EditCase_M : BaseViewModel
    {
        public int _careerstatus;
        public string Pic { get; set; }
        public int id { get; set; }

        public List<PracticeCase> Case { get; set; }

        public static EditCase_M ToViewModel(int careerstatus,List<PracticeCase> Case,int CaseNo,string CaseName,int CaseCount)
        {
            return new EditCase_M()
            {
                _careerstatus= careerstatus,
                Case = Case,
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