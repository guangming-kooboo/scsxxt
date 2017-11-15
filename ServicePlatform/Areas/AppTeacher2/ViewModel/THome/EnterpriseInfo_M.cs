using Core.Services.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.AppTeacher2.ViewModel.THome
{
    public class EnterpriseInfo_M
    {
        public T_Enterprise _EnterpriseDetail;

        public static EnterpriseInfo_M ToViewModel(T_Enterprise EnterpriseDetail)
        {
            return new EnterpriseInfo_M()
            {
                _EnterpriseDetail= EnterpriseDetail
            };
        }
    }
}