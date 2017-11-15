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


    public class MenuRepository : BaseRepository, IRepository<Menu>
    {

        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest = Db.Menus.Select(a => new SelectListItem() { Text = a.MenuID, Value = a.Name }).ToList();
            return dest.Format(value);
        }

        public string Add(Menu model)
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

        public bool Update(Menu model, string note = "")
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

        public Menu Find(object id)
        {
            return Db.Menus.NoTrackingFind(a => a.MenuID == (string)id);
        }

        public List<Menu> All()
        {
            return Db.Menus.NoTrackingToList();
        }

        public List<Menu> All(Func<Menu, bool> filter)
        {
            return Db.Menus.NoTrackingWhere(filter);
        }
    }
}
