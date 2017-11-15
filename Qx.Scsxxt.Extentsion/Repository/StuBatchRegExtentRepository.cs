using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Qx.Scsxxt.Extentsion.Entity;
using Qx.Tools.Interfaces;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Extentsion.Repository
{
    public class StuBatchRegExtentRepository : BaseRepository, IRepository<Qx.Scsxxt.Extentsion.Entity.T_StuBatchReg_Extent>
    {
        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest =
                Db.T_StuBatchReg_Extent.Select(a => new SelectListItem {Text = a.PracticeNo, Value = a.IsEffect.ToString() }).ToList();
            return dest.Format(value);
        }

        public string Add(T_StuBatchReg_Extent model)
        {
            model.PracticeNo = Pk;
            if (Find(model.PracticeNo) == null)
            {
                return Db.SaveAdd(model) ? Pk : null;
            }
            return "";
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public bool Update(T_StuBatchReg_Extent model, string note = "")
        {
            if (Find(model.PracticeNo) != null)
            {
                return Db.SaveModified(model);
            }
            return false;
        }

        public T_StuBatchReg_Extent Find(object id)
        {
            return Db.T_StuBatchReg_Extent.NoTrackingFind(a => a.PracticeNo == (string) id);
        }

        public List<T_StuBatchReg_Extent> All()
        {
            return Db.T_StuBatchReg_Extent.NoTrackingToList();
        }

        public List<T_StuBatchReg_Extent> All(Func<T_StuBatchReg_Extent, bool> filter)
        {
            return Db.T_StuBatchReg_Extent.NoTrackingWhere(filter);
        }
    }
}