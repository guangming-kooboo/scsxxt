using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Services.Entity;
using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class RecEnt_Des_M : BaseViewModel
    {
        public string Video { get; set; }

        public string Photos { get; set; }

        public string Uid { get; set; }

        public string PracticeNo { get; set; }
        public int VolunteerSequence { get; set; }
        public int PosSequence { get; set; }

        public static RecEnt_Des_M ToViewModel(string EntPracNo, int VolunteerSequence, int PosSequence,T_Enterprise enterprise/* List<T_DownLoadFiles> AdList*/)
        {
            return new RecEnt_Des_M()
            {
                //Id =enterprise.T_EntBatchReg.,
                EntPracNo = EntPracNo,
                Ent_Name = enterprise.Ent_Name,
                Email = enterprise.Email,
                EntAddress = enterprise.EntAddress,
                EntResume = enterprise.EntResume,
                phone=enterprise.Contectinfo,
                Video=enterprise.EntVideos,
                //AdList= AdList,
                Photos=enterprise.EntPhotos,
                VolunteerSequence= VolunteerSequence,
                PosSequence= PosSequence
                //  GetJobs = GetJobs
            };
        }
        //获取企业图片轮播图
        public List<T_DownLoadFiles> AdList { get; set; }


        public string EntPracNo { get; set; }
        public string Ent_Name { get; set; }

        [Display(Name = "企业地址")]
        public string EntAddress { get; set; }

        [Display(Name ="企业介绍")]
        public string EntResume { get; set; }
    
        [Display(Name = "企业邮箱")]
        public string Email { get; set; }
        [Display(Name ="电话")]
        public string phone { get; set; }
        private List<Job> GetJobs { get; set; }
        public List<Job> Jobs { get; set; }
        public List<Volunteer> Volunteers { get; set; }

    }
}