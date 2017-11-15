using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Qx.Scsxxt.Extentsion.Entity;
using Qx.Tools.Interfaces;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Scsxxt.Extentsion.Repository
{
    public class StuScoreApplyRepository : BaseRepository, IRepository<T_StuScoreApply>
    {
        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest =
                Db.T_StuScoreApply.Select(a => new SelectListItem {Text = a.PracticeNo, Value = a.PracticeNo }).ToList();
            return dest.Format(value);
        }

        public string Add(T_StuScoreApply model)
        {
            model.PracticeNo = model.PracticeNo.HasValue()? model.PracticeNo:Pk;
            if (Find(model.PracticeNo) == null)
            {
                return Db.SaveAdd(model) ? Pk : null;
            }
            return "";
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public bool Update(T_StuScoreApply model, string note = "")
        {
            if (Find(model.PracticeNo) != null)
            {
                return Db.SaveModified(model);
            }
            return false;
        }

        public T_StuScoreApply Find(object id)
        {
            return Db.T_StuScoreApply.NoTrackingFind(a => a.PracticeNo == (string) id);
        }

        public List<T_StuScoreApply> All()
        {
            return Db.T_StuScoreApply.NoTrackingToList();
        }

        public List<T_StuScoreApply> All(Func<T_StuScoreApply, bool> filter)
        {
            return Db.T_StuScoreApply.NoTrackingWhere(filter);
        }
    }
}