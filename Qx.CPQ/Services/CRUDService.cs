using Qx.CPQ.Entity;
using Qx.CPQ.Models;
using Qx.Wbs.Services;
using System.Collections.Generic;
using System.Linq;
using Qx.Tools.CommonExtendMethods;
using System.Data.Entity;
using Qx.Tools;
using System;
using System.Web;
namespace Qx.CPQ.Services
{
    #region model

    public class QuestionnaireInfo
    {
        
        public string QuestionnaireID;
        
        public string QuestionnaireName;
        
        public string Summarize;

        public int QuestionnaireDomain;

        public int CreateTime;
      
        public string OwnerID;

        public int Status;
        
        public string OwnerCompany;

        public int share;

        public int Reference;

        public int CollectSeting;

        public int CollectNumber;

        public int IsLock;
    }

    public class QuestionInfo
    {
        public string QuestionnaireID;
        public string QuestionID;
        public int QuestionType;       
        public string QuestionName;
        public int QuestionDomain;
        public int share;
        public int Reference;
    }

    public class QuestionOptionInfo
    {
        public string QuestionOptionID;


        public string QuestionID;


        public string QuestionOptionIName;

        public int SequenID;
    }

    public class questionInfoTQInfo//问卷问题表的信息缩写questionInfotq
    {
        public string AttachID;


        public string QuestionnaireID;


        public string QuestionID;

        public int QuestionSequenceID;
    }

    #endregion


    public class CRUDService : BaseService
    {

      
        //对问题的上移操作
        public bool moveQuestionUp(string QuestionID)
        {
             //修改这一条的sequence，再修改上一条的sequence，查找上一条：先按照sequence排序，在找出大于这一条sequence的第一条 ，修改          
            //at_Down就是在下面的问题也就是qequence比较大的那一条
            CPQ_T_AttachQuestionToQuestionnaire at_Down= (from a in  Db.CPQ_T_AttachQuestionToQuestionnaire where a.QuestionID==QuestionID select a).FirstOrDefault(); 
            int sequence_Down = at_Down.QuestionSequenceID;
            string QuestionnaireID = at_Down.QuestionnaireID;

            //找出他的上一条
            CPQ_T_AttachQuestionToQuestionnaire at_up =(from b in Db.CPQ_T_AttachQuestionToQuestionnaire where b.QuestionnaireID==QuestionnaireID &&  b.QuestionSequenceID < sequence_Down select b) .OrderBy(c => c.QuestionSequenceID).FirstOrDefault();
         
             if (at_up != null)
            {   //上下两条交换QuestionSequenceID的值
                 at_Down.QuestionSequenceID=at_up.QuestionSequenceID;               
                 at_up.QuestionSequenceID=sequence_Down;

                 Db.Entry(at_Down).State=EntityState.Modified;
                 Db.Entry(at_up).State=EntityState.Modified;
                
                 //Db.SaveModified(at_Down);
                 //Db.SaveModified(at_up);
                 return Db.SaveChanges()>0;
            }
             else 
             {
                 return false;
             
             }
        }

        //对问题的下移操作
        public bool moveQuestionDown(string QuestionID)
        {
             //修改这一条的sequence，再修改上一条的sequence，查找上一条：先按照sequence排序，在找出大于这一条sequence的第一条 ，修改          
            //at_Down就是在下面的问题也就是qequence比较大的那一条
            CPQ_T_AttachQuestionToQuestionnaire at_up= (from a in  Db.CPQ_T_AttachQuestionToQuestionnaire where a.QuestionID==QuestionID select a).FirstOrDefault(); 
            int sequence_Up = at_up.QuestionSequenceID;
            string QuestionnaireID = at_up.QuestionnaireID;

            //找出他的上一条
            CPQ_T_AttachQuestionToQuestionnaire at_Down =(from b in Db.CPQ_T_AttachQuestionToQuestionnaire where b.QuestionnaireID==QuestionnaireID &&  b.QuestionSequenceID > sequence_Up select b).OrderBy(c => c.QuestionSequenceID).FirstOrDefault();
         
             if (at_Down != null)
            {   //上下两条交换QuestionSequenceID的值
                 at_up.QuestionSequenceID=at_Down.QuestionSequenceID;               
                 at_Down.QuestionSequenceID=sequence_Up;

                 Db.Entry(at_up).State=EntityState.Modified;
                 Db.Entry(at_Down).State=EntityState.Modified;
                
                 //Db.SaveModified(at_Down);
                 //Db.SaveModified(at_up);
                 return Db.SaveChanges()>0;
            }
             else 
             {
                 return false;
             
             }
        }

