using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class PrepareVolunteerList_M: BaseViewModel
    {
        public List<Volunteer> Volunteer1_1 { get; set; }
        public List<Volunteer> Volunteer1_2 { get; set; }
        public List<Volunteer> Volunteer2_1 { get; set; }
        public List<Volunteer> Volunteer2_2 { get; set; }
        public string EntPracNo { get; set; }
        public string PosID { get; set; }
        public int CareerStatus { get; set; }
        public int VolunteerSequence { get; set; }
        public int PosSequence { get; set; }
        public static PrepareVolunteerList_M ToViewModel(List<Volunteer> volunteers, int CareerStatus)
        {
            return new PrepareVolunteerList_M()
            {
                Volunteer1_1 = volunteers.Where(a => a.VolunteerSequence == 1 && a.PosSequence == 1 && a.PreVolStatus == "0").ToList(),
                Volunteer1_2 = volunteers.Where(a => a.VolunteerSequence == 1 && a.PosSequence == 2 && a.PreVolStatus == "0").ToList(),
                Volunteer2_1 = volunteers.Where(a => a.VolunteerSequence == 2 && a.PosSequence == 1 && a.PreVolStatus == "0").ToList(),
                Volunteer2_2 = volunteers.Where(a => a.VolunteerSequence == 2 && a.PosSequence == 2 && a.PreVolStatus == "0").ToList(),
                CareerStatus = CareerStatus
            };
        }
    }
}