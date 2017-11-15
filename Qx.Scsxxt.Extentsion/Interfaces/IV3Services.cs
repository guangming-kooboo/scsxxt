using Qx.Scsxxt.Extentsion.Entity;
using Qx.Scsxxt.Extentsion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qx.Scsxxt.Extentsion.Interfaces
{
    public interface IV3Services
    {
        string get_PracticeNo(string userId);
        bool get_stuscoreapply(string prano);
        bool AddStuScoreRecord(T_StuScoreApply model);
        bool BackFillRecord(string prano, double score);
        List<Rank> GetEntRank(string EntCategoryID);
        bool PassOrNot(T_StuScoreApply model);
        bool BackState(string practiceno);
    }
}
