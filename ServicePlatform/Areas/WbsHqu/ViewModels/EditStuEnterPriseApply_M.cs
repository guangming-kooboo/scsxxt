using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Qx.Scsxxt.Extentsion.Entity;

namespace ServicePlatform.Areas.WbsHqu.ViewModels
{
    public class EditStuEnterPriseApply_M
    {
        public static EditStuEnterPriseApply_M ToViewModel(List<SelectListItem> posttionitems, V3_StuEnterPriseApply applyrecord)
        {
            return new EditStuEnterPriseApply_M()
            {
                entname = applyrecord.T_Enterprise.Ent_Name,
                StuEnterPriseApplyID=applyrecord.StuEnterPriseApplyID,
                Ent_No = applyrecord.Ent_No,
                UserID = applyrecord.UserID,
                posDesc=applyrecord.posDesc,
                practiceDept=applyrecord.practiceDept,
                practiceDivDept = applyrecord.practiceDivDept,
               // starttime = applyrecord.practiceStart.ToString(),
             //   endtime = applyrecord.practiceEnd.ToString(),
                positionId = applyrecord.positionId,
                posttionitems= posttionitems
            };
        }

        public string starttime { get; set; }
        public string endtime { get; set; }
        [Display(Name = "企业名称")]
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