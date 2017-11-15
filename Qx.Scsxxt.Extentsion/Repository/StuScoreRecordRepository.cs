using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Qx.Scsxxt.Extentsion.Entity;
using Qx.Tools.Interfaces;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Extentsion.Repository
{
    public class StuScoreRecordRepository : BaseRepository, IRepository<T_StuScoreRecord>
    {
        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest =
                Db.T_StuScoreRecord.Select(a => new SelectListItem {Text = a.StuScoreStuScoreID, Value = a.StuScoreStuScoreID }).ToList();
            return dest.Format(value);
        }

        public string Add(T_StuScoreRecord model)
        {
            model.StuScoreStuScoreID = Pk;
            if (Find(model.StuScoreStuScoreID) == null)
            {
                return Db.SaveAdd(model) ? Pk : null;
            }
            return "";
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public bool Update(T_StuScoreRecord model, string note = "")
        {
            if (Find(model.StuScoreStuScoreID) != null)
            {
                return Db.SaveModified(model);
            }
            return false;
        }

        public T_StuScoreRecord Find(object id)
        {
            return Db.T_StuScoreRecord.NoTrackingFind(a => a.StuScoreStuScoreID == (string) id);
        }

        public List<T_StuScoreRecord> All()
        {
            return Db.T_StuScoreRecord.NoTrackingToList();
        }

        public List<T_StuScoreRecord> All(Func<T_StuScoreRecord, bool> filter)
        {
            return Db.T_StuScoreRecord.NoTrackingWhere(filter);
        }
    }
}