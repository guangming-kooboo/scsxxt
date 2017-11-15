using System.Collections.Generic;
using System.Web.Mvc;
using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{//岗位
    public class RecEnt_Post_M : BaseViewModel
    { 
        public static RecEnt_Post_M ToViewModel(List<Job> jobs, string EntPracNo, int VolunteerSequence, int PosSequence)
        {
            return new RecEnt_Post_M()
            {
                EntPracNo = EntPracNo,
                VolunteerSequence= VolunteerSequence,
                PosSequence= PosSequence,
                //PositionName= PositionName,
                //PosID= PosID,
                Jobs =jobs
            };
        }
        public string Uid { get; set; }
        public List<Job> Jobs { get; set; }
        public string EntPracNo { get; set; }
        public string PracticeNo { get; set; }//学生实习号
        public string PosID { get; set; }
        public string PositionName { get; set; }
        public int PosSequence { get; set; }
        public int VolunteerSequence { get; set; }
        //[Display(Name = "志愿")]
        public List<SelectListItem> selectItems = new List<SelectListItem>(){
                  new SelectListItem(){Text="第一志愿",Value="1",Selected=true},
                  new SelectListItem(){Text="第二志愿",Value="2"}};
      
        //  public List<>
    }
}