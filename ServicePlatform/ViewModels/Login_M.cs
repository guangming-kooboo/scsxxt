using System.ComponentModel.DataAnnotations;

namespace ServicePlatform.ViewModels
{
    public class Login_M
    {
        [Required]
        [StringLength(10)]
        [Display(Name = "登录名")]
        public string UserId;
        [Required]
        [StringLength(20)]
        [Display(Name = "密码")]
        public string UserPsw;
    }
}