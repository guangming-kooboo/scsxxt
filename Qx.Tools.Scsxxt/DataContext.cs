﻿using System.Collections.Generic;

namespace Qx.Tools.Scsxxt
{
    public class DataContext
    {
        public string UserID { get; set; }
        public string UserName { get; set; }

        public int UserType { get; set; }
        public bool IsLogin { get; set; }
      
        public string UserUnit { get; set; }
    
       
        private Dictionary<string, object> UserData { get; set; }

        public void SetFiled(string key, object value)
        {
            if (UserData == null)
            {
                UserData = new Dictionary<string, object> {{key, value}};
            }
            else if (UserData.ContainsKey(key))
            {
                UserData[key] = value;
            }
            else
            {
                UserData.Add(key, value);
            }
        }

        public object GetFiled(string key)
        {
            if (UserData == null || !UserData.ContainsKey(key))
            {
                return null;
            }
            return UserData[key];
        }
    }
}