using System.Collections.Generic;
using Qx.Community.Interfaces;
using Qx.Community.Models;

namespace ServicePlatform.Areas.BBS.ViewModels.Home
{
    //版块
    public class Main_M
    {

        public static Main_M ToViewModel(List<Zone> Zones,string id)
        {
            return new Main_M() { Zones = Zones ,userid=id};
        }

        public List<Zone> Zones;
        public string userid;
      

    }
}