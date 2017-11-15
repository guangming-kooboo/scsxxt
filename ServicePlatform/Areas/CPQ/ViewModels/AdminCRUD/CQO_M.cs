using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qx.CPQ.Entity;
using System.ComponentModel.DataAnnotations;
using Qx.CPQ.Services;

namespace ServicePlatform.Areas.CPQ.ViewModels.AdminCRUD
{
    public class CQO_M
    {
      
        public static CQO_M ToViewModel(List<CPQ_C_QuestionnaireDomain> QuestionnaireDomain,List<CPQ_C_Share> Share)
        {
            var model = new CQO_M();
            
            model._QuestionnaireDomainListItem = QuestionnaireDomain.Select(a => new SelectListItem()
            {
                Text = a.QuestionnaireDomainName,
                Value = a.QuestionnaireDomainID.ToString()
            }).ToList();
            model._ShareListItem = Share.Select(b => new SelectListItem()
            {
                Text = b.shareName,
                Value = b.shareID.ToString()
            }).ToList();
            return model;

        }

        public static QuestionnaireInfo ToModel(CQO_M cqo)
        {
            QuestionnaireInfo questionnaireInfo = new QuestionnaireInfo()
            {
                QuestionnaireDomain = cqo.QuestionnaireDomain,
                QuestionnaireName = cqo.QuestionnaireName,
                share = cqo.share,
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
            return questionnaireInfo;
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

        
       
        public List<SelectListItem> _QuestionnaireDomainListItem;
        public List<SelectListItem> _ShareListItem;
    }
}