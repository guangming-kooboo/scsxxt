using Core.Services.Entity;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class Main_M : BaseViewModel
    {
        public int _careerstatus;
        public string Uid { get; set; }
        public int _Count { get; set; }
        public static Main_M ToViewModel(int careerstatus,string uid, T_StuBatchReg AllComment,int count,int EntCount,int EvaluateSchoolCount,int EvaluateEntCount,int DiaryCount,int CaseCount)
        {
            return new Main_M()
            {
                Uid = uid,
                _careerstatus= careerstatus,
                _AllComment = AllComment,
                _EntCount = EntCount,
                _Count = count,
                _EvaluateSchoolCount= EvaluateSchoolCount,
                _EvaluateEntCount= EvaluateEntCount,
                _DiaryCount= DiaryCount,
                _CaseCount= CaseCount
            };
        }

        public T_StuBatchReg _AllComment { get; set; }

        public int _CaseCount { get; set; }

        public int _DiaryCount { get; set; }

        public int _EvaluateSchoolCount { get; set; }

        public int _EvaluateEntCount { get; set; }

        public int _EntCount { get; set; }
    }
}