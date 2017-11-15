using Qx.Scsxxt.Extentsion.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.V2.ViewModels.stubatchregextentAdd
{
    public class stubatchregextentAdd_M
    {
        public T_StuBatchReg_Extent ToModel()
        {
            return new T_StuBatchReg_Extent()
            {
               
            };
        }
        public static stubatchregextentAdd_M ToViewModel(string studentname, List<SelectListItem> T_EnterpriseselectItems)
        {
            return new stubatchregextentAdd_M()
            {
                studentname = studentname,
            T_EnterpriseselectItems = T_EnterpriseselectItems
            };
        }
        [Display(Name = "收件人")]
        [StringLength(50)]
        public string studentname { get; set; }
        public List<SelectListItem> T_EnterpriseselectItems { get; set; }
        public string Ent_No { get; set; }

}
}