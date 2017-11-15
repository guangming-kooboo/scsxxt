using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CPQ.Models
{
    public class QuestionOption
    {
        public int Sequence;
        public string OptionName;
        public string OptionID;
        public int selectedNumber;//选项被选择的个数
    }
}