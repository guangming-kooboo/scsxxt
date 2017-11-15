using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Qx.Scsxxt.Extentsion.Entity;
using Qx.Tools.Interfaces;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Extentsion.Repository
{
    public class V3_EnterpriseApplyRepository : BaseRepository, IRepository<Qx.Scsxxt.Extentsion.Entity.V3_EnterpriseApply>
    {
        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest =
                Db.V3_EnterpriseApply.Select(a => new SelectListItem {Text = a.Ent_No, Value = a.Ent_Name.ToString() }).ToList();
            return dest.Format(value);
        }

        public string Add(V3_EnterpriseApply model)
        {
            model.Ent_No = Pk;
            if (Find(model.Ent_No) == null)
            {
                return Db.SaveAdd(model) ? Pk : null;
            }
            return "";
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public bool Update(V3_EnterpriseApply model, string note = "")
        {
            if (Find(model.Ent_No) != null)
            {
                return Db.SaveModified(model);
            }
            return false;
        }

        public V3_EnterpriseApply Find(object id)
        {
            return Db.V3_EnterpriseApply.NoTrackingFind(a => a.Ent_No == (string) id);
        }

        public List<V3_EnterpriseApply> All()
        {
            return Db.V3_EnterpriseApply.NoTrackingToList();
        }

        public List<V3_EnterpriseApply> All(Func<V3_EnterpriseApply, bool> filter)
        {
            return Db.V3_EnterpriseApply.NoTrackingWhere(filter);
        }
    }
}