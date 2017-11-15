using Qx.Scsxxt.Extentsion.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.WbsHqu.ViewModels
{
    public class StuScoreApply_M
    {
        public T_StuScoreApply ToModel()
        {
            return new T_StuScoreApply() { PracticeNo = PracticeNo,
                UserID = UserID,
                EvidenceFile = EvidenceFile,
                ApplyReviewScore = ApplyReviewScore,
                ApplyContents = ApplyContents,
                StateID = StateID,
                PracticeCaseComment = PracticeCaseComment,
                WeekRecordScore = WeekRecordScore,
                PracticeCaseScore = PracticeCaseScore,
                WeekRecordComment = WeekRecordComment,
                PracticeContent = PracticeContent,
                PracticeReport = PracticeReport,
                PracticeReportFile = PracticeReportFile,
                PracticeStartEnd = PracticeStartEnd,
                PracUnitComment = PracUnitComment,
                SchoolComment = SchoolComment,
                TutorComment = TutorComment,
                StuEvaEntScore = StuEvaEntScore,
                StuEvaSchoolScore= StuEvaSchoolScore,
                ReviewScore= ReviewScore,
                AuditOpinion= AuditOpinion
            };
        }
        public static StuScoreApply_M ToViewModel(string prano, string userId)
        {
            //创建
            return new StuScoreApply_M() {PracticeNo=prano,UserID=userId };
        }
        public static StuScoreApply_M ToViewModel(T_StuScoreApply model)
        {
            //编辑
            return new StuScoreApply_M() {
                PracticeNo = model.PracticeNo,
                UserID = model.UserID,
                EvidenceFile = model.EvidenceFile,
                ApplyReviewScore = model.ApplyReviewScore,
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
                ReviewScore=model.ReviewScore,
                AuditOpinion =model.AuditOpinion
            };
        }

        [Key]
        [StringLength(50)]
        [Display(Name ="实习号")]
        public string PracticeNo { get; set; }

        [StringLength(50)]
        [Display(Name = "用户ID")]
        public string UserID { get; set; }


        [Display(Name = "证明材料")]
        public string EvidenceFile { get; set; }

        [Display(Name = "申请实习分数")]
        public double? ApplyReviewScore { get; set; }

        [Display(Name = "申请理由")]
        public string ApplyContents { get; set; }

        [StringLength(50)]
        [Display(Name = "状态")]
        public string StateID { get; set; }


        [Display(Name = "实习案例评语")]
        public string PracticeCaseComment { get; set; }

        [Display(Name = "实习周记成绩")]
        public double? WeekRecordScore { get; set; }

        [Display(Name = "实习案例成绩")]
        public double? PracticeCaseScore { get; set; }

        [Display(Name = "实习周记评语")]
        public string WeekRecordComment { get; set; }

        [Display(Name = "实习内容")]
        public string PracticeContent { get; set; }

 
        [Display(Name = "实习报告")]
        public string PracticeReport { get; set; }


        [Display(Name = "实习报告文件附件的URL")]
        public string PracticeReportFile { get; set; }

        [StringLength(50)]
        [Display(Name = "实习时间")]
        public string PracticeStartEnd { get; set; }

        [Display(Name = "实习单位评语")]
        public string PracUnitComment { get; set; }


        [Display(Name = "学校评语")]
        public string SchoolComment { get; set; }

    
        [Display(Name = "导师评语")]
        public string TutorComment { get; set; }

        [Display(Name = "学生对企业的评分")]
        public double? StuEvaEntScore { get; set; }

        [Display(Name = "学生对学校的评分")]
        public double? StuEvaSchoolScore { get; set; }

        [Display(Name = "最终成绩")]
        //[Required(ErrorMessage = "请填写最终成绩")]
        public double? ReviewScore { get; set; }

    
        [Display(Name = "审核意见")]
        //[Required(ErrorMessage = "请填写审核意见")]
        public string AuditOpinion { get; set; }

        public string type { get; set; }

    }
}