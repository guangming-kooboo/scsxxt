using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ServicePlatform.Areas.Platform.Models.Home
{
    public class Login
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public string UserPwd { get; set; }
        public string SchoolID { get; set; }
        public string Ent_No { get; set; }
        public List<SelectListItem> SchoolItems { get; set; }
        public List<SelectListItem> EntItems { get; set; }

        public string getUnitID()
        {
            if (string.IsNullOrEmpty(SchoolID)|| SchoolID=="-1")
            {
                if (string.IsNullOrEmpty(Ent_No) || Ent_No == "-1")
                {
                    return "Platform";
                }
                else
                {
                    return Ent_No;
                }
            }
            else
            {
                return SchoolID;
            }
               
        }
    }
}