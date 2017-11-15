using ServicePlatform.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qx.Scsxxt.Core.Services;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class RecEnt_FAQ_M : BaseViewModel
    {
        public static RecEnt_FAQ_M ToViewModel(string Ent_No,string EntPracNo, int VolunteerSequence, int PosSequence, List<DownLoadFiles> DownLoadFiles)
        {
            return new RecEnt_FAQ_M()
            {
                Ent_No= Ent_No,
                EntPracNo = EntPracNo,
                PosSequence= PosSequence,
                VolunteerSequence= VolunteerSequence,
                DownLoadFiles = DownLoadFiles,
            };
        }

        public List<DownLoadFiles> DownLoadFiles { get; set; }

        public int PosSequence { get; set; }

        public int VolunteerSequence { get; set; }

        public string EntPracNo { get; set; }
        public string Ent_No { get;  set; }
    }
}