using Qx.Community.Entity;
using Qx.Community.Interfaces;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;

namespace Qx.Community.Repository
{


    public class PhotoRepository : BaseRepository, IRepository<BBS_Photo>
    {

        public System.Collections.Generic.List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            throw new System.NotImplementedException();
        }

        public string Add(BBS_Photo model)
        {
            return Db.SaveAdd(model) ? model.PhtotID : null;
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(id);
        }

        public bool Update(BBS_Photo model, string note = "")
        {
            throw new System.NotImplementedException();
        }

        public BBS_Photo Find(object id)
        {
            return Db.BBS_Photo.Find(id);
        }

        public System.Collections.Generic.List<BBS_Photo> All()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<BBS_Photo> All(System.Func<BBS_Photo, bool> filter)
        {
            throw new System.NotImplementedException();
        }

        public BBS_Photo Find(object[] id)
        {
            throw new System.NotImplementedException();
        }
    }
}
