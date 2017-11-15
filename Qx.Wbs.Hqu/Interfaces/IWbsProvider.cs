using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qx.Wbs.Hqu.Interfaces
{
    public interface IWbsProvider
    {
        bool QuotaHasChild(string quotataskId);
        bool HasQuantifyChild(string wbstaskId);
        bool HasQuotaChild(string wbstaskId);
        bool CopyWbs(string wbstaskId);
    }
}
