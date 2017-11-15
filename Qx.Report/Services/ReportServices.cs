using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using Qx.Report.Configs;
using Qx.Report.Interfaces;
using Qx.Tools;
using Qx.Tools.CommonExtendMethods;

namespace Qx.Report.Services
{
    public  class ReportServices : IReportServices
    {
        private Models.Report _report;
        private List<List<string>> _dataRows;
        private List<List<string>> _dataRowsWithOperat;

        public static List<List<string>> ToTableList2( SqlDataReader reader)
        {
            //所有行
            var rows = new List<List<string>>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //逐行添加内容
                    var cols = new List<string>();
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        var value = "";
                        #region 根据格式取值
                        var type = reader.GetDataTypeName(i).ToLower();
                        if (reader.IsDBNull(i))
                        {
                            value = "";
                        }
                        else if (type.Contains("int"))
                        {
                            var v = reader.GetInt32(i);
                            if (v > 1000000000)
                            {
                                value = reader.GetInt32(i).ToDateTime().ToStr();
                            }
                            else
                            {
                                value = reader.GetInt32(i) + "";
                            }

                        }
                        else if (type.Contains("float"))
                        {
                            value = reader.GetDouble(i) + "";
                        }
                        else if (type.Contains("datetime"))
                        {
                            value = reader.GetDateTime(i).ToString("yyyy-MM-dd HH:mm") + "";
                        }
                        else
                        {

                            value = (reader.GetString(i) + "").
                                   Replace("%", "");//2016-09-05 配合报表转义规则
                            if (value.Contains("%"))
                                throw new Exception("替换不完全！");
                        }
                        #endregion
                        cols.Add(value);
                    }
                    rows.Add(cols);
                }
            }
            reader.Close();
            reader.Dispose();
            return rows;
        }
        public bool Add(Models.Report model)
        {
            var sql = string.Format(@"INSERT INTO [dbo].[T_ReportData]
           ([ReportID]
           ,[ReportName]
           ,[SqlStr]
           ,[HeadFields]
           ,[RecordsPerPage]
           ,[ParaNames]
           ,[ColunmToShow]
           ,[OperateColum]
           )
     VALUES
           ('{0}'
           ,'{1}'
           ,'{2}'
           ,'{3}'
           ,'{4}'
           ,'{5}'
           ,'{6}'
           ,'{7}')", model.ReportID.Replace("'","''"),
                   model.ReportName.Replace("'", "''"),
                                      model.SqlStr.Replace("'", "''"),
                                      model.HeadFields.Replace("'", "''"),
                                      model.RecordsPerPage,
                                      model.ParaNames.Replace("'", "''"),
                                      model.ColunmToShow.Replace("'", "''"),
                                      model.OperateColum.Replace("'", "''")
                                      );
            return sql.ExecuteNonQuery(Setting.ReportRuleConnectionString) > 0;
        }

        public bool Update(Models.Report model)
        {
            var sql = string.Format(@"UPDATE [dbo].[T_ReportData]
               SET [ReportName] = '{1}'
                  ,[SqlStr] =  '{2}'
                  ,[HeadFields] = '{3}'
                  ,[RecordsPerPage] = '{4}'
                  ,[ParaNames] = '{5}'
                  ,[ColunmToShow] = '{6}'
                  ,[OperateColum] = '{7}'
             WHERE [ReportID] = '{0}'", model.ReportID.Replace("'", "''")
                                      , model.ReportName.Replace("'", "''"),
                                      model.SqlStr.Replace("'", "''"),
                                      model.HeadFields.Replace("'", "''"),
                                      model.RecordsPerPage,
                                      model.ParaNames.Replace("'", "''"),
                                      model.ColunmToShow.Replace("'", "''"),
                                      model.OperateColum.Replace("'", "''")
                                      );
           return sql.ExecuteNonQuery(Setting.ReportRuleConnectionString)>0;
        }


        public bool Delete(string id)
        {
            var sql = string.Format(@"DELETE FROM [dbo].[T_ReportData]
            WHERE [ReportID] = '{0}'", id);
            return sql.ExecuteNonQuery(Setting.ReportRuleConnectionString) > 0;
        }

        public Models.Report GetReprot(string id)
        {
            if(string.IsNullOrEmpty(id))
                throw new Exception("请传入报表编号！");
            var sql = string.Format("select ReportID,ReportName,SqlStr,HeadFields,RecordsPerPage,ParaNames,ColunmToShow,OperateColum FROM dbo.T_ReportData  where ReportID='{0}'", id);

            return  Models.Report.ToReport(sql.ExecuteReader(Setting.ReportRuleConnectionString).ToTableList());
        }
      
        private void _ExcuteSql(string id, string parms, string dbConnStringKey)
        {
            _dataRows = new List<List<string>>();
            _dataRowsWithOperat = new List<List<string>>();
            _report = GetReprot(id);//*赋值

            #region 处理报表逻辑
            var sql = "";
            try
            {
                sql = string.Format(_report.SqlStr, FormatString(parms.UnPackString(';')));
            }
            catch (Exception ex)
            {
                //throw ex;
                throw new Exception("异常被捕获：请检查填入的参数个数和Sql中预留的参数个数是否相同！");
            }

            var temp = new List<List<string>>();

            //读取列配置  
            var colunmToShowConfig = _report.ColunmToShow.UnPackString(';').ToList();
            //读取标题
            var title = (_report.HeadFields.UnPackString(';'));
            //读取操作列配置    --列索引:值:内容;列索引:值:内容
            var operateConfigs = _report.OperateColum.UnPackString(';');

            //读取数据行
            var dataRows =
                ToTableList2(//不知为何不能调用dll中的扩展方法!
                sql.ExecuteReader(dbConnStringKey == null ? Setting.ReportDataConnectionString : ConfigurationManager.ConnectionStrings[dbConnStringKey].ConnectionString)
                    );


            //读取操作列
            var operatCol = GetOperatCol(dataRows, operateConfigs);
#endregion
            //插入标题
            temp.Add(title);
            //插入数据行
            temp.AddRange(dataRows);
            //过滤显示列
            temp = temp.FilterRows(colunmToShowConfig);

            _dataRows.AddRange(temp);//*赋值
            //插入操作列
            temp = temp.AddCol(operatCol);

            _dataRowsWithOperat.AddRange(temp);//*赋值
           
        }



        private object[] FormatString(List<string> srcString, char flag = '%')
        {
            return srcString.Select(o => flag + o.Replace("请选择", "") + flag).ToArray();
        }

        #region 获取操作列
        //获取多行的多条配置的操作列
        private List<string> GetOperatCol(List<List<string>> rows, List<string> operateConfigs)
        {
            var dest = new List<string> { "操作" };
            dest.AddRange(rows.Select(row => _GetOperatCol(row, operateConfigs).PackString( ' ')));
            return dest;

        }
        //获取某行的多条配置的操作列
        private List<string> _GetOperatCol(List<string> row, List<string> operateConfigs)
        {
            return operateConfigs.Select(config => __GetOperatColFactory(row, config)).ToList();
        }
        //获取某行的一条配置的操作列
        #region 配置帮助

        // v1.0.0 配置帮助 (按新版本配置，旧版本将在新项目中弃用)

        //说明             参数个数       参数格式                                          
        //无条件无参数         1          内容                                                
        //无条件带一个参数     2          内容:参数1列索引;                                   
        //带条件不带参数       3          条件列索引:条件值:内容;                             
        //带条件带一个参数     4          条件列索引:条件值:内容:参数1列索引;                 
        //带条件带两个参数     5          条件列索引:条件值:内容:参数1列索引:参数2列索引;     


        // v1.0.1 配置帮助

        //类型    说明             配置参数个数      带类型的参数格式
        //  N0    无条件无参数         2             类型:内容
        //  N1    无条件带一个参数     3             类型:内容:参数1列索引;
        //  N2    无条件带两个参数     4             类型:内容:参数1列索引:参数2列索引;
        //  Y0    带条件不带参数       4             类型:条件列索引:条件值:内容;
        //  Y1    带条件带一个参数     5             类型:条件列索引:条件值:内容:参数1列索引;
        //  Y2    带条件带两个参数     6             类型:条件列索引:条件值:内容:参数1列索引:参数2列索引;


       
        #endregion

        private string __GetOperatColFactory(List<string> row, string operateConfig)
        {
            var v1 = __GetOperatCol(row, operateConfig);
            var v2 = __GetOperatColExtend(row, operateConfig);                    
            return v1 + v2;
        }
        #region 转义规则
        //  字符                   转义后
        //   ;                    $semicolon
      
        #endregion
        private string __GetOperatCol(List<string> row, string operateConfig)
        {
            var _operateConfig = operateConfig.Replace("\r\n", "")
                .Replace("$semicolon",";")//2016-09-05 新增转义规则
                .Trim().UnPackString(':');
            var html = "";

            #region 防错预判断
            if ((!(_operateConfig.Count >= 0 &&
                _operateConfig.Count <= 5))||(_operateConfig.Count > 0 &&
               (_operateConfig[0][0] == 'Y' ||
              _operateConfig[0][0] == 'N')))
            {
                return "";
            }

            #endregion


            for (var i = 0; i < row.Count; i++)
            {
                switch (_operateConfig.Count)
                {
                    case 1:
                        {//不带条件直接中断
                            return _operateConfig[0] + "&nbsp;|&nbsp;";
                        } break;
                    case 2:
                        {//不带条件直接中断
                            return string.Format(_operateConfig[0], row[int.Parse(_operateConfig[1])]) + "&nbsp;|&nbsp;";
                        }
                        break;
                    case 3:
                        if (int.Parse(_operateConfig[0]) == i && _operateConfig[1] == row[i])
                        {
                            html += _operateConfig[2] + "&nbsp;|&nbsp;";
                        }
                        break;
                    case 4:
                        if (int.Parse(_operateConfig[0]) == i && _operateConfig[1] == row[i])
                        {
                            html += string.Format(_operateConfig[2], row[int.Parse(_operateConfig[3])]) + "&nbsp;|&nbsp;";
                        }
                        break;
                    case 5:
                        if (int.Parse(_operateConfig[0]) == i && _operateConfig[1] == row[i])
                        {
                            html += string.Format(_operateConfig[2], row[int.Parse(_operateConfig[3])], row[int.Parse(_operateConfig[4])]) + "&nbsp;|&nbsp;";
                        }
                        break;
                }

            }
            return html;
        }

        private string __GetOperatColExtend(List<string> row, string operateConfig)
        {
            var _operateConfig = operateConfig.Replace("\r\n", "")
                 .Replace("$semicolon", ";")//2016-09-05 新增转义规则
                .Trim()
                .UnPackString(':');//UnPackString(operateConfig, ':', true).ToList();
            var html = "";

            #region 防错预判断
            if (!
                (_operateConfig.Count > 0 &&
               (_operateConfig[0][0] == 'Y' ||
              _operateConfig[0][0] == 'N')))
            {
                return "";
            }

            #endregion

            for (var i = 0; i < row.Count; i++)
            {
                switch (_operateConfig[0])
                {
                    case "N0":
                        {//不带条件直接中断
                            return _operateConfig[1] + "&nbsp;|&nbsp;";
                        } break;
                    case "N1":
                        {//不带条件直接中断
                            return string.Format(_operateConfig[1], row[int.Parse(_operateConfig[2])]) + "&nbsp;|&nbsp;";
                        }
                        break;
                    case "N2":
                        {//不带条件直接中断
                            return string.Format(_operateConfig[1], row[int.Parse(_operateConfig[2])], row[int.Parse(_operateConfig[3])]) + "&nbsp;|&nbsp;";
                        }
                        break;
                    case "Y0":
                        if (int.Parse(_operateConfig[1]) == i && _operateConfig[2] == row[i])
                        {
                            html += _operateConfig[3] + "&nbsp;|&nbsp;";
                        }
                        break;
                    case "Y1":
                        if (int.Parse(_operateConfig[1]) == i && _operateConfig[2] == row[i])
                        {
                            html += string.Format(_operateConfig[3], row[int.Parse(_operateConfig[4])]) + "&nbsp;|&nbsp;";
                        }
                        break;
                    case "Y2":
                        if (int.Parse(_operateConfig[1]) == i && _operateConfig[2] == row[i])
                        {
                            html += string.Format(_operateConfig[3], row[int.Parse(_operateConfig[4])], row[int.Parse(_operateConfig[5])]) + "&nbsp;|&nbsp;";
                        }
                        break;
                }

            }
            return html;
        }
        #endregion


        public string ToExcel(string id, string parms, string templateFileDir, string outputFileDir, string dbConnStringKey)
        {
            _ExcuteSql(id, parms, dbConnStringKey);
            return new ExcelUtility(_dataRows, templateFileDir, outputFileDir).ToExcel().FullName;
        }

        public List<List<string>> ToHtml(string id, string parms, string dbConnStringKey)
        {
            _ExcuteSql(id, parms, dbConnStringKey);
            return _dataRowsWithOperat;
        }
    }
}
