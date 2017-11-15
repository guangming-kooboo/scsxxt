
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qx.Wbs.Hqu.Interfaces;
using Qx.Tools.CommonExtendMethods;
using Qx.Wbs.Hqu.Entity;

namespace Qx.Wbs.Hqu.Services
{
    public class WbsProvider : BaseRepository, IWbsProvider
    {
        private static QuotaTask Copy(QuotaTask quotatask)
        {
            var newquotatask = new QuotaTask();
            newquotatask.QuotaTaskID = quotatask.QuotaTaskID;
            newquotatask.WbsTaskID = quotatask.WbsTaskID;
            newquotatask.FatherID = quotatask.FatherID;
            newquotatask.TaskName = quotatask.TaskName;
            newquotatask.RelativeWeight = quotatask.RelativeWeight;
            newquotatask.AbsoluteWeight = quotatask.AbsoluteWeight;
            newquotatask.TaskContent = quotatask.TaskContent;
            newquotatask.BeginTime = quotatask.BeginTime;
            newquotatask.EndTime = quotatask.EndTime;
            newquotatask.CreateTime = DateTime.Now;
            newquotatask.TaskSequence = quotatask.TaskSequence;
            newquotatask.IsLeafNode = quotatask.IsLeafNode;
            return newquotatask;
        }
        private static WbsTask Copy(WbsTask wbstask)
        {
            var newwbstask = new WbsTask();
            newwbstask.WbsTaskID = wbstask.WbsTaskID;
            newwbstask.TaskName = wbstask.TaskName;
            newwbstask.QuotaTaskWeight = wbstask.QuotaTaskWeight;
            newwbstask.QuantifyTaskWeight = wbstask.QuantifyTaskWeight;
            newwbstask.MotorTaskWeight = wbstask.MotorTaskWeight;
            newwbstask.BeginTime = wbstask.BeginTime;
            newwbstask.EndTime = wbstask.EndTime;
            newwbstask.OwnerID = wbstask.OwnerID;
            newwbstask.CreateTime = DateTime.Now;
            return newwbstask;
        }
        private static QuantifyTask Copy(QuantifyTask quantifytask)
        {
            var newquantifytask = new QuantifyTask();
            newquantifytask.QuantifyTaskID = quantifytask.QuantifyTaskID;
            newquantifytask.TaskName = quantifytask.TaskName;
            newquantifytask.WbsTaskID = quantifytask.WbsTaskID;
            newquantifytask.RelativeWeight = quantifytask.RelativeWeight;
            newquantifytask.AbsoluteWeight = quantifytask.AbsoluteWeight;
            newquantifytask.TaskContent = quantifytask.TaskContent;
            newquantifytask.BeginTime = quantifytask.BeginTime;
            newquantifytask.EndTime = quantifytask.EndTime;
            newquantifytask.TaskContent = quantifytask.TaskContent;
            newquantifytask.TaskSequence = quantifytask.TaskSequence;
            return newquantifytask;

        }
        public bool HasQuotaChild(string wbstaskId)
        {
            var data = Db.QuotaTasks.Where(a => a.FatherID == "0" && a.WbsTaskID == wbstaskId).ToList();
            if (data.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool QuotaHasChild(string quotataskId)
        {
            var data = Db.QuotaTasks.Where(a => a.FatherID == quotataskId).ToList();
            if (data.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool HasQuantifyChild(string wbstaskId)
        {
            var data = Db.QuantifyTasks.Where(a => a.WbsTaskID == wbstaskId).ToList();
            if (data.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //工作量的复制    
        public bool CopyWbs(string wbstaskId)
        {
            #region 数据复制
            //复制工作量
            var wbstask = Db.WbsTasks.Find(wbstaskId);
            var newwbstask = Copy(wbstask);
            newwbstask.WbsTaskID = DateTime.Now.Random2();

            //复制定量任务
            var quantifytask = Db.QuantifyTasks.Where(a => a.WbsTaskID == wbstaskId).SingleOrDefault();
            var newquantifytask = Copy(quantifytask);
            if (newquantifytask != null)
            {
                newquantifytask.QuantifyTaskID = DateTime.Now.Random2();
                newquantifytask.WbsTaskID = newwbstask.WbsTaskID;
            }

            //复制定额任务
            string tempstring = "";
            int quotanum = Db.QuotaTasks.Where(a => a.WbsTaskID == wbstaskId).ToList().Count();
            var quotaquery = new List<QuotaTask>();//旧定额任务队列
            var newquotaquery = new List<QuotaTask>();//新定额任务队列
            var rootquota = Db.QuotaTasks.Where(a => a.WbsTaskID == wbstaskId && a.FatherID == "0").ToList();
            if (rootquota.Count > 0)
            {
                quotaquery.AddRange(rootquota);//原根定额任务入队
                for (int i = 0; i < rootquota.Count; i++)
                {
                    var tempquota = Copy(rootquota[i]);
                    tempquota.WbsTaskID = newwbstask.WbsTaskID;
                    tempquota.QuotaTaskID = DateTime.Now.Random2();
                    newquotaquery.Add(tempquota);
                }
            }
            for (int i = 0; i < quotanum; i++)
            {
                tempstring = quotaquery[i].QuotaTaskID;
                var templistquota = Db.QuotaTasks.Where(a => a.FatherID == tempstring).ToList();
                if (templistquota.Count > 0)
                {
                    quotaquery.AddRange(templistquota);
                    for (int j = 0; j < templistquota.Count; j++)
                    {
                        var tempquota = Copy(templistquota[j]);
                        tempquota.QuotaTaskID = DateTime.Now.Random2();
                        tempquota.FatherID = newquotaquery[i].QuotaTaskID;
                        tempquota.WbsTaskID = newwbstask.WbsTaskID;
                        newquotaquery.Add(tempquota);
                    }
                }
            }
            #endregion

            try
            {
                Db.WbsTasks.Add(newwbstask);
                Db.QuantifyTasks.Add(newquantifytask);
                Db.QuotaTasks.AddRange(newquotaquery);
                return Db.Saved();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                newquotaquery = null;
                newquantifytask = null;
                newwbstask = null;
                quotaquery = null;
                rootquota = null;
            }

        }
        
    }
}
