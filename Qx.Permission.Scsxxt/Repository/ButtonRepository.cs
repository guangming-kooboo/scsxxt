using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Qx.Permission.Scsxxt.Entity;
using Qx.Permission.Scsxxt.Services;
using Qx.Tools.Scsxxt.CommonExtendMethods;
using Qx.Tools.Scsxxt.Interfaces;

namespace Qx.Permission.Scsxxt.Repository
{


    public class ButtonRepository : BaseRepository, IRepository<Button>
    {

        public List<SelectListItem> ToSelectItems(string value = "")
        {
            var dest = Db.Buttons.Select(a => new SelectListItem() { Text = a.ButtonID, Value = a.Name }).ToList();
            return dest.Format(value);
        }

        public string Add(Button model)
        {
            model.ButtonID = Pk;
            if (Find(model.ButtonID) == null)
            {
                return Db.SaveAdd(model) ? Pk : null;
            }
            else
            {
                return "";
            }
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public bool Update(Button model, string note = "")
        {
            if (Find(model.ButtonID) != null)
            {
                return Db.SaveModified(model);
            }
            else
            {
                return false;
            }
        }

        public Button Find(object id)
        {
            return Db.Buttons.NoTrackingFind(a => a.ButtonID == (string)id);
        }

        public List<Button> All()
        {
            return Db.Buttons.NoTrackingToList();
        }

        public List<Button> All(Func<Button, bool> filter)
        {
            return Db.Buttons.NoTrackingWhere(filter);
        }
    }
}
