using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.AppTeacher2.ViewModel.THome
{
    public class MsgDetail_M
    {
        public static MsgDetail_M ToViewModel(string uid,string MsgID)
        {
            return new MsgDetail_M()
            {
                uid= uid,
                MsgID = MsgID
            };
        }

        public string uid { get; set; }

        public string MsgID { get; set; }
    }
}