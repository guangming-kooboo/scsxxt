using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Qx.Scsxxt.Extentsion.Entity;
using Qx.Tools.Interfaces;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Extentsion.Repository
{
    public class PracBatchRepository : BaseRepository, IRepository<Qx.Scsxxt.Extentsion.Entity.T_PracBatch>
    {
        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest =
                Db.T_PracBatch.Select(a => new SelectListItem {Text = a.PracBatchID, Value = a.BatchName }).ToList();
            return dest.Format(value);
        }

        public string Add(T_PracBatch model)
        {
            model.PracBatchID = Pk;
            if (Find(model.PracBatchID) == null)
            {
                return Db.SaveAdd(model) ? Pk : null;
            }
            return "";
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public bool Update(T_PracBatch model, string note = "")
        {
            if (Find(model.PracBatchID) != null)
            {
                return Db.SaveModified(model);
            }
            return false;
        }

        public T_PracBatch Find(object id)
        {
            return Db.T_PracBatch.NoTrackingFind(a => a.PracBatchID == (string) id);
        }

        public List<T_PracBatch> All()
        {
            return Db.T_PracBatch.NoTrackingToList();
        }

        public List<T_PracBatch> All(Func<T_PracBatch, bool> filter)
        {
            return Db.T_PracBatch.NoTrackingWhere(filter);
        }
    }
}