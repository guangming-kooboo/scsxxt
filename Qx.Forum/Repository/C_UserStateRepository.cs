using Qx.Community.Entity;
using Qx.Community.Interfaces;
using Qx.Tools.Interfaces;

namespace Qx.Community.Repository
{


    public class C_UserStateRepository : BaseRepository, IRepository<BBS_C_UserState>
    {

        public System.Collections.Generic.List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            throw new System.NotImplementedException();
        }

        public string Add(BBS_C_UserState model)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(object id)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(BBS_C_UserState model, string note = "")
        {
            throw new System.NotImplementedException();
        }

        public BBS_C_UserState Find(object id)
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<BBS_C_UserState> All()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<BBS_C_UserState> All(System.Func<BBS_C_UserState, bool> filter)
        {
            throw new System.NotImplementedException();
        }

        public BBS_C_UserState Find(object[] id)
        {
            throw new System.NotImplementedException();
        }
    }
}
