
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Services.Entity;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class EnterpriseInfoIndex_M
    {
        public static EnterpriseInfoIndex_M ToViewModel(T_Enterprise enterprise, string EntPracNo, string uid,int VolunteerSequence, int PosSequence)
        {
            return new EnterpriseInfoIndex_M()
            {
                EntPracNo = EntPracNo,
                Ent_Name = enterprise.Ent_Name,
                Email = enterprise.Email,
                EntAddress = enterprise.EntAddress,
                EntResume = enterprise.EntResume,
                phone = enterprise.Contectinfo,
                Video = enterprise.EntVideos,
                //AdList= AdList,
                Photos = enterprise.EntPhotos,
                Uid = uid,
                VolunteerSequence = VolunteerSequence,
                PosSequence = PosSequence
            };
        }

        public object PosSequence { get; set; }

        public object VolunteerSequence { get; set; }

        public string EntPracNo { get; set; }

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