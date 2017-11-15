using Qx.Community.Entity;
using Qx.Community.Interfaces;
using Qx.Tools.Interfaces;

namespace Qx.Community.Repository
{


    public class FriendRepository : BaseRepository, IRepository<BBS_Friend>
    {

        public System.Collections.Generic.List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            throw new System.NotImplementedException();
        }

        public string Add(BBS_Friend model)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(object id)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(BBS_Friend model, string note = "")
        {
            throw new System.NotImplementedException();
        }

        public BBS_Friend Find(object id)
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<BBS_Friend> All()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<BBS_Friend> All(System.Func<BBS_Friend, bool> filter)
        {
            throw new System.NotImplementedException();
        }

        public BBS_Friend Find(object[] id)
        {
            throw new System.NotImplementedException();
        }
    }
}
