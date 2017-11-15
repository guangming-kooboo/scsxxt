using Qx.Community.Entity;
using Qx.Community.Interfaces;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;

namespace Qx.Community.Repository
{


    public class ForumManagerRepository : BaseRepository, IRepository<BBS_ForumManager>
    {

        public System.Collections.Generic.List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            throw new System.NotImplementedException();
        }

        public string Add(BBS_ForumManager model)
        {
            return Db.SaveAdd(model) ? model.ForumID : null;
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(id);
        }

        public bool Update(BBS_ForumManager model, string note = "")
        {
            throw new System.NotImplementedException();
        }

        public BBS_ForumManager Find(object id)
        {
            return Db.BBS_ForumManager.Find(id);
        }

        public System.Collections.Generic.List<BBS_ForumManager> All()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<BBS_ForumManager> All(System.Func<BBS_ForumManager, bool> filter)
        {
            throw new System.NotImplementedException();
        }

        public BBS_ForumManager Find(object[] id)
        {
            throw new System.NotImplementedException();
        }
    }
}
