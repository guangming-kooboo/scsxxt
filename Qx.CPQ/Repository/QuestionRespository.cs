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
    public class QuestionRespository : BaseService, IRepository<CPQ_T_Question>
    {
        public string Add(CPQ_T_Question model)
        {
            
                return Db.SaveAdd(model) ? PKInt+"" : null;
           
        }

        public List<CPQ_T_Question> All(Func<CPQ_T_Question, bool> filter)
        {
            return Db.CPQ_T_Question.Where(filter).ToList();
        }

        public List<CPQ_T_Question> All()
        {
            return Db.CPQ_T_Question.ToList();
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public CPQ_T_Question Find(object id)
        {
            return Db.CPQ_T_Question.Find(id);
        }

        public List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            var dest = Db.CPQ_T_Question.Select(a => new SelectListItem() { Text = a.QuestionName, Value = a.QuestionID }).ToList();
            dest.ForEach(a =>
            {
                a.Selected = a.Value == value;
            });
            return dest;
        }

        public bool Update(CPQ_T_Question model, string note = "")
        {
            switch (note)
            {
                case "答题":
                    {
                        //数据库更新操作

                    }; break;
               
                default: throw new NotImplementedException();
            }

            return true;//db.savemodofiy
        }
    }
}
