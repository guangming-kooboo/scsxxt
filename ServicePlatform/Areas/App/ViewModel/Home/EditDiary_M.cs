using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class EditDiary_M
    {
        public static EditDiary_M ToViewModel(Diary diary, int DiaryCount)
        {
            return new EditDiary_M()
            {
                PracticeNo=diary.PracticeNo,
                RecordNo=diary.RecordNo,
                RecordTitle=diary.RecordTitle,
                RecordContent=diary.RecordContent,
                Pic = diary.Pic,
                DiaryCount= DiaryCount
                //  RecordDate=diary.RecordDate
            };
        }

        public int DiaryCount { get; set; }

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