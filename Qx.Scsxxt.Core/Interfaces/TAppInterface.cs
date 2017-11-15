using Core.Model;
using Core.Services.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Core.Interfaces
{
    public  interface TAppInterface
    {
        T_Faculty TeacherInfo(string uid);
        string GetPracticeNo(string uid);
        string GetPracEntName(string PracticeNo);
        T_User FindTeacher(string uid);
        bool ForgetPsw(string uid, string psw);
        bool Login(string UserID, string Psw);
        List<SelectListItem> GetSchoolIItems();
        T_Enterprise EnterpriseDetail(string Ent_No);
        List<Enterprise> EntpriseList(string SchoolID, string EntCategoryID);
        List<SelectListItem> EntCategory();
        List<Enterprise> GetPositionCount();
        List<SelectListItem> GetEntryYear(string schoolid);
        List<SelectListItem> GetClass(int EntryYear);
        List<Position> PositionList(string Ent_No);
        List<DownLoadFiles> GetDownLoadFiles(string Ent_No, int DLFileColumnID);
        List<Student> StudentList(string classid);
        Student StudentInfo(string uid);
    }
}
