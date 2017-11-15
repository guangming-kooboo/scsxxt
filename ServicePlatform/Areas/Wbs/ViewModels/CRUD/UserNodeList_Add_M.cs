using Qx.Wbs.Entity;
using Qx.Wbs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.Wbs.ViewModels.CRUD
{
    public class UserNodeList_Add_M 
    {
        public static UserNodeList_Add_M ToViewModel(Wbs_Nodes node)
        {
            var model = new UserNodeList_Add_M();
            model.NodeID = node.NodeID;
            model._NodeName = node.NodeName;
            model._Decription = node.Decription;
            return model;
        }
         [Display(Name = "编号")]
        public string SerialID { get; set; }
         [Display(Name = "账号")]
        public string UserID { get; set; }
         [Display(Name = "预分配")]
        public double? UserWeight { get; set; }
          [Display(Name = "任务ID")]
        public string NodeID { get; set; }
        [Display(Name = "任务名称")]
        public string _NodeName { get; set; }
         [Display(Name = "任务描述")]
        public string _Decription { get; set; }
    }
}