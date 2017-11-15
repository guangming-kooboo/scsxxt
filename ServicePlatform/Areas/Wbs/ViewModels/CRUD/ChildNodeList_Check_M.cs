using Qx.Wbs.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.Wbs.ViewModels.CRUD
{
    public class ChildNodeList_Check_M
    {
        public static ChildNodeList_Check_M ToViewModel(Wbs_Nodes node)
        {
            var model = new ChildNodeList_Check_M();
            model._BeginTime = node.BeginTime;
            model._EndTime = node.EndTime;
            model._Place = node.Place;
            model._NodeName = node.NodeName;
            model._NodeWeight = node.NodeWeight;
            model._Decription = node.Decription;
            model.NodeID = node.NodeID;
            return model;
        }
        [Display(Name = "节点ID")]
        public string NodeID { get; set; }
        [Display(Name="任务名称")]
        public string _NodeName { get; set; }
        [Display(Name = "任务描述")]
        public string _Decription { get; set; }
        [Display(Name = "任务比重")]
        public double? _NodeWeight { get; set; }
        [Display(Name = "任务开始时间")]
        public DateTime? _BeginTime { get; set; }
        [Display(Name = "任务结束时间")]
        public DateTime? _EndTime { get; set; }
        [Display(Name = "任务地点")]
        public string _Place { get; set; }
    }
}