using System.ComponentModel.DataAnnotations;
using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class EditDiary_M : BaseViewModel
    {
        public int _careerstatus;

        public static EditDiary_M ToViewModel(int careerstatus,Diary diary, int DiaryCount)
        {
            return new EditDiary_M()
            {
                _careerstatus= careerstatus,
                PracticeNo =diary.PracticeNo,
                RecordNo=diary.RecordNo,
                RecordTitle=diary.RecordTitle,
                RecordContent=diary.RecordContent,
                Pic = diary.Pic,
                DiaryCount= DiaryCount
                //  RecordDate=diary.RecordDate
            };
        }

        public int DiaryCount { get; set; }
        public int id { get; set; }

        public Diary ToModel()
        {
            return new Diary()
            {
                PracticeNo=PracticeNo,
                RecordNo=RecordNo,
                RecordTitle=RecordTitle,
                RecordContent=RecordContent,
                RecordDate=RecordDate,
                Pic = Pic
            };
        }
        public string PracticeNo { get; set; }
        [Required]
        [Display(Name = "周记id")]
        public int RecordNo { get; set; }

        public int RecordDate { get; set; }

        [Required(ErrorMessage = "请填写周记标题")]
        [Display(Name = "周记题目")]
        public string RecordTitle { get; set; }

        [Required(ErrorMessage = "请填写周记内容")]
        [Display(Name = "周记内容")]
        public string RecordContent { get; set; }
        public string Uid { get; set; }
        public string Pic { get; set; }
    }
}