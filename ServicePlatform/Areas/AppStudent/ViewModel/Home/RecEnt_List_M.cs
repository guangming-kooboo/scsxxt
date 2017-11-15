using System.Collections.Generic;
using System.Web.Mvc;
using Core.Services.Entity;
using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class RecEnt_List_M : BaseViewModel
    {

        public string PracticeNo { get; set; }
        public string EntPracNo { get; set; }
        public int VolunteerSequence { get; set; }
        public int PosSequence { get; set; }
        public List<Grogshop> Grogshops { get; set; }
        public List<T_DownLoadFiles> PicList { get; set; }

        public static RecEnt_List_M ToViewModel(List<Grogshop> grogshops, string EntCategoryID,List<SelectListItem> EntCategory,/*List<T_DownLoadFiles> PicList,*/ int VolunteerSequence, int PosSequence, List<AllEnterprise> PositionCount)
        {
            return new RecEnt_List_M()
            {
                Grogshops = grogshops,
                EntCategory= EntCategory,
               // PicList= PicList,
                VolunteerSequence = VolunteerSequence,
                PosSequence= PosSequence,
                _PositionCount= PositionCount,
                EntCategoryID = EntCategoryID
            };
        }

        public string EntCategoryID { get; set; }

        public List<SelectListItem> EntCategory { get; set; }

        public List<AllEnterprise> _PositionCount { get; set; }
    }
}