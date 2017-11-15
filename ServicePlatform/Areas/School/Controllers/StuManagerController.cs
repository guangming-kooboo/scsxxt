using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
using Qx.Tools.CommonExtendMethods;
using Qx.Tools.Scsxxt;
using ServicePlatform.Controllers.Base;
using Core.Services;
using Core.Services.Entity;
using T_StuBatchReg = ServicePlatform.Models.T_StuBatchReg;
using T_Student = ServicePlatform.Models.T_Student;
using T_User = ServicePlatform.Models.T_User;
using T_UserRole = ServicePlatform.Areas.Permission.Models.T_UserRole;

namespace ServicePlatform.Areas.School.Controllers
{
    public class StuManagerController : BaseController
    {
        private SchoolContext sc = new SchoolContext();
        private UserContext uc = new UserContext();
        private SchoolHelper SchoolHelper = new SchoolHelper();
        private Repository _repository;
        private PermissionContext pc = new PermissionContext();

        public StuManagerController()
        {
            _repository = new Repository();
        }

        //学生信息维护
        [BaseActionFilter]
        public ActionResult StuImprot(string schoolid)
        {
            schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            var q0 = (from f in sc.School where f.SchoolID == schoolid select f.SchoolName).ToList();
            ViewBag.SchoolName = q0[0].ToString();
            ViewBag.SchoolID = schoolid;


            //获取年级
            List<SelectListItem> EntryYear = SchoolHelper.GetEntryYearList(schoolid);
            ViewData["EntryYear"] = EntryYear;

            //获取专业
            List<SelectListItem> Specialty = SchoolHelper.GetSpecialtyList(schoolid);
            ViewData["Specialty"] = Specialty;

            //获取班级
            List<SelectListItem> Class = SchoolHelper.GetClassList(schoolid);
            ViewData["Class"] = Class;

            return View();
        }

        [BaseActionFilter]
        public ActionResult StuRegToBatch(string classno, string speno, string schoolid)
        {
            schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            if (classno == null)
            {
                classno = Request.Form["classno"];
            }
            if(speno==null)
            {
                speno = Request.Form["speno"];
            }
            var q_spename1 = (from f in sc.T_Class where f.ClassID == classno select f).ToList();

            if (q_spename1 != null && q_spename1.Count != 0)
            {
                ViewBag.SpeName = q_spename1[0].SpeName.Trim();
            }
            var q_classname = (from f in sc.T_Class where f.ClassID == classno select f.ClassName).ToList();
            if (q_classname != null && q_classname.Count != 0)
            {
                //string ss = q_classname[0];
                ViewBag.ClassName = q_classname[0].Trim();
            }
            ViewBag.ClassNo = classno;
            ViewBag.Speno = speno;
            ViewBag.Schoolid = schoolid;
            var q_batch = from f in sc.PracBatch where f.SchoolID == schoolid select f;
            ViewData["BatchName"] = new SelectList(q_batch, "PracBatchID", "BatchName");

            var q = (from f in sc.Student where f.StuClass == classno select f).ToList();
            string []belong=new string[q.Count];
            for (int i = 0; i < q.Count;i++ )
            {
                string userid=q[i].UserID;
                var q_pici=(from f in sc.StuBatchReg where f.UserID==userid select f).ToList();
                if (q_pici != null && q_pici.Count != 0)
                {
                    string baic = string.Empty;
                    for (int j = 0; j < q_pici.Count; j++)
                    {
                        baic = q_pici[j].PracBatchID;
                        var q_picim = (from f in sc.PracBatch where f.PracBatchID == baic select f.BatchName).ToList();
                        if (q_picim.Count==0)
                        {
                            belong[i] += "该学生的批次信息有误，请检查！";
                        }
                        else
                        {
                            belong[i] += q_picim[0] + " || ";
                        }
                        
                    }
                }
                else
                {
                    belong[i] = "无";
                }
            }
            ViewBag.Belong = belong;
            ViewBag.NowBacth = SchoolHelper.GetCurrentBatch(schoolid);
            return View(q);
        }

