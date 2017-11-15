using Core.Services.Entity;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class RecEnt_Detail_M : BaseViewModel
    {
        public static RecEnt_Detail_M ToViewModel(T_Enterprise enterprise, string EntPracNo, int VolunteerSequence, int PosSequence)
        {
            return new RecEnt_Detail_M()
            {
                Ent_No =enterprise.Ent_No,
                EntPracNo = EntPracNo,
                Ent_Name = enterprise.Ent_Name,
                Email = enterprise.Email,
                EntAddress = enterprise.EntAddress,
                EntResume = enterprise.EntResume,
                phone = enterprise.Contectinfo,
                Video = enterprise.EntVideos,
                //AdList= AdList,
                Photos = enterprise.EntPhotos,
                VolunteerSequence = VolunteerSequence,
                PosSequence = PosSequence
            };
        }
        public int DLFileColumnID { get; set; }
        public int PosSequence { get; set; }

        public int VolunteerSequence { get; set; }

        public string EntPracNo { get; set; }

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