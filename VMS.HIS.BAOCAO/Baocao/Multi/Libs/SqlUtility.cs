using SubSonic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VMS.HIS.DAL;

namespace VNS.HandyTools
{
    public class SqlData
    {
        public string ColName { get; set; } 
        public string Value { get; set; }

        public SqlData(string sColName, string sValue)
        {
            ColName = sColName;
            Value = sValue;
        }
    }

    public class SqlUtility
    {
       
        static SqlUtility()
        {
           
        }

        public static string SetValueText(string Value)
        {
            return "N'" + Value.Replace("'","''") + "'";
        }

        //myConnection.InfoMessage += new SqlInfoMessageEventHandler(myConnection_InfoMessage);

        //private static void myConnection_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        //{
        //    sSqlQueryInfoMessage += e.Message;
        //}

        public static DataSet GetDataSet(string Query)
        {
            QueryCommand cmd = SysMultiReport.CreateQuery().BuildCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandSql = Query;
            return DataService.GetDataSet(cmd);
        }

        //private static string sSqlQueryInfoMessage = string.Empty;
        public static void ExecuteQuery(string Query)
        {
            QueryCommand cmd = SysMultiReport.CreateQuery().BuildCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandSql = Query;
            DataService.ExecuteQuery(cmd);
        }




        public static object InsertScopeIdentity(string TableName, List<SqlData> SqlData)
        {
            string sColumn = string.Join(",", (from data in SqlData select data.ColName).ToArray());
            string sValue = string.Join(",", (from data in SqlData select data.Value).ToArray());
            string sQuery = string.Format("insert into {0} ({1}) values ({2}) select SCOPE_IDENTITY()", TableName,sColumn, sValue);
            QueryCommand cmd = SysMultiReportConfig.CreateQuery().BuildCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandSql = sQuery;
            return DataService.ExecuteScalar(cmd);

        }

        public static void InsertNoId(string TableName, List<SqlData> SqlData)
        {
            string sColumn = string.Join(",", (from data in SqlData select data.ColName).ToArray());
            string sValue = string.Join(",", (from data in SqlData select data.Value).ToArray());
            string sQuery = string.Format("insert into {0} ({1}) values ({2})", TableName, sColumn, sValue);
            ExecuteQuery(sQuery);
        }

        public static void Update(string TableName, List<SqlData> SqlData, string StrWhere)
        {
            string s = string.Join(",", (from data in SqlData select data.ColName + " = " + data.Value).ToArray());
            string sQuery = string.Format("update {0} set {1} where {2}", TableName, s,StrWhere);
            QueryCommand cmd = SysMultiReportConfig.CreateQuery().BuildCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandSql = sQuery;
            DataService.ExecuteQuery(cmd);
        }
        
        public static void Delete(string TableName, string StrWhere)
        {
            string sQuery = string.Format("delete from {0} where {1}", TableName, StrWhere);
            ExecuteQuery(sQuery);
        }

        public static string GetVerifiedDbColNameString(string ColName)
        {
            try
            {
                return new string(ColName.Where(c => char.IsLetter(c) || char.IsDigit(c) || c == '_' || c == ' ' || c == ','
                     || c == '#' || c == '(' || c == ')' || c == '@' || c == '[' || c == ']'
                     || c == '$' || c == '%' || c == '/' || c == ':' || c == '-' || c == ';').ToArray());
            }
            catch (Exception)
            {
                return ColName;
            }
        }
    }
}
