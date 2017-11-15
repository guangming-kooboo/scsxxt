using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qx.Wbs.Models
{
      public class ArangeDetail
    {
        public string SerialID { get; set; }


        public string UserID { get; set; }

        public double ?UserWeight { get; set; }

        public double ?RealWeight { get; set; }


        public string Materal { get; set; }


        public DateTime ?FinishTime { get; set; }
        public string NodeID { get; set; }

    
        public string NodeName { get; set; }

    
        public string FatherNodeID { get; set; }



        public string Decription { get; set; }

        public double? NodeWeight { get; set; }

      
        public string Owner { get; set; }

        
        public DateTime? BeginTime { get; set; }

       
        public DateTime? EndTime { get; set; }

 
        public string Place { get; set; }

        public string UserName;
    }
}
