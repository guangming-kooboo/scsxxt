using Qx.Community.Entity;
using Qx.Community.Interfaces;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;

namespace Qx.Community.Repository
{


    public class ShareRepository : BaseRepository, IRepository<BBS_Share>
    {

        public System.Collections.Generic.List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            throw new System.NotImplementedException();
        }

        public string Add(BBS_Share model)
        {
            return Db.SaveAdd(model) ? model.ShareID : null;
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(id);
        }

        public bool Update(BBS_Share model, string note = "")
        {
            return Db.SaveModified(model);
        }

        public BBS_Share Find(object id)
        {
            return Db.BBS_Share.Find(id);
        }

        public System.Collections.Generic.List<BBS_Share> All()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<BBS_Share> All(System.Func<BBS_Share, bool> filter)
        {
            throw new System.NotImplementedException();
        }

        public BBS_Share Find(object[] id)
        {
            throw new System.NotImplementedException();
        }
    }
}
