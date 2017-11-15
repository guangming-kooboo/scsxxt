using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Models;
using ServicePlatform.Lib;
using ServicePlatform.App_Start;
using System.ComponentModel;
using ServicePlatform.Areas.Permission.Models;

namespace ServicePlatform.Lib
{
    public class SchoolHelper
    {
        private SchoolContext sc = new SchoolContext();
        private UserContext uc = new UserContext();
        private EnterpriseContext ec = new EnterpriseContext();
        private CodeTableContext ctc = new CodeTableContext();
        private PermissionContext pc = new PermissionContext();

        //获取该学校当前批次
        public string GetCurrentBatch(string schoolid)
        {
            if(schoolid=="")
            {
                throw new Exception("异常：未传入schoolid ！");
            }
            var q = (from f in sc.PracBatch where f.SchoolID == schoolid && f.IsCurrentBatch == "是" select f).ToList();
            if (q.Count > 1|| q.Count ==0)
            {
                throw new Exception("异常：找到"+ q.Count+ "个schoolid为" + schoolid+"的当前批次号");
                
            }
            else return q[0].PracBatchID;
        }

        //获取企业当前的实习号
        public string GetEntPracticeNo(string entid,string batchid)
        {
            var q = (from f in ec.T_EntBatchReg where f.Ent_No == entid && f.PracBatchID == batchid select f.EntPracNo).ToList();
            return q[0];
        }

        //获取企业参与的批次列表
        public List<SelectListItem> GetEntBatchList(string entno,string sid)
        {
            var q_bacth = (from f in ec.T_EntBatchReg where f.Ent_No == entno select f).ToList();
            var q_batch1 = (from f in q_bacth where f.SchoolID == sid select f).ToList();
            //var q_bacth = (from f in sc.School select f).ToList();
            List<SelectListItem> BatchList = new List<SelectListItem>();
            if (q_batch1.Count != 0 && q_batch1 != null)
            {
                for(int i = 0; i < q_batch1.Count; i++)
                {
                    BatchList.Add(new SelectListItem() { Text = q_batch1[i].PracBatchName, Value = q_batch1[i].PracBatchID });
                }
            }
            else
            {
                BatchList.Add(new SelectListItem() { Text = "批次不存在", Value = "批次不存在", Selected = true });
            }
            return BatchList;
        }

        //获取企业参与的批次列表--带焦点
        public List<SelectListItem> GetEntBatchList(string entno,string sid,string focus)
        {
            var q_bacth = (from f in ec.T_EntBatchReg where f.Ent_No == entno select f).ToList();
            //var q_bacth = (from f in sc.School select f).ToList();
            List<SelectListItem> BatchList = new List<SelectListItem>();
            if (q_bacth.Count != 0 && q_bacth != null)
            {
                for (int i = 0; i < q_bacth.Count; i++)
                {
                    if (q_bacth[i].PracBatchID == focus)
                    {
                        BatchList.Add(new SelectListItem() { Text = q_bacth[i].PracBatchName, Value = q_bacth[i].PracBatchID, Selected=true });
                    }
                    else
                    {
                        BatchList.Add(new SelectListItem() { Text = q_bacth[i].PracBatchName, Value = q_bacth[i].PracBatchID, Selected=false });
                    }
                    
                }
            }
            else
            {
                BatchList.Add(new SelectListItem() { Text = "批次不存在", Value = "批次不存在", Selected = true });
            }
            return BatchList;
        }

        //获取学生实习号
        public string GetStuPracticeNo(string userid)
        {
            var q = (from f in sc.StuBatchReg where f.UserID == userid select f.PracticeNo).ToList();
            if(q.Count==0||q==null)
            {
                return "未注册批次，数据出错!";
            }
            //string batchid = GetCurrentBatch(schoolid);
            return q[0];
        }

        //修改：获取学生当前实习号
        public string GetStuCurrentPracticeNo(string userid,string schoolid)
        {
            string batchid = GetCurrentBatch(schoolid);
            var q = (from f in sc.StuBatchReg where f.UserID == userid && f.PracBatchID == schoolid select f.PracticeNo).ToList();
            if (q.Count == 0 || q == null)
            {
                return "未注册批次，数据出错!";
            }
            
            return q[0];
        }
        //获取学校
        public List<SelectListItem> GetSchoolList()
        {
            var q_cate = (from f in sc.School select f).ToList();
            List<SelectListItem> School = new List<SelectListItem>();
            if (q_cate.Count != 0 && q_cate != null)
            {
                for (int i = 0; i < q_cate.Count; i++)
                {
                    School.Add(new SelectListItem() { Text = q_cate[i].SchoolName, Value = q_cate[i].SchoolID });
                }
            }
            else
            {
                School.Add(new SelectListItem() { Text = "学校不存在", Value = "学校不存在", Selected = true });
            }
            return School;
        }

