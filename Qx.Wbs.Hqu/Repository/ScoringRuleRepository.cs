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


    public class ScoringRuleRepository : BaseRepository, IRepository<ScoringRule>
    {
        public List<SelectListItem> ToSelectItems(string quantifytaskId)
        {
            var dest = Db.ScoringRules.Where(b=>b.QuantifyTaskID == quantifytaskId).Select(a => new SelectListItem() { Text = a.FormName, Value = a.ScoringRuleID }).ToList();
            return dest.Format(quantifytaskId);
        }

        public string Add(ScoringRule model)
        {
            model.ScoringRuleID = Pk;
            if (Find(model.ScoringRuleID) == null)
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

        public bool Update(ScoringRule model, string note = "")
        {
            if (Find(model.ScoringRuleID) != null)
            {
              return  Db.SaveModified(model);
            }
            else
            {
                return false;
            }
        }

        public ScoringRule Find(object id)
        {
            return Db.ScoringRules.NoTrackingFind(a=>a.ScoringRuleID == (string)id);
        }

        public List<ScoringRule> All()
        {
            return Db.ScoringRules.NoTrackingToList();
        }

        public List<ScoringRule> All(Func<ScoringRule, bool> filter)
        {
            return Db.ScoringRules.NoTrackingWhere(filter);
        }
    }
}