        public bool referenceQuestionnaire(string QuestionnaireID, DataContext dataContext)
        {
            CPQ_T_Questionnaire QuestionnaireModels = Db.CPQ_T_Questionnaire.Find(QuestionnaireID);
            string QuestionnaireIDCopy = dataContext.UserID + "-" + DateTime.Now.ToString("yyyyMMddhhmm");
            CPQ_T_Questionnaire Questionnaire = new CPQ_T_Questionnaire()
            {
                QuestionnaireID = QuestionnaireIDCopy,
                QuestionnaireDomain = QuestionnaireModels.QuestionnaireDomain,
                QuestionnaireName = QuestionnaireModels.QuestionnaireName,
                share = 2,
                Status = QuestionnaireModels.Status,
                Summarize = QuestionnaireModels.Summarize,
                CreateTime = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")),
                OwnerID = dataContext.UserID,
                OwnerCompany = dataContext.UserUnit,
                Reference = 0,
                CollectSeting = QuestionnaireModels.CollectSeting,
                CollectNumber = 0,
                IsLock = 0

            };
            Db.Entry(Questionnaire).State = EntityState.Added;
           // Db.SaveAdd(Questionnaire);

            List<string> QuestionID_Models = (from a in Db.CPQ_T_AttachQuestionToQuestionnaire where a.QuestionnaireID == QuestionnaireID select a.QuestionID).ToList();
            int count = 0;//加一个count防止时间太短添加导致主键冲突
            int sequence = 1;//这个是用来添加问题次序的
            foreach (var b in QuestionID_Models)
            {

                CPQ_T_Question QuestionModels = Db.CPQ_T_Question.Find(b);
                string QuestionID = dataContext.UserID + "-" + DateTime.Now.ToString("yyyyMMddhhmm") + count;
                count++;
                CPQ_T_Question Question = new CPQ_T_Question()
                {
                    QuestionID = QuestionID,
                    QuestionDomain = QuestionModels.QuestionDomain,
                    QuestionName = QuestionModels.QuestionName,
                    QuestionType = QuestionModels.QuestionType,
                    Reference = 0,
                    share = 2

                };
                //Db.SaveAdd(Question);
                Db.Entry(Question).State = EntityState.Added;
                //往问卷问题表添加

                CPQ_T_AttachQuestionToQuestionnaire at = new CPQ_T_AttachQuestionToQuestionnaire()
                {

                    QuestionID = QuestionID,
                    QuestionnaireID = QuestionnaireIDCopy,
                    QuestionSequenceID = sequence,
                    AttachID = QuestionnaireIDCopy + "-" + QuestionID + "-" + sequence
                };
                sequence++;
                Db.Entry(at).State = EntityState.Added;
               // Db.SaveAdd(at);
                //接下去复对于每个问题拥有的所有选项
                List<CPQ_T_QuestionOption> QuestionOptionsModels = (from c in Db.CPQ_T_QuestionOption where c.QuestionID == b select c).ToList();

                foreach (var d in QuestionOptionsModels)
                {

                    CPQ_T_QuestionOption QuestionOption = new CPQ_T_QuestionOption()
                    {
                        QuestionID = QuestionID,
                        QuestionOptionIName = d.QuestionOptionIName,
                        SequenID = d.SequenID,
                        QuestionOptionID = QuestionID + "-" + d.SequenID
                    };
                    Db.Entry(QuestionOption).State = EntityState.Added;
                   // Db.SaveAdd(QuestionOption);

                }



            }

            QuestionnaireModels.Reference = QuestionnaireModels.Reference + 1;//引用玩后对问卷模板的引用数加1
            Db.Entry(QuestionnaireModels).State = EntityState.Modified;
            //Db.SaveModified(QuestionnaireModels);
            return Db.SaveChanges()>0;
        }
        
