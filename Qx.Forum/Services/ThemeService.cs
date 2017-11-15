using System;
using System.Collections.Generic;
using System.Linq;
using Qx.Community.Interfaces;
using Qx.Community.Models;

namespace Qx.Community.Services
{
    public class ThemeService : BaseService, IThemeService
    {
        //获取主题 返回值为Theme
        public List<Theme> GetTheme(string columnId)
        {

            var theme = Db.BBS_Theme.Where(a => a.ColumnID == columnId).Select(b => new Theme()
            {
                 ColumnID=b.ColumnID,
                ThemeName = b.ThemeName,
                ID = b.ThemeID.ToString()
            }).ToList();

            return theme;
        }
        public string GetThemeID(string themename)
        {
            string theme = Db.BBS_Theme.Where(a => a.ThemeName == themename).Select(b => b.ThemeID).FirstOrDefault();
            return theme;
        }
        public Theme GetThemeByThemeID(string themeid)
        {
            var theme = Db.BBS_Theme.Where(a => a.ThemeID == themeid).Select(b => new Theme()
            {
                ThemeName = b.ThemeName,
                ThemeExplain = b.ThemeExplain,
                ID = b.ThemeID,
                ColumnID = b.ColumnID
            }).FirstOrDefault();
            return theme;
        }
        //获取主题下面的帖子数  返回值为int
        public int PostCount()
        {
            int PostCount = new Random().Next(0, 10000);
            return PostCount;
        }
        //获取主题下面的回帖数  返回值为int
        public int ReplyCount()
        {
            int ReplyCount =  new Random().Next(0,10000);
            return ReplyCount;
        }
    }
}