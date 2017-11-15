using Qx.Wbs.Hqu.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.WbsHqu.ViewModels
{
    public class CheckRecord_M
    {
        public CheckRecord ToModel()
        {
            return new CheckRecord() {FinishID=FinishID, Auditor=Auditor, CheckOpinion=CheckOpinion,
             CreateTime=CreateTime, OwerID=OwerID, State=State, TaskName=TaskName, TaskType=TaskType, UserID=UserID};
        }

        public static CheckRecord_M ToViewModel(CheckRecord model)
        {
            return new ViewModels.CheckRecord_M() {FinishID=model.FinishID,
            TaskName=model.TaskName, UserID=model.UserID,OwerID=model.OwerID,
            CreateTime=model.CreateTime,State=model.State, TaskType=model.TaskType
             };
        }

        [Key]
        [StringLength(50)]
        public string FinishID { get; set; }

        [StringLength(50)]
        public string TaskName { get; set; }

        public int? TaskType { get; set; }

        public int? State { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreateTime { get; set; }

        [StringLength(50)]
        public string Auditor { get; set; }

        [StringLength(50)]
        public string CheckOpinion { get; set; }

        [StringLength(50)]
        public string UserID { get; set; }

        [StringLength(50)]
        public string OwerID { get; set; }

    }
}