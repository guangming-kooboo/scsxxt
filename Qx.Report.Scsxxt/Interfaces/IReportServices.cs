using System.Collections.Generic;
using Qx.Report.Scsxxt.Models;

namespace Qx.Report.Scsxxt.Interfaces
{
    public interface IReportServices
    {
        bool Add(Models.Report model);
        bool Update(Models.Report model);
        bool Delete(string id);
        Models.Report GetReprot(string id);

        string ToExcel(string id, string parms, string templateFileDir, string outputFileDir,
            List<List<string>> dataRows);

        List<List<string>> ToHtml(string id, string parms, List<List<string>> dataRows);
        List<List<string>> GetDbDataSource(string id, string parms, string dbConnStringKey);
        string ToExcel(string id, string parms, string templateFileDir, string outputFileDir, string dbConnStringKey);
        List<List<string>> ToHtml(string id, string parms, string dbConnStringKey);
        List<List<string>> CrossDb(string id, string parms, List<List<string>> dataRows, List<CrossDbParam> paramList);
    }
}