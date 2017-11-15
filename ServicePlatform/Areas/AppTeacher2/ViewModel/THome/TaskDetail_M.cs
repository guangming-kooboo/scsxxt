using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qx.Tools.Scsxxt.CommonExtendMethods;
using Qx.Wbs.Hqu.Entity;
namespace ServicePlatform.Areas.AppTeacher2.ViewModel.THome
{
    public class TaskDetail_M
    {
        public static TaskDetail_M ToViewModel(QuotaTaskStaffDistribution AppQuotadis,int type,int state,string taskname,string Name)
        {
            return new TaskDetail_M()
            {//定额
                _AppQuotadis= AppQuotadis,
                type= type,
                state= state,
                _taskname= taskname,
                Name= Name
            };
        }

        public string Name { get; set; }

        public int type { get; set; }

        public QuotaTaskStaffDistribution _AppQuotadis { get; set; }

        public static TaskDetail_M ToViewModel(QuantifyTaskCompletion AppQuantifycom, int type, int state, string taskname,string Name)
        {
            return new TaskDetail_M()
            {//定量
                _AppQuantifycom= AppQuantifycom,
                type= type,
                state= state,
                _taskname= taskname,
                Name= Name
            };
        }

        public string _taskname { get; set; }

        public QuantifyTaskCompletion _AppQuantifycom { get; set; }
        public int state { get; set; }
    }
}