using ServicePlatform.Lib;
using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.HPManager.FuncHelper
{
    public class GetContentData
    {

        public IEnumerable<T_Content> LoadContentData(HPMGetPageInfo Info, out int total, string UnitID, int ConTypeID, int GetColID)
        {
            ContentContext ne = new ContentContext();
            List<T_Content> GetCon;
            switch (ConTypeID) {
                case ContentType.News:
                    GetCon = (from a in ne.T_Content from b in ne.T_News where (a.ContentID == b.NewsID && a.UnitID == UnitID && b.NewsColumnID == GetColID) select a).ToList();                    
                    break; 
                case ContentType.AD:
                    GetCon = (from a in ne.T_Content from b in ne.T_News where (a.ContentID == b.NewsID && a.UnitID == UnitID) select a).ToList();                    
                    //应该修改为广告表,广告部分写好后要对其进行修改
                    break; 
                case ContentType.DownLoadFile:
                    GetCon = (from a in ne.T_Content from b in ne.T_DownLoadFiles where (a.ContentID == b.DLFileID && a.UnitID == UnitID && b.DLFileColumnID == GetColID) select a).ToList();                    
                    break; 
                default:
                    GetCon = (from a in ne.T_Content from b in ne.T_News where (a.ContentID == b.NewsID && a.UnitID == UnitID) select a).ToList();                    
                    //无法匹配，则转化为全集
                    break; 
            }
            var Result = GetCon.Skip(Info.PageSize * (Info.PageIndex - 1)).Take(Info.PageSize);
            //total = Result.Count();
            total = GetCon.Count();
            return Result;
        }

        public IEnumerable<T_Content> LoadBaseContentData(HPMGetPageInfo Info, out int total, string UnitID)
        {
            ContentContext ne = new ContentContext();
      
            List<T_Content> GetCon = (from a in ne.T_Content where (a.UnitID == UnitID) select a).ToList(); 

            var Result = GetCon.Skip(Info.PageSize * (Info.PageIndex - 1)).Take(Info.PageSize);
            total = GetCon.Count();
            return Result;
        }

    }
}