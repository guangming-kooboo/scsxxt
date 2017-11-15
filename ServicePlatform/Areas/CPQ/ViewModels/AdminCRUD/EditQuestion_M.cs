
using Qx.CPQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CPQ.ViewModels.AdminCRUD
{
    public class EditQuestion_M
    {


        public static EditQuestion_M ToViewModel(Question Question)
        {
            return new EditQuestion_M()
            {

                Question = Question,

            };
        }
        public Question Question;
    }
}