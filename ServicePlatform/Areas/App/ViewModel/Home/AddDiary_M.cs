using Qx.Scsxxt.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.App.ViewModel.Home
{
    public class AddDiary_M
    {

        public static AddDiary_M ToViewModel(string uid,int DiaryCount)
        {
            return new AddDiary_M()
            {
                Uid=uid,
                DiaryCount= DiaryCount+1
            };
        }

        public int DiaryCount { get; set; }

        public Diary ToModel()
        {
            return new Diary()
            {
                PracticeNo= PracticeNo,
                RecordNo = RecordNo,
                RecordTitle = RecordTitle,
                RecordContent=RecordContent,
                RecordDate=RecordDate,       
                Pic =Pic
            };
        }

        public string PracticeNo { get; set; }

        [Required]
        [Display(Name = "周记id")]
        public int RecordNo { get; set; }

        public int RecordDate { get; set; }

        [Required(ErrorMessage = "请填写周记标题")]
        [Display(Name ="周记题目")]
        public string RecordTitle { get; set; }

        [Required(ErrorMessage = "请填写周记内容")]
        [Display(Name ="周记内容")]
        public string RecordContent { get; set; }
        public string Uid { get; set; }
        public string Pic { get; set; }
    }
}