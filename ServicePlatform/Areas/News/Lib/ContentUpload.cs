using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.News.Lib
{
    public static class ContentUpload
    {
        //用于文件上传操作
        public static bool Upload(HttpPostedFileBase UploadFileBase, string BaseDirectory,string FileName)
        {
            //BaseDirectory是站内文件存储文件夹
            string FilePath = "";
            if (UploadFileBase != null && UploadFileBase.ContentLength > 0)
            {
                //获取站内地址，上传文件的相对路径是由BaseDirectory和文件名组合而成，BaseDirectory需要在ConstructPath方法中构造
                //FilePath = BaseDirectory + UploadFileBase.FileName;
                FilePath = BaseDirectory+FileName;//附加上自定义文件名
                //保存文件
                UploadFileBase.SaveAs(FilePath);
                //UploadFileBase.SaveAs(HttpContext.Server.MapPath(FilePath));
            }
            return true;
        }

        public static string ConstructPath(string Subsystem,string Unit,int ContentType)
        {
            //对BaseDirectory路径进行构造,Subsystem代表平台，Unit代表单位,ContentType代表内容种类
            string BaseDirectory = "/UserFiles/";
            if (!String.IsNullOrEmpty(Subsystem) && !String.IsNullOrEmpty(Unit))
            {
                //子系统和单位不为空
                switch (ContentType)
                {
                    case 11:
                        //内容种类为新闻类型
                        BaseDirectory = BaseDirectory + Subsystem + "/" + Unit +"/Contents"+"/News/";
                        break;
                    case 21:
                        //内容种类为广告类型
                        BaseDirectory = BaseDirectory + Subsystem + "/" + Unit +"/Contents"+"/Ads/";
                        break;
                    case 31:
                        //内容种类为下载文件类型
                        BaseDirectory = BaseDirectory + Subsystem + "/" + Unit +"/Contents"+"/DownLoadFiles/";
                        break;
                    case 41:
                        //内容种类为印章类型
                        BaseDirectory = BaseDirectory + Subsystem + "/" + Unit + "/Contents" + "/Stamps/";
                        break;
                    case 51:
                        //内容种类为签名类型
                        BaseDirectory = BaseDirectory + Subsystem + "/" + Unit + "/Contents" + "/Signatures/";
                        break;
                    default:
                        break;
                }
            }
            if (BaseDirectory != "/UserFiles/")
            {
                return BaseDirectory;
            }
            else {
                return null;
            }
        }

        public static string ConstructFileName(HttpPostedFileBase UploadFileBase, string ContentID, string Unit)
        { 
            //用于构建文件名
            string FileName = "";

            string Suffix = UploadFileBase.ContentType;//通过后缀名来判断上传文件的类型
            string[] Segments = Suffix.Split('/');
            string Type = Segments[0];//判断文件类型是图片还是视频
            string Extension = Segments[1];//文件的后缀名
            if (Type == "image")
            {
                FileName = "PIC_" + ContentID + "_1";
            }
            else if (Type == "video")
            {
                FileName = "VIDEO_" + ContentID + "_1";
            }
            FileName = FileName + "." + Extension;//与文件后缀名合并
            if (FileName != "")
            {
                return FileName;
            }
            else {
                return null;
            }
        }

        //public static string GetFileNum(string PathParam)
        //{
        //    int fileNum = 0;
        //    try
        //    {
        //        string[] fileList = System.IO.Directory.GetFileSystemEntries(PathParam);
        //        foreach (string file in fileList)
        //        {
        //            if (System.IO.Directory.Exists(file))
        //                GetFileNum(file);
        //            else
        //                fileNum++;
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        //异常处理
        //    }
        //    string num = Convert.ToString(fileNum);
        //    return num;
        //}
    }
}