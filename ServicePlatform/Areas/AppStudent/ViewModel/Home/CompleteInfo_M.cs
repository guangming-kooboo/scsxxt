using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Core.Services.Entity;
using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class CompleteInfo_M:BaseViewModel
    {
        public UserInfo ToModel()
        {
            return new UserInfo()
            {             
                Phone = StuCellphone,
                Mail = StuEMail,
                QQ = StuQQ,
                Sex = StuSex,
                BirthDay = StuBirthday,
                Photo = StuPhoto,
                StuResume= StuResume
            };
        }
        public static CompleteInfo_M ToViewModel(T_Student Student)
        {
            return new CompleteInfo_M()
            {
                StuCellphone = Student.StuCellphone,
                StuEMail = Student.StuEMail,
                StuQQ = Student.StuQQ,
                StuSex = Student.StuSex,
                StuBirthday = Student.StuBirthday,
                StuPhoto = Student.MainPhoto,
                StuResume = Student.StuResume
            };
        }

        public T_Student _Student { get; set; }

        public List<SelectListItem> _UnitItems;
       
        public string StuPhoto { get; set; }

        [Required(ErrorMessage = "请填写手机号码")]
       // [RegularExpression(@"^\d{11}$")]//11位的数字
        [StringLength (11)]
        [Display(Name = "手机号码")]
        public string StuCellphone{ get; set; }

        [Required(ErrorMessage = "请填写QQ号码")]
        [StringLength(20)]
        [Display(Name = "QQ号码")]
        public string StuQQ { get; set; }

        [Required(ErrorMessage = "请填写邮箱")]
        //[DataType(DataType.EmailAddress)]
        [StringLength(100)]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "邮件地址不规范")]
        [Display(Name = "邮箱")]
        public string StuEMail { get; set; }

        [Required(ErrorMessage = "请填写你的生日")]
        [StringLength(200)]
        [Display(Name = "出生日期")]
       
        public string StuBirthday { get; set; }

        [Required(ErrorMessage = "请选择性别")]
        [StringLength(2)]
        [Display(Name = "性别")]
        public string StuSex { get; set; }
        public List<SelectListItem> selectItems = new List<SelectListItem>(){
                  new SelectListItem(){Text="男",Value="男",Selected=true},
                  new SelectListItem(){Text="女",Value="女"}};

        [Required(ErrorMessage = "请填写个人简介")]
        [Display(Name = "个人简介")]
        public  string StuResume { get; set; }
    }
}