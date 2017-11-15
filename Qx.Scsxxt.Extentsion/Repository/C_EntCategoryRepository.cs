using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Qx.Scsxxt.Extentsion.Entity;
using Qx.Tools.Interfaces;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Extentsion.Repository
{
    public class C_EntCategoryRepository : BaseRepository, IRepository<Qx.Scsxxt.Extentsion.Entity.C_EntCategory>
    {
        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest =
                Db.C_EntCategory.Select(a => new SelectListItem {Text = a.EntCategoryName, Value = a.EntCategoryID.ToString() }).ToList();
            return dest.Format(value);
        }

        public string Add(C_EntCategory model)
        {
            model.EntCategoryID = Pk;
            if (Find(model.EntCategoryID) == null)
            {
                return Db.SaveAdd(model) ? Pk : null;
            }
            return "";
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public bool Update(C_EntCategory model, string note = "")
        {
            if (Find(model.EntCategoryID) != null)
            {
                return Db.SaveModified(model);
            }
            return false;
        }

        public C_EntCategory Find(object id)
        {
            return Db.C_EntCategory.NoTrackingFind(a => a.EntCategoryID == (string) id);
        }

        public List<C_EntCategory> All()
        {
            return Db.C_EntCategory.NoTrackingToList();
        }

        public List<C_EntCategory> All(Func<C_EntCategory, bool> filter)
        {
            return Db.C_EntCategory.NoTrackingWhere(filter);
        }
    }
}