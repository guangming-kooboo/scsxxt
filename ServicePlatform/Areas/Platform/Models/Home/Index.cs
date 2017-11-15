using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Services.Entity;


namespace ServicePlatform.Areas.Platform.Models.Home
{
    public class Index
    {
        public List<T_School> SchoolList { get; set; }
        public List<T_Enterprise> EnterpriseList { get; set; }
        public List<T_HomePageContent> PlateInfoList801 { get; set; }
        public List<T_HomePageContent> PlateInfoList802 { get; set; }
        public List<T_HomePageContent> PlateInfoList803 { get; set; }
        public List<T_HomePageContent> PlateInfoList804 { get; set; }
        public List<T_HomePageContent> PlateInfoList805 { get; set; }
    }
}