using Qx.Community.Entity;
using Qx.Community.Interfaces;
using Qx.Tools.CommonExtendMethods;
using System;
using Qx.Tools.Interfaces;

namespace Qx.Community.Repository
{
    public class ForumRepository : BaseRepository, IRepository<BBS_Forum>
    {

        public System.Collections.Generic.List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            throw new System.NotImplementedException();
        }

        public string Add(BBS_Forum model)
        {
            model.ForumID = DateTime.Now.Random().ToString();
            return Db.SaveAdd(model) ? model.ForumID : null;
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(id);
        }

        public bool Update(BBS_Forum model, string note = "")
        {
            var old = Db.BBS_Forum.Find(model.ForumID);
            old.ForumExplain = model.ForumExplain;
            old.ForumName = model.ForumName;
            return Db.SaveModified(model);
        }

        public BBS_Forum Find(object id)
        {
            return Db.BBS_Forum.Find(id);
        }

        public System.Collections.Generic.List<BBS_Forum> All()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<BBS_Forum> All(System.Func<BBS_Forum, bool> filter)
        {
            throw new System.NotImplementedException();
        }

        public BBS_Forum Find(object[] id)
        {
            throw new System.NotImplementedException();
        }
    }
}
