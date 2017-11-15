using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Qx.Wbs.Hqu.Entity;

namespace ServicePlatform.Areas.WbsHqu.ViewModels
{
    public class Staff_M
    {
        [Display(Name = "员工号")]
        public string StaffID { get; set; }

        [Display(Name = "员工姓名")]
        public string StaffName { get; set; }

        public Staff ToModel()
        {
            return new Staff() {StaffID =StaffID,StaffName = StaffName};
        }

        public static Staff_M ToViewModel(Staff model)
        {
            return new Staff_M() {StaffName = model.StaffName,StaffID = model.StaffID};
        }

    }
}