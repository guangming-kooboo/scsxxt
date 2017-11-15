using Core.Services.Entity;
using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class IdentificationTable_M
    {
        public string StuNo { get; set; }

        public string StuName { get; set; }
        public string StuSex { get; set; }
        public string SpeName { get; set; }
        public int EnteyYear { get; set; }
        public string ClassName { get; set; }

        public T_StuBatchReg AllComment { get; set; }
        public UserInfo StuAllInfo { get; set; }

        public string EntName { get; set; }
        public string PositionName { get; set; }

        public string uid { get;  set; }

        public static IdentificationTable_M ToViewModel(string uid, Volunteer volunteer, UserInfo StuAllInfo, T_StuBatchReg AllComment)
        {
            return new IdentificationTable_M()
            {
                uid=uid,
                EntName = volunteer.EntName,
                PositionName = volunteer.PositionName,
                StuAllInfo= StuAllInfo,
                AllComment= AllComment,
                StuName=StuAllInfo.Name,
                StuSex=StuAllInfo.Sex,
                EnteyYear=StuAllInfo.EntryYear,
                SpeName=StuAllInfo.SpeName,
                ClassName=StuAllInfo.ClassName,
                StuNo=StuAllInfo.StuNO
            };
        }

    }
}