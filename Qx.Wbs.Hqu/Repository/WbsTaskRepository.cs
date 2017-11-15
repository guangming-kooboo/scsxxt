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


    public class WbsTaskRepository : BaseRepository, IRepository<WbsTask>
    {
        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest = Db.WbsTasks.Select(a => new SelectListItem() { Text = a.TaskName, Value = a.WbsTaskID }).ToList();
            return dest.Format(value);
        }

        public string Add(WbsTask model)
        {
            model.WbsTaskID = Pk;
            if (Find(model.WbsTaskID) == null)
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

        public bool Update(WbsTask model, string note = "")
        {
            if (Find(model.WbsTaskID) != null)
            {
              return  Db.SaveModified(model);
            }
            else
            {
                return false;
            }
        }

        public WbsTask Find(object id)
        {
            return Db.WbsTasks.NoTrackingFind(a=>a.WbsTaskID == (string)id);
        }

        public List<WbsTask> All()
        {
            return Db.WbsTasks.NoTrackingToList();
        }

        public List<WbsTask> All(Func<WbsTask, bool> filter)
        {
            return Db.WbsTasks.NoTrackingWhere(filter);
        }
    }
}
