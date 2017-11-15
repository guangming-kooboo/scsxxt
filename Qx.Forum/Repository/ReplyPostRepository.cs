using Qx.Community.Entity;
using Qx.Community.Interfaces;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;

namespace Qx.Community.Repository
{


    public class ReplyPostRepository : BaseRepository, IRepository<BBS_ReplyPost>
    {

        public System.Collections.Generic.List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            throw new System.NotImplementedException();
        }

        public string Add(BBS_ReplyPost model)
        {
            return Db.SaveAdd(model) ? model.ReplyPostID : null;
        }

        public bool Delete(object id)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(BBS_ReplyPost model, string note = "")
        {
            throw new System.NotImplementedException();
        }

        public BBS_ReplyPost Find(object id)
        {
            return Db.BBS_ReplyPost.Find(id);
        }

        public System.Collections.Generic.List<BBS_ReplyPost> All()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<BBS_ReplyPost> All(System.Func<BBS_ReplyPost, bool> filter)
        {
            throw new System.NotImplementedException();
        }

        public BBS_ReplyPost Find(object[] id)
        {
            throw new System.NotImplementedException();
        }
    }
}
