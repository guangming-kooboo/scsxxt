using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Models;
using System.Text;
using ServicePlatform.App_Start;
using ServicePlatform.Lib;
using ServicePlatform.Areas.Permission.Models;
using System.Data.Entity.Validation;
using Qx.Tools.Interfaces;
using Qx.Tools.Scsxxt.CommonExtendMethods;
using Qx.Wbs.Hqu.Entity;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.School.Controllers
{
    public class FacultyManagerController : BaseController
    {
        private SchoolContext sc = new SchoolContext();
        private UserContext uc = new UserContext();
        private EnterpriseContext ec = new EnterpriseContext();
        private CodeTableContext ctc = new CodeTableContext();
        private SchoolHelper SchoolHelper = new SchoolHelper();
        private IRepository<Staff> _staff;

        public FacultyManagerController(IRepository<Staff> staff)
        {
            _staff = staff;
        }

        //教师列表
        [BaseActionFilter]
        public ActionResult FacultyList()
        {
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            var q = (from f in sc.T_Faculty where f.SchoolID == schoolid select f).ToList();
            ViewBag.TeacherList = q;
            return View();
        }
        //添加教师
        [BaseActionFilter]
        [ValidateInput(false)]
        public ActionResult AddFaculty(string UID)
        {

            string userid = string.Empty;
            if (UID != null)
            {
                userid = UID;
            }
            else
            {
                userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            }
            T_Faculty tf = sc.T_Faculty.Find(userid);
            if (tf != null)
            {
                ViewBag.Focus = tf.FacProPos;
                List<SelectListItem> ProPositionList = SchoolHelper.GetProPositionList(tf.FacProPos);
                ViewData["ProPositionList"] = ProPositionList;
                ViewBag.TeaInfo = tf;
                ViewBag.SexFocus = tf.FacSex.Trim(); ;
            }
            else
            {
                List<SelectListItem> ProPositionList = SchoolHelper.GetProPositionList();
                ViewData["ProPositionList"] = ProPositionList;
            }
            return View();
        }
        //添加教师
        [BaseActionFilter]
        public ActionResult FacaultyAdd(HttpPostedFileBase fphoto)
        {
            string fno = Request.Form["fno"];
            string fname = Request.Form["fname"];
            string fphone = Request.Form["fphone"];
            string fpos = Request.Form["ProPositionList"].Trim();
            string femail = Request.Form["femail"];
            string fabstract = Request.Form["fabstract"];
            string fsex = Request.Form["fsex"].Trim();
            string fstatus = Request.Form["fstatus"];

            T_Faculty tf_new = new T_Faculty();
            //string userid  = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
                   
                string userid = schoolid + "-" + fno;
                var q = (from f in sc.T_Faculty where f.UserID == userid select f).ToList();
                if (q.Count != 0)//不为空则修改
                {
                    T_Faculty tf = q[0];
                    //tf.UserID = userid;
                    tf.EmailAddress = femail;
                    tf.FacAbstract = fabstract;
                    tf.FacName = fname;
                    tf.FacNo = fno;
                    tf.FacProPos = Convert.ToInt32(fpos);
                    tf.FacSex = fsex;
                    tf.FacStatus = Convert.ToInt32(fstatus);
                    tf.PhoneNo = fphone;
                    string old = tf.FacPhoto;
                    if (old != null)
                    {
                        Lib.FileOperate.DeleteFlie(old);
                    }
                    if (fphoto != null)//如果重新提交了照片
                    {

                        string url = "/UserFiles/School/" + schoolid + "/OtherFiles/FacPhotos/";
                        string result = Lib.FileOperate.UploadFile_OriName(fphoto, url, userid);
                        tf.FacPhoto = result;
                    }
                    else//不更新照片
                    {
                        tf.FacPhoto = "/Areas/School/Content/S_StuBaseInformation/img/defaultpic.png";
                    }
                    sc.SaveChanges();

                    var oldstaff = _staff.Find(schoolid + "-" + fno);
                    if (oldstaff != null)
                    {
                        Staff staff=new Staff();
                        staff.StaffID = oldstaff.StaffID;
                        staff.StaffName = fname;
                        _staff.Update(staff);
                    }

                    T_User tu = new T_User();
                    var uid = schoolid + "-" + fno;//用户ID
                    var olduser = uc.T_User.FirstOrDefault(a => a.UserID == uid);
                    if (olduser != null)
                    {
                        tu.NickName = fname;
                        tu.UserID = olduser.UserID;
                        tu.UserPwd = olduser.UserPwd;
                        tu.Note = olduser.Note;
                        tu.UserType = olduser.UserType;

                        ec.T_User.AddOrUpdate(tu);
                        ec.SaveChanges();
                    }
                    return Alert("修改成功", "/School/FacultyManager/FacultyList");
                 //   return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
                }
                else//否则添加
                {
                    
                        T_User tu = new T_User();
                        tu.UserID = schoolid + "-" + fno;//用户ID
                        tu.NickName = fname;//用户名
                        //tu.UserPwd = table.Rows[i][0].ToString();//密码
                        tu.UserPwd = ServicePlatform.Lib.EncryptString.NoneEncrypt(fno);//密文存储密码
                        tu.UserType = 52;//用户类型--学生
                        tu.Note = DateTime.Now.ToString() + "由" + (Session["Vars"] as ServicePlatform.Config.Vars).UserID + "添加";
                 
                      ec.T_User.AddOrUpdate(tu);
                        ec.SaveChanges();

                        //添加到工作量的员工表里面
                        Staff staff = new Staff();
                        staff.StaffID = schoolid + "-" + fno;
                        staff.StaffName = fname;
                        if (_staff.Find(fno) == null)
                        {
                            _staff.Add(staff);
                        }
                       
                    }
                  

                    tf_new.UserID = userid;
                    tf_new.EmailAddress = femail;
                    tf_new.FacAbstract = fabstract;
                    tf_new.FacName = fname;
                    tf_new.FacNo = fno;
                    tf_new.FacProPos = Convert.ToInt32(fpos);
                    tf_new.FacSex = fsex;
                    tf_new.FacStatus = Convert.ToInt32(fstatus);
                    tf_new.PhoneNo = fphone;
                    tf_new.SchoolID = schoolid;
                    if (fphoto != null)//如果重新提交了照片
                    {
                        //string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
                        string url = "/UserFiles/School/" + schoolid + "/OtherFiles/";
                        string result = Lib.FileOperate.UploadFile_OriName(fphoto, url, userid);
                        tf_new.FacPhoto = result;
                    }
                    else//不更新照片
                    {
                        tf_new.FacPhoto = "/Areas/School/Content/S_StuBaseInformation/img/defaultpic.png";
                    }
                    sc.T_Faculty.AddOrUpdate(tf_new);
             
                    sc.SaveChanges();
            
           
            return Alert("添加成功", "/School/FacultyManager/FacultyList?pageId=M23-3");//返回上一页并刷新
        }

        //删除教师
        [BaseActionFilter]
        public ActionResult Faculty_Delete(string UserID)
        {
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            T_Faculty ts = ec.T_Faculty.Find(UserID);
            if (ts != null)
            {
                ec.T_Faculty.Remove(ts);
                ec.SaveChanges();
            }
            var uid = DataContext.UserUnit + "-" + UserID;

            var user = uc.T_User.FirstOrDefault(o => o.UserID == uid);
            if (user != null)
            {
                uc.T_User.Remove(user);
                uc.SaveChanges();
            }
            var staffno = uid;
            if (_staff.Find(staffno) != null)
            {
                _staff.Delete(staffno);
            }

            return Redirect("/School/FacultyManager/FacultyList");
        }

        //学校详情
        [BaseActionFilter]
        public ActionResult SchoolBaseInfo()
        {
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            var q = (from f in sc.School where f.SchoolID == schoolid select f).ToList();
            ViewBag.SchoolInfo = q[0];
            return View();
        }

        //提交修改
        [BaseActionFilter]
        [HttpPost]
        public ActionResult ModifySchoolBaseInfo(HttpPostedFileBase slogo)
        {
            string sadd = Request.Form["sadd"];
            string sphone = Request.Form["sphone"];
            string semail = Request.Form["semail"];
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;        
            var q = (from f in sc.School where f.SchoolID == schoolid select f).ToList();
            if (q.Count != 0)//不为空则修改
            {
                T_School ts = q[0];
                ts.Address = sadd;
                ts.Contact = sphone;
                ts.Email = semail;
                sc.SaveChanges();
                return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
            }
            else
            {
                return Redirect("/School/FacultyManager/SchoolBaseInfo");
            }
        }

        [HttpPost]
        public ActionResult SchoolLogoUpload()
        {
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            string FilePath = "/UserFiles/School/" + schoolid + "/BasicInfo/";
            var flist = Request.Files;
            var q = (from f in sc.School where f.SchoolID == schoolid select f).ToList();
            if (q.Count != 0 && flist!=null)//不为空则修改
            {
                T_School ts = q[0];
                string old = ts.SchoolLogo;
                if (old != null)
                {
                    Lib.FileOperate.DeleteFlie(old);
                }
                var c = flist[0];
                if (c != null)//如果重新提交了照片
                {
                    string url = "/UserFiles/School/" + schoolid + "/BasicInfo/";
                    string result = Lib.FileOperate.UploadFile_OriName(c, url, schoolid);
                    ts.SchoolLogo = result;
                }
                else//不更新照片
                {
                    //ts.SchoolLogo = "/Areas/School/Content/S_StuBaseInformation/img/defaultpic.png";
                }
                sc.SaveChanges();
                return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
            }
            else
            {
                return Redirect("/School/FacultyManager/SchoolBaseInfo");
            }
        }


    }
}

