﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.BBS.ViewModels.Home
{
    public class _SearchLeft_M
    {
        public static _SearchLeft_M ToViewModel(string keyword, string type)
        {
            return new _SearchLeft_M() { keyword = keyword, type = type };
        }
        public string keyword;
        public string type;
    }
}