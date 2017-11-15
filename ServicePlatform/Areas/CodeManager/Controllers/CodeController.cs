using ServicePlatform.Models;
using ServicePlatform.Areas.CodeManager.ToolHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.SqlClient;
using ServicePlatform.App_Start;
using ServicePlatform.Config;
using ServicePlatform.Controllers.Base;
using ServicePlatform.Lib;

namespace ServicePlatform.Areas.CodeManager.Controllers
{
    public class CodeController : BaseController
    {
        //
        // GET: /CodeManager/Code/

        private CodeTableContext CodeTable = new CodeTableContext();
        private SchoolContext schooldb = new SchoolContext();//
        private ContentContext Contentdb = new ContentContext();


    
         
        #region 专业管理

        [BaseActionFilter]
        public ActionResult speciality1()
        {
          
            var a = (from v in schooldb.C_Specialty
                     where v.SchoolID== DataContext.UserUnit
                     select v.EntryYear ).ToList().Distinct<int>();
            ViewBag.entryyear = a;
            ViewData["sid"] = DataContext.UserUnit;
            return View("~/Areas/CodeManager/Views/Code/StuSpeciality/speciality1.cshtml");
        }

         [BaseActionFilter]
        public ActionResult GetTheDataSpe()
        {
            string sid = Request["sid"];

            List<C_Specialty> ccc = (from v in CodeTable.C_Specialty
                                     where v.SchoolID == sid
                                     select v).ToList();
            int counthaha = ccc.Count();
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            string TheValue = Request["classValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数

            int TotalNum = 0;



            var service = new GetSpe();
            var theEnca = service.LoadNewsData(Temp, 3, counthaha,sid, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
            var result = from b in theEnca select b;



            if (TheAttribute == "fasong")
            {
                int ENTRYYEARINT = Convert.ToInt32(TheValue);
                theEnca = service.LoadNewsData(Temp, ENTRYYEARINT, counthaha - 9, sid, out TotalNum);
                result = from b in theEnca select b;

            }


            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串


            return Content(str);
        }
         [BaseActionFilter]
        public ActionResult DeleteSpeciality()
        {
            //删除新闻数据
            string Deleteyear = Request.Form[0];//获取需要删除的入学年份组成的字符串，且以逗号分割
            string[] SplitArray = Deleteyear.Split(',');//放到数组里
            string Deleteno = Request.Form[1];//获取需要删除的专业编号组成的字符串，且以逗号分割
            string[] SplitArray1 = Deleteno.Split(',');//
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();

            int ArrayCount = SplitArray.Count();

            string StringCount = Convert.ToString(ArrayCount);
            
            for (int a = 0; a < ArrayCount; a++)
            {
                

                try
                {
                    string speno = SplitArray1[a];//不能直接把SplitArray1[a]放入linq语句会报错
                    int TargetID = Convert.ToInt32(SplitArray[a]);
                    string schoolid = ShenFenID;
                    C_Specialty tempspe = (from v in CodeTable.C_Specialty where v.EntryYear == TargetID && v.SpeNo == speno && v.SchoolID == schoolid select v).FirstOrDefault();
                    CodeTable.C_Specialty.Remove(tempspe);
                    CodeTable.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这个专业被引用了不能删除'); history.go(-1)</script>");
                }
                //直接执行，有错误就弹出警告，因为这个专业可能不止被一个表引用到了



            }

            return Content(StringCount);
        }
         [BaseActionFilter]
        public ActionResult speDetail(string spe)
        {
            string[] SplitArray = spe.Split(';');
            //spe = spe.Replace("'", "");
            int year = int.Parse(SplitArray[1]);

            string sno = SplitArray[0];
            string schoolid = (Session["Vars"] as Vars).getUserUnit();

            //显示新闻详情页

            var temp = (from v in schooldb.School where v.SchoolID == schoolid select v.SchoolName).FirstOrDefault();
            ViewBag.schoolname = temp;


            C_Specialty tempspe = (from v in CodeTable.C_Specialty where v.EntryYear == year && v.SpeNo == sno && v.SchoolID == schoolid select v).FirstOrDefault();

            ViewBag.spedetail = tempspe;
            return View("~/Areas/CodeManager/Views/Code/StuSpeciality/speDetail.cshtml");
        }

         [BaseActionFilter]
        public ActionResult Updatespe(string spe)
        {
            string[] SplitArray = spe.Split(';');
            //spe = spe.Replace("'", "");
            int year = int.Parse(SplitArray[1]);
            string sno = SplitArray[0];
            string schoolid = (Session["Vars"] as Vars).getUserUnit();
            var temp = (from v in schooldb.School where v.SchoolID == schoolid select v.SchoolName).FirstOrDefault();
            ViewBag.schoolname = temp;
            // Session["speno1"] = sno;
            // Session["speyear1"] = year;//session是只能放字符串类型的么，就是说不管把什么值放进session取出来都是字符串么？

            C_Specialty tempspe = (from v in CodeTable.C_Specialty where v.EntryYear == year && v.SpeNo == sno && v.SchoolID == schoolid select v).FirstOrDefault();

            ViewBag.spe = tempspe;
            return View("~/Areas/CodeManager/Views/Code/StuSpeciality/Updatespe.cshtml");
        }

         [BaseActionFilter]
        public ActionResult ReceUpdateView()
        {
            string speyear = Request.Form["EntryYear"];
            int speyear1 = int.Parse(speyear);//接收入学年份并把它转换为数字
            string speno = Request.Form["SpeNo"];
            string spename = Request.Form["SpeName"];
            string schoolid = (Session["Vars"] as Vars).getUserUnit(); ;
          

            try
            {
                C_Specialty temp = (from c in CodeTable.C_Specialty where c.EntryYear == speyear1 && c.SpeNo == speno && c.SchoolID == schoolid select c).FirstOrDefault();
                temp.EntryYear = speyear1;
                temp.SpeNo = speno;
                temp.SpeName = spename;
                CodeTable.SaveChanges();
            }
            catch (Exception)
            {

                return Content("<script>alert('这个专业被引用了不能修改'); history.go(-1)</script>");
            }

            return Redirect("/CodeManager/Code/speciality1");
        }

         [BaseActionFilter]
        public ActionResult Addspe()
        {
            string schoolid = (Session["Vars"] as Vars).getUserUnit();
            var temp = (from v in schooldb.School where v.SchoolID == schoolid select v.SchoolName).FirstOrDefault();
            ViewBag.schoolname = temp;

           

            return View("~/Areas/CodeManager/Views/Code/StuSpeciality/Addspe.cshtml");
        }

       

        [HttpPost]
        [BaseActionFilter]
        public ActionResult reciveAddSpe()
        {
            int EntryYear = int.Parse(Request.Form["EntryYear"]);
            string SpeName = Request.Form["SpeName"];
            string SpeNo = Request.Form["SpeNo"];
            string SchoolID = (Session["Vars"] as Vars).getUserUnit();
            C_Specialty Spetemp = (from v in CodeTable.C_Specialty where v.EntryYear == EntryYear && v.SpeNo == SpeNo && v.SchoolID == SchoolID select v).FirstOrDefault();
            if (Spetemp != null)
            {
                return Content("<script>alert('该专业已经存在'); history.go(-1)</script>");

            }
            else
            {
                C_Specialty Speform = new C_Specialty { EntryYear = EntryYear, SpeName = SpeName, SpeNo = SpeNo, SchoolID = SchoolID };
                CodeTable.C_Specialty.Add(Speform);
                CodeTable.SaveChanges();
                return Redirect("/CodeManager/Code/speciality1");


            }

        }


       
#endregion 专业管理

        #region 班级管理


        public string classid;
         [BaseActionFilter]
        public ActionResult class1()
        {
          //  List<C_Specialty> TEMP = (from v in CodeTable.C_Specialty select v).ToList();
           // ViewBag.shengfen = TEMP;
            var a = (from v in schooldb.C_Specialty select v.EntryYear).ToList().Distinct<int>();
            ViewBag.entryyear = a;
            return View("~/Areas/CodeManager/Views/Code/StuClass/class1.cshtml");
        }


         [BaseActionFilter]
         public ActionResult receajaxchangeAddspe()//三个页面引用这个ajax
         {
             string schoolid = Request.Form[0];//获取需要删除的入学年份组成的字符串，且以逗号分割
             int entryno = Convert.ToInt32(schoolid);
            

             //string TENPP1 = (from v in CodeTable.C_City where v.CityID == PID select v.CityName).FirstOrDefault();

             var TTT = (from c in CodeTable.C_Specialty where c.EntryYear == entryno select c.SpeName).ToList().Distinct<string>();
             if (TTT != null)
             {
                 // string[] spename = TTT.SpeName;
                 string[] TTT1 = new string[TTT.Count()];
                 string TTT11 = "";
                 int i = 0;
                 foreach (var c in TTT)
                 {
                     TTT1[i] = c;
                     i++;

                 }
                 for (int j = 0; j < TTT.Count(); j++)
                 {
                     TTT11 += TTT1[j] + ",";
                 }
                 // var JsonResult = new { total=, rows = result };
                 //string str = JsonSerializeHelper.SerializeToJson(JsonResult);
                 return Content(TTT11);
             }
             else
             {

                 string sfalse = "cuowu";
                 return Content(sfalse);
             }
         }


         [BaseActionFilter]
        public ActionResult GetTheDataClass()
        {
            //新闻列表分页和查询功能

            List<T_Class> ccc = (from v in schooldb.T_Class select v).ToList();
            int counthaha = ccc.Count();
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //接受DataGrid传来的参数
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
           

            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            


            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数
            int TotalNum = 0;
            var service = new GetClass();
            var theClass= service.LoadNewsData(Temp, 3, "sss",counthaha, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
            var result = from b in theClass where b.SchoolID==ShenFenID select b;

           



            //依据搜索框传递的属性名和相应值得到搜索出来的模型
          //  List<T_Class> GetSpe = schooldb.T_Class.ToList();


            if (TheAttribute == "fasong")
            {
                string TheValue = Request["classValue"];
                string[] SPENOENTRY = TheValue.Split(';');
                string nianfen = SPENOENTRY[1];
                int nianfenint = Convert.ToInt32(nianfen);
                string zhuanyename = SPENOENTRY[0];
                if (zhuanyename != "--请选择--")
                {
                    C_Specialty temp123 = (from v in CodeTable.C_Specialty where v.EntryYear == nianfenint && v.SpeName == zhuanyename select v).FirstOrDefault();
                    string spenos = temp123.SpeNo;
                    theClass = service.LoadNewsData(Temp, nianfenint, spenos, counthaha - 9, out TotalNum);
                    result = from b in theClass select b;
                }
                else
                {
                   
                    theClass = service.LoadNewsData(Temp, nianfenint,"--请选择--", counthaha - 9, out TotalNum);
                    result = from b in theClass select b;
                }

            }
            

            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串
           

            return Content(str);
        }

         [BaseActionFilter]
        public ActionResult Deleteclass()
        {
            //删除新闻数据

            string Deleteid = Request.Form[0];//获取需要删除的专业编号组成的字符串，且以逗号分割
            string[] SplitArray1 = Deleteid.Split(',');//取到要删除的新闻编号放进数组里

            int ArrayCount = SplitArray1.Count();

            string StringCount = Convert.ToString(ArrayCount);

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string classid = SplitArray1[a];//不能直接把SplitArray1[a]放入linq语句会报错

                try
                {
                    T_Class tempspe = (from v in schooldb.T_Class where v.ClassID == classid select v).FirstOrDefault();
                    schooldb.T_Class.Remove(tempspe);
                    schooldb.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这条班级信息在别的表被引用了不能删除'); </script>");
                }

            }

            return Content(StringCount);
        }

         [BaseActionFilter]
        public ActionResult ClassDetail(string id)
        {
            
            string schoolid = (Session["Vars"] as Vars).getUserUnit();
            var temp = (from v in schooldb.School where v.SchoolID == schoolid select v.SchoolName).FirstOrDefault();
            ViewBag.schoolname = temp;
            // char stemp = id[0];
            T_Class class1 = (from v in schooldb.T_Class where v.ClassID == id select v).FirstOrDefault();//获取详情页新闻对象
            //  IEnumerable<T_Class> GetSpe = schooldb.T_Class;
            // T_Class class1= schooldb.T_Class.Find(id);他说用这个会缺少一个强制转换？法将类型“ServicePlatform.Areas.School.Models.T_Class”隐式转换为“System.Collections.IEnumerable”。存在一个显式转换(是否缺少强制转换?) 
            ViewBag.classDetail = class1;
            return View("~/Areas/CodeManager/Views/Code/StuClass/classDetail.cshtml");
        }
         [BaseActionFilter]
        public ActionResult UpdateClass(string id)
        {

            string schoolid1 = (Session["Vars"] as Vars).getUserUnit();
            var temp = (from v in schooldb.School where v.SchoolID == schoolid1 select v.SchoolName).FirstOrDefault();
            ViewBag.schoolname = temp;
            classid = id;
            Session["Class3"] = id;
            
            var schoolid = (from v in schooldb.C_Specialty select v.SchoolID).ToList().Distinct<string>();
           
            T_Class class1 = (from v in schooldb.T_Class where v.ClassID == id select v).FirstOrDefault();
            ViewBag.updateClass = class1;
            ViewBag.yearInt = schoolid;
             int enyear=class1.EntryYear;
             string speno=class1.SpeNo;
             string spename = (from v in schooldb.C_Specialty where( v.SchoolID == schoolid1 && v.SpeNo == speno && v.EntryYear == enyear) select v.SpeName).FirstOrDefault();
             ViewBag.spename = spename;
            return View("~/Areas/CodeManager/Views/Code/StuClass/UpdateClass.cshtml");
        }
         [BaseActionFilter]
        public ActionResult UpdatereceClass()//接收修改班级信息的action
         {
             string Schoolid = (Session["Vars"] as Vars).getUserUnit();
            int entryyear = int.Parse(Request.Form["entryyear"]);
            string entryno = Request.Form["speno"];
            string ClassNo = Request.Form["ClassNo"];
            string ClassName = Request.Form["ClassName"];
           // string classid = Request.Form["ClassID"];
            string classid1 = Session["Class3"].ToString();
          

            try
            {
                T_Class CLASS2 = (from c in schooldb.T_Class where c.ClassID == classid1 select c).FirstOrDefault();
                CLASS2.ClassID = ClassNo;
                CLASS2.ClassName = ClassName;
                CLASS2.EntryYear = entryyear;
                CLASS2.SpeNo = entryno;
                CLASS2.SchoolID = Schoolid;
                schooldb.SaveChanges();

            }
            catch (Exception)
            {

                return Content("<script>alert('这条班级信息在别的表被引用了不能修改'); history.go(-1)</script>");
            }
            return RedirectToAction("class1"); 

        }
         [BaseActionFilter]
        public ActionResult Addc()
        {

            string schoolid1 = (Session["Vars"] as Vars).getUserUnit();
            var temp = (from v in schooldb.School where v.SchoolID == schoolid1 select v.SchoolName).FirstOrDefault();
            ViewBag.schoolname = temp;//通过schoolid找到该学校的名字
            var a = (from v in schooldb.C_Specialty select v.EntryYear).ToList().Distinct<int>();
            ViewBag.schoolID = schoolid1;//登录者的学校id
             
            ViewBag.entyryear = a;//该学校所有的入学年份
            
            return View("~/Areas/CodeManager/Views/Code/StuClass/Addc.cshtml");
        }
        [HttpPost]
        [BaseActionFilter]
        public ActionResult reciveClass()
        {
            int entryyear = int.Parse(Request.Form["entryyear"]);//request.form是接收来自发送的post方式
            string entryno = Request.Form["speno"];
            string ClassNo = Request.Form["ClassNo"];
            string ClassName = Request.Form["ClassName"];
            string Schoolid = (Session["Vars"] as Vars).getUserUnit();
            // C_Specialty Spetemp = (from v in schooldb.C_Specialty where v.EntryYear == entryyear && v.SpeNo == entryno select v).FirstOrDefault();
            //if (Spetemp == null)
            //{
            //   return Content("<script>alert('不存在这个专业请重新填写'); history.go(-1)</script>");

            //            }//因为是用下拉框选出来的所以肯定是有这个专业的就不用写这个了

            T_Class addc = (from v in schooldb.T_Class where v.ClassID == ClassNo select v).FirstOrDefault();
            if (addc != null)
                return Content("<script>alert('已经有这个班级了请重新填写'); history.go(-1)</script>");
            else
            {

                T_Class Speform = new T_Class { EntryYear = entryyear, SpeNo = entryno, ClassID = ClassNo, ClassName = ClassName, SchoolID = Schoolid };
                schooldb.T_Class.Add(Speform);
                schooldb.SaveChanges();
                return Redirect("/CodeManager/Code/class1");


            }

        }

         [BaseActionFilter]

        public ActionResult receajaxClass()
        {
            string Deleteyear = Request.Form[0];//获取需要删除的入学年份组成的字符串，且以逗号分割
            int yearint = int.Parse(Deleteyear);//取到要删除的新闻编号放进数组里
            string Deleteno = Request.Form[1];//获取需要删除的专业编号组成的字符串，且以逗号分割
            string schoolid = (Session["Vars"] as Vars).getUserUnit();
            //  string[] SplitArray1 = Deleteno.Split(',');//取到要删除的新闻编号放进数组里
            C_Specialty TTT = (from c in schooldb.C_Specialty where c.EntryYear == yearint && c.SpeName == Deleteno && c.SchoolID == schoolid select c).FirstOrDefault();
            if (TTT != null)
            {
                string spename = TTT.SpeNo;
                return Content(spename);
            }
            else
            {
                string sfalse = "cuowu";
                return Content(sfalse);
            }

        }
         [BaseActionFilter]
        public ActionResult receajax1Class()
        {
            string Deleteyear = Request.Form[0];//获取需要删除的入学年份组成的字符串，且以逗号分割
            int yearint = int.Parse(Deleteyear);//取到要删除的新闻编号放进数组里
            string schoolid = (Session["Vars"] as Vars).getUserUnit();
            //string Deleteno = Request.Form[1];//获取需要删除的专业编号组成的字符串，且以逗号分割
            //  string[] SplitArray1 = Deleteno.Split(',');//取到要删除的新闻编号放进数组里
            var TTT = (from c in CodeTable.C_Specialty where c.EntryYear == yearint && c.SchoolID == schoolid select c.SpeName).ToList().Distinct<string>();
            if (TTT != null)
            {
                // string[] spename = TTT.SpeName;
                string[] TTT1 = new string[TTT.Count()];
                string TTT11 = "";
                int i = 0;
                foreach (var c in TTT)
                {
                    TTT1[i] = c;
                    i++;

                }
                for (int j = 0; j < TTT.Count(); j++)
                {
                    TTT11 += TTT1[j] + ",";
                }
                // var JsonResult = new { total=, rows = result };
                //string str = JsonSerializeHelper.SerializeToJson(JsonResult);
                return Content(TTT11);
            }
            else
            {
                string sfalse = "cuowu";
                return Content(sfalse);
            }


        }

        
        #endregion 班级管理


         
        
        #region 省份管理
        [BaseActionFilter]
        public ActionResult C_Province1()
        {
            return View("~/Areas/CodeManager/Views/Code/C_Province/C_Province1.cshtml");
        }
        public ActionResult GetTheDataProvince()
        {


            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            string TheValue = Request["classValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数
            int TotalNum = 0;
            var service = new getProvince();
            var theEnca = service.LoadNewsData(Temp, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
            var result = from b in theEnca select b;

            #region Category Search




            if (TheAttribute == "ProvinceID")
            {
                result = from a in result where (String.IsNullOrEmpty(TheValue) || a.ProvinceID.Contains(TheValue)) select a;
            }
            if (TheAttribute == "ProvinceName")
            {
                result = from a in result where (String.IsNullOrEmpty(TheValue) || a.ProvinceName.Contains(TheValue)) select a;
            }

            #endregion


            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串


            return Content(str);
        }

         [BaseActionFilter]
        public ActionResult DeleteProvince()//接受表格传来的信息删除表格信息
        {
            //删除新闻数据

            string Deleteid = Request.Form[0];
            string[] SplitArray1 = Deleteid.Split(',');

            int ArrayCount = SplitArray1.Count();

            string StringCount = Convert.ToString(ArrayCount);

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string classid = SplitArray1[a];//不能直接把SplitArray1[a]放入linq语句会报错

                try
                {
                    C_Province tempspe = (from v in CodeTable.C_Province where v.ProvinceID == classid select v).FirstOrDefault();
                    CodeTable.C_Province.Remove(tempspe);
                    CodeTable.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这条种类信息在别的表被引用了不能删除');history.go(-1)</script>");
                }

            }

            return Content(StringCount);
        }


         [BaseActionFilter]
        public ActionResult AddProvince()//添加新的种类信息
        {
            return View("~/Areas/CodeManager/Views/Code/C_Province/AddProvince.cshtml");

        }
         [BaseActionFilter]
        public ActionResult ReAddProvince()//接受添加种类信息的表单和数据库交互
        {
            string id = Request.Form["ProvinceID"];
            string name = Request.Form["ProvinceName"];
            int a = id.Count();
            if (a != 6)
            {

                return Content("<script>alert('请输入6个数字的省份编号'); history.go(-1)</script>");

            }
            C_Province temp = (from v in CodeTable.C_Province where v.ProvinceID == id select v).FirstOrDefault();
            if (temp != null)
            {
                return Content("<script>alert('这个省份已经存在'); history.go(-1)</script>");

            }
            else
            {
                C_Province temp1 = new C_Province { ProvinceID = id, ProvinceName = name };
                CodeTable.C_Province.Add(temp1);
                CodeTable.SaveChanges();

            }
            return Redirect("/CodeManager/Code/C_Province1");//注意前面那个platform也加上不然会报错找不到视图


        }
         [BaseActionFilter]
        public ActionResult detailProvince(string id)//查看详情
        {
            C_Province temp = (from c in CodeTable.C_Province where c.ProvinceID == id select c).FirstOrDefault();
            ViewBag.detail = temp;

            return View("~/Areas/CodeManager/Views/Code/C_Province/detailProvince.cshtml");

        }
         [BaseActionFilter]
        public ActionResult updateProvince(string id)//修改种类信息
        {
            Session["Provinceid"] = id;//我在session里还没设置过期时间
            C_Province temp = (from c in CodeTable.C_Province where c.ProvinceID == id select c).FirstOrDefault();
            ViewBag.update = temp;

            return View("~/Areas/CodeManager/Views/Code/C_Province/updateProvince.cshtml");

        }
         [BaseActionFilter]
        public ActionResult ReupdateProvince()//接收修改后的信息和数据库进行交互
        {
            string Enid = Request.Form["ProvinceID"];
            string Enname = Request.Form["ProvinceName"];
            string id = Convert.ToString(Session["Provinceid"]);
            if (id != Enid)
            {
                return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

            }
            else
            {
                C_Province TEMPP = (from c in CodeTable.C_Province where c.ProvinceID == id select c).FirstOrDefault();

                try
                {
                    TEMPP.ProvinceID = Enid;
                    TEMPP.ProvinceName = Enname;
                    CodeTable.SaveChanges();

                }
                catch (Exception)
                {

                    return Content("<script>alert('这个省份被别的表关联掉了不能修改'); history.go(-1)</script>");
                }
            }

            return Redirect("/CodeManager/Code/C_Province1");

        }



         #endregion 省份管理

        #region 城市管理
        [BaseActionFilter]
        public ActionResult C_City1()
        {
            List<C_Province> TEMP = (from v in CodeTable.C_Province select v).ToList();
            ViewBag.shengfen = TEMP;
            return View("~/Areas/CodeManager/Views/Code/C_City/C_City1.cshtml");
        }
         [BaseActionFilter]
        public ActionResult GetTheDataCity()
        {


            List<C_City> ccc = (from v in CodeTable.C_City select v).ToList();
            int counthaha = ccc.Count();
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            string TheValue = Request["classValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数

            int TotalNum = 0;



            var service = new getCity();
            var theEnca = service.LoadNewsData(Temp, "nihao", counthaha, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
            var result = from b in theEnca select b;



            if (TheAttribute == "fasong")
            {
                C_Province temp123 = (from v in CodeTable.C_Province where v.ProvinceName == TheValue select v).FirstOrDefault();
                string CityIDS = temp123.ProvinceID;
                theEnca = service.LoadNewsData(Temp, CityIDS, counthaha - 9, out TotalNum);
                result = from b in theEnca select b;

            }







            /* #region Category Search

            


             if (TheAttribute == "CityID")
             {
                 result = from a in result where (String.IsNullOrEmpty(TheValue) || a.CityID.Contains(TheValue)) select a;
             }
             if (TheAttribute == "CityName")
             {
                 result = from a in result where (String.IsNullOrEmpty(TheValue) || a.CityName.Contains(TheValue)) select a;
             }

             #endregion*/


            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串


            return Content(str);
        }

         [BaseActionFilter]
        public ActionResult DeleteCity()//接受表格传来的信息删除表格信息
        {
            //删除新闻数据

            string Deleteid = Request.Form[0];
            string[] SplitArray1 = Deleteid.Split(',');

            int ArrayCount = SplitArray1.Count();

            string StringCount = Convert.ToString(ArrayCount);

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string classid = SplitArray1[a];//不能直接把SplitArray1[a]放入linq语句会报错

                try
                {
                    C_City tempspe = (from v in CodeTable.C_City where v.CityID == classid select v).FirstOrDefault();
                    CodeTable.C_City.Remove(tempspe);
                    CodeTable.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这个城市在别的表被引用了不能删除');history.go(-1)</script>");
                }

            }

            return Content(StringCount);
        }


         [BaseActionFilter]
        public ActionResult AddCity()//添加新的种类信息
        {

            List<C_Province> temp = (from c in CodeTable.C_Province select c).ToList();
            ViewBag.PROVINCE = temp;
            return View("~/Areas/CodeManager/Views/Code/C_City/AddCity.cshtml");

        }
         [BaseActionFilter]
        public ActionResult ReAddCity()//接受添加种类信息的表单和数据库交互
        {
            string Pname = Request.Form["ProvinceName"];
            string id = Request.Form["CityID"];
            string name = Request.Form["CityName"];
            int a = id.Count();
            if (a != 6)
            {

                return Content("<script>alert('请输入6个数字的城市编号'); history.go(-1)</script>");

            }

            C_City temp = (from v in CodeTable.C_City where v.CityID == id select v).FirstOrDefault();
            if (temp != null)
            {
                return Content("<script>alert('这个省份已经存在'); history.go(-1)</script>");

            }
            else
            {
                C_Province temp2 = (from v in CodeTable.C_Province where v.ProvinceName == Pname select v).FirstOrDefault();
                string pid = temp2.ProvinceID;
                C_City temp1 = new C_City { CityID = id, CityName = name, ProvinceID = pid };
                CodeTable.C_City.Add(temp1);
                CodeTable.SaveChanges();

            }
            return Redirect("/CodeManager/Code/C_City1");//注意前面那个platform也加上不然会报错找不到视图


        }
         [BaseActionFilter]
        public ActionResult detailCity(string id)//查看详情
        {

            C_City temp = (from c in CodeTable.C_City where c.CityID == id select c).FirstOrDefault();
            ViewBag.detail = temp;

            string pid = temp.ProvinceID;
            C_Province temp3 = (from v in CodeTable.C_Province where v.ProvinceID == pid select v).FirstOrDefault();
            string pname = temp3.ProvinceName;
            ViewBag.pname = pname;

            return View("~/Areas/CodeManager/Views/Code/C_City/detailCity.cshtml");

        }

         [BaseActionFilter]
        public ActionResult updateCity(string id)//修改种类信息
        {
            Session["Cityid"] = id;//我在session里还没设置过期时间

            C_City temp = (from c in CodeTable.C_City where c.CityID == id select c).FirstOrDefault();
            ViewBag.update = temp;

            string pid = temp.ProvinceID;
            C_Province temp3 = (from v in CodeTable.C_Province where v.ProvinceID == pid select v).FirstOrDefault();
            string pname = temp3.ProvinceName;
            ViewBag.pname = pname;

            List<C_Province> temp4 = (from v in CodeTable.C_Province select v).ToList();
            ViewBag.pnameall = temp4;

            return View("~/Areas/CodeManager/Views/Code/C_City/updateCity.cshtml");

        }
         [BaseActionFilter]
        public ActionResult ReupdateCity()//接收修改后的信息和数据库进行交互
        {
            string Pname = Request.Form["ProvinceName"];


            string Enid = Request.Form["CityID"];
            string Enname = Request.Form["CityName"];
            string id = Convert.ToString(Session["Cityid"]);
            if (id != Enid)
            {
                return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

            }
            else
            {
                C_Province ttt = (from v in CodeTable.C_Province where v.ProvinceName == Pname select v).FirstOrDefault();
                string Pid = ttt.ProvinceID;
                C_City TEMPP = (from c in CodeTable.C_City where c.CityID == id select c).FirstOrDefault();
                try
                {
                    TEMPP.CityID = Enid;
                    TEMPP.ProvinceID = Pid;
                    TEMPP.CityName = Enname;
                    CodeTable.SaveChanges();

                }
                catch (Exception)
                {

                    return Content("<script>alert('这个城市被别的表关联掉了不能修改'); history.go(-1)</script>");
                }
            }
            return Redirect("/CodeManager/Code/C_City1");

        }


            #endregion 城市管理
 
        #region 县区管理
        [BaseActionFilter]
        public ActionResult C_County1()
        {
            List<C_Province> TEMP = (from v in CodeTable.C_Province select v).ToList();

            ViewBag.shengfen = TEMP;
            return View("~/Areas/CodeManager/Views/Code/C_County/C_County1.cshtml");
        }

         [BaseActionFilter]
        public ActionResult receajaxchangeAddCounty()//三个页面引用这个ajax
        {
            string schoolid = Request.Form[0];//获取需要删除的入学年份组成的字符串，且以逗号分割

            C_Province TENPP = (from v in CodeTable.C_Province where v.ProvinceName == schoolid select v).FirstOrDefault();
            string PID = TENPP.ProvinceID;

            //string TENPP1 = (from v in CodeTable.C_City where v.CityID == PID select v.CityName).FirstOrDefault();

            var TTT = (from c in CodeTable.C_City where c.ProvinceID == PID select c.CityName).ToList().Distinct<string>();
            if (TTT != null)
            {
                // string[] spename = TTT.SpeName;
                string[] TTT1 = new string[TTT.Count()];
                string TTT11 = "";
                int i = 0;
                foreach (var c in TTT)
                {
                    TTT1[i] = c;
                    i++;

                }
                for (int j = 0; j < TTT.Count(); j++)
                {
                    TTT11 += TTT1[j] + ",";
                }
                // var JsonResult = new { total=, rows = result };
                //string str = JsonSerializeHelper.SerializeToJson(JsonResult);
                return Content(TTT11);
            }
            else
            {

                string sfalse = "cuowu";
                return Content(sfalse);
            }
        }
         [BaseActionFilter]
        public ActionResult GetTheDataCounty()
        {


            List<C_County> ccc = (from v in CodeTable.C_County select v).ToList();
            int counthaha = ccc.Count();
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            string TheValue = Request["classValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数

            int TotalNum = 0;



            var service = new getCounty();
            var theEnca = service.LoadNewsData(Temp, "nihao", counthaha, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
            var result = from b in theEnca select b;



            if (TheAttribute == "fasong")
            {
                C_City temp123 = (from v in CodeTable.C_City where v.CityName == TheValue select v).FirstOrDefault();
                string CityIDS = temp123.CityID;
                theEnca = service.LoadNewsData(Temp, CityIDS, counthaha - 9, out TotalNum);
                result = from b in theEnca select b;

            }







            /* #region Category Search

            


             if (TheAttribute == "CityID")
             {
                 result = from a in result where (String.IsNullOrEmpty(TheValue) || a.CityID.Contains(TheValue)) select a;
             }
             if (TheAttribute == "CityName")
             {
                 result = from a in result where (String.IsNullOrEmpty(TheValue) || a.CityName.Contains(TheValue)) select a;
             }

             #endregion*/


            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串


            return Content(str);
        }

         [BaseActionFilter]
        public ActionResult DeleteCounty()//接受表格传来的信息删除表格信息
        {
            //删除新闻数据

            string Deleteid = Request.Form[0];
            string[] SplitArray1 = Deleteid.Split(',');

            int ArrayCount = SplitArray1.Count();

            string StringCount = Convert.ToString(ArrayCount);

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string classid = SplitArray1[a];//不能直接把SplitArray1[a]放入linq语句会报错

                try
                {
                    C_County tempspe = (from v in CodeTable.C_County where v.CountyID == classid select v).FirstOrDefault();
                    CodeTable.C_County.Remove(tempspe);
                    CodeTable.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这个县区在别的表被引用了不能删除');history.go(-1)</script>");
                }

            }

            return Content(StringCount);
        }


         [BaseActionFilter]
        public ActionResult AddCounty()//添加新的种类信息
        {

            List<C_Province> temp = (from c in CodeTable.C_Province select c).ToList();
            ViewBag.PROVINCE = temp;
            return View("~/Areas/CodeManager/Views/Code/C_County/AddCounty.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReAddCounty()//接受添加种类信息的表单和数据库交互
        {
            string Cname = Request.Form["CityName"];
            string id = Request.Form["CountyID"];
            string name = Request.Form["CountyName"];


            int a = id.Count();
            if (a != 6)
            {

                return Content("<script>alert('请输入6个数字的县区编号'); history.go(-1)</script>");

            }

            C_County temp = (from v in CodeTable.C_County where v.CountyID == id select v).FirstOrDefault();
            if (temp != null)
            {
                return Content("<script>alert('这个县区已经存在'); history.go(-1)</script>");

            }
            else
            {
                C_City temp2 = (from v in CodeTable.C_City where v.CityName == Cname select v).FirstOrDefault();
                string pid = temp2.CityID;
                C_County temp1 = new C_County { CountyID = id, CountyName = name, CityID = pid };
                CodeTable.C_County.Add(temp1);
                CodeTable.SaveChanges();

            }
            return Redirect("/CodeManager/Code/C_County1");//注意前面那个platform也加上不然会报错找不到视图


        }
        [BaseActionFilter]
        public ActionResult detailCounty(string id)//查看详情
        {

            C_County temp = (from c in CodeTable.C_County where c.CountyID == id select c).FirstOrDefault();
            ViewBag.detail = temp;



            string Cid = temp.CityID;
            C_City temp3 = (from v in CodeTable.C_City where v.CityID == Cid select v).FirstOrDefault();
            string Cname = temp3.CityName;
            ViewBag.Cname = Cname;

            string Pid = temp3.ProvinceID;
            string pname = (from v in CodeTable.C_Province where v.ProvinceID == Pid select v.ProvinceName).FirstOrDefault();
            ViewBag.Pname = pname;

            return View("~/Areas/CodeManager/Views/Code/C_County/detailCounty.cshtml");

        }
         [BaseActionFilter]
        public ActionResult updateCounty(string id)//修改种类信息
        {
            Session["Countyid"] = id;//我在session里还没设置过期时间

            C_County temp = (from c in CodeTable.C_County where c.CountyID == id select c).FirstOrDefault();
            ViewBag.update = temp;

            string pid = temp.CityID;
            C_City temp3 = (from v in CodeTable.C_City where v.CityID == pid select v).FirstOrDefault();
            string Cname = temp3.CityName;
            ViewBag.Cname = Cname;

            string Pid = temp3.ProvinceID;
            string pname = (from v in CodeTable.C_Province where v.ProvinceID == Pid select v.ProvinceName).FirstOrDefault();
            ViewBag.Pname = pname;

            List<C_Province> temp4 = (from v in CodeTable.C_Province select v).ToList();
            ViewBag.pnameall = temp4;

            return View("~/Areas/CodeManager/Views/Code/C_County/updateCounty.cshtml");

        }
         [BaseActionFilter]
        public ActionResult ReupdateCounty()//接收修改后的信息和数据库进行交互
        {
            string Cname = Request.Form["CityName"];


            string Enid = Request.Form["CountyID"];
            string Enname = Request.Form["CountyName"];
            string id = Convert.ToString(Session["Countyid"]);
            if (id != Enid)
            {
                return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

            }
            else
            {
                C_City ttt = (from v in CodeTable.C_City where v.CityName == Cname select v).FirstOrDefault();
                string Cid = ttt.CityID;
                C_County TEMPP = (from c in CodeTable.C_County where c.CountyID == id select c).FirstOrDefault();
                try
                {
                    TEMPP.CountyID = Enid;
                    TEMPP.CityID = Cid;
                    TEMPP.CountyName = Enname;
                    CodeTable.SaveChanges();

                }
                catch (Exception)
                {

                    return Content("<script>alert('这个县区被别的表关联掉了不能修改'); history.go(-1)</script>");
                }
            }
            return Redirect("/CodeManager/Code/C_County1");

        }


        #endregion 县区管理


        
      
        
        #region 企业类型管理
        [BaseActionFilter]

        public ActionResult C_EntCategory1()
        {
            return View("~/Areas/CodeManager/Views/Code/C_EntCategory/C_EntCategory1.cshtml");
        }
         [BaseActionFilter]
        public ActionResult GetTheDataEntCategory()
        {
            //新闻列表分页和查询功能

            //接受DataGrid传来的参数

            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            string TheValue = Request["classValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数
            int TotalNum = 0;
            var service = new getEnca();
            var theEnca = service.LoadNewsData(Temp, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
            var result = from b in theEnca select b;

            #region Category Search



            //依据搜索框传递的属性名和相应值得到搜索出来的模型
            // List<T_Class> GetSpe = CodeTable.T_Class.ToList();



            //var result = from a in theNews from b in GetState where a.FlowState==b.NewsStateID select new { a.NewsID, a.NewsTitle, a.NewsTypeID, a.NewsAuthor, a.NewsPublisher, a.PubDate, b.NewsStateName, a.IsShow, a.IsLocked, a.NewsColunmID, a.PicUrl, a.VideoUrl, a.Html, a.LinkUrl, a.Download };

            if (TheAttribute == "EntCategoryID")
            {
                result = from a in result where (String.IsNullOrEmpty(TheValue) || a.EntCategoryID.Contains(TheValue)) select a;
            }
            if (TheAttribute == "EntCategoryName")
            {
                result = from a in result where (String.IsNullOrEmpty(TheValue) || a.EntCategoryName.Contains(TheValue)) select a;
            }

            #endregion


            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串
            //ChangeNewsState c=new ChangeNewsState();
            //string Newstr=c.ChangeState(str);

            return Content(str);
        }

         [BaseActionFilter]
        public ActionResult DeleteEnca()//接受表格传来的信息删除表格信息
        {
            //删除新闻数据

            string Deleteid = Request.Form[0];//获取需要删除的专业编号组成的字符串，且以逗号分割
            string[] SplitArray1 = Deleteid.Split(',');//取到要删除的新闻编号放进数组里

            int ArrayCount = SplitArray1.Count();

            string StringCount = Convert.ToString(ArrayCount);

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string classid = SplitArray1[a];//不能直接把SplitArray1[a]放入linq语句会报错

                try
                {
                    C_EntCategory tempspe = (from v in CodeTable.C_EntCategory where v.EntCategoryID == classid select v).FirstOrDefault();
                    CodeTable.C_EntCategory.Remove(tempspe);
                    CodeTable.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这条种类信息在别的表被引用了不能删除');history.go(-1)</script>");
                }

            }

            return Content(StringCount);
        }


         [BaseActionFilter]
        public ActionResult AddEnca()//添加新的种类信息
        {
            return View("~/Areas/CodeManager/Views/Code/C_EntCategory/AddEnca.cshtml");

        }
         [BaseActionFilter]
        public ActionResult ReAddEnca()//接受添加种类信息的表单和数据库交互
        {
            string id = Request.Form["EntCategoryID"];
            string name = Request.Form["EntCategoryName"];
            C_EntCategory temp = (from v in CodeTable.C_EntCategory where v.EntCategoryID == id select v).FirstOrDefault();
            if (temp != null)
            {
                return Content("<script>alert('这个种类已经存在'); history.go(-1)</script>");

            }
            else
            {
                C_EntCategory temp1 = new C_EntCategory { EntCategoryID = id, EntCategoryName = name };
                CodeTable.C_EntCategory.Add(temp1);
                CodeTable.SaveChanges();

            }
            return Redirect("/Platform/C_EntCategory/C_EntCategory1");//注意前面那个platform也加上不然会报错找不到视图


        }
         [BaseActionFilter]
        public ActionResult detailEnta(string id)//查看详情
        {
            C_EntCategory temp = (from c in CodeTable.C_EntCategory where c.EntCategoryID == id select c).FirstOrDefault();
            ViewBag.detail = temp;

            return View("~/Areas/CodeManager/Views/Code/C_EntCategory/detailEnca.cshtml");

        }
         [BaseActionFilter]
        public ActionResult updateEnca(string id)//修改种类信息
        {
            Session["enid"] = id;//我在session里还没设置过期时间
            C_EntCategory temp = (from c in CodeTable.C_EntCategory where c.EntCategoryID == id select c).FirstOrDefault();
            ViewBag.update = temp;

            return View("~/Areas/CodeManager/Views/Code/C_EntCategory/updateEnca.cshtml");

        }
         [BaseActionFilter]
        public ActionResult ReupdateEnca()//接收修改后的信息和数据库进行交互
        {
            string Enid = Request.Form["EntCategoryID"];
            string Enname = Request.Form["EntCategoryName"];
            string id = Convert.ToString(Session["enid"]);
            if (id != Enid)
            {
                return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

            }
            else
            {
                C_EntCategory TEMPP = (from c in CodeTable.C_EntCategory where c.EntCategoryID == id select c).FirstOrDefault();

                try
                {
                    TEMPP.EntCategoryID = Enid;
                    TEMPP.EntCategoryName = Enname;
                    CodeTable.SaveChanges();

                }
                catch (Exception)
                {

                    return Content("<script>alert('这个种类被别的表关联掉了不能修改'); history.go(-1)</script>");
                }
            }

            return Redirect("/CodeManager/Code/C_EntCategory1");

        }
        #endregion 企业类型管理

        #region 企业级别管理

        [BaseActionFilter]
        public ActionResult C_EntRank1()
        {
            var qiye = (from c in CodeTable.C_EntCategory select c).ToList().Distinct();
            ViewBag.qiye = qiye;
            return View("~/Areas/CodeManager/Views/Code/C_EntRank/C_EntRank1.cshtml");
        }
         [BaseActionFilter]
        public ActionResult GetTheDataEntRank()
        {
            //新闻列表分页和查询功能

            //接受DataGrid传来的参数
            //string aaaa = Request.Form[0];

            List<C_EntCategory> CCC = (from v in CodeTable.C_EntCategory select v).ToList();
            List<C_EntRank> CCC12 = (from v in CodeTable.C_EntRank select v).ToList();
            int counthaha = CCC12.Count();

            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            string TheValue = Request["classValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数
            int TotalNum = 0;
            var service = new getEntr();
            var theEnca = service.LoadNewsData(Temp, "nihao", counthaha, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
            var ENR = from b in theEnca select b;

            var result = (from v in ENR
                          from b in CCC
                          where (v.EntCategoryID == b.EntCategoryID)
                          select new { b.EntCategoryID, b.EntCategoryName, v.EntRankID, v.EntRankName, });
            var Result = result.OrderBy(c => c.EntCategoryName);
            if (TheAttribute == "fasong")
            {
                if (TheValue == "--请选择--") { TotalNum = 0; result = null; }
                else
                {
                    C_EntCategory CCC1 = (from v in CodeTable.C_EntCategory where v.EntCategoryName == TheValue select v).FirstOrDefault();
                    string qiyebianhao = CCC1.EntCategoryID;
                    var theEnca1 = service.LoadNewsData(Temp, qiyebianhao, counthaha - 9, out TotalNum);
                    ENR = from b in theEnca1 select b;
                    result = (from v in ENR
                              from b in CCC
                              where (v.EntCategoryID == b.EntCategoryID)
                              select new { b.EntCategoryID, b.EntCategoryName, v.EntRankID, v.EntRankName, });
                }

            }

            /*  #region Category Search



              //依据搜索框传递的属性名和相应值得到搜索出来的模型
              // List<T_Class> GetSpe = CodeTable.T_Class.ToList();



              //var result = from a in theNews from b in GetState where a.FlowState==b.NewsStateID select new { a.NewsID, a.NewsTitle, a.NewsTypeID, a.NewsAuthor, a.NewsPublisher, a.PubDate, b.NewsStateName, a.IsShow, a.IsLocked, a.NewsColunmID, a.PicUrl, a.VideoUrl, a.Html, a.LinkUrl, a.Download };
           
             if (TheAttribute == "EntCategoryID")
              {
                  result = from a in result where (String.IsNullOrEmpty(TheValue) || a.EntCategoryID.Contains(TheValue)) select a;
              }
              if (TheAttribute == "EntCategoryName")
              {
                  result = from a in result where (String.IsNullOrEmpty(TheValue) || a.EntCategoryName.Contains(TheValue)) select a;
              }

              if (TheAttribute == "EntRankID")
              {
                  result = from a in result where (String.IsNullOrEmpty(TheValue) || a.EntRankID.Contains(TheValue)) select a;
              }
              if (TheAttribute == "EntRankName")
              {
                  result = from a in result where (String.IsNullOrEmpty(TheValue) || a.EntRankName.Contains(TheValue)) select a;
              }
              #endregion
             */

            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = Result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串
            //ChangeNewsState c=new ChangeNewsState();
            //string Newstr=c.ChangeState(str);

            return Content(str);
        }

         [BaseActionFilter]
        public ActionResult DeleteEntr()//接受表格传来的信息删除表格信息
        {
            //删除新闻数据

            string Deleteid = Request.Form[0];//获取需要删除的专业编号组成的字符串，且以逗号分割
            string[] SplitArray1 = Deleteid.Split(',');//取到要删除的新闻编号放进数组里

            int ArrayCount = SplitArray1.Count();

            string StringCount = Convert.ToString(ArrayCount);

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string classid = SplitArray1[a];//不能直接把SplitArray1[a]放入linq语句会报错

                try
                {
                    C_EntRank tempspe = (from v in CodeTable.C_EntRank where v.EntRankID == classid select v).FirstOrDefault();
                    CodeTable.C_EntRank.Remove(tempspe);
                    CodeTable.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这条种类信息在别的表被引用了不能删除');history.go(-1)</script>");
                }

            }

            return Content(StringCount);
        }


         [BaseActionFilter]
        public ActionResult AddEntr()//添加新的种类信息
        {
            List<C_EntCategory> TEMP = (from v in CodeTable.C_EntCategory select v).ToList();
            var result = TEMP.OrderBy(v => v.EntCategoryID);//排序不成功。。大写的不开心
            List<string> temp = (from a in CodeTable.C_EntRank select a.EntRankID).ToList();
            int[] entr = new int[temp.Count()];
            for (int i = 0; i < temp.Count(); i++)
            {
                entr[i] = int.Parse(temp[i]);

            }
            int max = entr[0];
            for (int j = 0; j < temp.Count(); j++)
            {
                if (max < entr[j])
                {
                    max = entr[j];

                }

            }
            ViewBag.entrrrrr = max + 1;


            ViewBag.entaid = result;


            return View("~/Areas/CodeManager/Views/Code/C_EntRank/AddEntr.cshtml");

        }

        /*   public ActionResult receajax()//添加新的种类信息
           {
               string sid = Request.Form[0];
               string sname = (from v in CodeTable.C_EntCategory where v.EntCategoryID == sid select v.EntCategoryName).FirstOrDefault();

               return Content(sname);

           }*/
         [BaseActionFilter]
        public ActionResult ReAddEntr()//接受添加种类信息的表单和数据库交互
        {
            string id = Request.Form["EntCategoryID"];
            string name = Request.Form["C_EntRankName"];
            string entid = Request.Form["C_EntRankID"];
            string entaid = Convert.ToString((from d in CodeTable.C_EntCategory where d.EntCategoryName == id select d.EntCategoryID).FirstOrDefault());
            C_EntRank temp = (from v in CodeTable.C_EntRank where v.EntRankID == entid select v).FirstOrDefault();
            if (temp != null)
            {
                return Content("<script>alert('这个等级已经存在'); history.go(-1)</script>");

            }
            else
            {
                // try
                // {
                C_EntRank temp1 = new C_EntRank { EntCategoryID = entaid, EntRankName = name, EntRankID = entid };
                CodeTable.C_EntRank.Add(temp1);
                CodeTable.SaveChanges();
                // }
                //  catch (Exception)
                //   {
                //  return Content("<script>alert('不能添加,警告!'); history.go(-1)</script>");

                // }


            }
            return Redirect("/CodeManager/Code/C_EntRank1");//注意前面那个platform也加上不然会报错找不到视图


        }
         [BaseActionFilter]
        public ActionResult detailEntr(string id)//查看详情
        {
            C_EntRank temp = (from c in CodeTable.C_EntRank where c.EntRankID == id select c).FirstOrDefault();
            string sid = temp.EntCategoryID;
            C_EntCategory temp1 = (from c in CodeTable.C_EntCategory where c.EntCategoryID == sid select c).FirstOrDefault();
            ViewBag.sname = temp1.EntCategoryName;
            ViewBag.detail = temp;



            return View("~/Areas/CodeManager/Views/Code/C_EntRank/detailEntr.cshtml");

        }
         [BaseActionFilter]
        public ActionResult updateEntr(string id)//修改种类信息
        {
            Session["enidd"] = id;//我在session里还没设置过期时间
            var TEMP = (from v in CodeTable.C_EntCategory select v.EntCategoryName).ToList<string>().Distinct();
            C_EntRank temp = (from c in CodeTable.C_EntRank where c.EntRankID == id select c).FirstOrDefault();
            string bn = temp.EntCategoryID;
            string BBBN = (from v in CodeTable.C_EntCategory where v.EntCategoryID == bn select v.EntCategoryName).FirstOrDefault();
            ViewBag.namemm = BBBN;
            ViewBag.update = temp;
            ViewBag.ggggg = TEMP;


            return View("~/Areas/CodeManager/Views/Code/C_EntRank/updateEntr.cshtml");

        }
         [BaseActionFilter]
        public ActionResult ReupdateEntr()//接收修改后的信息和数据库进行交互
        {
            string Enid = Request.Form["EntCategoryID"];//这个其实是企业类型名称
            string Enname = Request.Form["C_EntRankName"];
            string Ertid = Request.Form["C_EntRankID"];
            string id = Convert.ToString(Session["enidd"]);
            if (id != Ertid)
            {
                return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

            }
            else
            {
                C_EntCategory TEMPP = (from c in CodeTable.C_EntCategory where c.EntCategoryName == Enid select c).FirstOrDefault();
                string bb = TEMPP.EntCategoryID;
                C_EntRank TEM = (from c in CodeTable.C_EntRank where c.EntRankID == Ertid select c).FirstOrDefault();
                try
                {
                    TEM.EntRankID = Ertid;
                    TEM.EntRankName = Enname;
                    TEM.EntCategoryID = bb;
                    CodeTable.SaveChanges();

                }
                catch (Exception)
                {

                    return Content("<script>alert('这个种类被别的表关联掉了不能删除'); history.go(-1)</script>");
                }
            }

            return Redirect("/CodeManager/Code/C_EntRank1");

        }

         #endregion 企业级别管理
       
        #region 岗位表管理
        [BaseActionFilter]
         public ActionResult C_Position1()
        {
            return View("~/Areas/CodeManager/Views/Code/C_Position/C_Position1.cshtml");
        }
         [BaseActionFilter]
        public ActionResult GetTheDataPosition()
        {
           

            int pageIndex = int.Parse(Request["page"]);
            int pageSize = 50;

            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            string TheValue = Request["classValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数
            int TotalNum = 0;
            var service = new getPosition();
            var thePosition = service.LoadNewsData(Temp, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
            var result = thePosition.ToList();

            #region Category Search



            

            if (TheAttribute == "PositionID")
            {
                result = result.Where(a=>a.PositionID== TheValue).ToList();
            }
            if (TheAttribute == "PositionName")
            {
                result = result.Where(a => a.PositionName .Contains(TheValue)).ToList();
            }

            #endregion
                

            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = result.Select(a =>
            new {
                PositionID = int.Parse(a.PositionID),
                PositionName = a.PositionName
            }).
                OrderBy(b => b.PositionID).ToList()
            };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串
            //ChangeNewsState c=new ChangeNewsState();
            //string Newstr=c.ChangeState(str);

            return Content(str);
        }

         [BaseActionFilter]
        public ActionResult DeletePosition()//接受表格传来的信息删除表格信息
        {
           

            string Deleteid = Request.Form[0];
            string[] SplitArray1 = Deleteid.Split(',');

            int ArrayCount = SplitArray1.Count();

            string StringCount = Convert.ToString(ArrayCount);

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string classid = SplitArray1[a];

                try
                {
                    C_Position tempspe = (from v in CodeTable.C_Position where v.PositionID == classid select v).FirstOrDefault();
                    CodeTable.C_Position.Remove(tempspe);
                    CodeTable.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这条种类信息在别的表被引用了不能删除');history.go(-1)</script>");
                }

            }

            return Content(StringCount);
        }


         [BaseActionFilter]
        public ActionResult AddPosition()//添加新的种类信息
        {
            return View("~/Areas/CodeManager/Views/Code/C_Position/AddPosition.cshtml");

        }
         [BaseActionFilter]
        public ActionResult ReAddPosition()//接受添加种类信息的表单和数据库交互
        {
            string id = Request.Form["PositionID"];
            string name = Request.Form["PositionName"];
            C_Position temp = (from v in CodeTable.C_Position where v.PositionID == id select v).FirstOrDefault();
            if (temp != null)
            {
                return Content("<script>alert('这个种类已经存在'); history.go(-1)</script>");

            }
            else
            {
                C_Position temp1 = new C_Position { PositionID = id, PositionName = name };
                CodeTable.C_Position.Add(temp1);
                CodeTable.SaveChanges();

            }
            return Redirect("/CodeManager/Code/C_Position1");//注意前面那个platform也加上不然会报错找不到视图


        }
         [BaseActionFilter]
        public ActionResult detailPosition(string id)//查看详情
        {
            C_Position temp = (from c in CodeTable.C_Position where c.PositionID == id select c).FirstOrDefault();
            ViewBag.detail = temp;

            return View("~/Areas/CodeManager/Views/Code/C_Position/detailPosition.cshtml");

        }
         [BaseActionFilter]

        public ActionResult updatePosition(string id)//修改种类信息
        {
            Session["positionid"] = id;//我在session里还没设置过期时间
            C_Position temp = (from c in CodeTable.C_Position where c.PositionID == id select c).FirstOrDefault();
            ViewBag.update = temp;

            return View("~/Areas/CodeManager/Views/Code/C_Position/updatePosition.cshtml");

        }
         [BaseActionFilter]
        public ActionResult ReupdatePosition()//接收修改后的信息和数据库进行交互
        {
            string Enid = Request.Form["PositionID"];
            string Enname = Request.Form["PositionName"];
            string id = Convert.ToString(Session["positionid"]);
            if (id != Enid)
            {
                return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

            }
            else
            {
                C_Position TEMPP = (from c in CodeTable.C_Position where c.PositionID == id select c).FirstOrDefault();

                try
                {
                    TEMPP.PositionID = Enid;
                    TEMPP.PositionName = Enname;
                    CodeTable.SaveChanges();

                }
                catch (Exception)
                {

                    return Content("<script>alert('这个种类被别的表关联掉了不能删除'); history.go(-1)</script>");
                }
            }

            return Redirect("/CodeManager/Code/C_Position1");

        }


        #endregion 岗位表管理

        #region 职称表管理
        [BaseActionFilter]
        public ActionResult C_ProPosition1()
        {
            return View("~/Areas/CodeManager/Views/Code/C_ProPosition/C_ProPosition1.cshtml");
        }
         [BaseActionFilter]
        public ActionResult GetTheDataProPosition()
        {
            //新闻列表分页和查询功能

            //接受DataGrid传来的参数

            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            string TheValue = Request["classValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数
            int TotalNum = 0;
            var service = new getProPosition();
            var theEnca = service.LoadNewsData(Temp, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
            var result = from b in theEnca select b;

            #region Category Search



            //依据搜索框传递的属性名和相应值得到搜索出来的模型
            // List<T_Class> GetSpe = CodeTable.T_Class.ToList();



            //var result = from a in theNews from b in GetState where a.FlowState==b.NewsStateID select new { a.NewsID, a.NewsTitle, a.NewsTypeID, a.NewsAuthor, a.NewsPublisher, a.PubDate, b.NewsStateName, a.IsShow, a.IsLocked, a.NewsColunmID, a.PicUrl, a.VideoUrl, a.Html, a.LinkUrl, a.Download };

            if (TheAttribute == "ProPosID")
            {
                int ivalue = Convert.ToInt32(TheValue);
                result = from a in result where (String.IsNullOrEmpty(TheValue) || a.ProPosID == ivalue) select a;
            }
            if (TheAttribute == "ProPosName")
            {
                result = from a in result where (String.IsNullOrEmpty(TheValue) || a.ProPosName.Contains(TheValue)) select a;
            }

            #endregion


            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串
            //ChangeNewsState c=new ChangeNewsState();
            //string Newstr=c.ChangeState(str);

            return Content(str);
        }

         [BaseActionFilter]
        public ActionResult DeleteProPosition()//接受表格传来的信息删除表格信息
        {
            //删除新闻数据

            string Deleteid = Request.Form[0];//获取需要删除的专业编号组成的字符串，且以逗号分割
            string[] SplitArray1 = Deleteid.Split(',');//取到要删除的新闻编号放进数组里

            int ArrayCount = SplitArray1.Count();

            string StringCount = Convert.ToString(ArrayCount);

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string classid = SplitArray1[a];//不能直接把SplitArray1[a]放入linq语句会报错
                int intid = Convert.ToInt32(classid);

                try
                {
                    C_ProPosition tempspe = (from v in CodeTable.C_ProPosition where v.ProPosID == intid select v).FirstOrDefault();
                    CodeTable.C_ProPosition.Remove(tempspe);
                    CodeTable.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这条职称信息在别的表被引用了不能删除');history.go(-1)</script>");
                }

            }

            return Content(StringCount);
        }


         [BaseActionFilter]
        public ActionResult AddProPosition()//添加新的种类信息
        {
            return View("~/Areas/CodeManager/Views/Code/C_ProPosition/AddProPosition.cshtml");

        }
         [BaseActionFilter]
        public ActionResult ReAddProPosition()//接受添加种类信息的表单和数据库交互
        {
            string id1 = Request.Form["ProPosID"];
            int id222 = Convert.ToInt32(id1);
            string name = Request.Form["ProPosName"];
            if (id1.Count() != 4)
            {
                return Content("<script>alert('请输入四个数字的职称编号'); history.go(-1)</script>");
            }
            else
            {
                C_ProPosition temp = (from v in CodeTable.C_ProPosition where v.ProPosID == id222 select v).FirstOrDefault();
                if (temp != null)
                {
                    return Content("<script>alert('这个职称已经存在'); history.go(-1)</script>");

                }
                else
                {
                    /*C_ProPosition temp22 = new C_ProPosition{ ProPosID = id222, ProPosName = name};
                    CodeTable.C_ProPosition.Add(temp22);
                    CodeTable.SaveChanges();*/
                    CodeTable.Database.ExecuteSqlCommand("insert into C_ProPosition (ProPosID,ProPosName) values(" + id222 + ",'" + name + "')");
                    

                }
                return Redirect("/CodeManager/Code/C_ProPosition1");//注意前面那个platform也加上不然会报错找不到视图
            }
              
        }
         [BaseActionFilter]
        public ActionResult detailProPosition(int id)//查看详情
        {
            C_ProPosition temp = (from c in CodeTable.C_ProPosition where c.ProPosID == id select c).FirstOrDefault();
            ViewBag.detail = temp;

            return View("~/Areas/CodeManager/Views/Code/C_ProPosition/detailProPosition.cshtml");

        }
         [BaseActionFilter]

        public ActionResult updateProPosition(int id)//修改种类信息
        {
            Session["ProPositionid"] = id;//我在session里还没设置过期时间
            C_ProPosition temp = (from c in CodeTable.C_ProPosition where c.ProPosID == id select c).FirstOrDefault();
            ViewBag.update = temp;

            return View("~/Areas/CodeManager/Views/Code/C_ProPosition/updateProPosition.cshtml");

        }
         [BaseActionFilter]
        public ActionResult ReupdateProPosition()//接收修改后的信息和数据库进行交互
        {
            string Enid = Request.Form["ProPosID"];
            int enidint = Convert.ToInt32(Enid);
            string Enname = Request.Form["ProPosName"];
            string id = Convert.ToString(Session["ProPositionid"]);
            if (id != Enid)
            {
                return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

            }
            else
            {
                C_ProPosition TEMPP = (from c in CodeTable.C_ProPosition where c.ProPosID == enidint select c).FirstOrDefault();

                try
                {
                    TEMPP.ProPosID = enidint;
                    TEMPP.ProPosName = Enname;
                    CodeTable.SaveChanges();

                }
                catch (Exception)
                {

                    return Content("<script>alert('这个职称被别的表关联掉了不能修改'); history.go(-1)</script>");
                }
            }

            return Redirect("/CodeManager/Code/C_ProPosition1");

        }


        #endregion 职称表管理
        
         
        
        
        #region 申请状态表管理
        public ActionResult C_ApplyStatus1()
        {
            return View("~/Areas/CodeManager/Views/Code/C_ApplyStatus/C_ApplyStatus1.cshtml");
        }
        public ActionResult GetTheDataApplyStatus()
        {
            //新闻列表分页和查询功能

            //接受DataGrid传来的参数

            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            string TheValue = Request["classValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数
            int TotalNum = 0;
            var service = new getApplyStatus();
            var theEnca = service.LoadNewsData(Temp, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
            var result = from b in theEnca select b;

            #region Category Search



            //依据搜索框传递的属性名和相应值得到搜索出来的模型
            // List<T_Class> GetSpe = CodeTable.T_Class.ToList();



            //var result = from a in theNews from b in GetState where a.FlowState==b.NewsStateID select new { a.NewsID, a.NewsTitle, a.NewsTypeID, a.NewsAuthor, a.NewsPublisher, a.PubDate, b.NewsStateName, a.IsShow, a.IsLocked, a.NewsColunmID, a.PicUrl, a.VideoUrl, a.Html, a.LinkUrl, a.Download };

            if (TheAttribute == "ApplyStatusID")
            {
                int ivalue = Convert.ToInt32(TheValue);
                result = from a in result where (String.IsNullOrEmpty(TheValue) || a.ApplyStatusID == ivalue) select a;
            }
            if (TheAttribute == "ApplyStatusName")
            {
                result = from a in result where (String.IsNullOrEmpty(TheValue) || a.ApplyStatusName.Contains(TheValue)) select a;
            }

            #endregion


            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串
            //ChangeNewsState c=new ChangeNewsState();
            //string Newstr=c.ChangeState(str);

            return Content(str);
        }


        public ActionResult DeleteApplyStatus()//接受表格传来的信息删除表格信息
        {
            //删除新闻数据

            string Deleteid = Request.Form[0];//获取需要删除的专业编号组成的字符串，且以逗号分割
            string[] SplitArray1 = Deleteid.Split(',');//取到要删除的新闻编号放进数组里

            int ArrayCount = SplitArray1.Count();

            string StringCount = Convert.ToString(ArrayCount);

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string classid = SplitArray1[a];//不能直接把SplitArray1[a]放入linq语句会报错
                int intid = Convert.ToInt32(classid);

                try
                {
                    C_ApplyStatus tempspe = (from v in CodeTable.C_ApplyStatus where v.ApplyStatusID == intid select v).FirstOrDefault();
                    CodeTable.C_ApplyStatus.Remove(tempspe);
                    CodeTable.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这条申请状态信息在别的表被引用了不能删除');history.go(-1)</script>");
                }

            }

            return Content(StringCount);
        }



        public ActionResult AddApplyStatus()//添加新的种类信息
        {
            return View("~/Areas/CodeManager/Views/Code/C_ApplyStatus/AddApplyStatus.cshtml");

        }
        public ActionResult ReAddApplyStatus()//接受添加种类信息的表单和数据库交互
        {
            string id1 = Request.Form["ApplyStatusID"];
            int id222 = Convert.ToInt32(id1);
            string name = Request.Form["ApplyStatusName"];
            if (id1.Count() > 4)
            {
                return Content("<script>alert('请输入小于四个数字的申请状态编号'); history.go(-1)</script>");
            }


            C_ApplyStatus temp = (from v in CodeTable.C_ApplyStatus where v.ApplyStatusID == id222 select v).FirstOrDefault();
            if (temp != null)
            {
                return Content("<script>alert('这个申请状态已经存在'); history.go(-1)</script>");

            }
            else
            {
                /* C_ApplyStatus TEMP1 = new C_ApplyStatus { ApplyStatusID = 16, ApplyStatusName = name };
                 CodeTable.C_ApplyStatus.Add(TEMP1);
                 CodeTable.SaveChanges();
*/
                CodeTable.Database.ExecuteSqlCommand("insert into C_ApplyStatus (ApplyStatusID,ApplyStatusName) values(" + id222 + ",'" + name + "')");
               
            }
            return Redirect("/CodeManager/Code/C_ApplyStatus1");//注意前面那个platform也加上不然会报错找不到视图



        }
        public ActionResult detailApplyStatus(int id)//查看详情
        {
            //int id= Convert.ToInt32(id1);
            C_ApplyStatus temp = (from c in CodeTable.C_ApplyStatus where c.ApplyStatusID == id select c).FirstOrDefault();
            ViewBag.detail = temp;

            return View("~/Areas/CodeManager/Views/Code/C_ApplyStatus/detailApplyStatus.cshtml");

        }

        public ActionResult updateApplyStatus(int id)//修改种类信息
        {
            //int id = Convert.ToInt32(id1);
            Session["ApplyStatusid"] = id;//我在session里还没设置过期时间
            C_ApplyStatus temp = (from c in CodeTable.C_ApplyStatus where c.ApplyStatusID == id select c).FirstOrDefault();
            ViewBag.update = temp;

            return View("~/Areas/CodeManager/Views/Code/C_ApplyStatus/updateApplyStatus.cshtml");

        }
        public ActionResult ReupdateApplyStatus()//接收修改后的信息和数据库进行交互
        {
            string Enid = Request.Form["ApplyStatusID"];
            int enidint = Convert.ToInt32(Enid);
            string Enname = Request.Form["ApplyStatusName"];
            string id = Convert.ToString(Session["ApplyStatusid"]);
            if (id != Enid)
            {
                return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

            }
            else
            {
                C_ApplyStatus TEMPP = (from c in CodeTable.C_ApplyStatus where c.ApplyStatusID == enidint select c).FirstOrDefault();

                try
                {
                    TEMPP.ApplyStatusID = enidint;
                    TEMPP.ApplyStatusName = Enname;
                    CodeTable.SaveChanges();

                }
                catch (Exception)
                {

                    return Content("<script>alert('这个申请状态被别的表关联掉了不能修改'); history.go(-1)</script>");
                }
            }

            return Redirect("/CodeManager/Code/C_ApplyStatus1");

        }

         #endregion 申请状态表管理



        
        #region 学生评价学校评估表管理

        /*
         首先批次号从批次表里取出来，就一开始上面批次的选择框和下面的选择框批次号需要约束一下是登陆者的单位（学校）
         其次关于复制我默认的会从当中找出是全空的批次才会出现在选择框当中
         * 这里因为会有身份过滤所以只有一所学校的时候BatchName和批次号都是一对一的，因此记得每次两个列互相查找的时候要加个schoolID的条件
         
         */
         [BaseActionFilter]
        public ActionResult C_StuEvaluateSchoolItem1()
        {
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
             EnterpriseContext CodeTable1 = new EnterpriseContext();
             List<T_PracBatch> temp = (from v in CodeTable1.PracBatch where v.SchoolID==ShenFenID select v).ToList();//身份验证取出该学校所有的批次号
             ViewBag.piciid = temp;

            return View("~/Areas/CodeManager/Views/Code/C_StuEvaluateSchoolItem/C_StuEvaluateSchoolItem1.cshtml");
        }
        [BaseActionFilter]
        public ActionResult GetTheDataStuEvaluateSchoolItem()
        {
            
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();//获取当前登录的用户身份信息（比如这个是学校ID）
            List<C_StuEvaluateSchoolItem> ccc = (from v in CodeTable.C_StuEvaluateSchoolItem select v).ToList();
            int counthaha = ccc.Count();
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            string TheValue = Request["classValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数

            int TotalNum = 0;



            var service = new getStuEvaluateSchoolItem();
            var theEnca = service.LoadNewsData(Temp, ShenFenID, counthaha, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
            var result = from b in theEnca select b;



            if (TheAttribute == "fasong")
            {
                EnterpriseContext CodeTable1 = new EnterpriseContext();
                T_PracBatch temp123 = (from v in CodeTable1.PracBatch where v.BatchName == TheValue select v).FirstOrDefault();
                string CityIDS = temp123.PracBatchID;
                
                theEnca = service.LoadNewsData(Temp, CityIDS, counthaha - 9, out TotalNum);
                result = from b in theEnca select b;

            }




            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串
            //ChangeNewsState c=new ChangeNewsState();
            //string Newstr=c.ChangeState(str);

            return Content(str);
        }

        [BaseActionFilter]
        public ActionResult receajaxUnsetstuEvaluateSchoolitem()
        {
            string Alloption = Request.Form[0];//获取所有option的值然后与这里的值作比较，如果一样就不改变
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            var temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v.PracBatchID).ToList().Distinct<string>();
           
            
            string[] TTT1 = new string[150];//用来存放没有条目的批次号
            int i = 0;
            string TTT11 = "";
            foreach (var c in temp)
            {
                C_StuEvaluateSchoolItem temp1 = (from q in CodeTable.C_StuEvaluateSchoolItem where q.PracBatchID == c select q).FirstOrDefault();
                if (temp1 == null) {
                    string temp123 = (from a in CodeTable1.PracBatch where a.PracBatchID == c select a.BatchName).FirstOrDefault();
                    TTT1[i] += temp123;
                    i++;

                }
            }

    
          
                for (int j = 0; j < i; j++)
                {
                    TTT11 += TTT1[j] + ",";
                }


                if (Alloption != TTT11)
                {
                    return Content(TTT11);
                }

                else
                {

                    return Content("Same");
                }
            }

        [BaseActionFilter]
        public ActionResult receajaxFuzhistuEvaluateSchoolitem()
        {

            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            string PidToName = Request.Form[0];//要复制到这个批次号下面
            string PidBeiName = Request.Form[1];//被复制的那个批次号


            string PidTo = (from v in CodeTable1.PracBatch where v.BatchName == PidToName && v.SchoolID == ShenFenID select v.PracBatchID).FirstOrDefault();
            string PidBei = (from v in CodeTable1.PracBatch where v.BatchName == PidBeiName && v.SchoolID == ShenFenID select v.PracBatchID).FirstOrDefault();

            var temps = (from v in CodeTable.C_StuEvaluateSchoolItem where v.PracBatchID == PidBei select v).ToList();
            int count1 = temps.Count();
            int jishu = 0;
            foreach (var c in temps) {
              
                  int  ItemSequence1=c.ItemSequence;
                  string  ItemName=c.ItemName;
                  string PracBatchID=PidTo;
                  string ItemNo = PidTo + "-" + ItemSequence1;
                C_StuEvaluateSchoolItem temp22 = new C_StuEvaluateSchoolItem { ItemNo = ItemNo, ItemSequence = ItemSequence1, ItemName = ItemName, PracBatchID = PracBatchID };
                CodeTable.C_StuEvaluateSchoolItem.Add(temp22);
                if (CodeTable.SaveChanges()>0) {
                    jishu++;
                
                }
            
            }





            
            if (jishu  == count1)
            {
                string spename = "chenggong";
                return Content(spename);
            }
            else
            {
                string sfalse = "shibai";
                return Content(sfalse);
            }

        }



        [BaseActionFilter]
       public ActionResult DeleteStuEvaluateSchoolItem()//接受表格传来的信息删除表格信息
        {
            //删除新闻数据

            string Deleteid = Request.Form[0];//获取需要删除的专业编号组成的字符串，且以逗号分割
            string[] SplitArray1 = Deleteid.Split(',');//取到要删除的新闻编号放进数组里

            int ArrayCount = SplitArray1.Count();

            string StringCount = Convert.ToString(ArrayCount);

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string classid = SplitArray1[a];//不能直接把SplitArray1[a]放入linq语句会报错
               // int intid = Convert.ToInt32(classid);

                try
                {
                    C_StuEvaluateSchoolItem tempspe = (from v in CodeTable.C_StuEvaluateSchoolItem where v.ItemNo == classid select v).FirstOrDefault();
                    CodeTable.C_StuEvaluateSchoolItem.Remove(tempspe);
                    CodeTable.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这条条目信息在别的表被引用了不能删除');history.go(-1)</script>");
                }

            }

            return Content(StringCount);
        }

        [BaseActionFilter]
       public ActionResult receajaxGetstuEvaluateSchoolitemPID() {
           string batchname = Request.Form[0];
           string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
           EnterpriseContext CodeTable1 = new EnterpriseContext();
           string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == batchname select v.PracBatchID).FirstOrDefault();

           return Content(PracBatchID);
       }
        [BaseActionFilter]
        public ActionResult AddStuEvaluateSchoolItem()//添加新的种类信息
       {
          
           string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            List<T_PracBatch> temp = (from v in CodeTable1.PracBatch  where v.SchoolID==ShenFenID select v).ToList();
            ViewBag.piciid = temp;
            return View("~/Areas/CodeManager/Views/Code/C_StuEvaluateSchoolItem/AddStuEvaluateSchoolItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReAddStuEvaluateSchoolItem()//接受添加种类信息的表单和数据库交互
        {
            string ItemNo = Request.Form["ItemNo"];
            string BatchName = Request.Form["BatchName"];//获取批次名的同时通过下面的几句转换成批次号
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == BatchName select v.PracBatchID).FirstOrDefault();
            string ItemName = Request.Form["ItemName"];
            string ItemSequence = Request.Form["ItemSequence"];
            int ItemSequence1 = Convert.ToInt32(ItemSequence);
            int ItemPower = Convert.ToInt32(Request.Form["ItemPower"]);
            


            C_StuEvaluateSchoolItem temp = (from v in CodeTable.C_StuEvaluateSchoolItem where v.ItemNo == ItemNo select v).FirstOrDefault();
            if (temp != null)
            {
                return Content("<script>alert('这个条目已经存在'); history.go(-1)</script>");

            }
            else
            {
                if (ItemPower < 0 || ItemPower > 100) { return Content("<script>alert('条目权重请输入0到100之间的数字'); history.go(-1)</script>"); }
                else {
                C_StuEvaluateSchoolItem temp22 = new C_StuEvaluateSchoolItem { ItemNo = ItemNo, ItemSequence = ItemSequence1, ItemName = ItemName, PracBatchID = PracBatchID, ItemPower = ItemPower };
                CodeTable.C_StuEvaluateSchoolItem.Add(temp22);
                CodeTable.SaveChanges();

             

                return Redirect("/CodeManager/Code/C_StuEvaluateSchoolItem1");//注意前面那个platform也加上不然会报错找不到视图
            }
            }

        }
        [BaseActionFilter]
        public ActionResult detailStuEvaluateSchoolItem(string id)//查看详情
        {
            C_StuEvaluateSchoolItem temp = (from c in CodeTable.C_StuEvaluateSchoolItem where c.ItemNo == id select c).FirstOrDefault();
            ViewBag.detail = temp;
             EnterpriseContext CodeTable1 = new EnterpriseContext();
             string temp2 = temp.PracBatchID;
             string batchname = (from v in CodeTable1.PracBatch where v.PracBatchID == temp2 select v.BatchName).FirstOrDefault();
            ViewBag.batchName = batchname;

            return View("~/Areas/CodeManager/Views/Code/C_StuEvaluateSchoolItem/detailStuEvaluateSchoolItem.cshtml");

        }
        [BaseActionFilter]

        public ActionResult updateStuEvaluateSchoolItem(string id)//修改种类信息
        {



            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            Session["itemno"] = id;//我在session里还没设置过期时间
            C_StuEvaluateSchoolItem temp = (from c in CodeTable.C_StuEvaluateSchoolItem where c.ItemNo == id select c).FirstOrDefault();
            ViewBag.update1 = temp;//该条条目的全部信息
            string updateid = (from c in CodeTable.C_StuEvaluateSchoolItem where c.ItemNo == id select c.PracBatchID).FirstOrDefault();
            List<T_PracBatch> temp1 = (from v in CodeTable1.PracBatch where v.SchoolID==ShenFenID select v).ToList();
            string temp2 = (from v in CodeTable1.PracBatch where v.PracBatchID == updateid select v.BatchName).FirstOrDefault();
            
            ViewBag.update = temp2;
            ViewBag.piciid = temp1;
            return View("~/Areas/CodeManager/Views/Code/C_StuEvaluateSchoolItem/updateStuEvaluateSchoolItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReupdateStuEvaluateSchoolItem()//接收修改后的信息和数据库进行交互
        {
            string ItemNo = Request.Form["ItemNo"];
            string BatchName = Request.Form["BatchName"];//获取批次名的同时通过下面的几句转换成批次号
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == BatchName select v.PracBatchID).FirstOrDefault();
            string ItemName = Request.Form["ItemName"];
            string ItemSequence = Request.Form["ItemSequence"];
            int ItemSequence1 = Convert.ToInt32(ItemSequence);
            int ItemPower = Convert.ToInt32(Request.Form["ItemPower"]);
            string id = Convert.ToString(Session["itemno"]);
            if (id != ItemNo)
            {
                return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

            }
            else
            {
                if (ItemPower < 0 || ItemPower > 100) { return Content("<script>alert('条目权重请输入0到100之间的数字'); history.go(-1)</script>"); }

                else {  C_StuEvaluateSchoolItem TEMPP = (from c in CodeTable.C_StuEvaluateSchoolItem where c.ItemNo == ItemNo select c).FirstOrDefault();

                try
                {
                    TEMPP.PracBatchID = PracBatchID;
                    TEMPP.ItemName = ItemName;
                    TEMPP.ItemSequence = ItemSequence1;
                    TEMPP.ItemPower = ItemPower;
                    CodeTable.SaveChanges();

                }
                catch (Exception)
                {

                    return Content("<script>alert('这个条目被别的表关联掉了不能修改'); history.go(-1)</script>");
                }
                }
            }

            return Redirect("/CodeManager/Code/C_StuEvaluateSchoolItem1");

        }

        #endregion  学生评价学校评估表管理

        #region 学生评价学校评估等级表管理

        [BaseActionFilter]
        public ActionResult C_StuEvaSchoolGradeLevelItem1()
        {
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            List<T_PracBatch> temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();//身份验证取出该学校所有的批次号
            ViewBag.piciid = temp;

            return View("~/Areas/CodeManager/Views/Code/C_StuEvaSchoolGradeLevelItem/C_StuEvaSchoolGradeLevelItem1.cshtml");
        }
        [BaseActionFilter]
        public ActionResult GetTheDataStuEvaSchoolGradeLevelItem()
        {

            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();//获取当前登录的用户身份信息（比如这个是学校ID）
            List<C_StuEvaSchoolGradeLevelItem> ccc = (from v in CodeTable.C_StuEvaSchoolGradeLevelItem select v).ToList();
            int counthaha = ccc.Count();
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            string TheValue = Request["classValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数

            int TotalNum = 0;



            var service = new GetStuEvaSchoolGradeLevelItem();
            var theEnca = service.LoadNewsData(Temp, ShenFenID, counthaha, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
            var result = from b in theEnca select b;



            if (TheAttribute == "fasong")
            {
                EnterpriseContext CodeTable1 = new EnterpriseContext();
                T_PracBatch temp123 = (from v in CodeTable1.PracBatch where v.BatchName == TheValue select v).FirstOrDefault();
                string CityIDS = temp123.PracBatchID;

                theEnca = service.LoadNewsData(Temp, CityIDS, counthaha - 9, out TotalNum);
                result = from b in theEnca select b;

            }




            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串
            //ChangeNewsState c=new ChangeNewsState();
            //string Newstr=c.ChangeState(str);

            return Content(str);
        }

        [BaseActionFilter]
        public ActionResult receajaxUnsetStuEvaSchoolGradeLevelItem()
        {
           string Alloption = Request.Form[0];//获取所有option的值然后与这里的值作比较，如果一样就不改变
            //string result = System.Text.RegularExpressions.Regex.Replace(Alloption, "--请选择--", "");
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            var temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v.PracBatchID).ToList().Distinct<string>();


            string[] TTT1 = new string[150];//用来存放没有条目的批次号
            int i = 0;
            string TTT11 = "";
            foreach (var c in temp)
            {
                C_StuEvaSchoolGradeLevelItem temp1 = (from q in CodeTable.C_StuEvaSchoolGradeLevelItem where q.PracBatchID == c select q).FirstOrDefault();
                if (temp1 == null)
                {
                    string temp123 = (from a in CodeTable1.PracBatch where a.PracBatchID == c select a.BatchName).FirstOrDefault();
                    TTT1[i] += temp123;
                    i++;

                }
            }



            for (int j = 0; j < i; j++)
            {
                TTT11 += TTT1[j] + ",";
            }

            //string  TTT111 = "," + TTT11;

            if (Alloption != TTT11)
            {
                return Content(TTT11);
            }

            else { 
            
            return Content("Same");
            }
        }

        [BaseActionFilter]
        public ActionResult receajaxFuzhiStuEvaSchoolGradeLevelItem()
        {

            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            string PidToName = Request.Form[0];//要复制到这个批次号下面
            string PidBeiName = Request.Form[1];//被复制的那个批次号


            string PidTo = (from v in CodeTable1.PracBatch where v.BatchName == PidToName && v.SchoolID == ShenFenID select v.PracBatchID).FirstOrDefault();
            string PidBei = (from v in CodeTable1.PracBatch where v.BatchName == PidBeiName && v.SchoolID == ShenFenID select v.PracBatchID).FirstOrDefault();

            var temps = (from v in CodeTable.C_StuEvaSchoolGradeLevelItem where v.PracBatchID == PidBei select v).ToList();
            int count1 = temps.Count();
            int jishu = 0;
            foreach (var c in temps)
            {

                int ItemSequence1 = c.ItemSequence;
                string ItemName = c.ItemName;
                string PracBatchID = PidTo;
                string ItemNo = PidTo + "-" + ItemSequence1;
                int Weight =Convert.ToInt32(c.Weight);
                string Note = c.Note;
                C_StuEvaSchoolGradeLevelItem temp22 = new C_StuEvaSchoolGradeLevelItem { ItemNo = ItemNo, ItemSequence = ItemSequence1, ItemName = ItemName, PracBatchID = PracBatchID,Weight=Weight,Note=Note };
                CodeTable.C_StuEvaSchoolGradeLevelItem.Add(temp22);
                if (CodeTable.SaveChanges() > 0)
                {
                    jishu++;

                }

            }






            if (jishu == count1)
            {
                string spename = "chenggong";
                return Content(spename);
            }
            else
            {
                string sfalse = "shibai";
                return Content(sfalse);
            }

        }



        [BaseActionFilter]
        public ActionResult DeleteStuEvaSchoolGradeLevelItem()//接受表格传来的信息删除表格信息
        {
            //删除新闻数据

            string Deleteid = Request.Form[0];//获取需要删除的专业编号组成的字符串，且以逗号分割
            string[] SplitArray1 = Deleteid.Split(',');//取到要删除的新闻编号放进数组里

            int ArrayCount = SplitArray1.Count();

            string StringCount = Convert.ToString(ArrayCount);

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string classid = SplitArray1[a];//不能直接把SplitArray1[a]放入linq语句会报错
                // int intid = Convert.ToInt32(classid);

                try
                {
                    C_StuEvaSchoolGradeLevelItem tempspe = (from v in CodeTable.C_StuEvaSchoolGradeLevelItem where v.ItemNo == classid select v).FirstOrDefault();
                    CodeTable.C_StuEvaSchoolGradeLevelItem.Remove(tempspe);
                    CodeTable.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这条条目信息在别的表被引用了不能删除');history.go(-1)</script>");
                }

            }

            return Content(StringCount);
        }


        [BaseActionFilter]
        public ActionResult receajaxGetstuEvaSchoolitemgradePID()
        {
           string batchname = Request.Form[0];
           string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
           EnterpriseContext CodeTable1 = new EnterpriseContext();
           string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == batchname select v.PracBatchID).FirstOrDefault();

           return Content(PracBatchID);
       }
        [BaseActionFilter]
        public ActionResult AddStuEvaSchoolGradeLevelItem()//添加新的种类信息
        {

            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            List<T_PracBatch> temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();

            ViewBag.piciid = temp;
            return View("~/Areas/CodeManager/Views/Code/C_StuEvaSchoolGradeLevelItem/AddStuEvaSchoolGradeLevelItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReAddStuEvaSchoolGradeLevelItem()//接受添加种类信息的表单和数据库交互
        {
            string ItemNo = Request.Form["ItemNo"];
            string BatchName = Request.Form["BatchName"];//获取批次名的同时通过下面的几句转换成批次号
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == BatchName select v.PracBatchID).FirstOrDefault();
            string ItemName = Request.Form["ItemName"];
            string ItemSequence = Request.Form["ItemSequence"];
            int Weight =Convert.ToInt32( Request.Form["Weight"]);
            string Note = Request.Form["Note"];
            int ItemSequence1 = Convert.ToInt32(ItemSequence);
           
            C_StuEvaSchoolGradeLevelItem temp = (from v in CodeTable.C_StuEvaSchoolGradeLevelItem where v.ItemNo == ItemNo select v).FirstOrDefault();
            if (temp != null)
            {
                return Content("<script>alert('这个条目已经存在'); history.go(-1)</script>");

            }
            else
            {
                if (Weight < 0 || Weight > 100) { return Content("<script>alert('条目权重的值为0到100的数字'); history.go(-1)</script>"); }
                else {
                C_StuEvaSchoolGradeLevelItem temp22 = new C_StuEvaSchoolGradeLevelItem { ItemNo = ItemNo, ItemSequence = ItemSequence1, ItemName = ItemName, PracBatchID = PracBatchID,Weight=Weight,Note=Note };
                CodeTable.C_StuEvaSchoolGradeLevelItem.Add(temp22);
                CodeTable.SaveChanges();

                /* string Connectstr = "Data Source=120.24.91.132;Initial Catalog=Platform;Persist Security Info=True;Uid=sa;password=Hqu33333";
                 SqlConnection conn = new SqlConnection(Connectstr);
                 string strSQL = "insert into C_ProPosition (ProPosID,ProPosName) values(" + id222 + ",'" + name + "')";
                 conn.Open();
                 try
                 {
                     SqlCommand cmd = new SqlCommand(strSQL, conn);
                     cmd.ExecuteNonQuery();
                 }
                 catch (SqlException ex)
                 {
                     //...System.Data.SqlClient.SqlException: “microsoft”附近有语法错误
                 }
                 conn.Close();
                 */

                return Redirect("/CodeManager/Code/C_StuEvaSchoolGradeLevelItem1");//注意前面那个platform也加上不然会报错找不到视图
            }
            }
        }
        [BaseActionFilter]
        public ActionResult detailStuEvaSchoolGradeLevelItem(string id)//查看详情
        {
            C_StuEvaSchoolGradeLevelItem temp = (from c in CodeTable.C_StuEvaSchoolGradeLevelItem where c.ItemNo == id select c).FirstOrDefault();
            ViewBag.detail = temp;
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string temp2 = temp.PracBatchID;
            string batchname = (from v in CodeTable1.PracBatch where v.PracBatchID == temp2 select v.BatchName).FirstOrDefault();
            ViewBag.batchName = batchname;

            return View("~/Areas/CodeManager/Views/Code/C_StuEvaSchoolGradeLevelItem/detailStuEvaSchoolGradeLevelItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult updateStuEvaSchoolGradeLevelItem(string id)//修改种类信息
        {
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            Session["itemno"] = id;//我在session里还没设置过期时间
            string updateid = (from c in CodeTable.C_StuEvaSchoolGradeLevelItem where c.ItemNo == id select c.PracBatchID).FirstOrDefault();
            
            
           
            List<T_PracBatch> temp1 = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();
            string temp2 = (from v in CodeTable1.PracBatch where v.PracBatchID == updateid select v.BatchName).FirstOrDefault();
            C_StuEvaSchoolGradeLevelItem TEMP3 = (from v in CodeTable.C_StuEvaSchoolGradeLevelItem where v.ItemNo == id select v).FirstOrDefault();
            ViewBag.update1 = TEMP3;

            ViewBag.update = temp2;
            ViewBag.piciid = temp1;
            return View("~/Areas/CodeManager/Views/Code/C_StuEvaSchoolGradeLevelItem/updateStuEvaSchoolGradeLevelItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReupdateStuEvaSchoolGradeLevelItem()//接收修改后的信息和数据库进行交互
        {
            string ItemNo = Request.Form["ItemNo"];
            string BatchName = Request.Form["BatchName"];//获取批次名的同时通过下面的几句转换成批次号
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == BatchName select v.PracBatchID).FirstOrDefault();
            string ItemName = Request.Form["ItemName"];
            string ItemSequence = Request.Form["ItemSequence"];
            int ItemSequence1 = Convert.ToInt32(ItemSequence);
            int Weight = Convert.ToInt32(Request.Form["Weight"]);
            string Note = Request.Form["Note"];

            string id = Convert.ToString(Session["itemno"]);
            if (id != ItemNo)
            {
                return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

            }
            else
            {
                if (Weight < 0 || Weight > 100) { return Content("<script>alert('条目权重的值为0到100的数字'); history.go(-1)</script>"); }
                else { 
                C_StuEvaSchoolGradeLevelItem TEMPP = (from c in CodeTable.C_StuEvaSchoolGradeLevelItem where c.ItemNo == ItemNo select c).FirstOrDefault();

                try
                {
                    TEMPP.PracBatchID = PracBatchID;
                    TEMPP.ItemName = ItemName;
                    TEMPP.ItemSequence = ItemSequence1;
                    TEMPP.Note = Note;
                    TEMPP.Weight = Weight;
                    CodeTable.SaveChanges();

                }
                catch (Exception)
                {

                    return Content("<script>alert('这个条目被别的表关联掉了不能修改'); history.go(-1)</script>");
                }
                }
            }

            return Redirect("/CodeManager/Code/C_StuEvaSchoolGradeLevelItem1");

        }

        #endregion 学生评价学校评估等级表管理



        #region 学生评价企业评估表管理

        /*
         首先批次号从批次表里取出来，就一开始上面批次的选择框和下面的选择框批次号需要约束一下是登陆者的单位（学校）
         其次关于复制我默认的会从当中找出是全空的批次才会出现在选择框当中
         * 这里因为会有身份过滤所以只有一所学校的时候BatchName和批次号都是一对一的，因此记得每次两个列互相查找的时候要加个schoolID的条件
         
         */
        [BaseActionFilter]
        public ActionResult C_StuEvaluateEntItem1()
        {
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            List<T_PracBatch> temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();//身份验证取出该学校所有的批次号
            ViewBag.piciid = temp;

            return View("~/Areas/CodeManager/Views/Code/C_StuEvaluateEntItem/C_StuEvaluateEntItem1.cshtml");
        }
        [BaseActionFilter]
        public ActionResult GetTheDataStuEvaluateEntItem()
        {

            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();//获取当前登录的用户身份信息（比如这个是学校ID）
            List<C_StuEvaluateEntItem> ccc = (from v in CodeTable.C_StuEvaluateEntItem select v).ToList();
            int counthaha = ccc.Count();
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            string TheValue = Request["classValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数

            int TotalNum = 0;



            var service = new getStuEvaluateEntItem();
            var theEnca = service.LoadNewsData(Temp, ShenFenID, counthaha, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
            var result = from b in theEnca select b;



            if (TheAttribute == "fasong")
            {
                EnterpriseContext CodeTable1 = new EnterpriseContext();
                T_PracBatch temp123 = (from v in CodeTable1.PracBatch where v.BatchName == TheValue select v).FirstOrDefault();
                string CityIDS = temp123.PracBatchID;

                theEnca = service.LoadNewsData(Temp, CityIDS, counthaha - 9, out TotalNum);
                result = from b in theEnca select b;

            }




            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串
            //ChangeNewsState c=new ChangeNewsState();
            //string Newstr=c.ChangeState(str);

            return Content(str);
        }

        [BaseActionFilter]
        public ActionResult receajaxUnsetStuEvaluateEntItem()
        {
            string Alloption = Request.Form[0];//获取所有option的值然后与这里的值作比较，如果一样就不改变
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            var temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v.PracBatchID).ToList().Distinct<string>();


            string[] TTT1 = new string[150];//用来存放没有条目的批次号
            int i = 0;
            string TTT11 = "";
            foreach (var c in temp)
            {
                C_StuEvaluateEntItem temp1 = (from q in CodeTable.C_StuEvaluateEntItem where q.PracBatchID == c select q).FirstOrDefault();
                if (temp1 == null)
                {
                    string temp123 = (from a in CodeTable1.PracBatch where a.PracBatchID == c select a.BatchName).FirstOrDefault();
                    TTT1[i] += temp123;
                    i++;

                }
            }



            for (int j = 0; j < i; j++)
            {
                TTT11 += TTT1[j] + ",";
            }
            if (Alloption != TTT11)
            {
                return Content(TTT11);
            }

            else
            {

                return Content("Same");
            }
        }

        [BaseActionFilter]
        public ActionResult receajaxFuzhiStuEvaluateEntItem()
        {

            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            string PidToName = Request.Form[0];//要复制到这个批次号下面
            string PidBeiName = Request.Form[1];//被复制的那个批次号


            string PidTo = (from v in CodeTable1.PracBatch where v.BatchName == PidToName && v.SchoolID == ShenFenID select v.PracBatchID).FirstOrDefault();
            string PidBei = (from v in CodeTable1.PracBatch where v.BatchName == PidBeiName && v.SchoolID == ShenFenID select v.PracBatchID).FirstOrDefault();

            var temps = (from v in CodeTable.C_StuEvaluateEntItem where v.PracBatchID == PidBei select v).ToList();
            int count1 = temps.Count();
            int jishu = 0;
            foreach (var c in temps)
            {

                int ItemSequence1 = c.ItemSequence;
                string ItemName = c.ItemName;
                string PracBatchID = PidTo;
                string ItemNo = PidTo + "-" + ItemSequence1;
                C_StuEvaluateEntItem temp22 = new C_StuEvaluateEntItem { ItemNo = ItemNo, ItemSequence = ItemSequence1, ItemName = ItemName, PracBatchID = PracBatchID };
                CodeTable.C_StuEvaluateEntItem.Add(temp22);
                if (CodeTable.SaveChanges() > 0)
                {
                    jishu++;

                }

            }






            if (jishu == count1)
            {
                string spename = "chenggong";
                return Content(spename);
            }
            else
            {
                string sfalse = "shibai";
                return Content(sfalse);
            }

        }



        [BaseActionFilter]
        public ActionResult DeleteStuEvaluateEntItem()//接受表格传来的信息删除表格信息
        {
            //删除新闻数据

            string Deleteid = Request.Form[0];//获取需要删除的专业编号组成的字符串，且以逗号分割
            string[] SplitArray1 = Deleteid.Split(',');//取到要删除的新闻编号放进数组里

            int ArrayCount = SplitArray1.Count();

            string StringCount = Convert.ToString(ArrayCount);

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string classid = SplitArray1[a];//不能直接把SplitArray1[a]放入linq语句会报错
                // int intid = Convert.ToInt32(classid);

                try
                {
                    C_StuEvaluateEntItem tempspe = (from v in CodeTable.C_StuEvaluateEntItem where v.ItemNo == classid select v).FirstOrDefault();
                    CodeTable.C_StuEvaluateEntItem.Remove(tempspe);
                    CodeTable.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这条条目信息在别的表被引用了不能删除');history.go(-1)</script>");
                }

            }

            return Content(StringCount);
        }

        [BaseActionFilter]
        public ActionResult receajaxGetStuEvaluateEntItemPID()
        {
            string batchname = Request.Form[0];
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == batchname select v.PracBatchID).FirstOrDefault();

            return Content(PracBatchID);
        }
        [BaseActionFilter]
        public ActionResult AddStuEvaluateEntItem()//添加新的种类信息
        {

            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            List<T_PracBatch> temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();
            ViewBag.piciid = temp;
            return View("~/Areas/CodeManager/Views/Code/C_StuEvaluateEntItem/AddStuEvaluateEntItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReAddStuEvaluateEntItem()//接受添加种类信息的表单和数据库交互
        {
            string ItemNo = Request.Form["ItemNo"];
            string BatchName = Request.Form["BatchName"];//获取批次名的同时通过下面的几句转换成批次号
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == BatchName select v.PracBatchID).FirstOrDefault();
            string ItemName = Request.Form["ItemName"];
            string ItemSequence = Request.Form["ItemSequence"];
            int ItemSequence1 = Convert.ToInt32(ItemSequence);
            int ItemPower = Convert.ToInt32(Request.Form["ItemPower"]);

            C_StuEvaluateEntItem temp = (from v in CodeTable.C_StuEvaluateEntItem where v.ItemNo == ItemNo select v).FirstOrDefault();
            if (temp != null)
            {
                return Content("<script>alert('这个条目已经存在'); history.go(-1)</script>");

            }
            else
            {
                if (ItemPower < 0 || ItemPower > 100) { return Content("<script>alert('条目权重请输入0到100的数字'); history.go(-1)</script>"); }

                else {  C_StuEvaluateEntItem temp22 = new C_StuEvaluateEntItem { ItemNo = ItemNo, ItemSequence = ItemSequence1, ItemName = ItemName, PracBatchID = PracBatchID, ItemPower = ItemPower };
                CodeTable.C_StuEvaluateEntItem.Add(temp22);
                CodeTable.SaveChanges();

                /* string Connectstr = "Data Source=120.24.91.132;Initial Catalog=Platform;Persist Security Info=True;Uid=sa;password=Hqu33333";
                 SqlConnection conn = new SqlConnection(Connectstr);
                 string strSQL = "insert into C_ProPosition (ProPosID,ProPosName) values(" + id222 + ",'" + name + "')";
                 conn.Open();
                 try
                 {
                     SqlCommand cmd = new SqlCommand(strSQL, conn);
                     cmd.ExecuteNonQuery();
                 }
                 catch (SqlException ex)
                 {
                     //...System.Data.SqlClient.SqlException: “microsoft”附近有语法错误
                 }
                 conn.Close();
                 */

                return Redirect("/CodeManager/Code/C_StuEvaluateEntItem1");//注意前面那个platform也加上不然会报错找不到视图
            }
            }

        }
        [BaseActionFilter]
        public ActionResult detailStuEvaluateEntItem(string id)//查看详情
        {
            C_StuEvaluateEntItem temp = (from c in CodeTable.C_StuEvaluateEntItem where c.ItemNo == id select c).FirstOrDefault();
            ViewBag.detail = temp;
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string temp2 = temp.PracBatchID;
            string batchname = (from v in CodeTable1.PracBatch where v.PracBatchID == temp2 select v.BatchName).FirstOrDefault();
            ViewBag.batchName = batchname;

            return View("~/Areas/CodeManager/Views/Code/C_StuEvaluateEntItem/detailStuEvaluateEntItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult updateStuEvaluateEntItem(string id)//修改种类信息
        {



            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            Session["itemno"] = id;//我在session里还没设置过期时间
            C_StuEvaluateEntItem temp = (from c in CodeTable.C_StuEvaluateEntItem where c.ItemNo == id select c).FirstOrDefault();
            ViewBag.update1 = temp;//该条条目的全部信息
            string updateid = (from c in CodeTable.C_StuEvaluateEntItem where c.ItemNo == id select c.PracBatchID).FirstOrDefault();
            List<T_PracBatch> temp1 = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();
            string temp2 = (from v in CodeTable1.PracBatch where v.PracBatchID == updateid select v.BatchName).FirstOrDefault();

            ViewBag.update = temp2;
            ViewBag.piciid = temp1;
            return View("~/Areas/CodeManager/Views/Code/C_StuEvaluateEntItem/updateStuEvaluateEntItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReupdateStuEvaluateEntItem()//接收修改后的信息和数据库进行交互
        {
            string ItemNo = Request.Form["ItemNo"];
            string BatchName = Request.Form["BatchName"];//获取批次名的同时通过下面的几句转换成批次号
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == BatchName select v.PracBatchID).FirstOrDefault();
            string ItemName = Request.Form["ItemName"];
            string ItemSequence = Request.Form["ItemSequence"];
            int ItemSequence1 = Convert.ToInt32(ItemSequence);
            int ItemPower = Convert.ToInt32(Request.Form["ItemPower"]);
            string id = Convert.ToString(Session["itemno"]);
            if (id != ItemNo)
            {
                return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

            }
            else
            {
                if (ItemPower < 0 || ItemPower > 100) { return Content("<script>alert('条目权重请输入0到100的数字'); history.go(-1)</script>"); }

                else { C_StuEvaluateEntItem TEMPP = (from c in CodeTable.C_StuEvaluateEntItem where c.ItemNo == ItemNo select c).FirstOrDefault();

                try
                {
                    TEMPP.PracBatchID = PracBatchID;
                    TEMPP.ItemName = ItemName;
                    TEMPP.ItemSequence = ItemSequence1;
                    TEMPP.ItemPower = ItemPower;
                    CodeTable.SaveChanges();

                }
                catch (Exception)
                {

                    return Content("<script>alert('这个条目被别的表关联掉了不能修改'); history.go(-1)</script>");
                }
            }
            }
            return Redirect("/CodeManager/Code/C_StuEvaluateEntItem1");

        }

        #endregion  学生评价企业评估表管理

        #region 学生评价企业评估等级表管理



        [BaseActionFilter]
        public ActionResult C_StuEvaEntGradeLevelItem1()
        {
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            List<T_PracBatch> temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();//身份验证取出该学校所有的批次号
            ViewBag.piciid = temp;

            return View("~/Areas/CodeManager/Views/Code/C_StuEvaEntGradeLevelItem/C_StuEvaEntGradeLevelItem1.cshtml");
        }
        [BaseActionFilter]
        public ActionResult GetTheDataStuEvaEntGradeLevelItem()
        {

            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();//获取当前登录的用户身份信息（比如这个是学校ID）
            List<C_StuEvaEntGradeLevelItem> ccc = (from v in CodeTable.C_StuEvaEntGradeLevelItem select v).ToList();
            int counthaha = ccc.Count();
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            string TheValue = Request["classValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数

            int TotalNum = 0;



            var service = new GetStuEvaEntGradeLevelItem();
            var theEnca = service.LoadNewsData(Temp, ShenFenID, counthaha, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
            var result = from b in theEnca select b;



            if (TheAttribute == "fasong")
            {
                EnterpriseContext CodeTable1 = new EnterpriseContext();
                T_PracBatch temp123 = (from v in CodeTable1.PracBatch where v.BatchName == TheValue select v).FirstOrDefault();
                string CityIDS = temp123.PracBatchID;

                theEnca = service.LoadNewsData(Temp, CityIDS, counthaha - 9, out TotalNum);
                result = from b in theEnca select b;

            }




            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串
            //ChangeNewsState c=new ChangeNewsState();
            //string Newstr=c.ChangeState(str);

            return Content(str);
        }

        [BaseActionFilter]
        public ActionResult receajaxUnsetStuEvaEntGradeLevelItem()
        {
            string Alloption = Request.Form[0];//获取所有option的值然后与这里的值作比较，如果一样就不改变
            //string result = System.Text.RegularExpressions.Regex.Replace(Alloption, "--请选择--", "");
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            var temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v.PracBatchID).ToList().Distinct<string>();


            string[] TTT1 = new string[150];//用来存放没有条目的批次号
            int i = 0;
            string TTT11 = "";
            foreach (var c in temp)
            {
                C_StuEvaEntGradeLevelItem temp1 = (from q in CodeTable.C_StuEvaEntGradeLevelItem where q.PracBatchID == c select q).FirstOrDefault();
                if (temp1 == null)
                {
                    string temp123 = (from a in CodeTable1.PracBatch where a.PracBatchID == c select a.BatchName).FirstOrDefault();
                    TTT1[i] += temp123;
                    i++;

                }
            }



            for (int j = 0; j < i; j++)
            {
                TTT11 += TTT1[j] + ",";
            }

            //string  TTT111 = "," + TTT11;

            if (Alloption != TTT11)
            {
                return Content(TTT11);
            }

            else
            {

                return Content("Same");
            }
        }

        [BaseActionFilter]
        public ActionResult receajaxFuzhiStuEvaEntGradeLevelItem()
        {

            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            string PidToName = Request.Form[0];//要复制到这个批次号下面
            string PidBeiName = Request.Form[1];//被复制的那个批次号


            string PidTo = (from v in CodeTable1.PracBatch where v.BatchName == PidToName && v.SchoolID == ShenFenID select v.PracBatchID).FirstOrDefault();
            string PidBei = (from v in CodeTable1.PracBatch where v.BatchName == PidBeiName && v.SchoolID == ShenFenID select v.PracBatchID).FirstOrDefault();

            var temps = (from v in CodeTable.C_StuEvaEntGradeLevelItem where v.PracBatchID == PidBei select v).ToList();
            int count1 = temps.Count();
            int jishu = 0;
            foreach (var c in temps)
            {

                int ItemSequence1 = c.ItemSequence;
                string ItemName = c.ItemName;
                string PracBatchID = PidTo;
                string ItemNo = PidTo + "-" + ItemSequence1;
                int Weight = Convert.ToInt32(c.Weight);
                string Note = c.Note;
                C_StuEvaEntGradeLevelItem temp22 = new C_StuEvaEntGradeLevelItem { ItemNo = ItemNo, ItemSequence = ItemSequence1, ItemName = ItemName, PracBatchID = PracBatchID, Weight = Weight, Note = Note };
                CodeTable.C_StuEvaEntGradeLevelItem.Add(temp22);
                if (CodeTable.SaveChanges() > 0)
                {
                    jishu++;

                }

            }






            if (jishu == count1)
            {
                string spename = "chenggong";
                return Content(spename);
            }
            else
            {
                string sfalse = "shibai";
                return Content(sfalse);
            }

        }



        [BaseActionFilter]
        public ActionResult DeleteStuEvaEntGradeLevelItem()//接受表格传来的信息删除表格信息
        {
            //删除新闻数据

            string Deleteid = Request.Form[0];//获取需要删除的专业编号组成的字符串，且以逗号分割
            string[] SplitArray1 = Deleteid.Split(',');//取到要删除的新闻编号放进数组里

            int ArrayCount = SplitArray1.Count();

            string StringCount = Convert.ToString(ArrayCount);

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string classid = SplitArray1[a];//不能直接把SplitArray1[a]放入linq语句会报错
                // int intid = Convert.ToInt32(classid);

                try
                {
                    C_StuEvaEntGradeLevelItem tempspe = (from v in CodeTable.C_StuEvaEntGradeLevelItem where v.ItemNo == classid select v).FirstOrDefault();
                    CodeTable.C_StuEvaEntGradeLevelItem.Remove(tempspe);
                    CodeTable.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这条条目信息在别的表被引用了不能删除');history.go(-1)</script>");
                }

            }

            return Content(StringCount);
        }


        [BaseActionFilter]
        public ActionResult receajaxGetStuEvaEntGradeLevelItemPID()
        {
            string batchname = Request.Form[0];
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == batchname select v.PracBatchID).FirstOrDefault();

            return Content(PracBatchID);
        }
        [BaseActionFilter]
        public ActionResult AddStuEvaEntGradeLevelItem()//添加新的种类信息
        {

            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            List<T_PracBatch> temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();

            ViewBag.piciid = temp;
            return View("~/Areas/CodeManager/Views/Code/C_StuEvaEntGradeLevelItem/AddStuEvaEntGradeLevelItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReAddStuEvaEntGradeLevelItem()//接受添加种类信息的表单和数据库交互
        {
            string ItemNo = Request.Form["ItemNo"];
            string BatchName = Request.Form["BatchName"];//获取批次名的同时通过下面的几句转换成批次号
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == BatchName select v.PracBatchID).FirstOrDefault();
            string ItemName = Request.Form["ItemName"];
            string ItemSequence = Request.Form["ItemSequence"];
            int Weight = Convert.ToInt32(Request.Form["Weight"]);
            string Note = Request.Form["Note"];
            int ItemSequence1 = Convert.ToInt32(ItemSequence);

            C_StuEvaEntGradeLevelItem temp = (from v in CodeTable.C_StuEvaEntGradeLevelItem where v.ItemNo == ItemNo select v).FirstOrDefault();
            if (temp != null)
            {
                return Content("<script>alert('这个条目已经存在'); history.go(-1)</script>");

            }
            else
            {
                if (Weight < 0 || Weight > 100) { return Content("<script>alert('条目权重的值为0到100的数字'); history.go(-1)</script>"); }
                else { 
                C_StuEvaEntGradeLevelItem temp22 = new C_StuEvaEntGradeLevelItem { ItemNo = ItemNo, ItemSequence = ItemSequence1, ItemName = ItemName, PracBatchID = PracBatchID, Weight = Weight, Note = Note };
                CodeTable.C_StuEvaEntGradeLevelItem.Add(temp22);
                CodeTable.SaveChanges();

                /* string Connectstr = "Data Source=120.24.91.132;Initial Catalog=Platform;Persist Security Info=True;Uid=sa;password=Hqu33333";
                 SqlConnection conn = new SqlConnection(Connectstr);
                 string strSQL = "insert into C_ProPosition (ProPosID,ProPosName) values(" + id222 + ",'" + name + "')";
                 conn.Open();
                 try
                 {
                     SqlCommand cmd = new SqlCommand(strSQL, conn);
                     cmd.ExecuteNonQuery();
                 }
                 catch (SqlException ex)
                 {
                     //...System.Data.SqlClient.SqlException: “microsoft”附近有语法错误
                 }
                 conn.Close();
                 */

                return Redirect("/CodeManager/Code/C_StuEvaEntGradeLevelItem1");//注意前面那个platform也加上不然会报错找不到视图
                }
            }

        }
        [BaseActionFilter]
        public ActionResult detailStuEvaEntGradeLevelItem(string id)//查看详情
        {
            C_StuEvaEntGradeLevelItem temp = (from c in CodeTable.C_StuEvaEntGradeLevelItem where c.ItemNo == id select c).FirstOrDefault();
            ViewBag.detail = temp;
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string temp2 = temp.PracBatchID;
            string batchname = (from v in CodeTable1.PracBatch where v.PracBatchID == temp2 select v.BatchName).FirstOrDefault();
            ViewBag.batchName = batchname;

            return View("~/Areas/CodeManager/Views/Code/C_StuEvaEntGradeLevelItem/detailStuEvaEntGradeLevelItem.cshtml");

        }
        [BaseActionFilter]

        public ActionResult updateStuEvaEntGradeLevelItem(string id)//修改种类信息
        {
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            Session["itemno"] = id;//我在session里还没设置过期时间
            string updateid = (from c in CodeTable.C_StuEvaEntGradeLevelItem where c.ItemNo == id select c.PracBatchID).FirstOrDefault();



            List<T_PracBatch> temp1 = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();
            string temp2 = (from v in CodeTable1.PracBatch where v.PracBatchID == updateid select v.BatchName).FirstOrDefault();
            C_StuEvaEntGradeLevelItem TEMP3 = (from v in CodeTable.C_StuEvaEntGradeLevelItem where v.ItemNo == id select v).FirstOrDefault();
            ViewBag.update1 = TEMP3;

            ViewBag.update = temp2;
            ViewBag.piciid = temp1;
            return View("~/Areas/CodeManager/Views/Code/C_StuEvaEntGradeLevelItem/updateStuEvaEntGradeLevelItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReupdateStuEvaEntGradeLevelItem()//接收修改后的信息和数据库进行交互
        {
            string ItemNo = Request.Form["ItemNo"];
            string BatchName = Request.Form["BatchName"];//获取批次名的同时通过下面的几句转换成批次号
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == BatchName select v.PracBatchID).FirstOrDefault();
            string ItemName = Request.Form["ItemName"];
            string ItemSequence = Request.Form["ItemSequence"];
            int ItemSequence1 = Convert.ToInt32(ItemSequence);
            int Weight = Convert.ToInt32(Request.Form["Weight"]);
            string Note = Request.Form["Note"];

            string id = Convert.ToString(Session["itemno"]);
            if (id != ItemNo)
            {
                return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

            }
            else
            {
                if (Weight < 0 || Weight > 100) { return Content("<script>alert('条目权重的值为0到100的数字'); history.go(-1)</script>"); }
                else { 
                C_StuEvaEntGradeLevelItem TEMPP = (from c in CodeTable.C_StuEvaEntGradeLevelItem where c.ItemNo == ItemNo select c).FirstOrDefault();

                try
                {
                    TEMPP.PracBatchID = PracBatchID;
                    TEMPP.ItemName = ItemName;
                    TEMPP.ItemSequence = ItemSequence1;
                    TEMPP.Note = Note;
                    TEMPP.Weight = Weight;
                    CodeTable.SaveChanges();

                }
                catch (Exception)
                {

                    return Content("<script>alert('这个条目被别的表关联掉了不能修改'); history.go(-1)</script>");
                }
            }
            }
            return Redirect("/CodeManager/Code/C_StuEvaEntGradeLevelItem1");

        }
        #endregion 学生评价企业评估等级表管理


        #region 企业评价学生评估表管理

        [BaseActionFilter]
        public ActionResult C_EntEvaluateStuItem1()
        {
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            List<T_PracBatch> temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();//身份验证取出该学校所有的批次号
            ViewBag.piciid = temp;

            return View("~/Areas/CodeManager/Views/Code/C_EntEvaluateStuItem/C_EntEvaluateStuItem1.cshtml");
        }
        [BaseActionFilter]
        public ActionResult GetTheDataEntEvaluateStuItem()
        {

            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();//获取当前登录的用户身份信息（比如这个是学校ID）
            List<C_EntEvaluateStuItem> ccc = (from v in CodeTable.C_EntEvaluateStuItem select v).ToList();
            int counthaha = ccc.Count();
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            string TheValue = Request["classValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数

            int TotalNum = 0;



            var service = new getEntEvaluateStuItem();
            var theEnca = service.LoadNewsData(Temp, ShenFenID, counthaha, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
            var result = from b in theEnca select b;



            if (TheAttribute == "fasong")
            {
                EnterpriseContext CodeTable1 = new EnterpriseContext();
                T_PracBatch temp123 = (from v in CodeTable1.PracBatch where v.BatchName == TheValue select v).FirstOrDefault();
                string CityIDS = temp123.PracBatchID;

                theEnca = service.LoadNewsData(Temp, CityIDS, counthaha - 9, out TotalNum);
                result = from b in theEnca select b;

            }




            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串
            //ChangeNewsState c=new ChangeNewsState();
            //string Newstr=c.ChangeState(str);

            return Content(str);
        }

        [BaseActionFilter]
        public ActionResult receajaxUnsetEntEvaluateStuItem()
        {
            string Alloption = Request.Form[0];//获取所有option的值然后与这里的值作比较，如果一样就不改变
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            var temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v.PracBatchID).ToList().Distinct<string>();


            string[] TTT1 = new string[150];//用来存放没有条目的批次号
            int i = 0;
            string TTT11 = "";
            foreach (var c in temp)
            {
                C_EntEvaluateStuItem temp1 = (from q in CodeTable.C_EntEvaluateStuItem where q.PracBatchID == c select q).FirstOrDefault();
                if (temp1 == null)
                {
                    string temp123 = (from a in CodeTable1.PracBatch where a.PracBatchID == c select a.BatchName).FirstOrDefault();
                    TTT1[i] += temp123;
                    i++;

                }
            }



            for (int j = 0; j < i; j++)
            {
                TTT11 += TTT1[j] + ",";
            }


            if (Alloption != TTT11)
            {
                return Content(TTT11);
            }

            else
            {

                return Content("Same");
            }
        }

        [BaseActionFilter]
        public ActionResult receajaxFuzhiEntEvaluateStuItem()
        {

            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            string PidToName = Request.Form[0];//要复制到这个批次号下面
            string PidBeiName = Request.Form[1];//被复制的那个批次号


            string PidTo = (from v in CodeTable1.PracBatch where v.BatchName == PidToName && v.SchoolID == ShenFenID select v.PracBatchID).FirstOrDefault();
            string PidBei = (from v in CodeTable1.PracBatch where v.BatchName == PidBeiName && v.SchoolID == ShenFenID select v.PracBatchID).FirstOrDefault();

            var temps = (from v in CodeTable.C_EntEvaluateStuItem where v.PracBatchID == PidBei select v).ToList();
            int count1 = temps.Count();
            int jishu = 0;
            foreach (var c in temps)
            {

                int ItemSequence1 = c.ItemSequence;
                string ItemName = c.ItemName;
                string PracBatchID = PidTo;
                string ItemNo = PidTo + "-" + ItemSequence1;
                C_EntEvaluateStuItem temp22 = new C_EntEvaluateStuItem { ItemNo = ItemNo, ItemSequence = ItemSequence1, ItemName = ItemName, PracBatchID = PracBatchID };
                CodeTable.C_EntEvaluateStuItem.Add(temp22);
                if (CodeTable.SaveChanges() > 0)
                {
                    jishu++;

                }

            }






            if (jishu == count1)
            {
                string spename = "chenggong";
                return Content(spename);
            }
            else
            {
                string sfalse = "shibai";
                return Content(sfalse);
            }

        }



        [BaseActionFilter]
        public ActionResult DeleteEntEvaluateStuItem()//接受表格传来的信息删除表格信息
        {
            //删除新闻数据

            string Deleteid = Request.Form[0];//获取需要删除的专业编号组成的字符串，且以逗号分割
            string[] SplitArray1 = Deleteid.Split(',');//取到要删除的新闻编号放进数组里

            int ArrayCount = SplitArray1.Count();

            string StringCount = Convert.ToString(ArrayCount);

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string classid = SplitArray1[a];//不能直接把SplitArray1[a]放入linq语句会报错
                // int intid = Convert.ToInt32(classid);

                try
                {
                    C_EntEvaluateStuItem tempspe = (from v in CodeTable.C_EntEvaluateStuItem where v.ItemNo == classid select v).FirstOrDefault();
                    CodeTable.C_EntEvaluateStuItem.Remove(tempspe);
                    CodeTable.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这条条目信息在别的表被引用了不能删除');history.go(-1)</script>");
                }

            }

            return Content(StringCount);
        }

        [BaseActionFilter]
        public ActionResult receajaxGetEntEvaluateStuItemPID()
        {
            string batchname = Request.Form[0];
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == batchname select v.PracBatchID).FirstOrDefault();

            return Content(PracBatchID);
        }
        [BaseActionFilter]
        public ActionResult AddEntEvaluateStuItem()//添加新的种类信息
        {

            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            List<T_PracBatch> temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();
            ViewBag.piciid = temp;
            return View("~/Areas/CodeManager/Views/Code/C_EntEvaluateStuItem/AddEntEvaluateStuItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReAddEntEvaluateStuItem()//接受添加种类信息的表单和数据库交互
        {
            string ItemNo = Request.Form["ItemNo"];
            string BatchName = Request.Form["BatchName"];//获取批次名的同时通过下面的几句转换成批次号
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == BatchName select v.PracBatchID).FirstOrDefault();
            string ItemName = Request.Form["ItemName"];
            string ItemSequence = Request.Form["ItemSequence"];
            int ItemSequence1 = Convert.ToInt32(ItemSequence);


            C_EntEvaluateStuItem temp = (from v in CodeTable.C_EntEvaluateStuItem where v.ItemNo == ItemNo select v).FirstOrDefault();
            if (temp != null)
            {
                return Content("<script>alert('这个条目已经存在'); history.go(-1)</script>");

            }
            else
            {
                C_EntEvaluateStuItem temp22 = new C_EntEvaluateStuItem { ItemNo = ItemNo, ItemSequence = ItemSequence1, ItemName = ItemName, PracBatchID = PracBatchID };
                CodeTable.C_EntEvaluateStuItem.Add(temp22);
                CodeTable.SaveChanges();

                /* string Connectstr = "Data Source=120.24.91.132;Initial Catalog=Platform;Persist Security Info=True;Uid=sa;password=Hqu33333";
                 SqlConnection conn = new SqlConnection(Connectstr);
                 string strSQL = "insert into C_ProPosition (ProPosID,ProPosName) values(" + id222 + ",'" + name + "')";
                 conn.Open();
                 try
                 {
                     SqlCommand cmd = new SqlCommand(strSQL, conn);
                     cmd.ExecuteNonQuery();
                 }
                 catch (SqlException ex)
                 {
                     //...System.Data.SqlClient.SqlException: “microsoft”附近有语法错误
                 }
                 conn.Close();
                 */

                return Redirect("/CodeManager/Code/C_EntEvaluateStuItem1");//注意前面那个platform也加上不然会报错找不到视图
            }

        }
        [BaseActionFilter]
        public ActionResult detailEntEvaluateStuItem(string id)//查看详情
        {
            C_EntEvaluateStuItem temp = (from c in CodeTable.C_EntEvaluateStuItem where c.ItemNo == id select c).FirstOrDefault();
            ViewBag.detail = temp;
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string temp2 = temp.PracBatchID;
            string batchname = (from v in CodeTable1.PracBatch where v.PracBatchID == temp2 select v.BatchName).FirstOrDefault();
            ViewBag.batchName = batchname;

            return View("~/Areas/CodeManager/Views/Code/C_EntEvaluateStuItem/detailEntEvaluateStuItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult updateEntEvaluateStuItem(string id)//修改种类信息
        {



            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            Session["itemno"] = id;//我在session里还没设置过期时间
            C_EntEvaluateStuItem temp = (from c in CodeTable.C_EntEvaluateStuItem where c.ItemNo == id select c).FirstOrDefault();
            ViewBag.update1 = temp;//该条条目的全部信息
            string updateid = (from c in CodeTable.C_EntEvaluateStuItem where c.ItemNo == id select c.PracBatchID).FirstOrDefault();
            List<T_PracBatch> temp1 = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();
            string temp2 = (from v in CodeTable1.PracBatch where v.PracBatchID == updateid select v.BatchName).FirstOrDefault();

            ViewBag.update = temp2;
            ViewBag.piciid = temp1;
            return View("~/Areas/CodeManager/Views/Code/C_EntEvaluateStuItem/updateEntEvaluateStuItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReupdateEntEvaluateStuItem()//接收修改后的信息和数据库进行交互
        {
            string ItemNo = Request.Form["ItemNo"];
            string BatchName = Request.Form["BatchName"];//获取批次名的同时通过下面的几句转换成批次号
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == BatchName select v.PracBatchID).FirstOrDefault();
            string ItemName = Request.Form["ItemName"];
            string ItemSequence = Request.Form["ItemSequence"];
            int ItemSequence1 = Convert.ToInt32(ItemSequence);

            string id = Convert.ToString(Session["itemno"]);
            if (id != ItemNo)
            {
                return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

            }
            else
            {
                C_EntEvaluateStuItem TEMPP = (from c in CodeTable.C_EntEvaluateStuItem where c.ItemNo == ItemNo select c).FirstOrDefault();

                try
                {
                    TEMPP.PracBatchID = PracBatchID;
                    TEMPP.ItemName = ItemName;
                    TEMPP.ItemSequence = ItemSequence1;
                    CodeTable.SaveChanges();

                }
                catch (Exception)
                {

                    return Content("<script>alert('这个条目被别的表关联掉了不能修改'); history.go(-1)</script>");
                }
            }

            return Redirect("/CodeManager/Code/C_EntEvaluateStuItem1");

        }
        #endregion 企业评价学生评估表管理

        #region 企业评价学生评估等级表管理

        [BaseActionFilter]
        public ActionResult C_EntEvaStuGradeLevelItem1()
        {
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            List<T_PracBatch> temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();//身份验证取出该学校所有的批次号
            ViewBag.piciid = temp;

            return View("~/Areas/CodeManager/Views/Code/C_EntEvaStuGradeLevelItem/C_EntEvaStuGradeLevelItem1.cshtml");
        }
        [BaseActionFilter]
        public ActionResult GetTheDataEntEvaStuGradeLevelItem()
        {

            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();//获取当前登录的用户身份信息（比如这个是学校ID）
            List<C_EntEvaStuGradeLevelItem> ccc = (from v in CodeTable.C_EntEvaStuGradeLevelItem select v).ToList();
            int counthaha = ccc.Count();
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            string TheValue = Request["classValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数

            int TotalNum = 0;



            var service = new GetEntEvaStuGradeLevelItem();
            var theEnca = service.LoadNewsData(Temp, ShenFenID, counthaha, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
            var result = from b in theEnca select b;



            if (TheAttribute == "fasong")
            {
                EnterpriseContext CodeTable1 = new EnterpriseContext();
                T_PracBatch temp123 = (from v in CodeTable1.PracBatch where v.BatchName == TheValue select v).FirstOrDefault();
                string CityIDS = temp123.PracBatchID;

                theEnca = service.LoadNewsData(Temp, CityIDS, counthaha - 9, out TotalNum);
                result = from b in theEnca select b;

            }




            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串
            //ChangeNewsState c=new ChangeNewsState();
            //string Newstr=c.ChangeState(str);

            return Content(str);
        }

        [BaseActionFilter]
        public ActionResult receajaxUnsetEntEvaStuGradeLevelItem()
        {
            string Alloption = Request.Form[0];//获取所有option的值然后与这里的值作比较，如果一样就不改变
            //string result = System.Text.RegularExpressions.Regex.Replace(Alloption, "--请选择--", "");
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            var temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v.PracBatchID).ToList().Distinct<string>();


            string[] TTT1 = new string[150];//用来存放没有条目的批次号
            int i = 0;
            string TTT11 = "";
            foreach (var c in temp)
            {
                C_EntEvaStuGradeLevelItem temp1 = (from q in CodeTable.C_EntEvaStuGradeLevelItem where q.PracBatchID == c select q).FirstOrDefault();
                if (temp1 == null)
                {
                    string temp123 = (from a in CodeTable1.PracBatch where a.PracBatchID == c select a.BatchName).FirstOrDefault();
                    TTT1[i] += temp123;
                    i++;

                }
            }



            for (int j = 0; j < i; j++)
            {
                TTT11 += TTT1[j] + ",";
            }

            //string  TTT111 = "," + TTT11;

            if (Alloption != TTT11)
            {
                return Content(TTT11);
            }

            else
            {

                return Content("Same");
            }
        }

        [BaseActionFilter]
        public ActionResult receajaxFuzhiEntEvaStuGradeLevelItem()
        {

            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            string PidToName = Request.Form[0];//要复制到这个批次号下面
            string PidBeiName = Request.Form[1];//被复制的那个批次号


            string PidTo = (from v in CodeTable1.PracBatch where v.BatchName == PidToName && v.SchoolID == ShenFenID select v.PracBatchID).FirstOrDefault();
            string PidBei = (from v in CodeTable1.PracBatch where v.BatchName == PidBeiName && v.SchoolID == ShenFenID select v.PracBatchID).FirstOrDefault();

            var temps = (from v in CodeTable.C_EntEvaStuGradeLevelItem where v.PracBatchID == PidBei select v).ToList();
            int count1 = temps.Count();
            int jishu = 0;
            foreach (var c in temps)
            {

                int ItemSequence1 = c.ItemSequence;
                string ItemName = c.ItemName;
                string PracBatchID = PidTo;
                string ItemNo = PidTo + "-" + ItemSequence1;
                int Weight = Convert.ToInt32(c.Weight);
                string Note = c.Note;
                C_EntEvaStuGradeLevelItem temp22 = new C_EntEvaStuGradeLevelItem { ItemNo = ItemNo, ItemSequence = ItemSequence1, ItemName = ItemName, PracBatchID = PracBatchID, Weight = Weight, Note = Note };
                CodeTable.C_EntEvaStuGradeLevelItem.Add(temp22);
                if (CodeTable.SaveChanges() > 0)
                {
                    jishu++;

                }

            }






            if (jishu == count1)
            {
                string spename = "chenggong";
                return Content(spename);
            }
            else
            {
                string sfalse = "shibai";
                return Content(sfalse);
            }

        }



        [BaseActionFilter]
        public ActionResult DeleteEntEvaStuGradeLevelItem()//接受表格传来的信息删除表格信息
        {
            //删除新闻数据

            string Deleteid = Request.Form[0];//获取需要删除的专业编号组成的字符串，且以逗号分割
            string[] SplitArray1 = Deleteid.Split(',');//取到要删除的新闻编号放进数组里

            int ArrayCount = SplitArray1.Count();

            string StringCount = Convert.ToString(ArrayCount);

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string classid = SplitArray1[a];//不能直接把SplitArray1[a]放入linq语句会报错
                // int intid = Convert.ToInt32(classid);

                try
                {
                    C_EntEvaStuGradeLevelItem tempspe = (from v in CodeTable.C_EntEvaStuGradeLevelItem where v.ItemNo == classid select v).FirstOrDefault();
                    CodeTable.C_EntEvaStuGradeLevelItem.Remove(tempspe);
                    CodeTable.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这条条目信息在别的表被引用了不能删除');history.go(-1)</script>");
                }

            }

            return Content(StringCount);
        }


        [BaseActionFilter]
        public ActionResult receajaxGetEntEvaStuGradeLevelItemPID()
        {
            string batchname = Request.Form[0];
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == batchname select v.PracBatchID).FirstOrDefault();

            return Content(PracBatchID);
        }
        [BaseActionFilter]
        public ActionResult AddEntEvaStuGradeLevelItem()//添加新的种类信息
        {

            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            List<T_PracBatch> temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();

            ViewBag.piciid = temp;
            return View("~/Areas/CodeManager/Views/Code/C_EntEvaStuGradeLevelItem/AddEntEvaStuGradeLevelItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReAddEntEvaStuGradeLevelItem()//接受添加种类信息的表单和数据库交互
        {
            string ItemNo = Request.Form["ItemNo"];
            string BatchName = Request.Form["BatchName"];//获取批次名的同时通过下面的几句转换成批次号
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == BatchName select v.PracBatchID).FirstOrDefault();
            string ItemName = Request.Form["ItemName"];
            string ItemSequence = Request.Form["ItemSequence"];
            int Weight = Convert.ToInt32(Request.Form["Weight"]);
            string Note = Request.Form["Note"];
            int ItemSequence1 = Convert.ToInt32(ItemSequence);

            C_EntEvaStuGradeLevelItem temp = (from v in CodeTable.C_EntEvaStuGradeLevelItem where v.ItemNo == ItemNo select v).FirstOrDefault();
            if (temp != null)
            {
                return Content("<script>alert('这个条目已经存在'); history.go(-1)</script>");

            }
            else
            {
                C_EntEvaStuGradeLevelItem temp22 = new C_EntEvaStuGradeLevelItem { ItemNo = ItemNo, ItemSequence = ItemSequence1, ItemName = ItemName, PracBatchID = PracBatchID, Weight = Weight, Note = Note };
                CodeTable.C_EntEvaStuGradeLevelItem.Add(temp22);
                CodeTable.SaveChanges();

                /* string Connectstr = "Data Source=120.24.91.132;Initial Catalog=Platform;Persist Security Info=True;Uid=sa;password=Hqu33333";
                 SqlConnection conn = new SqlConnection(Connectstr);
                 string strSQL = "insert into C_ProPosition (ProPosID,ProPosName) values(" + id222 + ",'" + name + "')";
                 conn.Open();
                 try
                 {
                     SqlCommand cmd = new SqlCommand(strSQL, conn);
                     cmd.ExecuteNonQuery();
                 }
                 catch (SqlException ex)
                 {
                     //...System.Data.SqlClient.SqlException: “microsoft”附近有语法错误
                 }
                 conn.Close();
                 */

                return Redirect("/CodeManager/Code/C_EntEvaStuGradeLevelItem1");//注意前面那个platform也加上不然会报错找不到视图
            }

        }
        [BaseActionFilter]
        public ActionResult detailEntEvaStuGradeLevelItem(string id)//查看详情
        {
            C_EntEvaStuGradeLevelItem temp = (from c in CodeTable.C_EntEvaStuGradeLevelItem where c.ItemNo == id select c).FirstOrDefault();
            ViewBag.detail = temp;
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string temp2 = temp.PracBatchID;
            string batchname = (from v in CodeTable1.PracBatch where v.PracBatchID == temp2 select v.BatchName).FirstOrDefault();
            ViewBag.batchName = batchname;

            return View("~/Areas/CodeManager/Views/Code/C_EntEvaStuGradeLevelItem/detailEntEvaStuGradeLevelItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult updateEntEvaStuGradeLevelItem(string id)//修改种类信息
        {
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            Session["itemno"] = id;//我在session里还没设置过期时间
            string updateid = (from c in CodeTable.C_EntEvaStuGradeLevelItem where c.ItemNo == id select c.PracBatchID).FirstOrDefault();



            List<T_PracBatch> temp1 = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();
            string temp2 = (from v in CodeTable1.PracBatch where v.PracBatchID == updateid select v.BatchName).FirstOrDefault();
            C_EntEvaStuGradeLevelItem TEMP3 = (from v in CodeTable.C_EntEvaStuGradeLevelItem where v.ItemNo == id select v).FirstOrDefault();
            ViewBag.update1 = TEMP3;

            ViewBag.update = temp2;
            ViewBag.piciid = temp1;
            return View("~/Areas/CodeManager/Views/Code/C_EntEvaStuGradeLevelItem/updateEntEvaStuGradeLevelItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReupdateEntEvaStuGradeLevelItem()//接收修改后的信息和数据库进行交互
        {
            string ItemNo = Request.Form["ItemNo"];
            string BatchName = Request.Form["BatchName"];//获取批次名的同时通过下面的几句转换成批次号
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == BatchName select v.PracBatchID).FirstOrDefault();
            string ItemName = Request.Form["ItemName"];
            string ItemSequence = Request.Form["ItemSequence"];
            int ItemSequence1 = Convert.ToInt32(ItemSequence);
            int Weight = Convert.ToInt32(Request.Form["Weight"]);
            string Note = Request.Form["Note"];

            string id = Convert.ToString(Session["itemno"]);
            if (id != ItemNo)
            {
                return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

            }
            else
            {
                C_EntEvaStuGradeLevelItem TEMPP = (from c in CodeTable.C_EntEvaStuGradeLevelItem where c.ItemNo == ItemNo select c).FirstOrDefault();

                try
                {
                    TEMPP.PracBatchID = PracBatchID;
                    TEMPP.ItemName = ItemName;
                    TEMPP.ItemSequence = ItemSequence1;
                    TEMPP.Note = Note;
                    TEMPP.Weight = Weight;
                    CodeTable.SaveChanges();

                }
                catch (Exception)
                {

                    return Content("<script>alert('这个条目被别的表关联掉了不能修改'); history.go(-1)</script>");
                }
            }

            return Redirect("/CodeManager/Code/C_EntEvaStuGradeLevelItem1");

        }

        #endregion 企业评价学生评估等级表管理

        #region 学校评价学生评估等级表管理
        [BaseActionFilter]
        public ActionResult C_SchoolEvaStuGradeLevelItem1()
        {
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            List<T_PracBatch> temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();//身份验证取出该学校所有的批次号
            ViewBag.piciid = temp;

            return View("~/Areas/CodeManager/Views/Code/C_SchoolEvaStuGradeLevelItem/C_SchoolEvaStuGradeLevelItem1.cshtml");
        }
        [BaseActionFilter]
        public ActionResult GetTheDataSchoolEvaStuGradeLevelItem()
        {

            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();//获取当前登录的用户身份信息（比如这个是学校ID）
            List<C_SchoolEvaStuGradeLevelItem> ccc = (from v in CodeTable.C_SchoolEvaStuGradeLevelItem select v).ToList();
            int counthaha = ccc.Count();
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            string TheValue = Request["classValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数

            int TotalNum = 0;



            var service = new GetSchoolEvaStuGradeLevelItem();
            var theEnca = service.LoadNewsData(Temp, ShenFenID, counthaha, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
            var result = from b in theEnca select b;



            if (TheAttribute == "fasong")
            {
                EnterpriseContext CodeTable1 = new EnterpriseContext();
                T_PracBatch temp123 = (from v in CodeTable1.PracBatch where v.BatchName == TheValue select v).FirstOrDefault();
                string CityIDS = temp123.PracBatchID;

                theEnca = service.LoadNewsData(Temp, CityIDS, counthaha - 9, out TotalNum);
                result = from b in theEnca select b;

            }




            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串
            //ChangeNewsState c=new ChangeNewsState();
            //string Newstr=c.ChangeState(str);

            return Content(str);
        }
        [BaseActionFilter]

        public ActionResult receajaxUnsetSchoolEvaStuGradeLevelItem()
        {
            string Alloption = Request.Form[0];//获取所有option的值然后与这里的值作比较，如果一样就不改变
            //string result = System.Text.RegularExpressions.Regex.Replace(Alloption, "--请选择--", "");
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            var temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v.PracBatchID).ToList().Distinct<string>();


            string[] TTT1 = new string[150];//用来存放没有条目的批次号
            int i = 0;
            string TTT11 = "";
            foreach (var c in temp)
            {
                C_SchoolEvaStuGradeLevelItem temp1 = (from q in CodeTable.C_SchoolEvaStuGradeLevelItem where q.PracBatchID == c select q).FirstOrDefault();
                if (temp1 == null)
                {
                    string temp123 = (from a in CodeTable1.PracBatch where a.PracBatchID == c select a.BatchName).FirstOrDefault();
                    TTT1[i] += temp123;
                    i++;

                }
            }



            for (int j = 0; j < i; j++)
            {
                TTT11 += TTT1[j] + ",";
            }

            //string  TTT111 = "," + TTT11;

            if (Alloption != TTT11)
            {
                return Content(TTT11);
            }

            else
            {

                return Content("Same");
            }
        }

        [BaseActionFilter]
        public ActionResult receajaxFuzhiSchoolEvaStuGradeLevelItem()
        {

            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            string PidToName = Request.Form[0];//要复制到这个批次号下面
            string PidBeiName = Request.Form[1];//被复制的那个批次号


            string PidTo = (from v in CodeTable1.PracBatch where v.BatchName == PidToName && v.SchoolID == ShenFenID select v.PracBatchID).FirstOrDefault();
            string PidBei = (from v in CodeTable1.PracBatch where v.BatchName == PidBeiName && v.SchoolID == ShenFenID select v.PracBatchID).FirstOrDefault();

            var temps = (from v in CodeTable.C_SchoolEvaStuGradeLevelItem where v.PracBatchID == PidBei select v).ToList();
            int count1 = temps.Count();
            int jishu = 0;
            foreach (var c in temps)
            {

                int ItemSequence1 = c.ItemSequence;
                string ItemName = c.ItemName;
                string PracBatchID = PidTo;
                string ItemNo = PidTo + "-" + ItemSequence1;
                int Weight = Convert.ToInt32(c.Weight);
                string Note = c.Note;
                C_SchoolEvaStuGradeLevelItem temp22 = new C_SchoolEvaStuGradeLevelItem { ItemNo = ItemNo, ItemSequence = ItemSequence1, ItemName = ItemName, PracBatchID = PracBatchID, Weight = Weight, Note = Note };
                CodeTable.C_SchoolEvaStuGradeLevelItem.Add(temp22);
                if (CodeTable.SaveChanges() > 0)
                {
                    jishu++;

                }

            }






            if (jishu == count1)
            {
                string spename = "chenggong";
                return Content(spename);
            }
            else
            {
                string sfalse = "shibai";
                return Content(sfalse);
            }

        }



            [BaseActionFilter]
        public ActionResult DeleteSchoolEvaStuGradeLevelItem()//接受表格传来的信息删除表格信息
        {
            //删除新闻数据

            string Deleteid = Request.Form[0];//获取需要删除的专业编号组成的字符串，且以逗号分割
            string[] SplitArray1 = Deleteid.Split(',');//取到要删除的新闻编号放进数组里

            int ArrayCount = SplitArray1.Count();

            string StringCount = Convert.ToString(ArrayCount);

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string classid = SplitArray1[a];//不能直接把SplitArray1[a]放入linq语句会报错
                // int intid = Convert.ToInt32(classid);

                try
                {
                    C_SchoolEvaStuGradeLevelItem tempspe = (from v in CodeTable.C_SchoolEvaStuGradeLevelItem where v.ItemNo == classid select v).FirstOrDefault();
                    CodeTable.C_SchoolEvaStuGradeLevelItem.Remove(tempspe);
                    CodeTable.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这条条目信息在别的表被引用了不能删除');history.go(-1)</script>");
                }

            }

            return Content(StringCount);
        }

        [BaseActionFilter]

        public ActionResult receajaxGetSchoolEvaStuGradeLevelItemPID()
        {
            string batchname = Request.Form[0];
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == batchname select v.PracBatchID).FirstOrDefault();

            return Content(PracBatchID);
        }
        [BaseActionFilter]
        public ActionResult AddSchoolEvaStuGradeLevelItem()//添加新的种类信息
        {

            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            List<T_PracBatch> temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();

            ViewBag.piciid = temp;
            return View("~/Areas/CodeManager/Views/Code/C_SchoolEvaStuGradeLevelItem/AddSchoolEvaStuGradeLevelItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReAddSchoolEvaStuGradeLevelItem()//接受添加种类信息的表单和数据库交互
        {
            string ItemNo = Request.Form["ItemNo"];
            string BatchName = Request.Form["BatchName"];//获取批次名的同时通过下面的几句转换成批次号
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == BatchName select v.PracBatchID).FirstOrDefault();
            string ItemName = Request.Form["ItemName"];
            string ItemSequence = Request.Form["ItemSequence"];
            int Weight = Convert.ToInt32(Request.Form["Weight"]);
            string Note = Request.Form["Note"];
            int ItemSequence1 = Convert.ToInt32(ItemSequence);

            C_SchoolEvaStuGradeLevelItem temp = (from v in CodeTable.C_SchoolEvaStuGradeLevelItem where v.ItemNo == ItemNo select v).FirstOrDefault();
            if (temp != null)
            {
                return Content("<script>alert('这个条目已经存在'); history.go(-1)</script>");

            }
            else
            {
                C_SchoolEvaStuGradeLevelItem temp22 = new C_SchoolEvaStuGradeLevelItem { ItemNo = ItemNo, ItemSequence = ItemSequence1, ItemName = ItemName, PracBatchID = PracBatchID, Weight = Weight, Note = Note };
                CodeTable.C_SchoolEvaStuGradeLevelItem.Add(temp22);
                CodeTable.SaveChanges();

                /* string Connectstr = "Data Source=120.24.91.132;Initial Catalog=Platform;Persist Security Info=True;Uid=sa;password=Hqu33333";
                 SqlConnection conn = new SqlConnection(Connectstr);
                 string strSQL = "insert into C_ProPosition (ProPosID,ProPosName) values(" + id222 + ",'" + name + "')";
                 conn.Open();
                 try
                 {
                     SqlCommand cmd = new SqlCommand(strSQL, conn);
                     cmd.ExecuteNonQuery();
                 }
                 catch (SqlException ex)
                 {
                     //...System.Data.SqlClient.SqlException: “microsoft”附近有语法错误
                 }
                 conn.Close();
                 */

                return Redirect("/CodeManager/Code/C_SchoolEvaStuGradeLevelItem1");//注意前面那个platform也加上不然会报错找不到视图
            }

        }
        [BaseActionFilter]
        public ActionResult detailSchoolEvaStuGradeLevelItem(string id)//查看详情
        {
            C_SchoolEvaStuGradeLevelItem temp = (from c in CodeTable.C_SchoolEvaStuGradeLevelItem where c.ItemNo == id select c).FirstOrDefault();
            ViewBag.detail = temp;
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string temp2 = temp.PracBatchID;
            string batchname = (from v in CodeTable1.PracBatch where v.PracBatchID == temp2 select v.BatchName).FirstOrDefault();
            ViewBag.batchName = batchname;

            return View("~/Areas/CodeManager/Views/Code/C_SchoolEvaStuGradeLevelItem/detailSchoolEvaStuGradeLevelItem.cshtml");

        }

        public ActionResult updateSchoolEvaStuGradeLevelItem(string id)//修改种类信息
        {
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            Session["itemno"] = id;//我在session里还没设置过期时间
            string updateid = (from c in CodeTable.C_SchoolEvaStuGradeLevelItem where c.ItemNo == id select c.PracBatchID).FirstOrDefault();



            List<T_PracBatch> temp1 = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();
            string temp2 = (from v in CodeTable1.PracBatch where v.PracBatchID == updateid select v.BatchName).FirstOrDefault();
            C_SchoolEvaStuGradeLevelItem TEMP3 = (from v in CodeTable.C_SchoolEvaStuGradeLevelItem where v.ItemNo == id select v).FirstOrDefault();
            ViewBag.update1 = TEMP3;

            ViewBag.update = temp2;
            ViewBag.piciid = temp1;
            return View("~/Areas/CodeManager/Views/Code/C_SchoolEvaStuGradeLevelItem/updateSchoolEvaStuGradeLevelItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReupdateSchoolEvaStuGradeLevelItem()//接收修改后的信息和数据库进行交互
        {
            string ItemNo = Request.Form["ItemNo"];
            string BatchName = Request.Form["BatchName"];//获取批次名的同时通过下面的几句转换成批次号
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == BatchName select v.PracBatchID).FirstOrDefault();
            string ItemName = Request.Form["ItemName"];
            string ItemSequence = Request.Form["ItemSequence"];
            int ItemSequence1 = Convert.ToInt32(ItemSequence);
            int Weight = Convert.ToInt32(Request.Form["Weight"]);
            string Note = Request.Form["Note"];

            string id = Convert.ToString(Session["itemno"]);
            if (id != ItemNo)
            {
                return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

            }
            else
            {
                C_SchoolEvaStuGradeLevelItem TEMPP = (from c in CodeTable.C_SchoolEvaStuGradeLevelItem where c.ItemNo == ItemNo select c).FirstOrDefault();

                try
                {
                    TEMPP.PracBatchID = PracBatchID;
                    TEMPP.ItemName = ItemName;
                    TEMPP.ItemSequence = ItemSequence1;
                    TEMPP.Note = Note;
                    TEMPP.Weight = Weight;
                    CodeTable.SaveChanges();

                }
                catch (Exception)
                {

                    return Content("<script>alert('这个条目被别的表关联掉了不能修改'); history.go(-1)</script>");
                }
            }

            return Redirect("/CodeManager/Code/C_SchoolEvaStuGradeLevelItem1");

        }

        #endregion 学校评价学生评估等级表管理

        #region 实习考勤项目表管理
        [BaseActionFilter]
        public ActionResult C_PracAttendanceItem1()
        {
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            List<T_PracBatch> temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();//身份验证取出该学校所有的批次号
            ViewBag.piciid = temp;

            return View("~/Areas/CodeManager/Views/Code/C_PracAttendanceItem/C_PracAttendanceItem1.cshtml");
        }
        [BaseActionFilter]
        public ActionResult GetTheDataPracAttendanceItem()
        {

            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();//获取当前登录的用户身份信息（比如这个是学校ID）
            List<C_PracAttendanceItem> ccc = (from v in CodeTable.C_PracAttendanceItem select v).ToList();
            int counthaha = ccc.Count();
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            string TheValue = Request["classValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数

            int TotalNum = 0;



            var service = new getPracAttendanceItem();
            var theEnca = service.LoadNewsData(Temp, ShenFenID, counthaha, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
            var result = from b in theEnca select b;



            if (TheAttribute == "fasong")
            {
                EnterpriseContext CodeTable1 = new EnterpriseContext();
                T_PracBatch temp123 = (from v in CodeTable1.PracBatch where v.BatchName == TheValue select v).FirstOrDefault();
                string CityIDS = temp123.PracBatchID;

                theEnca = service.LoadNewsData(Temp, CityIDS, counthaha - 9, out TotalNum);
                result = from b in theEnca select b;

            }




            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串
            //ChangeNewsState c=new ChangeNewsState();
            //string Newstr=c.ChangeState(str);

            return Content(str);
        }
        [BaseActionFilter]

        public ActionResult receajaxUnsetPracAttendanceItem()
        {
            string Alloption = Request.Form[0];//获取所有option的值然后与这里的值作比较，如果一样就不改变
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            var temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v.PracBatchID).ToList().Distinct<string>();


            string[] TTT1 = new string[150];//用来存放没有条目的批次号
            int i = 0;
            string TTT11 = "";
            foreach (var c in temp)
            {
                C_PracAttendanceItem temp1 = (from q in CodeTable.C_PracAttendanceItem where q.PracBatchID == c select q).FirstOrDefault();
                if (temp1 == null)
                {
                    string temp123 = (from a in CodeTable1.PracBatch where a.PracBatchID == c select a.BatchName).FirstOrDefault();
                    TTT1[i] += temp123;
                    i++;

                }
            }



            for (int j = 0; j < i; j++)
            {
                TTT11 += TTT1[j] + ",";
            }


            if (Alloption != TTT11)
            {
                return Content(TTT11);
            }

            else
            {

                return Content("Same");
            }
        }

        [BaseActionFilter]
        public ActionResult receajaxFuzhiPracAttendanceItem()
        {

            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            string PidToName = Request.Form[0];//要复制到这个批次号下面
            string PidBeiName = Request.Form[1];//被复制的那个批次号


            string PidTo = (from v in CodeTable1.PracBatch where v.BatchName == PidToName && v.SchoolID == ShenFenID select v.PracBatchID).FirstOrDefault();
            string PidBei = (from v in CodeTable1.PracBatch where v.BatchName == PidBeiName && v.SchoolID == ShenFenID select v.PracBatchID).FirstOrDefault();

            var temps = (from v in CodeTable.C_PracAttendanceItem where v.PracBatchID == PidBei select v).ToList();
            int count1 = temps.Count();
            int jishu = 0;
            foreach (var c in temps)
            {

                int ItemSequence1 = c.ItemSequence;
                string ItemName = c.ItemName;
                string PracBatchID = PidTo;
                string ItemNo = PidTo + "-" + ItemSequence1;
                C_PracAttendanceItem temp22 = new C_PracAttendanceItem { ItemNo = ItemNo, ItemSequence = ItemSequence1, ItemName = ItemName, PracBatchID = PracBatchID };
                CodeTable.C_PracAttendanceItem.Add(temp22);
                if (CodeTable.SaveChanges() > 0)
                {
                    jishu++;

                }

            }






            if (jishu == count1)
            {
                string spename = "chenggong";
                return Content(spename);
            }
            else
            {
                string sfalse = "shibai";
                return Content(sfalse);
            }

        }



        [BaseActionFilter]
        public ActionResult DeletePracAttendanceItem()//接受表格传来的信息删除表格信息
        {
            //删除新闻数据

            string Deleteid = Request.Form[0];//获取需要删除的专业编号组成的字符串，且以逗号分割
            string[] SplitArray1 = Deleteid.Split(',');//取到要删除的新闻编号放进数组里

            int ArrayCount = SplitArray1.Count();

            string StringCount = Convert.ToString(ArrayCount);

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string classid = SplitArray1[a];//不能直接把SplitArray1[a]放入linq语句会报错
                // int intid = Convert.ToInt32(classid);

                try
                {
                    C_PracAttendanceItem tempspe = (from v in CodeTable.C_PracAttendanceItem where v.ItemNo == classid select v).FirstOrDefault();
                    CodeTable.C_PracAttendanceItem.Remove(tempspe);
                    CodeTable.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这条条目信息在别的表被引用了不能删除');history.go(-1)</script>");
                }

            }

            return Content(StringCount);
        }
        [BaseActionFilter]

        public ActionResult receajaxGetPracAttendanceItemPID()
        {
            string batchname = Request.Form[0];
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == batchname select v.PracBatchID).FirstOrDefault();

            return Content(PracBatchID);
        }
        [BaseActionFilter]
        public ActionResult AddPracAttendanceItem()//添加新的种类信息
        {

            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            List<T_PracBatch> temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();
            ViewBag.piciid = temp;
            return View("~/Areas/CodeManager/Views/Code/C_PracAttendanceItem/AddPracAttendanceItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReAddPracAttendanceItem()//接受添加种类信息的表单和数据库交互
        {
            string ItemNo = Request.Form["ItemNo"];
            string BatchName = Request.Form["BatchName"];//获取批次名的同时通过下面的几句转换成批次号
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == BatchName select v.PracBatchID).FirstOrDefault();
            string ItemName = Request.Form["ItemName"];
            string ItemSequence = Request.Form["ItemSequence"];
            int ItemSequence1 = Convert.ToInt32(ItemSequence);


            C_PracAttendanceItem temp = (from v in CodeTable.C_PracAttendanceItem where v.ItemNo == ItemNo select v).FirstOrDefault();
            if (temp != null)
            {
                return Content("<script>alert('这个条目已经存在'); history.go(-1)</script>");

            }
            else
            {
                C_PracAttendanceItem temp22 = new C_PracAttendanceItem { ItemNo = ItemNo, ItemSequence = ItemSequence1, ItemName = ItemName, PracBatchID = PracBatchID };
                CodeTable.C_PracAttendanceItem.Add(temp22);
                CodeTable.SaveChanges();

                /* string Connectstr = "Data Source=120.24.91.132;Initial Catalog=Platform;Persist Security Info=True;Uid=sa;password=Hqu33333";
                 SqlConnection conn = new SqlConnection(Connectstr);
                 string strSQL = "insert into C_ProPosition (ProPosID,ProPosName) values(" + id222 + ",'" + name + "')";
                 conn.Open();
                 try
                 {
                     SqlCommand cmd = new SqlCommand(strSQL, conn);
                     cmd.ExecuteNonQuery();
                 }
                 catch (SqlException ex)
                 {
                     //...System.Data.SqlClient.SqlException: “microsoft”附近有语法错误
                 }
                 conn.Close();
                 */

                return Redirect("/CodeManager/Code/C_PracAttendanceItem1");//注意前面那个platform也加上不然会报错找不到视图
            }

        }
        [BaseActionFilter]
        public ActionResult detailPracAttendanceItem(string id)//查看详情
        {
            C_PracAttendanceItem temp = (from c in CodeTable.C_PracAttendanceItem where c.ItemNo == id select c).FirstOrDefault();
            ViewBag.detail = temp;
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string temp2 = temp.PracBatchID;
            string batchname = (from v in CodeTable1.PracBatch where v.PracBatchID == temp2 select v.BatchName).FirstOrDefault();
            ViewBag.batchName = batchname;

            return View("~/Areas/CodeManager/Views/Code/C_PracAttendanceItem/detailPracAttendanceItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult updatePracAttendanceItem(string id)//修改种类信息
        {



            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            Session["itemno"] = id;//我在session里还没设置过期时间
            C_PracAttendanceItem temp = (from c in CodeTable.C_PracAttendanceItem where c.ItemNo == id select c).FirstOrDefault();
            ViewBag.update1 = temp;//该条条目的全部信息
            string updateid = (from c in CodeTable.C_PracAttendanceItem where c.ItemNo == id select c.PracBatchID).FirstOrDefault();
            List<T_PracBatch> temp1 = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();
            string temp2 = (from v in CodeTable1.PracBatch where v.PracBatchID == updateid select v.BatchName).FirstOrDefault();

            ViewBag.update = temp2;
            ViewBag.piciid = temp1;
            return View("~/Areas/CodeManager/Views/Code/C_PracAttendanceItem/updatePracAttendanceItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReupdatePracAttendanceItem()//接收修改后的信息和数据库进行交互
        {
            string ItemNo = Request.Form["ItemNo"];
            string BatchName = Request.Form["BatchName"];//获取批次名的同时通过下面的几句转换成批次号
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == BatchName select v.PracBatchID).FirstOrDefault();
            string ItemName = Request.Form["ItemName"];
            string ItemSequence = Request.Form["ItemSequence"];
            int ItemSequence1 = Convert.ToInt32(ItemSequence);

            string id = Convert.ToString(Session["itemno"]);
            if (id != ItemNo)
            {
                return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

            }
            else
            {
                C_PracAttendanceItem TEMPP = (from c in CodeTable.C_PracAttendanceItem where c.ItemNo == ItemNo select c).FirstOrDefault();

                try
                {
                    TEMPP.PracBatchID = PracBatchID;
                    TEMPP.ItemName = ItemName;
                    TEMPP.ItemSequence = ItemSequence1;
                    CodeTable.SaveChanges();

                }
                catch (Exception)
                {

                    return Content("<script>alert('这个条目被别的表关联掉了不能修改'); history.go(-1)</script>");
                }
            }

            return Redirect("/CodeManager/Code/C_PracAttendanceItem1");

        }

        #endregion 实习考勤项目表管理

        #region 实习考勤项目等级表管理
        [BaseActionFilter]
        public ActionResult C_PracAttendanceGradeItem1()
        {
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            List<T_PracBatch> temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();//身份验证取出该学校所有的批次号
            ViewBag.piciid = temp;

            return View("~/Areas/CodeManager/Views/Code/C_PracAttendanceGradeItem/C_PracAttendanceGradeItem1.cshtml");
        }
        [BaseActionFilter]
        public ActionResult GetTheDataPracAttendanceGradeItem()
        {

            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();//获取当前登录的用户身份信息（比如这个是学校ID）
            List<C_PracAttendanceGradeItem> ccc = (from v in CodeTable.C_PracAttendanceGradeItem select v).ToList();
            int counthaha = ccc.Count();
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            string TheValue = Request["classValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数

            int TotalNum = 0;



            var service = new GetPracAttendanceGradeItem();
            var theEnca = service.LoadNewsData(Temp, ShenFenID, counthaha, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
            var result = from b in theEnca select b;



            if (TheAttribute == "fasong")
            {
                EnterpriseContext CodeTable1 = new EnterpriseContext();
                T_PracBatch temp123 = (from v in CodeTable1.PracBatch where v.BatchName == TheValue select v).FirstOrDefault();
                string CityIDS = temp123.PracBatchID;

                theEnca = service.LoadNewsData(Temp, CityIDS, counthaha - 9, out TotalNum);
                result = from b in theEnca select b;

            }




            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串
            //ChangeNewsState c=new ChangeNewsState();
            //string Newstr=c.ChangeState(str);

            return Content(str);
        }

        [BaseActionFilter]
        public ActionResult receajaxUnsetPracAttendanceGradeItem()
        {
            string Alloption = Request.Form[0];//获取所有option的值然后与这里的值作比较，如果一样就不改变
            //string result = System.Text.RegularExpressions.Regex.Replace(Alloption, "--请选择--", "");
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            var temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v.PracBatchID).ToList().Distinct<string>();


            string[] TTT1 = new string[150];//用来存放没有条目的批次号
            int i = 0;
            string TTT11 = "";
            foreach (var c in temp)
            {
                C_PracAttendanceGradeItem temp1 = (from q in CodeTable.C_PracAttendanceGradeItem where q.PracBatchID == c select q).FirstOrDefault();
                if (temp1 == null)
                {
                    string temp123 = (from a in CodeTable1.PracBatch where a.PracBatchID == c select a.BatchName).FirstOrDefault();
                    TTT1[i] += temp123;
                    i++;

                }
            }



            for (int j = 0; j < i; j++)
            {
                TTT11 += TTT1[j] + ",";
            }

            //string  TTT111 = "," + TTT11;

            if (Alloption != TTT11)
            {
                return Content(TTT11);
            }

            else
            {

                return Content("Same");
            }
        }

        [BaseActionFilter]
        public ActionResult receajaxFuzhiPracAttendanceGradeItem()
        {

            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            string PidToName = Request.Form[0];//要复制到这个批次号下面
            string PidBeiName = Request.Form[1];//被复制的那个批次号


            string PidTo = (from v in CodeTable1.PracBatch where v.BatchName == PidToName && v.SchoolID == ShenFenID select v.PracBatchID).FirstOrDefault();
            string PidBei = (from v in CodeTable1.PracBatch where v.BatchName == PidBeiName && v.SchoolID == ShenFenID select v.PracBatchID).FirstOrDefault();

            var temps = (from v in CodeTable.C_PracAttendanceGradeItem where v.PracBatchID == PidBei select v).ToList();
            int count1 = temps.Count();
            int jishu = 0;
            foreach (var c in temps)
            {

                int ItemSequence1 = c.ItemSequence;
                string ItemName = c.ItemName;
                string PracBatchID = PidTo;
                string ItemNo = PidTo + "-" + ItemSequence1;
                int Weight = Convert.ToInt32(c.Weight);
                string Note = c.Note;
                C_PracAttendanceGradeItem temp22 = new C_PracAttendanceGradeItem { ItemNo = ItemNo, ItemSequence = ItemSequence1, ItemName = ItemName, PracBatchID = PracBatchID, Weight = Weight, Note = Note };
                CodeTable.C_PracAttendanceGradeItem.Add(temp22);
                if (CodeTable.SaveChanges() > 0)
                {
                    jishu++;

                }

            }






            if (jishu == count1)
            {
                string spename = "chenggong";
                return Content(spename);
            }
            else
            {
                string sfalse = "shibai";
                return Content(sfalse);
            }

        }



        [BaseActionFilter]
        public ActionResult DeletePracAttendanceGradeItem()//接受表格传来的信息删除表格信息
        {
            //删除新闻数据

            string Deleteid = Request.Form[0];//获取需要删除的专业编号组成的字符串，且以逗号分割
            string[] SplitArray1 = Deleteid.Split(',');//取到要删除的新闻编号放进数组里

            int ArrayCount = SplitArray1.Count();

            string StringCount = Convert.ToString(ArrayCount);

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string classid = SplitArray1[a];//不能直接把SplitArray1[a]放入linq语句会报错
                // int intid = Convert.ToInt32(classid);

                try
                {
                    C_PracAttendanceGradeItem tempspe = (from v in CodeTable.C_PracAttendanceGradeItem where v.ItemNo == classid select v).FirstOrDefault();
                    CodeTable.C_PracAttendanceGradeItem.Remove(tempspe);
                    CodeTable.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这条条目信息在别的表被引用了不能删除');history.go(-1)</script>");
                }

            }

            return Content(StringCount);
        }


        [BaseActionFilter]
        public ActionResult receajaxGetPracAttendanceGradeItemPID()
        {
            string batchname = Request.Form[0];
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == batchname select v.PracBatchID).FirstOrDefault();

            return Content(PracBatchID);
        }
        [BaseActionFilter]
        public ActionResult AddPracAttendanceGradeItem()//添加新的种类信息
        {

            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            List<T_PracBatch> temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();

            ViewBag.piciid = temp;
            return View("~/Areas/CodeManager/Views/Code/C_PracAttendanceGradeItem/AddPracAttendanceGradeItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReAddPracAttendanceGradeItem()//接受添加种类信息的表单和数据库交互
        {
            string ItemNo = Request.Form["ItemNo"];
            string BatchName = Request.Form["BatchName"];//获取批次名的同时通过下面的几句转换成批次号
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == BatchName select v.PracBatchID).FirstOrDefault();
            string ItemName = Request.Form["ItemName"];
            string ItemSequence = Request.Form["ItemSequence"];
            int Weight = Convert.ToInt32(Request.Form["Weight"]);
            string Note = Request.Form["Note"];
            int ItemSequence1 = Convert.ToInt32(ItemSequence);

            C_PracAttendanceGradeItem temp = (from v in CodeTable.C_PracAttendanceGradeItem where v.ItemNo == ItemNo select v).FirstOrDefault();
            if (temp != null)
            {
                return Content("<script>alert('这个条目已经存在'); history.go(-1)</script>");

            }
            else
            {
                C_PracAttendanceGradeItem temp22 = new C_PracAttendanceGradeItem { ItemNo = ItemNo, ItemSequence = ItemSequence1, ItemName = ItemName, PracBatchID = PracBatchID, Weight = Weight, Note = Note };
                CodeTable.C_PracAttendanceGradeItem.Add(temp22);
                CodeTable.SaveChanges();

                /* string Connectstr = "Data Source=120.24.91.132;Initial Catalog=Platform;Persist Security Info=True;Uid=sa;password=Hqu33333";
                 SqlConnection conn = new SqlConnection(Connectstr);
                 string strSQL = "insert into C_ProPosition (ProPosID,ProPosName) values(" + id222 + ",'" + name + "')";
                 conn.Open();
                 try
                 {
                     SqlCommand cmd = new SqlCommand(strSQL, conn);
                     cmd.ExecuteNonQuery();
                 }
                 catch (SqlException ex)
                 {
                     //...System.Data.SqlClient.SqlException: “microsoft”附近有语法错误
                 }
                 conn.Close();
                 */

                return Redirect("/CodeManager/Code/C_PracAttendanceGradeItem1");//注意前面那个platform也加上不然会报错找不到视图
            }

        }
        [BaseActionFilter]
        public ActionResult detailPracAttendanceGradeItem(string id)//查看详情
        {
            C_PracAttendanceGradeItem temp = (from c in CodeTable.C_PracAttendanceGradeItem where c.ItemNo == id select c).FirstOrDefault();
            ViewBag.detail = temp;
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string temp2 = temp.PracBatchID;
            string batchname = (from v in CodeTable1.PracBatch where v.PracBatchID == temp2 select v.BatchName).FirstOrDefault();
            ViewBag.batchName = batchname;

            return View("~/Areas/CodeManager/Views/Code/C_PracAttendanceGradeItem/detailPracAttendanceGradeItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult updatePracAttendanceGradeItem(string id)//修改种类信息
        {
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            Session["itemno"] = id;//我在session里还没设置过期时间
            string updateid = (from c in CodeTable.C_PracAttendanceGradeItem where c.ItemNo == id select c.PracBatchID).FirstOrDefault();



            List<T_PracBatch> temp1 = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();
            string temp2 = (from v in CodeTable1.PracBatch where v.PracBatchID == updateid select v.BatchName).FirstOrDefault();
            C_PracAttendanceGradeItem TEMP3 = (from v in CodeTable.C_PracAttendanceGradeItem where v.ItemNo == id select v).FirstOrDefault();
            ViewBag.update1 = TEMP3;

            ViewBag.update = temp2;
            ViewBag.piciid = temp1;
            return View("~/Areas/CodeManager/Views/Code/C_PracAttendanceGradeItem/updatePracAttendanceGradeItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReupdatePracAttendanceGradeItem()//接收修改后的信息和数据库进行交互
        {
            string ItemNo = Request.Form["ItemNo"];
            string BatchName = Request.Form["BatchName"];//获取批次名的同时通过下面的几句转换成批次号
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == BatchName select v.PracBatchID).FirstOrDefault();
            string ItemName = Request.Form["ItemName"];
            string ItemSequence = Request.Form["ItemSequence"];
            int ItemSequence1 = Convert.ToInt32(ItemSequence);
            int Weight = Convert.ToInt32(Request.Form["Weight"]);
            string Note = Request.Form["Note"];

            string id = Convert.ToString(Session["itemno"]);
            if (id != ItemNo)
            {
                return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

            }
            else
            {
                C_PracAttendanceGradeItem TEMPP = (from c in CodeTable.C_PracAttendanceGradeItem where c.ItemNo == ItemNo select c).FirstOrDefault();

                try
                {
                    TEMPP.PracBatchID = PracBatchID;
                    TEMPP.ItemName = ItemName;
                    TEMPP.ItemSequence = ItemSequence1;
                    TEMPP.Note = Note;
                    TEMPP.Weight = Weight;
                    CodeTable.SaveChanges();

                }
                catch (Exception)
                {

                    return Content("<script>alert('这个条目被别的表关联掉了不能修改'); history.go(-1)</script>");
                }
            }

            return Redirect("/CodeManager/Code/C_PracAttendanceGradeItem1");

        }
        #endregion 实习考勤项目等级表管理



         #region 实习案例表管理


        [BaseActionFilter]
        public ActionResult C_PracticeCaseItem1()
        {
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            List<T_PracBatch> temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();//身份验证取出该学校所有的批次号
            ViewBag.piciid = temp;

            return View("~/Areas/CodeManager/Views/Code/C_PracticeCaseItem/C_PracticeCaseItem1.cshtml");
        }
        [BaseActionFilter]
        public ActionResult GetTheDataPracticeCaseItem()
        {

            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();//获取当前登录的用户身份信息（比如这个是学校ID）
            List<C_PracticeCaseItem> ccc = (from v in CodeTable.C_PracticeCaseItem select v).ToList();
            int counthaha = ccc.Count();
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            string TheValue = Request["classValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数

            int TotalNum = 0;



            var service = new getPracticeCaseItem();
            var theEnca = service.LoadNewsData(Temp, ShenFenID, counthaha, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
            var result = from b in theEnca select b;



            if (TheAttribute == "fasong")
            {
                EnterpriseContext CodeTable1 = new EnterpriseContext();
                T_PracBatch temp123 = (from v in CodeTable1.PracBatch where v.BatchName == TheValue select v).FirstOrDefault();
                string CityIDS = temp123.PracBatchID;

                theEnca = service.LoadNewsData(Temp, CityIDS, counthaha - 9, out TotalNum);
                result = from b in theEnca select b;

            }




            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串
            //ChangeNewsState c=new ChangeNewsState();
            //string Newstr=c.ChangeState(str);

            return Content(str);
        }

        [BaseActionFilter]
        public ActionResult receajaxUnsetPracticeCaseItem()
        {
            string Alloption = Request.Form[0];//获取所有option的值然后与这里的值作比较，如果一样就不改变
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            var temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v.PracBatchID).ToList().Distinct<string>();


            string[] TTT1 = new string[150];//用来存放没有条目的批次号
            int i = 0;
            string TTT11 = "";
            foreach (var c in temp)
            {
                C_PracticeCaseItem temp1 = (from q in CodeTable.C_PracticeCaseItem where q.PracBatchID == c select q).FirstOrDefault();
                if (temp1 == null)
                {
                    string temp123 = (from a in CodeTable1.PracBatch where a.PracBatchID == c select a.BatchName).FirstOrDefault();
                    TTT1[i] += temp123;
                    i++;

                }
            }



            for (int j = 0; j < i; j++)
            {
                TTT11 += TTT1[j] + ",";
            }


            if (Alloption != TTT11)
            {
                return Content(TTT11);
            }

            else
            {

                return Content("Same");
            }
        }

        [BaseActionFilter]
        public ActionResult receajaxFuzhiPracticeCaseItem()
        {

            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            string PidToName = Request.Form[0];//要复制到这个批次号下面
            string PidBeiName = Request.Form[1];//被复制的那个批次号


            string PidTo = (from v in CodeTable1.PracBatch where v.BatchName == PidToName && v.SchoolID == ShenFenID select v.PracBatchID).FirstOrDefault();
            string PidBei = (from v in CodeTable1.PracBatch where v.BatchName == PidBeiName && v.SchoolID == ShenFenID select v.PracBatchID).FirstOrDefault();

            var temps = (from v in CodeTable.C_PracticeCaseItem where v.PracBatchID == PidBei select v).ToList();
            int count1 = temps.Count();
            int jishu = 0;
            foreach (var c in temps)
            {

                int ItemSequence1 = c.ItemSequence;
                string ItemName = c.ItemName;
                string PracBatchID = PidTo;
                string ItemNo = PidTo + "-" + ItemSequence1;
                C_PracticeCaseItem temp22 = new C_PracticeCaseItem { ItemNo = ItemNo, ItemSequence = ItemSequence1, ItemName = ItemName, PracBatchID = PracBatchID };
                CodeTable.C_PracticeCaseItem.Add(temp22);
                if (CodeTable.SaveChanges() > 0)
                {
                    jishu++;

                }

            }






            if (jishu == count1)
            {
                string spename = "chenggong";
                return Content(spename);
            }
            else
            {
                string sfalse = "shibai";
                return Content(sfalse);
            }

        }



        [BaseActionFilter]
        public ActionResult DeletePracticeCaseItem()//接受表格传来的信息删除表格信息
        {
            //删除新闻数据

            string Deleteid = Request.Form[0];//获取需要删除的专业编号组成的字符串，且以逗号分割
            string[] SplitArray1 = Deleteid.Split(',');//取到要删除的新闻编号放进数组里

            int ArrayCount = SplitArray1.Count();

            string StringCount = Convert.ToString(ArrayCount);

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string classid = SplitArray1[a];//不能直接把SplitArray1[a]放入linq语句会报错
                // int intid = Convert.ToInt32(classid);

                try
                {
                    C_PracticeCaseItem tempspe = (from v in CodeTable.C_PracticeCaseItem where v.ItemNo == classid select v).FirstOrDefault();
                    CodeTable.C_PracticeCaseItem.Remove(tempspe);
                    CodeTable.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这条条目信息在别的表被引用了不能删除');history.go(-1)</script>");
                }

            }

            return Content(StringCount);
        }

        [BaseActionFilter]
        public ActionResult receajaxGetPracticeCaseItemPID()
        {
            string batchname = Request.Form[0];
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == batchname select v.PracBatchID).FirstOrDefault();

            return Content(PracBatchID);
        }
        public ActionResult AddPracticeCaseItem()//添加新的种类信息
        {

            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            List<T_PracBatch> temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();
            ViewBag.piciid = temp;
            return View("~/Areas/CodeManager/Views/Code/C_PracticeCaseItem/AddPracticeCaseItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReAddPracticeCaseItem()//接受添加种类信息的表单和数据库交互
        {
            string ItemNo = Request.Form["ItemNo"];
            string BatchName = Request.Form["BatchName"];//获取批次名的同时通过下面的几句转换成批次号
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == BatchName select v.PracBatchID).FirstOrDefault();
            string ItemName = Request.Form["ItemName"];
            string ItemSequence = Request.Form["ItemSequence"];
            int ItemSequence1 = Convert.ToInt32(ItemSequence);


            C_PracticeCaseItem temp = (from v in CodeTable.C_PracticeCaseItem where v.ItemNo == ItemNo select v).FirstOrDefault();
            if (temp != null)
            {
                return Content("<script>alert('这个条目已经存在'); history.go(-1)</script>");

            }
            else
            {
                C_PracticeCaseItem temp22 = new C_PracticeCaseItem { ItemNo = ItemNo, ItemSequence = ItemSequence1, ItemName = ItemName, PracBatchID = PracBatchID };
                CodeTable.C_PracticeCaseItem.Add(temp22);
                CodeTable.SaveChanges();

                /* string Connectstr = "Data Source=120.24.91.132;Initial Catalog=Platform;Persist Security Info=True;Uid=sa;password=Hqu33333";
                 SqlConnection conn = new SqlConnection(Connectstr);
                 string strSQL = "insert into C_ProPosition (ProPosID,ProPosName) values(" + id222 + ",'" + name + "')";
                 conn.Open();
                 try
                 {
                     SqlCommand cmd = new SqlCommand(strSQL, conn);
                     cmd.ExecuteNonQuery();
                 }
                 catch (SqlException ex)
                 {
                     //...System.Data.SqlClient.SqlException: “microsoft”附近有语法错误
                 }
                 conn.Close();
                 */

                return Redirect("/CodeManager/Code/C_PracticeCaseItem1");//注意前面那个platform也加上不然会报错找不到视图
            }

        }
        [BaseActionFilter]
        public ActionResult detailPracticeCaseItem(string id)//查看详情
        {
            C_PracticeCaseItem temp = (from c in CodeTable.C_PracticeCaseItem where c.ItemNo == id select c).FirstOrDefault();
            ViewBag.detail = temp;
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string temp2 = temp.PracBatchID;
            string batchname = (from v in CodeTable1.PracBatch where v.PracBatchID == temp2 select v.BatchName).FirstOrDefault();
            ViewBag.batchName = batchname;

            return View("~/Areas/CodeManager/Views/Code/C_PracticeCaseItem/detailPracticeCaseItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult updatePracticeCaseItem(string id)//修改种类信息
        {



            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            Session["itemno"] = id;//我在session里还没设置过期时间
            C_PracticeCaseItem temp = (from c in CodeTable.C_PracticeCaseItem where c.ItemNo == id select c).FirstOrDefault();
            ViewBag.update1 = temp;//该条条目的全部信息
            string updateid = (from c in CodeTable.C_PracticeCaseItem where c.ItemNo == id select c.PracBatchID).FirstOrDefault();
            List<T_PracBatch> temp1 = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();
            string temp2 = (from v in CodeTable1.PracBatch where v.PracBatchID == updateid select v.BatchName).FirstOrDefault();

            ViewBag.update = temp2;
            ViewBag.piciid = temp1;
            return View("~/Areas/CodeManager/Views/Code/C_PracticeCaseItem/updatePracticeCaseItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReupdatePracticeCaseItem()//接收修改后的信息和数据库进行交互
        {
            string ItemNo = Request.Form["ItemNo"];
            string BatchName = Request.Form["BatchName"];//获取批次名的同时通过下面的几句转换成批次号
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == BatchName select v.PracBatchID).FirstOrDefault();
            string ItemName = Request.Form["ItemName"];
            string ItemSequence = Request.Form["ItemSequence"];
            int ItemSequence1 = Convert.ToInt32(ItemSequence);

            string id = Convert.ToString(Session["itemno"]);
            if (id != ItemNo)
            {
                return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

            }
            else
            {
                C_PracticeCaseItem TEMPP = (from c in CodeTable.C_PracticeCaseItem where c.ItemNo == ItemNo select c).FirstOrDefault();

                try
                {
                    TEMPP.PracBatchID = PracBatchID;
                    TEMPP.ItemName = ItemName;
                    TEMPP.ItemSequence = ItemSequence1;
                    CodeTable.SaveChanges();

                }
                catch (Exception)
                {

                    return Content("<script>alert('这个条目被别的表关联掉了不能修改'); history.go(-1)</script>");
                }
            }

            return Redirect("/CodeManager/Code/C_PracticeCaseItem1");

        }
         #endregion 实习案例表管理


        #region 实习鉴定表管理

        [BaseActionFilter]
        public ActionResult C_PracticeIdentificationItem1()
        {
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            List<T_PracBatch> temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();//身份验证取出该学校所有的批次号
            ViewBag.piciid = temp;

            return View("~/Areas/CodeManager/Views/Code/C_PracticeIdentificationItem/C_PracticeIdentificationItem1.cshtml");
        }
        [BaseActionFilter]
        public ActionResult GetTheDataPracticeIdentificationItem()
        {

            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();//获取当前登录的用户身份信息（比如这个是学校ID）
            List<C_PracticeIdentificationItem> ccc = (from v in CodeTable.C_PracticeIdentificationItem select v).ToList();
            int counthaha = ccc.Count();
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            string TheValue = Request["classValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数

            int TotalNum = 0;



            var service = new getPracticeIdentificationItem();
            var theEnca = service.LoadNewsData(Temp, ShenFenID, counthaha, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
            var result = from b in theEnca select b;



            if (TheAttribute == "fasong")
            {
                EnterpriseContext CodeTable1 = new EnterpriseContext();
                T_PracBatch temp123 = (from v in CodeTable1.PracBatch where v.BatchName == TheValue select v).FirstOrDefault();
                string CityIDS = temp123.PracBatchID;

                theEnca = service.LoadNewsData(Temp, CityIDS, counthaha - 9, out TotalNum);
                result = from b in theEnca select b;

            }




            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串
            //ChangeNewsState c=new ChangeNewsState();
            //string Newstr=c.ChangeState(str);

            return Content(str);
        }

        [BaseActionFilter]
        public ActionResult receajaxUnsetPracticeIdentificationItem()
        {
            string Alloption = Request.Form[0];//获取所有option的值然后与这里的值作比较，如果一样就不改变
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            var temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v.PracBatchID).ToList().Distinct<string>();


            string[] TTT1 = new string[150];//用来存放没有条目的批次号
            int i = 0;
            string TTT11 = "";
            foreach (var c in temp)
            {
                C_PracticeIdentificationItem temp1 = (from q in CodeTable.C_PracticeIdentificationItem where q.PracBatchID == c select q).FirstOrDefault();
                if (temp1 == null)
                {
                    string temp123 = (from a in CodeTable1.PracBatch where a.PracBatchID == c select a.BatchName).FirstOrDefault();
                    TTT1[i] += temp123;
                    i++;

                }
            }



            for (int j = 0; j < i; j++)
            {
                TTT11 += TTT1[j] + ",";
            }


            if (Alloption != TTT11)
            {
                return Content(TTT11);
            }

            else
            {

                return Content("Same");
            }
        }

        [BaseActionFilter]
        public ActionResult receajaxFuzhiPracticeIdentificationItem()
        {

            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            string PidToName = Request.Form[0];//要复制到这个批次号下面
            string PidBeiName = Request.Form[1];//被复制的那个批次号


            string PidTo = (from v in CodeTable1.PracBatch where v.BatchName == PidToName && v.SchoolID == ShenFenID select v.PracBatchID).FirstOrDefault();
            string PidBei = (from v in CodeTable1.PracBatch where v.BatchName == PidBeiName && v.SchoolID == ShenFenID select v.PracBatchID).FirstOrDefault();

            var temps = (from v in CodeTable.C_PracticeIdentificationItem where v.PracBatchID == PidBei select v).ToList();
            int count1 = temps.Count();
            int jishu = 0;
            foreach (var c in temps)
            {

                int ItemSequence1 = c.ItemSequence;
                string ItemName = c.ItemName;
                string PracBatchID = PidTo;
                string ItemNo = PidTo  + "-"+ItemSequence1;
                C_PracticeIdentificationItem temp22 = new C_PracticeIdentificationItem { ItemNo = ItemNo, ItemSequence = ItemSequence1, ItemName = ItemName, PracBatchID = PracBatchID };
                CodeTable.C_PracticeIdentificationItem.Add(temp22);
                if (CodeTable.SaveChanges() > 0)
                {
                    jishu++;

                }

            }






            if (jishu == count1)
            {
                string spename = "chenggong";
                return Content(spename);
            }
            else
            {
                string sfalse = "shibai";
                return Content(sfalse);
            }

        }


        [BaseActionFilter]

        public ActionResult DeletePracticeIdentificationItem()//接受表格传来的信息删除表格信息
        {
            //删除新闻数据

            string Deleteid = Request.Form[0];//获取需要删除的专业编号组成的字符串，且以逗号分割
            string[] SplitArray1 = Deleteid.Split(',');//取到要删除的新闻编号放进数组里

            int ArrayCount = SplitArray1.Count();

            string StringCount = Convert.ToString(ArrayCount);

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string classid = SplitArray1[a];//不能直接把SplitArray1[a]放入linq语句会报错
                // int intid = Convert.ToInt32(classid);

                try
                {
                    C_PracticeIdentificationItem tempspe = (from v in CodeTable.C_PracticeIdentificationItem where v.ItemNo == classid select v).FirstOrDefault();
                    CodeTable.C_PracticeIdentificationItem.Remove(tempspe);
                    CodeTable.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这条条目信息在别的表被引用了不能删除');history.go(-1)</script>");
                }

            }

            return Content(StringCount);
        }

        [BaseActionFilter]
        public ActionResult receajaxGetPracticeIdentificationItemPID()
        {
            string batchname = Request.Form[0];
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == batchname select v.PracBatchID).FirstOrDefault();

            return Content(PracBatchID);
        }
        public ActionResult AddPracticeIdentificationItem()//添加新的种类信息
        {

            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            List<T_PracBatch> temp = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();
            ViewBag.piciid = temp;
            return View("~/Areas/CodeManager/Views/Code/C_PracticeIdentificationItem/AddPracticeIdentificationItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReAddPracticeIdentificationItem()//接受添加种类信息的表单和数据库交互
        {
            string ItemNo = Request.Form["ItemNo"];
            string BatchName = Request.Form["BatchName"];//获取批次名的同时通过下面的几句转换成批次号
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == BatchName select v.PracBatchID).FirstOrDefault();
            string ItemName = Request.Form["ItemName"];
            string ItemSequence = Request.Form["ItemSequence"];
            int ItemSequence1 = Convert.ToInt32(ItemSequence);


            C_PracticeIdentificationItem temp = (from v in CodeTable.C_PracticeIdentificationItem where v.ItemNo == ItemNo select v).FirstOrDefault();
            if (temp != null)
            {
                return Content("<script>alert('这个条目已经存在'); history.go(-1)</script>");

            }
            else
            {
                C_PracticeIdentificationItem temp22 = new C_PracticeIdentificationItem { ItemNo = ItemNo, ItemSequence = ItemSequence1, ItemName = ItemName, PracBatchID = PracBatchID };
                CodeTable.C_PracticeIdentificationItem.Add(temp22);
                CodeTable.SaveChanges();

                /* string Connectstr = "Data Source=120.24.91.132;Initial Catalog=Platform;Persist Security Info=True;Uid=sa;password=Hqu33333";
                 SqlConnection conn = new SqlConnection(Connectstr);
                 string strSQL = "insert into C_ProPosition (ProPosID,ProPosName) values(" + id222 + ",'" + name + "')";
                 conn.Open();
                 try
                 {
                     SqlCommand cmd = new SqlCommand(strSQL, conn);
                     cmd.ExecuteNonQuery();
                 }
                 catch (SqlException ex)
                 {
                     //...System.Data.SqlClient.SqlException: “microsoft”附近有语法错误
                 }
                 conn.Close();
                 */

                return Redirect("/CodeManager/Code/C_PracticeIdentificationItem1");//注意前面那个platform也加上不然会报错找不到视图
            }

        }
        [BaseActionFilter]
        public ActionResult detailPracticeIdentificationItem(string id)//查看详情
        {
            C_PracticeIdentificationItem temp = (from c in CodeTable.C_PracticeIdentificationItem where c.ItemNo == id select c).FirstOrDefault();
            ViewBag.detail = temp;
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string temp2 = temp.PracBatchID;
            string batchname = (from v in CodeTable1.PracBatch where v.PracBatchID == temp2 select v.BatchName).FirstOrDefault();
            ViewBag.batchName = batchname;

            return View("~/Areas/CodeManager/Views/Code/C_PracticeIdentificationItem/detailPracticeIdentificationItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult updatePracticeIdentificationItem(string id)//修改种类信息
        {



            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            Session["itemno"] = id;//我在session里还没设置过期时间
            C_PracticeIdentificationItem temp = (from c in CodeTable.C_PracticeIdentificationItem where c.ItemNo == id select c).FirstOrDefault();
            ViewBag.update1 = temp;//该条条目的全部信息
            string updateid = (from c in CodeTable.C_PracticeIdentificationItem where c.ItemNo == id select c.PracBatchID).FirstOrDefault();
            List<T_PracBatch> temp1 = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID select v).ToList();
            string temp2 = (from v in CodeTable1.PracBatch where v.PracBatchID == updateid select v.BatchName).FirstOrDefault();

            ViewBag.update = temp2;
            ViewBag.piciid = temp1;
            return View("~/Areas/CodeManager/Views/Code/C_PracticeIdentificationItem/updatePracticeIdentificationItem.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReupdatePracticeIdentificationItem()//接收修改后的信息和数据库进行交互
        {
            string ItemNo = Request.Form["ItemNo"];
            string BatchName = Request.Form["BatchName"];//获取批次名的同时通过下面的几句转换成批次号
            string ShenFenID = (Session["Vars"] as Vars).getUserUnit();
            EnterpriseContext CodeTable1 = new EnterpriseContext();
            string PracBatchID = (from v in CodeTable1.PracBatch where v.SchoolID == ShenFenID && v.BatchName == BatchName select v.PracBatchID).FirstOrDefault();
            string ItemName = Request.Form["ItemName"];
            string ItemSequence = Request.Form["ItemSequence"];
            int ItemSequence1 = Convert.ToInt32(ItemSequence);

            string id = Convert.ToString(Session["itemno"]);
            if (id != ItemNo)
            {
                return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

            }
            else
            {
                C_PracticeIdentificationItem TEMPP = (from c in CodeTable.C_PracticeIdentificationItem where c.ItemNo == ItemNo select c).FirstOrDefault();

                try
                {
                    TEMPP.PracBatchID = PracBatchID;
                    TEMPP.ItemName = ItemName;
                    TEMPP.ItemSequence = ItemSequence1;
                    CodeTable.SaveChanges();

                }
                catch (Exception)
                {

                    return Content("<script>alert('这个条目被别的表关联掉了不能修改'); history.go(-1)</script>");
                }
            }

            return Redirect("/CodeManager/Code/C_PracticeIdentificationItem1");

        }
        #endregion 实习鉴定表管理
        
        
        #region 内容栏目管理
        [BaseActionFilter]

        public ActionResult C_ContentColumn1()
        {
            return View("~/Areas/CodeManager/Views/Code/C_ContentColumn/C_ContentColumn1.cshtml");
        }
        [BaseActionFilter]
        public ActionResult GetTheDataContentColumn()
        {
            //新闻列表分页和查询功能

            //接受DataGrid传来的参数

            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            string TheValue = Request["classValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数
            int TotalNum = 0;
            var service = new getContentColumn();
            var theEnca = service.LoadNewsData(Temp, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
           // var result1 = from b in theEnca select b;

            #region Category Search

          //  CodeTable.Database.ExecuteSqlCommand("");

            //依据搜索框传递的属性名和相应值得到搜索出来的模型
            // List<T_Class> GetSpe = CodeTable.T_Class.ToList();



           var  result = from a in theEnca from b in Contentdb.C_ContentType where a.ContTypeID == b.ConTypeID select new {a.ContColumnID,a.ContColumnName,b.ConTypeName,a.SybSystem };

            if (TheAttribute == "ContColumnID")
            {
                int TargetID = Convert.ToInt32(TheValue);
                result = from a in result where (String.IsNullOrEmpty(TheValue) || a.ContColumnID == TargetID) select a;
            }
            if (TheAttribute == "ConTypeName")
            {
                
                result = from a in result where (String.IsNullOrEmpty(TheValue) || a.ConTypeName.Contains(TheValue)) select a;
            }
            if (TheAttribute == "ContColumnName")
            {
                result = from a in result where (String.IsNullOrEmpty(TheValue) || a.ContColumnName.Contains(TheValue)) select a;
            }
            if (TheAttribute == "SybSystem")
            {
                result = from a in result where (String.IsNullOrEmpty(TheValue) || a.SybSystem.Contains(TheValue)) select a;
            }

            #endregion


            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串
            //ChangeNewsState c=new ChangeNewsState();
            //string Newstr=c.ChangeState(str);

            return Content(str);
        }

        [BaseActionFilter]
        public ActionResult DeleteContentColumn()//接受表格传来的信息删除表格信息
        {
            //删除新闻数据

            string Deleteid = Request.Form[0];//获取需要删除的专业编号组成的字符串，且以逗号分割
            string[] SplitArray1 = Deleteid.Split(',');//取到要删除的新闻编号放进数组里

            int ArrayCount = SplitArray1.Count();

            string StringCount = Convert.ToString(ArrayCount);

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                int classid = Convert.ToInt32(SplitArray1[a]);//不能直接把SplitArray1[a]放入linq语句会报错

                try
                {
                    C_ContentColumn tempspe = (from v in Contentdb.C_ContentColumn where v.ContColumnID == classid select v).FirstOrDefault();
                    Contentdb.C_ContentColumn.Remove(tempspe);
                    Contentdb.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这条种类信息在别的表被引用了不能删除');history.go(-1)</script>");
                }

            }

            return Content(StringCount);
        }


        [BaseActionFilter]
        public ActionResult AddContentColumn()//添加新的种类信息
        {

            List<C_ContentType> temp = (from v in Contentdb.C_ContentType select v).ToList();
            ViewBag.temp = temp;
            string[] system = { SubSystem.SCHOOL, SubSystem.PLATFORM, SubSystem.ENTERPRISE };
            ViewBag.system = system;
            return View("~/Areas/CodeManager/Views/Code/C_ContentColumn/AddContentColumn.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReAddContentColumn()//接受添加种类信息的表单和数据库交互
        {
            int ContColumnID = Convert.ToInt32(Request.Form["ContColumnID"]);
            string ContColumnName = Request.Form["ContColumnName"];
            string SybSystem = Request.Form["SybSystem"];
            string ContTypeName = Request.Form["ContTypeName"];
            int ContTypeID = (from v in Contentdb.C_ContentType where v.ConTypeName == ContTypeName select v.ConTypeID).FirstOrDefault();
            C_ContentColumn temp = (from v in Contentdb.C_ContentColumn where v.ContColumnID == ContColumnID select v).FirstOrDefault();
            if (temp != null)
            {
                return Content("<script>alert('这个种类已经存在'); history.go(-1)</script>");

            }
            else
            {/*
                C_ContentColumn temp1 = new C_ContentColumn { ContColumnID=ContColumnID,ContColumnName=ContColumnName,ContTypeID=ContColumnID,SybSystem=SybSystem };
                Contentdb.C_ContentColumn.Add(temp1);
                Contentdb.SaveChanges();
               */
                Contentdb.Database.ExecuteSqlCommand("insert into C_ContentColumn (ContColumnID,ContColumnName,ContTypeID,SybSystem) values(" + ContColumnID + ",'" + ContColumnName + "','" + ContTypeID + "','" + SybSystem + "')");
                
                

            }
            return Redirect("/Code/C_ContentColumn1");//注意前面那个platform也加上不然会报错找不到视图


        }
        [BaseActionFilter]
        public ActionResult detailContentColumn(int id)//查看详情
        {
            C_ContentColumn temp = (from c in Contentdb.C_ContentColumn where c.ContColumnID == id select c).FirstOrDefault();
            ViewBag.detail = temp;
            int temp2 = temp.ContTypeID;
            string conttypeName = (from v in Contentdb.C_ContentType where v.ConTypeID == temp2 select v.ConTypeName).FirstOrDefault();
            ViewBag.contypeName = conttypeName;
            return View("~/Areas/CodeManager/Views/Code/C_ContentColumn/detailContentColumn.cshtml");

        }
        [BaseActionFilter]
        public ActionResult updateContentColumn(int id)//修改种类信息
        {
            string[] systemStr = { SubSystem.SCHOOL, SubSystem.PLATFORM, SubSystem.ENTERPRISE };

            ViewBag.system = systemStr;
            List<C_ContentType> temp1 = (from v in Contentdb.C_ContentType select v).ToList();
            ViewBag.temp1 = temp1;
            Session["enid"] = id;//我在session里还没设置过期时间
            C_ContentColumn temp = (from c in Contentdb.C_ContentColumn where c.ContColumnID == id select c).FirstOrDefault();
            ViewBag.update = temp;
            int  temp2 = temp.ContTypeID;
            string conttypeName = (from v in Contentdb.C_ContentType where v.ConTypeID == temp2 select v.ConTypeName).FirstOrDefault();
            ViewBag.contypeName = conttypeName;


            return View("~/Areas/CodeManager/Views/Code/C_ContentColumn/updateContentColumn.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReupdateContentColumn()//接收修改后的信息和数据库进行交互
        {

           
            int ContColumnID = Convert.ToInt32(Request.Form["ContColumnID"]);
            string ContColumnName = Request.Form["ContColumnName"];
            string SybSystem = Request.Form["SybSystem"];
            string ContTypeName = Request.Form["ContTypeName"];
            int ContTypeID = (from v in Contentdb.C_ContentType where v.ConTypeName == ContTypeName select v.ConTypeID).FirstOrDefault();
            int id = Convert.ToInt32(Session["enid"]);
            if (id != ContColumnID)
            {
                return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

            }
            else
            {
                C_ContentColumn TEMPP = (from c in Contentdb.C_ContentColumn where c.ContColumnID == id select c).FirstOrDefault();

                try
                {
                   
                    TEMPP.ContColumnName = ContColumnName;
                    TEMPP.ContTypeID = ContTypeID;
                    TEMPP.SybSystem = SybSystem;
                    Contentdb.SaveChanges();

                }
                catch (Exception)
                {

                    return Content("<script>alert('这个种类被别的表关联掉了不能修改'); history.go(-1)</script>");
                }

                /*  string Connectstr = "Data Source=120.24.91.132;Initial Catalog=Platform;Persist Security Info=True;Uid=sa;password=Hqu33333";
                  SqlConnection conn = new SqlConnection(Connectstr);
                  string strSQL = "update C_ContentColumn set ContColumnName=" + ContColumnName + ",ContTypeID=" + ContTypeID + ",SybSystem=" + SybSystem + "where ContColumnID=" + ContColumnID + "";
                  conn.Open();
                  try
                  {
                      SqlCommand cmd = new SqlCommand(strSQL, conn);
                      cmd.ExecuteNonQuery();
                  }
                  catch (SqlException ex)
                  {
                      return Content("<script>alert('这个种类被别的表关联掉了不能修改'); history.go(-1)</script>");
                      //...System.Data.SqlClient.SqlException: “microsoft”附近有语法错误
                  }
                  conn.Close();
              }*/
            }
            return Redirect("/CodeManager/Code/C_ContentColumn1");

        }


        #endregion 内容栏目管理

        #region 内容类型表管理
        [BaseActionFilter]
        public ActionResult C_ContentType1()
        {
            return View("~/Areas/CodeManager/Views/Code/C_ContentType/C_ContentType1.cshtml");
        }
        [BaseActionFilter]
        public ActionResult GetTheDataContentType()
        {
            //新闻列表分页和查询功能

            //接受DataGrid传来的参数

            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            string TheValue = Request["classValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数
            int TotalNum = 0;
            var service = new getContentType();
            var theEnca = service.LoadNewsData(Temp, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
            var result = from b in theEnca select b;

            #region Category Search



            //依据搜索框传递的属性名和相应值得到搜索出来的模型
            // List<T_Class> GetSpe = CodeTable.T_Class.ToList();



            //var result = from a in theNews from b in GetState where a.FlowState==b.NewsStateID select new { a.NewsID, a.NewsTitle, a.NewsTypeID, a.NewsAuthor, a.NewsPublisher, a.PubDate, b.NewsStateName, a.IsShow, a.IsLocked, a.NewsColunmID, a.PicUrl, a.VideoUrl, a.Html, a.LinkUrl, a.Download };

            if (TheAttribute == "ConTypeID")
            {
                int ivalue = Convert.ToInt32(TheValue);
                result = from a in result where (String.IsNullOrEmpty(TheValue) || a.ConTypeID == ivalue) select a;
            }
            if (TheAttribute == "ConTypeName")
            {
                result = from a in result where (String.IsNullOrEmpty(TheValue) || a.ConTypeName.Contains(TheValue)) select a;
            }

            #endregion


            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串
            //ChangeNewsState c=new ChangeNewsState();
            //string Newstr=c.ChangeState(str);

            return Content(str);
        }
        [BaseActionFilter]

        public ActionResult DeleteContentType()//接受表格传来的信息删除表格信息
        {
            //删除新闻数据

            string Deleteid = Request.Form[0];//获取需要删除的专业编号组成的字符串，且以逗号分割
            string[] SplitArray1 = Deleteid.Split(',');//取到要删除的新闻编号放进数组里

            int ArrayCount = SplitArray1.Count();

            string StringCount = Convert.ToString(ArrayCount);

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string classid = SplitArray1[a];//不能直接把SplitArray1[a]放入linq语句会报错
                int intid = Convert.ToInt32(classid);

                try
                {
                    C_ContentType tempspe = (from v in Contentdb.C_ContentType where v.ConTypeID == intid select v).FirstOrDefault();
                    Contentdb.C_ContentType.Remove(tempspe);
                    Contentdb.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这条申请状态信息在别的表被引用了不能删除');history.go(-1)</script>");
                }

            }

            return Content(StringCount);
        }


        [BaseActionFilter]
        public ActionResult AddContentType()//添加新的种类信息
        {
            return View("~/Areas/CodeManager/Views/Code/C_ContentType/AddContentType.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReAddContentType()//接受添加种类信息的表单和数据库交互
        {
            string id1 = Request.Form["ConTypeID"];
            int id222 = Convert.ToInt32(id1);
            string name = Request.Form["ConTypeName"];
            if (id1.Count() > 4)
            {
                return Content("<script>alert('请输入小于四个数字的申请状态编号'); history.go(-1)</script>");
            }


            C_ContentType temp = (from v in Contentdb.C_ContentType where v.ConTypeID == id222 select v).FirstOrDefault();
            if (temp != null)
            {
                return Content("<script>alert('这个栏目类型已经存在'); history.go(-1)</script>");

            }
            else
            {
                /* C_ContentType TEMP1 = new C_ContentType { ApplyStatusID = 16, ApplyStatusName = name };
                 Contentdb.C_ContentType.Add(TEMP1);
                 Contentdb.SaveChanges();
*/
                Contentdb.Database.ExecuteSqlCommand("insert into C_ContentType (ConTypeID,ConTypeName) values(" + id222 + ",'" + name + "')");
                
            }
            return Redirect("/CodeManager/Code/C_ContentType1");//注意前面那个platform也加上不然会报错找不到视图



        }
        [BaseActionFilter]
        public ActionResult detailContentType(int id)//查看详情
        {
            //int id= Convert.ToInt32(id1);
            C_ContentType temp = (from c in Contentdb.C_ContentType where c.ConTypeID == id select c).FirstOrDefault();
            ViewBag.detail = temp;

            return View("~/Areas/CodeManager/Views/Code/C_ContentType/detailContentType.cshtml");

        }
        [BaseActionFilter]
        public ActionResult updateContentType(int id)//修改种类信息
        {
            //int id = Convert.ToInt32(id1);
            Session["ConTypeid"] = id;//我在session里还没设置过期时间
            C_ContentType temp = (from c in Contentdb.C_ContentType where c.ConTypeID == id select c).FirstOrDefault();
            ViewBag.update = temp;

            return View("~/Areas/CodeManager/Views/Code/C_ContentType/updateContentType.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReupdateContentType()//接收修改后的信息和数据库进行交互
        {
            string Enid = Request.Form["ConTypeID"];
            int enidint = Convert.ToInt32(Enid);
            string Enname = Request.Form["ConTypeName"];
            string id = Convert.ToString(Session["ConTypeid"]);
            if (id != Enid)
            {
                return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

            }
            else
            {
                C_ContentType TEMPP = (from c in Contentdb.C_ContentType where c.ConTypeID == enidint select c).FirstOrDefault();

                try
                {
                    TEMPP.ConTypeID = enidint;
                    TEMPP.ConTypeName = Enname;
                    Contentdb.SaveChanges();

                }
                catch (Exception)
                {

                    return Content("<script>alert('这个内容类型被别的表关联掉了不能修改'); history.go(-1)</script>");
                }
            }

            return Redirect("/CodeManager/Code/C_ContentType1");

        }


        #endregion 内容类型表管理


         #region 新闻类型表管理

        [BaseActionFilter]
        public ActionResult C_NewsType1()
        {
            return View("~/Areas/CodeManager/Views/Code/C_NewsType/C_NewsType1.cshtml");
        }
        [BaseActionFilter]
        public ActionResult GetTheDataNewsType()
        {
            //新闻列表分页和查询功能

            //接受DataGrid传来的参数

            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            string TheValue = Request["classValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数
            int TotalNum = 0;
            var service = new getNewsType();
            var theEnca = service.LoadNewsData(Temp, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
            var result = from b in theEnca select b;

            #region Category Search



            //依据搜索框传递的属性名和相应值得到搜索出来的模型
            // List<T_Class> GetSpe = CodeTable.T_Class.ToList();



            //var result = from a in theNews from b in GetState where a.FlowState==b.NewsStateID select new { a.NewsID, a.NewsTitle, a.NewsTypeID, a.NewsAuthor, a.NewsPublisher, a.PubDate, b.NewsStateName, a.IsShow, a.IsLocked, a.NewsColunmID, a.PicUrl, a.VideoUrl, a.Html, a.LinkUrl, a.Download };

            if (TheAttribute == "TypeID")
            {
                int ivalue = Convert.ToInt32(TheValue);
                result = from a in result where (String.IsNullOrEmpty(TheValue) || a.TypeID == ivalue) select a;
            }
            if (TheAttribute == "TypeName")
            {
                result = from a in result where (String.IsNullOrEmpty(TheValue) || a.TypeName.Contains(TheValue)) select a;
            }

            #endregion


            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串
            //ChangeNewsState c=new ChangeNewsState();
            //string Newstr=c.ChangeState(str);

            return Content(str);
        }
        [BaseActionFilter]

        public ActionResult DeleteNewsType()//接受表格传来的信息删除表格信息
        {
            //删除新闻数据

            string Deleteid = Request.Form[0];//获取需要删除的专业编号组成的字符串，且以逗号分割
            string[] SplitArray1 = Deleteid.Split(',');//取到要删除的新闻编号放进数组里

            int ArrayCount = SplitArray1.Count();

            string StringCount = Convert.ToString(ArrayCount);

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                string classid = SplitArray1[a];//不能直接把SplitArray1[a]放入linq语句会报错
                int intid = Convert.ToInt32(classid);

                try
                {
                    C_NewsType tempspe = (from v in Contentdb.C_NewsType where v.TypeID == intid select v).FirstOrDefault();
                    Contentdb.C_NewsType.Remove(tempspe);
                    Contentdb.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这条新闻类型信息在别的表被引用了不能删除');history.go(-1)</script>");
                }

            }

            return Content(StringCount);
        }


        [BaseActionFilter]
        public ActionResult AddNewsType()//添加新的种类信息
        {
            return View("~/Areas/CodeManager/Views/Code/C_NewsType/AddNewsType.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReAddNewsType()//接受添加种类信息的表单和数据库交互
        {
            string id1 = Request.Form["TypeID"];
            int id222 = Convert.ToInt32(id1);
            string name = Request.Form["TypeName"];
            if (id1.Count() > 4)
            {
                return Content("<script>alert('请输入小于四个数字的申请状态编号'); history.go(-1)</script>");
            }


            C_NewsType temp = (from v in Contentdb.C_NewsType where v.TypeID == id222 select v).FirstOrDefault();
            if (temp != null)
            {
                return Content("<script>alert('这个栏目类型已经存在'); history.go(-1)</script>");

            }
            else
            {

                Contentdb.Database.ExecuteSqlCommand("insert into C_NewsType (TypeID,TypeName) values(" + id222 + ",'" + name + "')");
               
            }
            return Redirect("/CodeManager/Code/C_NewsType1");//注意前面那个platform也加上不然会报错找不到视图



        }
        [BaseActionFilter]
        public ActionResult detailNewsType(int id)//查看详情
        {
            //int id= Convert.ToInt32(id1);
            C_NewsType temp = (from c in Contentdb.C_NewsType where c.TypeID == id select c).FirstOrDefault();
            ViewBag.detail = temp;

            return View("~/Areas/CodeManager/Views/Code/C_NewsType/detailNewsType.cshtml");

        }
        [BaseActionFilter]
        public ActionResult updateNewsType(int id)//修改种类信息
        {
            //int id = Convert.ToInt32(id1);
            Session["NewsTypeid"] = id;//我在session里还没设置过期时间
            C_NewsType temp = (from c in Contentdb.C_NewsType where c.TypeID == id select c).FirstOrDefault();
            ViewBag.update = temp;

            return View("~/Areas/CodeManager/Views/Code/C_NewsType/updateNewsType.cshtml");

        }
        [BaseActionFilter]
        public ActionResult ReupdateNewsType()//接收修改后的信息和数据库进行交互
        {
            string Enid = Request.Form["TypeID"];
            int enidint = Convert.ToInt32(Enid);
            string Enname = Request.Form["TypeName"];
            string id = Convert.ToString(Session["NewsTypeid"]);
            if (id != Enid)
            {
                return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

            }
            else
            {
                C_NewsType TEMPP = (from c in Contentdb.C_NewsType where c.TypeID == enidint select c).FirstOrDefault();

                try
                {
                    TEMPP.TypeID = enidint;
                    TEMPP.TypeName = Enname;
                    Contentdb.SaveChanges();

                }
                catch (Exception)
                {

                    return Content("<script>alert('这个内容类型被别的表关联掉了不能修改'); history.go(-1)</script>");
                }
            }

            return Redirect("/CodeManager/Code/C_NewsType1");

        }
         #endregion 新闻类型表管理



         #region 下载文件类型表管理
          [BaseActionFilter]
        public ActionResult C_DownLoadFileColumn1()
        {
            return View("~/Areas/CodeManager/Views/Code/C_DownLoadFileColumn/C_DownLoadFileColumn1.cshtml");
        }
         [BaseActionFilter]
          public ActionResult GetTheDataDownLoadFileColumn()
        {
           

            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);

            //获取后台查询值
            string TheAttribute = Request["classAttribute"];
            string TheValue = Request["classValue"];

            //构建DataGrid分页数据方法所需要的参数
            var Temp = new GetDataGridInfo()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            //分页数据输出的参数
            int TotalNum = 0;
            var service = new getDownLoadFileColumn();
            var thePosition = service.LoadNewsData(Temp, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
            var result = from b in thePosition select b;

            #region Category Search





            if (TheAttribute == "DownLoadFileColumnID")
            {
                int temp = Convert.ToInt32(TheValue);
                result = from a in result where (String.IsNullOrEmpty(TheValue) || a.DownLoadFileColumnID==temp) select a;
            }
            if (TheAttribute == "DownLoadFileColumnName")
            {
                result = from a in result where (String.IsNullOrEmpty(TheValue) || a.DownLoadFileColumnName.Contains(TheValue)) select a;
            }
            if (TheAttribute == "SybSystem")
            {
                result = from a in result where (String.IsNullOrEmpty(TheValue) || a.SybSystem.Contains(TheValue)) select a;
            }

            #endregion


            //total和rows是前台DataGrid所需要的
            var JsonResult = new { total = TotalNum, rows = result };
            string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串
            //ChangeNewsState c=new ChangeNewsState();
            //string Newstr=c.ChangeState(str);

            return Content(str);
        }

         [BaseActionFilter]
         public ActionResult DeleteDownLoadFileColumn()//接受表格传来的信息删除表格信息
        {
           

            string Deleteid = Request.Form[0];
            string[] SplitArray1 = Deleteid.Split(',');

            int ArrayCount = SplitArray1.Count();

            string StringCount = Convert.ToString(ArrayCount);

            for (int a = 0; a < ArrayCount; a++)
            {
                //依次删除相应数据
                int classid = Convert.ToInt32(SplitArray1[a]);

                try
                {
                    C_DownLoadFileColumn tempspe = (from v in CodeTable.C_DownLoadFileColumn where v.DownLoadFileColumnID == classid select v).FirstOrDefault();
                    CodeTable.C_DownLoadFileColumn.Remove(tempspe);
                    CodeTable.SaveChanges();
                }
                catch (Exception)
                {

                    return Content("<script>alert('这条下载文件类型在别的表被引用了不能删除');history.go(-1)</script>");
                }

            }

            return Content(StringCount);
        }


         [BaseActionFilter]
         public ActionResult AddDownLoadFileColumn()//添加新的种类信息
        {
            string[] system = { SubSystem.SCHOOL, SubSystem.PLATFORM, SubSystem.ENTERPRISE };
            ViewBag.system = system;
            return View("~/Areas/CodeManager/Views/Code/C_DownLoadFileColumn/AddDownLoadFileColumn.cshtml");
            
        }
         [BaseActionFilter]
         public ActionResult ReAddDownLoadFileColumn()//接受添加种类信息的表单和数据库交互
        {
            int DownLoadFileColumnID = Convert.ToInt32( Request.Form["DownLoadFileColumnID"]);
            string DownLoadFileColumnName = Request.Form["DownLoadFileColumnName"];
            string SybSystem = Request.Form["SybSystem"];
            C_DownLoadFileColumn temp = (from v in CodeTable.C_DownLoadFileColumn where v.DownLoadFileColumnID == DownLoadFileColumnID select v).FirstOrDefault();
            if (temp != null)
            {
                return Content("<script>alert('这个种类已经存在'); history.go(-1)</script>");

            }
            else
            {
               
                CodeTable.Database.ExecuteSqlCommand("insert into C_DownLoadFileColumn (DownLoadFileColumnID,DownLoadFileColumnName,SybSystem) values(" + DownLoadFileColumnID + ",'" + DownLoadFileColumnName + "','" + SybSystem + "')");

            }
            return Redirect("/CodeManager/Code/C_DownLoadFileColumn1");//注意前面那个platform也加上不然会报错找不到视图


        }
         [BaseActionFilter]
         public ActionResult detailDownLoadFileColumn(int id)//查看详情
        {
            C_DownLoadFileColumn temp = (from c in CodeTable.C_DownLoadFileColumn where c.DownLoadFileColumnID == id select c).FirstOrDefault();
            ViewBag.detail = temp;

            return View("~/Areas/CodeManager/Views/Code/C_DownLoadFileColumn/detailDownLoadFileColumn.cshtml");

        }
         [BaseActionFilter]

         public ActionResult updateDownLoadFileColumn(int id)//修改种类信息
        {
            Session["DownLoadFileColumnid"] = id;//我在session里还没设置过期时间
            C_DownLoadFileColumn temp = (from c in CodeTable.C_DownLoadFileColumn where c.DownLoadFileColumnID == id select c).FirstOrDefault();
            ViewBag.update = temp;
            string[] systemStr = { SubSystem.SCHOOL, SubSystem.PLATFORM, SubSystem.ENTERPRISE };
             List<string> system= new List<string>(systemStr);

             ViewBag.system = systemStr;
            return View("~/Areas/CodeManager/Views/Code/C_DownLoadFileColumn/updateDownLoadFileColumn.cshtml");

        }
         [BaseActionFilter]
         public ActionResult ReupdateDownLoadFileColumn()//接收修改后的信息和数据库进行交互
        {
            int DownLoadFileColumnID = Convert.ToInt32(Request.Form["DownLoadFileColumnID"]);
            string DownLoadFileColumnName = Request.Form["DownLoadFileColumnName"];
            string SybSystem = Request.Form["SybSystem"];
            int id = Convert.ToInt32(Session["DownLoadFileColumnid"]);
            if (id != DownLoadFileColumnID)
            {
                return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

            }
            else
            {
                C_DownLoadFileColumn TEMPP = (from c in CodeTable.C_DownLoadFileColumn where c.DownLoadFileColumnID == id select c).FirstOrDefault();

                try
                {
                    TEMPP.DownLoadFileColumnID = DownLoadFileColumnID;
                    TEMPP.DownLoadFileColumnName = DownLoadFileColumnName;
                    TEMPP.SybSystem = SybSystem;
                    CodeTable.SaveChanges();

                }
                catch (Exception)
                {

                    return Content("<script>alert('这个种类被别的表关联掉了不能删除'); history.go(-1)</script>");
                }
            }

            return Redirect("/CodeManager/Code/C_DownLoadFileColumn1");

        }


       

        #endregion 下载文件类型表管理

        #region 广告栏目表管理

         [BaseActionFilter]
         public ActionResult C_ADColumn1()
         {
             return View("~/Areas/CodeManager/Views/Code/C_ADColumn/C_ADColumn1.cshtml");
         }
         [BaseActionFilter]
         public ActionResult GetTheDataADColumn()
         {


             int pageIndex = int.Parse(Request["page"]);
             int pageSize = int.Parse(Request["rows"]);

             //获取后台查询值
             string TheAttribute = Request["classAttribute"];
             string TheValue = Request["classValue"];

             //构建DataGrid分页数据方法所需要的参数
             var Temp = new GetDataGridInfo()
             {
                 PageIndex = pageIndex,
                 PageSize = pageSize
             };

             //分页数据输出的参数
             int TotalNum = 0;
             var service = new getADColumn();
             var thePosition = service.LoadNewsData(Temp, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
             var result = from b in thePosition select b;

             #region Category Search





             if (TheAttribute == "ADColumnID")
             {
                 int temp = Convert.ToInt32(TheValue);
                 result = from a in result where (String.IsNullOrEmpty(TheValue) || a.ADColumnID == temp) select a;
             }
             if (TheAttribute == "ADColumnName")
             {
                 result = from a in result where (String.IsNullOrEmpty(TheValue) || a.ADColumnName.Contains(TheValue)) select a;
             }
             if (TheAttribute == "SybSystem")
             {
                 result = from a in result where (String.IsNullOrEmpty(TheValue) || a.SybSystem.Contains(TheValue)) select a;
             }

             #endregion


             //total和rows是前台DataGrid所需要的
             var JsonResult = new { total = TotalNum, rows = result };
             string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串
             //ChangeNewsState c=new ChangeNewsState();
             //string Newstr=c.ChangeState(str);

             return Content(str);
         }

         [BaseActionFilter]
         public ActionResult DeleteADColumn()//接受表格传来的信息删除表格信息
         {


             string Deleteid = Request.Form[0];
             string[] SplitArray1 = Deleteid.Split(',');

             int ArrayCount = SplitArray1.Count();

             string StringCount = Convert.ToString(ArrayCount);

             for (int a = 0; a < ArrayCount; a++)
             {
                 //依次删除相应数据
                 int classid = Convert.ToInt32(SplitArray1[a]);

                 try
                 {
                     C_ADColumn tempspe = (from v in CodeTable.C_ADColumn where v.ADColumnID == classid select v).FirstOrDefault();
                     CodeTable.C_ADColumn.Remove(tempspe);
                     CodeTable.SaveChanges();
                 }
                 catch (Exception)
                 {

                     return Content("<script>alert('这条下载文件类型在别的表被引用了不能删除');history.go(-1)</script>");
                 }

             }

             return Content(StringCount);
         }


         [BaseActionFilter]
         public ActionResult AddADColumn()//添加新的种类信息
         {
             string[] system = { SubSystem.SCHOOL, SubSystem.PLATFORM, SubSystem.ENTERPRISE };
             ViewBag.system = system;
             return View("~/Areas/CodeManager/Views/Code/C_ADColumn/AddADColumn.cshtml");
             
             
         }
         [BaseActionFilter]
         public ActionResult ReAddADColumn()//接受添加种类信息的表单和数据库交互
         {
             int ADColumnID = Convert.ToInt32(Request.Form["ADColumnID"]);
             string ADColumnName = Request.Form["ADColumnName"];
             string SybSystem = Request.Form["SybSystem"];
             C_ADColumn temp = (from v in CodeTable.C_ADColumn where v.ADColumnID == ADColumnID select v).FirstOrDefault();
             if (temp != null)
             {
                 return Content("<script>alert('这个种类已经存在'); history.go(-1)</script>");

             }
             else
             {

                 CodeTable.Database.ExecuteSqlCommand("insert into C_ADColumn (ADColumnID,ADColumnName,SybSystem) values(" + ADColumnID + ",'" + ADColumnName + "','" + SybSystem + "')");

             }
             return Redirect("/CodeManager/Code/C_ADColumn1");//注意前面那个platform也加上不然会报错找不到视图


         }
         [BaseActionFilter]
         public ActionResult detailADColumn(int id)//查看详情
         {
             C_ADColumn temp = (from c in CodeTable.C_ADColumn where c.ADColumnID == id select c).FirstOrDefault();
             ViewBag.detail = temp;

             return View("~/Areas/CodeManager/Views/Code/C_ADColumn/detailADColumn.cshtml");

         }
         [BaseActionFilter]

         public ActionResult updateADColumn(int id)//修改种类信息
         {
             Session["ADColumnid"] = id;//我在session里还没设置过期时间
             C_ADColumn temp = (from c in CodeTable.C_ADColumn where c.ADColumnID == id select c).FirstOrDefault();
             ViewBag.update = temp;
             string[] systemStr = { SubSystem.SCHOOL, SubSystem.PLATFORM, SubSystem.ENTERPRISE };
             List<string> system = new List<string>(systemStr);

             ViewBag.system = systemStr;
             return View("~/Areas/CodeManager/Views/Code/C_ADColumn/updateADColumn.cshtml");

         }
         [BaseActionFilter]
         public ActionResult ReupdateADColumn()//接收修改后的信息和数据库进行交互
         {
             int ADColumnID = Convert.ToInt32(Request.Form["ADColumnID"]);
             string ADColumnName = Request.Form["ADColumnName"];
             string SybSystem = Request.Form["SybSystem"];
             int id = Convert.ToInt32(Session["ADColumnid"]);
             if (id != ADColumnID)
             {
                 return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

             }
             else
             {
                 C_ADColumn TEMPP = (from c in CodeTable.C_ADColumn where c.ADColumnID == id select c).FirstOrDefault();

                 try
                 {
                     TEMPP.ADColumnID = ADColumnID;
                     TEMPP.ADColumnName = ADColumnName;
                     TEMPP.SybSystem = SybSystem;
                     CodeTable.SaveChanges();

                 }
                 catch (Exception)
                 {

                     return Content("<script>alert('这个种类被别的表关联掉了不能删除'); history.go(-1)</script>");
                 }
             }

             return Redirect("/CodeManager/Code/C_ADColumn1");

         }
        #endregion 广告栏目表管理

        #region 印章类型表管理

         [BaseActionFilter]
         public ActionResult C_StampType1()
         {
             return View("~/Areas/CodeManager/Views/Code/C_StampType/C_StampType1.cshtml");
         }
         [BaseActionFilter]
         public ActionResult GetTheDataStampType()
         {


             int pageIndex = int.Parse(Request["page"]);
             int pageSize = int.Parse(Request["rows"]);

             //获取后台查询值
             string TheAttribute = Request["classAttribute"];
             string TheValue = Request["classValue"];

             //构建DataGrid分页数据方法所需要的参数
             var Temp = new GetDataGridInfo()
             {
                 PageIndex = pageIndex,
                 PageSize = pageSize
             };

             //分页数据输出的参数
             int TotalNum = 0;
             var service = new getStampType();
             var thePosition = service.LoadNewsData(Temp, out TotalNum);//这里是分页比如你有好多页码想要在三页里面显示
             var result = from b in thePosition select b;

             #region Category Search





             if (TheAttribute == "TypeID")
             {
                 int temp = Convert.ToInt32(TheValue);
                 result = from a in result where (String.IsNullOrEmpty(TheValue) || a.TypeID == temp) select a;
             }
             if (TheAttribute == "TypeName")
             {
                 result = from a in result where (String.IsNullOrEmpty(TheValue) || a.TypeName.Contains(TheValue)) select a;
             }
             if (TheAttribute == "SybSystem")
             {
                 result = from a in result where (String.IsNullOrEmpty(TheValue) || a.SybSystem.Contains(TheValue)) select a;
             }

             #endregion


             //total和rows是前台DataGrid所需要的
             var JsonResult = new { total = TotalNum, rows = result };
             string str = JsonSerializeHelper.SerializeToJson(JsonResult);//把Json对象转换成string字符串
             //ChangeNewsState c=new ChangeNewsState();
             //string Newstr=c.ChangeState(str);

             return Content(str);
         }

         [BaseActionFilter]
         public ActionResult DeleteStampType()//接受表格传来的信息删除表格信息
         {


             string Deleteid = Request.Form[0];
             string[] SplitArray1 = Deleteid.Split(',');

             int ArrayCount = SplitArray1.Count();

             string StringCount = Convert.ToString(ArrayCount);

             for (int a = 0; a < ArrayCount; a++)
             {
                 //依次删除相应数据
                 int classid = Convert.ToInt32(SplitArray1[a]);

                 try
                 {
                     C_StampType tempspe = (from v in Contentdb.C_StampType where v.TypeID == classid select v).FirstOrDefault();
                     Contentdb.C_StampType.Remove(tempspe);
                     Contentdb.SaveChanges();
                 }
                 catch (Exception)
                 {

                     return Content("<script>alert('这条印章类型在别的表被引用了不能删除');history.go(-1)</script>");
                 }

             }

             return Content(StringCount);
         }


         [BaseActionFilter]
         public ActionResult AddStampType()//添加新的种类信息
         {
             string[] system = { SubSystem.SCHOOL, SubSystem.PLATFORM,SubSystem.ENTERPRISE };
             ViewBag.system = system;
             return View("~/Areas/CodeManager/Views/Code/C_StampType/AddStampType.cshtml");

         }
         [BaseActionFilter]
         public ActionResult ReAddStampType()//接受添加种类信息的表单和数据库交互
         {
             int StampTypeID = Convert.ToInt32(Request.Form["TypeID"]);
             string StampTypeName = Request.Form["TypeName"];
             string SybSystem = Request.Form["SybSystem"];
             C_StampType temp = (from v in Contentdb.C_StampType where v.TypeID == StampTypeID select v).FirstOrDefault();
             if (temp != null)
             {
                 return Content("<script>alert('这个种类已经存在'); history.go(-1)</script>");

             }
             else
             {

                 Contentdb.Database.ExecuteSqlCommand("insert into C_StampType (TypeID,TypeName,SybSystem) values(" + StampTypeID + ",'" + StampTypeName + "','" + SybSystem + "')");

             }
             return Redirect("/CodeManager/Code/C_StampType1");//注意前面那个platform也加上不然会报错找不到视图


         }
         [BaseActionFilter]
         public ActionResult detailStampType(int id)//查看详情
         {
             C_StampType temp = (from c in Contentdb.C_StampType where c.TypeID == id select c).FirstOrDefault();
             ViewBag.detail = temp;

             return View("~/Areas/CodeManager/Views/Code/C_StampType/detailStampType.cshtml");

         }
         [BaseActionFilter]

         public ActionResult updateStampType(int id)//修改种类信息
         {
             Session["StampTypeid"] = id;//我在session里还没设置过期时间
             C_StampType temp = (from c in Contentdb.C_StampType where c.TypeID == id select c).FirstOrDefault();
             ViewBag.update = temp;
             string[] systemStr = { SubSystem.SCHOOL, SubSystem.PLATFORM, SubSystem.ENTERPRISE };
             List<string> system = new List<string>(systemStr);

             ViewBag.system = systemStr;
             return View("~/Areas/CodeManager/Views/Code/C_StampType/updateStampType.cshtml");

         }
         [BaseActionFilter]
         public ActionResult ReupdateStampType()//接收修改后的信息和数据库进行交互
         {
             int StampTypeID = Convert.ToInt32(Request.Form["TypeID"]);
             string StampTypeName = Request.Form["TypeName"];
             string SybSystem = Request.Form["SybSystem"];
             int id = Convert.ToInt32(Session["StampTypeid"]);
             if (id != StampTypeID)
             {
                 return Content("<script>alert('这个是主键不能改'); history.go(-1)</script>");

             }
             else
             {
                 C_StampType TEMPP = (from c in Contentdb.C_StampType where c.TypeID == id select c).FirstOrDefault();

                 try
                 {
                     TEMPP.TypeID = StampTypeID;
                     TEMPP.TypeName = StampTypeName;
                     TEMPP.SybSystem = SybSystem;
                     Contentdb.SaveChanges();

                 }
                 catch (Exception)
                 {

                     return Content("<script>alert('这个种类被别的表关联掉了不能删除'); history.go(-1)</script>");
                 }
             }

             return Redirect("/CodeManager/Code/C_StampType1");

         }


        #endregion 印章类型表管理
    }
}
