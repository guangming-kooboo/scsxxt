using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Model;

namespace ServicePlatform.Areas.AppTeacher2.ViewModel.THome
{
    public class StudentDetail_M
    {
        public static StudentDetail_M ToViewModel(Student StudentInfo)
        {
            return new StudentDetail_M()
            {
                _StudentInfo= StudentInfo
            };
        }

        public Student _StudentInfo { get; set; }
    }
}