       public bool referenceQuestion(string QuestionID,string QuestionnaireID,DataContext dataContext){
           CPQ_T_Question QuestionModels = Db.CPQ_T_Question.Find(QuestionID);
           //加一个count防止时间太短添加导致主键冲突
          string PK= DateTime.Now.Random().ToString();
          
           //10385-14093当前用户，后一个版本改成问题ID给用随机数赋值了
          // string QuestionID_copy = dataContext.UserID + "-" + DateTime.Now.ToString("yyyyMMddhhmm") + RandKey;
          string QuestionID_copy = PK;
           CPQ_T_Question Question = new CPQ_T_Question()
           {
               QuestionID = QuestionID_copy,
               QuestionDomain = QuestionModels.QuestionDomain,
               QuestionName = QuestionModels.QuestionName,
               QuestionType = QuestionModels.QuestionType,
               Reference = 0,
               share = 2

           };
           //Db.SaveAdd(Question);
           Db.Entry(Question).State = EntityState.Added;
           //往问卷问题表添加


           var sequenceList = (from a in Db.CPQ_T_AttachQuestionToQuestionnaire where a.QuestionnaireID == QuestionnaireID select (a.QuestionSequenceID)).ToList();
           int sequenceID;
           if (sequenceList.Count == 0)
           {

               sequenceID = 1;
           }
           else
           {
               // sequenceList.Sort();//本来是打算对list进行sort排序，然后最小的事第一个最大的事最后一个，但是好像list自己有一个max方法
               sequenceID = sequenceList.Max() + 1;
           }
           CPQ_T_AttachQuestionToQuestionnaire at = new CPQ_T_AttachQuestionToQuestionnaire()
           {

               QuestionID = QuestionID_copy,
               QuestionnaireID = QuestionnaireID,
               QuestionSequenceID = sequenceID,
               AttachID = QuestionnaireID + "-" + QuestionID_copy + "-" + sequenceID
           };

           //Db.SaveAdd(at);
           Db.Entry(at).State = EntityState.Added;
           //接下去复对于每个问题拥有的所有选项
           List<CPQ_T_QuestionOption> QuestionOptionsModels = (from c in Db.CPQ_T_QuestionOption where c.QuestionID == QuestionID select c).ToList();

           foreach (var d in QuestionOptionsModels)
           {

               CPQ_T_QuestionOption QuestionOption = new CPQ_T_QuestionOption()
               {
                   QuestionID = QuestionID_copy,
                   QuestionOptionIName = d.QuestionOptionIName,
                   SequenID = d.SequenID,
                   QuestionOptionID = QuestionID_copy + "-" + d.SequenID
               };
               //Db.SaveAdd(QuestionOption);
               Db.Entry(QuestionOption).State = EntityState.Added;
           }

           QuestionModels.Reference = QuestionModels.Reference + 1;//引用玩后对问题模板的引用数加1
          // Db.SaveModified(QuestionModels);
           Db.Entry(QuestionModels).State = EntityState.Modified;

           return Db.SaveChanges() > 0;
       }

