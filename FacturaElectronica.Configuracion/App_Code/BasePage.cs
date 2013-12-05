using System;
using System.Data;
using System.Web.UI;

namespace Accendo.ComprobanteElectronico
{
    public class BasePage : Page
    {
        public BasePage()
        {
        }

        static public string GetConnectionString()
        {
            string strResult = string.Empty;

            strResult = XMLConnector.GetConfigurationField("connectionstring");

            return strResult;
        }

        static public DataTable GetLogDataTable()
        {
            DataTable returnDTable = new DataTable();

            returnDTable = MSSQLConnector.GetListOf("CbteError", "ErrorID, CbteID, ErrorSeccion, ErrorDescripcion", null,null ,0);

            returnDTable.Columns["ErrorID"].ColumnName = "ID";
            returnDTable.Columns["CbteID"].ColumnName = "ID Comprobante";
            returnDTable.Columns["ErrorSeccion"].ColumnName = "Seccion";
            returnDTable.Columns["ErrorDescripcion"].ColumnName = "Descripcion";

            return returnDTable;
        }

        static public DataTable GetAFIPTable(string TableName, string Fields, string OrderBy)
        {
            DataTable returnDTable = new DataTable();

            returnDTable = MSSQLConnector.GetListOf(TableName, Fields, null, OrderBy, 0);

            return returnDTable;
        }

        static public DataTable GetAFIPTableItem(string TableName, string TableCondition)
        {
            DataTable returnDTable = new DataTable();

            returnDTable = MSSQLConnector.GetListOf(TableName, "*", TableCondition,null, 0);

            return returnDTable;
        }

        static public DataTable GetCodigoAFIPValues(string TableName)
        {
            DataTable returnDTable = new DataTable();

            returnDTable = MSSQLConnector.GetListOf(TableName, "CodigoAFIP", null,null, 0);

            return returnDTable;
        }

        static public bool SetAFIPTableItem(string TableName, string UpdateQuery, string TableCondition)
        {
            bool bReturn = false;

            bReturn = MSSQLConnector.SetTable(TableName, UpdateQuery, TableCondition);

            return bReturn;
        }

        static public bool InsertAFIPTableItem(string TableName, string InsertQuery)
        {
            bool bReturn = false;

            bReturn = MSSQLConnector.InsertTable(TableName, InsertQuery);

            return bReturn;
        }

        static public bool DeleteAFIPTableItem(string TableName, string DeleteQuery)
        {
            bool bReturn = false;

            bReturn = MSSQLConnector.DeleteTable(TableName, DeleteQuery);

            return bReturn;
        }

        static public DataTable GetComprobanteDataTable(string CbteID)
        {
            try
            {
                DataTable returnDTable = new DataTable();

                returnDTable = MSSQLConnector.GetListOf("CbteCabecera", "*", "CbteID = " + CbteID, "", 0);

                return returnDTable;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        static public DataTable GetComprobanteDetailDataTable(string CbteID)
        {
            try
            {
                DataTable returnDTable = new DataTable();

                returnDTable = MSSQLConnector.GetListOf("CbteLinea", "*", "CbteID = " + CbteID, "", 0);

                return returnDTable;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        static public string GetDescripcionMoneda(string CodMoneda)
        {
            DataTable returnDTable = new DataTable();

            string strResult = string.Empty;

            returnDTable = MSSQLConnector.GetListOf("AFIPMoneda", "Descripcion", "CodigoAFIP = '" + CodMoneda + "'","", 0);

            if(returnDTable != null && returnDTable.Rows.Count > 0)
            {
                strResult = returnDTable.Rows[0]["Descripcion"].ToString();
            }

            return strResult;
        }

        static public string GetDescripcionPais(string CodPais)
        {
            DataTable returnDTable = new DataTable();

            string strResult = string.Empty;

            returnDTable = MSSQLConnector.GetListOf("AFIPPais", "Descripcion", "CodigoAFIP = '" + CodPais + "'", null, 0);

            if (returnDTable != null && returnDTable.Rows.Count > 0)
            {
                strResult = returnDTable.Rows[0]["Descripcion"].ToString();
            }

            return strResult;
        }

        static public string GetComprobanteTipo(string ComprobanteID)
        {
            DataTable returnDTable = new DataTable();

            string strResult = string.Empty;

            returnDTable = MSSQLConnector.GetListOf("CbteCabecera", "Descripcion", "CodigoAFIP = '" + ComprobanteID + "'",null, 0);

            if (returnDTable != null && returnDTable.Rows.Count > 0)
            {
                strResult = returnDTable.Rows[0]["Descripcion"].ToString();
            }

            return strResult;
        }

        static public DataTable GetConfigurationData(string EmpresaID)
        {
            DataTable returnDTable = new DataTable();

            returnDTable = MSSQLConnector.GetListOf("ConfiguracionGeneral inner join Empresas on Empresas.EmpresaID = ConfiguracionGeneral.EmpresaID ", "*", "Empresas.EmpresaID = '" + EmpresaID + "'",null, 0);

            return returnDTable;
        }

        static public DataTable GetEmpresas()
        {
            DataTable returnDTable = new DataTable();

            returnDTable = MSSQLConnector.GetListOf("Empresas", "*", null,null, 0);

            return returnDTable;
        }

        static public bool UpdateConfigurationData(string TableName, string UpdateQuery, string ConditionQuery)
        {
            bool bResult = false;

            bResult = MSSQLConnector.SetTable(TableName, UpdateQuery, ConditionQuery);

            return bResult;
        }
    }
}

