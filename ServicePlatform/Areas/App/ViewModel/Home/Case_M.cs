using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class Case_M
    {
        public static Case_M ToViewModel(string uid)
        {
            return new Case_M()
            {
                Uid = uid
            };
        }
        public string Uid { get; set; }
    }
}