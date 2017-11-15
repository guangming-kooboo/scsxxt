using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Qx.Wbs.Hqu.Entity;

namespace ServicePlatform.Areas.WbsHqu.ViewModels
{
    public class QuotaTask_M
    {
        public QuotaTask ToModel()
        {
            return new QuotaTask()
            {
                QuotaTaskID = QuotaTaskID,WbsTaskID = WbsTaskID,FatherID = FatherID,TaskName = TaskName,
                RelativeWeight = RelativeWeight,AbsoluteWeight = AbsoluteWeight,TaskContent = TaskContent,
                BeginTime = Convert.ToDateTime(BeginTime),
                EndTime = Convert.ToDateTime(EndTime),
                CreateTime = CreateTime,TaskSequence = TaskSequence,
                IsLeafNode = Convert.ToInt32(IsLeafNode)
            };
        }
        public static QuotaTask_M ToViewModel(List<QuotaTask> fathers)
        {
            return new QuotaTask_M() { fathers = fathers };
        }

        public static QuotaTask_M ToViewModel(QuotaTask model)
        {
            return new QuotaTask_M()
            {
                QuotaTaskID = model.QuotaTaskID,WbsTaskID = model.WbsTaskID,FatherID = model.FatherID,
                TaskName = model.TaskName,RelativeWeight = model.RelativeWeight*100,AbsoluteWeight = model.AbsoluteWeight,
                TaskContent = model.TaskContent,
                BeginTime = model.BeginTime.ToString("yyyy-MM-dd HH:mm:ss"),
                EndTime = model.EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss") ?? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                CreateTime = model.CreateTime,
                TaskSequence = model.TaskSequence,IsLeafNode = model.IsLeafNode.ToString(),
                IsLeafNodeItem=new List<SelectListItem>()
                {
                    new SelectListItem()
                    {Value = "1",Text = "是"},
                    new SelectListItem()
                    {Value = "0",Text = "否"}
                }
            };
        }

        public static QuotaTask_M ToViewModel(string fatherId,string wbstaskId)
        {
            return new QuotaTask_M() {
                FatherID=fatherId,
                WbsTaskID=wbstaskId,
                BeginTime=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                EndTime=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                IsLeafNodeItem = new List<SelectListItem>()
                {
                    new SelectListItem()
                    {Value = "1",Text = "是"},
                    new SelectListItem()
                    {Value = "0",Text = "否"}
                }
            };
        }


        [StringLength(50)]
        [Display(Name = "定额任务ID")]
        public string QuotaTaskID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "工作量ID")]
        public string WbsTaskID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "父节点ID")]
        public string FatherID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "定额任务名称")]
        public string TaskName { get; set; }

        [Display(Name = "相对占比")]
        public double RelativeWeight { get; set; }

        [Display(Name = "绝对占比")]
        public double? AbsoluteWeight { get; set; }

        [StringLength(500)]
        [Display(Name = "工作内容")]
        public string TaskContent { get; set; }

        [Display(Name = "开始时间")]
        [Column(TypeName = "datetime2")]
        public string BeginTime { get; set; }

        [Column(TypeName = "datetime2")]
        [Display(Name = "结束时间")]
        public string EndTime { get; set; }

        [Column(TypeName = "datetime2")]
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "任务顺序")]
        public int TaskSequence { get; set; }

        [Display(Name = "是否为叶子节点")]
        public string IsLeafNode { get; set; }

        public List<SelectListItem> IsLeafNodeItem;
        public List<QuotaTask> fathers;

    }
}