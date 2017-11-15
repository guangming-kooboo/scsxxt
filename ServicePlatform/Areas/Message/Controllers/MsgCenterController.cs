using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Models;
using ServicePlatform.Lib;
using ServicePlatform.App_Start;
using ServicePlatform.Areas.Permission.Models;
using System.Text.RegularExpressions;
using Webdiyer.WebControls.Mvc;

namespace ServicePlatform.Areas.Message.Controllers
{
    public class MsgCenterController : Controller
    {
        private SchoolContext sc = new SchoolContext();
        private UserContext uc = new UserContext();
        private EnterpriseContext ec = new EnterpriseContext();
        private CodeTableContext ctc = new CodeTableContext();
        private PermissionContext pc = new PermissionContext();
        private SchoolHelper SchoolHelper = new SchoolHelper();
        //
        // GET: /Admin/MsgCenter/


        [BaseActionFilterAttribute]
        public ActionResult Index(string UserID)
        {
            var q = (from f in sc.T_SysMsg where f.MsgOwner == UserID select f).ToList();
            if (q.Count != 0)
            {
                return View(q);
            }
            else return Content("用户身份已过期，请重新登录");
        }

        //手动调整字符长度；
        string cutstring(char[] temp)
        {
            string result = string.Empty;
            if (temp.Length > 15)
            {
                for (int i = 0; i < 15; i++)
                {
                    result += temp[i];
                }
                result += "...";
                return result;
            }
            else
            {
                for (int j = 0; j < temp.Length; j++)
                {
                    result += temp[j];
                }
                return result;
            }
        }

        //更新新消息数量
        void updateReaded()
        { 
            //#region 处理消息数量
            //string uid = (Session["vars"] as ServicePlatform.Config.Vars).UserID;
            //var quertype = (from f in ac.User where f.UserID == uid select f.UserTypes).ToList();
            //var query = new List<T_MsgRec>();
            //var query1 = new List<T_MsgRec>();
            //if (quertype.Count == 0 || quertype[0] == null)
            //{
            //    query = (from f in mc.MsgRec where (f.MsgTypeID == 0 && f.Receiver == uid && f.ReadTime == -1) select f).ToList();
            //}
            //else
            //{
            //    query = (from f in mc.MsgRec where (f.Receiver == uid && f.ReadTime == -1) select f).ToList();
            //}
            //(Session["vars"] as ServicePlatform.Config.Vars).MyMsgCount = query.Count;
            //#endregion
        }

        //判断字符串是否为纯数字
        bool judge_all_num(string temp)
        {
            if (temp != null)
            {
                if (Regex.Matches(temp, "[\u4e00-\u9fa5]").Count != 0)
                {
                    return false;
                }
                else
                {
                    for (int i = 0; i < temp.Length; i++)
                    {
                        byte tempByte = Convert.ToByte(temp[i]);
                        if (tempByte < 48 || tempByte > 57)
                            return false;
                    }
                    return true;
                }
            }
            else
            {
                return false;
            }
        }


