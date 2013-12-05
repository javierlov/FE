using System;
using System.Data;
using System.Data.SqlClient;

namespace Accendo.ComprobanteElectronico
{
    public class MSSQLConnector
    {
        static MSSQLConnector()
        {
        }

        static public DataTable GetListOf(string TableName, string ListOfFields, string FieldCondition, string OrderBy, int iTop)
        {
            SqlDataAdapter dataAdapter = null;
            DataSet dataSet = null;
            DataTable dTable = null;

            string connectionString = string.Empty;
            string commandString = string.Empty;

            try
            {
                connectionString = FacturaElectronica.DBEngine.SQLEngine.GetConnestionStringXMLSetting();                                    
                commandString = "select " + ListOfFields + " from " + TableName;

                if (FieldCondition != null && FieldCondition != string.Empty)
                    commandString += " where " + FieldCondition;

                if (OrderBy != null && OrderBy != string.Empty)
                    commandString += " order by " + OrderBy;

                dataAdapter = new SqlDataAdapter(commandString, connectionString);

                dataSet = new DataSet();

                dataAdapter.Fill(dataSet, TableName);

                dTable = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("FacturaElectronica.MSSQLConnector.GetListOf", ex.Message, 
                    System.Diagnostics.EventLogEntryType.Error);
                throw (ex);
            }

            return dTable;
        }

        static private string GetRegistryKey(string RegPath, string regKey)
        {
            return FacturaElectronica.DBEngine.SQLEngine.GetConnestionStringXMLSetting();

            /*
            string strResult = string.Empty;

            RegistryKey key1 = null;

            key1 = Registry.LocalMachine.OpenSubKey(RegPath);

            if (key1 == null)
                throw (new Exception("Could not load registry key"));

            strResult = Convert.ToString(key1.GetValue(regKey));

            return strResult;
             */
        }

        static public bool SetTable(string TableName, string UpdateQuery, string ConditionQuery)
        {
            SqlCommand dbQuery = new SqlCommand();
            SqlConnection dbConnection = new SqlConnection();

            string connectionString = string.Empty;
            string cmd = string.Empty;

            connectionString = FacturaElectronica.DBEngine.SQLEngine.GetConnestionStringXMLSetting();                
            dbConnection.ConnectionString = connectionString;
            dbConnection.Open();

            cmd = "update " + TableName + " set " + UpdateQuery + " where " + ConditionQuery;

            dbQuery.Connection = dbConnection;
            dbQuery.CommandText = cmd;
            dbQuery.ExecuteNonQuery();

            dbConnection.Close();

            bool strResult = false;

            return strResult;
        }

        static public bool InsertTable(string TableName, string UpdateQuery)
        {
            SqlCommand dbQuery = new SqlCommand();
            SqlConnection dbConnection = new SqlConnection();

            string connectionString = string.Empty;
            string cmd = string.Empty;

            connectionString = FacturaElectronica.DBEngine.SQLEngine.GetConnestionStringXMLSetting();                
            dbConnection.ConnectionString = connectionString;
            dbConnection.Open();

            cmd = "insert into " + TableName + " " + UpdateQuery;

            dbQuery.Connection = dbConnection;
            dbQuery.CommandText = cmd;
            dbQuery.ExecuteNonQuery();

            dbConnection.Close();

            bool strResult = false;

            return strResult;
        }

        static public bool DeleteTable(string TableName, string DeleteQuery)
        {
            SqlCommand dbQuery = new SqlCommand();
            SqlConnection dbConnection = new SqlConnection();

            string connectionString = string.Empty;
            string cmd = string.Empty;

            connectionString = FacturaElectronica.DBEngine.SQLEngine.GetConnestionStringXMLSetting();                
            dbConnection.ConnectionString = connectionString;
            dbConnection.Open();

            cmd = "delete from " + TableName + " where " + DeleteQuery;

            dbQuery.Connection = dbConnection;
            dbQuery.CommandText = cmd;
            dbQuery.ExecuteNonQuery();

            dbConnection.Close();

            bool strResult = false;

            return strResult;
        }
    }
}