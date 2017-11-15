using Qx.CPQ.Entity;
using Qx.CPQ.Models;
using Qx.Wbs.Services;
using System.Collections.Generic;
using System.Linq;
using Qx.Tools.CommonExtendMethods;
namespace Qx.CPQ.Services
{
    public class CpqService : BaseService
    {

        //通过问卷ID查询出整个问卷的信息（包括问题和问题选项）,这些信息一般是预览试卷的时候用到的
        public Questionnaire QuestionnaireInfo(string QuestionnaireID)
        {
            Questionnaire Questionnaire =
                Db.CPQ_T_Questionnaire.
                Where(z => z.QuestionnaireID == QuestionnaireID).
                Select(a => new Questionnaire()
                {

                    QuestionnaireID = a.QuestionnaireID,
                    QuestionnaireName = a.QuestionnaireName,
                    Summarize = a.Summarize,
                    share=(int)a.share,
                    CollectNumber=(int)a.CollectNumber,
                    CollectSeting=(int)a.CollectSeting,
                    CreateTime=a.CreateTime,
                    IsLock=(int)a.IsLock,
                    OwnerCompany=a.OwnerCompany,
                    OwnerID=a.OwnerID,
                    QuestionnaireDomain=a.QuestionnaireDomain,
                    Reference=(int)a.Reference,
                    Status=a.Status,

                    Questions = a.CPQ_T_AttachQuestionToQuestionnaire.Select(b => new Question()
                    {
                        Sequence = b.QuestionSequenceID,
                        QuestionName = b.CPQ_T_Question.QuestionName,
                        QuestionType = b.CPQ_T_Question.QuestionType,
                        QuestionDomain=b.CPQ_T_Question.QuestionDomain,
                        Reference = (int)b.CPQ_T_Question.Reference,
                        share = (int)b.CPQ_T_Question.share,
                        QuestionID=b.QuestionID,
                        
                        QuestionOptions = b.CPQ_T_Question.CPQ_T_QuestionOption.Select(c => new QuestionOption()
                        {   OptionID=c.QuestionOptionID,
                            OptionName = c.QuestionOptionIName,
                            Sequence = c.SequenID
                        }).OrderBy(d => d.Sequence).ToList(),

                    }).OrderBy(e => e.Sequence).ToList(),
                }).FirstOrDefault();
            return Questionnaire;
        }



        //通过问题ID查询出整个问题的信息（包括问题名和问题选项）,这些信息一般是预览问题的时候用到的
        public Question QuestionInfo(string QuestionID) {
            Question Question =
                 Db.CPQ_T_Question.
                 Where(z => z.QuestionID == QuestionID).
                 Select(a => new Question()
                 {
                     QuestionDomain=a.QuestionDomain,
                     Reference=(int)a.Reference,
                     share=(int)a.share,
                     QuestionID = a.QuestionID,
                     QuestionType = a.QuestionType,
                     QuestionName = a.QuestionName,
                     

                     QuestionOptions = a.CPQ_T_QuestionOption.Select(b => new QuestionOption()
                     {
                         OptionName = b.QuestionOptionIName,
                         Sequence = b.SequenID


                     }).OrderBy(d => d.Sequence).ToList(),

                 }).FirstOrDefault();

            return Question;
        }

