using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Qx.Tools;
using Qx.Tools.CommonExtendMethods;
using Qx.Wbs.Hqu.Entity;
using Qx.Wbs.Hqu.Interfaces;
using Qx.Wbs.Hqu.Models;

namespace Qx.Wbs.Hqu.Services
{
    public class WbsService : BaseRepository, IWbsService
    {
        public class Task
        {
            public string taskId;
        }
        public double AbsoluteWeightSub(string QuotaTaskFatherID, double RelativeWeight)
        {
            var father = Db.QuotaTasks.Find(QuotaTaskFatherID);
            double AbsoluteWeight = RelativeWeight * father.AbsoluteWeight.Value;
            return AbsoluteWeight;
        }
        public double AbsoluteWeight(string WbsTaskID, double RelativeWeight)
        {
            double AbsoluteWeight = RelativeWeight * Db.WbsTasks.Find(WbsTaskID).QuotaTaskWeight;
            return AbsoluteWeight;
        }

        public bool UpdateStaffDistribut(string quotataskId)
        {
            //更新人员分配的绝对占比
            var quota = Db.QuotaTasks.Find(quotataskId);
            var query = new List<QuotaTaskStaffDistribution>();
            query = Db.QuotaTaskStaffDistributions.Where(a => a.QuotaTaskID == quotataskId).ToList();
            for (int i = 0; i < query.Count; i++)
            {
                query[i].AbsoluteWeight = query[i].RelativeWeight * quota.AbsoluteWeight;
                Db.QuotaTaskStaffDistributions.AddOrUpdate(query[i]);
            }
            return Db.Saved();
        }
        public bool UpdateSubAbsoluteWeight(string quotataskId)
        {
            //更新子节点的绝对占比
            UpdateStaffDistribut(quotataskId);
            var query = new List<QuotaTask>();
            query = Db.QuotaTasks.Where(a => a.FatherID == quotataskId).ToList();
            while (query.Any())
            {
                var queryid = query[0].QuotaTaskID;
                var subquery = Db.QuotaTasks.Where(c => c.FatherID == queryid).ToList();
                if (subquery != null)
                {
                    query.AddRange(subquery);
                }
                var fatherabsolute = Db.QuotaTasks.Find(query[0].FatherID).AbsoluteWeight;
                query[0].AbsoluteWeight = fatherabsolute * query[0].RelativeWeight;
                Db.QuotaTasks.AddOrUpdate(query[0]);
                UpdateStaffDistribut(query[0].QuotaTaskID);
                query.Remove(query[0]);
            }
            return Db.Saved();
        }
        public bool UpdateAbsoluteWeight(string wbstaskId)
        {
            //更新所有子节点的绝对占比
            var wbstask = Db.WbsTasks.Where(a => a.WbsTaskID == wbstaskId).FirstOrDefault();
            var quotatask = Db.QuotaTasks.Where(b => b.WbsTaskID == wbstask.WbsTaskID && b.FatherID == "0").ToList();
            for (int i = 0; i < quotatask.Count; i++)
            {
                quotatask[i].AbsoluteWeight = quotatask[i].RelativeWeight * wbstask.QuotaTaskWeight;
                Db.QuotaTasks.AddOrUpdate(quotatask[i]);
                UpdateSubAbsoluteWeight(quotatask[i].QuotaTaskID);

            }
            var quantifytask = Db.QuantifyTasks.Where(c => c.WbsTaskID == wbstask.WbsTaskID).ToList();
            for (int i = 0; i < quantifytask.Count; i++)
            {
                quantifytask[i].AbsoluteWeight = quantifytask[i].RelativeWeight * wbstask.QuantifyTaskWeight;
                Db.QuantifyTasks.AddOrUpdate(quantifytask[i]);
            }
            return Db.Saved();
        }
        public bool QuotaTaskWeight(QuotaTask quotatask)
        {
            //判断相对占比相加是否超过1
            double weight = quotatask.RelativeWeight;
            var fatherId = quotatask.FatherID;
            if (fatherId != "0")
            {
                var quotataskList =
                    Db.QuotaTasks.Where(a => a.FatherID == fatherId && a.QuotaTaskID != quotatask.QuotaTaskID)
                        .ToList();
                foreach (var item in quotataskList)
                {
                    weight += item.RelativeWeight;
                }
            }
            else
            {
                var quotataskList =
                    Db.QuotaTasks.Where(
                        a =>
                            a.FatherID == "0" && a.WbsTaskID == quotatask.WbsTaskID &&
                            a.QuotaTaskID != quotatask.QuotaTaskID).ToList();
                foreach (var item in quotataskList)
                {
                    weight += item.RelativeWeight;
                }
            }

            if (weight > 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool QuantifyTaskWeight(QuantifyTask quantifytask)
        {
            //判断顶级相对占比相加是否超过1
            double weight = quantifytask.RelativeWeight;
            var quantaskList =
                Db.QuantifyTasks.Where(
                    a => a.WbsTaskID == quantifytask.WbsTaskID && a.QuantifyTaskID != quantifytask.QuantifyTaskID)
                    .ToList();
            foreach (var item in quantaskList)
            {
                weight += item.RelativeWeight;
            }

            //var quantifytask = Db.QuantifyTasks.Where(a => a.WbsTaskID == wbstaskId).ToList();
            //foreach (var item in quantifytask)
            //{
            //    weight += item.RelativeWeight;
            //}

            if (weight > 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //检查人员占比是否超过1
        public bool StaffWeight(QuotaTaskStaffDistribution staffdis)
        {
            double weight = staffdis.RelativeWeight;
            var staff =
                Db.QuotaTaskStaffDistributions.Where(
                    a =>
                        a.QuotaTaskID == staffdis.QuotaTaskID &&
                        a.QuotaTaskStaffDistributionID != staffdis.QuotaTaskStaffDistributionID).ToList();
            foreach (var item in staff)
            {
                weight += item.RelativeWeight;
            }
            if (weight > 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //找兄弟节点
        public string quotaBrother(QuotaTask quota)
        {
            string quotask;
            if (quota.FatherID == "0")
            {
                quotask = Db.QuotaTasks.Where(a => a.WbsTaskID == quota.WbsTaskID && a.FatherID == "0" && a.TaskSequence == quota.TaskSequence).Select(b => b.QuotaTaskID).FirstOrDefault();
            }
            else
            {
                quotask = Db.QuotaTasks.Where(c => c.FatherID == quota.FatherID && c.TaskSequence == quota.TaskSequence).Select(d => d.QuotaTaskID).FirstOrDefault();
            }
            return quotask;
        }

        public string quantifyBrother(QuantifyTask quantify)
        {
            string quantifybrother;
            quantifybrother = Db.QuantifyTasks.Where(a => a.WbsTaskID == quantify.WbsTaskID && a.TaskSequence == quantify.TaskSequence).Select(b => b.QuantifyTaskID).FirstOrDefault();
            return quantifybrother;
        }

        public string DistributionBrother(QuotaTaskStaffDistribution quodis)
        {
            string quodisbrother;
            quodisbrother =
                Db.QuotaTaskStaffDistributions.Where(
                    a => a.QuotaTaskID == quodis.QuotaTaskID && a.StaffSequence == quodis.StaffSequence)
                    .Select(b => b.QuotaTaskStaffDistributionID)
                    .FirstOrDefault();
            return quodisbrother;
        }

        public List<QuotaTask> FindFather(string quotaId)
        {
            var father = Db.QuotaTasks.FirstOrDefault(a => a.QuotaTaskID == quotaId);
            List<QuotaTask> fathers = new List<QuotaTask>();
            while (father != null)
            {
                fathers.Add(father);
                if (father.FatherID == "0")
                    break;
                father = Db.QuotaTasks.Find(father.FatherID);
            }
            fathers.Reverse();
            return fathers;
        }

        public string FindFatherQuotaTask(string quotaskId)
        {
            string returnid;
            var fatherId = Db.QuotaTasks.Find(quotaskId);
            if (fatherId.FatherID == "0")
            {
                returnid = "0;" + fatherId.WbsTaskID;
            }
            else
            {
                returnid = "1;" + fatherId.FatherID;
            }
            return returnid;
        }

        public List<SelectListItem> FindLeafNode(string wbstaskId)
        {
            var data =
                Db.QuotaTasks.Where(a => a.WbsTaskID == wbstaskId && a.IsLeafNode == 1).Select(b => new SelectListItem()
                {
                    Value = b.QuotaTaskID,
                    Text = b.TaskName
                }).ToList();
            return data.Format(wbstaskId);
        }

        public List<List<string>> CheckRecord(string userId, bool type)
        {
            //审核记录的数据:type真为用户;type假为管理员
            var data = new List<List<string>>();
            if (type)
            {
                data = Db.CheckRecords.Where(a => a.UserID == userId).Select(b => new List<string>() {
                    b.FinishID,
                    b.TaskName,
                    b.State==0?"待审核":(b.State==1?"审核通过":"审核未通过"),
                    b.CreateTime.ToString(),
                    b.Auditor,
                    b.CheckOpinion
                }).ToList();
            }
            else
            {
                data = Db.CheckRecords.Where(a => a.OwerID == userId).Select(b => new List<string>() {
                    b.FinishID,
                    b.TaskName,
                    b.State==0?"待审核":(b.State==1?"审核通过":"审核未通过"),
                    b.CreateTime.ToString(),
                    b.Auditor,
                    b.CheckOpinion
                }).ToList();
            }
            return data;
          
        }
        
        public bool CheckRecord(CheckRecord model, bool type)
        {
            //add or update CheckRecord
            try
            {
                if (type)
                {
                    //Add
                    Db.CheckRecords.Add(model);
                }
                else
                {
                    //Update
                    Db.SaveModified(model);
                }
                return Db.Saved();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool CreateCheckRecord(string taskdisId, string taskname, string wbsId, int type, string userId)
        {
            var checkrecord = new CheckRecord();
            checkrecord.FinishID = taskdisId;
            checkrecord.TaskName = taskname;
            checkrecord.TaskType = type;//0:定额;1定量
            checkrecord.State = 0;//待审核
            checkrecord.CreateTime = DateTime.Now;
            checkrecord.UserID = userId;
            checkrecord.OwerID = Db.WbsTasks.Find(wbsId).OwnerID;
            checkrecord.WbsTaskID = wbsId;
            return CheckRecord(checkrecord,true);
        }
        public List<WbsTask> GetTasks(string userunit)
        {
            return Db.WbsTasks.Where(a => a.OwnerID.Contains(userunit)).ToList();
        }
        public List<Models.Task> GetMyTask(string type,string wbsid,string userid)
        {
            List<Models.Task> body = new List<Models.Task>();
            if (type == "0")
            {
                var model = Db.QuotaTaskStaffDistributions.Where(a => a.StaffID == userid && a.QuotaTask.WbsTaskID == wbsid && (a.IsComplete ==0)).ToList();
                foreach (var item in model)
                {
                    body.Add(new Models.Task()
                    {
                        Id = item.QuotaTaskID,
                        Name = item.QuotaTask.TaskName
                    });
                }
                
            }
            
                return body;
        }
        public QuantifyTask GetQuantifyTask(string wbstaskId)
        {
            //用wbsId获取quantifytaskId
            var quantify = Db.QuantifyTasks.FirstOrDefault(a => a.WbsTaskID == wbstaskId);
            if (quantify != null)
            {
                return quantify;
            }
            else
            {
                return null;
            }
        }
        public QuotaTaskStaffDistribution GetQuotadis(string quotataskId)
        {
            var quotatask = Db.QuotaTaskStaffDistributions.FirstOrDefault(a => a.QuotaTaskID == quotataskId);
            return quotatask;
        }
        public bool Record(string wbstaskid, string tasktype, string mytaskid, string note, string certificate,string userId)
        {
            if (tasktype == "0")//定额
            {
                var mytask = Db.QuotaTaskStaffDistributions.Where(a => a.QuotaTaskID == mytaskid).FirstOrDefault();
                mytask.Certificate = certificate;
                mytask.IsComplete = 2;
                Db.SaveModified(mytask);
                return CreateCheckRecord(mytask.QuotaTaskStaffDistributionID, mytask.QuotaTask.TaskName, mytask.QuotaTask.WbsTaskID, 0, userId);
                 
            }
            else
            {
                QuantifyTaskCompletion quantifytaskcom = new QuantifyTaskCompletion();
                quantifytaskcom.QuantifyTaskCompletionID = DateTime.Now.Random().ToString();
                quantifytaskcom.QuantifyTaskID =
                    Db.QuantifyTasks.Where(a => a.WbsTaskID == wbstaskid).FirstOrDefault().QuantifyTaskID;
                quantifytaskcom.ScoringRuleID = mytaskid;
                quantifytaskcom.StaffID = userId;
                quantifytaskcom.StaffName = userId;
                quantifytaskcom.FinishTime = DateTime.Now;
                quantifytaskcom.CompleteNote = note;
                quantifytaskcom.Certificate = certificate;
                Db.QuantifyTaskCompletions.Add(quantifytaskcom);
                var formname = Db.ScoringRules.Find(mytaskid).FormName;
                return CreateCheckRecord(quantifytaskcom.QuantifyTaskCompletionID, formname, wbstaskid, 1, userId);
            }
        }
        public double AppStaffSumary(string wbsId,string userId,string type)//type：2全部;type:0定额；type：1定量
        {
            double quotaRate = 0;
            double quantifyRate = 0;

            var quotatask = Db.QuotaTasks.Where(a => a.WbsTaskID == wbsId).FirstOrDefault();
            var quantify = Db.QuantifyTasks.Where(a => a.WbsTaskID == wbsId).FirstOrDefault();
            if (quotatask != null && quantify!=null)
            {
                double SumScore = 0;//总分
                double MyScore = 0;//我的分数
                var quotabody = Db.QuotaTaskStaffDistributions.
                    Where(b => b.QuotaTaskID == quotatask.QuotaTaskID && b.StaffID == userId).ToList();
                //var quantifybody = Db.QuantifyTaskCompletions.Where(c => c.QuantifyTaskID == quantify.QuantifyTaskID && c.StaffID == userId).ToList();
                var quantifybody = Db.QuantifyTaskCompletions.Where(c => c.QuantifyTaskID == quantify.QuantifyTaskID).ToList();
                if (quotabody.Count != 0 && quantifybody.Count != 0)
                {
                    foreach (var item in quotabody)
                    {
                        if (Convert.ToBoolean(item.IsComplete))
                        {
                            quotaRate += item.RelativeWeight;
                        }
                    }
                    foreach (var item in quantifybody)
                    {
                        if (item.StaffID == userId)
                        {
                            MyScore += item.ScoringRule.Score;
                        }
                        SumScore += item.ScoringRule.Score;
                    }
                    quantifyRate += MyScore / SumScore;
                    if (type == "0") //定额
                    {
                        return quotaRate;
                    }
                    else if(type=="1")//定量
                    {
                        return quantifyRate;
                    }
                    else
                    {
                        return quotaRate + quantifyRate;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        public List<CheckRecord> AppUserCheckRecord(string wbsId,string userId, int type)
        {
            if (wbsId == null || userId == null)
            {
                return null;
            }
            else
            {
                if (type == 0 || type == 1)
                {
                    var body =
                        Db.CheckRecords.Where(a => a.WbsTaskID == wbsId && a.UserID == userId && a.TaskType == type)
                            .ToList();
                    return body;
                }
                else
                {
                    var body =
                      Db.CheckRecords.Where(a => a.WbsTaskID == wbsId && a.UserID == userId)
                          .ToList();
                    return body;
                }
            }
        }
        public List<CheckRecord> AppAdminCheckRecord(string wbsId, string owerId,int type)
        {
            var body = Db.CheckRecords.Where(a => a.WbsTaskID == wbsId && a.OwerID == owerId && a.TaskType==type).ToList();
            return body;
        }
        public QuotaTaskStaffDistribution AppQuotadis(string finishId)
        {
            var body = Db.QuotaTaskStaffDistributions.Find(finishId);
            return body;
        }
        public string AppQuotatask(string taskid,int type)
        {
            if (type == 0)
            {
                return Db.QuotaTasks.FirstOrDefault(a => a.QuotaTaskID == taskid).TaskName;
            }
            else
            {
                return Db.ScoringRules.FirstOrDefault(a => a.ScoringRuleID == taskid).FormName;
            }
        }
        public QuantifyTaskCompletion AppQuantifycom(string finishId)
        {
            var body = Db.QuantifyTaskCompletions.Find(finishId);
            return body;
        }

    }
}
