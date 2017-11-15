using Qx.Scsxxt.Extentsion.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.WbsHqu.ViewModels
{
    public class EnterPriseApply_M
    {
        public V3_EnterpriseApply ToModel()
        {
            return new V3_EnterpriseApply
            {
                EntRankID = EntRankID,
                ApplyState = ApplyState,
                Contectinfo = Contectinfo,
                Email = Email,
                EntAddress = EntAddress,
                EntCategoryID = EntCategoryID,
                EntResume = EntResume,
                Ent_Name = Ent_Name,
                Ent_No = Ent_No,
                RegisterTime = RegisterTime,
                SchoolID = SchoolID,
                UpdateTime = UpdateTime,
                UserID = UserID
            };
        }
        public static EnterPriseApply_M ToViewModel(List<SelectListItem> EntCategoryItems )
        {
            return new EnterPriseApply_M()
            {
                EntCategoryItems= EntCategoryItems,
            };
        }
       
        [StringLength(20)]
        public string Ent_No { get; set; }
        [Display(Name = "企业名称")]

        [StringLength(1024)]
        public string Ent_Name { get; set; }
        [Display(Name = "企业分类")]
        [StringLength(6)]
        public string EntCategoryID { get; set; }
        [Display(Name = "企业等级")]
        [StringLength(6)]
        public string EntRankID { get; set; }
        [Display(Name = "企业地址")]
        [StringLength(1024)]
        public string EntAddress { get; set; }

        [StringLength(20)]
        public string UserID { get; set; }

        [StringLength(10)]
        public string SchoolID { get; set; }

        public int RegisterTime { get; set; }

        public int UpdateTime { get; set; }

        public int ApplyState { get; set; }
        [Display(Name = "企业联系信息")]
        [StringLength(1024)]
        public string Contectinfo { get; set; }
        [Display(Name = "企业邮箱")]
        [StringLength(50)]
        public string Email { get; set; }
        [Display(Name = "企业简介")]
        [Column(TypeName = "text")]
        public string EntResume { get; set; }
        public List<SelectListItem> EntCategoryItems;
        public List<SelectListItem> EntRankItems = new List<SelectListItem>() { new SelectListItem { Value = "请选择", Text = "请选择" } };
    }
}