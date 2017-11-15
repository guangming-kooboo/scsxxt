using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ServicePlatform.Models
{
    #region 内容使用
    [Table("T_HomePageContent")]
    public class T_HomePageContent
    {
        [Key, Column(Order = 0)]
        [Required]
        public string HPCID { get; set; }

        [Key, Column(Order = 1)]
        [Required]
        public int HPColID { get; set; }

        [Required]
        public int HPCSeq { get; set; }
    }

    [Table("T_HomePageColumn")]
    public class T_HomePageColumn
    {
        [Key]
        [Required]
        [Display(Name = "首页栏目编号")]
        public int HPColumnID { get; set; }

        [Display(Name = "首页栏目标题")]
        public string HPColumnName { get; set; }

        [Display(Name = "首页栏目显示条数")]
        public int ColRowCount { get; set; }

        [Display(Name = "时间排序")]
        public string OnTimeDESC { get; set; }

        [Display(Name = "内容种类")]
        public string ContentType { get; set; }

        [Display(Name = "内容来源")]
        public int ContentFrom { get; set; }

        [Display(Name = "单位编号")]
        public string UnitID { get; set; }
    }

    #endregion 内容使用


    #region 内容顶层
    [Table("C_ContentType")]
    public class C_ContentType
    {
        [Key]
        [Required]
        [Display(Name = "内容种类编号")]
        public int ConTypeID { get; set; }

        [Required]
        [Display(Name = "内容种类名称")]
        public string ConTypeName { get; set; }
    }

    [Table("T_Content")]
    public class T_Content
    {
        [Key]
        [Required]
        [Display(Name = "内容编号")]
        public string ContentID { get; set; }

        [Display(Name = "内容种类编号")]
        public int ContentTypeID { get; set; }

        [Display(Name = "内容标题")]
        public string ContentTitle { get; set; }

        [Display(Name = "内容发布人")]
        public string ContentPublisher { get; set; }

        [Display(Name = "内容发布时间")]
        public int PubDate { get; set; }

        [Display(Name = "单位编号")]
        public string UnitID { get; set; }
    }

    [Table("C_ContentColumn")]
    public class C_ContentColumn
    {
        [Key]
        [Required]
        //[StringLength(4,ErrorMessage="请输入4个数字",MinimumLength=4)]
        [Display(Name = "内容栏目编号")]
        public int ContColumnID { get; set; }

        [Display(Name = "内容栏目名称")]
        public string ContColumnName { get; set; }

        [Display(Name = "所属内容类型")]
        public int ContTypeID { get; set; }

        [Display(Name = "所属子系统")]
        public string SybSystem { get; set; }

    }

    #endregion 内容顶层

    #region 新闻

    [Table("T_News")]
    public class T_News
    {
        [Key]
        [Required]
        [Display(Name = "News+10位新闻编号（InnerID）")]
        public string NewsID { get; set; }

        [Required]
        [Display(Name = "新闻种类")]
        public int NewsTypeID { get; set; }

        [Display(Name = "新闻作者")]
        public string NewsAuthor { get; set; }

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
        [AllowHtml]
        public string Html { get; set; }

        [Display(Name = "新闻链接地址")]
        public string LinkUrl { get; set; }

        [Display(Name = "资源下载")]
        public string Download { get; set; }

        [Display(Name = "新闻编号")]
        public int InnerID { get; set; }
    }


    [Table("C_NewsState")]
    public class C_NewsState
    {
        [Key]
        public int NewsStateID { get; set; }
        public string NewsStateName { get; set; }
    }

    public class C_NewsTime
    {
        public int OriTime { get; set; }
        public string ManTime { get; set; }
        public void SetManTime(int a)
        {
            ManTime = Convert.ToString(ServicePlatform.Areas.News.ToolHelper.HandleTime.ConvertIntDateTime(a));
        }
    }

    [Table("C_NewsType")]
    public class C_NewsType
    {
        [Key]
        public int TypeID { get; set; }
        public string TypeName { get; set; }
    }

    public class NewsIsLocked
    {
        public int IsLockID { get; set; }//1或-1
        public string IsLockName { get; set; }//锁定或不锁定
    }

    public class NewsIsShow
    {
        public int IsShowID { get; set; }//1或-1
        public string IsShowName { get; set; }//禁用或不禁用
    }



    #endregion 新闻



    #region 下载文件
    [Table("T_DownLoadFiles")]
    public class T_DownLoadFiles
    {
        [Key]
        [Required]
        [Display(Name = "DLF+10位下载文件编号（InnerID）")]
        public string DLFileID { get; set; }

        [Display(Name = "发布栏目")]
        public int DLFileColumnID { get; set; }

        [Display(Name = "资源地址")]
        public string DLFileUrl { get; set; }

        [Display(Name = "资源编号")]
        public int InnerID { get; set; }

    }
   
#endregion 下载文件

    #region 签名
    [Table("T_Signature")]
    public class T_Signature
    {
        [Key]
        [Required]
        [Display(Name = "Sign+10位签名编号（InnerID）")]
        public string SignatureID { get; set; }

        [Display(Name = "签名地址")]
        public string SignatureUrl { get; set; }

        [Display(Name = "签名编号")]
        public int InnerID { get; set; }

        [Display(Name = "用户编号")]
        public string UserID { get; set; }
    }

#endregion 签名

    #region 印章

    [Table("T_Stamps")]
    public class T_Stamps
    {
        [Key]
        [Required]
        [Display(Name = "Stamp+10位印章编号（InnerID）")]
        public string StampsID { get; set; }

        [Display(Name = "印章种类")]
        public int StampsTypeID { get; set; }

        [Display(Name = "印章地址")]
        public string StampsUrl { get; set; }

        [Display(Name = "印章编号")]
        public int InnerID { get; set; }
    }

    public class C_StampType
    {
        [Key]
        [Required]
        [Display(Name = "印章种类编号")]
        public int TypeID { get; set; }

        [Display(Name = "印章种类名称")]
        public string TypeName { get; set; }

        [Display(Name = "单位编号")]
        public string SybSystem { get; set; }
    }
    #endregion 印章




}


