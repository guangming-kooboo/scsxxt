using Qx.Community.Entity;
using Qx.Community.Interfaces;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;

namespace Qx.Community.Repository
{


    public class NoteRepository : BaseRepository, IRepository<BBS_Note>
    {

        public System.Collections.Generic.List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            throw new System.NotImplementedException();
        }

        public string Add(BBS_Note model)
        {
            return Db.SaveAdd(model) ? model.NoteID : null;
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(id);
        }

        public bool Update(BBS_Note model, string note = "")
        {
            throw new System.NotImplementedException();
        }

        public BBS_Note Find(object id)
        {
            return Db.BBS_Note.Find(id);
        }

        public System.Collections.Generic.List<BBS_Note> All()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<BBS_Note> All(System.Func<BBS_Note, bool> filter)
        {
            throw new System.NotImplementedException();
        }

        public BBS_Note Find(object[] id)
        {
            throw new System.NotImplementedException();
        }
    }
}
