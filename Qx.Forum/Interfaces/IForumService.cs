using System.Collections.Generic;
using Qx.Community.Models;

namespace Qx.Community.Interfaces
{
    public interface IForumService
    {
        List<Forum> GetPopularForum();
        List<Forum> GetForums();
        Forum GetForum(string id);
        Column GetColumn(string columId);
        List<Column> GetColumns(string keyword);
        List<Column> GetColumnsByForumID(string id);
        List<Zone> GetZone();
    }
}