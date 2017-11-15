
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Services.Entity;
using Qx.Scsxxt.Core.Services;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class IndexofDownLoadFiles_M
    {
        public static IndexofDownLoadFiles_M ToViewModel(string uid,string Ent_No,List<DownLoadFiles> DownLoadFiles)
        {
            return new IndexofDownLoadFiles_M()
            {
                DownLoadFiles= DownLoadFiles,
                uid= uid,
                Ent_No= Ent_No
            };
        }

        public string Ent_No { get; set; }

        public List<DownLoadFiles> DownLoadFiles { get; set; }

        public string uid { get; set; }

      
    }
}