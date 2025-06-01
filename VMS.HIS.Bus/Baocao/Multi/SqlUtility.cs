using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace VietBaIT.HandyTools
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
        public static string SqlConnectionString;

        static SqlUtility()
        {
            string SERVERADDRESS = Utility.ReadXMLtoString("config.xml", "Config", "SERVERADDRESS");
            string DATABASE_ID = Utility.ReadXMLtoString("config.xml", "Config", "DATABASE_ID");
            string USERNAME = Utility.ReadXMLtoString("config.xml", "Config", "USERNAME");
            string PASSWORD = Utility.ReadXMLtoString("config.xml", "Config", "PASSWORD");
            SqlConnectionString = string.Format("Data Source = {0};Initial Catalog= {1};User Id = {2};Password= {3};Pooling=false", SERVERADDRESS, DATABASE_ID, USERNAME, PASSWORD);
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
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(SqlConnectionString))
            using (SqlCommand command = new SqlCommand(Query, conn))
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                dataAdapter.Fill(ds);
            return ds;
        }

        //private static string sSqlQueryInfoMessage = string.Empty;
        public static void ExecuteQuery(string Query)
        {
            using (var conn = new SqlConnection(SqlConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                //conn.InfoMessage += myConnection_InfoMessage;
                conn.Open();
                cmd.CommandText = Query;
                cmd.ExecuteNonQuery();
            }
        }



        public static object ExecuteScalar(string Query)
        {
            object id ;
            using (var conn = new SqlConnection(SqlConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = Query;
                id = cmd.ExecuteScalar();
            }
            return id;
        }

        public static object InsertScopeIdentity(string TableName, List<SqlData> SqlData)
        {
            string sColumn = string.Join(",", (from data in SqlData select data.ColName).ToArray());
            string sValue = string.Join(",", (from data in SqlData select data.Value).ToArray());
            string sQuery = string.Format("insert into {0} ({1}) values ({2}) select SCOPE_IDENTITY()", TableName,sColumn, sValue);
            return ExecuteScalar(sQuery);
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
            ExecuteQuery(sQuery);
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