        //获取学校--带焦点
        public List<SelectListItem> GetSchoolList(string focus)
        {
            var q_cate = (from f in sc.School select f).ToList();
            List<SelectListItem> School = new List<SelectListItem>();
            if (q_cate.Count != 0 && q_cate != null)
            {
                for (int i = 0; i < q_cate.Count; i++)
                {
                    if (q_cate[i].SchoolID == focus)
                    {
                        School.Add(new SelectListItem() { Text = q_cate[i].SchoolName, Value = q_cate[i].SchoolID ,Selected=true});
                    }
                    else
                    {
                        School.Add(new SelectListItem() { Text = q_cate[i].SchoolName, Value = q_cate[i].SchoolID, Selected = false });
                    }
                    
                }
            }
            else
            {
                School.Add(new SelectListItem() { Text = "学校不存在", Value = "学校不存在", Selected = true });
            }
            return School;
        }

        //获取学校---带筛选条件
        public List<SelectListItem> GetSchoolList(List<string> Condition,string focus)
        {
            var q_cate = (from f in sc.School select f).ToList();
            List<SelectListItem> School = new List<SelectListItem>();
            if (q_cate.Count != 0 && q_cate != null)
            {
                for (int i = 0; i < q_cate.Count; i++)
                {
                    if (Condition.Contains(q_cate[i].SchoolID))
                    {
                        if(q_cate[i].SchoolID==focus)
                        {
                            School.Add(new SelectListItem() { Text = q_cate[i].SchoolName, Value = q_cate[i].SchoolID, Selected = true });
                        }
                        else
                        {
                            School.Add(new SelectListItem() { Text = q_cate[i].SchoolName, Value = q_cate[i].SchoolID, Selected = false });
                        }                       
                    }

                }
            }
            else
            {
                School.Add(new SelectListItem() { Text = "学校不存在", Value = "学校不存在", Selected = true });
            }
            return School;
        }
        //获取年级
        public List<SelectListItem> GetEntryYearList(string schoolid)
        {
            var q_cate = (from f in sc.C_Specialty where f.SchoolID == schoolid select f).ToList();
            string nowyear = q_cate[0].EntryYear.ToString();
            List<SelectListItem> EntryYear = new List<SelectListItem>();
            if (q_cate.Count != 0 && q_cate != null)
            {
                EntryYear.Add(new SelectListItem() { Text = q_cate[0].EntryYear_S, Value = q_cate[0].EntryYear_S, Selected = true });
                for (int i = 1; i < q_cate.Count; i++)
                {
                    if (q_cate[i].EntryYear_S != nowyear)
                    {
                        EntryYear.Add(new SelectListItem() { Text = q_cate[i].EntryYear_S, Value = q_cate[i].EntryYear_S, Selected = false });
                        nowyear = q_cate[i].EntryYear_S;
                    }
                }
            }
            else
            {
                EntryYear.Add(new SelectListItem() { Text = "年级不存在", Value = "年级不存在", Selected = true });
            }
            return EntryYear;
        }

        //获取年级--带焦点
        public List<SelectListItem> GetEntryYearList(string schoolid,string focus)
        {
            var q_cate = (from f in sc.C_Specialty where f.SchoolID == schoolid select f).ToList();
            string nowyear = string.Empty;
            List<SelectListItem> EntryYear = new List<SelectListItem>();
            if (q_cate.Count != 0 && q_cate != null)
            {
                //EntryYear.Add(new SelectListItem() { Text = q_cate[0].EntryYear_S, Value = q_cate[0].EntryYear_S, Selected = true });
                for (int i = 0; i < q_cate.Count; i++)
                {
                    if (q_cate[i].EntryYear_S != nowyear)
                    {
                        if (q_cate[i].EntryYear_S==focus)
                        {
                            EntryYear.Add(new SelectListItem() { Text = q_cate[i].EntryYear_S, Value = q_cate[i].EntryYear_S, Selected = true });
                        }
                        else
                        {
                            EntryYear.Add(new SelectListItem() { Text = q_cate[i].EntryYear_S, Value = q_cate[i].EntryYear_S, Selected = false });
                        }
                        nowyear = q_cate[i].EntryYear_S;
                    }
                }
            }
            else
            {
                EntryYear.Add(new SelectListItem() { Text = "年级不存在", Value = "年级不存在", Selected = true });
            }
            return EntryYear;
        }

