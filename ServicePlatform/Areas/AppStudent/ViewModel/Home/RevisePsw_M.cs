using System.ComponentModel.DataAnnotations;
using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class RevisePsw_M : BaseViewModel
    {
        public string Uid { get; set; }
        public static RevisePsw_M ToViewModel(string uid)
        {
            return new RevisePsw_M()
            {
                Uid=uid
            };
        }
        public UserInfo ToModel()
        {
            return new UserInfo()
            {          
                Uid=Uid,   
                Psw = NewPwd             
            };
        }
        
       // [Required(ErrorMessage = "请填写新密码")]
        [StringLength(100)]
        [Display(Name = "新密码")]
        public string NewPwd { get; set; }

       // [Required(ErrorMessage = "请填写原密码")]
        [StringLength(100)]
        [Display(Name = "原密码")]
        public string OldPwd { get; set; }

        //[Required(ErrorMessage = "请再次确认密码")]
        [StringLength(100)]
        [Display(Name = "再次确认")]
        public string SecondPwd { get; set; }

    }
}