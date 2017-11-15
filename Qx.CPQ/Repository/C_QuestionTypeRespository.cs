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
    public class C_QuestionTypeRespository : BaseService, IRepository<CPQ_C_QuestionType>
    {
        public string Add(CPQ_C_QuestionType model)
        {
            model.QuestionTypeID = PKInt;//主键在这里赋值
            if (Find(PKInt) == null)
            {
                return Db.SaveAdd(model) ? PKInt + "" : null;
            }
            else
            {
                return "";
            }
        }

        public List<CPQ_C_QuestionType> All(Func<CPQ_C_QuestionType, bool> filter)
        {
            return Db.CPQ_C_QuestionType.Where(filter).ToList();
        }

        public List<CPQ_C_QuestionType> All()
        {
            return Db.CPQ_C_QuestionType.ToList();
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public CPQ_C_QuestionType Find(object id)
        {
            return Db.CPQ_C_QuestionType.Find(id);
        }

        public List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            var dest = Db.CPQ_C_QuestionType.Select(a => new SelectListItem() { Text = a.QuestionTypeName, Value = a.QuestionTypeID+"" }).ToList().Format(value);
           
            return dest;
        }

        public bool Update(CPQ_C_QuestionType model, string note = "")
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
