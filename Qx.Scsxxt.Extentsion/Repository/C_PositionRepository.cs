using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Qx.Scsxxt.Extentsion.Entity;
using Qx.Tools.Interfaces;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Extentsion.Repository
{
    public class C_PositionRepository : BaseRepository, IRepository<Qx.Scsxxt.Extentsion.Entity.C_Position>
    {
        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest =
                Db.C_Position.Select(a => new SelectListItem {Text = a.PositionName, Value = a.PositionID.ToString() }).ToList();
            return dest.Format(value);
        }

        public string Add(C_Position model)
        {
            model.PositionID = Pk;
            if (Find(model.PositionID) == null)
            {
                return Db.SaveAdd(model) ? Pk : null;
            }
            return "";
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public bool Update(C_Position model, string note = "")
        {
            if (Find(model.PositionID) != null)
            {
                return Db.SaveModified(model);
            }
            return false;
        }

        public C_Position Find(object id)
        {
            return Db.C_Position.NoTrackingFind(a => a.PositionID == (string) id);
        }

        public List<C_Position> All()
        {
            return Db.C_Position.NoTrackingToList();
        }

        public List<C_Position> All(Func<C_Position, bool> filter)
        {
            return Db.C_Position.NoTrackingWhere(filter);
        }
    }
}