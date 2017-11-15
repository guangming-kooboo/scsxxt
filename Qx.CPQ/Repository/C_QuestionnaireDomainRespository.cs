using Qx.CPQ.Entity;
using Qx.Tools.Interfaces;
using Qx.Wbs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qx.Tools.CommonExtendMethods;
using System.Web.Mvc;
namespace Qx.CPQ.Repository
{
    public class C_QuestionnaireDomainRespository : BaseService, IRepository<CPQ_C_QuestionnaireDomain>
    {
        public string Add(CPQ_C_QuestionnaireDomain model)
        {
            model.QuestionnaireDomainID = PKInt;//主键在这里赋值
            if (Find(PKInt) == null)
            {
                return Db.SaveAdd(model) ? PKInt + "" : null;
            }
            else
            {
                return "";
            }
        }

        public List<CPQ_C_QuestionnaireDomain> All(Func<CPQ_C_QuestionnaireDomain, bool> filter)
        {
            return Db.CPQ_C_QuestionnaireDomain.Where(filter).ToList();
        }

        public List<CPQ_C_QuestionnaireDomain> All()
        {
            return Db.CPQ_C_QuestionnaireDomain.ToList();
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public CPQ_C_QuestionnaireDomain Find(object id)
        {
            return Db.CPQ_C_QuestionnaireDomain.Find(id);
        }

        public List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            var dest = Db.CPQ_C_QuestionnaireDomain.Select(a => new SelectListItem() { Text = a.QuestionnaireDomainName, Value = a.QuestionnaireDomainID+"" }).ToList().Format(value);
           
            return dest;
        }

        public bool Update(CPQ_C_QuestionnaireDomain model, string note = "")
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
