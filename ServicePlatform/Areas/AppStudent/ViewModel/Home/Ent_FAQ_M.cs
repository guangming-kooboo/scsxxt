using ServicePlatform.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qx.Scsxxt.Core.Services;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class Ent_FAQ_M : BaseViewModel
    {
        public string Ent_No { get; set; }

        public static Ent_FAQ_M ToViewModel(string Ent_No, List<DownLoadFiles> DownLoadFiles)
        {
            return new Ent_FAQ_M() {
                DownLoadFiles = DownLoadFiles,
                Ent_No = Ent_No
            };
        }

        public List<DownLoadFiles> DownLoadFiles { get; set; }
    }
}