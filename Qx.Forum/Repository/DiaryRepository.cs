using Qx.Community.Entity;
using Qx.Community.Interfaces;
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Interfaces;

namespace Qx.Community.Repository
{


    public class DiaryRepository : BaseRepository, IRepository<BBS_Diary>
    {

        public System.Collections.Generic.List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            throw new System.NotImplementedException();
        }

        public string Add(BBS_Diary model)
        {
            return Db.SaveAdd(model) ? model.DiaryID : null;
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(id);
        }

        public bool Update(BBS_Diary model, string note = "")
        {
            return Db.SaveModified(model);
        }

        public BBS_Diary Find(object id)
        {
            return Db.BBS_Diary.Find(id);
        }

        public System.Collections.Generic.List<BBS_Diary> All()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<BBS_Diary> All(System.Func<BBS_Diary, bool> filter)
        {
            throw new System.NotImplementedException();
        }

        public BBS_Diary Find(object[] id)
        {
            throw new System.NotImplementedException();
        }
    }
}
