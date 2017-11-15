using ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlatform.Areas.CodeManager.ToolHelpers
{
    public class GetClass
    {
        private SchoolContext schooldb = new SchoolContext();//
       
        public IEnumerable<T_Class> LoadNewsData(GetDataGridInfo Info, int CityIDS,string sss, int COUNT, out int total)
        {
            //参数Info用于获取分页需要用的PageSize和PageIndex,total返回结果总数
            //创建news集合
            List<T_Class> GetEnca = schooldb.T_Class.ToList();
            if (COUNT == GetEnca.Count())
            {
                total = GetEnca.Count();
                var Result = GetEnca.OrderBy(c => c.ClassID).Skip(Info.PageSize * (Info.PageIndex - 1)).Take(Info.PageSize);
                return Result;
            }
            else
            {
                if (sss != "--请选择--")
                {
                    List<T_Class> TTT = (from v in schooldb.T_Class where v.EntryYear == CityIDS && v.SpeNo == sss select v).ToList();
                    var Result = TTT.OrderBy(c => c.ClassID).Skip(Info.PageSize * (Info.PageIndex - 1)).Take(Info.PageSize);
                    total = TTT.Count();
                    return Result;
                }
                else {

                    List<T_Class> TTT = (from v in schooldb.T_Class where v.EntryYear == CityIDS select v).ToList();
                    var Result = TTT.OrderBy(c => c.ClassID).Skip(Info.PageSize * (Info.PageIndex - 1)).Take(Info.PageSize);
                    total = TTT.Count();
                    return Result;
                
                }

                


            }

        }
    }
}