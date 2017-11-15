using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.News.ToolHelper
{
    public class JsonSerializeHelper
    {
        /// <summary> 
        /// 把object对象序列化成json字符串 
        /// </summary> 
        /// <param name="obj">序列话的实例</param> 
        /// <returns>序列化json字符串</returns> 
        public static string SerializeToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary> 
        /// 把json字符串反序列化成Object对象 
        /// </summary> 
        /// <param name="json">json字符串</param> 
        /// <returns>对象实例</returns> 
        public static Object DeserializeFromJson(string json)
        {
            return JsonConvert.DeserializeObject(json);
        }

        /// <summary> 
        /// 把json字符串反序列化成泛型T 
        /// </summary> 
        /// <typeparam name="T">泛型</typeparam> 
        /// <param name="json">json字符串</param> 
        /// <returns>泛型T</returns> 
        public static T DeserializeFromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}