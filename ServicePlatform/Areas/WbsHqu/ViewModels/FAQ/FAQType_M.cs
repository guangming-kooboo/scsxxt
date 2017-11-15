using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Core.Services.Entity;

namespace ServicePlatform.Areas.WbsHqu.ViewModels.FAQ
{
    public class FAQType_M
    {

        public static FAQType_M ToViewModel(string name,string id,string fatherId)
        {
            return new FAQType_M() { _FAQTypeId=id,_TypeName =name, fatherId = fatherId};

        }
        public C_FAQType ToModel()
        {
            return new C_FAQType()
            {
                FAQTypeId= _FAQTypeId,
                TypeName= _TypeName,
                FatherID = fatherId
            };
        }

        public string fatherId { get; set; }
        public string _FAQTypeId { get; set; }
        [Display(Name="类型名称")]
        public string _TypeName { get; set; }
    }

}