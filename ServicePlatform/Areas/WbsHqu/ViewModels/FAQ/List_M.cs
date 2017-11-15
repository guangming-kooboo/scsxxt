using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.WbsHqu.ViewModels.FAQ
{
    public class List_M
    {
        public static List_M ToViewModel(string FatherId)
        {
            return new List_M() { FatherId = FatherId};
        }

        public string FatherId { get; set ; }
    }
}