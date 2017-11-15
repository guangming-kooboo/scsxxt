using Qx.CPQ.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CPQ.ViewModels.AdminCRUD
{
    public class CancelPublish_M
    {
        public static CPQ_T_Questionnaire ToModel(CPQ_T_Questionnaire Questionnaire, int status)
        {
            Questionnaire.Status = status;
            
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
    }
}