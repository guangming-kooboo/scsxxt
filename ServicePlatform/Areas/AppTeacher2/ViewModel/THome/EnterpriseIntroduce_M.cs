using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Model;
using Core.Services.Entity;

namespace ServicePlatform.Areas.AppTeacher2.ViewModel.THome
{
    public class EnterpriseIntroduce_M
    {
        public  T_Enterprise _EntpriseDetail;

        public static EnterpriseIntroduce_M ToViewModel(T_Enterprise EntpriseDetail)
        {
            return new EnterpriseIntroduce_M() {
                _EntpriseDetail= EntpriseDetail
            };
        }    
    }
}