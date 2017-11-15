using System.Collections.Generic;
using Qx.Community.Models;

namespace Qx.Community.Interfaces
{
    public interface IVisitorService
    {
        List<RecentVisitor> GetVisitor(string userid);
    }
}