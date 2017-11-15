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


    public class CheckRecordRepository : BaseRepository, IRepository<CheckRecord>
    {
        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest = Db.CheckRecords.Select(a => new SelectListItem() { Text = a.TaskName, Value = a.FinishID }).ToList();
            return dest.Format(value);
        }

        public string Add(CheckRecord model)
        {
            model.FinishID = Pk;
            if (Find(model.FinishID) == null)
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

        public bool Update(CheckRecord model, string note = "")
        {
            if (Find(model.FinishID) != null)
            {
              return  Db.SaveModified(model);
            }
            else
            {
                return false;
            }
        }

        public CheckRecord Find(object id)
        {
            return Db.CheckRecords.NoTrackingFind(a=>a.FinishID == (string)id);
        }

        public List<CheckRecord> All()
        {
            return Db.CheckRecords.NoTrackingToList();
        }

        public List<CheckRecord> All(Func<CheckRecord, bool> filter)
        {
            return Db.CheckRecords.NoTrackingWhere(filter);
        }
    }
}
