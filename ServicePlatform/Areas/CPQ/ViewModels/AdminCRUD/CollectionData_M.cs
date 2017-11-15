using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CPQ.ViewModels.AdminCRUD
{
    public class CollectionData_M
    {
        public static CollectionData_M ToViewModel(string AnswerAdress, string QuestionnaireID, int CollectedSetting)
        {
            return new CollectionData_M()
            {

                AnswerAdress = AnswerAdress,
                QuestionnaireID = QuestionnaireID,
                CollectedSetting = CollectedSetting
            };
        }
       public int CollectedSetting;
       public string AnswerAdress;
       public string QuestionnaireID;
    }
}