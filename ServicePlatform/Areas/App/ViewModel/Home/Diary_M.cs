using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class Diary_M
    {
        public static Diary_M ToViewModel(string uid)
        {
            return new Diary_M()
            {
                Uid =uid
            };
        }
        public List<Diary> Diarys { get; set; }

        [Display(Name ="周记id")]
        public string Id;
        public string Uid { get; set; }
    }
}