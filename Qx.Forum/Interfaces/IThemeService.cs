using System.Collections.Generic;
using Qx.Community.Models;

namespace Qx.Community.Interfaces
{
    public interface IThemeService
    {
        List<Theme> GetTheme(string columnId);
        Theme GetThemeByThemeID(string themeid);
        int PostCount();
        int ReplyCount();
        string GetThemeID(string themename);
    }
}