using System;
using System.Collections.Generic;
using System.Linq;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;
using Qx.Wbs.Hqu.Services;
using Qx.Wbs.Hqu.Entity;
using System.Web.Mvc;

namespace Djk.VipCard.Repository
{


    public class StaffRepository : BaseRepository, IRepository<Staff>
    {
        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest = Db.Staffs.Select(a => new SelectListItem() { Text = a.StaffName, Value = a.StaffID }).ToList();
            return dest.Format(value);
        }

        public string Add(Staff model)
        {
            //model.StaffID = Pk;
            if (Find(model.StaffID) == null)
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

        public bool Update(Staff model, string note = "")
        {
            if (Find(model.StaffID) != null)
            {
              return  Db.SaveModified(model);
            }
            else
            {
                return false;
            }
        }

        public Staff Find(object id)
        {
            return Db.Staffs.NoTrackingFind(a=>a.StaffID == (string)id);
        }

        public List<Staff> All()
        {
            return Db.Staffs.NoTrackingToList();
        }

        public List<Staff> All(Func<Staff, bool> filter)
        {
            return Db.Staffs.NoTrackingWhere(filter);
        }
    }
}
