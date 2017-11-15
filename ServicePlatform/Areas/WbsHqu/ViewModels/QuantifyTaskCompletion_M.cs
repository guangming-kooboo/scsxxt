using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Qx.Tools.CommonExtendMethods;
using Qx.Wbs.Hqu.Entity;

namespace ServicePlatform.Areas.WbsHqu.ViewModels
{
    public class QuantifyTaskCompletion_M
    {
        public QuantifyTaskCompletion ToModel()
        {
            return new QuantifyTaskCompletion()
            {
                QuantifyTaskID = QuantifyTaskID ,QuantifyTaskCompletionID = QuantifyTaskCompletionID,
                ScoringRuleID = ScoringRuleID,StaffID = StaffID,StaffName = StaffName,FinishTime =Convert.ToDateTime(FinishTime),
                CompleteNote = CompleteNote,Certificate = Certificate
            };
        }
        public static QuantifyTaskCompletion_M ToViewModel(string wbstaskId, string flag)
        {
            return new QuantifyTaskCompletion_M() { WbsTaskID = wbstaskId, flag = flag };
        }
        public static QuantifyTaskCompletion_M ToViewModel(string taskid ,List<SelectListItem> ScoringRuleItem )
        {
            //添加人员
            return new QuantifyTaskCompletion_M()
            {
                BackParams = taskid,
                QuantifyTaskID = taskid.UnPackString(';')[0],
                ScoringRuleItem = ScoringRuleItem,
                FinishTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
        }
        public static QuantifyTaskCompletion_M ToViewModel(string backparams)
        {
            return new QuantifyTaskCompletion_M() {BackParams=backparams };
        }

        public static QuantifyTaskCompletion_M ToViewModel(string taskid,string staffid,string staffname, List<SelectListItem> ScoringRuleItem)
        {
            return  new QuantifyTaskCompletion_M() { BackParams= taskid, QuantifyTaskID = taskid.UnPackString(';')[0],StaffID = staffid, StaffName=staffname,ScoringRuleItem = ScoringRuleItem,FinishTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        }

        [StringLength(50)]
        [Display(Name = "完成ID")]
        public string QuantifyTaskCompletionID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "定量任务ID")]
        public string QuantifyTaskID { get; set; }
        
        [StringLength(50)]
        [Display(Name = "计分标准")]
        public string ScoringRuleID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "人员ID")]
        public string StaffID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "人员名称")]
        public string StaffName { get; set; }

        [Column(TypeName = "datetime2")]
        [Display(Name = "完成时间")]
        public string FinishTime { get; set; }

        [StringLength(500)]
        [Display(Name = "备注")]
        public string CompleteNote { get; set; }

        [StringLength(50)]
        [Display(Name = "证明材料")]
        public string Certificate { get; set; }

        public string BackParams { get; set; }

        public string WbsTaskID { get; set; }

        public string flag { get; set; }

        public List<SelectListItem> ScoringRuleItem;
    }
}