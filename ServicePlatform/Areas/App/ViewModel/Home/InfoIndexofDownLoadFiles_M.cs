using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class InfoIndexofDownLoadFiles_M
    {
        public static InfoIndexofDownLoadFiles_M ToViewModel(string uid, string EntPracNo, int VolunteerSequence, int PosSequence, List<DownLoadFiles> DownLoadFiles)
        {
            return new InfoIndexofDownLoadFiles_M()
            {
                DownLoadFiles = DownLoadFiles,
                uid = uid,
                EntPracNo = EntPracNo,
                VolunteerSequence= VolunteerSequence,
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