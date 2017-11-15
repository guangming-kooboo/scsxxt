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


    public class QuotaTaskRepository : BaseRepository, IRepository<QuotaTask>
    {
        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest = Db.QuotaTasks.Select(a => new SelectListItem() { Text = a.TaskName, Value = a.QuotaTaskID }).ToList();
            return dest.Format(value);
        }

        public string Add(QuotaTask model)
        {
            model.QuotaTaskID = Pk;
            if (Find(model.QuotaTaskID) == null)
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

        public bool Update(QuotaTask model, string note = "")
        {
            if (Find(model.QuotaTaskID) != null)
            {
              return  Db.SaveModified(model);
            }
            else
            {
                return false;
            }
        }

        public QuotaTask Find(object id)
        {
            return Db.QuotaTasks.NoTrackingFind(a=>a.QuotaTaskID == (string)id);
        }

        public List<QuotaTask> All()
        {
            return Db.QuotaTasks.NoTrackingToList();
        }

        public List<QuotaTask> All(Func<QuotaTask, bool> filter)
        {
            return Db.QuotaTasks.NoTrackingWhere(filter);
        }
    }
}