        //写消息
        [HttpGet]
        [BaseActionFilterAttribute]
        public ActionResult NewMsg()
        {
            //string userid = (Session["vars"] as ServicePlatform.Config.Vars).UserID;
            List<SelectListItem> RoleList = SchoolHelper.GetRoleList();
            ViewData["RoleList"] = RoleList;

            List<SelectListItem> UserListTemp = SchoolHelper.GetUserList();
            List<SelectListItem> UserList = new List<SelectListItem>();
            foreach (var item in UserListTemp)
            {
                UserList.Add(new SelectListItem() { Text = item.Text + ";" + item.Value, Value = item.Text + ";" + item.Value });
            }
            ViewData["UserList"] = UserList;


            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [BaseActionFilterAttribute]
        public ActionResult NewMsg(string action, T_SysMsg tm)
        {
            //string msgcontent = Request.Form["msgcontent"].ToString();
            T_SysMsg tm_temp = new T_SysMsg();

            tm_temp.MsgContent = tm.MsgContent;
            int createtime = DateTimeFormat.ConvertDateTimeInt(System.DateTime.Now);
            tm_temp.CreateTime = createtime;
            string msgid = createtime.ToString();
            tm_temp.MsgID = msgid;
            string sender=(Session["vars"] as ServicePlatform.Config.Vars).UserID;
            tm_temp.MsgOwner = sender;
            tm_temp.IsLocked=0;
            string type=Request.Form["flag"];
            string roleid = Request.Form["usertype"];
            string sendtouser = Request.Form["userid"];

            if (type == "true")
            {
                tm_temp.MsgTypeID = 11;//群发
            }
            else
            {
                tm_temp.MsgTypeID = 12;//个人
            }
            tm_temp.FatherMsgID = "0";
            if (action == "发送")
            {                   
                sc.T_SysMsg.Add(tm_temp);
                sc.SaveChanges();

                T_MsgSend temp = new T_MsgSend();
                temp.MsgID = msgid;
                temp.Receiver = roleid;
                temp.Sender = sender;
                temp.SendTime = createtime;
                temp.SendTypeID = 11;
                sc.T_MsgSend.Add(temp);
                sc.SaveChanges();

                if(type=="true")//发送角色信息
                {            
                    var q = (from f in pc.UserRole where f.RoleID == roleid select f.UserID).ToList();
                    try
                    {
                        for (int i = 0; i < q.Count; i++)
                        {
                            T_MsgRec tr = new T_MsgRec();
                            tr.MsgID = msgid;
                            tr.ReadTime = -1;
                            tr.Receiver = q[i];
                            sc.T_MsgRec.Add(tr);
                            sc.SaveChanges();
                        }
                        return Content("<script>alert('消息发送成功！');window.location.href = document.referrer;</script>");//返回上一页并刷新
                    }
                    catch
                    {
                        T_SysMsg ts_rollback = sc.T_SysMsg.Find(msgid);
                        if (ts_rollback!=null)
                        {
                            sc.T_SysMsg.Remove(ts_rollback);
                            sc.SaveChanges();
                        }
                        return Content("<script>alert('消息发送失败！');window.location.href = document.referrer;</script>");//返回上一页并刷新
                    }                          
                }
                else//发送个人信息
                {
                    try
                    {
                        sendtouser = sendtouser.Trim();
                        string[] getuserid = sendtouser.Split(';');
                        T_MsgRec tr = new T_MsgRec();
                        tr.MsgID = msgid;
                        tr.ReadTime = -1;
                        tr.Receiver = getuserid[1];
                        sc.T_MsgRec.Add(tr);
                        sc.SaveChanges();
                        return Content("<script>alert('消息发送成功！');window.location.href = document.referrer;</script>");//返回上一页并刷新
                    }
                    catch
                    {
                        T_SysMsg ts_rollback = sc.T_SysMsg.Find(msgid);
                        if (ts_rollback != null)
                        {
                            sc.T_SysMsg.Remove(ts_rollback);
                            sc.SaveChanges();
                        }
                        return Content("<script>alert('消息发送失败！');window.location.href = document.referrer;</script>");//返回上一页并刷新
                    }

                }
            }
            else
            {
                try
                {
                    sc.T_SysMsg.Add(tm_temp);
                    sc.SaveChanges();
                    T_MsgSend temp = new T_MsgSend();
                    temp.MsgID = msgid;
                    temp.Receiver = roleid;
                    temp.Sender = sender;
                    temp.SendTime = -1;
                    temp.SendTypeID = 12;
                    sc.T_MsgSend.Add(temp);
                    sc.SaveChanges();
                    return Content("<script>alert('消息保存成功！');window.location.href = document.referrer;</script>");//返回上一页并刷新
                }
                catch
                {
                    T_SysMsg ts_rollback = sc.T_SysMsg.Find(msgid);
                    if (ts_rollback != null)
                    {
                        sc.T_SysMsg.Remove(ts_rollback);
                        sc.SaveChanges();
                    }
                    return Content("<script>alert('消息发送失败！');window.location.href = document.referrer;</script>");//返回上一页并刷新
                }         
            }
           
        }


        [HttpPost]
        [ValidateInput(false)]
        [BaseActionFilterAttribute]
        public string SendMsgInterface(int type, string sender, string content, string to)
        {
            T_SysMsg tm_temp = new T_SysMsg();
            tm_temp.MsgContent = content;
            int createtime = DateTimeFormat.ConvertDateTimeInt(System.DateTime.Now);
            tm_temp.CreateTime = createtime;
            string msgid = createtime.ToString();
            if (type == 0)//发送给群组
            {             
                tm_temp.MsgID = msgid;
                tm_temp.MsgOwner = sender;
                tm_temp.IsLocked = 0;
                string roleid = to;
                string sendtouser = Request.Form["userid"];
                tm_temp.MsgTypeID = 11;//群发
                tm_temp.FatherMsgID = "0";
                sc.T_SysMsg.Add(tm_temp);
                sc.SaveChanges();
                T_MsgSend temp = new T_MsgSend();
                temp.MsgID = msgid;
                temp.Receiver = roleid;
                temp.Sender = sender;
                temp.SendTime = createtime;
                temp.SendTypeID = 11;
                sc.T_MsgSend.Add(temp);
                sc.SaveChanges();
                var q = (from f in pc.UserRole where f.RoleID == roleid select f.UserID).ToList();
                try
                {
                    for (int i = 0; i < q.Count; i++)
                    {
                        T_MsgRec tr = new T_MsgRec();
                        tr.MsgID = msgid;
                        tr.ReadTime = -1;
                        tr.Receiver = q[i];
                        sc.T_MsgRec.Add(tr);
                        sc.SaveChanges();
                    }
                    return msgid;
                }
                catch
                {
                    T_SysMsg ts_rollback = sc.T_SysMsg.Find(msgid);
                    if (ts_rollback != null)
                    {
                        sc.T_SysMsg.Remove(ts_rollback);
                        sc.SaveChanges();
                    }
                    return "";
                }
            }
            else
            {
                try
                {
                    sc.T_SysMsg.Add(tm_temp);
                    sc.SaveChanges();
                    T_MsgSend temp = new T_MsgSend();
                    temp.MsgID = msgid;
                    temp.Receiver = to;
                    temp.Sender = sender;
                    temp.SendTime = -1;
                    temp.SendTypeID = 12;
                    sc.T_MsgSend.Add(temp);
                    sc.SaveChanges();
                    return msgid;
                }
                catch
                {
                    T_SysMsg ts_rollback = sc.T_SysMsg.Find(msgid);
                    if (ts_rollback != null)
                    {
                        sc.T_SysMsg.Remove(ts_rollback);
                        sc.SaveChanges();
                    }
                    return "";
                }         
            }
        }

        //消息回复
        [HttpPost]
        [ValidateInput(false)]
        [BaseActionFilterAttribute]
        public ActionResult RecMsg_Replay(T_SysMsg tm)
        {
            string userid=(Session["vars"] as ServicePlatform.Config.Vars).UserID;
            string msgcontent = Request.Form["msgcontent"].ToString();
            T_SysMsg tm_temp = new T_SysMsg();     
            tm_temp.MsgContent = msgcontent;
            tm_temp.MsgTypeID = 12;
            tm_temp.FatherMsgID = tm.MsgID;
            tm_temp.MsgOwner = userid;
            tm_temp.IsLocked = 0;
            int datetime = DateTimeFormat.ConvertDateTimeInt(System.DateTime.Now);
            string msgid=datetime.ToString();
            string recevier=tm.MsgOwner;
            tm_temp.CreateTime = datetime;
            tm_temp.MsgID=msgid;
            sc.T_SysMsg.Add(tm_temp);
            sc.SaveChanges();
            T_MsgRec tmr = new T_MsgRec();
            tmr.MsgID = msgid;
            tmr.ReadTime = -1;
            tmr.Receiver = recevier;
            sc.T_MsgRec.Add(tmr);

            try
            {
                sc.SaveChanges();
                return Content("<script>alert('消息回复成功！');window.location.href = document.referrer;</script>");//返回上一页并刷新
            }
            catch
            {
                T_MsgRec ts = sc.T_MsgRec.Find(msgid, recevier);
                T_SysMsg tss = sc.T_SysMsg.Find(msgid);
                if(ts!=null)
                {
                    sc.T_MsgRec.Remove(ts);
                }
                if(tss!=null)
                {
                    sc.T_SysMsg.Remove(tss);
                }
                sc.SaveChanges();
            }
            return Content("<script>alert('消息回复失败！');window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        //回复消息
        [BaseActionFilterAttribute]
        public ActionResult RecMsg_Replay(string id)
        {
            T_SysMsg tm = sc.T_SysMsg.Find(id);
            //string msgtid = tm.MsgTypeID;
            //var q = (from f in mc.MsgType where f.MsgTypeID == msgtid select f.MsgTypeName).ToList();
            //@ViewBag.msgt = q[0].ToString();
            //return Content("<script>window.history.go(-2);</script>");//返回上一页并刷新
            return View("MsgReplay", tm);
        }

        //详情页的回复
        [BaseActionFilterAttribute]
        public ActionResult Msg_Detail_Reply(T_SysMsg tm)
        {
            string msgid = tm.MsgID;
            T_SysMsg tmm = sc.T_SysMsg.Find(msgid);
            //int msgtid = tmm.MsgTypeID;
            //var q = (from f in sc.T_SysMsg where f.MsgTypeID == msgtid select f).ToList();
           // @ViewBag.msgt = q[0].ToString();
            //return Content("<script>alert('消息回复成功！');window.location.href = document.referrer;</script>");//返回上一页并刷新
            return View("MsgReplay", tmm);
        }

        //查看新消息详情
        [BaseActionFilterAttribute]
        public ActionResult RecMsg_Detail(string id)
        {
            T_SysMsg tm = sc.T_SysMsg.Find(id);
            int datetime = tm.CreateTime;
            string recever = (Session["vars"] as ServicePlatform.Config.Vars).UserID;
            //string recever="777";

            T_MsgRec tmr = sc.T_MsgRec.Find(id, recever);
            tmr.ReadTime = datetime;
            sc.SaveChanges();

            string dt = DateTimeFormat.ConvertIntDateTime(datetime).ToString();
            @ViewBag.dttime = dt;
            @ViewBag.msgcontent = tm.MsgContent;
            @ViewBag.PageName = "/Message/MsgCenter/RecMsg";
            @ViewBag.Display = true;
            return View("New_Msg_Detail", tm);
        }

        //删除收件箱消息
        [BaseActionFilterAttribute]
        public ActionResult RecMsg_Delete(string id)
        {
            string recever = (Session["vars"] as ServicePlatform.Config.Vars).UserID;
            T_MsgRec tm = sc.T_MsgRec.Find(id, recever);
            sc.T_MsgRec.Remove(tm);
            sc.SaveChanges();
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        ////批量删除消息删除消息////批量标记为已读
        [BaseActionFilterAttribute]
        public ActionResult RecMsg_Delete_ALL(string action, int id = 1)
        {
            string userid=(Session["vars"] as ServicePlatform.Config.Vars).UserID;
            string idstring = Request.Form["piliang"];
            idstring = idstring.Replace(" ", "");
            string[] temp = idstring.Split('!');
            if (action == "批量删除")
            {
                for (int i = 0; i < temp.Length - 1; i++)
                {
                    temp[i] = temp[i].Trim();
                    //int msgid = Convert.ToInt32(temp[i]);
                    T_MsgRec tm = sc.T_MsgRec.Find(temp[i], userid);
                    sc.T_MsgRec.Remove(tm);
                }
                sc.SaveChanges();
                return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
            }
            else if (action == "批量标记为已读")
            {
                for (int i = 0; i < temp.Length - 1; i++)
                {
                    temp[i] = temp[i].Trim();
                    //int msgid = Convert.ToInt32(temp[i]);
                    T_MsgRec tm = sc.T_MsgRec.Find(temp[i], userid);
                    tm.ReadTime = DateTimeFormat.ConvertDateTimeInt(DateTime.Now);
                    sc.SaveChanges();
                }
                return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
            }
            else
            {
                string jiansuo = Request.Form["jiansuo"];
                int num_jiansuo;
                if (judge_all_num(jiansuo))
                {
                    num_jiansuo = Convert.ToInt32(jiansuo);
                }
                else
                {
                    num_jiansuo = -1;
                }
                //string recever = (Session["vars"] as ServicePlatform.Config.Vars).UserID;;
                var q_result = (from f in sc.T_MsgRec where f.Receiver == userid select f).ToList();
                int count = q_result.Count;
                if (q_result.Count != 0)
                {
                    var q_tempresult = new List<T_MsgRec>();

                    for (int k = 0; k < q_result.Count; k++)
                    {
                        string mid = q_result[k].MsgID;
                        T_SysMsg ts = sc.T_SysMsg.Find(mid);
                        if (ts.MsgContent.ToString().Contains(jiansuo) || ts.MsgOwner.ToString().Contains(jiansuo) || DateTimeFormat.ConvertIntDateTime(ts.CreateTime).ToString().Contains(jiansuo))
                        {
                            T_MsgRec tm = sc.T_MsgRec.Find(mid, userid);
                            q_tempresult.Add(tm);
                        }
                    }
                    string msgcontent = string.Empty;
                    string[] msgtype = new string[count];//消息类型
                    string[] status = new string[count];//是否已读
                    string[] sender = new string[count];//发送人
                    string[] sendtime = new string[count];//发送时间
                    string[] Msg = new string[count];//消息内容
                    for (int i = 0; i < count; i++)
                    {
                        string msgid = q_tempresult[i].MsgID;
                        var q = (from f in sc.T_SysMsg where f.MsgID == msgid select f.MsgTypeID).FirstOrDefault();
                        #region 处理消息类型
                        if (q == 12)
                        {
                            msgtype[i] = "个人";
                        }
                        else
                        {
                            msgtype[i] = "群组";
                        }
                        #endregion

                        #region 处理是否已读
                        if (q_tempresult[i].ReadTime == -1)
                        {
                            status[i] = "<font color=\"#FF00FF\"><b>未读</b></font>";
                        }
                        else
                        {
                            status[i] = "<font color=\"#B5A642\">已读</font>";
                        }
                        #endregion

                        #region 处理消息本体
                        msgid = q_tempresult[i].MsgID;
                        var q_msg = (from f in sc.T_SysMsg where f.MsgID == msgid select f).ToList();
                        msgcontent = q_msg[0].MsgContent;
                        char[] temp1 = msgcontent.ToCharArray();
                        msgcontent = cutstring(temp1);//处理消息内容
                        Msg[i] = msgcontent;
                        sender[i] = q_msg[0].MsgOwner;//发送人
                        sendtime[i] = DateTimeFormat.ConvertIntDateTime(q_msg[0].CreateTime).ToString();//发送时间
                        #endregion
                    }
                    ViewBag.Sender = sender;//发送人
                    ViewBag.SendTime = sendtime;//发送时间
                    ViewBag.Msgtype = msgtype;//消息类型
                    ViewBag.Msgcontent = Msg;//处理消息内容
                    ViewBag.HasRead = status;//是否已读

                    var model = q_tempresult.ToPagedList(id, 10);
                    ViewBag.PageIndex = id;//页面索引
                    if (Request.IsAjaxRequest())
                    {
                        return PartialView("_MsgList", model);
                    }
                    return View("_MsgList",model);
                }
                else
                {
                    return Content("<script>alert('未检索到任何结果');history.go(-1);</script>");//返回上一页并刷新
                }
            }
        }


        ////批量删除收件箱消息和草稿箱消息
        [BaseActionFilterAttribute]
        public ActionResult MsgSendDraft_Delete_ALL(string action, int id = 1)
        {
            if (action == "批量删除")
            {
                string idstring = Request.Form["piliang"];
                idstring = idstring.Replace(" ", "");
                string[] temp = idstring.Split('!');
                for (int i = 0; i < temp.Length - 1; i++)
                {
                    temp[i] = temp[i].Trim();
                    //int msgid = Convert.ToInt32(temp[i]);
                    T_SysMsg tm = sc.T_SysMsg.Find(temp[i]);

                    if (tm != null)
                    { sc.T_SysMsg.Remove(tm); }

                    sc.SaveChanges();
                }
                sc.SaveChanges();            
            }
            else
            {
                string getflga = Request.Form["draft"];
                string jiansuo = Request.Form["jiansuo"];
                int num_jiansuo;
                if (judge_all_num(jiansuo))
                {
                    num_jiansuo = Convert.ToInt32(jiansuo);
                }
                else
                {
                    num_jiansuo = -1;
                }
                string userid = (Session["vars"] as ServicePlatform.Config.Vars).UserID; ;
            }
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }


        //草稿编辑
        [BaseActionFilterAttribute]
        public ActionResult MsgDraft_Edit(string id)
        {
            T_SysMsg tm = sc.T_SysMsg.Find(id);
            if (tm.MsgTypeID == 11)
            {
                List<SelectListItem> RoleList = SchoolHelper.GetRoleList(tm.Receiver);
                ViewData["RoleList"] = RoleList;

                List<SelectListItem> UserList = SchoolHelper.GetUserList();
                ViewData["UserList"] = UserList;
            }
            else
            {
                List<SelectListItem> RoleList = SchoolHelper.GetRoleList();
                ViewData["RoleList"] = RoleList;

                List<SelectListItem> UserList = SchoolHelper.GetUserList(tm.Receiver);
                ViewData["UserList"] = UserList;
            }

            return View("NewMsg", tm);
        }

        //发送草稿
        [BaseActionFilterAttribute]
        public ActionResult MsgDraft_Send(string id)
        {
            T_SysMsg ts = sc.T_SysMsg.Find(id);
            T_MsgSend tm = sc.T_MsgSend.Find(id);
            tm.SendTime = DateTimeFormat.ConvertDateTimeInt(DateTime.Now);
            tm.SendTypeID = 11;
            sc.SaveChanges();

            string recever = tm.Receiver;
            if(ts.MsgTypeID==11)
            {
                var q = (from f in pc.UserRole where f.RoleID == recever select f.UserID).ToList();
                for (int i = 0; i < q.Count; i++)
                {
                    T_MsgRec tr = new T_MsgRec();
                    tr.MsgID = ts.MsgID; ;
                    tr.ReadTime = -1;
                    tr.Receiver = q[i];
                    sc.T_MsgRec.Add(tr);
                    sc.SaveChanges();
                }
            }
            else
            {
                T_MsgRec tr = new T_MsgRec();
                tr.MsgID = ts.MsgID; ;
                tr.ReadTime = -1;
                tr.Receiver = recever;
                sc.T_MsgRec.Add(tr);
                sc.SaveChanges();
            }
            return Content("<script>alert('消息已发送！');window.location.href = document.referrer;</script>");//返回上一页并刷新
        }

        //删除草稿
        [BaseActionFilterAttribute]
        public ActionResult MsgDraft_Delete(string id)
        {
            T_SysMsg tm = sc.T_SysMsg.Find(id);

            if (tm != null)
            { sc.T_SysMsg.Remove(tm); }
            sc.SaveChanges();
            return RedirectToAction("MsgDraft");
        }

        //查看已发送的消息详情
        [BaseActionFilterAttribute]
        public ActionResult MsgSended_Detail(string id)
        {
            T_SysMsg tm = sc.T_SysMsg.Find(id);
            @ViewBag.msgcontent = tm.MsgContent;
            int datetime = tm.CreateTime;
            string dt = DateTimeFormat.ConvertIntDateTime(datetime).ToString();
            @ViewBag.dttime = dt;
            @ViewBag.PageName = "/Message/MsgCenter/MsgSended";
            @ViewBag.Display = false;
            return View("New_Msg_Detail", tm);
        }

        //删除已发送的消息
        [BaseActionFilterAttribute]
        public ActionResult MsgSended_Delete(string id)
        {
            T_SysMsg ts=sc.T_SysMsg.Find(id);
            sc.T_SysMsg.Remove(ts);
            sc.SaveChanges();
            return RedirectToAction("MsgSended");
        }

        //新消息详情页面
        public ViewResult New_Msg_Detail()
        {
            return View();
        }


        //消息回复页面
        public ViewResult MsgReplay()
        {
            return View();
        }

        //AJAX分页技术
        [BaseActionFilterAttribute]
        public ActionResult RecMsg(int id = 1)
        {
            using (var mc = new SchoolContext())
            {
                string userid = (Session["vars"] as ServicePlatform.Config.Vars).UserID;
                var q_msgrec = (from f in sc.T_MsgRec where f.Receiver == userid select f).ToList();
                int count = q_msgrec.Count;
                if (count != 0)
                {
                    string msgcontent = string.Empty;
                    string[] msgtype = new string[count];//消息类型
                    string[] status = new string[count];//是否已读
                    string[] sender = new string[count];//发送人
                    string[] sendtime = new string[count];//发送时间
                    string[] Msg = new string[count];//消息内容
                    for (int i = 0; i < count; i++)
                    {
                        string msgid=q_msgrec[i].MsgID;
                        var q = (from f in sc.T_SysMsg where f.MsgID == msgid select f.MsgTypeID).FirstOrDefault();
                        #region 处理消息类型
                        if (q == 12)
                        {
                            msgtype[i] = "个人";
                        }
                        else
                        {
                            msgtype[i] = "群组";
                        }
                        #endregion

                        #region 处理是否已读
                        if (q_msgrec[i].ReadTime == -1)
                        {
                            status[i] = "<font color=\"#FF00FF\"><b>未读</b></font>";
                        }
                        else
                        {
                        status[i] = "<font color=\"#B5A642\">已读</font>";
                        }
                        #endregion

                        #region 处理消息本体
                        msgid = q_msgrec[i].MsgID;
                        var q_msg = (from f in sc.T_SysMsg where f.MsgID == msgid select f).ToList();
                        msgcontent = q_msg[0].MsgContent;
                        char[] temp = msgcontent.ToCharArray();
                        msgcontent = cutstring(temp);//处理消息内容
                        Msg[i] = msgcontent;
                        sender[i] = q_msg[0].MsgOwner;//发送人
                        sendtime[i] = DateTimeFormat.ConvertIntDateTime(q_msg[0].CreateTime).ToString();//发送时间
                        #endregion
                    }
                    ViewBag.Sender = sender;//发送人
                    ViewBag.SendTime = sendtime;//发送时间
                    ViewBag.Msgtype = msgtype;//消息类型
                    ViewBag.Msgcontent = Msg;//处理消息内容
                    ViewBag.HasRead = status;//是否已读

                    var model = q_msgrec.ToPagedList(id, 10);
                    ViewBag.PageIndex = id;//页面索引
                    if (Request.IsAjaxRequest())
                    {
                        return PartialView("_MsgList", model);
                    }
                    return View(model);
                }
                else
                {
                    return Content("您的收件箱空空如也，您可稍后刷新或浏览别的网页-_-||");
                }
            }
        }


        //发件箱--分页
        [BaseActionFilterAttribute]
        public ActionResult MsgSended(int id = 1) 
        {
            string userid = (Session["vars"] as ServicePlatform.Config.Vars).UserID;
            //string userid = "777";
            var query1 = (from f in sc.T_SysMsg where f.MsgOwner == userid select f).ToList();
            var query = (from f in query1 where f.SendTypeID == 11 select f).ToList();
            if (query.Count != 0)
            {
                string[] datetime = new string[query.Count];
                string[] msgtype = new string[query.Count];
                string[] hasred = new string[query.Count];
                string[] content = new string[query.Count];
                string[] receive = new string[query.Count];
                string[] recrole = new string[query.Count];
                for (int i = 0; i < query.Count; i++)
                {
                    datetime[i] = (DateTimeFormat.ConvertIntDateTime(query[i].SendTime)).ToString();
                    int msgtypeid = query[i].MsgTypeID;
                    string rr = query[i].Receiver;
                    #region 处理消息类型
                    if (msgtypeid == 12)
                    {
                        msgtype[i] = "个人";

                        var q = (from f in ec.T_User where f.UserID == rr select f.NickName).FirstOrDefault();
                        receive[i] = q;
                        recrole[i] = "/";
                    }
                    else
                    {
                        msgtype[i] = "群组";
                        var q = (from f in pc.Role where f.RoleID == rr select f.RoleName).FirstOrDefault();
                        receive[i] = "/";
                        recrole[i] = q;
                    }
                    #endregion

                    content[i] = query[i].MsgContent;
                    #region 处理已读未读

                    string msgid=query[i].MsgID;
                    var q_all = (from f in sc.T_MsgRec where f.MsgID == msgid select f).ToList();
                    var q_notread = (from f in sc.T_MsgRec where f.MsgID == msgid && f.ReadTime == -1 select f).ToList();
                    hasred[i] = q_notread.Count.ToString() + " / " + q_all.Count.ToString()+"(未读/全部)";

                    #endregion
                }
                ViewBag.dt = datetime;
                ViewBag.msgt = msgtype;
                ViewBag.Count = query.Count;
                ViewBag.Read = hasred;
                ViewBag.MsgCon = content;
                ViewBag.Receive = receive;
                ViewBag.Recrole = recrole;
                var model = query.ToPagedList(id, 10);
                ViewBag.PageIndex = id;
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_MsgSendedList", model);
                }
                return View(model);
            }
            else
            {
                return Content("您的发件箱空空如也-_-||");
            }
        }


        //草稿箱--分页
        [BaseActionFilterAttribute]
        public ActionResult MsgDraft(int id = 1)
        {
            string userid = (Session["vars"] as ServicePlatform.Config.Vars).UserID;
            //string userid="777";
            var query1 = (from f in sc.T_SysMsg where f.MsgOwner==userid select f).ToList();
            var query=(from f in query1 where f.SendTypeID==12 select f).ToList();

            if (query.Count != 0)
            {
                string[] datetime = new string[query.Count];
                string[] msgtype = new string[query.Count];
                string[] content = new string[query.Count];
                string[] receive = new string[query.Count];
                string[] recrole = new string[query.Count];
                for (int i = 0; i < query.Count; i++)
                {
                    datetime[i] = (DateTimeFormat.ConvertIntDateTime(query[i].CreateTime)).ToString();
                    int msgtypeid = query[i].MsgTypeID;
                    string rr=query[i].Receiver;
                    #region 处理消息类型
                    if (msgtypeid == 12)
                    {
                        msgtype[i] = "个人";
                        
                        var q = (from f in ec.T_User where f.UserID == rr select f.NickName).FirstOrDefault();
                        receive[i] = q;
                        recrole[i] = "/";
                    }
                    else
                    {
                        msgtype[i] = "群组";
                        var q = (from f in pc.Role where f.RoleID == rr select f.RoleName).FirstOrDefault();
                        receive[i] = "/";
                        recrole[i] = q;
                    }
                    #endregion

                    content[i] = query[i].MsgContent;
                }
                ViewBag.dt = datetime;
                ViewBag.msgt = msgtype;
                ViewBag.Count = query.Count;
                ViewBag.MsgCon = content;
                ViewBag.Receive = receive;
                ViewBag.Recrole = recrole;
                var model = query.ToPagedList(id, 10);
                ViewBag.PageIndex = id;
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_MsgDraftList", model);
                }
                return View(model);
            }
            else
            {
                return Content("您的草稿箱空空如也-_-||");
            }
        }


        [BaseActionFilterAttribute]
        public ActionResult Editor()
        {
            return View();
        }

        //标记已读
        [BaseActionFilterAttribute]
        public ActionResult MarkReaded(string id)
        {
            string recever = (Session["vars"] as ServicePlatform.Config.Vars).UserID;
            T_MsgRec tmr = sc.T_MsgRec.Find(id, recever);
            tmr.ReadTime = DateTimeFormat.ConvertDateTimeInt(DateTime.Now);
            sc.SaveChanges();
            return Content("<script>window.location.href = document.referrer;</script>");//返回上一页并刷新
        }
        public object tm { get; set; }

    }
}
