using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class Resume_M
    {
        public static Resume_M ToViewModel(string uid,List<Resume> Resumes)
        {
            return new Resume_M()
            {
                Resumes= Resumes,
                uid=uid
            };
        }

        public string uid { get; set; }

        public List<Resume> Resumes { get; set; }
    }
}