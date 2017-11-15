using Qx.Wbs.Entity;
using Qx.Wbs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.Wbs.ViewModels.CRUD
{
    public class UserNodeList_Edit_M
    {
        public static UserNodeList_Edit_M ToViewModel(ArangeDetail node)
        {
            var model = new UserNodeList_Edit_M();
            model._NodeName = node.NodeName;
            model._Decription = node.Decription;
            model._UserName = node.UserName + "(" + node.UserID+")";
            model._UserID = node.UserID; 
            model._UserWeight = node.UserWeight;
            model.SerialID = node.SerialID;
            model.RealWeight = node.RealWeight;
            model.Materal = node.Materal;
            return model;
        }
        [Display(Name = "编号")]
        public string SerialID { get; set; }
           [Display(Name = "证明材料")]
        public string Materal { get; set; }

         [Display(Name = "实际完成")]
        public double? RealWeight { get; set; }
         [Display(Name = "完成时间")]
         public DateTime FinshTime { get; set; }

        [Display(Name = "任务名称")]
        public string _NodeName { get; set; }
        [Display(Name = "任务描述")]
        public string _Decription { get; set; }
        [Display(Name = "账号")]
        public string _UserID { get; set; }
        [Display(Name = "预分配")]

        public double? _UserWeight { get; set; }
        [Display(Name = "姓名")]

        public string _UserName { get; set; }
        
    }
  
}