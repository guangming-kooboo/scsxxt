using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Qx.Scsxxt.Extentsion.Entity;
using Qx.Tools.Interfaces;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Extentsion.Repository
{
    public class UserRepository : BaseRepository, IRepository<Qx.Scsxxt.Extentsion.Entity.T_User>
    {
        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest =
                Db.T_User.Select(a => new SelectListItem {Text = a.UserID, Value = a.NickName}).ToList();
            return dest.Format(value);
        }

        public string Add(T_User model)
        {
            model.UserID = Pk;
            if (Find(model.UserID) == null)
            {
                return Db.SaveAdd(model) ? Pk : null;
            }
            return "";
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public bool Update(T_User model, string note = "")
        {
            if (Find(model.UserID) != null)
            {
                return Db.SaveModified(model);
            }
            return false;
        }

        public T_User Find(object id)
        {
            return Db.T_User.NoTrackingFind(a => a.UserID == (string) id);
        }

        public List<T_User> All()
        {
            return Db.T_User.NoTrackingToList();
        }

        public List<T_User> All(Func<T_User, bool> filter)
        {
            return Db.T_User.NoTrackingWhere(filter);
        }
    }
}