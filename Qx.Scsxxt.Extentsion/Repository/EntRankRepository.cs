using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Qx.Scsxxt.Extentsion.Entity;
using Qx.Tools.Interfaces;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Extentsion.Repository
{
    public class C_EntRankRepository : BaseRepository, IRepository<Qx.Scsxxt.Extentsion.Entity.C_EntRank>
    {
        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest =
                Db.C_EntRank.Select(a => new SelectListItem {Text = a.EntRankName+a.C_EntCategory.EntCategoryName, Value = a.EntRankID+";"+a.EntCategoryID }).ToList();
            return dest.Format(value);
        }

        public string Add(C_EntRank model)
        {
            model.EntRankID = Pk;
            if (Find(model.EntRankID) == null)
            {
                return Db.SaveAdd(model) ? Pk : null;
            }
            return "";
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public bool Update(C_EntRank model, string note = "")
        {
            if (Find(model.EntRankID) != null)
            {
                return Db.SaveModified(model);
            }
            return false;
        }

        public C_EntRank Find(object id)
        {
            return Db.C_EntRank.NoTrackingFind(a => a.EntRankID == (string) id);
        }

        public List<C_EntRank> All()
        {
            return Db.C_EntRank.NoTrackingToList();
        }

        public List<C_EntRank> All(Func<C_EntRank, bool> filter)
        {
            return Db.C_EntRank.NoTrackingWhere(filter);
        }
    }
}