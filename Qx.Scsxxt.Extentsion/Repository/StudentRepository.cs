using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Qx.Scsxxt.Extentsion.Entity;

using Qx.Tools.Interfaces;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Extentsion.Repository
{
    public class StudentRepository : BaseRepository, IRepository<Qx.Scsxxt.Extentsion.Entity.T_Student>
    {
        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest =
                Db.T_Student.Select(a => new SelectListItem {Text = a.UserID, Value = a.StuName }).ToList();
            return dest.Format(value);
        }

        public string Add(T_Student model)
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

        public bool Update(T_Student model, string note = "")
        {
            if (Find(model.UserID) != null)
            {
                return Db.SaveModified(model);
            }
            return false;
        }

        public T_Student Find(object id)
        {
            return Db.T_Student.NoTrackingFind(a => a.UserID == (string) id);
        }

        public List<T_Student> All()
        {
            return Db.T_Student.NoTrackingToList();
        }

        public List<T_Student> All(Func<T_Student, bool> filter)
        {
            return Db.T_Student.NoTrackingWhere(filter);
        }
    }
}