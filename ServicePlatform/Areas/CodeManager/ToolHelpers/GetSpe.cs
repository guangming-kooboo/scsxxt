using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CodeManager.ToolHelpers
{
    public class GetSpe
    {
        private CodeTableContext storeDB = new CodeTableContext();
        public IEnumerable<C_Specialty> LoadNewsData(GetDataGridInfo Info, int CityIDS,int COUNT, string sid, out int total)
        {
            //参数Info用于获取分页需要用的PageSize和PageIndex,total返回结果总数
            //创建news集合
            List<C_Specialty> GetEnca = storeDB.C_Specialty.Where(a=>a.SchoolID==sid).ToList();
            if (COUNT == GetEnca.Count())
            {
                total = GetEnca.Count();
                var Result = GetEnca.OrderBy(c => c.SpeNo).Skip(Info.PageSize * (Info.PageIndex - 1)).Take(Info.PageSize);
                return Result;
            }
            else
            {
                List<C_Specialty> TTT = (from v in storeDB.C_Specialty
                                         where v.EntryYear == CityIDS &&
                                        v.SchoolID == sid
                                         select v).ToList();
                var Result = TTT.OrderBy(c => c.SpeNo).Skip(Info.PageSize * (Info.PageIndex - 1)).Take(Info.PageSize);
                total = TTT.Count();
                return Result;


            }

        }
    }
}