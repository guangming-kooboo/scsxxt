using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Qx.Scsxxt.Extentsion.Entity;
using Qx.Tools.Interfaces;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Extentsion.Repository
{
    public class EnterpriseToCheckRepository : BaseRepository, IRepository<Qx.Scsxxt.Extentsion.Entity.T_Enterprise_ToCheck>
    {
        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest =
                Db.T_Enterprise_ToCheck.Select(a => new SelectListItem {Text = a.Ent_No, Value = a.Ent_Name}).ToList();
            return dest.Format(value);
        }

        public string Add(T_Enterprise_ToCheck model)
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

        public bool Update(T_Enterprise_ToCheck model, string note = "")
        {
            if (Find(model.Ent_No) != null)
            {
                return Db.SaveModified(model);
            }
            return false;
        }

        public T_Enterprise_ToCheck Find(object id)
        {
            return Db.T_Enterprise_ToCheck.NoTrackingFind(a => a.Ent_No == (string) id);
        }

        public List<T_Enterprise_ToCheck> All()
        {
            return Db.T_Enterprise_ToCheck.NoTrackingToList();
        }

        public List<T_Enterprise_ToCheck> All(Func<T_Enterprise_ToCheck, bool> filter)
        {
            return Db.T_Enterprise_ToCheck.NoTrackingWhere(filter);
        }
    }
}