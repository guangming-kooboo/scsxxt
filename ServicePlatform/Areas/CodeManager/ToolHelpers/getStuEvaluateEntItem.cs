using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CodeManager.ToolHelpers
{
    public class getStuEvaluateEntItem
    {
        private CodeTableContext storeDB = new CodeTableContext();
        private EnterpriseContext CodeTable1 = new EnterpriseContext();
        public IEnumerable<C_StuEvaluateEntItem> LoadNewsData(GetDataGridInfo Info, string CityIDS, int COUNT, out int total)
        {
            //参数Info用于获取分页需要用的PageSize和PageIndex,total返回结果总数
            //创建news集合
            List<T_PracBatch> pici = CodeTable1.PracBatch.ToList();
            List<C_StuEvaluateEntItem> GetEnca = storeDB.C_StuEvaluateEntItem.ToList();
            if (COUNT == GetEnca.Count())
            {
                IEnumerable<C_StuEvaluateEntItem> TTT = (from v in GetEnca from j in pici where j.SchoolID == CityIDS && v.PracBatchID == j.PracBatchID select v).ToList();
                var Result = TTT.OrderBy(c => c.ItemNo).Skip(Info.PageSize * (Info.PageIndex - 1)).Take(Info.PageSize);
                total = TTT.Count();
                return Result;
            }
            else
            {
                List<C_StuEvaluateEntItem> TTT = (from v in storeDB.C_StuEvaluateEntItem where v.PracBatchID == CityIDS select v).ToList();//评估表里室友批次编号的没有的事批次名
                var Result = TTT.OrderBy(c => c.ItemNo).Skip(Info.PageSize * (Info.PageIndex - 1)).Take(Info.PageSize);
                total = TTT.Count();
                return Result;


            }

        }
    }
}