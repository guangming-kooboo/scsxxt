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


    public class UserRoleRepository : BaseRepository, IRepository<UserRole>
    {

        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest = Db.UserRoles.Select(a => new SelectListItem() { Text = a.UserRoleID, Value = a.UserRoleID }).ToList();
            return dest.Format(value);
        }

        public string Add(UserRole model)
        {
            model.UserRoleID = model.UserID+"-"+ model.RoleID;
            if (Find(model.UserRoleID) == null)
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

        public bool Update(UserRole model, string note = "")
        {
            if (Find(model.UserRoleID) != null)
            {
                return Db.SaveModified(model);
            }
            else
            {
                return false;
            }
        }

        public UserRole Find(object id)
        {
            return Db.UserRoles.NoTrackingFind(a => a.UserRoleID == (string)id);
        }

        public List<UserRole> All()
        {
            return Db.UserRoles.NoTrackingToList();
        }

        public List<UserRole> All(Func<UserRole, bool> filter)
        {
            return Db.UserRoles.NoTrackingWhere(filter);
        }
    }
}
