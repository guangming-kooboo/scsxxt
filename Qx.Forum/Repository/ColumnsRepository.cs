using Qx.Community.Entity;
using Qx.Community.Interfaces;
using Qx.Tools.CommonExtendMethods;
using System;
using Qx.Tools.Interfaces;

namespace Qx.Community.Repository
{


    public class ColumnsRepository : BaseRepository, IRepository<BBS_Columns>
    {

        public System.Collections.Generic.List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            throw new System.NotImplementedException();
        }

        public string Add(BBS_Columns model)
        {
            model.ColumnID = DateTime.Now.Random().ToString();
            model.FatherColumnID = "-1";
            return Db.SaveAdd(model) ? model.ForumID : null;
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(id);
        }

        public bool Update(BBS_Columns model, string note = "")
        {
             var old =Db.BBS_Columns.Find(model.ColumnID);
             old.ColumnName = model.ColumnName;
             old.ColumnExplain = model.ColumnExplain;
             return Db.SaveModified(old);
        }

        public BBS_Columns Find(object id)
        {
            return Db.BBS_Columns.Find(id);
        }

        public System.Collections.Generic.List<BBS_Columns> All()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<BBS_Columns> All(System.Func<BBS_Columns, bool> filter)
        {
            throw new System.NotImplementedException();
        }

        public BBS_Columns Find(object[] id)
        {
            throw new System.NotImplementedException();
        }
    }
}
