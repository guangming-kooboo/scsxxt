using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qx.Scsxxt.Core.Services;

namespace ServicePlatform.Areas.WbsHqu.ViewModels.FAQ
{
    public class AddFAQ_M
    {
        public static AddFAQ_M ToViewModel(List<SelectListItem> FAQtype, List<SelectListItem> ChilldFAQtype, string typeid)
        {
            return new AddFAQ_M()
            {
                _FAQTypeId= typeid,
                _FAQtype = FAQtype,
                _ChilldFAQtype= ChilldFAQtype,
            };
        }

        public List<SelectListItem> _ChilldFAQtype { get; set; }

        public List<SelectListItem> _FAQtype { get; set; }

        public Qx.Scsxxt.Core.Services.FAQ ToModel()
        {
            return new Qx.Scsxxt.Core.Services.FAQ()
            {
                Title= _Title,
                Contents= _Contents,
                FAQTypeId= _FAQTypeId,
                Video= _Video,
                CreatTime= _CreatTime,
                UserID= UserID,
                FAQID= _FAQID,
                StateID= _StateID,
                Abstract= Abstract,
                //Pic = Pic
            };
        }
        [Display(Name = "图片")]
        public string FAQpic { get; set; }
        public string Pic { get; set; }
        public string _FAQID { get; set; }
        public string _StateID { get; set; }
        [Display(Name = "标题")]
        public string _Title { get; set; }
        [Display(Name = "内容")]
        public string _Contents { get; set; }
        [Display(Name = "选择父级分类")]
        public string _FAQTypeId { get; set; }
        [Display(Name = "选择子级分类")]
        public string _ChildFAQTypeId { get; set; }
        [Display(Name = "视频链接")]
        public string _Video { get; set; }
        public DateTime _CreatTime { get; set; }
        public string UserID { get; set; }
        [Display(Name = "摘要")]
        public string Abstract { get; set; }
    }
}