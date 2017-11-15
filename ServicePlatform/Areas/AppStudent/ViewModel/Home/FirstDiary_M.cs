using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class FirstDiary_M : BaseViewModel
    {
        public static FirstDiary_M ToViewModel()
        {
            return new FirstDiary_M()
            {
               
            };
        }
        public List<Diary> Diarys { get; set; }

        [Display(Name ="周记id")]
        public string Id;
        
    }
}