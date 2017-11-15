using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class FormalVolunteer_M
    {
        public int CareerStatus { get; set; }

        public List<Volunteer> Volunteers { get; set; }

        public string Uid { get; set; }
        public List<Volunteer> Volunteer1_1 { get; set; }
        public List<Volunteer> Volunteer1_2 { get; set; }
        public List<Volunteer> Volunteer2_1 { get; set; }
        public List<Volunteer> Volunteer2_2 { get; set; }
        public static FormalVolunteer_M ToViewModel(string uid, List<Volunteer> volunteers,int CareerStatus)
        {
            return new FormalVolunteer_M()//正式志愿：PreVolStatus=="1"  表示预填志愿转为正式志愿
            {
                CareerStatus= CareerStatus,
                Uid =uid,
                Volunteer1_1 = volunteers.Where(a => a.VolunteerSequence == 1 && a.PosSequence == 1 && a.PreVolStatus=="1").ToList(),
                Volunteer1_2 = volunteers.Where(a => a.VolunteerSequence == 1 && a.PosSequence == 2 && a.PreVolStatus == "1").ToList(),
                Volunteer2_1 = volunteers.Where(a => a.VolunteerSequence == 2 && a.PosSequence == 1 && a.PreVolStatus == "1").ToList(),
                Volunteer2_2 = volunteers.Where(a => a.VolunteerSequence == 2 && a.PosSequence == 2 && a.PreVolStatus == "1").ToList()
            };
        }
    }
}