       public bool addQuestionSome(QuestionInfo questionInfo) {
            CPQ_T_Question  Question =new CPQ_T_Question()
            {
                QuestionName="请填写题目内容",
                QuestionID=questionInfo.QuestionID,
                QuestionDomain = questionInfo.QuestionDomain,
                QuestionType = questionInfo.QuestionType,
                share=2,
                Reference=0

            };
           Db.Entry(Question).State=EntityState.Added;
            
            //根据问卷ID从问卷问题表找所有的问卷序列，如果返回空，就令这题的次序是1，如果不为空就是次序的最大值+1
           var sequenceList = (from a in Db.CPQ_T_AttachQuestionToQuestionnaire where a.QuestionnaireID == questionInfo.QuestionnaireID select (a.QuestionSequenceID)).ToList();
           int sequenceID;    
           if(sequenceList.Count==0){

                sequenceID=1;
            }
            else{              
               // sequenceList.Sort();//本来是打算对list进行sort排序，然后最小的事第一个最大的事最后一个，但是好像list自己有一个max方法
                sequenceID=sequenceList.Max()+1;
            }



           CPQ_T_AttachQuestionToQuestionnaire at = new CPQ_T_AttachQuestionToQuestionnaire()
           {
               AttachID = questionInfo.QuestionnaireID + "-" + questionInfo.QuestionID + "-" + sequenceID,//问卷问题表的命名规范就是问卷id+问题id+问题次序
               QuestionID = questionInfo.QuestionID,
               QuestionnaireID = questionInfo.QuestionnaireID,
               QuestionSequenceID = sequenceID
           };
           
           Db.Entry(at).State=EntityState.Added;
            //if (Db.SaveAdd(Question)&&Db.SaveAdd(at))
            //{

                if (questionInfo.share ==1)
                {//如果是共享的
                    string QuestionnaireID_shareID = questionInfo.QuestionnaireID + "-1";//只有引用数=0的时候才可以修改

                    if (Db.CPQ_T_Questionnaire.Find(QuestionnaireID_shareID) != null && Db.CPQ_T_Questionnaire.Find(QuestionnaireID_shareID).Reference == 0)
                    {

                        CPQ_T_Question Question_share = new CPQ_T_Question()
                        {
                            QuestionName = "请填写题目内容",
                            QuestionID = questionInfo.QuestionID + "-1",
                            QuestionDomain = questionInfo.QuestionDomain,
                            QuestionType = questionInfo.QuestionType,
                            share = 1,
                            Reference = 0

                        };

                       // Db.SaveAdd(Question_share);
                        Db.Entry(Question_share).State=EntityState.Added;

                        CPQ_T_AttachQuestionToQuestionnaire at_share = new CPQ_T_AttachQuestionToQuestionnaire()
                        {
                            AttachID = questionInfo.QuestionnaireID + "-1" + "-" + questionInfo.QuestionID + "-1" + "-" + sequenceID,//问卷问题表的命名规范就是问卷id+问题id+问题次序
                            QuestionID = questionInfo.QuestionID + "-1",
                            QuestionnaireID = questionInfo.QuestionnaireID+"-1",
                            QuestionSequenceID = sequenceID
                            
                        };
                       
                        
                       // Db.SaveAdd(at_share);
                        Db.Entry(at_share).State=EntityState.Added;
                    }
                }
                
            return Db.SaveChanges()>0;
      }

