using Core.Services.Entity;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class Ent_Detail_M : BaseViewModel
    {
        public static Ent_Detail_M ToViewModel(T_Enterprise enterprise, string Ent_No)
        {
            return new Ent_Detail_M()
            {
                Ent_No = enterprise.Ent_No,
                Ent_Name = enterprise.Ent_Name,
                EntAddress = enterprise.EntAddress,
                EntResume = enterprise.EntResume,
                Email = enterprise.Email,
                phone = enterprise.Contectinfo,
                Photos = enterprise.EntPhotos,             
                Video = enterprise.EntVideos,
   
            };
        }


        public int DLFileColumnID { get; set; }
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