
using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CodeManager.ToolHelpers
{
    public class getEntr
    {
        private CodeTableContext storeDB = new CodeTableContext();
        public IEnumerable<C_EntRank> LoadNewsData(GetDataGridInfo Info, string CityIDS, int COUNT, out int total)
        {
            //参数Info用于获取分页需要用的PageSize和PageIndex,total返回结果总数
            //创建news集合
            List<C_EntRank> GetEnca = storeDB.C_EntRank.ToList();
            if (COUNT == GetEnca.Count())
            {
                total = GetEnca.Count();
                var Result = GetEnca.OrderBy(c => c.EntRankID).Skip(Info.PageSize * (Info.PageIndex - 1)).Take(Info.PageSize);
                return Result;
            }
            else
            {
                List<C_EntRank> TTT = (from v in storeDB.C_EntRank where v.EntCategoryID == CityIDS select v).ToList();
                var Result = TTT.OrderBy(c => c.EntCategoryID).Skip(Info.PageSize * (Info.PageIndex - 1)).Take(Info.PageSize);
                total = TTT.Count();
                return Result;


            }

        }
    }
}