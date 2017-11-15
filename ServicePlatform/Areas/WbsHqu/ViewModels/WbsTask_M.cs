using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Qx.Wbs.Hqu.Entity;

namespace ServicePlatform.Areas.WbsHqu.ViewModels
{
    public class WbsTask_M
    {
        public WbsTask ToModel()
        {
            return new WbsTask()
            {
                WbsTaskID = WbsTaskID,TaskName = TaskName,QuotaTaskWeight =QuotaTaskWeight,
                QuantifyTaskWeight = QuantifyTaskWeight,MotorTaskWeight = MotorTaskWeight,
                BeginTime =Convert.ToDateTime(BeginTime),EndTime =Convert.ToDateTime( EndTime),OwnerID = OwnerID,CreateTime =CreateTime
            };
        }

        public static WbsTask_M ToViewModel(string ownerid)
        {
            return new WbsTask_M() {BeginTime=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),EndTime=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), OwnerID=ownerid};
        }

        public static WbsTask_M ToViewModel(WbsTask model)
        {
            return new WbsTask_M()
            {
                WbsTaskID = model.WbsTaskID,TaskName = model.TaskName,
                QuotaTaskWeight = model.QuotaTaskWeight * 100,
                QuantifyTaskWeight = model.QuantifyTaskWeight * 100,
                MotorTaskWeight = model.MotorTaskWeight * 100,
                BeginTime = model.BeginTime.ToString("yyyy-MM-dd HH:mm:ss"),EndTime =  model.EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss")??DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                OwnerID = model.OwnerID,CreateTime = model.CreateTime
            };
        }
        
        [StringLength(50)]
        public string WbsTaskID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "工作量名称")]
        public string TaskName { get; set; }

        [Display(Name = "定额占比")]
        public double QuotaTaskWeight { get; set; }

        [Display(Name = "定量占比")]
        public double QuantifyTaskWeight { get; set; }

        [Display(Name = "机动占比")]
        public double MotorTaskWeight { get; set; }

        [Display(Name = "开始时间")]
        [Column(TypeName = "datetime2")]
        public string BeginTime { get; set; }

        [Display(Name = "结束时间")]
        [Column(TypeName = "datetime2")]
        public string EndTime { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "拥有者ID")]
        public string OwnerID { get; set; }

        [Column(TypeName = "datetime2")]
        [Display(Name = "创建时间")]
        public DateTime  CreateTime { get; set; }
        
    }
}