        //获取专业
        public List<SelectListItem> GetSpecialtyList(string schoolid)
        {
            var q_cate = (from f in sc.C_Specialty where f.SchoolID == schoolid select f).ToList();
            List<SelectListItem> Specialty = new List<SelectListItem>();
            if (q_cate.Count != 0 && q_cate != null)
            {
                for (int i = 0; i < q_cate.Count; i++)
                {
                    Specialty.Add(new SelectListItem() { Text = q_cate[i].SpeName, Value = q_cate[i].SpeNo });
                }
            }
            else
            {
                Specialty.Add(new SelectListItem() { Text = "专业不存在", Value = "专业不存在", Selected = false});
            }
            return Specialty;
        }

        //获取专业--带焦点
        public List<SelectListItem> GetSpecialtyList(string schoolid,string focus)
        {
            var q_cate = (from f in sc.C_Specialty where f.SchoolID == schoolid select f).ToList();
            List<SelectListItem> Specialty = new List<SelectListItem>();
            if (q_cate.Count != 0 && q_cate != null)
            {
                for (int i = 0; i < q_cate.Count; i++)
                {
                    if (q_cate[i].SpeNo == focus)
                    {
                        Specialty.Add(new SelectListItem() { Text = q_cate[i].SpeName, Value = q_cate[i].SpeNo,Selected=true });
                    }
                    else
                    {
                        Specialty.Add(new SelectListItem() { Text = q_cate[i].SpeName, Value = q_cate[i].SpeNo, Selected = false });
                    }      
                }
            }
            else
            {
                Specialty.Add(new SelectListItem() { Text = "专业不存在", Value = "专业不存在", Selected = false });
            }
            return Specialty;
        }

        //获取专业-带年份-带焦点
        public List<SelectListItem> GetSpecialtyList(string schoolid,int entryyear, string focus)
        {
            var q_cate = (from f in sc.C_Specialty where f.SchoolID == schoolid && f.EntryYear == entryyear select f).ToList();
            List<SelectListItem> Specialty = new List<SelectListItem>();
            if (q_cate.Count != 0 && q_cate != null)
            {
                for (int i = 0; i < q_cate.Count; i++)
                {
                    if (q_cate[i].SpeNo == focus)
                    {
                        Specialty.Add(new SelectListItem() { Text = q_cate[i].SpeName, Value = q_cate[i].SpeNo, Selected = true });
                    }
                    else
                    {
                        Specialty.Add(new SelectListItem() { Text = q_cate[i].SpeName, Value = q_cate[i].SpeNo, Selected = false });
                    }
                }
            }
            else
            {
                Specialty.Add(new SelectListItem() { Text = "专业不存在", Value = "专业不存在", Selected = false });
            }
            return Specialty;
        }

        //获取班级
        public List<SelectListItem> GetClassList(string schoolid)
        {
            var q_cate = (from f in sc.T_Class where f.SchoolID == schoolid select f).ToList();
            List<SelectListItem> Class = new List<SelectListItem>();
            if (q_cate.Count != 0 && q_cate != null)
            {
                for (int i = 0; i < q_cate.Count; i++)
                {
                    Class.Add(new SelectListItem() { Text = q_cate[i].ClassName, Value = q_cate[i].ClassID });
                }
            }
            else
            {
                Class.Add(new SelectListItem() { Text = "班级不存在", Value = "班级不存在", Selected = true });
            }
            return Class;
        }

        //获取班级--带焦点
        public List<SelectListItem> GetClassList(string schoolid,string focus)
        {
            var q_cate = (from f in sc.T_Class where f.SchoolID == schoolid select f).ToList();
            List<SelectListItem> Class = new List<SelectListItem>();
            if (q_cate.Count != 0 && q_cate != null)
            {
                for (int i = 0; i < q_cate.Count; i++)
                {
                    if(q_cate[i].ClassID==focus)
                    {
                        Class.Add(new SelectListItem() { Text = q_cate[i].ClassName, Value = q_cate[i].ClassID ,Selected=true});
                    }
                    else
                    {
                        Class.Add(new SelectListItem() { Text = q_cate[i].ClassName, Value = q_cate[i].ClassID ,Selected=false});
                    }
                }
            }
            else
            {
                Class.Add(new SelectListItem() { Text = "班级不存在", Value = "班级不存在", Selected = true });
            }
            return Class;
        }

