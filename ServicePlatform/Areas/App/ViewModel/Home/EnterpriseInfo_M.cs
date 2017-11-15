using Core.Services.Entity;
using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class EnterpriseInfo_M
    {
        public string Uid { get; set; }

        public string PracticeNo { get; set; }

        public int VolunteerSequence { get; set; }
        public int PosSequence { get; set; }
        public List<Grogshop> Grogshops { get; set; }
        public List<T_DownLoadFiles> PicList { get; set; }

        public static EnterpriseInfo_M ToViewModel(List<Grogshop> grogshops, string EntCategoryID,List<SelectListItem> EntCategory,/*List<T_DownLoadFiles> PicList,*/ string uid, int VolunteerSequence, int PosSequence, List<AllEnterprise> PositionCount)
        {
            return new EnterpriseInfo_M()
            {
                Grogshops = grogshops,
                EntCategory= EntCategory,
               // PicList= PicList,
                VolunteerSequence = VolunteerSequence,
                PosSequence= PosSequence,
                Uid = uid,
                PositionCount= PositionCount,
                EntCategoryID = EntCategoryID
            };
        }

        public string EntCategoryID { get; set; }

        public List<SelectListItem> EntCategory { get; set; }

        public List<AllEnterprise> PositionCount { get; set; }
    }
}