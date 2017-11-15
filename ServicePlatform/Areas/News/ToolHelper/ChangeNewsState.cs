using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.News.ToolHelper
{
    public class ChangeNewsState
    {
        
        private ContentContext storeDB = new ContentContext();
        public string ChangeState(string str)
        {
            int NewsCount = storeDB.T_News.Count();
            int SearchIndex = 0;
            int[] StateIndex = new int[NewsCount];
            for (int i = 0; i < NewsCount; i++)
            {
                StateIndex[i] = str.IndexOf("FlowState", SearchIndex) + 11;
                SearchIndex = StateIndex[i];
                string targetchar = str.Substring(StateIndex[i], 1);
                if (targetchar == "1")
                {
                    string ReplaceChar = "待审核";
                    str = str.Remove(StateIndex[i], 1);
                    str = str.Insert(StateIndex[i], ReplaceChar);
                }
                if (targetchar == "2")
                {
                    string ReplaceChar = "通过";
                    str = str.Remove(StateIndex[i], 1);
                    str = str.Insert(StateIndex[i], ReplaceChar);
                }
                if (targetchar == "3")
                {
                    string ReplaceChar = "草稿箱";
                    str = str.Remove(StateIndex[i], 1);
                    str = str.Insert(StateIndex[i], ReplaceChar);
                }
            }
            return str;
        }
    }
}