        //获取班级-带专业-带焦点
        public List<SelectListItem> GetClassList(string schoolid, string speno, string focus)
        {
            var q_cate = (from f in sc.T_Class where f.SchoolID == schoolid && f.SpeNo == speno select f).ToList();
            List<SelectListItem> Class = new List<SelectListItem>();
            if (q_cate.Count != 0 && q_cate != null)
            {
                for (int i = 0; i < q_cate.Count; i++)
                {
                    if (q_cate[i].ClassID == focus)
                    {
                        Class.Add(new SelectListItem() { Text = q_cate[i].ClassName, Value = q_cate[i].ClassID, Selected = true });
                    }
                    else
                    {
                        Class.Add(new SelectListItem() { Text = q_cate[i].ClassName, Value = q_cate[i].ClassID, Selected = false });
                    }
                }
            }
            else
            {
                Class.Add(new SelectListItem() { Text = "班级不存在", Value = "班级不存在", Selected = true });
            }
            return Class;
        }

        //获取企业
        public List<SelectListItem> GetEntList(string prcticeno)
        {
            var q_cate = (from f in ec.T_EntBatchReg where f.PracBatchID == prcticeno select f).ToList();
            List<SelectListItem> Enterprise = new List<SelectListItem>();
            if (q_cate.Count != 0 && q_cate != null)
            {
                for (int i = 0; i < q_cate.Count; i++)
                {
                    Enterprise.Add(new SelectListItem() { Text = q_cate[i].Ent_Name, Value = q_cate[i].Ent_No });
                }
            }
            else
            {
                Enterprise.Add(new SelectListItem() { Text = "企业不存在", Value = "企业不存在", Selected = true });
            }
            return Enterprise;
        }

        //获取企业--带焦点
        public List<SelectListItem> GetEntList(string prcticeno,string focus)
        {
            var q_cate = (from f in ec.T_EntBatchReg where f.PracBatchID == prcticeno select f).ToList();
            List<SelectListItem> Enterprise = new List<SelectListItem>();
            if (q_cate.Count != 0 && q_cate != null)
            {
                for (int i = 0; i < q_cate.Count; i++)
                {
                    if (q_cate[i].Ent_No == focus)
                    {
                        Enterprise.Add(new SelectListItem() { Text = q_cate[i].Ent_Name, Value = q_cate[i].Ent_No,Selected=true });
                    }
                    else
                    {
                        Enterprise.Add(new SelectListItem() { Text = q_cate[i].Ent_Name, Value = q_cate[i].Ent_No,Selected=false});
                    }
                    
                }
            }
            else
            {
                Enterprise.Add(new SelectListItem() { Text = "企业不存在", Value = "企业不存在", Selected = true });
            }
            return Enterprise;
        }

        //获取岗位
        public List<SelectListItem> GetPosList(string EntPracticeNo)
        {
            var q_cate = (from f in ec.T_PracticePosition where f.EntPracNo == EntPracticeNo select f).ToList();
            List<SelectListItem> Position = new List<SelectListItem>();
            //Position.Add(new SelectListItem() { Text = "全部", Value = "全部", Selected = true });
            if (q_cate.Count != 0 && q_cate != null)
            {
                for (int i = 0; i < q_cate.Count; i++)
                {
                    Position.Add(new SelectListItem() { Text = q_cate[i].PositionName, Value = q_cate[i].PositionID });
                }
            }
            else
            {
                Position.Add(new SelectListItem() { Text = "班级不存在", Value = "班级不存在", Selected = true });
            }
            return Position;
        }

