using ServicePlatform.Controllers.Base;
using System.ComponentModel.DataAnnotations;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class AddPracticeReport_M : BaseViewModel
    {
        public static AddPracticeReport_M ToViewModel( )
        {
            return new AddPracticeReport_M()
            {
                
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
       //[Required(ErrorMessage = "请填写实习报告")]
        public string PracticeReport { get; set; }
    }
}