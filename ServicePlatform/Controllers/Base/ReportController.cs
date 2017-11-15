using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.OleDb;
using System.IO;


namespace ServicePlatform.Controllers.Base
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult  OuputExcel(string TemplateFileName){
            string result = string.Empty;
            string strConn;
            string strTemplateFilePath = AppDomain.CurrentDomain.BaseDirectory + "UserFiles/ReportTemplate/" + TemplateFileName;
            //string DownLoadFileName = (Session["Vars"] as ServicePlatform.Config.Vars).UserID + "-" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + "-" + TemplateFileName;
            string DownLoadFileName =  DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + "-" + TemplateFileName;
            string strFilePath = AppDomain.CurrentDomain.BaseDirectory + "UserFiles/ReportDownLoad/" + DownLoadFileName;
            
            string strSQL;

            //File.Copy(strTemplateFilePath, strFilePath, "true");
            //File
            System.IO.File.Copy(strTemplateFilePath, strFilePath, true);
            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +  strFilePath + ";" + "Extended Properties=Excel 12.0";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            strSQL = "Insert into [Sheet1$] values('1','2','3')";
            OleDbCommand mySqlCommand = new OleDbCommand(strSQL, conn);
            try
            {
                mySqlCommand.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
            conn.Close();
            //path = Server.MapPath("~/frog.jpg.jpg");            
            return File(strFilePath, "application/zip-x-compressed", DownLoadFileName);

        }

    }
}