        //获取岗位--带焦点
        public List<SelectListItem> GetPosList(string EntPracticeNo,string focus)
        {
            var q_cate = (from f in ec.T_PracticePosition where f.EntPracNo == EntPracticeNo select f).ToList();
            List<SelectListItem> Position = new List<SelectListItem>();
            //Position.Add(new SelectListItem() { Text = "全部", Value = "全部", Selected = true });
            if (q_cate.Count != 0 && q_cate != null)
            {
                for (int i = 0; i < q_cate.Count; i++)
                {
                    if (q_cate[i].PositionID == focus)
                    {
                        Position.Add(new SelectListItem() { Text = q_cate[i].PositionName, Value = q_cate[i].PositionID, Selected=true });
                    }
                    else
                    {
                        Position.Add(new SelectListItem() { Text = q_cate[i].PositionName, Value = q_cate[i].PositionID ,Selected=false});
                    }
                    
                }
            }
            else
            {
                Position.Add(new SelectListItem() { Text = "岗位不存在", Value = "岗位不存在", Selected = true });
            }
            return Position;
        }

        public List<SelectListItem> GetRoleList()
        {
            var q_cate = (from f in pc.Role select f).ToList();
            List<SelectListItem> Role = new List<SelectListItem>();
            if (q_cate.Count != 0 && q_cate != null)
            {
                for (int i = 0; i < q_cate.Count; i++)
                {
                    Role.Add(new SelectListItem() { Text = q_cate[i].RoleName, Value = q_cate[i].RoleID });
                }
            }
            else
            {
                Role.Add(new SelectListItem() { Text = "角色不存在", Value = "角色不存在", Selected = true });
            }
            return Role;
        }

        public List<SelectListItem> GetRoleList(string focus)
        {
            var q_cate = (from f in pc.Role select f).ToList();
            List<SelectListItem> Role = new List<SelectListItem>();
            if (q_cate.Count != 0 && q_cate != null)
            {
                for (int i = 0; i < q_cate.Count; i++)
                {
                    if (q_cate[i].RoleID == focus)
                    {
                        Role.Add(new SelectListItem() { Text = q_cate[i].RoleName, Value = q_cate[i].RoleID ,Selected=true});
                    }
                    else
                    {
                        Role.Add(new SelectListItem() { Text = q_cate[i].RoleName, Value = q_cate[i].RoleID, Selected = false });
                    }              
                }
            }
            else
            {
                Role.Add(new SelectListItem() { Text = "角色不存在", Value = "角色不存在", Selected = true });
            }
            return Role;
        }

        public List<SelectListItem> GetUserList()
        {
            var q_cate = (from f in ec.T_User select f).ToList();
            List<SelectListItem> Role = new List<SelectListItem>();
            if (q_cate.Count != 0 && q_cate != null)
            {
                for (int i = 0; i < q_cate.Count; i++)
                {
                    Role.Add(new SelectListItem() { Text = q_cate[i].NickName, Value = q_cate[i].UserID });
                }
            }
            else
            {
                Role.Add(new SelectListItem() { Text = "用户不存在", Value = "用户不存在", Selected = true });
            }
            return Role;
        }

        public List<SelectListItem> GetUserList(string focus)
        {
            var q_cate = (from f in ec.T_User select f).ToList();
            List<SelectListItem> Role = new List<SelectListItem>();
            if (q_cate.Count != 0 && q_cate != null)
            {
                for (int i = 0; i < q_cate.Count; i++)
                {
                    if (q_cate[i].UserID == focus)
                    {
                        Role.Add(new SelectListItem() { Text = q_cate[i].NickName, Value = q_cate[i].UserID ,Selected=true});
                    }
                    else
                    {
                        Role.Add(new SelectListItem() { Text = q_cate[i].NickName, Value = q_cate[i].UserID ,Selected=false});
                    }
                    
                }
            }
            else
            {
                Role.Add(new SelectListItem() { Text = "用户不存在", Value = "用户不存在", Selected = true });
            }
            return Role;
        }

        public List<SelectListItem> GetProPositionList()
        {
            var q_cate = (from f in ctc.C_ProPosition select f).ToList();
            List<SelectListItem> Role = new List<SelectListItem>();
            if (q_cate.Count != 0 && q_cate != null)
            {
                for (int i = 0; i < q_cate.Count; i++)
                {
                    Role.Add(new SelectListItem() { Text = q_cate[i].ProPosName, Value = q_cate[i].ProPosID.ToString() });
                }
            }
            else
            {
                Role.Add(new SelectListItem() { Text = "职称不存在", Value = "职称不存在", Selected = true });
            }
            return Role;
        }

