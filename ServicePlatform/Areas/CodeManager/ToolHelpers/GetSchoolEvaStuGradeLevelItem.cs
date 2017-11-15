using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CodeManager.ToolHelpers
{
    public class GetSchoolEvaStuGradeLevelItem
    {
        private CodeTableContext storeDB = new CodeTableContext();
        private EnterpriseContext CodeTable1 = new EnterpriseContext();
        public IEnumerable<C_SchoolEvaStuGradeLevelItem> LoadNewsData(GetDataGridInfo Info, string CityIDS, int COUNT, out int total)
        {
            //参数Info用于获取分页需要用的PageSize和PageIndex,total返回结果总数
            //创建news集合
            List<T_PracBatch> pici = CodeTable1.PracBatch.ToList();
            List<C_SchoolEvaStuGradeLevelItem> GetEnca = storeDB.C_SchoolEvaStuGradeLevelItem.ToList();
            if (COUNT == GetEnca.Count())
            {
                IEnumerable<C_SchoolEvaStuGradeLevelItem> TTT = (from v in GetEnca from j in pici where j.SchoolID == CityIDS && v.PracBatchID == j.PracBatchID select v).ToList();
                var Result = TTT.OrderBy(c => c.ItemNo).Skip(Info.PageSize * (Info.PageIndex - 1)).Take(Info.PageSize);
                total = TTT.Count();
                return Result;
            }
            else
            {
                List<C_SchoolEvaStuGradeLevelItem> TTT = (from v in storeDB.C_SchoolEvaStuGradeLevelItem where v.PracBatchID == CityIDS select v).ToList();
                var Result = TTT.OrderBy(c => c.ItemNo).Skip(Info.PageSize * (Info.PageIndex - 1)).Take(Info.PageSize);
                total = TTT.Count();
                return Result;


            }

        }
    }
}