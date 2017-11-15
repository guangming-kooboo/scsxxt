using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class VolunteerDetail_M : BaseViewModel
    {
        public Volunteer _volunteer;

        public static VolunteerDetail_M ToViewModel(Volunteer volunteer)
        {
            return new VolunteerDetail_M()
            {
                _volunteer= volunteer
            };
        }
    }
}