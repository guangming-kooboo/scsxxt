using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class DetailPrepareVolunteer_M : BaseViewModel
    {
        public  Volunteer volunteer { get; set; }
        public string uid { get; set; }
        public static DetailPrepareVolunteer_M ToViewModel(Volunteer volunteer,string uid)
        {
            return new DetailPrepareVolunteer_M()
            {
                volunteer= volunteer,
                uid= uid
            };
        }      
    }
}