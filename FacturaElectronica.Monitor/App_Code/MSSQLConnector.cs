using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using Microsoft.Win32;

namespace FacturaElectronica
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
                    commandString += " Order BY " + OrderBy + " DESC";

                dataAdapter = new SqlDataAdapter(commandString, connectionString);

                dataSet = new DataSet();

                dataAdapter.Fill(dataSet, TableName);

                dTable = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
            }

            return dTable;
        }

        static public DataTable GetListOf(string DBServer, string DBName, string TableName, string ListOfFields, string FieldCondition, int iTop)
        {
            SqlCommand dbQuery = new SqlCommand();
            SqlConnection dbConnection = new SqlConnection();

            DataTable dTable = new DataTable();

            string commandString = string.Empty;

            try
            {
                commandString = "select " + ListOfFields + " from " + TableName;

                if (FieldCondition != null && FieldCondition != string.Empty)
                    commandString += " where " + FieldCondition;

                dbConnection.ConnectionString = "server=" + DBServer + ";Integrated Security=true;Database=" + DBName;
                dbConnection.Open();

                dbQuery.Connection = dbConnection;
                dbQuery.CommandText = commandString;

                dTable.Load(dbQuery.ExecuteReader());

                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dTable;
        }

        static public bool UpdateTable()
        {
            bool bResult = false;

            //string cmd = string.Empty;
            //cmd = "update ConfiguracionGeneral set ";
            //cmd += "EstadoTransaccion = '" + estadoDocumento + "', ";
            //cmd += "AFIPCodigoError = '" + afipCodigoError + "', ";
            //cmd += "AFIPMensajeError = '" + afipMensajeError + "', ";
            //cmd += "CAE = '" + cae + "', ";
            //cmd += "FechaVencimiento = '" + fechaVencimiento + "' ";
            //cmd += "where CbteID = " + SQLID;

            return bResult;
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

        static public DataTable GetListInnerJoinOf(string TableName1, string TableName2, string ListOfFields, string FieldCondition, string InnerJoinCondition, int iTop)
        {
            SqlDataAdapter dataAdapter = null;
            DataSet dataSet = null;
            DataTable dTable = null;

            string connectionString = string.Empty;
            string commandString = string.Empty;

            try
            {
                connectionString = FacturaElectronica.DBEngine.SQLEngine.GetConnestionStringXMLSetting();                    
                /*
                SELECT Employee.Username 
                FROM Employee INNER JOIN Project 
                ON Employee.EmployeeID = Project.EmployeeID 
                WHERE Employee.City = 'Boston' 
                AND Project.ProjectName = 'Hardwork'; 
                */

                commandString = "SELECT " + ListOfFields + " FROM " + TableName1 + " INNER JOIN " + TableName2 + " ON " + InnerJoinCondition;

                if (FieldCondition != null && FieldCondition != string.Empty)
                    commandString += " WHERE " + FieldCondition;

                dataAdapter = new SqlDataAdapter(commandString, connectionString);

                dataSet = new DataSet();

                dataAdapter.Fill(dataSet, TableName1);

                dTable = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
            }

            return dTable;
        }
    }
}