using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.Wbs.ViewModels.CRUD
{
    public class NodeList_Add_M
    {
       [Display(Name = "任务节点")]
        public string NodeID { get; set; }
       [Display(Name = "任务名称")]
       public string NodeName { get; set; }
        [Display(Name = "任务描述")]
       public string Decription { get; set; }
        [Display(Name = "任务地点")]
        public string Place { get; set; }
        [Display(Name = "任务开始时间")]
        public DateTime BeginTime { get; set; }
        [Display(Name = "任务结束时间")]
        public DateTime EndTime { get; set; }
    }
}