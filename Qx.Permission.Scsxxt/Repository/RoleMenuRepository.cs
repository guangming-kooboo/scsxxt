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


    public class RoleMenuRepository : BaseRepository, IRepository<RoleMenu>
    {

        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest = Db.RoleMenus.Select(a => new SelectListItem() { Text = a.RoleMenuID, Value = a.RoleMenuID }).ToList();
            return dest.Format(value);
        }

        public string Add(RoleMenu model)
        {
            model.RoleMenuID = model.RoleID+"-"+ model.MenuID;
            if (Find(model.RoleMenuID) == null)
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

        public bool Update(RoleMenu model, string note = "")
        {
            if (Find(model.RoleMenuID) != null)
            {
                return Db.SaveModified(model);
            }
            else
            {
                return false;
            }
        }

        public RoleMenu Find(object id)
        {
            return Db.RoleMenus.NoTrackingFind(a => a.RoleMenuID == (string)id);
        }

        public List<RoleMenu> All()
        {
            return Db.RoleMenus.NoTrackingToList();
        }

        public List<RoleMenu> All(Func<RoleMenu, bool> filter)
        {
            return Db.RoleMenus.NoTrackingWhere(filter);
        }
    }
}
