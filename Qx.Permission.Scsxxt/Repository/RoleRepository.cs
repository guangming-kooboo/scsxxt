using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Qx.Permission.Scsxxt.Entity;
using Qx.Permission.Scsxxt.Services;
using Qx.Tools.Scsxxt.CommonExtendMethods;
using Qx.Tools.Scsxxt.Interfaces;

namespace Qx.Permission.Scsxxt.Repository
{


    public class RoleRepository : BaseRepository, IRepository<Role>
    {

        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest = Db.Roles.Select(a => new SelectListItem() { Text = a.RoleID, Value = a.Name }).ToList();
            return dest.Format(value);
        }

        public string Add(Role model)
        {
            model.RoleID = Pk;
            if (Find(model.RoleID) == null)
            {
                return Db.SaveAdd(model) ? Pk : null;
            }
            else
            {
                return "";
            }
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public bool Update(Role model, string note = "")
        {
            if (Find(model.RoleID) != null)
            {
                return Db.SaveModified(model);
            }
            else
            {
                return false;
            }
        }

        public Role Find(object id)
        {
            return Db.Roles.NoTrackingFind(a => a.RoleID == (string)id);
        }

        public List<Role> All()
        {
            return Db.Roles.NoTrackingToList();
        }

        public List<Role> All(Func<Role, bool> filter)
        {
            return Db.Roles.NoTrackingWhere(filter);
        }
    }
}
