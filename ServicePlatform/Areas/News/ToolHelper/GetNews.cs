using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.News.ToolHelper
{
    public class GetNews
    {
        private ContentContext storeDB = new ContentContext();
        public IEnumerable<T_News> LoadNewsData(GetDataGridInfo Info,string GetUnitID, out int total)
        {
            //参数Info用于获取分页需要用的PageSize和PageIndex,total返回结果总数
            //创建news集合
            //List<T_News> GetNews = storeDB.T_News.Where(p => p.UnitID == GetUnitID).ToList();
            List<T_News> GetNews =(from a in storeDB.T_News from b in storeDB.T_Content where (a.NewsID == b.ContentID && b.UnitID == GetUnitID) select a).ToList();        
            var Result = GetNews.OrderBy(b => b.InnerID).Skip(Info.PageSize * (Info.PageIndex - 1)).Take(Info.PageSize);
            total = GetNews.Count();
            return Result;
        }
    }
}