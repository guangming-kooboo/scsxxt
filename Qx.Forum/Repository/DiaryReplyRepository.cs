
using Qx.Community.Entity;
using Qx.Community.Interfaces;
using Qx.Tools.Interfaces;

namespace Qx.Community.Repository
{


    public class DiaryReplyRepository : BaseRepository, IRepository<BBS_DiaryReply>
    {

        public System.Collections.Generic.List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            throw new System.NotImplementedException();
        }

        public string Add(BBS_DiaryReply model)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(object id)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(BBS_DiaryReply model, string note = "")
        {
            throw new System.NotImplementedException();
        }

        public BBS_DiaryReply Find(object id)
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<BBS_DiaryReply> All()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<BBS_DiaryReply> All(System.Func<BBS_DiaryReply, bool> filter)
        {
            throw new System.NotImplementedException();
        }

        public BBS_DiaryReply Find(object[] id)
        {
            throw new System.NotImplementedException();
        }
    }
}
