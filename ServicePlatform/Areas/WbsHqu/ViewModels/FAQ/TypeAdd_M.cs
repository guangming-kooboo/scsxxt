using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Core.Services.Entity;

namespace ServicePlatform.Areas.WbsHqu.ViewModels.FAQ
{
    public class TypeAdd_M
    {
        public static TypeAdd_M ToViewModel(string name, string id)
        {
            return new TypeAdd_M() { _FAQTypeId = id, _TypeName = name };

        }
        public C_FAQType ToModel()
        {
            return new C_FAQType()
            {
                FAQTypeId = _FAQTypeId,
                TypeName = _TypeName
            };
        }
        public string _FAQTypeId { get; set; }
        [Display(Name = "类型名称")]
        public string _TypeName { get; set; }
    }
}