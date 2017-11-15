using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Qx.Wbs.Hqu.Entity;


namespace Qx.Wbs.Hqu.Interfaces
{
    public interface IWbsService
    {
        double AbsoluteWeightSub(string QuotaTaskFatherID, double RelativeWeight);

        double AbsoluteWeight(string WbsTaskID, double RelativeWeight);

        bool UpdateStaffDistribut(string quotataskId);

        bool UpdateSubAbsoluteWeight(string quotataskId);

        bool UpdateAbsoluteWeight(string wbstaskId);

        bool QuotaTaskWeight(QuotaTask quotatask);

        bool QuantifyTaskWeight(QuantifyTask quantifytask);

        bool StaffWeight(QuotaTaskStaffDistribution staffdis);

        string quantifyBrother(QuantifyTask quantify);

        string quotaBrother(QuotaTask quota);

        string DistributionBrother(QuotaTaskStaffDistribution quodis);

        List<QuotaTask> FindFather(string quotaId);

        string FindFatherQuotaTask(string quotaskId);

        List<SelectListItem> FindLeafNode(string wbstaskId);
        List<List<string>> CheckRecord(string userId, bool type);
        bool CheckRecord(CheckRecord model, bool type);
        bool CreateCheckRecord(string taskdisId, string taskname, string wbsId, int type, string userId);
        //工作量
        List<WbsTask> GetTasks(string userunit);
       //具体的工作量
        List<Models.Task> GetMyTask(string type, string wbsid, string userid);
        QuantifyTask GetQuantifyTask(string wbstaskId);
        QuotaTaskStaffDistribution GetQuotadis(string quotataskId);
        bool Record(string wbstaskid, string tasktype, string mytaskid, string note, string certificate, string userId);
        double AppStaffSumary(string wbsId, string userId, string type);//APP完成比例
        List<CheckRecord> AppUserCheckRecord(string wbsId, string userId,int type);
        List<CheckRecord> AppAdminCheckRecord(string wbsId, string owerId, int type);
        QuotaTaskStaffDistribution AppQuotadis(string finishId);
        QuantifyTaskCompletion AppQuantifycom(string finishId);
        string AppQuotatask(string taskid, int type);
    }
}
