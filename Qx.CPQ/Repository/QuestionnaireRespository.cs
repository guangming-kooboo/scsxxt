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
    public class QuestionnaireRespository : BaseService, IRepository<CPQ_T_Questionnaire>
    {
        public string Add(CPQ_T_Questionnaire model)
        {
            //model.QuestionnaireID = model.OwnerID + "-" + DateTime.Now.ToString("yyyyMMddhhmm");

            //if (Find(model.QuestionnaireID) == null)
            //{
            //    return Db.SaveAdd(model) ? model.QuestionnaireID : null;
            //}
            //else
            //{
            //    return "";
            //}
            return Db.SaveAdd(model) ? model.QuestionnaireID : null;
        }

        public List<CPQ_T_Questionnaire> All(Func<CPQ_T_Questionnaire, bool> filter)
        {
            return Db.CPQ_T_Questionnaire.Where(filter).ToList();
        }

        public List<CPQ_T_Questionnaire> All()
        {
            return Db.CPQ_T_Questionnaire.ToList();
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public CPQ_T_Questionnaire Find(object id)
        {
            return Db.CPQ_T_Questionnaire.Find(id);
        }

        public List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            var dest = Db.CPQ_T_Questionnaire.Select(a => new SelectListItem() { Text = a.QuestionnaireName, Value = a.QuestionnaireID }).ToList().Format(value);
           
            return dest;
        }

        public bool Update(CPQ_T_Questionnaire model, string note = "")
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
