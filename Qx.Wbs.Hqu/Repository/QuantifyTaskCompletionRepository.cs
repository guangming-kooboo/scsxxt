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


    public class QuantifyTaskCompletionRepository : BaseRepository, IRepository<QuantifyTaskCompletion>
    {
        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest = Db.QuantifyTaskCompletions.Select(a => new SelectListItem() { Text = a.StaffName, Value = a.QuantifyTaskCompletionID }).ToList();
            return dest.Format(value);
        }

        public string Add(QuantifyTaskCompletion model)
        {
            model.QuantifyTaskCompletionID = Pk;
            if (Find(model.QuantifyTaskCompletionID) == null)
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

        public bool Update(QuantifyTaskCompletion model, string note = "")
        {
            if (Find(model.QuantifyTaskCompletionID) != null)
            {
              return  Db.SaveModified(model);
            }
            else
            {
                return false;
            }
        }

        public QuantifyTaskCompletion Find(object id)
        {
            return Db.QuantifyTaskCompletions.NoTrackingFind(a=>a.QuantifyTaskCompletionID == (string)id);
        }

        public List<QuantifyTaskCompletion> All()
        {
            return Db.QuantifyTaskCompletions.NoTrackingToList();
        }

        public List<QuantifyTaskCompletion> All(Func<QuantifyTaskCompletion, bool> filter)
        {
            return Db.QuantifyTaskCompletions.NoTrackingWhere(filter);
        }
    }
}
