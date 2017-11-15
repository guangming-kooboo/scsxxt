using Qx.Community.Entity;
using Qx.Community.Interfaces;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;

namespace Qx.Community.Repository
{


    public class ReplyPraiseRepository : BaseRepository, IRepository<BBS_ReplyParise>
    {

        public System.Collections.Generic.List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            throw new System.NotImplementedException();
        }

        public string Add(BBS_ReplyParise model)
        {
            return Db.SaveAdd(model) ? model.PraiseID : null;
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(id);
        }

        public bool Update(BBS_ReplyParise model, string note = "")
        {
            throw new System.NotImplementedException();
        }

        public BBS_ReplyParise Find(object id)
        {
            return Db.BBS_ReplyParise.Find(id);
        }

        public System.Collections.Generic.List<BBS_ReplyParise> All()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<BBS_ReplyParise> All(System.Func<BBS_ReplyParise, bool> filter)
        {
            throw new System.NotImplementedException();
        }

        public BBS_ReplyParise Find(object[] id)
        {
            throw new System.NotImplementedException();
        }
    }
}
