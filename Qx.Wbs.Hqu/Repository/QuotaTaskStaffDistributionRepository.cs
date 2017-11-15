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


    public class QuotaTaskStaffDistributionRepository : BaseRepository, IRepository<QuotaTaskStaffDistribution>
    {
        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest = Db.QuotaTaskStaffDistributions.Select(a => new SelectListItem() { Text = a.StaffName, Value = a.QuotaTaskStaffDistributionID }).ToList();
            return dest.Format(value);
        }

        public string Add(QuotaTaskStaffDistribution model)
        {
            model.QuotaTaskStaffDistributionID = Pk;
            if (Find(model.QuotaTaskStaffDistributionID) == null)
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

        public bool Update(QuotaTaskStaffDistribution model, string note = "")
        {
            if (Find(model.QuotaTaskStaffDistributionID) != null)
            {
              return  Db.SaveModified(model);
            }
            else
            {
                return false;
            }
        }

        public QuotaTaskStaffDistribution Find(object id)
        {
            return Db.QuotaTaskStaffDistributions.NoTrackingFind(a=>a.QuotaTaskStaffDistributionID == (string)id);
        }

        public List<QuotaTaskStaffDistribution> All()
        {
            return Db.QuotaTaskStaffDistributions.NoTrackingToList();
        }

        public List<QuotaTaskStaffDistribution> All(Func<QuotaTaskStaffDistribution, bool> filter)
        {
            return Db.QuotaTaskStaffDistributions.NoTrackingWhere(filter);
        }
    }
}
