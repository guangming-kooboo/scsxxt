using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Areas.Permission.Lib;
using System.Reflection;
namespace ServicePlatform.Lib
{
    public static class PageHelper
    {

        public static string Tip(string tip,int returnIndex)
        {
           
            return  "<script language=javascript>alert('" + tip + "');history.go(" + returnIndex + ");</script>";
        }
        public static string Tip(string tip)
        {
            return "<script>alert('"+tip+"'); window.location.href=document.referrer</script>";
        }

        public static string Tip(string tip, string returnUrl)
        {


            tip = " <script language=javascript>alert('" + tip + "');window.location.href='" + returnUrl + "';</script>";
            
            return tip;
        }
        public static List<SelectListItem> GetCodeTable(DbContext db, string tableName, string nameSpace, int textIndex = 1,int valueIndex = 0,string contitions="")
        {
          
            string fullName = nameSpace + "." + tableName;
            var obj = Assembly.GetExecutingAssembly().CreateInstance(fullName);

             List<SelectListItem>items=new List<SelectListItem>();

             var datas = db.Database.SqlQuery(obj.GetType(), "select * from " + tableName +" "+ contitions, new object[] { });
            
             foreach (var data in datas)
             {

                 var item = LibHelper.ReflectionHelper.GetObjInfo(data)[1];
                items.Add(new SelectListItem() {  Value = item[valueIndex],Text = item[textIndex] });
             }

             return items;
        }

        public static List<SelectListItem> GetSelectListItem<T>(DbContext db, string sql, int textIndex = 1, int valueIndex = 0,string deafultValue="")
        {

            string fullName = typeof(T).FullName;
            var obj = Assembly.GetExecutingAssembly().CreateInstance(fullName);

            List<SelectListItem> items = new List<SelectListItem>();

            var datas = db.Database.SqlQuery(obj.GetType(), sql, new object[] { });

            foreach (var data in datas)
            {

                var item = LibHelper.ReflectionHelper.GetObjInfo(data)[1];
                if (deafultValue == item[valueIndex])
                {
                    items.Add(new SelectListItem() { Value = item[valueIndex], Text = item[textIndex], Selected = true });
                }     
                else
                {
                    items.Add(new SelectListItem() { Value = item[valueIndex], Text = item[textIndex], Selected = false });
                }
            }

            return items;
        }
        public static List<SelectListItem> GetSelectListItem<T>(List<T> srcData, int textIndex = 1, int valueIndex = 0)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var data in srcData)
            {

                var item = LibHelper.ReflectionHelper.GetObjInfo(data)[1];
                items.Add(new SelectListItem() { Value = item[valueIndex], Text = item[textIndex] });
            }

            return items;
        }
        public static string GetSelectHtml(List<SelectListItem> items, string name, string className = "dfinput")
        {

            string html = "<select class=\"" + className + "\" name=\"" + name + "\"  id=\"" + name + "\" >";

            html += "<option selected=\"selected\" value=\"" + "-1" + "\">" + "请选择" + "</option>";
             foreach (var item in items)
             {
                 if (item.Text.Contains("全部") || item.Text.Contains("请选择"))
                     continue;
                 if (item.Selected)
                 {
                     html += "<option selected=\"selected\" value=\"" + item.Value + "\">" + item.Text + "</option>";
                 }
                 else
                 {
                     html += "<option value=\"" + item.Value + "\">" + item.Text + "</option>";
                 }
              
             }
             html += " </select>";
             return html;
        }

        public static List<string> GetModelNote<T>(T model, string[] notes)
        {
            List<string> result = new List<string>();


            string[][] temp = LibHelper.ReflectionHelper.GetObjInfo(model);
            temp[1] = notes;

            if (temp[0].Length == temp[1].Length)
            {
                result.Add(" public  class " + model.GetType().Name + "_Note");
                result.Add("{");


                //声明成员
                for (int i = 0; i < temp[0].Length; i++)
                {
                    result.Add("  public string " + temp[0][i] + " { get; set; }");
                }
                //构造函数
                result.Add("public " + model.GetType().Name + "_Note()");
                result.Add("   {");
                for (int i = 0; i < temp[0].Length; i++)
                {
                    result.Add(" " + temp[0][i] + " = \"" + temp[1][i] + "\";");
                }
                result.Add("   }");
                //注释
                string noteStr = "{";
                for (int i = 0; i < notes.Length; i++)
                {
                    noteStr += "\"" + notes[i] + "\",";
                }

                result.Add("//PageHelper.GetModelNote<" + model.GetType().Name + ">(new " + model.GetType().Name + "(),new string[]" + noteStr.Substring(0, noteStr.Length - 1) + "});");
                result.Add("}");
            }
            else
            {
                result.Add("参数个数不匹配！");
            }


            return result;
        }

        public static string ToJsArray(this string[] array)
        {
            if (array!=null)
            {
                var result = array.Aggregate("new Array(", (current, item) => current + ("\"" + item + "\","));
                return result.Substring(0, result.Length - 1) + ")";
            }
            else
            {
                return "new Array()";
            }
        
            
        }
        #region 权限管理菜单生成
        public static string getMenuHead()
        {
            return "<dd>";     
        }
        public static string getTitle(string title)
        {
            return "<div class='title'><span><img src='/Areas/Platform/Content/Admin/images/leftico01.png' /></span>" + title + "</div>";
        }
        public static string getItemHead()
        {
            return "<ul class='menuson'>";
        }
        public static string getItem(string url,string name,string pageId)
        {
            return "<li><cite></cite><a href='" + url + "?pageId=" + pageId + "' target='rightFrame'>" + name + "</a><i></i></li>";
        }
        public static string getItemFoot()
        {
            return "</ul>";
        }
        public static string getMenuFoot()
        {
            return "</dd>";
        }
     
        #endregion
    }
}
       