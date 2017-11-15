using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicePlatform.Models
{
    public class PracBatch_M
    {
        [Display(Name = "实习批次ID")]
        public string PracBatchID { set; get; }

        [Display(Name = "批次ID")]
        public string BatchID { set; get; }

        [Display(Name = "学校名")]
        public string SchoolID { set; get; }

        [Display(Name = "批次名")]
        public string BatchName { set; get; }

        [Display(Name = "起止日期")]
        public string StartEnd { set; get; }

        [Display(Name = "是否当前批次")]
        public string IsCurrentBatch { set; get; }

        [Display(Name = "是否生效")]
        public int IsActive { set; get; }

        [Display(Name = "评分比重")]
        public int SchoolReviewWeight { set; get; }

        public List<SelectListItem> iscurrentItems = new List<SelectListItem>()
        {
            new SelectListItem() {Text = "是", Value = "是", Selected = true},
            new SelectListItem() {Text = "否", Value = "否"}
        };
        public List<SelectListItem> isactiveItems = new List<SelectListItem>()
        {
            new SelectListItem() {Text = "是", Value = "1", Selected = true},
            new SelectListItem() {Text = "否", Value = "0"}
        };

        public static PracBatch_M ToViewModel(T_PracBatch batch)
        {
            return new PracBatch_M()
            {
                BatchID = batch.BatchID,
                PracBatchID = batch.PracBatchID,
                SchoolID = batch.SchoolID,
                BatchName = batch.BatchName,
                IsCurrentBatch = batch.IsCurrentBatch,
                IsActive = batch.IsActive,             
                StartEnd = batch.StartEnd,
                SchoolReviewWeight = batch.SchoolReviewWeight
            };
        }
    }
}