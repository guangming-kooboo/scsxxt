using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CPQ.ViewModels.Admin
{
    public class Index_M
    {
        public static Index_M ToViewModel(string ownerID)
        {
            var model = new Index_M();           
            model.ownerID = ownerID;
            return model;

        }

        public string ownerID;

    }
}