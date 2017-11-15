using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;


namespace ServicePlatform.Models
{

    [Table("T_User")]
    public class T_User
    {
        [Key]
        [Display(Name = "用户ID")]

        public string UserID { get; set; }


        [Display(Name = "昵称")]

        public string NickName { get; set; }



        [Display(Name = "密码")]
        [Required]
        public string UserPwd { get; set; }


        [Display(Name = "用户类型")]
        public int UserType { get; set; }

        [Display(Name = "日志")]
        public string Note { get; set; }


    }
    #region 注释类


    public class T_User_Note
    {

        public string UserID { get; set; }

        public string NickName { get; set; }

        public string UserPwd { get; set; }

        public string UserType { get; set; }

        public string Note { get; set; }

        public T_User_Note()
        {

            UserID = "用户账号";

            NickName = "用户姓名";

            UserPwd = "密码";

            UserType = "用户类型";

            Note = "日志";

        }
        //PageHelper.GetModelNote<T_User>(new T_User(),new string[]{"用户ID","昵称","密码","用户类型","日志"});
    }

   
    #endregion
  
}