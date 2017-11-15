using Qx.Wbs.Entity;
using Qx.Wbs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.Wbs.ViewModels.CRUD
{
    public class ChildNodeList_Add_M
    {
        public static ChildNodeList_Add_M ToViewModel(Wbs_Nodes node)
        {
            var model = new ChildNodeList_Add_M();
            model.FatherNodeID = node.NodeID;
            return model;
        }
        [Display(Name = "父节点ID")]
        public string FatherNodeID { get; set; }
        [Display(Name = "节点ID")]
        public double? NodeID { get; set; }
        [Display(Name = "任务比重")]
        public string NodeWeight { get; set; }
        [Display(Name = "任务名称")]
        public string NodeName { get; set; }
        [Display(Name = "任务描述")]
        public string Decription { get; set; }
        [Display(Name = "任务开始时间")]
        public DateTime? BeginTime { get; set; }
        [Display(Name = "任务结束时间")]
        public DateTime? EndTime { get; set; }
        [Display(Name = "任务地点")]
        public string Place { get; set; }
    }
}