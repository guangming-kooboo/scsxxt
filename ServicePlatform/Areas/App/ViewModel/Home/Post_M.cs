using Core.Services.Entity;
using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.App.ViewModel.Home
{//岗位
    public class Post_M
    { 
        public static Post_M ToViewModel(List<Job> jobs, string uid, string EntPracNo, int VolunteerSequence, int PosSequence)
        {
            return new Post_M()
            {
                Uid= uid,
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