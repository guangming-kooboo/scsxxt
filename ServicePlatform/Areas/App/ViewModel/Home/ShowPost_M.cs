using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class ShowPost_M
    {
        public string Ent_No { get; set; }
        public List<Job> Jobs { get; set; }
        public string Uid { get; set; }
        public static ShowPost_M ToViewModel(List<Job> jobs,string Ent_No,string uid)
        {
            return new ShowPost_M()
            {
                Jobs=jobs,
                Ent_No= Ent_No,
                Uid=uid
            };
        }
    }
}