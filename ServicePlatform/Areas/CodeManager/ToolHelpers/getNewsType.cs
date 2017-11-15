using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CodeManager.ToolHelpers
{
    public class getNewsType
    {
        private ContentContext storeDB = new ContentContext();
        public IEnumerable<C_NewsType> LoadNewsData(GetDataGridInfo Info, out int total)
        {
            //参数Info用于获取分页需要用的PageSize和PageIndex,total返回结果总数
            //创建news集合
            List<C_NewsType> GetNews = storeDB.C_NewsType.ToList();
            total = GetNews.Count();
            var Result = GetNews.OrderBy(b => b.TypeID).Skip(Info.PageSize * (Info.PageIndex - 1)).Take(Info.PageSize);//依据参数获取一定数目的新闻            
            return Result;
        }
    }
}