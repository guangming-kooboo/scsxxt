using Qx.Community.Entity;
using Qx.Community.Interfaces;
using Qx.Tools.CommonExtendMethods;
using System;
using Qx.Tools.Interfaces;

namespace Qx.Community.Repository
{


    public class ThemeRepository : BaseRepository, IRepository<BBS_Theme>
    {

        public System.Collections.Generic.List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            throw new System.NotImplementedException();
        }

        public string Add(BBS_Theme model)
        {
            model.ThemeID = DateTime.Now.Random().ToString();
            return Db.SaveAdd(model) ? model.ThemeID : null;
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(id);
        }

        public bool Update(BBS_Theme model, string note = "")
        {
            var old =Db.BBS_Theme.Find(model.ThemeID);
            old.ThemeName = model.ThemeName;
            old.ThemeExplain = model.ThemeExplain;
            return Db.SaveModified(old);
        }

        public BBS_Theme Find(object id)
        {
            return Db.BBS_Theme.Find(id);
        }

        public System.Collections.Generic.List<BBS_Theme> All()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<BBS_Theme> All(System.Func<BBS_Theme, bool> filter)
        {
            throw new System.NotImplementedException();
        }

        public BBS_Theme Find(object[] id)
        {
            throw new System.NotImplementedException();
        }
    }
}
