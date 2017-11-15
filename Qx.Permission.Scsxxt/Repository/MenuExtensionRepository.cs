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


    public class MenuExtensionRepository : BaseRepository, IRepository<MenuExtension>
    {

        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest = Db.MenuExtensions.Select(a => new SelectListItem() { Text = a.Menu.Name, Value = a.MenuID }).ToList();
            return dest.Format(value);
        }

        public string Add(MenuExtension model)
        {
            if (Find(model.MenuID) == null)
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

        public bool Update(MenuExtension model, string note = "")
        {
            if (Find(model.MenuID) != null)
            {
                return Db.SaveModified(model);
            }
            else
            {
                return false;
            }
        }

        public MenuExtension Find(object id)
        {
            return Db.MenuExtensions.NoTrackingFind(a => a.MenuID == (string)id);
        }

        public List<MenuExtension> All()
        {
            return Db.MenuExtensions.NoTrackingToList();
        }

        public List<MenuExtension> All(Func<MenuExtension, bool> filter)
        {
            return Db.MenuExtensions.NoTrackingWhere(filter);
        }
    }
}
