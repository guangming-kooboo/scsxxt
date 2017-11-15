using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.WbsHqu.ViewModels
{
    public class StuEnterPriseApply_M
    {
        public static StuEnterPriseApply_M ToViewModel(List<SelectListItem> posttionitems ,string Ent_No,string entname)
        {
            return new StuEnterPriseApply_M()
            {
                Ent_No=Ent_No,
                entname=entname,
                practiceEnd=DateTime.Now,
                practiceStart=DateTime.Now,
               posttionitems=posttionitems
            };
        }
        [Display(Name ="企业名称")]
        public string entname { get; set; }
        [StringLength(50)]
        public string StuEnterPriseApplyID { get; set; }
        
        [StringLength(20)]
        public string Ent_No { get; set; }

        [StringLength(20)]
        public string UserID { get; set; }

        public int? ApplyState { get; set; }

        public int? ApplyTime { get; set; }
        [Required(ErrorMessage = "请填写岗位描述")]
        [Display(Name = "岗位描述")]
        [Column(TypeName = "text")]
        public string posDesc { get; set; }


        [Display(Name = "实习部门")]
        [StringLength(50)]
        [Required(ErrorMessage = "请填写实习部门")]
        public string practiceDept { get; set; }


        [Display(Name = "实习分部门")]
        [StringLength(50)]
        [Required(ErrorMessage = "请填写实习分部门")]
        public string practiceDivDept { get; set; }


        [Display(Name = "实习开始时间")]
        [Required(ErrorMessage = "请填写实习开始时间")]
        public DateTime practiceStart { get; set; }


        [Display(Name = "实习结束时间")]
        [Required(ErrorMessage = "请填写实习结束时间")]
        public DateTime practiceEnd { get; set; }


        [Display(Name = "实习岗位")]
        [Required(ErrorMessage = "请选择实习岗位")]
        public string positionId { get; set; }
        public List<SelectListItem> posttionitems { get; set; }
    }
}