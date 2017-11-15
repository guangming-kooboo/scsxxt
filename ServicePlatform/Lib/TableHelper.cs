using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Lib
{
    public class TableHelper
    {
        private string TableID;
        private string[] tableTh;//获取模型属性列       
        private string[][] dataList;//获取数据列

        public TableHelper(string tbid,string[] tbth,string[][]data)
        {
            TableID = tbid;
            tableTh = tbth;
            dataList = data;
        }

        public string GettableID()
        {
            return TableID;
        }

        public string[] GettableTh()
        {
            return tableTh;
        }

        public string[][] GetdataList()
        {
            return dataList;
        }
    }
}