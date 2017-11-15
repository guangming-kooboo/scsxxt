using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.SigAndStamp.Funcs
{
    public class GetSigData
    {        
        private ContentContext storeDB = new ContentContext();
        public IEnumerable<T_Signature> LoadSigData(GetDataInfo Info, string GetUserID, out int total)
        {
            List<T_Signature> GetSig = storeDB.T_Signature.Where(p => p.UserID == GetUserID).ToList();
            var Result = GetSig.OrderBy(b => b.InnerID).Skip(Info.PageSize * (Info.PageIndex - 1)).Take(Info.PageSize);
            total = GetSig.Count();
            return Result;
        }
    }
}