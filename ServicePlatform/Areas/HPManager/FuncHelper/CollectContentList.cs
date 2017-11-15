using ServicePlatform.Lib;
using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.HPManager.FuncHelper
{
    public static class CollectContentList
    {
        public static List<T_Content> Collect(int ColumnId, int Type)
        { 
            ContentContext store = new ContentContext();
            int ContentFromId = 0;
            //int ContentFromId =Convert.ToInt32(from a in store.T_HomePageColumn where (a.HPColumnID == ColumnId) select a.ContentFrom);
            var GetModel = from a in store.T_HomePageColumn where (a.HPColumnID == ColumnId) select a;
            foreach (var m in GetModel)
            {
                ContentFromId = m.ContentFrom;
            }

            var TargetContent=(from a in store.T_HomePageContent from b in store.T_Content where(a.HPColID==ColumnId && a.HPCID==b.ContentID) select b).ToList();
            if (ContentFromId!=0)
            {
                //表示该栏目内的栏目有来自C_ConetntColumn表的约束，栏目中显示C_ConetntColumn表中该栏目的所有内容
                if (Type == ContentType.News)
                {
                    //内容类型为新闻
                    TargetContent = (from a in store.T_Content from b in store.T_News where (a.ContentID == b.NewsID && b.NewsColumnID == ContentFromId) select a).ToList();
                }

                /*以下还需要添加广告、图章、签名等的逻辑判断*/
            }
            return TargetContent;
        }

        public static List<T_Content> Collect2(string ColumnName, int Type,string theUnitID)
        {
            ContentContext store = new ContentContext();
            int ColumnId = 0;
            var GetAim = from c in store.T_HomePageColumn where (c.HPColumnName == ColumnName && c.UnitID == theUnitID) select c;
            if (GetAim!=null)
            {
                foreach (var m in GetAim)
                {
                    ColumnId = m.HPColumnID;
                }
            }

            int ContentFromId = 0;
            //int ContentFromId =Convert.ToInt32(from a in store.T_HomePageColumn where (a.HPColumnID == ColumnId) select a.ContentFrom);
            var GetModel = from a in store.T_HomePageColumn where (a.HPColumnID == ColumnId && a.UnitID == theUnitID) select a;
            foreach (var m in GetModel)
            {
                ContentFromId = m.ContentFrom;
            }

            var TargetContent = (from a in store.T_HomePageContent from b in store.T_Content where (a.HPColID == ColumnId && a.HPCID == b.ContentID && b.UnitID == theUnitID) select b).ToList();
            if (ContentFromId != 0)
            {
                //表示该栏目内的栏目有来自C_ConetntColumn表的约束，栏目中显示C_ConetntColumn表中该栏目的所有内容
                if (Type == ContentType.News)
                {
                    //内容类型为新闻
                    TargetContent = (from a in store.T_Content from b in store.T_News where (a.ContentID == b.NewsID && b.NewsColumnID == ContentFromId && a.UnitID == theUnitID) select a).ToList();
                }
                if (Type == ContentType.DownLoadFile)
                {
                    //内容类型为新闻
                    TargetContent = (from a in store.T_Content from b in store.T_DownLoadFiles where (a.ContentID == b.DLFileID && b.DLFileColumnID == ContentFromId && a.UnitID == theUnitID) select a).ToList();
                }

                /*以下还需要添加广告、图章、签名等的逻辑判断*/
            }
            return TargetContent;
        }

    }
}