using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServicePlatform.Models
{
    public class School
    {
    }
    
    [Table("C_NewsColumn")]
    public class C_NewsColumn
    {
        [Key]
        public int NewsColumnID { get; set; }
        public string NewsColumnName { get; set; }
    }

    [Table("C_NewsState")]
    public class C_NewsState
    {
        [Key]
        public int NewsStateID { get; set; }
        public string NewsStateName { get; set; }
    }

    [Table("C_NewsType")]
    public class C_NewsType
    {
        [Key]
        public int TypeID { get; set; }
        public string TypeName { get; set; }
    }

    public class C_Sex
    {
        public bool SexId { get; set; }//true or false
        public string SexName { get; set; }//男生or女生
    }



    

    //专业表
    [Table("C_Specialty")]
    public class C_Specialty
    {

        [Key, Column(Order = 0)]
        [Display(Name = "年级")]
        public int EntryYear { get; set; }
        [Key, Column(Order = 1)]
        [Display(Name = "专业编号")]
        public string SpeNo { get; set; }
        [Required(ErrorMessage = "专业名称是必填项")]
        public string SpeName { get; set; }

    }

    //班级表
    [Table("T_Class")]
    public class T_Class
    {
        [Key]
        public string ClassID { get; set; }
        [Required(ErrorMessage = "年级是必填项")]
        public int EntryYear { get; set; }
        [Required(ErrorMessage = "专业是必填项")]
        public string SpeNo { get; set; }
        [Required(ErrorMessage = "班级名称是必填项")]
        public string ClassName { get; set; }

        [NotMapped]
        [Display(Name = "专业名称")]
        public string SpeName
        {
            get
            {
                SchoolContext db = new SchoolDbContext();
                C_Specialty spe = db.C_Specialty.Find(this.EntryYear, this.SpeNo);
                if (spe != null)
                {
                    return spe.SpeName;
                }
                else
                {
                    return "";
                }
            }
        }
    }


 


     public class NewsIsLocked
    {
        public int IsLockID { get; set; }//1或-1
        public string IsLockName { get; set; }//锁定或不锁定
    }
}

 public class NewsIsShow
    {
        public int IsShowID { get; set; }//1或-1
        public string IsShowName { get; set; }//禁用或不禁用
    }


[Table("T_News")]
    public class T_News
    {
        [Key]
        [Required]
        [Display(Name = "新闻编号")]
        public int NewsID { get; set; }

        [Display(Name = "新闻标题")]
        public string NewsTitle { get; set; }

        [Required]
        [Display(Name = "新闻种类")]
        public int NewsTypeID { get; set; }

        [Display(Name = "新闻作者")]
        public string NewsAuthor { get; set; }

        [Display(Name = "新闻发布者")]
        public string NewsPublisher { get; set; }

        [Display(Name = "新闻发布时间")]
        public Nullable<int> PubDate { get; set; }

        [Display(Name = "新闻状态")]
        public int FlowState { get; set; }

        [Display(Name = "新闻是否禁用")]
        public Nullable<int> IsShow { get; set; }

        [Display(Name = "新闻是否锁定")]
        public Nullable<int> IsLocked { get; set; }

        [Display(Name = "新闻添加栏目")]
        public Nullable<int> NewsColumnID { get; set; }

        [Display(Name = "新闻图片地址")]
        public string PicUrl { get; set; }

        [Display(Name = "新闻视频地址")]
        public string VideoUrl { get; set; }

        [Display(Name = "新闻图文")]
        public string Html { get; set; }

        [Display(Name = "新闻链接地址")]
        public string LinkUrl { get; set; }

        [Display(Name = "资源下载")]
        public string Download { get; set; }
    }

}