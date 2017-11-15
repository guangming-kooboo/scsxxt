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


    public class RoleButtonForbidRepository : BaseRepository, IRepository<RoleButtonForbid>
    {

        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest = Db.RoleButtonForbids.Select(a => new SelectListItem() { Text = a.RoleButtonForbidID, Value = a.RoleButtonForbidID }).ToList();
            return dest.Format(value);
        }

        public string Add(RoleButtonForbid model)
        {
            model.RoleButtonForbidID = model.RoleID+"-"+ model.ButtonID;
            if (Find(model.RoleButtonForbidID) == null)
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

        public bool Update(RoleButtonForbid model, string note = "")
        {
            if (Find(model.RoleButtonForbidID) != null)
            {
                return Db.SaveModified(model);
            }
            else
            {
                return false;
            }
        }

        public RoleButtonForbid Find(object id)
        {
            return Db.RoleButtonForbids.NoTrackingFind(a => a.RoleButtonForbidID == (string)id);
        }

        public List<RoleButtonForbid> All()
        {
            return Db.RoleButtonForbids.NoTrackingToList();
        }

        public List<RoleButtonForbid> All(Func<RoleButtonForbid, bool> filter)
        {
            return Db.RoleButtonForbids.NoTrackingWhere(filter);
        }
    }
}
