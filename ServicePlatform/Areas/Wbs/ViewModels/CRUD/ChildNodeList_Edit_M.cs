using Qx.Wbs.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.Wbs.ViewModels.CRUD
{
    public class ChildNodeList_Edit_M
    {
        public static ChildNodeList_Edit_M ToViewModel(Wbs_Nodes node)
        {
            var model = new ChildNodeList_Edit_M();
            model.NodeID = node.NodeID;
            model.NodeName = node.NodeName;
            model.NodeWeight = node.NodeWeight;
            model.Decription = node.Decription;
            model.BeginTime = node.BeginTime;
            model.EndTime = node.EndTime;
            model.Place = node.Place;
            return model;
        }
        [Display(Name = "节点ID")]

        public string NodeID { get; set; }
        [Display(Name = "任务比重")]

        public double? NodeWeight { get; set; }
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