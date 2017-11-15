using Qx.CPQ.Entity;
using Qx.Tools.Interfaces;
using Qx.Wbs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Qx.Tools.CommonExtendMethods;
namespace Qx.CPQ.Repository
{
    public class C_ShareRespository : BaseService, IRepository<CPQ_C_Share>
    {
        public string Add(CPQ_C_Share model)
        {
            model.shareID = PKInt;//主键在这里赋值
            if (Find(PKInt) == null)
            {
                return Db.SaveAdd(model) ? PKInt + "" : null;
            }
            else
            {
                return "";
            }
        }

        public List<CPQ_C_Share> All(Func<CPQ_C_Share, bool> filter)
        {
            return Db.CPQ_C_Share.Where(filter).ToList();
        }

        public List<CPQ_C_Share> All()
        {
            return Db.CPQ_C_Share.ToList();
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public CPQ_C_Share Find(object id)
        {
            return Db.CPQ_C_Share.Find(id);
        }

        public List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            var dest = Db.CPQ_C_Share.Select(a => new SelectListItem() { Text = a.shareName, Value = a.shareID + "" }).ToList().Format(value);
            
            return dest;
        }

        public bool Update(CPQ_C_Share model, string note = "")
        {
            switch (note)
            {
                case "":
                    {
                        Db.SaveModified(model);

                    }; break;

                default: throw new NotImplementedException();
            }

            return true;//db.savemodofiy
        }
    }
}
