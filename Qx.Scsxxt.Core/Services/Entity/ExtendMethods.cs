using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Qx.Tools.CommonExtendMethods;

namespace Core.Services.Entity
{
    //#region ExtendMethods
    //public static class ExtendMethods
    //{
    //    public static int Random(this DateTime time)
    //    {
    //        return CommonExtendMethods.Random(time);
    //    }
    //    public static string TimeToNow(this DateTime time)
    //    {
    //        return CommonExtendMethods.TimeToNow(time);
    //    }
    //    public static string ToStr(this DateTime time, bool showToNow = false)
    //    {
    //        return CommonExtendMethods.ToStr(time, showToNow);
            
    //    }
    //    public static List<T> Grap<T>(this List<T> data, int start, int end)
    //    {
    //        return CommonExtendMethods.Grap<T>(data, start, end);
    //    }
    //    public static List<SelectListItem> Format(this List<SelectListItem> items,string selectedValue)
    //    {
    //        return CommonExtendMethods.Format(items, selectedValue);
    //    }
    //    public static bool IsSame<T, K>(this object obj)
    //    {
    //        return CommonExtendMethods.IsSame<T, K>(obj);
            
    //    }
    //    public static DateTime ToDateTime(this int time)
    //    {
    //        return CommonExtendMethods.ToDateTime(time);
          
    //    }
    //    public static int ToInt(this DateTime time)
    //    {
    //        return CommonExtendMethods.ToInt(time);
          
           
    //    }
    //    public static string ToTimeStr(this DateTime time)
    //    {
    //        return CommonExtendMethods.ToTimeStr(time);
    //    }

    //    #region 字符串处理 
    //    public static List<string> UnPackString(this string srcString, char flag = ';')
    //    {
    //        return CommonExtendMethods.UnPackString(srcString, flag);
    
    //    }

    //    public static string PackString(this List<string> srcList, char flag = ';')
    //    {
    //        return CommonExtendMethods.PackString(srcList, flag);
    //    }
    //    public static string[] FormatString(this List<string> srcString, char flag = '%')
    //    {
    //        return CommonExtendMethods.FormatString(srcString, flag);
    //     }
    //    #endregion

    //}

    //#region 快速创建 IEqualityComparer<T> 的实例
    ////var equalityComparer1 = Equality<Person>.CreateComparer(p => p.ID);
    //public static class Equality<T>
    //{
    //    public static IEqualityComparer<T> CreateComparer<V>(Func<T, V> keySelector)
    //    {
    //        return new CommonEqualityComparer<V>(keySelector);
    //    }
    //    public static IEqualityComparer<T> CreateComparer<V>(Func<T, V> keySelector, IEqualityComparer<V> comparer)
    //    {
    //        return new CommonEqualityComparer<V>(keySelector, comparer);
    //    }

    //    class CommonEqualityComparer<V> : IEqualityComparer<T>
    //    {
    //        private Func<T, V> keySelector;
    //        private IEqualityComparer<V> comparer;

    //        public CommonEqualityComparer(Func<T, V> keySelector, IEqualityComparer<V> comparer)
    //        {
    //            this.keySelector = keySelector;
    //            this.comparer = comparer;
    //        }
    //        public CommonEqualityComparer(Func<T, V> keySelector)
    //            : this(keySelector, EqualityComparer<V>.Default)
    //        { }

    //        public bool Equals(T x, T y)
    //        {
    //            return comparer.Equals(keySelector(x), keySelector(y));
    //        }
    //        public int GetHashCode(T obj)
    //        {
    //            return comparer.GetHashCode(keySelector(obj));
    //        }
    //    }
    //}
    //#endregion

    



    //#endregion
}
