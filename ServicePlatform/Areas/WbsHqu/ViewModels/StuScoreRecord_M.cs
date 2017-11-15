using Qx.Scsxxt.Extentsion.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.WbsHqu.ViewModels
{
    public class StuScoreRecord_M
    {
        public static StuScoreRecord_M ToViewModel(T_StuScoreRecord model)
        {
            //编辑
            return new StuScoreRecord_M()
            {
                StuScoreStuScoreID = model.StuScoreStuScoreID,
                PraticeNo = model.PraticeNo,
                UserID = model.UserID,
                Evidence = model.Evidence,
                ApplyReviewScore = model.ApplyReviewScore.Value ,
                ApplyContents = model.ApplyContents,
                StateID = model.StateID,
                PracticeCaseComment = model.PracticeCaseComment,
                WeekRecordScore = model.WeekRecordScore,
                PracticeCaseScore = model.PracticeCaseScore,
                WeekRecordComment = model.WeekRecordComment,
                PracticeContent = model.PracticeContent,
                PracticeReport = model.PracticeReport,
                PracticeReportFile = model.PracticeReportFile,
                PracticeStartEnd = model.PracticeStartEnd,
                PracUnitComment = model.PracUnitComment,
                SchoolComment = model.SchoolComment,
                TutorComment = model.TutorComment,
                StuEvaEntScore = model.StuEvaEntScore,
                StuEvaSchoolScore = model.StuEvaSchoolScore,
                ReviewScore = model.ReviewScore,
                AuditOpinion = model.AuditOpinion,
                CreateTime = model.CreateTime
            };
        }

        [Key]
        [StringLength(50)]
        public string StuScoreStuScoreID { get; set; }

        [StringLength(50)]
        [Display(Name = "实习号")]
        public string PraticeNo { get; set; }

        [StringLength(50)]
        [Display(Name = "用户ID")]
        public string UserID { get; set; }

        [StringLength(500)]
        [Display(Name = "证明材料")]
        public string Evidence { get; set; }

        [Display(Name = "申请实习分数")]
        public double ApplyReviewScore { get; set; }

        [StringLength(500)]
        [Display(Name = "申请理由")]
        public string ApplyContents { get; set; }

        [StringLength(50)]
        [Display(Name = "状态")]
        public string StateID { get; set; }

        [StringLength(1024)]
        [Display(Name = "实习案例评语")]
        public string PracticeCaseComment { get; set; }

        [Display(Name = "实习周记成绩")]
        public double? WeekRecordScore { get; set; }

        [Display(Name = "实习案例成绩")]
        public double? PracticeCaseScore { get; set; }

        [StringLength(1024)]
        [Display(Name = "实习周记评语")]
        public string WeekRecordComment { get; set; }

        [Column(TypeName = "text")]
        [Display(Name = "实习内容")]
        public string PracticeContent { get; set; }

        [Column(TypeName = "text")]
        [Display(Name = "实习报告")]
        public string PracticeReport { get; set; }

        [StringLength(200)]
        [Display(Name = "实习报告文件附件的URL")]
        public string PracticeReportFile { get; set; }

        [StringLength(50)]
        [Display(Name = "实习时间")]
        public string PracticeStartEnd { get; set; }

        [StringLength(1024)]
        [Display(Name = "实习单位评语")]
        public string PracUnitComment { get; set; }

        [StringLength(1024)]
        [Display(Name = "学校评语")]
        public string SchoolComment { get; set; }

        [StringLength(1024)]
        [Display(Name = "导师评语")]
        public string TutorComment { get; set; }

        [Display(Name = "学生对企业的评分")]
        public double? StuEvaEntScore { get; set; }

        [Display(Name = "学生对学校的评分")]
        public double? StuEvaSchoolScore { get; set; }

        [Display(Name = "最终成绩")]
        public double? ReviewScore { get; set; }

        [StringLength(500)]
        [Display(Name = "审核意见")]
        public string AuditOpinion { get; set; }
        [Column(TypeName = "datetime2")]
        [Display(Name = "审核时间")]
        public DateTime? CreateTime { get; set; }
    }
}