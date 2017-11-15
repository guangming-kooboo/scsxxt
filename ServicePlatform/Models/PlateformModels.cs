using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicePlatform.Models
{
    public class PlateformModels
    {
    }
    public class T_Platformer
    {

        public string PFNo { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PFName { get; set; }
        [Key]
        public string UserID { get; set; }

    }
    #region 注释类
    public class T_Platformer_Note
    {

        public string PFNo { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PFName { get; set; }
        public string UserID { get; set; }

        public T_Platformer_Note()
        {

            PFNo = "员工编号";
            Phone = "员工电话";
            Address = "员工地址";
            Email = "员工电邮";
            PFName = "员工姓名";
            UserID = "员工用户ID";

        }
    }
    #endregion
}