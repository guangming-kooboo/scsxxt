using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Qx.Scsxxt.Extentsion.Entity;

using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;

namespace Qx.Scsxxt.Extentsion.Repository
{
    public class StuBatchRegRepository : BaseRepository, IRepository<Qx.Scsxxt.Extentsion.Entity.T_StuBatchReg>
    {
        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest =
                Db.T_StuBatchReg.Select(a => new SelectListItem {Text = a.PracticeNo, Value = a.UserID }).ToList();
            return dest.Format(value);
        }

        public string Add(T_StuBatchReg model)
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

        public bool Update(T_StuBatchReg model, string note = "")
        {
            if (Find(model.PracticeNo) != null)
            {
                return Db.SaveModified(model);
            }
            return false;
        }

        public T_StuBatchReg Find(object id)
        {
            return Db.T_StuBatchReg.NoTrackingFind(a => a.PracticeNo == (string) id);
        }

        public List<T_StuBatchReg> All()
        {
            return Db.T_StuBatchReg.NoTrackingToList();
        }

        public List<T_StuBatchReg> All(Func<T_StuBatchReg, bool> filter)
        {
            return Db.T_StuBatchReg.NoTrackingWhere(filter);
        }
    }
}