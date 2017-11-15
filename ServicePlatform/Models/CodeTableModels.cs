using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;


namespace ServicePlatform.Models
{


    public class C_City//城市
    {

        public string ProvinceID { get; set; }
        [Key]
        public string CityID { get; set; }
        public string CityName { get; set; }
    }
    public class C_ApplyStatus//申请状态
    {
        [Key]
        public int ApplyStatusID { get; set; }
        public string ApplyStatusName { get; set; }
    }
    public class C_County//县区
    {

        public string CityID { get; set; }
        [Key]
        public string CountyID { get; set; }
        public string CountyName { get; set; }
    }
    public class C_EditStatus//修改状态
    {
        [Key]
        public int StatusID { get; set; }
        public string StatusName { get; set; }
    }

    public class C_CareerStatus
    {
        [Key]
        public int CareerStatusID { get; set; }
        public string CareerStatusName { get; set; }
    }
    public class C_EntCategory
    {
        [Key]
        public string EntCategoryID { get; set; }
        public string EntCategoryName { get; set; }
    }
    public class C_EntEvaluateStuItem//企业评价学生条目
    {
        [Key]
        public string ItemNo { get; set; }
        public string PracBatchID { get; set; }
        public int ItemSequence { get; set; }
        public string ItemName { get; set; }
        public int ItemPower { get; set; }
    }

    public class C_EntEvaStuGradeLevelItem//企业评价学生等级
    {
        [Key]
        public string ItemNo { get; set; }
        public string ItemName { get; set; }
        public string Note { get; set; }
        public int Weight { get; set; }
        public string PracBatchID { get; set; }
        public int ItemSequence { get; set; }

    }
    public class C_EntRank
    {
        [Key]
        public string EntRankID { get; set; }
        public string EntCategoryID { get; set; }
        public string EntRankName { get; set; }
    }
    public class C_Position
    {
        [Key]
        public string PositionID { get; set; }
        public string PositionName { get; set; }
    }
    public class C_PracAttendanceItem//实习考勤项目表
    {
        [Key]
        public string ItemNo { get; set; }
       
        public string ItemName { get; set; }
        public int ItemSequence { get; set; }
        public string PracBatchID { get; set; }//这是我新加上去的属性
    }


    public class C_PracAttendanceGradeItem//实习考勤项目等级表
    {
        [Key]
        public string ItemNo { get; set; }
        public string ItemName { get; set; }
        public string Note { get; set; }
        public int Weight { get; set; }
        public string PracBatchID { get; set; }
        public int ItemSequence { get; set; }
    }
    public class C_PracticeCaseItem//实习案例表
    {
        [Key]
        public string ItemNo { get; set; }
        public string PracBatchID { get; set; }
        public string ItemName { get; set; }
        public int ItemSequence { get; set; }
    }
    public class C_PracticeIdentificationItem//实习鉴定项目
    {
        [Key]
        public string ItemNo { get; set; }
        public string PracBatchID { get; set; }
        public string ItemName { get; set; }
        public int ItemSequence { get; set; }
    }

    public class C_ProPosition
    {
        [Key]
        public int ProPosID { get; set; }
        public string ProPosName { get; set; }
    }
    public class C_Province
    {
        [Key]
        public string ProvinceID { get; set; }
        public string ProvinceName { get; set; }
    }

    public class C_ReadStatus
    {
        [Key]
        public int StatusID { get; set; }
        public string StatusName { get; set; }
    }
    public class C_SelfEvaluationItem
    {
        [Key]
        public string ItemNo { get; set; }
        public int ExecYear { get; set; }
        public string ItemName { get; set; }
        public int ItemSequence { get; set; }
    }
    public class C_Specialty
    {
        [Key, Column(Order = 0)]
        public int EntryYear { get; set; }

        [NotMapped]
        public string EntryYear_S
        {
            get
            {
                return this.EntryYear.ToString();
            }
        }

        [Key, Column(Order = 1)]
        public string SpeNo { get; set; }

        [Key, Column(Order = 2)]
        public string SchoolID { get; set; }
        public string SpeName { get; set; }
    }
    public class C_StuEvaluateEntItem//学生评价企业条目
    {
        [Key]
        public string ItemNo { get; set; }
        public string PracBatchID { get; set; }
        public string ItemName { get; set; }
        public int ItemSequence { get; set; }
        [Range(0, 100)]
        public int ItemPower { get; set; }
    }
    public class C_StuEvaEntGradeLevelItem //学生评价企业条目等级
    {
        [Key]
        public string ItemNo { get; set; }
        public string PracBatchID { get; set; }
        public string ItemName { get; set; }
        public string Note { get; set; }
        [Range(0, 100)]
        public int Weight { get; set; }
        public int ItemSequence { get; set; }
    }
    public class C_StuEvaSchoolGradeLevelItem //学生评价学校条目等级
    {
        [Key]
        public string ItemNo { get; set; }
        public string PracBatchID { get; set; }
        public string ItemName { get; set; }
        public string Note { get; set; }
       
