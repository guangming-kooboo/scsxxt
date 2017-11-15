using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.DownLoadFile.Func
{
    public class GetDLData
    {
        private ContentContext storeDB = new ContentContext();
        public IEnumerable<T_DownLoadFiles> LoadDLData(GetDataInfo Info, string theUnitID, out int total)
            //根据企业编号得到企业的下载文件列表
        {
            List<T_DownLoadFiles> GetDLs = (from a in storeDB.T_DownLoadFiles from b in storeDB.T_Content where (a.DLFileID == b.ContentID && b.UnitID == theUnitID) select a).ToList();
            var Result = GetDLs.OrderBy(b => b.InnerID).Skip(Info.PageSize * (Info.PageIndex - 1)).Take(Info.PageSize);
            total = GetDLs.Count();
            return Result;
        }
    }
}