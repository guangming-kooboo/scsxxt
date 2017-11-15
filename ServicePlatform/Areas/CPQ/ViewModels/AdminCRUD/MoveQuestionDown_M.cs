using Qx.CPQ.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CPQ.ViewModels.AdminCRUD
{
    public class MoveQuestionDown_M
    {

        public static CPQ_T_AttachQuestionToQuestionnaire ToModel(CPQ_T_AttachQuestionToQuestionnaire at, int sequence)
        {
            at.QuestionSequenceID = sequence;
            return at;
        }


        [Key]
        [StringLength(70)]
        public string AttachID;

        [Required]
        [StringLength(50)]
        public string QuestionnaireID;

        [Required]
        [StringLength(50)]
        public string QuestionID;

        public int QuestionSequenceID;
    }
}