using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class Main_M
    {
        public string Uid { get; set; }
        public int Count { get; set; }
        public static Main_M ToViewModel(string uid,int count,int EntCount)
        {
            return new Main_M()
            {
                Uid = uid,
                EntCount= EntCount,
                Count = count
            };
        }

        public int EntCount { get; set; }
    }
}