using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class DetailFormalVolunteer_M : BaseViewModel
    {
        public Volunteer volunteer { get; set; }
        public string uid { get; set; }
        public static DetailFormalVolunteer_M ToViewModel(Volunteer volunteer, string uid)
        {
            return new DetailFormalVolunteer_M()
            {
                volunteer = volunteer,
                uid = uid
            };
        }
    }
}