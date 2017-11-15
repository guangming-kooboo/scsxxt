using System.Collections.Generic;
using Core.Services.Entity;
using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class PositionEvlTable_M : BaseViewModel
    {
        public string PracticeDept { get; set; }
        public string PracticeDivDept { get; set; }

        public T_StuBatchReg AllComment { get; set; }

        public string EntName { get; set; }
        public string PositionName { get; set; }

        public string StuName { get; set; }

        public List<EntEvaluateStu> EntEvaStu { get; set; }

        public static PositionEvlTable_M ToViewModel(List<EntEvaluateStu> EntEvaStu, UserInfo StuAllInfo, Volunteer volunteer, T_StuBatchReg AllComment)
        {
            if (volunteer == null)
            {
                return new PositionEvlTable_M()
                {
                    EntEvaStu = EntEvaStu,
                    StuName = StuAllInfo.Name,
                    EntName = "暂无企业录用",
                    PositionName = "暂无企业录用",
                    AllComment = AllComment,
                    StuAllInfo= StuAllInfo,
                    PracticeDept = "暂无企业录用",
                    PracticeDivDept = "暂无企业录用",
                };
            }
            else
            {
                return new PositionEvlTable_M()
                {
                    EntEvaStu = EntEvaStu,
                    StuName = StuAllInfo.Name,
                    EntName = volunteer.EntName,
                    PositionName = volunteer.PositionName,
                    AllComment = AllComment,
                    StuAllInfo= StuAllInfo,
                    PracticeDept = volunteer.PracticeDept,
                    PracticeDivDept = volunteer.PracticeDivDept
                };
            }
        }

        public UserInfo StuAllInfo { get; set; }
    }
}