        public List<SelectListItem> GetProPositionList(int focus)
        {
            var q_cate = (from f in ctc.C_ProPosition select f).ToList();
            List<SelectListItem> Role = new List<SelectListItem>();
            if (q_cate.Count != 0 && q_cate != null)
            {
                for (int i = 0; i < q_cate.Count; i++)
                {
                    if (q_cate[i].ProPosID == focus)
                    {
                        Role.Add(new SelectListItem() { Text = q_cate[i].ProPosName, Value = q_cate[i].ProPosID.ToString(), Selected = true });
                    }
                    else
                    {
                        Role.Add(new SelectListItem() { Text = q_cate[i].ProPosName, Value = q_cate[i].ProPosID.ToString(), Selected = false });
                    }
                }
            }
            else
            {
                Role.Add(new SelectListItem() { Text = "职称不存在", Value = "职称不存在", Selected = true });
            }
            return Role;
        }

        //获取相应的人数统计数据
        /* 0:未安排面试时间
         * 1:已安排面试时间
         * 2:已面试
         * 3:未通过面试
         * 4:通过面试
         * 5:学生同意,分配成功
         * 6:学生拒绝,分配失败
         * 7:已录入实习鉴定表
         * 8:已录入实习评价
         * */
        public int GetRelativeNum(int Key)
        {
            return 0;
        }


        //获取模型列及行
        public static string[] GetModelInfo_Th(object o)
        {
            var t = o.GetType();
            var p = t.GetProperties();
            string[] Properties = new string[p.Length];
            string[] PropertiesDisplayName = new string[p.Length+1];
            //string[] Values = new string[p.Length];
            for (int i = 0; i < p.Length; i++)
            {
                Properties[i] = p[i].Name;
                var property = t.GetProperty(Properties[i]);
                if (property != null)
                {
                    var displayName = ((DisplayNameAttribute[])property.GetCustomAttributes(typeof(DisplayNameAttribute), false)).FirstOrDefault();
                    if (displayName != null)
                    {
                        PropertiesDisplayName[i] = displayName.DisplayName.ToString();
                    }
                    else
                    {
                        PropertiesDisplayName[i] = "未显示名字";
                    }
                }
                else
                {
                    PropertiesDisplayName[i] = "未显示名字";
                }
                //Values[i] = t.GetProperty(p[i].Name).GetValue(o).ToString();
            }
            PropertiesDisplayName[p.Length] = "操作";
            return PropertiesDisplayName;
        }

        public static string[] GetModelInfo_Tr(object o)
        {
            var t = o.GetType();
            var p = t.GetProperties();
            //string[] Properties = new string[p.Length];
            string[] Values = new string[p.Length+1];
            for (int i = 0; i < p.Length; i++)
            {
                object tt = t.GetProperty(p[i].Name).GetValue(o).GetType();
                string ttt = tt.ToString();
                //Properties[i] = p[i].Name;
                Values[i] = t.GetProperty(p[i].Name).GetValue(o).ToString();
            }
            return Values;
        }


        //获取学生评价企业、学校的总分
        public double GetStuEvaEntSchoolScore(string practiceNo,string type)
        {
            double score = 0.0;
            int temp_score = 0;
            int Power=0;
            if(type=="Ent")
            {

                var q = (from f in sc.StuEvaluateEnt where f.PracticeNo == practiceNo select f).ToList();

                if (q.Count==0)
                {
                    return score;
                }
                for(int i=0;i<q.Count;i++)
                {
                    //权重*分数
                    var _weight = ctc.C_StuEvaluateEntItem.Find(q[i].ItemNo).ItemPower;
                    var _score = ctc.C_StuEvaEntGradeLevelItem.Find(q[i].ItemGrade).Weight;
                    temp_score += _weight * _score;
                    // 权重和
                    Power += _weight;
                }        
            }
            else
            {
                var q = (from f in sc.StuEvaluateSchool where f.PracticeNo == practiceNo select f).ToList();
                if (q.Count == 0)
                {
                    return score;
                }
                for(int i=0;i<q.Count;i++)
                {
                    //权重*分数
                    var _weight = ctc.C_StuEvaluateSchoolItem.Find(q[i].ItemNo).ItemPower;
                    var _score = ctc.C_StuEvaSchoolGradeLevelItem.Find(q[i].ItemGrade).Weight;
                    temp_score += _weight * _score;
                    // 权重和
                    Power += _weight;
                }
            }

            if (Power == 0 || temp_score==0)
            {
                return score;
            }
            score = temp_score / Convert.ToDouble(Power);
            score = Math.Round(score, 2);
            return score;
        }
    }
}