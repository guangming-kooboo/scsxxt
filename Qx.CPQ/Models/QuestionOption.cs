using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Qx.CPQ.Models
{
    public class QuestionOption
    {
        public int Sequence;
        public string OptionName;
        public string OptionID;
        public int selectedNumber;//选项被选择的个数

        public void ToModel(out string type, out int type2)
        {
            type = "1"; type2 = 2;
        }
      
      
    }
}