using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qx.Scsxxt.Core.Services;

namespace ServicePlatform.Areas.WbsHqu.ViewModels.FAQ
{
    public class EditFaq_M
    {
        public string _FAQID { get; set; }
        public string _StateID { get; set; }
        [Display(Name = "标题")]
        public string _Title { get; set; }
        [Display(Name = "内容")]
        public string _Contents { get; set; }
        [Display(Name = "选择父级分类")]
        public string _FAQTypeId { get; set; }
        [Display(Name = "视频链接")]
        public string _Video { get; set; }
        public DateTime _CreatTime { get; set; }
        public string UserID { get; set; }
        [Display(Name = "摘要")]
        public string Abstract { get; set; }
        [Display(Name = "图片")]
        public string FAQpic { get; set; }
        public List<SelectListItem> _FAQtype { get; set; }
        public static EditFaq_M ToViewModel(Qx.Scsxxt.Core.Services.FAQ faq,string fathertypeid,string typeid, List<SelectListItem> FAQtype,List<SelectListItem> FAQChildType)
        {
            if (fathertypeid == "0")//就是他自己
            {
                return new EditFaq_M()
                {
                    _Title = faq.Title,
                    // _Contents = faq.Contents,
                    _Contents =faq.Contents,
                    _FAQTypeId =faq.FAQTypeId,
                    _Video = faq.Video,
                    _CreatTime = faq.CreatTime,
                    _FAQID = faq.FAQID,
                    _StateID = faq.StateID,
                    _FAQtype = FAQtype,
                    Abstract = faq.Abstract,
                    Pic = faq.Pic,
                   _ChildFAQTypeId = "0",
                    _ChilldFAQtype = FAQChildType,
                    fatherid= fathertypeid,
                    childid= typeid
                };
            }
            else
            {
                return new EditFaq_M()
                {
                    _Title = faq.Title,
                    _Contents = faq.Contents,
                    _FAQTypeId = fathertypeid,
                    _Video = faq.Video,
                    _CreatTime = faq.CreatTime,
                    _FAQID = faq.FAQID,
                    _StateID = faq.StateID,
                    _FAQtype = FAQtype,
                    Abstract = faq.Abstract,
                    Pic = faq.Pic,
                    _ChildFAQTypeId = faq.FAQTypeId,
                    _ChilldFAQtype = FAQChildType,
                    typeid = typeid,
                    fatherid = fathertypeid,
                    childid = typeid
                };
            }
        }

        public string childid { get; set; }

        public string fatherid { get; set; }

        public string typeid { get; set; }

        public string Pic { get; set; }

        public List<SelectListItem> _ChilldFAQtype { get; set; }
        [Display(Name = "选择子级分类")]
        public string _ChildFAQTypeId { get; set; }
        
        public Qx.Scsxxt.Core.Services.FAQ ToModel()
        {
            return new Qx.Scsxxt.Core.Services.FAQ()
            {
                Title = _Title,
                Contents = _Contents,
                FAQTypeId = _FAQTypeId,
                Video = _Video,
                CreatTime = _CreatTime,
                UserID = UserID,
                FAQID = _FAQID,
                StateID = _StateID,
                Abstract = Abstract,
                Pic = Pic
            };
        }
    }
}