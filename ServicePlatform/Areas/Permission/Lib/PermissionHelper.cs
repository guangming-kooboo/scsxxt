using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ServicePlatform.Areas.Permission.Lib
{
    public static class PermissionHelper
    {
        public static string[] GetProperties(string objName, string nameSpace)
        {
            return GetProperties(nameSpace + "." + objName);
        }
        public static string[] GetProperties(string objName)
        {
            var p = Assembly.GetExecutingAssembly().CreateInstance(objName).GetType().GetProperties();
            string[] result = new string[p.Length];
            for (int i = 0; i < p.Length; i++)
            {
                result[i] = p[i].Name;

            }
            return result;
        }
        public static string[][] GetInfo(object o)
        {
            var t = o.GetType();
            var p = t.GetProperties();
            string[] Properties = new string[p.Length];
            string[] Values = new string[p.Length];
            for (int i = 0; i < p.Length; i++)
            {
                Properties[i] = p[i].Name;
                Values[i] = t.GetProperty(p[i].Name).GetValue(o).ToString();
            }
            string[][] result = new string[][] { Properties, Values };

            return result;
        }
    }


}