﻿using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CodeManager.ToolHelpers
{
    public class GetStuEvaSchoolGradeLevelItem
    {
        private CodeTableContext storeDB = new CodeTableContext();
        private EnterpriseContext CodeTable1 = new EnterpriseContext();
        public IEnumerable<C_StuEvaSchoolGradeLevelItem> LoadNewsData(GetDataGridInfo Info, string CityIDS, int COUNT, out int total)
        {
            //参数Info用于获取分页需要用的PageSize和PageIndex,total返回结果总数
            //创建news集合
            List<T_PracBatch> pici = CodeTable1.PracBatch.ToList();
            List<C_StuEvaSchoolGradeLevelItem> GetEnca = storeDB.C_StuEvaSchoolGradeLevelItem.ToList();
            if (COUNT == GetEnca.Count())
            {
                IEnumerable<C_StuEvaSchoolGradeLevelItem> TTT = (from v in GetEnca from j in pici where j.SchoolID == CityIDS && v.PracBatchID == j.PracBatchID select v).ToList();
                var Result = TTT.OrderBy(c => c.ItemNo).Skip(Info.PageSize * (Info.PageIndex - 1)).Take(Info.PageSize);
                total = TTT.Count();
                return Result;
            }
            else
            {
                List<C_StuEvaSchoolGradeLevelItem> TTT = (from v in storeDB.C_StuEvaSchoolGradeLevelItem where v.PracBatchID == CityIDS select v).ToList();
                var Result = TTT.OrderBy(c => c.ItemNo).Skip(Info.PageSize * (Info.PageIndex - 1)).Take(Info.PageSize);
                total = TTT.Count();
                return Result;


            }

        }
    }
}