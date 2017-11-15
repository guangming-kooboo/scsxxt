using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.HPManager.FuncHelper
{
    public class HPShowData
    {
        private ContentContext storeDB = new ContentContext();        
        public IQueryable<T_Content> GetShowData(int targetColumn) {
           //用于获取首页某栏目显示的数据,参数targetColumn标识要显示的栏目
           int ContentRowNum = 0;//该栏目的RowCount
           var n = from a in storeDB.T_HomePageColumn where (a.HPColumnID == targetColumn) select a;
           //只能返回1个或0个结果           
           foreach (var b in n)
          {
              ContentRowNum = b.ColRowCount;//获取指定栏目的需显示内容的条数
           }
           var ContentSet = (from aim in storeDB.T_HomePageContent from bim in storeDB.T_Content where (aim.HPColID == targetColumn && bim.ContentID == aim.HPCID) select bim).Take(ContentRowNum);
           return ContentSet;
        }
    }
}