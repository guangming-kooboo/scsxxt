using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Entity.ViewModels
{
    public class V_UnitCollection
    {
        public  IEnumerable<T_Enterprise> EnterpriseList { get; set; }
        public IEnumerable<T_School> SchoolList { get; set; }
    }
}
