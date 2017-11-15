using Core.Services.Entity;
using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class IdentificationTable_M : BaseViewModel
    {
        public string _schoolname;
        public string _StuNo { get; set; }

        public string _StuName { get; set; }
        public string _StuSex { get; set; }
        public string _SpeName { get; set; }
        public int _EnteyYear { get; set; }
        public string _ClassName { get; set; }

        public T_StuBatchReg AllComment { get; set; }
        public UserInfo StuAllInfo { get; set; }

        public string _EntName { get; set; }
        public string _PositionName { get; set; }

        public int _EnterYear { get; set; }

        public string _StuClass { get; set; }
        public string uid { get; set; }

        public static IdentificationTable_M ToViewModel(string schoolname, Volunteer volunteer, UserInfo StuAllInfo, T_StuBatchReg AllComment)
        {
            if (volunteer == null)
            {
                return new IdentificationTable_M()
                {
                    _schoolname = schoolname,
                    _EntName = "暂无企业录用",
                    _PositionName = "暂无企业录用",
                    StuAllInfo = StuAllInfo,
                    AllComment = AllComment,
                    _StuName = StuAllInfo.Name,
                    _StuClass = StuAllInfo.ClassName,
                    _EnterYear = StuAllInfo.EntryYear,
                    _StuSex = StuAllInfo.Sex,
                    _SpeName = StuAllInfo.SpeName,
                    _ClassName = StuAllInfo.ClassName,
                    _StuNo = StuAllInfo.StuNO
                };
            }
            else
            {
                return new IdentificationTable_M()
                {
                    _schoolname = schoolname,
                    _EntName = volunteer.EntName,
                    _PositionName = volunteer.PositionName,
                    StuAllInfo = StuAllInfo,
                    AllComment = AllComment,
                    _StuName = StuAllInfo.Name,
                    _StuClass = StuAllInfo.ClassName,
                    _EnterYear = StuAllInfo.EntryYear,
                    _StuSex = StuAllInfo.Sex,
                    _SpeName = StuAllInfo.SpeName,
                    _ClassName = StuAllInfo.ClassName,
                    _StuNo = StuAllInfo.StuNO
                };
            }
        }
    }
}