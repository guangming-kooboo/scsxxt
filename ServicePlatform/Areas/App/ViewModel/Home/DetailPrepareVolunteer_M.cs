using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class DetailPrepareVolunteer_M
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