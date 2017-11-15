using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Models.Template
{
    public class ItemList
    {
        public ItemList(string name,List<SelectListItem> items)
        {
            Items = items;
            Name = name;
        }

        public List<SelectListItem> Items { get; set; }
        public string Name { get; set; }
    }
}