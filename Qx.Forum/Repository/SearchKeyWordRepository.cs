using Qx.Community.Entity;
using Qx.Community.Interfaces;
using Qx.Tools.Interfaces;

namespace Qx.Community.Repository
{


    public class SearchKeyWordRepository : BaseRepository, IRepository<BBS_SearchKeyWord>
    {

        public System.Collections.Generic.List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            throw new System.NotImplementedException();
        }

        public string Add(BBS_SearchKeyWord model)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(object id)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(BBS_SearchKeyWord model, string note = "")
        {
            throw new System.NotImplementedException();
        }

        public BBS_SearchKeyWord Find(object id)
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<BBS_SearchKeyWord> All()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<BBS_SearchKeyWord> All(System.Func<BBS_SearchKeyWord, bool> filter)
        {
            throw new System.NotImplementedException();
        }

        public BBS_SearchKeyWord Find(object[] id)
        {
            throw new System.NotImplementedException();
        }
    }
}
