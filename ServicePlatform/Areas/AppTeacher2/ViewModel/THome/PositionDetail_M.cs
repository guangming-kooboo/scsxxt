using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Model;

namespace ServicePlatform.Areas.AppTeacher2.ViewModel.THome
{
    public class PositionDetail_M
    {
        public static PositionDetail_M ToViewModel(string Ent_No, List<Position> PositionList)
        {
            return new PositionDetail_M()
            {
                Ent_No=Ent_No,
                _PositionList= PositionList
            };
        }

        public List<Position> _PositionList { get; set; }

        public string Ent_No { get; set; }
    }
}