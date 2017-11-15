
using Core.Services.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class EnterpriseIndex_M
    {
        public static EnterpriseIndex_M ToViewModel(T_Enterprise enterprise, string Ent_No,string uid)
        {
            return new EnterpriseIndex_M()
            {
                Ent_No = enterprise.Ent_No,
                Ent_Name = enterprise.Ent_Name,
                EntAddress = enterprise.EntAddress,
                EntResume = enterprise.EntResume,
                Email = enterprise.Email,
                phone = enterprise.Contectinfo,
                Photos = enterprise.EntPhotos,             
                Video = enterprise.EntVideos,
                Uid = uid
            };
        }

        public string Uid { get; set; }

        public string Video { get; set; }

        public string Photos { get; set; }

        public string phone { get; set; }

        public string Email { get; set; }

        public string EntResume { get; set; }

        public string EntAddress { get; set; }

        public string Ent_Name { get; set; }

        public string Ent_No { get; set; }
    }
}