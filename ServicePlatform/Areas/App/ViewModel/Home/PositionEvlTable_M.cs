using Core.Services.Entity;
using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class PositionEvlTable_M
    {
        public string PracticeDept { get; set; }
        public string PracticeDivDept { get; set; }

        public T_StuBatchReg AllComment { get; set; }

        public string EntName { get; set; }
        public string PositionName { get; set; }

        public string StuName { get; set; }

        public List<EntEvaluateStu> EntEvaStu { get; set; }

        public static PositionEvlTable_M ToViewModel(List<EntEvaluateStu> EntEvaStu, UserInfo StuAllInfo, Volunteer volunteer, T_StuBatchReg AllComment,string uid)
        {
            return new PositionEvlTable_M()
            {
                EntEvaStu = EntEvaStu,
                StuName=StuAllInfo.Name,
                EntName = volunteer.EntName,
                PositionName = volunteer.PositionName,
                AllComment= AllComment,
                uid=uid,
                PracticeDept=volunteer.PracticeDept,
                PracticeDivDept=volunteer.PracticeDivDept
            };
        }

        public string uid { get; set; }
    }
}