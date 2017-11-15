using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CodeManager.ToolHelpers
{
    public class getContentColumn
    {
        private ContentContext storeDB = new ContentContext();
        public IEnumerable<C_ContentColumn> LoadNewsData(GetDataGridInfo Info, out int total)
        {
            //参数Info用于获取分页需要用的PageSize和PageIndex,total返回结果总数
            //创建news集合
            List<C_ContentColumn> GetEnca = storeDB.C_ContentColumn.ToList();
            total = GetEnca.Count();
            var Result = GetEnca.OrderBy(c => c.ContColumnID).Skip(Info.PageSize * (Info.PageIndex - 1)).Take(Info.PageSize);//依据参数获取一定数目的新闻            
            return Result;
        }
    }
}