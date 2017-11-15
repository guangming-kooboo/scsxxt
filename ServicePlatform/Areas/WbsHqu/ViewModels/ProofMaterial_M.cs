using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qx.Tools.CommonExtendMethods;

namespace ServicePlatform.Areas.WbsHqu.ViewModels
{
    public class ProofMaterial_M
    {
        public List<string> _contentArray
        {
            get { return _content.UnPackString(';'); }
        }

        public string _content { get; set; }
        public string note { get; set; }

        public static ProofMaterial_M ToViewModel(string content, string note)
        {
            return new ProofMaterial_M()
            {
                _content = content,
                note = note
            };
        }

        
    }
}