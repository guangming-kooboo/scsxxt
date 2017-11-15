using Qx.CPQ.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CPQ.ViewModels.AdminCRUD
{
    public class ReferenceQuestionnaire_M
    {

        public static CPQ_T_Questionnaire ToModel(CQO_M cqo, int share)
        {
            CPQ_T_Questionnaire Questionnaire = new CPQ_T_Questionnaire
            {
                QuestionnaireDomain = cqo.QuestionnaireDomain,
                QuestionnaireName = cqo.QuestionnaireName,
                share = share,
                Summarize = cqo.Summarize,
                Reference = cqo.Reference,
                Status = cqo.Status,
                CreateTime = cqo.CreateTime,
                OwnerID = cqo.OwnerID,
                OwnerCompany = cqo.OwnerCompany,
                QuestionnaireID = cqo.QuestionnaireID,
                CollectSeting = cqo.CollectSeting,
                CollectNumber = cqo.CollectNumber,
                IsLock = cqo.IsLock


            };
            return Questionnaire;
        }



        [Key]
        [StringLength(50)]
        public string QuestionnaireID;

        [Required]
        [StringLength(50)]
        public string QuestionnaireName;

        [Required]
        [StringLength(255)]
        public string Summarize;

        public int QuestionnaireDomain;

        public int CreateTime;

        [Required]
        [StringLength(20)]
        public string OwnerID;

        public int Status;

        [StringLength(50)]
        public string OwnerCompany;

        public int share;

        public int Reference;

        public int CollectSeting;

        public int CollectNumber;

        public int IsLock;


        //问题的字段
        [Key]
        [StringLength(50)]
        public string QuestionID;

        public int QuestionType;

        [Required]
        [StringLength(255)]
        public string QuestionName;

        public int QuestionDomain;

        public int Questionshare;

        public int QuestionReference { get; set; }

    }
}