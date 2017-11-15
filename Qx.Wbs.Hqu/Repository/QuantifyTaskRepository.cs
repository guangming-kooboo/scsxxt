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


    public class QuantifyTaskRepository : BaseRepository, IRepository<QuantifyTask>
    {
        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest = Db.QuantifyTasks.Select(a => new SelectListItem() { Text = a.TaskName, Value = a.QuantifyTaskID }).ToList();
            return dest.Format(value);
        }

        public string Add(QuantifyTask model)
        {
            model.QuantifyTaskID = Pk;
            if (Find(model.QuantifyTaskID) == null)
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

        public bool Update(QuantifyTask model, string note = "")
        {
            if (Find(model.QuantifyTaskID) != null)
            {
              return  Db.SaveModified(model);
            }
            else
            {
                return false;
            }
        }

        public QuantifyTask Find(object id)
        {
            return Db.QuantifyTasks.NoTrackingFind(a=>a.QuantifyTaskID == (string)id);
        }

        public List<QuantifyTask> All()
        {
            return Db.QuantifyTasks.NoTrackingToList();
        }

        public List<QuantifyTask> All(Func<QuantifyTask, bool> filter)
        {
            return Db.QuantifyTasks.NoTrackingWhere(filter);
        }
    }
}
