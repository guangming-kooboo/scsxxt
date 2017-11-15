using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Qx.Tools
{
    /// <summary>
    /// 用于加密字符串的类
    /// 其中包括可逆的方法和不可逆的方法（MD5）
    /// </summary>
    public static class EncryptUtility
    {
        private static string encryptKey = "CSTV";    //定义密钥  必须四个字符,否则会出错

        #region 加密字符串
        /// <summary> /// 加密字符串   
        /// </summary>  
        /// <param name="str">要加密的字符串</param>  
        /// <returns>加密后的字符串</returns>  
        public static string Encrypt(string str)
        {
            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();   //实例化加/解密类对象   

            byte[] key = Encoding.Unicode.GetBytes(encryptKey); //定义字节数组，用来存储密钥    

            byte[] data = Encoding.Unicode.GetBytes(str);//定义字节数组，用来存储要加密的字符串  

            MemoryStream MStream = new MemoryStream(); //实例化内存流对象      

            //使用内存流实例化加密流对象   
            CryptoStream CStream = new CryptoStream(MStream, descsp.CreateEncryptor(key, key), CryptoStreamMode.Write);

            CStream.Write(data, 0, data.Length);  //向加密流中写入数据      

            CStream.FlushFinalBlock();              //释放加密流      

            return Convert.ToBase64String(MStream.ToArray());//返回加密后的字符串  
        }
        #endregion

        #region 解密字符串
        /// <summary>  
        /// 解密字符串   
        /// </summary>  
        /// <param name="str">要解密的字符串</param>  
        /// <returns>解密后的字符串</returns>  
        public static string Decrypt(string str)
        {
            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();   //实例化加/解密类对象    

            byte[] key = Encoding.Unicode.GetBytes(encryptKey); //定义字节数组，用来存储密钥    

            byte[] data = Convert.FromBase64String(str);//定义字节数组，用来存储要解密的字符串  

            MemoryStream MStream = new MemoryStream(); //实例化内存流对象      

            //使用内存流实例化解密流对象       
            CryptoStream CStream = new CryptoStream(MStream, descsp.CreateDecryptor(key, key), CryptoStreamMode.Write);

            CStream.Write(data, 0, data.Length);      //向解密流中写入数据     

            CStream.FlushFinalBlock();               //释放解密流      

            return Encoding.Unicode.GetString(MStream.ToArray());       //返回解密后的字符串  
        }
        #endregion
        #region 不可逆的加密
        /// <summary>
        /// 对字符串进行加密（不可逆）
        /// </summary>
        /// <param name="Password">要加密的字符串</param>
        /// <param name="Format">加密方式,0 is SHA1,1 is MD5</param>
        /// <returns></returns>
        public static string NoneEncrypt(string strPwd)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = Encoding.Default.GetBytes(strPwd);
            byte[] md5data = md5.ComputeHash(data);
            md5.Clear();
            string str = "";
            for (int i = 0; i < md5data.Length; i++)
            {
                str += md5data[i].ToString("x").PadLeft(2, '0');
            }
            return str;
        }
        #endregion






        #region 字符串处理 
        public static List<string> UnPackString(string srcString, char flag = ';')
        {
            //关于控制检测
            //若数据库 存在该行，但是该列为空，此时读出的字符串 ""
            //若数据库 不存在该行 此时读出的字符串 null

            if (srcString != ""&&srcString != null)//是否为空 既然能传入肯定不是null
            {
                srcString.Trim();//清除空格
                 List<string>temp=srcString.Split(flag).ToList<string>();    //拆分成数组  
                if(temp[temp.Count-1]=="")
                {
                   temp.RemoveAt(temp.Count - 1);
                }
                return temp;
            }
            else
            {
                return new List<string>(0);
            }
        }

        public static string PackString(List<string> srcList, char flag = ';')
        {
            string tempString = "";

            if (srcList.Count > 0)//是否为空,只处理非空
            {
                for (int l = 0; l < srcList.Count; l++)
                {
                    tempString += srcList[l];//构造认证字符串
                    if (l < srcList.Count - 1)
                    {
                        tempString += flag;
                    }
                }
            }

            return tempString;
        }

        #endregion


    public static DataTable table()
    {
        // 会员ID      申请审核类型       操作
        //
        //
        //
        //
        DataTable dt = new DataTable("CheckTable");
        DataColumn dc1 = new DataColumn("id", Type.GetType("System.Int16"));
        DataColumn dc2 = new DataColumn("type", Type.GetType("System.String"));
        dt.Columns.Add(dc1);
        dt.Columns.Add(dc2);
        //以上代码完成了DataTable的构架，但是里面是没有任何数据的
    
        DataRow dr = dt.NewRow();
        dr["type"] = "娃娃";
        dr["id"] = 10;    
        

        return dt;
    }
    }
}