       public bool addQuestionOther_form(string QuestionContent)
       {
           //数组arrays第一个存类型，第二个存ID，第三个存问题名称，接下去存的是一个一个选项的内容按照选项sequence
           string[] arrays = QuestionContent.Split(';');
           string QuestionID = arrays[1];
           string Question_shareID = QuestionID + "-1";
           string QuestionName = arrays[2];
           CPQ_T_Question Question = Db.CPQ_T_Question.Find(QuestionID);
           Question.QuestionName = QuestionName;
           //Db.SaveModified(Question);//先保存问题内容，接下去保存问题的选项
           Db.Entry(Question).State = EntityState.Modified;

           CPQ_T_Question Question_SHARE = Db.CPQ_T_Question.Find(Question_shareID);
           if (Question_SHARE != null)
           {
               Question_SHARE.QuestionName = QuestionName;
               //Db.SaveModified(Question_SHARE);//先保存问题内容，接下去保存问题的选项
               Db.Entry(Question_SHARE).State = EntityState.Modified;

           }
           //如果是单选多选题
           if (arrays[0] == "1" || arrays[0] == "2")
           {
               for (int i = 3; i < arrays.Length; i++)
               {
                   int sequence = i - 2;

                   CPQ_T_QuestionOption qo = new CPQ_T_QuestionOption()
                   {
                       QuestionOptionID = QuestionID + "-" + sequence,//选项ID=问题ID+-+选项次序
                       QuestionID = QuestionID,
                       QuestionOptionIName = arrays[i],
                       SequenID = sequence
                   };
                   //Db.SaveAdd(qo);//保存这个选项
                   Db.Entry(qo).State = EntityState.Added;

                   if (Db.CPQ_T_Question.Find(Question_shareID) != null)
                   {

                       CPQ_T_QuestionOption qo_share = new CPQ_T_QuestionOption()
                       {
                           QuestionOptionID = Question_shareID + "-" + sequence,//选项ID=问题ID+-+选项次序
                           QuestionID = Question_shareID,
                           QuestionOptionIName = arrays[i],
                           SequenID = sequence
                       };

                       //Db.SaveAdd(qo_share);
                       Db.Entry(qo_share).State = EntityState.Added;
                   }


               }

           }
           //如果是填空题，因为只有题干，没有选项所有什么都不用做
           else if (arrays[0] == "3")
           {


           }     //如果这三个都不是，说明传过来的数据有问题
           
    
           
           return Db.SaveChanges()>0;
       }

       public bool editQuestion_form(string QuestionContent) {
           string[] arrays = QuestionContent.Split(';');
           string QuestionID = arrays[1];
           string Question_shareID = QuestionID + "-1";
           string QuestionName = arrays[2];
           CPQ_T_Question Question = Db.CPQ_T_Question.Find(QuestionID);
           Question.QuestionName = QuestionName;
           // Db.SaveModified(Question);//先保存问题内容，接下去保存问题的选项
           Db.Entry(Question).State = EntityState.Modified;
           if (arrays[0] == "1" || arrays[0] == "2")
           {
               //先删掉所有选项，再添加所有选项
               List<CPQ_T_QuestionOption> options = (from a in Db.CPQ_T_QuestionOption where a.QuestionID == QuestionID select a).ToList();
               foreach (var temp in options)
               {

                  // Db.SaveDelete(temp);
                   Db.Entry(temp).State = EntityState.Deleted;

               }

               for (int i = 3; i < arrays.Length; i++)
               {
                   int sequence = i - 2;

                   CPQ_T_QuestionOption qo = new CPQ_T_QuestionOption()
                   {
                       QuestionOptionID = QuestionID + "-" + sequence,//选项ID=问题ID+-+选项次序
                       QuestionID = QuestionID,
                       QuestionOptionIName = arrays[i],
                       SequenID = sequence
                   };
                  // Db.SaveAdd(qo);//保存这个选项
                   Db.Entry(qo).State = EntityState.Added;
               }
           }
           else if (arrays[0] == "3")
           {

           }
           return Db.SaveChanges()>0;
      
       
       
       
       }

