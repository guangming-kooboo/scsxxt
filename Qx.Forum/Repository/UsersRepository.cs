using Qx.Community.Entity;
using Qx.Community.Interfaces;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;

namespace Qx.Community.Repository
{


    public class UsersRepository : BaseRepository, IRepository<BBS_Users>
    {

        public System.Collections.Generic.List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            throw new System.NotImplementedException();
        }

        public string Add(BBS_Users model)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(object id)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(BBS_Users model, string note = "")
        {
            return Db.SaveModified(model);
        }

        public BBS_Users Find(object id)
        {
            return Db.BBS_Users.Find(id);
        }

        public System.Collections.Generic.List<BBS_Users> All()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<BBS_Users> All(System.Func<BBS_Users, bool> filter)
        {
            throw new System.NotImplementedException();
        }

        public BBS_Users Find(object[] id)
        {
            throw new System.NotImplementedException();
        }
    }
}
