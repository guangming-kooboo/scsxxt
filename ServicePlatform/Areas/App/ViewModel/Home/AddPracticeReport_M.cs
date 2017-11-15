using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Qx.Scsxxt.Core.Services;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class AddPracticeReport_M
    {
        public static AddPracticeReport_M ToViewModel(string uid)
        {
            return new AddPracticeReport_M()
            {
                UserID = uid
            };
        }

        public Qx.Scsxxt.Core.Services.Report ToModel()
        {
            return new Qx.Scsxxt.Core.Services.Report()
            {
                UserID= UserID,
                PracticeReport = PracticeReport
            };
        }
        public string UserID { get; set; }
        [Required(ErrorMessage = "请填写实习报告")]
        public string PracticeReport { get; set; }
    }
}