using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Models;
using System.Text;
using System.Web.Http;
using ServicePlatform.Lib;

namespace ServicePlatform.Areas.ServiceApi.Controllers
{
    public class HomeController : Controller
    {
        private UserContext uc = new UserContext();
        private SchoolContext SC = new SchoolContext();
        private EnterpriseContext ec = new EnterpriseContext();
        private CodeTableContext ctc = new CodeTableContext();

        SchoolHelper SchoolHelper = new SchoolHelper();

        List<T_PhoneMsg> PhoneMessageList = new List<T_PhoneMsg>();

        //登录验证 /ServiceApi/Home/Login
        public JsonResult Login(string schoolid, string username, string password)
        {
            string SchoolID = schoolid;
            string StuNo = username;
            string UserPwd = Lib.EncryptString.NoneEncrypt(password);//获得加密后的用户密码

            JsonResult js = new JsonResult();
            js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            //js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            var tempStu = (from Stu in SC.Student
                           from Cla in SC.T_Class
                           where Stu.StuClass == Cla.ClassID && Stu.StuNo == StuNo && Cla.SchoolID == SchoolID
                           select Stu.UserID).ToList();

            if (tempStu != null && tempStu.Count > 0)
            {
                string UserID;
                UserID = tempStu[0];
                var tempUserInfo = (
                    from User in uc.T_User
                    where User.UserID == UserID && User.UserPwd == UserPwd
                    select User
                    ).ToList();
                if (tempUserInfo != null && tempUserInfo.Count > 0)
                {
                    js.Data = "LOGINOK";
                    return js;
                }

            }
            js.Data = "LOGINERROR";
            return js;
            
        }//ending of Login function

        //获取学校信息
        public JsonResult SchoolList()
        {
            var SchoolList = (from SL in SC.School
                              where SL.SchoolID != "" || SL.SchoolLogo != "" || SL.SchoolName != ""
                              select new { schoolid = SL.SchoolID, schoolImage = SL.SchoolLogo, schoolName = SL.SchoolName }
    ).ToList();
            JsonResult js = new JsonResult();
            js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            js.Data = SchoolList;
            return js;
        }

        //获取企业信息
        public JsonResult EnterpriseList(string schoolid)
        {
            JsonResult js = new JsonResult();
            js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            string batchid = SchoolHelper.GetCurrentBatch(schoolid);
            if (batchid == "该学校未开放任何批次")
            {
                js.Data = -1;//该学校未开放任何批次
            }
            var q_ent = (from f in ec.T_EntBatchReg where f.PracBatchID == batchid select f.EntPracNo).ToList();
            if (q_ent.Count == 0)
            {
                js.Data = -2;//没有企业未注册到该学校的当前批次；
            }
            else
            {
                List<string> EntPracNoList = q_ent;
                var q_num = (from f in ec.T_PracticePosition
                             from k in ec.T_EntBatchReg
                             where f.EntPracNo == k.EntPracNo
                             && EntPracNoList.Contains(f.EntPracNo)
                             group f by k.Ent_No into g
                             select new { enterpriseId = g.Key, offerNum = g.Sum(f => f.PosQuantity) }).ToList();

                var q_final = (from k in q_num
                               from f in ec.T_Enterprise
                               where k.enterpriseId == f.Ent_No
                               && f.Ent_No != null && f.EntLogo != null && f.Ent_Name != null && f.EntAddress != null && f.EntPhotos != null && f.EntResume != null && f.Email != null
                               select new { enterpriseId = f.Ent_No, imageResourceId = f.EntLogo, enterpriseName = f.Ent_Name, enterpriseAddress = f.EntAddress, enterprisePhone = f.Contectinfo, enterpriseEmail = f.Email, enterpriseIntro = f.EntResume, offerNum = k.offerNum }).ToList();
                js.Data = q_final;
            }
            return js;

        }

        //企业职位列表
        public JsonResult EntPositionList(string schoolid,string entid)
        {
            JsonResult js = new JsonResult();
            js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            string batchid = SchoolHelper.GetCurrentBatch(schoolid);
            if (batchid == "该学校未开放任何批次")
            {
                js.Data = -1;//该学校未开放任何批次
            }
            var q_ent = (from f in ec.T_EntBatchReg where f.PracBatchID == batchid select f.EntPracNo).ToList();
            if (q_ent.Count == 0)
            {
                js.Data = -2;//该企业未注册到该学校的当前批次；
            }
            else
            {
                string EntPracNo = q_ent[0];
                var q_position = (from f in ec.T_PracticePosition where f.EntPracNo == EntPracNo select f).ToList();
                var q_position1 = (from f in q_position select new 
                { 
                    entPracNo=f.EntPracNo+"+"+f.PositionID,
                    postName = f.PositionName, 
                    offerNum = f.PosQuantity,
                    remainNum = f.PosQuantity - SC.PracticeVolunteer.Where(a=>a.VolunteerStatus==5&&a.PosID==f.PositionID&&a.PreVolStatus=="1"&&a.EntPracNo==f.EntPracNo).ToList().Count,
                    posDesc=f.PosDesc, 
                    deadLine = f.PosExpireDateStr 
                }).ToList();
                js.Data = q_position1;
            }
            return js;
        }

