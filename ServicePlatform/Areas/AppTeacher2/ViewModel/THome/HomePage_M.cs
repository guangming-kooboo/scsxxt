using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Services.Entity;

namespace ServicePlatform.Areas.AppTeacher2.ViewModel.THome
{
    public class HomePage_M
    {
        public static HomePage_M ToViewModel(T_Faculty TeacherInfo)
        {
            return new HomePage_M()
            {
                _TeacherInfo = TeacherInfo
            };
        }

        public T_Faculty _TeacherInfo { get; set; }
    }
}