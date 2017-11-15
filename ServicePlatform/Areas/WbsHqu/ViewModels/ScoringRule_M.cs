using System.ComponentModel.DataAnnotations;
using Qx.Wbs.Hqu.Entity;

namespace ServicePlatform.Areas.WbsHqu.ViewModels
{
    public class ScoringRule_M
    {
        public ScoringRule ToModel()
        {
            return new ScoringRule()
            {
                ScoringRuleID = ScoringRuleID,QuantifyTaskID = QuantifyTaskID,FormName = FormName,
                Score = Score,Note = Note
            };
        }

        public static ScoringRule_M ToViewModel(ScoringRule model, string param)
        {
            return new ScoringRule_M()
            {
                ScoringRuleID = model.ScoringRuleID,QuantifyTaskID = model.QuantifyTaskID,
                FormName = model.FormName,Score = model.Score,Note = model.Note,
                param = param
            };
        }
        public static ScoringRule_M ToViewModel(string wbstaskId, string extraparams)
        {
            return new ScoringRule_M() { WbsTaskID = wbstaskId };
        }
        public static ScoringRule_M ToViewModel(string quantifyId)
        {
            return new ScoringRule_M() { QuantifyTaskID= quantifyId };
        }

        [StringLength(50)]
        [Display(Name = "计分标准ID")]
        public string ScoringRuleID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "定量任务ID")]
        public string QuantifyTaskID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "计分标准名称")]
        public string FormName { get; set; }

        [Display(Name = "分数")]
        public double Score { get; set; }

        [StringLength(250)]
        [Display(Name = "备注")]
        public string Note { get; set; }
        public string WbsTaskID { get; set; }

        public string param { get; set; }
    }
}