        //批量注册当前批次
        [BaseActionFilter]
        public ActionResult RegStuToBatch()
        {
            string sid = Request.Form["schoolid"];
            string classno = Request.Form["classno"];
            string speno = Request.Form["speno"];
            string url = "/School/StuManager/StuRegToBatch?classno=" + classno + "&speno=" + speno + "&schoolid=" + sid;
            //string msg=Request.QueryString["msg"];
            string stuno = Request.Form["piliang"];
            if(stuno=="")
            {
                return Redirect(url);
                //return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
            }
            string Error = "";
            string batchid = Request.Form["batch"];
            stuno = stuno.Replace(" ", "");
            stuno = stuno.Replace("\r", "");
            stuno = stuno.Replace("\n", "");
            string[] temp = stuno.Split('!');
            for (int i = 0; i < temp.Length - 1; i++)
            {
                string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
                string userid = schoolid + "-" + temp[i];
                T_StuBatchReg sbr = new T_StuBatchReg();
                sbr.UserID = userid;
                sbr.PracBatchID = batchid;
                sbr.PracticeNo = userid + batchid;
                sbr.CareerStatusID = 10;
              
                    sc.StuBatchReg.Add(sbr);

                    T_UserRole tu = new T_UserRole();
                    tu.UserID = userid;
                    tu.RoleID = "61";
                    pc.UserRole.AddOrUpdate(tu);

                    T_UserRole tu1 = new T_UserRole();
                    tu1.UserID = userid;
                    tu1.RoleID = "C1";
                    pc.UserRole.AddOrUpdate(tu1);

                    pc.SaveChanges();
                    sc.SaveChanges();

             

             

            }

            try
            {
                
            }
            catch
            {
                Error += "导入失败，可能原因为：待注册的学生的批次已存在";
            }
          

            if (Error!="")
            {
                return Redirect(url);
                //Error += "上述学号的学生导入批次失败！";
                //return Content("<script>alert('" + Error + "');window.location.href = document.referrer;</script>");//返回上一页并刷新
            }
            return Redirect(url);
            //return Content("<script>alert('成功将选定学生导入选定批次');window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        //单个加入当前批次
        [BaseActionFilter]
        public ActionResult AddCurBatch(string userid, string batchid, string classno, string speno)
        {
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            string url="/School/StuManager/StuRegToBatch?classno="+classno+"&speno="+speno+"&schoolid="+schoolid;
            var q = (from f in sc.StuBatchReg where f.UserID == userid && f.PracBatchID == batchid select f).FirstOrDefault();
            if(q!=null)
            {
                return Content("<script>alert('请勿重复添加');window.location = '" + url + "';</script>");//返回上一页并刷新
            }
            if (userid != null && batchid!=null)
            {
                //string batchid = Request.Form["batch"];
                T_StuBatchReg sbr = new T_StuBatchReg();
                sbr.UserID = userid;
                sbr.PracBatchID = batchid;
                sbr.PracticeNo = userid + batchid;
                sbr.CareerStatusID = 10;

                var check = (from f in pc.UserRole where f.UserID == userid select f).FirstOrDefault();
                if (check == null)
                {
                    T_UserRole tu = new T_UserRole();
                    tu.UserID = userid;
                    tu.RoleID = "61";
                    pc.UserRole.Add(tu);

                    T_UserRole tu1 = new T_UserRole();
                    tu1.UserID = userid;
                    tu1.RoleID = "C1";
                    pc.UserRole.Add(tu1);
                    pc.UserRole.Add(tu);
                    pc.SaveChanges();
                }

                try
                {
                    sc.StuBatchReg.Add(sbr);
                    sc.SaveChanges();
                }
                catch
                {

                }

                return Redirect(url);
                //return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
            }
            else
            {
                return Redirect(url);
                //return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
            }
        }

        //单个移除当前批次
        //[BaseActionFilter]
        public ActionResult RemoveCurBatch(string userid, string batchid, string classno, string speno)
        {        
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            string url = "/School/StuManager/StuRegToBatch?classno=" + classno + "&speno=" + speno + "&schoolid=" + schoolid;
            string currentbacthic = SchoolHelper.GetCurrentBatch(schoolid);
            if (batchid != currentbacthic)
            {
                //return RedirectToAction("RegStuToBatch", new { msg = "aaaa" });
                return Content("<script>alert('非当前批次无法移除');window.location='" + url + "';</script>");//返回上一页并刷新
            }
            else
            {
                string pracbatchID = userid + batchid;
                T_StuBatchReg sbr = new T_StuBatchReg();

                T_UserRole tu = pc.UserRole.Find(userid, UserRoleConst.CurrentStudent);

                T_UserRole tu1 = pc.UserRole.Find(userid, UserRoleConst.CommonUser);

                var q = (from f in sc.StuBatchReg where f.UserID == userid && f.PracticeNo == pracbatchID select f).FirstOrDefault();
                if (q != null)
                {
                    try
                    {
                        sbr = q;
                        sc.StuBatchReg.Remove(sbr);
                        sc.SaveChanges();
                        if (tu != null && tu1 != null)
                        {
                            pc.UserRole.Remove(tu);
                            pc.UserRole.Remove(tu1);
                            pc.SaveChanges();
                        }
                        return Content("<script>alert('移除成功');window.location='" + url + "'</script>");//返回上一页并刷新
                    }
                    catch
                    {
                        return Content("<script>alert('移除失败，该学生已经进行相关操作，与之关联的数据项不为空！');window.location='" + url + "'</script>");//返回上一页并刷新
                    }           
                }
                else
                {
                    return Content("<script>alert('移除失败，该学生的数据异常');window.location='" + url + "';</script>");//返回上一页并刷新
                }
            }    
        }

        //已导入的学生列表
        [BaseActionFilter]
        public ActionResult ImportedStuList(string classno,string speno)
        {

            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
             int entryyear = sc.T_Class.FirstOrDefault(a => a.ClassID == classno).EntryYear;
            int year = entryyear;
            var q_spename = (from f in sc.C_Specialty where f.EntryYear == year && f.SpeNo == speno select f.SpeName).ToList();
            if (q_spename != null && q_spename.Count != 0)
            {
                ViewBag.SpeName = q_spename[0].Trim();
            }
            var q = (from f in sc.Student where f.StuClass == classno select f).ToList();

            var q_classname = (from f in sc.T_Class where f.ClassID == classno select f.ClassName).ToList();
            if (q_classname != null && q_classname.Count != 0)
            {
                //string ss = q_classname[0];
                ViewBag.ClassName = q_classname[0].Trim();
            }
            ViewBag.Schoolid = schoolid;
            ViewBag.ClassNo = classno;
            ViewBag.SpeNo = speno;
            return View(q);

        }


        public ActionResult AddSignerStu(string classid,string speno)
        {
            ViewData["classid"] = classid;
            ViewData["speno"] = speno;
            return View();
        }
        [HttpPost]
        public ActionResult AddSignerStu(string classid,string stuno, string stuname,string speno, string stucellphone)
        {
          //  DataContext dataContext=new DataContext();
            if (DataContext.UserUnit == null)
            {
                return Content("<script>alert('登录超时');window.location.href='/Platform/Home/Login';</script>");
            }
            else
            {
                var uid = DataContext.UserUnit + "-" + stuno;
                var pwd = Lib.EncryptString.NoneEncrypt(stuno);
                var user = uc.T_User.FirstOrDefault(o => o.UserID == uid);
                var stu = sc.Student.FirstOrDefault(a => a.UserID == uid);
                if (user == null && stu == null)
                {
                    //用户表
                    _repository.T_User.AddOrUpdate(new Core.Services.Entity.T_User()
                    {
                        UserID = uid,
                        UserPwd = pwd,
                        UserType = 12
                    });
                   //学生表
                    _repository.T_Student.AddOrUpdate(new Core.Services.Entity.T_Student()
                    {
                        StuNo = stuno,
                        StuName = stuname,
                        StuClass = classid,
                        StuCellphone = stucellphone,
                        UserID = uid,
                        StuHeight = 0,
                        StuWeight = 0
                    
                    });
                    //权限表
                    _repository.T_UserRole.AddOrUpdate(new Core.Services.Entity.T_UserRole()
                    {
                        RoleID = "81",
                     
                        UserID = uid
                    });
                    _repository.T_UserRole.AddOrUpdate(new Core.Services.Entity.T_UserRole()
                    {
                        RoleID = "C1",

                        UserID = uid
                    });


                    _repository.SaveChanges();
                    return Content("<script>alert('添加成功');window.location.href='/School/StuManager/ImportedStuList?classno=" + classid + "&speno=" + speno + "';</script>");
                }
                else
                {
                    return Content("<script>alert('已经添加了该学生，请重新添加');history.go(-1)</script>");
                }
            }
        }
        //学生密码重置
        [BaseActionFilter]
        public ActionResult StuPwdReset()
        {
            return View();
        }

        //重置学生教师密码
        [BaseActionFilter]
        public ActionResult ResetStuPwd()
        {
            string stuno = Request.Form["stuno"];
            string newpwd = Request.Form["newpwd"];
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            string user = schoolid + "-"+stuno;
            T_User tu = uc.T_User.Find(user);
            if(tu!=null)
            {
                if(newpwd!="")
                {
                    tu.UserPwd = ServicePlatform.Lib.EncryptString.NoneEncrypt(newpwd);//密文存储密码
                }
                else
                {
                    tu.UserPwd = ServicePlatform.Lib.EncryptString.NoneEncrypt(stuno);//密文存储密码;
                    
                }
                uc.Entry(tu).State=EntityState.Modified;
                uc.SaveChanges();
            }
            else
            {
                return Content("<script>alert('未找到该学生/职工信息');window.location.href = document.referrer;</script>");//返回上一页并刷新
            }
            return Content("<script>alert('成功重置该学生/职工密码');window.location.href = document.referrer;</script>");//返回上一页并刷新
        }


        //查看学生详情
        [BaseActionFilter]
        public ActionResult ViewDetail(string userid)
        {
            string url = "/School/S_StuBaseInformation/BaseInfoMaintan?userid=" + userid;
            return Redirect(url);
        }

        //编辑学生信息
        [BaseActionFilter]
        public ActionResult ModifyImportedStuInfo(string stuno,string stuname, string classid,string userid)
        {
            string entryyear = classid.Substring(0, 4);
            int year = Convert.ToInt32(entryyear);
            string speno = classid.Substring(4, classid.Length - 6);

            var q = (from f in sc.T_Class where f.SpeNo == speno && f.EntryYear == year select f).ToList();

            List<SelectListItem> Class = new List<SelectListItem>();
            for (int i = 0; i < q.Count;i++ )
            {
                if (q[i].ClassID == classid)
                {
                    Class.Add(new SelectListItem() { Text = q[i].ClassName, Value = q[i].ClassID, Selected = true });
                }
                else
                {
                    Class.Add(new SelectListItem() { Text = q[i].ClassName, Value = q[i].ClassID, Selected = false });
                }
            }
            ViewData["Class"] = Class;
            ViewBag.Stuno = stuno;
            ViewBag.StuName = stuname;
            ViewBag.UserID = userid;
            return View();
        }

        //保存编辑的学生信息
        [BaseActionFilter]
        public ActionResult ModifyStuInfo()
        {
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            string userid = Request.Form["userid"];
            string classno = Request.Form["classno"];
            string stuname = Request.Form["stuname"];
            string stuno = Request.Form["stuno"];
            T_User tuu = uc.T_User.Find(userid);
            if (tuu != null)
            {
                tuu.UserID = schoolid + stuno;
                tuu.UserPwd = stuno;
                tuu.NickName = stuname;
                uc.SaveChanges();
            }
            else
            {
                return Content("<script>alert('学生信息修改失败');window.location.href = document.referrer;</script>");//返回上一页并刷新
            }
            T_Student tu = sc.Student.Find(userid);
            if(tu!=null)
            {
                tu.StuNo = stuno;
                tu.UserID = schoolid + stuno;
                tu.StuClass = classno;
                tu.StuName = stuname;
                sc.SaveChanges();
            }
            else
            {
                return Content("<script>alert('学生信息修改失败');window.location.href = document.referrer;</script>");//返回上一页并刷新
            }
            return Content("<script>alert('学生信息修改成功');window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        //删除已经导入的学生信息
        [BaseActionFilter]
        public ActionResult DeleteImportedStu(string userid,string stuno, string classno)
        {
            try
            {
                var q = (from f in sc.Student where f.StuClass == classno && f.StuNo == stuno select f).ToList();
                if (q.Count != 0)
                {
                    T_Student ts = q[0];
                    sc.Student.Remove(ts);
                    sc.SaveChanges();

                    var qq = (from f in uc.T_User where f.UserID == userid select f).ToList();
                    if (qq.Count != 0 && qq != null)
                    {
                        T_User tu = qq[0];
                        uc.T_User.Remove(tu);
                        uc.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                return Alert("该生已参加实习，不能删除");
            }
         
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        ////批量删除
        [BaseActionFilterAttribute]
        public ActionResult DeleteALLImportedStu()
        {
            string userid = (Session["vars"] as ServicePlatform.Config.Vars).UserID;
            string idstring = Request.Form["piliang"];
            string classno = Request.Form["classno_dall"];
            idstring = idstring.Replace(" ", "");
            string[] temp = idstring.Split('!');
            foreach(var stuno in temp)
            {
                try
                {
                    var q = (from f in sc.Student where f.StuClass == classno && f.StuNo == stuno select f).ToList();
                    if (q.Count != 0)
                    {
                        T_Student ts = q[0];
                        string temp_userid = q[0].UserID;
                        sc.Student.Remove(ts);
                        sc.SaveChanges();

                        var qq = (from f in uc.T_User where f.UserID == temp_userid select f).ToList();
                        if (qq.Count != 0 && qq != null)
                        {
                            T_User tu = qq[0];
                            uc.T_User.Remove(tu);
                            uc.SaveChanges();
                        }
                    }
                }
                catch (Exception)
                {
                    return Alert("部分学生已参加实习，请移除其所参与的批次，否则不能删除");
                }             
            }
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }


        //Excel导入学生信息
        [HttpPost]
        [BaseActionFilter]  
        public ActionResult StudentImprot(HttpPostedFileBase filebase)
        {
            string return_msg = string.Empty;//记录处理结果
            string err_msg = string.Empty;//记录出错
            HttpPostedFileBase file = Request.Files["files"];
            string FileName;
            string savePath;
            if (file == null || file.ContentLength <= 0)
            {
                ViewBag.error = "文件不能为空";
                return Content("<script>alert('文件不能为空');window.history.go(-1);</script>");//返回上一页并刷新
            }
            else
            {
                #region 文件检查，创建临时文件
                string filename = Path.GetFileName(file.FileName);
                int filesize = file.ContentLength;//获取上传文件的大小单位为字节byte
                string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
                int Maxsize = 20000 * 1024;//定义上传文件的最大空间大小为4M
                string FileType = ".xls,.xlsx";//定义上传文件的类型字符串

                //FileName = NoFileName + DateTime.Now.ToString("YYYY-MM-DD hh:mm:ss") + fileEx;
                FileName = (Session["Vars"] as ServicePlatform.Config.Vars).UserID + "-" + NoFileName + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second  + fileEx;
                if (!FileType.Contains(fileEx))
                {
                    ViewBag.error = "文件类型不对，只能导入xls和xlsx格式的文件";
                    return Content("<script>alert('文件类型不对，只能导入xls和xlsx格式的文件');window.history.go(-1);</script>");//返回上一页并刷新
                }
                if (filesize >= Maxsize)
                {
                    ViewBag.error = "上传文件超过20M，不能上传";
                    return Content("<script>alert('上传文件超过20M，不能上传');window.history.go(-1);</script>");//返回上一页并刷新
                }
                string SchoolID = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
                string path = AppDomain.CurrentDomain.BaseDirectory + "UserFiles/School/" + SchoolID + "/";
                savePath = Path.Combine(path, FileName);
                file.SaveAs(savePath);

                #endregion
            }

            #region 文件读写，获取datatable
            string result = string.Empty;
            string strConn;
            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + savePath + ";" + "Extended Properties=Excel 12.0";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            OleDbDataAdapter myCommand = new OleDbDataAdapter("select * from [Sheet1$]", strConn);
            DataSet myDataSet = new DataSet();
            try
            {
                myCommand.Fill(myDataSet, "ExcelInfo");
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
            DataTable table = myDataSet.Tables["ExcelInfo"].DefaultView.ToTable();
            #endregion

            //获取学校班级专业等信息
            string schoolid = Request["sid"];
            string specility = Request["Specialty"];
            string classid=Request["Class"];

            //引用事务机制，出错时，事物回滚
            int Count_Suc = 0;
            int Count_Cmp = 0;
            string errorstu=string.Empty;
            bool addflag = true;
            using (UserContext uc_temp = new UserContext())
            {
                
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    //在用户表添加学生信息
                    T_User tu = new T_User();
                    tu.UserID = schoolid + "-" + table.Rows[i][0].ToString();//用户ID
                    tu.NickName = table.Rows[i][1].ToString();//用户名
                    //tu.UserPwd = table.Rows[i][0].ToString();//密码
                    tu.UserPwd = ServicePlatform.Lib.EncryptString.NoneEncrypt(table.Rows[i][0].ToString());//密文存储密码
                    tu.UserType = 12;//用户类型--学生
                    try
                    {
                        uc.T_User.Add(tu);
                        uc.SaveChanges();


                        //添加分散实习角色
                        _repository.T_UserRole.AddOrUpdate(new Core.Services.Entity.T_UserRole()
                        {
                            RoleID = "81",

                            UserID = tu.UserID
                        });
                        _repository.SaveChanges();
                        Count_Suc++;
                    }
                    catch(Exception e)
                    {
                        //err_msg = e.Message.ToString();
                        err_msg = e.InnerException.InnerException.Message.ToString().Replace("\r\n","");
                        if (addflag == true)
                        {
                            errorstu += table.Rows[i][0].ToString() + "," + table.Rows[i][1].ToString();
                            addflag = false;
                        }
                        continue;
                        
                    }
                    //在学生表添加学生信息
                    if(!table.Columns.Contains("学号"))
                    {
                        table.Columns.Add("学号");
                    }
                    if (!table.Columns.Contains("姓名"))
                    {
                        table.Columns.Add("姓名");
                    }
                    if (!table.Columns.Contains("电话"))
                    {
                        table.Columns.Add("电话");
                    }

                    T_Student ts = new T_Student();
                    ts.StuNo = table.Rows[i]["学号"] == null ? "无" : table.Rows[i][0].ToString();//学号
                    ts.StuName = table.Rows[i]["姓名"] == null ? "无" : table.Rows[i][1].ToString();//姓名
                    ts.StuCellphone = table.Rows[i]["电话"] == null ? "无" : table.Rows[i][2].ToString();//电话
                    ts.StuClass = classid;//班级号
                    ts.UserID = schoolid + "-" + table.Rows[i][0].ToString();//用户ID
                    try
                    {
                        sc.Student.Add(ts);
                        sc.SaveChanges();
                        Count_Cmp++;
                        continue;
                    }
                    catch
                    {
                        string userid = schoolid + "-" + table.Rows[i][0].ToString();
                        T_User tu_rollback = uc.T_User.Find(userid);
                        uc.T_User.Remove(tu_rollback);
                        uc.SaveChanges();
                        Count_Suc--;
                        if (addflag == true)
                        {
                            errorstu += table.Rows[i][0].ToString() + "," + table.Rows[i][1].ToString();
                            addflag = false;
                        }
                    }
                }
            }
            ViewBag.error = "导入成功";
            //System.Threading.Thread.Sleep(2000);
            return_msg = "成功导入"+ Count_Suc.ToString() + "个学生信息-----------";
            if (Count_Cmp != Count_Suc)
            {
                return_msg += "****出错位置发生在----" + errorstu + "----该学生之后的所有信息放弃导入！";
            }
            return_msg = return_msg+ "****其他信息:" + err_msg;
            //结束导入，关闭链接
            conn.Close();
            //删除临时文件
            System.IO.File.Delete(savePath);

            return Content("<script>alert('" + return_msg + "');window.history.go(-1);</script>");//返回上一页并刷新
            //return View("StuImprot");
        }

        //Excel导出学生信息
        [BaseActionFilter] 
        public FileResult ExportExcel(string action)
        {
            string schoolid=Request.Form["schoolid"];
            string classno=Request.Form["classno_e"];
            string speno=Request.Form["classno_e"];
            var sbHtml = new StringBuilder();
            sbHtml.Append("<table border='1' cellspacing='0' cellpadding='0'>");
            sbHtml.Append("<tr>");
            var lstTitle = new List<string> { "学号", "姓名", "性别", "身高", "体重", "QQ", "邮箱", "联系电话","个人简介" };
            foreach (var item in lstTitle)
            {
                sbHtml.AppendFormat("<td style='font-size: 14px;text-align:center;background-color: #DCE0E2; font-weight:bold;' height='25'>{0}</td>", item);
            }
            sbHtml.Append("</tr>");
            var q = (from f in sc.Student where f.StuClass == classno select f).ToList();
            for (int i = 0; i < q.Count; i++)
            {         
                if (schoolid + q[i].StuNo == q[i].UserID)
                {
                    sbHtml.Append("<tr>");
                    sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>" + q[i].StuNo + "</td>", i);
                    sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>" + q[i].StuName + "</td>", i);
                    sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>" + q[i].StuSex + "</td>", i);
                    sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>" + q[i].StuWeight + "</td>", i);
                    sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>" + q[i].StuHeight + "</td>", i);
                    sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>" + q[i].StuQQ + "</td>", i);
                    sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>" + q[i].StuEMail + "</td>", i);
                    sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>" + q[i].StuCellphone + "</td>", i);
                    sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>" + q[i].StuResume + "</td>", i);
                    sbHtml.Append("</tr>");
                }
            }
            sbHtml.Append("</table>");

            //第一种:使用FileContentResult
            byte[] fileContents = Encoding.UTF8.GetBytes(sbHtml.ToString());
            var q_classname = (from f in sc.T_Class where f.ClassID == classno select f.ClassName).ToList();
            if (q_classname != null && q_classname.Count != 0)
            {
                //string ss = q_classname[0];
                ViewBag.ClassName = q_classname[0].Trim();
            }
            string filename = q_classname[0].Trim()+" "+"学生信息.xls";
            return File(fileContents, "application/ms-excel", filename);

            //第二种:使用FileStreamResult
            //var fileStream = new MemoryStream(fileContents);
            //return File(fileStream, "application/ms-excel", "fileStream.xls");

            //第三种:使用FilePathResult
            //服务器上首先必须要有这个Excel文件,然会通过Server.MapPath获取路径返回.
            //var fileName = Server.MapPath("/Content/ACM/fileName.xls");
            //return File(fileName, "application/ms-excel", "fileName.xls");
        }

        //下载模版
        [BaseActionFilter]
        public FileResult GetFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Areas/School/Content/Home/file/";
            string fileName = "学生信息导入模版.xls";
            return File(path + fileName, "text/plain", fileName);
        }



        //控制器里面：   年级专业班级联动
        [HttpPost]
        public JsonResult GetSpeList()//专业列表
        {
            string EntryYear = Request["EntryYearCode"].ToString();
            //string schoolid = "10385";
            int Year = Convert.ToInt32(EntryYear);
            var Spe = (from p in sc.C_Specialty where p.EntryYear == Year select new { p.SpeNo, p.SpeName }).ToList();

            return Json(new { count = Spe.Count, Pos = Spe });
        }

        [HttpPost]
        public JsonResult GetClassList()
        {
            string SpeNo = Request["SpeCode"].ToString();
            var Class = (from p in sc.T_Class where p.SpeNo == SpeNo select p).ToList();
            return Json(new { count = Class.Count, Pos = Class });
        }

        [HttpPost]
        public JsonResult GetClassNum()
        {
            string ClassID = Request["ClassID"].ToString();
            var Classnum = (from f in sc.Student where f.StuClass == ClassID select f).ToList();
            return Json(new { count = Classnum.Count, Pos = Classnum.Count });
        }

        [HttpPost]
        public PartialViewResult GetStuList()
        {
            string classno = Request["ClassID"].ToString();
            string speno = Request["SpeNo"].ToString();
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            int entryyear = sc.T_Class.FirstOrDefault(a => a.ClassID == classno).EntryYear;
            int year = entryyear;
            var q_spename = (from f in sc.C_Specialty where f.EntryYear == year && f.SpeNo == speno select f.SpeName).ToList();
            if (q_spename != null && q_spename.Count != 0)
            {
                ViewBag.SpeName = q_spename[0].Trim();
            }
            var q_stulist = (from f in sc.Student where f.StuClass == classno select f).ToList();

            var q_classname = (from f in sc.T_Class where f.ClassID == classno select f.ClassName).ToList();
            if (q_classname != null && q_classname.Count != 0)
            {
                ViewBag.ClassName = q_classname[0].Trim();
            }
            ViewBag.Schoolid = schoolid;
            ViewBag.ClassNo = classno;
            ViewBag.SpeNo = speno;
            return PartialView("Partial_StuList", q_stulist);
        }

        [HttpPost]
        public PartialViewResult GetStuList2()
        {
            string classno = Request["ClassID"].ToString();
            string speno = Request["SpeNo"].ToString();
            string stuno = Request["StuNo"].ToString();
            string stuname = Request["StuName"].ToString();
            string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            int entryyear = sc.T_Class.FirstOrDefault(a => a.ClassID == classno).EntryYear;
            int year = entryyear;
            var q_spename = (from f in sc.C_Specialty where f.EntryYear == year && f.SpeNo == speno select f.SpeName).ToList();
            if (q_spename != null && q_spename.Count != 0)
            {
                ViewBag.SpeName = q_spename[0].Trim();
            }
            var q_stulist = (from f in sc.Student where f.StuClass == classno && (f.StuNo==stuno || f.StuName==stuname) select f).ToList();

            var q_classname = (from f in sc.T_Class where f.ClassID == classno select f.ClassName).ToList();
            if (q_classname != null && q_classname.Count != 0)
            {
                ViewBag.ClassName = q_classname[0].Trim();
            }
            ViewBag.Schoolid = schoolid;
            ViewBag.ClassNo = classno;
            ViewBag.SpeNo = speno;
            return PartialView("Partial_StuList", q_stulist);
        }

    }
}
