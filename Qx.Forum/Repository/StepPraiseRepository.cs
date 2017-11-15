using Qx.Community.Entity;
using Qx.Community.Interfaces;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;

namespace Qx.Community.Repository
{


    public class StepPraiseRepository : BaseRepository, IRepository<BBS_StepPraise>
    {

        public System.Collections.Generic.List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            throw new System.NotImplementedException();
        }

        public string Add(BBS_StepPraise model)
        {
            return Db.SaveAdd(model) ? model.SetpPraiseID : null;
        }

        public bool Delete(object id)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(BBS_StepPraise model, string note = "")
        {
            throw new System.NotImplementedException();
        }

        public BBS_StepPraise Find(object id)
        {
            return Db.BBS_StepPraise.Find(id);
        }

        public System.Collections.Generic.List<BBS_StepPraise> All()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<BBS_StepPraise> All(System.Func<BBS_StepPraise, bool> filter)
        {
            throw new System.NotImplementedException();
        }

        public BBS_StepPraise Find(object[] id)
        {
            throw new System.NotImplementedException();
        }
    }
}
