using Core.Services.Entity;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class PracticeReportDetail_M : BaseViewModel
    {
        public int _careerstatus;

        public static PracticeReportDetail_M ToViewModel(int careerstatus,T_StuBatchReg AllComment,string uid)
        {
            return new PracticeReportDetail_M()
            {
                AllComment= AllComment,
                uid= uid,
                _careerstatus= careerstatus
            };
        }

        public string uid { get; set; }

        public T_StuBatchReg AllComment { get; set; }
    }
}