       public string answerOneQuestionnaire_Form(string AnswerContent,string IpAdress) {


            string[] arrays = AnswerContent.Split(',');//然后根据，划分数组，最后一个不要，第一个是问卷ID,后面依次是问题以及答案
            string QuestionnaireID = arrays[0];

           
            CPQ_T_Questionnaire Questionnaire = Db.CPQ_T_Questionnaire.Find(QuestionnaireID);
            int collectSteing = Convert.ToInt32(Questionnaire.CollectSeting);
            if (collectSteing == 1) { //限制IP，即要先从答题表中看这份问卷这个的条目数是否为空
                CPQ_T_AnswerSheet answer_litmit = (from a in Db.CPQ_T_AnswerSheet where a.AnswererIP == IpAdress && a.QuestionnaireID == QuestionnaireID select a).FirstOrDefault();
                if (answer_litmit == null) {

                    for (int i = 1; i < arrays.Length - 1; i++)
                    {
                        string[] OptionAnswer = arrays[i].Split(':');//问题和答案之间按照：来区分第一个是问题ID，后面依次是答案
                        string QuestionID = OptionAnswer[0];
                        int QuestionType = Db.CPQ_T_Question.Find(QuestionID).QuestionType;
                        if (QuestionType == 1)
                        {
                            CPQ_T_AnswerSheet answer = new CPQ_T_AnswerSheet()
                            {
                                AnswererIP = IpAdress,
                                QuestionID = QuestionID,
                                QuestionnaireID = QuestionnaireID,
                                AnswerTime = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")),
                                Answer = OptionAnswer[1],
                                AnswerSheetID = IpAdress + Convert.ToInt32(DateTime.Now.ToString("yyyyMMddhhmm")) + QuestionID

                            };
                           // Db.SaveAdd(answer);
                            Db.Entry(answer).State=EntityState.Added;
                        }
                        else if (QuestionType == 2)
                        {

                            string answer_2 = OptionAnswer[1].Remove(OptionAnswer[1].LastIndexOf(";"), 1);
                            CPQ_T_AnswerSheet answer = new CPQ_T_AnswerSheet()
                            {
                                AnswererIP = IpAdress,
                                QuestionID = QuestionID,
                                QuestionnaireID = QuestionnaireID,
                                AnswerTime = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")),
                                Answer = answer_2,
                                AnswerSheetID = IpAdress + Convert.ToInt32(DateTime.Now.ToString("yyyyMMddhhmm")) + QuestionID

                            };
                            //Db.SaveAdd(answer);
                            Db.Entry(answer).State=EntityState.Added;
                        }
                        else if (QuestionType == 3)
                        {
                            CPQ_T_AnswerSheet answer = new CPQ_T_AnswerSheet()
                            {
                                AnswererIP = IpAdress,
                                QuestionID = QuestionID,
                                QuestionnaireID = QuestionnaireID,
                                AnswerTime = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")),
                                Answer = OptionAnswer[1],
                                AnswerSheetID = IpAdress + Convert.ToInt32(DateTime.Now.ToString("yyyyMMddhhmm")) + QuestionID

                            };
                           
                            Db.Entry(answer).State=EntityState.Added;
                        }
                
                
                }
                }
                else
                {
                   
                    return  "对不起，您已经填过这份问卷不能再填写了";
                }
            }
            else if (collectSteing == 2)
            {//问卷答题者无限制


                for (int i = 1; i < arrays.Length - 1; i++)
                {
                    string[] OptionAnswer = arrays[i].Split(':');//问题和答案之间按照：来区分第一个是问题ID，后面依次是答案
                    string QuestionID = OptionAnswer[0];
                    int QuestionType = Db.CPQ_T_Question.Find(QuestionID).QuestionType;
                    if (QuestionType == 1)
                    {
                        CPQ_T_AnswerSheet answer = new CPQ_T_AnswerSheet()
                        {
                            AnswererIP = IpAdress,
                            QuestionID = QuestionID,
                            QuestionnaireID = QuestionnaireID,
                            AnswerTime = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")),
                            Answer = OptionAnswer[1],
                            AnswerSheetID = IpAdress + Convert.ToInt64(DateTime.Now.ToString("yyyyMMddhhmm")) + QuestionID

                        };
                      //  Db.SaveAdd(answer);
                        Db.Entry(answer).State = EntityState.Added;

                    }
                    else if (QuestionType == 2)
                    {

                        string answer_2 = OptionAnswer[1].Remove(OptionAnswer[1].LastIndexOf(";"), 1);
                        CPQ_T_AnswerSheet answer = new CPQ_T_AnswerSheet()
                        {
                            AnswererIP = IpAdress,
                            QuestionID = QuestionID,
                            QuestionnaireID = QuestionnaireID,
                            AnswerTime = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")),
                            Answer = answer_2,
                            AnswerSheetID = IpAdress + Convert.ToInt64(DateTime.Now.ToString("yyyyMMddhhmm")) + QuestionID

                        };
                       // Db.SaveAdd(answer);
                        Db.Entry(answer).State = EntityState.Added;

                    }
                    else if (QuestionType == 3)
                    {
                        CPQ_T_AnswerSheet answer = new CPQ_T_AnswerSheet()
                        {
                            AnswererIP = IpAdress,
                            QuestionID = QuestionID,
                            QuestionnaireID = QuestionnaireID,
                            AnswerTime = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")),
                            Answer = OptionAnswer[1],
                            AnswerSheetID = IpAdress + Convert.ToInt64(DateTime.Now.ToString("yyyyMMddhhmm")) + QuestionID

                        };
                        //Db.SaveAdd(answer);
                        Db.Entry(answer).State = EntityState.Added;
                    }

                }

            }
            
            
            
                
                Questionnaire.IsLock = 1;
                Questionnaire.CollectNumber = Questionnaire.CollectNumber+1;
               // Db.SaveModified(Questionnaire);
                Db.Entry(Questionnaire).State = EntityState.Modified;
                if (Db.SaveChanges() > 0)
                {
                    return "success";
                }
                else
                {
                    return " error";
                }
       }

