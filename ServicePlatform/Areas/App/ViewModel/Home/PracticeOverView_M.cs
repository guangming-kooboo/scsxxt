using Core.Services.Entity;
using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class PracticeOverView_M
    {
        public static PracticeOverView_M ToViewModel(
            string uid, 
            List<SelectListItem> YetSubmit,
            int EntCount,
            int FinalVolunteerCount,
            int PrepareVolunteerCount,
            int FormalVolunteerCount,
            Volunteer FormalVolunteer, 
            int CareerStatus, 
            int DiaryCount,
            int PracticeCaseCount,
            T_StuBatchReg AllComment,
            int EvaluateEntCount,
            List<StuEvaluateEnt> EvaluateEnt,
            List<StuEvaluateSchool> EvaluateSchool,
            List<JobWanted> StuFindEnt,
            List<JobWanted> EntFindStu
            )
        {
            return new PracticeOverView_M()
            {
                uid=uid,
                YetSubmit= YetSubmit,
                EntCount= EntCount,
                NoSubmit=EntCount-YetSubmit.Count,
                FormalVolunteer= FormalVolunteer,
                CareerStatus= CareerStatus,
                DiaryCount= DiaryCount,
                PrepareVolunteerCount= PrepareVolunteerCount,
                FormalVolunteerCount= FormalVolunteerCount,
                PracticeCaseCount= PracticeCaseCount,
                AllComment= AllComment,
                EvaluateEntCount= EvaluateEntCount,
                EvaluateEnt= EvaluateEnt,
                EvaluateSchool= EvaluateSchool,
                FinalVolunteerCount= FinalVolunteerCount,
                StuFindEnt= StuFindEnt,
                EntFindStu= EntFindStu
            };
        }

        public List<JobWanted> StuFindEnt { get; set; }

        public List<JobWanted> EntFindStu { get; set; }

        public static PracticeOverView_M ToViewModel(
           string uid,
           List<SelectListItem> YetSubmit,
           int EntCount,
           int FinalVolunteerCount,
           int PrepareVolunteerCount,
           int FormalVolunteerCount,
           int CareerStatus,
           int DiaryCount,
           int PracticeCaseCount,
           T_StuBatchReg AllComment,
           int EvaluateEntCount,
           List<StuEvaluateEnt> EvaluateEnt,
           List<StuEvaluateSchool> EvaluateSchool,
           List<JobWanted> StuFindEnt,
           List<JobWanted> EntFindStu
           )
        {
            return new PracticeOverView_M()
            {
                uid = uid,
                YetSubmit = YetSubmit,
                EntCount = EntCount,
                NoSubmit = EntCount - YetSubmit.Count,
                CareerStatus = CareerStatus,
                DiaryCount = DiaryCount,
                PrepareVolunteerCount = PrepareVolunteerCount,
                FormalVolunteerCount = FormalVolunteerCount,
                PracticeCaseCount = PracticeCaseCount,
                AllComment = AllComment,
                EvaluateEntCount = EvaluateEntCount,
                EvaluateEnt = EvaluateEnt,
                EvaluateSchool = EvaluateSchool,
                FinalVolunteerCount = FinalVolunteerCount,
                StuFindEnt = StuFindEnt,
                EntFindStu = EntFindStu
            };
        }

        public int FinalVolunteerCount { get; set; }

        public List<StuEvaluateEnt> EvaluateEnt { get; set; }

        public List<StuEvaluateSchool> EvaluateSchool { get; set; }

        public int EvaluateEntCount { get; set; }

        public T_StuBatchReg AllComment { get; set; }

        public int PracticeCaseCount { get; set; }

        public int PrepareVolunteerCount { get; set; }

        public int FormalVolunteerCount { get; set; }

        public int DiaryCount { get; set; }

        public int CareerStatus { get; set; }

        public Volunteer FormalVolunteer { get; set; }

        public int NoSubmit { get; set; }

        public int EntCount { get; set; }

        public List<SelectListItem> YetSubmit { get; set; }

        public string uid { get; set; }
    }
}