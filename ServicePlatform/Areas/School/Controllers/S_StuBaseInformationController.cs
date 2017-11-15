using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.App_Start;
using ServicePlatform.Models;
using ServicePlatform.Lib;
using ServicePlatform.Areas.Permission.Models;
namespace ServicePlatform.Areas.School.Controllers
{
    public class S_StuBaseInformationController : Controller
    {
        private SchoolContext sc = new SchoolContext();
        private UserContext uc = new UserContext();
        private PermissionContext pc = new PermissionContext();

        public string CheckFileNameExist(HttpPostedFileBase[] Files,string checkStr,string ExstraStr="")
        {

            string warningStr = "";
            if (checkStr == null)
            {
                return warningStr;
            }
            for (int i = 0; i < Files.Count(); i++)
            {
                string temp = ExstraStr + Files[i].FileName;
                if (checkStr.Contains(temp))
                {
                    warningStr += Files[i].FileName + "    ";
                }
            }
            return warningStr;
        }

        //学生详情
        //[BaseActionFilter]
        public ActionResult BaseInfoMaintan(string userid, string ForbidFlag)
        {
            string recheck = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            var q_recheck = pc.UserRole.Where(a => a.UserID == recheck).Select(a => a.RoleID).ToList();
            if (!q_recheck.Contains("61"))
            {
                ForbidFlag = "true";
            }
            ViewBag.fobidList = (Session["Vars"] as ServicePlatform.Config.Vars).getButtons();
            if (userid==null)
            {
                userid=(Session["Vars"] as ServicePlatform.Config.Vars).UserID;
                ViewBag.ReplaceStr = userid + "-";
            }
           
            T_Student tu = sc.Student.Find(userid);
            if (tu != null)
            {
                string classno = tu.StuClass;
                var q_spename1 = (from f in sc.T_Class where f.ClassID == classno select f).ToList();
                string entryyear = q_spename1[0].EntryYear.ToString();
                ViewBag.Year = entryyear;
                string speno = q_spename1[0].SpeNo;
                int year = q_spename1[0].EntryYear;
                var q_spename = (from f in sc.C_Specialty where f.EntryYear == year && f.SpeNo == speno select f.SpeName).ToList();
                if (q_spename != null && q_spename.Count != 0)
                {
                    ViewBag.SpeName = q_spename[0].Trim();
                }
                //var q = (from f in sc.Student where f.StuClass == classno select f).ToList();

                var q_classname = (from f in sc.T_Class where f.ClassID == classno select f.ClassName).ToList();
                if (q_classname != null && q_classname.Count != 0)
                {
                    //string ss = q_classname[0];
                    ViewBag.ClassName = q_classname[0].Trim();
                }
                ViewBag.Sex = tu.StuSex;
                if (tu.MainPhoto == null)
                {
                    ViewBag.MainPhoto = "/Areas/School/Content/S_StuBaseInformation/img/defaultpic.png";
                }
                else
                {
                    ViewBag.MainPhoto = tu.MainPhoto;
                }

                List<T_StuPic> StuPicList = (from f in sc.T_StuPic where f.UserID == userid orderby f.Secquence ascending select f).ToList();
                ViewBag.ShowPics = StuPicList;
                //ViewBag.ShowPics = tu.Pics;
                ViewBag.ShowPicsMood = tu.PicMood;
                ViewBag.ShowVideos = tu.Videos;
                ViewBag.StuResume = tu.StuResourceFile;

                ViewBag.SchoolID = tu.SchoolID;
                ViewBag.ForbidFlag = ForbidFlag;
                return View(tu);
            }
            else
            {
                ViewBag.ForbidFlag = ForbidFlag;
                return Content("未找到该学生信息，可能他、她已被删除或数据出错");
            }



        }
        //导入新相片
        [BaseActionFilter]
        public ActionResult SaveStuHeadPic(HttpPostedFileBase Photo)
        {
            string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            T_Student tu = sc.Student.Find(userid);
            if (tu != null)
            {
                string old = tu.MainPhoto;
                if (old != null)
                {
                    Lib.FileOperate.DeleteFlie(old);
                }
            }
            if (Photo != null)//如果重新提交了照片
            {
                string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
                string url = "/UserFiles/School/" + schoolid + "/OtherFiles/StuHeadPic/";
                string result = Lib.FileOperate.UploadFile(Photo, url);
                tu.MainPhoto = result;

            }
            else//不更新照片
            {
                tu.MainPhoto = "/Areas/School/Content/S_StuBaseInformation/img/defaultpic.png";
            }
            sc.SaveChanges();
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        //上传精彩图集
        [BaseActionFilter]
        public ActionResult InprotBeatPics(HttpPostedFileBase[] Photo)
        {
            string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            if (Photo[0] != null)//如果重新提交了照片
            {


                //var q = (from f in sc.Student where f.UserID == userid select f.Pics).ToList();
                //string warningStr = CheckFileNameExist(Photo, q[0], userid + "-");
                //if (warningStr != "")
                //{
                //string uri = "/School/SubmitResults/Results?results=" + "下列文件的文件名已存在:" + warningStr;
                //return Redirect(uri);
                //}
                T_Student tu = sc.Student.Find(userid);
                string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
                string url = "/UserFiles/School/" + schoolid + "/OtherFiles/StuShowPic/";
                string result = Lib.FileOperate.UploadMultiFile(Photo, url, userid);

                string[] procee = result.Split(';');
                for (int i = 0; i < procee.Length; i++)
                {
                    T_StuPic temp = new T_StuPic();
                    temp.UserID = userid;
                    var q_getmax =
                    (from p in sc.T_StuPic
                     where p.UserID == userid
                     group p by p.UserID into g
                     select new
                     {
                         g.Key,
                         MaxInnerID = g.Max(p => p.InnerID)
                     }).ToList();
                    if (q_getmax.Count == 0)
                    {
                        temp.InnerID = 1;
                        temp.Secquence = 1;
                    }
                    else
                    {
                        temp.InnerID = q_getmax[0].MaxInnerID + 1;
                        temp.Secquence = q_getmax[0].MaxInnerID + 1;
                    }
                    temp.PicLink = procee[i];
                    temp.PicMood = "";
                    temp.PicName = Photo[i].FileName;

                    sc.T_StuPic.Add(temp);
                    try
                    {
                        sc.SaveChanges();
                    }
                    catch
                    {

                    }
                }
                try
                {
                    sc.SaveChanges();
                }
                catch { }
                //string PicMood = string.Empty;
                //string picmood = Request.Form["PicMood"];
                //string temppicmood = picmood;
                //if (picmood == "" || (picmood.Length - temppicmood.Replace(",", "").Length) != Photo.Count())
                //{
                //string uri = "/School/SubmitResults/Results?results=" + "描述的数量与照片数量不匹配，若不填写照片心情,可以填写等量的逗号！";
                //return Redirect(uri);
                //}
                //else
                //{
                //string[] process = picmood.Replace(",",";").Split(';');
                //for (int i = 0; i < process.Length-1;i++ )
                //{
                //if(process[i]=="")
                //{
                //PicMood += "无描述;";
                //}
                //else
                //{
                //PicMood += process[i]+";";
                //}
                //}
                //tu.PicMood = PicMood;
                //}

                //if (tu.Pics != null && tu.Pics != "")
                //{
                //    //tu.Pics = tu.Pics + ";" + result;
                //}
                //else
                //{
                //    //tu.Pics = result;
                //}

            }
            else//不更新照片
            {
                return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
            }
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        [BaseActionFilter]
        public ActionResult DeleteBeatPics(int innerid)
        {
            string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            //T_Student tu = sc.Student.Find(userid);
            T_StuPic tu = sc.T_StuPic.Find(userid, innerid);
            if (tu != null)
            {
                sc.T_StuPic.Remove(tu);
                sc.SaveChanges();
                //string temp = tu.Pics;
                //temp = temp.Replace(fileurl + ";", "");
                //if (temp==tu.Pics)
                //{
                //    temp = temp.Replace(";" + fileurl,"");
                //}
                //if (temp == tu.Pics)
                //{
                //    temp = temp.Replace(fileurl, "");
                //}
                //tu.Pics = temp;
                //Lib.FileOperate.DeleteFlie(fileurl);

                //string[] tempp = tu.PicMood.Split(';');
                //string PicMood = string.Empty;
                //for (int i = 0; i < tempp.Length;i++ )
                //{
                //    if(i!=picmood)
                //    {
                //        PicMood += tempp[i] + ";";
                //    }
                //}
                //tu.PicMood = PicMood;
                //sc.SaveChanges();                 
            }
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        [BaseActionFilter]
        public ActionResult AddPicMood(int innerid)
        {
            string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            //T_Student tu = sc.Student.Find(userid);
            T_StuPic tu = sc.T_StuPic.Find(userid, innerid);
            if (tu != null)
            {
                if(Request.Form["picmood"]!=null)
                {
                    tu.PicMood = Request.Form["picmood"];
                    sc.SaveChanges();            
                }              
            }
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        [BaseActionFilter]
        public ActionResult PicReorder(string type,int innerid)
        {
            string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            T_StuPic tu = sc.T_StuPic.Find(userid, innerid);
            if(type=="1")
            {
                var q = (from f in sc.T_StuPic where f.Secquence < tu.Secquence && f.UserID == userid orderby f.Secquence ascending select f).ToList();
                if(q.Count!=0)
                {
                    int order = tu.Secquence;
                    int order1 = q[q.Count - 1].Secquence;
                    T_StuPic berepalce = sc.T_StuPic.Find(userid, q[q.Count-1].InnerID);
                    tu.Secquence = order1;
                    berepalce.Secquence = order;
                    sc.SaveChanges();
                }
                else
                {
                    tu.Secquence += 1;
                    sc.SaveChanges();
                }
            }
            else
            {
                var q = (from f in sc.T_StuPic where f.Secquence > tu.Secquence && f.UserID == userid orderby f.Secquence ascending select f).ToList();
                if (q.Count != 0)
                {
                    int order = tu.Secquence;
                    int order1 = q[0].Secquence;
                    T_StuPic berepalce = sc.T_StuPic.Find(userid, q[0].InnerID);
                    if (berepalce!=null)
                    {
                        tu.Secquence = order1;
                        berepalce.Secquence = order;
                        sc.SaveChanges();
                    }
                }
                else
                {
                    tu.Secquence -= 1;
                    sc.SaveChanges();
                }
            }
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }


        public ActionResult ImportVedios(HttpPostedFileBase[] Video)
        {
            string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            if (Video[0] != null)
            {
                var q = (from f in sc.Student where f.UserID == userid select f.Videos).ToList();
                string warningStr = CheckFileNameExist(Video, q[0], userid + "-");
                if (warningStr != "")
                {
                    string uri = "/School/SubmitResults/Results?results=" + "下列文件的文件名已存在:" + warningStr;
                    return Redirect(uri);
                }
                T_Student tu = sc.Student.Find(userid);
                string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
                string url = "/UserFiles/School/" + schoolid + "/OtherFiles/StuShowVedios/";
                string result = Lib.FileOperate.UploadMultiFile(Video, url, userid);
                if (tu.Videos != null&&tu.Videos!="")
                {
                    tu.Videos = tu.Videos + ";" + result;
                }
                else
                {
                    tu.Videos = result;
                }
            }
            else//不更新照片
            {
                return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
            }
            sc.SaveChanges();
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        public ActionResult DeleteVedios(string fileurl)
        {
            string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            T_Student tu = sc.Student.Find(userid);
            if (tu != null)
            {
                string temp = tu.Videos;
                temp = temp.Replace(fileurl + ";", "");
                if (temp == tu.Videos)
                {
                    temp = temp.Replace(";" + fileurl, "");
                }
                if (temp == tu.Videos)
                {
                    temp = temp.Replace(fileurl, "");
                }
                tu.Videos = temp;
                Lib.FileOperate.DeleteFlie(fileurl);
                sc.SaveChanges();
            }
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        public ActionResult ImportResume(HttpPostedFileBase[] Resume)
        {
            string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;

            if (Resume[0] != null)
            {
                var q = (from f in sc.Student where f.UserID == userid select f.StuResourceFile).ToList();
                string warningStr = CheckFileNameExist(Resume, q[0], userid + "-");
                if (warningStr != "")
                {
                    string uri = "/School/SubmitResults/Results?results=" + "下列文件的文件名已存在:" + warningStr;
                    return Redirect(uri);
                }

                T_Student tu = sc.Student.Find(userid);
                string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
                string url = "/UserFiles/School/" + schoolid + "/OtherFiles/StuResume/";
                string result = Lib.FileOperate.UploadMultiFile(Resume, url, userid);
                if (tu.StuResourceFile != null&&tu.StuResourceFile!="")
                {
                    tu.StuResourceFile = tu.StuResourceFile + ";" + result;
                }
                else
                {
                    tu.StuResourceFile = result;
                }
            }
            else//不更新照片
            {
                return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
            }
            sc.SaveChanges();
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        public ActionResult DeleteResume(string fileurl)
        {
            string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            T_Student tu = sc.Student.Find(userid);
            if (tu != null)
            {
                string temp = tu.StuResourceFile;
                temp = temp.Replace(fileurl + ";", "");
                if (temp == tu.StuResourceFile)
                {
                    temp = temp.Replace(";" + fileurl, "");
                }
                if (temp == tu.StuResourceFile)
                {
                    temp = temp.Replace(fileurl, "");
                }
                tu.StuResourceFile = temp;
                Lib.FileOperate.DeleteFlie(fileurl);
                sc.SaveChanges();
            }
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        //保存学生详情
        public ActionResult SaveStuBaseInfo()
        {
            string userid = Request.Form["userid"];
            T_Student tu = sc.Student.Find(userid);
            if (tu != null)
            {
                string sex = Request.Form["SEX"];
                sex = sex.Substring(0, 1);
                string stdTel = Request.Form["stdTel"];
                string stdEmail = Request.Form["stdEmail"];
                string stdQQ = Request.Form["stdQQ"];
                string stdHeight = Request.Form["stdHeight"];
                string stdWeight = Request.Form["stdWeight"];
                string sdtAbstract = Request.Form["sdtAbstract"];
                tu.StuSex = sex;
                tu.StuCellphone = stdTel;
                tu.StuEMail = stdEmail;
                tu.StuQQ = stdQQ;
                tu.StuHeight = Convert.ToInt32(stdHeight);
                tu.StuWeight = Convert.ToInt32(stdWeight);
                tu.StuResume = sdtAbstract;
                sc.SaveChanges();
                return Content("<script>alert('信息保存成功');window.location.href = document.referrer;</script>");//返回上一页并刷新
            }
            else
            {
                return Content("<script>alert('信息保存失败');window.location.href = document.referrer;</script>");//返回上一页并刷新
            }
        }


    }
}