       public bool createQuestionnaireOutline(QuestionnaireInfo quwstionnaireInfo)
       {
         CPQ_T_Questionnaire Questionnaire=new CPQ_T_Questionnaire(){
         
                QuestionnaireDomain = quwstionnaireInfo.QuestionnaireDomain,
                QuestionnaireName = quwstionnaireInfo.QuestionnaireName,
                share = 2,
                Summarize = quwstionnaireInfo.Summarize,
                Reference = quwstionnaireInfo.Reference,
                Status = quwstionnaireInfo.Status,
                CreateTime = quwstionnaireInfo.CreateTime,
                OwnerID = quwstionnaireInfo.OwnerID,
                OwnerCompany = quwstionnaireInfo.OwnerCompany,
                QuestionnaireID = quwstionnaireInfo.QuestionnaireID,
                CollectSeting = quwstionnaireInfo.CollectSeting,
                CollectNumber = quwstionnaireInfo.CollectNumber,
                IsLock = quwstionnaireInfo.IsLock
         
         
         };
           Db.Entry(Questionnaire).State=EntityState.Added;



           if (quwstionnaireInfo.share == 1) {
               CPQ_T_Questionnaire Questionnaire_share = new CPQ_T_Questionnaire()
               {
                    QuestionnaireID = quwstionnaireInfo.QuestionnaireID + "-1",
                    QuestionnaireDomain = quwstionnaireInfo.QuestionnaireDomain,
                    QuestionnaireName = quwstionnaireInfo.QuestionnaireName,
                    share = 1,
                    Summarize = quwstionnaireInfo.Summarize,
                    Reference = quwstionnaireInfo.Reference,
                    Status = quwstionnaireInfo.Status,
                    CreateTime = quwstionnaireInfo.CreateTime,
                    OwnerID = quwstionnaireInfo.OwnerID,
                    OwnerCompany = quwstionnaireInfo.OwnerCompany,
                   
                    CollectSeting = quwstionnaireInfo.CollectSeting,
                    CollectNumber = quwstionnaireInfo.CollectNumber,
                    IsLock = quwstionnaireInfo.IsLock

               };


               Db.Entry(Questionnaire_share).State = EntityState.Added;
           }
           
           return Db.SaveChanges()>0;
       }


    
    }
}