using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qx.Tools.Scsxxt.CommonExtendMethods;
using Qx.Wbs.Hqu.Entity;

namespace ServicePlatform.Areas.AppTeacher2.ViewModel.THome
{
    public class Record_M
    {
        public static Record_M ToViewModel(List<WbsTask> Tasks)
        {
            return  new Record_M()
            {
                _Tasks= Tasks.Select(a => new SelectListItem() { Text = a.TaskName, Value = a.WbsTaskID }).ToList()
            };
        }
        public string WbsTaskID { get; set; }
        public List<SelectListItem> _Tasks { get; set; }
    }
}