        //通过问题ID查询出问题的答案，展示问题答案的时候用
        public QuestionAnalyticResult ShowQuestionAnswer(string QuestionID)
        {

            QuestionAnalyticResult qar =
             Db.CPQ_T_Question.
             Where(z => z.QuestionID == QuestionID).
             Select(a => new QuestionAnalyticResult()
             {
                 QuestionName = a.QuestionName,
                 QuestionType = a.QuestionType,//问题类型
                 answers = a.CPQ_T_QuestionOption.Select(b => new QuestionOption()//问题包含的选项以及答案，答案在下面赋值
                 {
                     OptionID = b.QuestionOptionID,
                     OptionName = b.QuestionOptionIName,
                     Sequence = b.SequenID
                 }).OrderBy(d => d.Sequence).ToList(),
             }).FirstOrDefault();


            //答案里面存的是选项表里的选项ID

            List<string> answer = (from a in Db.CPQ_T_AnswerSheet where a.QuestionID == QuestionID select a.Answer).ToList();
            if (answer != null)
            {
                if (qar.QuestionType == 1)//如果是单选题，则答案是选项ID，例如1212331231
                {
                    foreach (var temp in answer)
                    {//对于每一个答案，从QuestionAnalyticResult_M里遍历只要选项ID一样，选项的被选择数+1

                        foreach (var option in qar.answers)
                        {
                            if (option.OptionID == temp)
                            {
                                option.selectedNumber++;
                            }
                        }

                    }
                }
                else if (qar.QuestionType == 2)//如果是多选题，则答案是选项ID，例如1212331231;232323232表示他选了这两个选项,答案之间用分号隔开
                {
                    foreach (var temp in answer)
                    {
                        string[] temps = temp.Split(';');//先把多选题的多个答案变成数组，剩下的做法了单选题的一样
                        for (int i = 0; i < temps.Length; i++)
                        {
                            foreach (var option in qar.answers)
                            {
                                if (option.OptionID == temps[i])
                                {
                                    option.selectedNumber++;
                                }
                            }
                        }
                    }

                }
                else if (qar.QuestionType == 3)//如果是填空题，则答案是就是答案，例如“我觉得这个平台的想法设置很棒，我喜欢！”所以最后就是每一个答案连起来
                {
                    int count = 1;//用来计算这是第几个人的答案
                    qar.fill_Answer = "";
                    foreach (var temp in answer)
                    {//对于每一个答案，从QuestionAnalyticResult_M里遍历只要选项ID一样，选项的被选择数+1
                        qar.fill_Answer = qar.fill_Answer + "答案" + count + ": " + temp + ";";
                        count++;
                    }
                }
            }


            return qar;
        }


        //获取所有的问卷领域的list
        public List<CPQ_C_QuestionnaireDomain> GetQuestionnaireDomainList()
        {
            List<CPQ_C_QuestionnaireDomain> QuestionnaireDomain = Db.CPQ_C_QuestionnaireDomain.ToList();
            return QuestionnaireDomain;
        }

        //获取所有的分享类型的list
        public List<CPQ_C_Share> GetShareList()
        {
            List<CPQ_C_Share> Share = Db.CPQ_C_Share.ToList();
            return Share;
        }

        //获取问题领域的list
        public List<CPQ_C_QuestionDomain> GetQuestionDomainList()
        {
            List<CPQ_C_QuestionDomain> QuestionDomain = Db.CPQ_C_QuestionDomain.ToList();
            return QuestionDomain;
        }

        //获取问题类型的list
        public List<CPQ_C_QuestionType> GetQuestionTypeList()
        {
            List<CPQ_C_QuestionType> QuestionType = Db.CPQ_C_QuestionType.ToList();
            return QuestionType;
        }

        //根据问卷ID获取问卷题目
        public string GetQuestionnaireTitle(string QuestionnaireID)
        {
            string QuestionnaireTitle = Db.CPQ_T_Questionnaire.Find(QuestionnaireID).QuestionnaireName;
            return QuestionnaireTitle;
        }
        //根据问卷ID获取问卷简介
        public string  GetQuestionnairesummary(string QuestionnaireID)
        {
            string Questionnairesummary = Db.CPQ_T_Questionnaire.Find(QuestionnaireID).Summarize;
            return Questionnairesummary;
        }

        //根据问卷ID获取问卷的收集数
        public int GetQuestionnaireCollectNumber(string QuestionnaireID)
        {
            int CollectNumber = Db.CPQ_T_Questionnaire.Find(QuestionnaireID).CollectNumber.Value;
            return CollectNumber;
        }

        public List<QuestionAnalyticResult> GetQuestionnaireAnswer(string QuestionnaireID)
        {
            List<QuestionAnalyticResult> result = new List<QuestionAnalyticResult>();
            List<string> QuestionIDs = (from a in Db.CPQ_T_AttachQuestionToQuestionnaire where a.QuestionnaireID == QuestionnaireID select a.QuestionID).ToList();
            //找出问卷上所有的问题对于每一个问题调用一次ShowQuestionAnswer获取每一个问题的答案
            foreach (var QuestionID in QuestionIDs)
            {
                result.Add(ShowQuestionAnswer(QuestionID));
                
            }
            return result;
        }
    }
}