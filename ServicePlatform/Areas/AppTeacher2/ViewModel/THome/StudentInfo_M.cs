using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.AppTeacher2.ViewModel.THome
{
    public class StudentInfo_M
    {
        public  List<SelectListItem> _EntryYear;
        public string  gradeId;
        public string majorId;
        public string classId;
        public string _schoolid;

        public static StudentInfo_M ToViewModel(List<SelectListItem> EntryYear,string schoolid,string uid)
        {
            return new StudentInfo_M()
            {
                uid= uid,
                _EntryYear = EntryYear,
                _schoolid= schoolid
            };
        }

        public string uid { get; set; }
    }
}