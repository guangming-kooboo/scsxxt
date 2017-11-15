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
    public class AnswerSheetRespository : BaseService, IRepository<CPQ_T_AnswerSheet>
    {
        public string Add(CPQ_T_AnswerSheet model)
        {
            model.AnswerSheetID = model.AnswerTime+"-"+ model.AnswererIP;//主键在这里赋值
            if(Find(model.AnswerSheetID)==null)
            {     
                    return Db.SaveAdd(model)?model.AnswerSheetID:null;      
            }
            else
            {
                return "";
            }
        }

        public List<CPQ_T_AnswerSheet> All(Func<CPQ_T_AnswerSheet, bool> filter)
        {
            return Db.CPQ_T_AnswerSheet.Where(filter).ToList();
        }

        public List<CPQ_T_AnswerSheet> All()
        {
            return Db.CPQ_T_AnswerSheet.ToList();
        }

        public bool Delete(object id)
        {
            return Db.SaveDelete(Find(id));
        }

        public CPQ_T_AnswerSheet Find(object id)
        {
            return Db.CPQ_T_AnswerSheet.Find(id);
        }

        public List<System.Web.Mvc.SelectListItem> ToSelectItems(string value = "")
        {
            var dest = Db.CPQ_T_AnswerSheet.Select(a => new SelectListItem() { Text = a.Answer, Value = a.AnswerSheetID }).ToList().Format(value);
           
            return dest;
        }

        public bool Update(CPQ_T_AnswerSheet model, string note = "")
        {
            switch (note)
            {
                case "":{
                    Db.SaveModified(model);
                   
                };break;
               
                default :throw new NotImplementedException();
            }

            return true;//db.savemodofiy
        }
    }
}
