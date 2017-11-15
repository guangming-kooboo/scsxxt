using Qx.Community.Entity;
using Qx.Community.Interfaces;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;

namespace Qx.Community.Repository
{


    public class PostRepository : BaseRepository, IRepository<BBS_Post>
    {

        public System.Collections.Generic.List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            throw new System.NotImplementedException();
        }

        public string Add(BBS_Post model)
        {
            return Db.SaveAdd(model)?model.PostID:null;
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(id);
        }

        public bool Update(BBS_Post model, string note = "")
        {
            throw new System.NotImplementedException();
        }

        public BBS_Post Find(object id)
        {
            return Db.BBS_Post.Find(id);
        }

        public System.Collections.Generic.List<BBS_Post> All()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<BBS_Post> All(System.Func<BBS_Post, bool> filter)
        {
            throw new System.NotImplementedException();
        }

        public BBS_Post Find(object[] id)
        {
            throw new System.NotImplementedException();
        }
    }
}
