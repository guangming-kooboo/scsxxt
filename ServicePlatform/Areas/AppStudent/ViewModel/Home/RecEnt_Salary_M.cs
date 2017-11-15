using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class RecEnt_Salary_M : BaseViewModel
    {
        public static RecEnt_Salary_M ToViewModel( string EntPracNo, int VolunteerSequence, int PosSequence, List<DownLoadFiles> DownLoadFiles)
        {
            return new RecEnt_Salary_M()
            {
                DownLoadFiles = DownLoadFiles,
                EntPracNo = EntPracNo,
                VolunteerSequence = VolunteerSequence,
                PosSequence = PosSequence
            };
        }

        public int PosSequence { get; set; }

        public int VolunteerSequence { get; set; }

        public string EntPracNo { get; set; }

        public List<DownLoadFiles> DownLoadFiles { get; set; }

        public string uid { get; set; }

    }
}