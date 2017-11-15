using System.Collections.Generic;
using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class Resume_M : BaseViewModel
    {
        public static Resume_M ToViewModel(List<Resume> Resumes)
        {
            return new Resume_M()
            {
                Resumes= Resumes,
         
            };
        }



        public List<Resume> Resumes { get; set; }
    }
}