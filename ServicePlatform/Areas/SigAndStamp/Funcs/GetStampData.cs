using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.SigAndStamp.Funcs
{
    public class GetStampData
    {
        private ContentContext storeDB = new ContentContext();
        public IEnumerable<T_Stamps> LoadDLData(GetDataInfo Info, string GetUnitID, out int total)
        {
            List<T_Stamps> GetStamps = (from a in storeDB.T_Stamps from b in storeDB.T_Content where (a.StampsID == b.ContentID && b.UnitID == GetUnitID) select a).ToList();
            var Result = GetStamps.OrderBy(b => b.InnerID).Skip(Info.PageSize * (Info.PageIndex - 1)).Take(Info.PageSize);
            total = GetStamps.Count();
            return Result;
        }
    }
}