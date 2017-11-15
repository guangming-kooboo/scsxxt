using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Qx.Scsxxt.Extentsion.Entity;
using Qx.Tools.Interfaces;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Extentsion.Repository
{
    public class V3_StuEnterPriseApplyRepository : BaseRepository, IRepository<Qx.Scsxxt.Extentsion.Entity.V3_StuEnterPriseApply>
    {
        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest =
                Db.V3_StuEnterPriseApply.Select(a => new SelectListItem {Text = a.StuEnterPriseApplyID, Value = a.StuEnterPriseApplyID.ToString() }).ToList();
            return dest.Format(value);
        }

        public string Add(V3_StuEnterPriseApply model)
        {
            model.StuEnterPriseApplyID = Pk;
            if (Find(model.StuEnterPriseApplyID) == null)
            {
                return Db.SaveAdd(model) ? Pk : null;
            }
            return "";
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public bool Update(V3_StuEnterPriseApply model, string note = "")
        {
            if (Find(model.StuEnterPriseApplyID) != null)
            {
                return Db.SaveModified(model);
            }
            return false;
        }

        public V3_StuEnterPriseApply Find(object id)
        {
            return Db.V3_StuEnterPriseApply.NoTrackingFind(a => a.StuEnterPriseApplyID == (string) id);
        }

        public List<V3_StuEnterPriseApply> All()
        {
            return Db.V3_StuEnterPriseApply.NoTrackingToList();
        }

        public List<V3_StuEnterPriseApply> All(Func<V3_StuEnterPriseApply, bool> filter)
        {
            return Db.V3_StuEnterPriseApply.NoTrackingWhere(filter);
        }
    }
}