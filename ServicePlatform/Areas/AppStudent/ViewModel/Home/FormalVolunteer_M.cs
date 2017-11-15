using System.Collections.Generic;
using System.Linq;
using Core.Services.Entity;
using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class FormalVolunteer_M : BaseViewModel
    {
        public T_PracticeVolunteer data { get; set; }

        public string EntPracNo { get; set; }
        public string PosID { get; set; }
        public int CareerStatus { get; set; }
        public int id { get; set; }
        public List<Volunteer> Volunteers { get; set; }

        public string Uid { get; set; }
        public List<Volunteer> Volunteer1_1 { get; set; }
        public List<Volunteer> Volunteer1_2 { get; set; }
        public List<Volunteer> Volunteer2_1 { get; set; }
        public List<Volunteer> Volunteer2_2 { get; set; }
        public static FormalVolunteer_M ToViewModel( List<Volunteer> volunteers,int CareerStatus, T_PracticeVolunteer data)
        {
            return new FormalVolunteer_M()//正式志愿：PreVolStatus=="1"  表示预填志愿转为正式志愿
            {
                data= data,
                CareerStatus = CareerStatus,
               
                Volunteer1_1 = volunteers.Where(a => a.VolunteerSequence == 1 && a.PosSequence == 1 && a.PreVolStatus=="1").ToList(),
                Volunteer1_2 = volunteers.Where(a => a.VolunteerSequence == 1 && a.PosSequence == 2 && a.PreVolStatus == "1").ToList(),
                Volunteer2_1 = volunteers.Where(a => a.VolunteerSequence == 2 && a.PosSequence == 1 && a.PreVolStatus == "1").ToList(),
                Volunteer2_2 = volunteers.Where(a => a.VolunteerSequence == 2 && a.PosSequence == 2 && a.PreVolStatus == "1").ToList()
            };
        }
    }
}