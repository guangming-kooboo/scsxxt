using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Services.Entity;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class Ent_Des_M : BaseViewModel
    {
        public string Video { get; set; }

        public List<T_DownLoadFiles> AdList { get; set; }
 
        public static Ent_Des_M ToViewModel(T_Enterprise enterprise, string Ent_No)
        {
            return new Ent_Des_M()
            {
                Ent_No = enterprise.Ent_No,
                Ent_Name = enterprise.Ent_Name,
                EntAddress = enterprise.EntAddress,
                EntResume = enterprise.EntResume,
                Email = enterprise.Email,
                phone = enterprise.Contectinfo,
                Photos=enterprise.EntPhotos,
                //AdList = AdList,
                Video=enterprise.EntVideos,
                EntRank=enterprise.EntRank,
         
            };
        }

        public string EntRank { get; set; }

        public string Ent_No { get; set; }
        public string Ent_Name { get; set; }

        [Display(Name = "企业地址")]
        public string EntAddress { get; set; }

        [Display(Name = "企业介绍")]
        public string EntResume { get; set; }

        [Display(Name = "企业邮箱")]
        public string Email { get; set; }
        [Display(Name = "电话")]
        public string phone { get; set; }
        public string Photos { get; set; }
    }
}