        //返回企业图片列表
        public JsonResult EntPicList(string entid)
        {
            JsonResult js = new JsonResult();
            js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var q = (from f in ec.T_Enterprise where f.Ent_No == entid select f.EntAdPics).ToList();
            if(q.Count==0)
            {
                js.Data = -1;
            }
            else
            {
                js.Data = q;
            }
            return js;
        }

        //

        //学生上传头像
        public JsonResult ImportStuHeadPic(string userid)
        {

            JsonResult js = new JsonResult();
            js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            
            
            var inputStream = Request.InputStream;
            //HttpPostedFile Photo = new HttpPostedFile();
            var strLen = Convert.ToInt32(inputStream.Length);
            var strArr = new byte[strLen];
            inputStream.Read(strArr, 0, strLen);
            var requestMes = Encoding.UTF8.GetString(strArr);
            inputStream.Flush();
            inputStream.Close();
            inputStream.Dispose();


            js.Data += requestMes;
            //string userid = (Session["Vars"] as ServicePlatform.Config.Vars).UserID;
            //T_Student tu = SC.Student.Find(userid);
            //if (tu != null)
            //{
            //    string old = tu.MainPhoto;
            //    if (old != null)
            //    {
            //        Lib.FileOperate.DeleteFlie(old);
            //    }
            //}
            //if (Photo != null)//如果重新提交了照片
            //{
            //    string schoolid = (Session["Vars"] as ServicePlatform.Config.Vars).SchoolID;
            //    string url = "/UserFiles/School/" + schoolid + "/OtherFiles/StuHeadPic/";
            //    string result = Lib.FileOperate.UploadFile(Photo, url);
            //    tu.MainPhoto = result;
            //    js.Data = "OK";

            //}
            //else//不更新照片
            //{
            //    tu.MainPhoto = "/Areas/School/Content/S_StuBaseInformation/img/defaultpic.png";
            //    js.Data = "OK,Use Default";
            //}
            //try
            //{
            //    SC.SaveChanges();
            //    js.Data = "OK";
            //}
            //catch
            //{
            //    js.Data = "SaveFaild!Use Default";
            //}       
            //js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;         
            Console.WriteLine(js.ToString());
            js.Data += "aaaaaaaaaaa";
            return js;
            
            //return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        //获取待发送的信息列表
        public JsonResult GetMesaageList()
        {
            var q = (from f in SC.T_PhoneMsg where f.Flag == "0" select f).ToList();

            var q_temp=(from f in q select new { ID=f.ID, content = f.MsgContent, sendto = f.Phone }).ToList();
            for (int i = 0; i < q.Count; i++)
            {
                PhoneMessageList.Add(q[i]);
            }
            MessageSendedSuccess();//默认为所有信息发送成功

            JsonResult js = new JsonResult();
            js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            js.Data = q_temp;
            return js;     
        }

        //信息发送成功
        public void MessageSendedSuccess()
        {
            for (int i = 0; i < PhoneMessageList.Count; i++)
            {
                T_PhoneMsg temp = SC.T_PhoneMsg.Find(PhoneMessageList[i].ID);
                if (temp != null)
                {
                    temp.Flag = "1";
                }
                SC.SaveChanges();
            }
            PhoneMessageList.Clear();
            return;
        }


        //请求某条信息发送失败
        public void MessageSendedFailed(string ID)
        {
            T_PhoneMsg temp = SC.T_PhoneMsg.Find(ID);
            if (temp != null)
            {
                temp.Flag = "0";
            }
            SC.SaveChanges();
            return;
        }

        //修改学生个人信息
        public int ModifyStuInfo(string userid,string Stusex,string Cellphone,string Email,string QQ,string Birthday)
        {
            T_Student ts=SC.Student.Find(userid);
            if(ts==null)
            {
                return 0;//查无此人
            }
            else
            {
                ts.StuSex = Stusex;
                ts.StuCellphone = Cellphone;
                ts.StuEMail = Email;
                ts.StuQQ = QQ;
                ts.StuBirthday = Birthday;
                try
                {
                    SC.SaveChanges();
                }
                catch
                {
                    return -1;//修改失败，数据库错误
                }
            }
            return 1;//修改成功
        }

        //修改用户密码
        public int ModifyUserPwd(string userid,string oldpwd,string newpwd)
        {
            T_User tu = uc.T_User.Find(userid);
            if(tu==null)
            {
                return 0;//查无此人
            }
            if(Permission.Controllers.HomeController.UserPswChg(userid, oldpwd, newpwd)==true)
            {
                return 1;//修改成功
            }
            else
            {
                return -1;//修改失败，数据库错误
            }
        }


        //提交学生个人志愿
        public int IWantPracEnroll(string pracno, string entpracno,string userid, string posid,int posDegree,int WillDegree)
        {
            if (pracno != null)
            {
                T_PracticeVolunteer tp = new T_PracticeVolunteer();
                tp.PracticeNo = userid + pracno;
                tp.EntPracNo = entpracno;
                tp.PosID = posid;
                T_PracticeVolunteer tp_check = SC.PracticeVolunteer.Find(userid + pracno, entpracno, posid);
                if (tp_check != null)
                {
                    return -1;//该志愿/顺序下的岗位已存在
                }
                tp.VolunteerSequence = WillDegree;
                tp.PosSequence = posDegree;
                //查找当前批次是否存在已经正式报名的某志愿某顺序
                var q = (from f in SC.PracticeVolunteer where f.PracticeNo == pracno && f.VolunteerSequence == WillDegree && f.PosSequence == posDegree && f.PreVolStatus == "1" && f.PosID != posid select f).ToList();
                if (q == null || q.Count == 0)
                {
                    var check = (from f in SC.PracticeVolunteer where f.PracticeNo == pracno && f.EntPracNo == entpracno && f.VolunteerSequence != WillDegree && f.PreVolStatus == "1" select f).ToList();
                    if (check.Count == 0)
                    {
                        tp.VolunteerStatus = 1;
                        tp.PreVolStatus = "1";
                        SC.PracticeVolunteer.Add(tp);
                        return 1;//志愿提交成功提交
                    }
                    else
                    {
                        return -2;//正式报名同一公司的岗位须保持该公司的所有岗位在同一志愿顺序下！
                    }
                }
                else
                {
                    return -3;//该志愿-顺序下存在已经正式报名的岗位！
                }
                
            }
            else
            {
                return -4;//数据接收异常
            }
        }

        //上传学生周记
        public int StuWeekRec(string practiceno, string itemTitle, string itemContent, string itemSummary)
        {
            if (practiceno == "" || itemTitle == "" || itemContent == "" || itemSummary=="")
            {
                return 0;//资料不完整，请检查
            }
            try
            {
                T_WeekRecord tw = new T_WeekRecord();
                int no = DateTimeFormat.ConvertDateTimeInt(DateTime.Now);
                tw.PracticeNo = practiceno;
                tw.RecordDate = no;
                tw.RecordContent = itemContent;
                tw.RecordTitle = itemTitle;
                tw.RecordComment = itemSummary;
                tw.RecordNo = no;

                SC.WeekRecord.Add(tw);
                SC.SaveChanges();
                return 1;//周记提交成功
            }
            catch
            {
                return -1;//周记提交失败，数据库异常
            }
        }

        //上传获取案例填写列表
        //public JsonResult GetCaseList(string schoolid)
        //{
        //    //获取学校当前批次
        //    string prcbatchid = SchoolHelper.GetCurrentBatch(schoolid);
        //    ViewBag.CaseNo = DateTimeFormat.ConvertDateTimeInt(DateTime.Now).ToString();
        //    var q_caseitem = (from f in ctc.C_PracticeCaseItem where f.PracBatchID == prcbatchid orderby f.ItemSequence select f).ToList();
        //    ViewBag.PracticeCaseItem = q_caseitem;
        //}
        //获取实习案例的填写列表


        //获取系统消息
        public JsonResult GetSystemMsg(string userid)
        {
            JsonResult js = new JsonResult();
            js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var q = (from f in SC.T_MsgRec
                     from g in SC.T_SysMsg
                     where f.MsgID==g.MsgID
                     && f.Receiver == userid
                     select new{
                         MsgID=f.MsgID,
                         MsgContent=g.MsgContent,
                         Sender=g.MsgOwner
                     }).ToList();
            if(q.Count==0)
            {
                js.Data = -1;//没有您的消息
            }
            else
            {
                js.Data = q;
            }
            return js;
        }

    }//ending of HomeController
}//ending of namespace
