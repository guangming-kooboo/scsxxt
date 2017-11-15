using Qx.Scsxxt.Extentsion.Entity;
using Qx.Scsxxt.Extentsion.Interfaces;
using Qx.Scsxxt.Extentsion.Models;
using Qx.Scsxxt.Extentsion.Repository;
using Qx.Tools.CommonExtendMethods;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qx.Scsxxt.Extentsion.Services
{
    public class V3Services: BaseRepository,IV3Services
    {
        //获取总分[注意传入参数是stuUserId不是stuNo]
        public bool IsInDisperse(string stuUserId)
        {
            //查询学生所属当前批次
            var stuBath = Db.T_StuBatchReg.FirstOrDefault(a => a.UserID == stuUserId&&a.T_PracBatch.IsCurrentBatch=="是");
            if (stuBath == null)
            {
                return false;
            }
            //判断学生申请分散实习的批次号和当前批次号是否一致
            return Db.V3_StuEnterPriseApply.Any(a =>
                a.UserID == stuUserId && a.PracBatchID == stuBath.PracBatchID);
        }
        public string get_PracticeNo(string userId)
        {
            var practiceNo = Db.T_StuBatchReg.Where(a => a.UserID == userId).FirstOrDefault();
            if(practiceNo!=null)
            {
                return practiceNo.PracticeNo;
            }
            else
            {
                return null;
            }
        }
        public bool get_stuscoreapply(string prano)
        {
            var stuscoreapply = Db.T_StuScoreApply.Find(prano);
            if (stuscoreapply!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool AddStuScoreRecord(T_StuScoreApply model)
        {
            try
            {

                #region Creat Record
                var record = new T_StuScoreRecord();
                record.StuScoreStuScoreID = DateTime.Now.Random().ToString();
                record.PraticeNo = model.PracticeNo;
                record.UserID = model.UserID;
                record.Evidence = model.EvidenceFile;
                record.ApplyReviewScore = model.ApplyReviewScore;
                record.ApplyContents = model.ApplyContents;
                record.StateID = model.StateID;
                record.PracticeCaseComment = model.PracticeCaseComment;
                record.WeekRecordScore = model.WeekRecordScore;
                record.PracticeCaseScore = model.PracticeCaseScore;
                record.WeekRecordComment = model.WeekRecordComment;
                record.PracticeContent = model.PracticeContent;
                record.PracticeReport = model.PracticeReport;
                record.PracticeReportFile = model.PracticeReportFile;
                record.PracticeStartEnd = model.PracticeStartEnd;
                record.PracUnitComment = model.PracUnitComment;
                record.SchoolComment = model.SchoolComment;
                record.TutorComment = model.TutorComment;
                record.StuEvaEntScore = model.StuEvaEntScore;
                record.StuEvaSchoolScore = model.StuEvaSchoolScore;
                record.ReviewScore = model.ReviewScore;
                record.AuditOpinion = model.AuditOpinion;
                record.CreateTime = DateTime.Now;
                Db.T_StuScoreRecord.Add(record);
                #endregion
                return Db.Saved();
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool BackFillRecord(string prano,double score)
        {
            try
            {
                var practice = Db.T_StuBatchReg.Find(prano);
                practice.ReviewScore = score;
                return Db.SaveModified(practice);
            }
            catch
            {
                return false;
            }
            
        }

        public bool PassOrNot(T_StuScoreApply model)
        {
            
            #region Creat Record
                var record = new T_StuScoreRecord();
                record.StuScoreStuScoreID = DateTime.Now.Random().ToString();
                record.PraticeNo = model.PracticeNo;
                record.UserID = model.UserID;
                record.Evidence = model.EvidenceFile;
                record.ApplyReviewScore = model.ApplyReviewScore;
                record.ApplyContents = model.ApplyContents;
                record.StateID = model.StateID;
                record.PracticeCaseComment = model.PracticeCaseComment;
                record.WeekRecordScore = model.WeekRecordScore;
                record.PracticeCaseScore = model.PracticeCaseScore;
                record.WeekRecordComment = model.WeekRecordComment;
                record.PracticeContent = model.PracticeContent;
                record.PracticeReport = model.PracticeReport;
                record.PracticeReportFile = model.PracticeReportFile;
                record.PracticeStartEnd = model.PracticeStartEnd;
                record.PracUnitComment = model.PracUnitComment;
                record.SchoolComment = model.SchoolComment;
                record.TutorComment = model.TutorComment;
                record.StuEvaEntScore = model.StuEvaEntScore;
                record.StuEvaSchoolScore = model.StuEvaSchoolScore;
                record.ReviewScore = model.ReviewScore;
                record.AuditOpinion = model.AuditOpinion;
                record.CreateTime = DateTime.Now;
                Db.T_StuScoreRecord.Add(record);
           
            #endregion

            #region BackFillRecord
                var practice = Db.T_StuBatchReg.Find(model.PracticeNo);
                practice.ReviewScore = model.ReviewScore;
                Db.Entry(practice).State=EntityState.Modified;
            #endregion

            #region UpdateScoreApply
            Db.Entry(model).State=EntityState.Modified;
            #endregion

            return Db.Saved();
        }

        public List<Rank> GetEntRank(string EntCategoryID)
        {
            var ranks = Db.C_EntRank.Where(a => a.EntCategoryID == EntCategoryID).Select(b=>new Rank() { RankID=b.EntRankID,RankName=b.EntRankName } ).ToList();
            return ranks;
        }

        public bool BackState(string practiceno)
        {
            var apply = Db.T_StuScoreApply.FirstOrDefault(a => a.PracticeNo == practiceno);
            if (apply != null)
            {
                apply.StateID = "待审核";
                Db.Entry(apply).State = EntityState.Modified;
                return Db.Saved();
            }
            else
            {
                return false;
            }
        }
    }
}
