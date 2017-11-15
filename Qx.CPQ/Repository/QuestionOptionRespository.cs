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
    public class QuestionOptionRespository : BaseService, IRepository<CPQ_T_QuestionOption>
    {
        public string Add(CPQ_T_QuestionOption model)
        {
            model.QuestionOptionID = model.QuestionID + "-" + model.SequenID;

            if (Find(model.QuestionOptionID) == null)
            {
                return Db.SaveAdd(model) ? model.QuestionOptionID : null;
            }
            else
            {
                return "";
            }
        }

        public List<CPQ_T_QuestionOption> All(Func<CPQ_T_QuestionOption, bool> filter)
        {
            return Db.CPQ_T_QuestionOption.Where(filter).ToList();
        }

        public List<CPQ_T_QuestionOption> All()
        {
            return Db.CPQ_T_QuestionOption.ToList();
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public CPQ_T_QuestionOption Find(object id)
        {
            return Db.CPQ_T_QuestionOption.Find(id);
        }

        public List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            var dest = Db.CPQ_T_QuestionOption.Select(a => new SelectListItem() { Text = a.QuestionOptionIName, Value = a.QuestionOptionID }).ToList().Format(value);

            return dest;
        }

        public bool Update(CPQ_T_QuestionOption model, string note = "")
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
