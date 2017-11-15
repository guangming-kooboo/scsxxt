using Qx.Community.Entity;
using Qx.Community.Interfaces;
using Qx.Tools.Interfaces;

namespace Qx.Community.Repository
{


    public class VisitorRepository : BaseRepository, IRepository<BBS_Visitor>
    {

        public System.Collections.Generic.List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            throw new System.NotImplementedException();
        }

        public string Add(BBS_Visitor model)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(object id)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(BBS_Visitor model, string note = "")
        {
            throw new System.NotImplementedException();
        }

        public BBS_Visitor Find(object id)
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<BBS_Visitor> All()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<BBS_Visitor> All(System.Func<BBS_Visitor, bool> filter)
        {
            throw new System.NotImplementedException();
        }

        public BBS_Visitor Find(object[] id)
        {
            throw new System.NotImplementedException();
        }
    }
}
