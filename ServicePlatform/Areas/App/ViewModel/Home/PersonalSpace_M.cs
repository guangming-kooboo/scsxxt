using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class PersonalSpace_M
    {
        public int ForVolCount { get; set; }
        public int PerVolCount { get; set; }

        public string Uid { get; set; }

        public static PersonalSpace_M ToViewModel(string uid,int PerVolCount,int ForVolCount, UserInfo StuInfo)
        {
            return new PersonalSpace_M()
            {
                Uid=uid,
                PerVolCount= PerVolCount,
                ForVolCount= ForVolCount,
                StuInfo= StuInfo
            };
        }

        public UserInfo StuInfo { get; set; }
    } 
}