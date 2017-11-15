using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class Ent_Staff_M : BaseViewModel
    {
        public static Ent_Staff_M ToViewModel(string Ent_No, List<DownLoadFiles> DownLoadFiles)
        {
            return new Ent_Staff_M()
            {
                DownLoadFiles = DownLoadFiles,
               
                Ent_No = Ent_No
            };
        }

        public string Ent_No { get; set; }

        public List<DownLoadFiles> DownLoadFiles { get; set; }


    }
}