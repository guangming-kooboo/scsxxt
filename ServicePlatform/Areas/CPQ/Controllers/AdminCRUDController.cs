using ServicePlatform.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicePlatform.Areas.CPQ.ViewModels.AdminCRUD;
using Qx.CPQ.Entity;
using Qx.Tools.CommonExtendMethods;
using Qx.CPQ.Models;
using Qx.Tools.Interfaces;
using Qx.CPQ.Services;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace ServicePlatform.Areas.CPQ.Controllers
{
    public class AdminCRUDController : BaseCPQController//BaseController //针对会员,以及接下去会有针对列表的BaseAccountController,
    {

        //Db.T_User.Find(uid);//查找数据
        //Db.SaveAdd(user);//插入数据
        //Db.SaveModified(user);//修改数据
        //Db.SaveDelete(user);//删除数据
        // GET: /CPQ/AdminCRUD/
        
        private IRepository<CPQ_T_AnswerSheet> _answerSheetRespository;
        private IRepository<CPQ_T_AttachQuestionToQuestionnaire> _attachQuestionToQuestionnaireRespository;
        private IRepository<CPQ_T_Questionnaire> _questionnaireRespository;
        private IRepository<CPQ_T_QuestionOption> _questionOptionRespository;
        private IRepository<CPQ_T_Question> _questionRespository;

        public AdminCRUDController(IRepository<CPQ_T_AnswerSheet> answerSheetRespository,IRepository<CPQ_T_AttachQuestionToQuestionnaire> attachQuestionToQuestionnaireRespository,IRepository<CPQ_T_Questionnaire> questionnaireRespository,IRepository<CPQ_T_QuestionOption> questionOptionRespository,IRepository<CPQ_T_Question> questionRespository)
         {
             _answerSheetRespository = answerSheetRespository;
             _attachQuestionToQuestionnaireRespository = attachQuestionToQuestionnaireRespository;
             _questionnaireRespository = questionnaireRespository;
             _questionOptionRespository = questionOptionRespository;
             _questionRespository = questionRespository;
         }




        #region 对问卷模板库列表的操作
        //预览试卷模板
        public ActionResult previewquestionnaireModels(string QuestionnaireID)
        {
           
            InitAdminLayout("预览问卷模板", "");
            PreviewquestionnaireModels_M pq =
            PreviewquestionnaireModels_M.ToViewModel(_cpqService.QuestionnaireInfo(QuestionnaireID));
            return View(pq);
        }

       
        //引用试卷模板
        public ActionResult referenceQuestionnaire(string QuestionnaireID)
        {
            if (_crudService.referenceQuestionnaire(QuestionnaireID, DataContext))
            {

                string ownerID = DataContext.UserID;
                //到时候再用这个ownerid过滤
                return RedirectToAction("createdQuestionnaireList", "Admin", new { area="CPQ",ReportID = "CPQ已创建的问卷列表", Params = ownerID+";" });

            }
            else {

                return Alert("引用问卷失败！");
            }
        }

        #endregion 对问卷模板库列表的操作


        #region 对问题模板库列表的操作
        //预览问题模板
        public ActionResult previewQuestionModels(string QuestionID,string QuestionnaireID)
        {
            InitAdminLayout("预览问题模板", "");
            PreviewQuestionModels_M pqm =
            PreviewQuestionModels_M.ToViewModel(_cpqService.QuestionInfo(QuestionID), QuestionnaireID);
            return View(pqm);
        }


        //引用问题模板
        public ActionResult referenceQuestion(string QuestionID, string QuestionnaireID)
        {
            if (_crudService.referenceQuestion(QuestionID, QuestionnaireID, DataContext))
            {
                string ownerID = DataContext.UserID;
                //到时候再用这个ownerid过滤
                return RedirectToAction("editQuestionnaire", "Admin", new { area = "CPQ", ReportID = "CPQ编辑问卷", Params = QuestionnaireID + ";;" });

            }
            else {

                return Alert("引用问题失败！");
            }
           
           
            


        }
        #endregion 对问卷模板库列表的操作


        
        #region 对已创建问卷列表列表的操作

        //创建问卷的标题简介和选择问卷领域
        public ActionResult createQuestionnaireOutline()
        {           
            InitForm("创建问卷");
            CQO_M cqo = CQO_M.ToViewModel(_cpqService.GetQuestionnaireDomainList(),_cpqService.GetShareList());
            return View(cqo);

        }
        [HttpPost]
        public ActionResult createQuestionnaireOutline(CQO_M cqo)
        {
           
                cqo.QuestionnaireName = Request.Form["QuestionnaireName"];
                cqo.Summarize = Request.Form["Summarize"];
                cqo.share = Convert.ToInt32(Request.Form["share"]);
                cqo.QuestionnaireDomain = Convert.ToInt32(Request.Form["QuestionnaireDomain"]);
                cqo.QuestionnaireID =DataContext.UserID + "-" + DateTime.Now.ToString("yyyyMMddhhmm");//获取用户ID+日期随机
                // CollectSeting=2,收集设置2代表无限制 CollectNumber=0 收集数为0,share=1是共享，2是不共享
                //上面的是从页面过来的数据

                cqo.Reference = 0;
                cqo.Status = 1;
                cqo.CreateTime = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
                cqo.OwnerID = DataContext.UserID;
                cqo.OwnerCompany = DataContext.UserUnit;
                cqo.CollectNumber = 0;
                cqo.CollectSeting = 2;
                cqo.IsLock = 0;

                if (_crudService.createQuestionnaireOutline(CQO_M.ToModel(cqo)))
                {
                    return Redirect(BackToReport);
                }

                else
                {
                    return Alert("问卷创建失败", -1);
                }
           
        }

        //ajax获取的页面的问卷名称和简介并且保存修改
        [HttpPost]
        public ActionResult editQuestionnaire()
        {
            string dataInfo = Request.Form[0];
            string[] dataInfoArray = dataInfo.Split(';');
            string QuestionnaireID = dataInfoArray[0];
            string QuestionnaireName = dataInfoArray[1];
            string QuestionnaireSummary = dataInfoArray[2];

            _questionnaireRespository.Update(EditQuestionnaire_M.ToModel(_questionnaireRespository.Find(QuestionnaireID), QuestionnaireName, QuestionnaireSummary));
            
            return Content("success");
         }
        
        //删除问卷
        //根据传过来的问卷主键删除问卷
        public ActionResult DeleteQuestionnaire(String QuestionnaireID)
        {
            _questionnaireRespository.Delete(QuestionnaireID);
           
           return Redirect(BackToReport);
        }



        //问卷发布
        //1、未发布 2、已发布
        public ActionResult publishQuestionnaire(String questionnaireID)
        {
    
            if (_questionnaireRespository.Update(CancelPublish_M.ToModel(_questionnaireRespository.Find(questionnaireID), 2)))
            {
                return RedirectToAction("collectionData", new { QuestionnaireID = questionnaireID });//发布成功就跳到收集数据页面              
            }
            else
            {
                return Alert("问卷操作失败，请再试一次", -1);
            }
            
        }


        //问卷取消发布
        //1、未发布 2、已发布
        public ActionResult cancelPublish(string questionnaireID)
        { 
            if (_questionnaireRespository.Update(CancelPublish_M.ToModel(_questionnaireRespository.Find(questionnaireID), 1)))
            {
                return Redirect(BackToReport);
            }
            else
            {
                return Alert("问卷操作失败，请再试一次", -1);
            }
        }


        //预览问卷（比如编辑问卷页面的预览，不是模板库的预览）
        public ActionResult previewQuestionnaire(string QuestionnaireID)
        {
            InitAdminLayout("预览问卷", "");
            PreviewQuestionnaire_M pq =
            PreviewQuestionnaire_M.ToViewModel(_cpqService.QuestionnaireInfo(QuestionnaireID));
            return View(pq);
     
            
        }
        [HttpPost]
        public ActionResult previewQuestionnaire(PreviewQuestionnaire_M pq)//预览完返回已创建问卷列表
        {
            return Redirect(BackToReport);
        }
        //收集数据页面
        public ActionResult collectionData(String questionnaireID)
        {
           string AnswerAdress = "localhost:10385/CPQ/HomeCRUD/answerOneQuestionnaire?id=" + questionnaireID;
           int CollectSeting = _questionnaireRespository.Find(questionnaireID).CollectSeting.Value;
           CollectionData_M cd = CollectionData_M.ToViewModel(AnswerAdress, questionnaireID, CollectSeting);            
            InitAdminLayout("收集数据页面", "");
            return View(cd);
        }

        //collectSet=1是限制IP，collectSet=2是无限制
        public ActionResult collect_ajax()
        {

            string setContent = Request.Form[0];
            string[] setContentArray = setContent.Split(';');
            int collectSet = Convert.ToInt32(setContentArray[0]);//得到收集设置的数据
            string QuestionnaireID = setContentArray[1];

            if (_questionnaireRespository.Update(Collect_ajax_M.ToModel(_questionnaireRespository.Find(QuestionnaireID), collectSet)))
            {

                return Content("success");

            }
            else {
                return Content("error");
            }
            
        }
       

        #endregion 对已创建问卷列表列表的操作



        #region 对编辑问卷（问卷题目）列表的操作

        //添加具体一道问题的类型领域共享性和问题ID，以及在问卷问题表里面建立相应的联系
        public ActionResult addQuestionSome(string questionnaireID, int pageIndex = 1, int perCount = 10)
        {
            string questionnaireID_share = questionnaireID + "-1";
            int share;
            if (_questionnaireRespository.Find(questionnaireID_share) != null)
            {
                share = 1;
            }
            else
            {
                share = 2;
            }

            AddQuestionSome_M aq = AddQuestionSome_M.ToViewModel(_cpqService.GetQuestionDomainList(), _cpqService.GetQuestionTypeList(), questionnaireID, share);

            InitAdminLayout("添加问题", CurrentUrl());
            return View(aq);

        }

        [HttpPost]
        
        public ActionResult addQuestionSome(AddQuestionSome_M aq)
        {

            //这里还是收不到强类型视图传过来的AddQuestionSome_M，先用传统的方法取值
            aq.QuestionnaireID = Request.Form["QuestionnaireID"];
            aq.share = Convert.ToInt32(Request.Form["share"]);
            aq.QuestionType = Convert.ToInt32(Request.Form["QuestionType"]);
            aq.QuestionDomain = Convert.ToInt32(Request.Form["QuestionDomain"]);
           // aq.QuestionID = DataContext.UserID + "-" + DateTime.Now.ToString("yyyyMMddhhmm");//用户id加上具体到分钟的时间
            aq.QuestionID = DateTime.Now.Random().ToString();
           
            //从视图得到相应的数据后，再填充到QuestionInfo类中传到crudservices中进行增删改的操作
            if (_crudService.addQuestionSome(AddQuestionSome_M.ToModel(aq)))
            {
                return RedirectToAction("addQuestionOther", "AdminCRUD", new { questionID = aq.QuestionID });
                //这里注意传统的用自己凭借url的容易出现验证错误
                //比如这个错误，因为带参数有问号：从客户端(?)中检测到有潜在危险的 Request.Path 值这个错误
               
            }
            else
            {

                return Alert("问题创建失败", -1);
            }
        }


        //添加具体一道问题的主干内容以及选项
        //根据type的ID选择返回的是哪个创建问题的view
        public ActionResult addQuestionOther(string questionID)
        {

            int QuestionType = _questionRespository.Find(questionID).QuestionType;
            if (QuestionType == 1) {

                return RedirectToAction("addQuestionOther_radio", "AdminCRUD", new { questionID = questionID });
            }
            else if (QuestionType == 2) {
                return RedirectToAction("addQuestionOther_checkBox", "AdminCRUD", new { questionID = questionID });

            }
            else if (QuestionType == 3)
            {
                return RedirectToAction("addQuestionOther_fill", "AdminCRUD", new { questionID = questionID });   

            }
            else {

              return Alert("发生错误了");
            }      
        }

        //添加单选题
        public ActionResult addQuestionOther_radio(string questionID)
        {
            AddQuestionOther_M aqo = new AddQuestionOther_M();
            aqo.QuestionID = questionID;
            InitAdminLayout("添加单选题", CurrentUrl());

            return View(aqo);
        }


        //添加多选题
        public ActionResult addQuestionOther_checkBox(string questionID)
        {
            AddQuestionOther_M aqo = new AddQuestionOther_M();
            aqo.QuestionID = questionID;
            InitAdminLayout("添加多选题", CurrentUrl());
            return View(aqo);
        }


        //添加填空题
        public ActionResult addQuestionOther_fill(string questionID)
        {
            AddQuestionOther_M aqo = new AddQuestionOther_M();
            aqo.QuestionID = questionID;
            InitAdminLayout("添加填空题", CurrentUrl());
            return View(aqo);
        }
        //添加具体一道问题的主干内容以及选项
        
        
        public ActionResult addQuestionOther_form(string QuestionContent)
        {
            if (_crudService.addQuestionOther_form(QuestionContent))
            {
                return Redirect(BackToReport);//返回编辑问卷的列表

            }
            else 
            {
                return Alert("添加问题失败，请重新添加！");
            }
                    
        }
        //问题的编辑
        public ActionResult editQuestion(String QuestionID)
        {
            EditQuestion_M pq =
            EditQuestion_M.ToViewModel(_cpqService.QuestionInfo(QuestionID));
            InitAdminLayout("编辑问题页面", "");
            return View(pq);
      
        }



        public ActionResult editQuestion_form(string QuestionContent)
        {

            if (_crudService.editQuestion_form(QuestionContent))
            {
                 return Redirect(BackToReport);//返回编辑问卷的列表

            }          
            else
            {
                return Alert("编辑问题失败", -1);
            }
   
        }
        //问题的删除
        public ActionResult deleteQuestion(String QuestionID )
        {
            _questionRespository.Delete(QuestionID);
            return Redirect(BackToReport);//返回编辑问卷的列表

        }

        //问题的预览
        public ActionResult previewQuestion(string QuestionID)
        {
            InitAdminLayout("预览问题页面", "");
            PreviewQuestion_M pq =
             PreviewQuestion_M.ToViewModel(_cpqService.QuestionInfo(QuestionID));
            return View(pq);
            
                                   
        }
        [HttpPost]
        public ActionResult previewQuestion(PreviewQuestion_M pq)
        {
            return Redirect(BackToReport);
        }


        //问题的上移
        public ActionResult moveQuestionUp(string QuestionID)
        {          
            if (_crudService.moveQuestionUp(QuestionID))
            {
                return Redirect(BackToReport);
            }
            else
            {
                return Alert("这个题目已经是最上面的一条了，不能再上移");
            }
                      
        }

        //问题的下移
        public ActionResult moveQuestionDown(String QuestionID)
        {
            if (_crudService.moveQuestionDown(QuestionID))
            {
                return Redirect(BackToReport);
            }
            else
            {
                return Alert("这个题目已经是最下面的一条了，不能再下移");
            }
           
        }


        #endregion  对编辑问卷（问卷题目）列表的操作


        #region  对分析一份问卷列表的操作
        //分析一份问卷里面一个问题的数据
        public ActionResult questionAnalyticResult(string QuestionID)
        {
             QuestionAnalyticResult_M qar =
             QuestionAnalyticResult_M.ToViewModel(_cpqService.ShowQuestionAnswer(QuestionID)); 
             InitAdminLayout("问题分析页面", "");
             return View(qar);
            

        }
        [HttpPost]
        public ActionResult questionAnalyticResult(QuestionAnalyticResult_M qam)
        {
            return Redirect(BackToReport);
        }


        //客户端导出打印pdf文件
        public void downLoadPdf(string QuestionnaireID)
        {
            List<QuestionAnalyticResult> answer = _cpqService.GetQuestionnaireAnswer(QuestionnaireID);
            string pdf_Filename = _cpqService.GetQuestionnaireTitle(QuestionnaireID);
            string path = System.Web.HttpContext.Current.Server.MapPath("../");
       
            string fontpath = path + "Areas\\CPQ\\Content\\Font\\simsun.ttc";
            BaseFont bfchinese = BaseFont.CreateFont(fontpath+",1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font ChFont = new Font(bfchinese, 10);
            Font ChFont_blue = new Font(bfchinese, 15, Font.NORMAL, new BaseColor(51, 0, 153));
            Font ChFont_blueBig = new Font(bfchinese, 30, Font.NORMAL, new BaseColor(51, 0, 153));
            Font ChFont_msg = new Font(bfchinese, 12, Font.ITALIC, BaseColor.RED);
            //System.Web.HttpContext curContext = System.Web.HttpContext.Current;
            //在服务器端保存PDF时的文件名
            //string strFileName = pdf_Filename + ".pdf";
            string strFileName = pdf_Filename + ".pdf";
            //初始化一个目标文档类 
            Document document = new Document(PageSize.A4.Rotate(), 0, 0, 10, 10);
            //调用PDF的写入方法流
            //注意FileMode-Create表示如果目标文件不存在，则创建，如果已存在，则覆盖。
          
            string currentPath = path + "/Content/" + strFileName;
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(currentPath, FileMode.Create));
            try
            {

                //打开目标文档对象
                document.Open();
                PdfPTable tableTitle = new PdfPTable(2);

                PdfPCell cellTitle = new PdfPCell(new Phrase(pdf_Filename, ChFont_blueBig));

                cellTitle.Colspan = 2;

                cellTitle.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right

                tableTitle.AddCell(cellTitle);
                document.Add(tableTitle);
                foreach (var temp in answer)
                {

                    
                    if (temp.QuestionType == 1 || temp.QuestionType == 2)
                    {
                        //根据数据表内容创建一个PDF格式的表
                        PdfPTable table = new PdfPTable(2);

                        PdfPCell cell = new PdfPCell(new Phrase("题目：" + temp.QuestionName, ChFont_blue));

                        cell.Colspan = 2;

                        cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right

                        table.AddCell(cell);
                        // 告诉程序这行是表头，这样页数大于1时程序会自动为你加上表头。
                        

                        table.AddCell(new Phrase("选项", ChFont_blue));
                        table.AddCell(new Phrase("选择人数", ChFont_blue));

                        for (int j = 0; j < temp.answers.Count; j++)
                        {
                            table.AddCell(new Phrase(temp.answers[j].OptionName, ChFont_blue));
                           
                            table.AddCell(new Phrase(temp.answers[j].selectedNumber+""));
                        }

                        //如果最后一个单元格数据过多，不要移动到下一页显示
                        table.SplitLate = false;
                        //
                        table.SplitRows = false;
                        //在目标文档中添加转化后的表数据
                        document.Add(table);

                    }
                    else if (temp.QuestionType == 3)
                    {
                        string[] answersss = temp.fill_Answer.Split(';');
                        PdfPTable table = new PdfPTable(1);
                        PdfPCell cell = new PdfPCell(new Phrase("题目：" + temp.QuestionName, ChFont_blue));
                        cell.HorizontalAlignment = 1;
                        table.AddCell(cell);
                         
                        for (int k = 0; k < answersss.Length; k++)
                        {
                            table.AddCell(new Phrase(answersss[k], ChFont_blue));

                        
                       }
                        document.Add(table);
                    }


                }


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //关闭目标文件
                document.Close();
                //关闭写入流
                writer.Close();
            }

            // 弹出提示框，提示用户是否下载保存到本地
            try
            {
                //这里是你文件在项目中的位置,根目录下就这么写 
               // String FullFileName = System.Web.HttpContext.Current.Server.MapPath(strFileName);
                string FullFileName = currentPath;
                // String FullFileName= Server.MapPath("upload/"+strFileName);
                FileInfo DownloadFile = new FileInfo(currentPath);
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.ClearHeaders();
                System.Web.HttpContext.Current.Response.Buffer = false;
                System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename="
                    + System.Web.HttpUtility.UrlEncode(DownloadFile.FullName, System.Text.Encoding.UTF8));
                System.Web.HttpContext.Current.Response.AppendHeader("Content-Length", DownloadFile.Length.ToString());
                System.Web.HttpContext.Current.Response.WriteFile(DownloadFile.FullName);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

                System.Web.HttpContext.Current.Response.Flush();
                System.Web.HttpContext.Current.Response.End();
                System.IO.File.Delete(currentPath);
            }
        }

        

        #endregion  对分析一份问卷列表的操作


        
       






       


       


        

       


      
    }
}
