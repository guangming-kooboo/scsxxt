using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class AddPracticeCase_M
    {
        public List<PracticeCase> CaseItemNo { get; set; }

        public string Uid { get; set; }

        public static AddPracticeCase_M ToViewModel(string uid, List<PracticeCase>caseitemno,int CaseCount)
        {
            return new AddPracticeCase_M()
            {
                Uid=uid,
                CaseItemNo = caseitemno,
                CaseCount= CaseCount+1
            };
        }

        public int CaseCount { get; set; }

        public PracticeCase ToModel()
        {
            return new PracticeCase()
            {
                PracticeNo=PracticeNo,
                CaseNo=CaseNo,
                ItemNo=ItemNo,
              //  ItemName=ItemName,
                CaseName=CaseName,
                ItemContent=ItemContent,
                Pic = Pic,
                CaseTime= CaseTime
            };
        }

        public DateTime CaseTime { get; set; }

        public string PracticeNo { get; set; }
        public int CaseNo { get; set; }
        public string ItemNo { get; set; }

        [Required(ErrorMessage = "请填写案例名字")]
        public string CaseName { get; set; }

        [Required(ErrorMessage = "请填写案例内容")]
        public string ItemContent { get; set; }
        public string ItemName { get; set; }
        public string Pic { get; set; }
    }
}