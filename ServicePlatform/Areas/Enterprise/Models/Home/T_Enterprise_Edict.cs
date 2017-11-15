using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Services.Entity;


namespace ServicePlatform.Areas.Enterprise.Models.Home
{
    public class T_Enterprise_Edict
    {
       
        public T_Enterprise Enterprise { get; set; }
        public List<T_DownLoadFiles> LogoList { get; set; }
        public List<T_DownLoadFiles> AdList { get; set; }
        public List<T_DownLoadFiles> PicList { get; set; }
        public List<T_DownLoadFiles> VideoList { get; set; }
        public List<T_DownLoadFiles> PPTList { get; set; }
        public List<T_DownLoadFiles> ResourceList { get; set; }
        
    }
}