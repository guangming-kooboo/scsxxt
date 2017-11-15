using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Model;

namespace ServicePlatform.Areas.AppTeacher2.ViewModel.THome
{
    public class RecEnt_Staff_M
    {
        public static RecEnt_Staff_M ToViewModel(string Ent_No, List<DownLoadFiles> Files)
        {
            return new RecEnt_Staff_M()
            {
                Ent_No = Ent_No,
                _Files = Files
            };
        }

        public List<DownLoadFiles> _Files { get; set; }

        public string Ent_No { get; set; }
    }
}