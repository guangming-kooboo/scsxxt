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
    public class AttachQuestionToQuestionnaireRespository : BaseService, IRepository<CPQ_T_AttachQuestionToQuestionnaire>
    {
        public string Add(CPQ_T_AttachQuestionToQuestionnaire model)
        {
            model.AttachID = model.QuestionnaireID + "-" + model.QuestionID+ "-" +model.QuestionSequenceID;//主键在这里赋值
            if (Find(model.AttachID) == null)
            {
                return Db.SaveAdd(model) ? model.AttachID : null;
            }
            else
            {
                return "";
            }
        }

        public List<CPQ_T_AttachQuestionToQuestionnaire> All(Func<CPQ_T_AttachQuestionToQuestionnaire, bool> filter)
        {
            return Db.CPQ_T_AttachQuestionToQuestionnaire.Where(filter).ToList();
        }

        public List<CPQ_T_AttachQuestionToQuestionnaire> All()
        {
            return Db.CPQ_T_AttachQuestionToQuestionnaire.ToList();
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public CPQ_T_AttachQuestionToQuestionnaire Find(object id)
        {
            return Db.CPQ_T_AttachQuestionToQuestionnaire.Find(id);
        }

        public List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            var dest = Db.CPQ_T_AttachQuestionToQuestionnaire.Select(a => new SelectListItem() { Text = a.QuestionID, Value = a.AttachID }).ToList().Format(value);
            
            return dest;
        }

        public bool Update(CPQ_T_AttachQuestionToQuestionnaire model, string note = "")
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
