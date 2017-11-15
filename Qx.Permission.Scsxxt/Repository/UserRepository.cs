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


    public class UserRepository : BaseRepository, IRepository<User>
    {

        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest = Db.Users.Select(a => new SelectListItem() { Text = a.UserID, Value = a.NickName }).ToList();
            return dest.Format(value);
        }

        public string Add(User model)
        {
            if (Find(model.UserID) == null)
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

        public bool Update(User model, string note = "")
        {
            if (Find(model.UserID) != null)
            {
                return Db.SaveModified(model);
            }
            else
            {
                return false;
            }
        }

        public User Find(object id)
        {
            return Db.Users.NoTrackingFind(a => a.UserID == (string)id);
        }

        public List<User> All()
        {
            return Db.Users.NoTrackingToList();
        }

        public List<User> All(Func<User, bool> filter)
        {
            return Db.Users.NoTrackingWhere(filter);
        }
    }
}
