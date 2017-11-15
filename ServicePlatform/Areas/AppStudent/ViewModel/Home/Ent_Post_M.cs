using System.Collections.Generic;
using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class Ent_Post_M : BaseViewModel
    {
        public string Ent_No { get; set; }
        public List<Job> Jobs { get; set; }

        public static Ent_Post_M ToViewModel(List<Job> jobs,string Ent_No)
        {
            return new Ent_Post_M()
            {
                Jobs=jobs,
                Ent_No= Ent_No,
            };
        }
    }
}