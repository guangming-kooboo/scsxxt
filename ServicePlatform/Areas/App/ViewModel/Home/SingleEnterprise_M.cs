using Core.Services.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class SingleEnterprise_M
    {
        public string Video { get; set; }

        public List<T_DownLoadFiles> AdList { get; set; }
        public string Uid { get; set; }
        public static SingleEnterprise_M ToViewModel(T_Enterprise enterprise, string Ent_No,string uid)
        {
            return new SingleEnterprise_M()
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
                Uid = uid
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