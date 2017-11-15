using Qx.CPQ.Entity;
using Qx.CPQ.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Areas.CPQ.ViewModels.AdminCRUD
{
    public class AddQuestionSome_M
    {

        public static AddQuestionSome_M ToViewModel(List<CPQ_C_QuestionDomain> QuestionDomain, List<CPQ_C_QuestionType> QuestionType,string QuestionnaireID,int share)
        {
            var model = new AddQuestionSome_M();

            model.QuestionDomainListItem = QuestionDomain.Select(a => new SelectListItem()
            {
                Text = a.QuestionDomainName,
                Value = a.QuestionDomainID.ToString()
            }).ToList();
            model.QuestionTypeListItem = QuestionType.Select(b => new SelectListItem()
            {
                Text = b.QuestionTypeName,
                Value = b.QuestionTypeID.ToString()
            }).ToList();

            model.QuestionnaireID = QuestionnaireID;
            model.share = share;
            return model;

        }

        //public static CPQ_T_Question ToModel(CPQ_T_Question Question, int status)
        //{
        //    Question.Status = status;

        //    return Question;
        //}
        public static QuestionInfo ToModel(AddQuestionSome_M aq)
        {
            QuestionInfo questionInfo = new QuestionInfo
            {
                QuestionDomain = aq.QuestionDomain,
                share = aq.share,
                QuestionID = aq.QuestionID,
                QuestionnaireID = aq.QuestionnaireID,
                QuestionName = aq.QuestionName,
                QuestionType = aq.QuestionType,
                Reference = 0 


            };
            return questionInfo;
        }

        [Key]
        [StringLength(50)]
        public string QuestionID;

        public int QuestionType;

        [Required]
        [StringLength(255)]
        public string QuestionName;
        public int QuestionDomain;

        public int share;//从上一级的问卷读取share，一样

        public int Reference;



        public string QuestionnaireID;//问卷ID是为了到时候往问卷问题表添加
           
         
        
        public List<SelectListItem> QuestionDomainListItem;
         public List<SelectListItem> QuestionTypeListItem;

    }
}