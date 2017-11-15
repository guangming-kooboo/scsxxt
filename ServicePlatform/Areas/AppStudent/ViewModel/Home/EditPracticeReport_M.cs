using ServicePlatform.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Services.Entity;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class EditPracticeReport_M : BaseViewModel
    {
        public static EditPracticeReport_M ToViewModel(T_StuBatchReg AllComment)
        {
            return new EditPracticeReport_M()
            {
                _AllComment= AllComment
            };
        }
        public Qx.Scsxxt.Core.Services.Report ToModel()
        {
            return new Qx.Scsxxt.Core.Services.Report()
            {
                UserID = UserID,
                PracticeReport = PracticeReport
            };
        }
        public string UserID { get; set; }
        //[Required(ErrorMessage = "请填写实习报告")]

        public T_StuBatchReg _AllComment { get; set; }
        public string PracticeReport { get; set; }
    }
}