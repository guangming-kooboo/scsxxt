using System.ComponentModel.DataAnnotations;
using Qx.Scsxxt.Core.Services;
using ServicePlatform.Controllers.Base;

namespace ServicePlatform.Areas.AppStudent.ViewModel.Home
{
    public class AddDiary_M : BaseViewModel
    {

        public static AddDiary_M ToViewModel( int DiaryCount)
        {
            return new AddDiary_M()
            {
               
                DiaryCount = DiaryCount + 1
            };
        }

        public int DiaryCount { get; set; }

        public Diary ToModel()
        {
            return new Diary()
            {
                PracticeNo = PracticeNo,
                RecordNo = RecordNo,
                RecordTitle = RecordTitle,
                RecordContent = RecordContent,
                RecordDate = RecordDate,
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
       
        public string Pic { get; set; }
    }
}