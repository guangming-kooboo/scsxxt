using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;
using Qx.Wbs.Hqu.Entity;
using Qx.Tools.Web.Mvc.Html;

namespace ServicePlatform.Areas.WbsHqu.ViewModels
{
    public class QuotaTaskStaffDistribution_M
    {
        public QuotaTaskStaffDistribution ToModel()
        {
            return new QuotaTaskStaffDistribution()
            {
                QuotaTaskStaffDistributionID = QuotaTaskStaffDistributionID,
                QuotaTaskID = QuotaTaskID,
                StaffID = StaffID,
                StaffName = StaffName,
                RelativeWeight = RelativeWeight,
                AbsoluteWeight = AbsoluteWeight,
                DistributionExplain = DistributionExplain,
                IsComplete = IsComplete,
                Certificate = Certificate,
                FinishTime = Convert.ToDateTime(FinishTime),
                StaffSequence = StaffSequence
            };
        }
        public static QuotaTaskStaffDistribution_M ToViewModel(string flag, string taskId, string quotataskId)
        {
            return new QuotaTaskStaffDistribution_M() { Flag = flag, TaskID = taskId, QuotaTaskID = quotataskId };
        }
        public static QuotaTaskStaffDistribution_M ToViewModel(string wbstaskId, List<SelectListItem> LeafNodes, List<SelectListItem> StaffSelectItems)
        {
            return new QuotaTaskStaffDistribution_M()
            {
                StaffSelectItems = StaffSelectItems,
                WbsTaskId = wbstaskId,
                FinishTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                LeafNodeListItems = LeafNodes,
                IsCompleteItem = new List<SelectListItem>()
                {
                    new SelectListItem()
                    {
                        Value = "1",Text = "是"
                    },
                    new SelectListItem()
                    {
                        Value = "0",Text = "否"
                    }
                }
            };
        }
        public static QuotaTaskStaffDistribution_M ToViewModel(string quotataskId)
        {
            return new QuotaTaskStaffDistribution_M()
            {
                QuotaTaskID = quotataskId,
                FinishTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                IsCompleteItem = new List<SelectListItem>()
                {
                    new SelectListItem()
                    {
                        Value = "1",Text = "已完成"
                    },
                    new SelectListItem()
                    {
                        Value = "0",Text = "未完成"
                    },
                    new SelectListItem()
                    {
                        Value = "2",Text = "待审核"
                    }
                }
            };
        }

        public static QuotaTaskStaffDistribution_M ToViewModel(QuotaTaskStaffDistribution model)
        {
            return new QuotaTaskStaffDistribution_M()
            {
                QuotaTaskStaffDistributionID = model.QuotaTaskStaffDistributionID,
                QuotaTaskID = model.QuotaTaskID,
                StaffID = model.StaffID,
                RelativeWeight = model.RelativeWeight * 100,
                AbsoluteWeight = model.AbsoluteWeight ?? 0,
                StaffName = model.StaffName,
                DistributionExplain = model.DistributionExplain,
                IsComplete = model.IsComplete,
                Certificate = model.Certificate ?? "",
                FinishTime = model.FinishTime.Value.ToString("yyyy-MM-dd HH:mm:ss") ?? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                StaffSequence = model.StaffSequence,
                IsCompleteItem = new List<SelectListItem>()
                {
                    new SelectListItem()
                    {
                        Value = "1",Text = "已完成"
                    },
                    new SelectListItem()
                    {
                        Value = "0",Text = "未完成"
                    },
                    new SelectListItem()
                    {
                        Value = "2",Text = "待审核"
                    }
                }
            };
        }

        [StringLength(50)]
        [Display(Name = "分配人员ID")]
        public string QuotaTaskStaffDistributionID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "定额任务ID")]
        public string QuotaTaskID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "员工ID")]
        public string StaffID { get; set; }

        [Display(Name = "相对占比")]
        public double RelativeWeight { get; set; }

        [Display(Name = "绝对占比")]
        public double AbsoluteWeight { get; set; }


        [StringLength(50)]
        [Display(Name = "员工名字")]
        public string StaffName { get; set; }

        [StringLength(250)]
        [Display(Name = "分配说明")]
        public string DistributionExplain { get; set; }

        [Display(Name = "是否已完成")]
        public int IsComplete { get; set; }

        [Display(Name = "完成时间")]
        [Column(TypeName = "datetime2")]
        public string FinishTime { get; set; }

        [Display(Name = "证明材料")]
        [Column(TypeName = "datetime2")]
        public string Certificate { get; set; }

        [Display(Name = "顺序")]
        public int StaffSequence { get; set; }

        public List<SelectListItem> IsCompleteItem;
        public string TaskID { get; set; }
        public string Flag { get; set; }
        public string WbsTaskId { get; set; }

        public List<SelectListItem> LeafNodeListItems;

        public List<SelectListItem> StaffSelectItems;
    }
}