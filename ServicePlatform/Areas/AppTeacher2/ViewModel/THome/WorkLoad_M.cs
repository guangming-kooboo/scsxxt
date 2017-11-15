using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qx.Wbs.Hqu.Entity;

namespace ServicePlatform.Areas.AppTeacher2.ViewModel.THome
{
    public class WorkLoad_M
    {
        public static WorkLoad_M ToViewModel(List<WbsTask> Tasks, double total, List<CheckRecord> tasklist, string type)
        {
            return new WorkLoad_M()
            {
                type= type,
                _tasklist = tasklist,
                _total = total,
                _Tasks = Tasks.Select(a => new SelectListItem() { Text = a.TaskName, Value = a.WbsTaskID }).ToList()
            };
        }
        public List<CheckRecord> _tasklist { get; set; }

        public double _total { get; set; }

        public string WbsTaskID { get; set; }
        public List<SelectListItem> _Tasks { get; set; }

        public string type { get; set; }
        public List<SelectListItem> _Type = new List<SelectListItem>(){
                  new SelectListItem(){Text="全部",Value="2",Selected=true},
                  new SelectListItem(){Text="定额",Value="0"},
                  new SelectListItem(){Text="定量",Value="1"}};
    }
}