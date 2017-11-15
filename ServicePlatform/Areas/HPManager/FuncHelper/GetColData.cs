using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.HPManager.FuncHelper
{
    public class GetColData
    {
        private ContentContext storeDB = new ContentContext();  
        //该函数获取指定单位所有内容（T_Content）数据
        public IEnumerable<T_HomePageContent> LoadColData(HPMGetPageInfo Info, out int total,string UnitID)
        {
            //参数Info用于获取分页需要用的PageSize和PageIndex,total返回结果总数

            
            List<T_HomePageContent> GetCol;// = storeDB.T_HomePageContent.ToList();
            GetCol = (from hpc in storeDB.T_HomePageContent
                     from hpcc in storeDB.T_HomePageColumn
                     where (hpc.HPColID == hpcc.HPColumnID && hpcc.UnitID == UnitID)
                     select hpc).ToList();
            var Result = GetCol.Skip(Info.PageSize * (Info.PageIndex - 1)).Take(Info.PageSize);
            //total = Result.Count();
            total = GetCol.Count();
            return Result;
        }
    }
}