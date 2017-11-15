using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Qx.Wbs.Hqu.Entity;

namespace ServicePlatform.Areas.WbsHqu.ViewModels
{
    public class QuantifyTask_M
    {
        public QuantifyTask ToModel()
        {
            return new QuantifyTask()
            {
                QuantifyTaskID = QuantifyTaskID,WbsTaskID = WbsTaskID,TaskSequence = TaskSequence,
                TaskName = TaskName,BeginTime = Convert.ToDateTime(BeginTime),EndTime = Convert.ToDateTime(EndTime),RelativeWeight = RelativeWeight,
                AbsoluteWeight = AbsoluteWeight,TaskContent = TaskContent,CreateTime = CreateTime
            };
        }

        public static QuantifyTask_M ToViewModel(QuantifyTask model)
        {
            return new QuantifyTask_M()
            {
                QuantifyTaskID = model.QuantifyTaskID,WbsTaskID = model.WbsTaskID,
                TaskName = model.TaskName, BeginTime = model.BeginTime.ToString("yyyy-MM-dd HH:mm:ss"), EndTime = model.EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss") ?? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                RelativeWeight = model.RelativeWeight,AbsoluteWeight = model.AbsoluteWeight,TaskContent = model.TaskContent,
                CreateTime = model.CreateTime,TaskSequence = model.TaskSequence
            };
        }

        public static QuantifyTask_M ToViewModel(string quantifyId)
        {
            return new QuantifyTask_M()
            {
                WbsTaskID = quantifyId,
                BeginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                RelativeWeight = 1
            };
        }


        [StringLength(50)]
        [Display(Name = "定量任务ID")]
        public string QuantifyTaskID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "工作量ID")]
        public string WbsTaskID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "定量任务名称")]
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

        [Display(Name = "结束时间")]
        [Column(TypeName = "datetime2")]
        public string EndTime { get; set; }

        [Column(TypeName = "datetime2")]
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "任务顺序")]
        public int TaskSequence { get; set; }
    }
}