        [Range(0, 100)]
        public int Weight { get; set; }
        public int ItemSequence { get; set; }
    }

    public class C_UnitStatus
    {
        [Key]
        public int StatusID { get; set; }
        public string StatusName { get; set; }
    }
    public class C_UserType
    {
        [Key]
        public int UserTypeID { get; set; }
        public string UserTypeName { get; set; }
    }
    public class C_VolPosStatus
    {
        [Key]
        public int VolStatusID { get; set; }
        public string VolStatusName { get; set; }
    }

    public class C_DownLoadFileColumn//下载文件类型
    {
        [Key]
        public int DownLoadFileColumnID { get; set; }
        public string DownLoadFileColumnName { get; set; }
        public string SybSystem { get; set; }

    }
    public class C_ADColumn//广告栏目
    {
        [Key]
        public int ADColumnID { get; set; }
        public string ADColumnName { get; set; }
        public string SybSystem { get; set; }

    }

    public class C_StuInfoType
    {
        [Key]
        public int InfoTypeID { get; set; }
        public string InfoTypeName { get; set; }
    }

    public class C_SchoolReviewItem
    {
       
        public string ItemName { get; set; }
        public int ItemWeight { get; set; }
        public int ItemSequence { get; set; }
        [Key]
        public string ItemNo { get; set; }
        public string PracBatchID { get; set; }
    }

    public class C_StuEvaluateSchoolItem//学生评价学校条目
    {
        [Key]
        public string ItemNo { get; set; }
        public string PracBatchID { get; set; }
        public string ItemName { get; set; }
        public int ItemSequence { get; set; }
       
        [Range(0, 100)]
        public int ItemPower { get; set; }
    }
    public class C_EntReviewItem
    {
        [Key]
        public string ItemNo { get; set; }

        public string ItemName { get; set; }
        public int ItemWeight { get; set; }
        public int ItemSequence { get; set; }
        public string EntPracNo { get; set; }
      
      
    }

    public class C_MsgSendType
    {
        [Key]
        public int SendTypeID { get; set; }

        public string SendTypeName { get; set; }    
    }
    public class C_MsgType
    {
        [Key]
        public int MsgTypeID { get; set; }

        public string MsgTypeName { get; set; }
    }

    public class C_SchoolEvaStuGradeLevelItem //学生评价学校条目等级
    {
        [Key]
        public string ItemNo { get; set; }
        public string PracBatchID { get; set; }
        public string ItemName { get; set; }
        public string Note { get; set; }
        public int Weight { get; set; }
        public int ItemSequence { get; set; }
    }


    #region 注释类
    public class C_EntReviewItem_Note
    {

        public string ItemNo { get; set; }

        public string ItemName { get; set; }

        public string ItemWeight { get; set; }

        public string ItemSequence { get; set; }

        public string EntPracNo { get; set; }

        public C_EntReviewItem_Note()
        {

            ItemNo = "项目编号";

            ItemName = "项目名称";

            ItemWeight = "项目权重";

            ItemSequence = "项目排序";

            EntPracNo = "企业实习号";

        }

        //PageHelper.GetModelNote<C_EntReviewItem>(new C_EntReviewItem(),new string[]{"项目编号","项目名称","项目权重","项目排序","企业实习号"});

    }

    public class C_StuEvaluateSchoolItem_Note
    {

        public string ItemNo { get; set; }

        public string PracBatchID { get; set; }

        public string ItemName { get; set; }

        public string ItemSequence { get; set; }

        public C_StuEvaluateSchoolItem_Note()
        {

            ItemNo = "项目编号";

            PracBatchID = "实习批次号";

            ItemName = "项目名称";

            ItemSequence = "项目排序";

        }

        //PageHelper.GetModelNote<C_StuEvaluateSchoolItem>(new C_StuEvaluateSchoolItem(),new string[]{"项目编号","实习批次号","项目名称","项目排序"});

    }

    public class C_SchoolReviewItem_Note
    {

       

        public string ItemName { get; set; }

        public string ItemWeight { get; set; }

        public string ItemSequence { get; set; }
        public string ItemNo { get; set; }

        public string PracBatchID { get; set; }

        public C_SchoolReviewItem_Note()
        {

           

            ItemName = "项目名称";

            ItemWeight = "项目权重";

            ItemSequence = "项目排序";

            ItemNo = "项目编号";

            PracBatchID = "实习批次号";

        }

        //PageHelper.GetModelNote<C_SchoolReviewItem>(new C_SchoolReviewItem(),new string[]{"项目编号","实习批次号","项目名称","项目权重","项目排序"});

    }
    #endregion
}