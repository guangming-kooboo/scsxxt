using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Core.Services;
using Core.Services.Entity;
using Qx.Tools;
using Qx.Scsxxt.Extentsion.Services;
namespace Qx.Scsxxt.Core.Services.Utility
{
    public class ScoreServices : BaseServices
    {
        private IDML<T_StuBatchReg> StuBatchRegServices;
        private Repository Db;
        private V3Services _v3Services;
        public ScoreServices()
        {
            StuBatchRegServices = ServicesFactory.GetSevers<T_StuBatchReg>();
            Db=new Repository();
            _v3Services=new V3Services();
        }


       

        //获取总分[注意传入参数是stuUserId不是stuNo]
        public double GetReviewScoreByStuNo(string stuUserId)
        {
            var s = Db.T_StuBatchReg.FirstOrDefault(b => b.UserID == stuUserId);
            if(s!=null)
            {
                return (double)s.ReviewScore;
            }
            else
            {
                return -1;
            }
        }

        //计算总分[注意传入参数是stuUserId不是stuNo]
        public double CaculateReviewScoreByStuNo(string stuUserId)
        {
            var schoolScoreData = CaculateSchoolScoreBystuUserId(stuUserId);
            var entScoreData = CaculateEntScoreBystuUserId(stuUserId);
            if (schoolScoreData.Any(a=>a.SchoolItemScore<0))
            {
                return -1;//评分项目未填写完整
            }

            var schoolSum = schoolScoreData.Sum(item => (item.SchoolItemWeight / 100.0) * item.SchoolItemScore);
            var entSum = entScoreData.Sum(item => (item.EntItemWeight / 100.0) * item.EntItemScore);
            double schoolWeight;
            double entWeight;
            //取出旧数据
            var studentBatch = Db.T_StuBatchReg.FirstOrDefault(b => b.UserID == stuUserId);
            if (studentBatch != null)
            {
                //判断学生是否属于分散实习
                if (_v3Services.IsInDisperse(stuUserId))
                {
                    schoolWeight = 100;
                    entWeight = 0;
                }
                else
                {
                    schoolWeight = schoolScoreData[0].SchoolWeight;
                    entWeight = entScoreData[0].EntWeight;
                }

                //更新分数
                studentBatch.ReviewScore = schoolSum* (schoolWeight/100.0d) + entSum* (entWeight / 100.0d);
                Db.T_StuBatchReg.AddOrUpdate(studentBatch);
                Db.SaveChanges();
                return studentBatch.ReviewScore.Value;
            }
            else
            {
                return -2;//传入学号不合法
            }   
        }

        #region 算法

        //计算学校端分数
        private List<SchoolScoreModel> CaculateSchoolScoreBystuUserId(string stuUserId)
        {
            var result = Db.T_SchoolStudentReviewLink.Where(a => a.T_StuBatchReg.UserID == stuUserId &&
                                                                 a.T_SchoolReviewerTask.T_SchoolReviewItem.T_PracBatch .IsCurrentBatch == "是").
                Select(b => new SchoolScoreModel()
                {
                    StuUserId = b.T_StuBatchReg.UserID,
                    StuNo = b.T_StuBatchReg.T_Student.StuNo,
                    BatchName = b.T_SchoolReviewerTask.T_SchoolReviewItem.T_PracBatch.BatchName,
                    PracBatchId = b.T_SchoolReviewerTask.T_SchoolReviewItem.T_PracBatch.PracBatchID,
                    SchoolWeight = b.T_SchoolReviewerTask.T_SchoolReviewItem.T_PracBatch.SchoolReviewWeight.Value,
                    SchoolItemId = b.T_SchoolReviewerTask.ItemID,
                    SchoolItemWeight = b.T_SchoolReviewerTask.T_SchoolReviewItem.ItemWeight,
                    SchoolItemScore = b.ReviewScore.HasValue? b.ReviewScore.Value:-1
                }).ToList();
            return result;


            ////评分项未填写完整！
            //if (result.Count <= 0) return -1;

            //var schoolWeight = result[0].SchoolWeight;
            //var sum = result.Sum(item => (item.SchoolItemWeight / 100.0) * item.SchoolItemScore);
            //return (sum * (schoolWeight / 100.0));
        }
        //计算企业端分数
        private List<EntScoreModel> CaculateEntScoreBystuUserId(string stuUserId)
        {
            var result = Db.T_EntStudentReviewLink.Where(a => a.T_StuBatchReg.UserID == stuUserId &&
                                                                a.T_EntReviewerTask.T_EntReviewItem.T_EntBatchReg.T_PracBatch.IsCurrentBatch == "是").
               Select(b => new EntScoreModel()
               {
                   StuUserId = b.T_StuBatchReg.UserID,
                   StuNo = b.T_StuBatchReg.T_Student.StuNo,
                   BatchName = b.T_EntReviewerTask.T_EntReviewItem.T_EntBatchReg.T_PracBatch.BatchName,
                   EntPracBatchId = b.T_EntReviewerTask.T_EntReviewItem.T_EntBatchReg.T_PracBatch.PracBatchID,
                   EntWeight = 100 - b.T_EntReviewerTask.T_EntReviewItem.T_EntBatchReg.T_PracBatch.SchoolReviewWeight.Value,
                   EntItemId = b.T_EntReviewerTask.ItemID,
                   EntItemWeight = b.T_EntReviewerTask.T_EntReviewItem.ItemWeight.Value,
                   EntItemScore = b.ReviewScore.HasValue ? b.ReviewScore.Value : -1
               }).ToList();
            return result;
            ////评分项未填写完整！
            //if (result.Count <= 0) return -1;

            //var entWeight = result[0].EntWeight;
            //var sum = result.Sum(item => (item.EntItemWeight / 100.0) * item.EntItemScore);
            //return (sum * (entWeight / 100.0));
        }   
        #endregion

        #region 模型
        private class SchoolScoreModel
        {
            //学生userid
            public string StuUserId { get; set; }
            //学号
            public string StuNo { get; set; }
            //总批次
            public string BatchName { get; set; }
            //（学校）批次
            public string PracBatchId { get; set; }
            //（学校）评分所占比重
            public int SchoolWeight { get; set; }
            //（学校）成绩详情
     
            //（学校）批次下的评分项
            public string SchoolItemId { get; set; }
            //（学校）评分项所占权重
            public int SchoolItemWeight { get; set; }
            //（学校）评分项所得分数
            public double SchoolItemScore { get; set; }
        }
   

        private class EntScoreModel
        {
            //学生userid
            public string StuUserId { get; set; }
            //学号
            public string StuNo { get; set; }
            //总批次
            public string BatchName { get; set; }
            //（企业）批次
            public string EntPracBatchId { get; set; }
            //（企业）评分所占比重
            public int EntWeight { get; set; }
            //（企业）批次下的评分项
            public string EntItemId { get; set; }
            //（企业）评分项所占权重
            public int EntItemWeight { get; set; }
            //（企业）评分项所得分数
            public double EntItemScore { get; set; }
        }

#endregion

    }
}