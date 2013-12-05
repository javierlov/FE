using System;
using System.Data;
using System.Web.UI;
using Microsoft.Win32;
using System.Configuration;

namespace FacturaElectronica
{
    public class BasePage : Page
    {
        public BasePage()
        {
        }
        

        static public DataTable GetLogErrorData(string CbteID)
        {
            DataTable returnDTable = new DataTable();

            returnDTable = MSSQLConnector.GetListOf("CbteError", "ErrorID, ErrorSeccion, ErrorDescripcion", "CbteID = " + CbteID, "CbteID", 0);

            returnDTable.Columns["ErrorID"].ColumnName = "ID";
            returnDTable.Columns["ErrorSeccion"].ColumnName = "Seccion";
            returnDTable.Columns["ErrorDescripcion"].ColumnName = "Descripcion";

            return returnDTable;
        }

        static public DataTable GetCbteLogData(string CbteID, string NumeroLegal, string CbteTipo, string NumeroSAP)
        {
            DataTable returnDTable = new DataTable();
            
            string strPuntoVenta = string.Empty;
            string strNroComprobanteDesde = string.Empty;

            strPuntoVenta = NumeroLegal.Substring(0, 4);
            strNroComprobanteDesde = NumeroLegal.Substring(4, 8);

            returnDTable = MSSQLConnector.GetListOf("CbteCabecera", "CbteID, FechaComprobante, RTRIM(dbo.CbteCabecera.PuntoVenta) + RTRIM(dbo.CbteCabecera.LetraComprobante) + RTRIM(dbo.CbteCabecera.NroComprobanteDesde) AS [Nro. Legal], EstadoTransaccion, NombreObjetoSalida", "CbteID <> " + CbteID + " and PuntoVenta = '" + strPuntoVenta + "' and NroComprobanteDesde = '" + strNroComprobanteDesde + "' and TipoComprobante = '" + CbteTipo + "'", "CbteID", 0);

            returnDTable.Columns["CbteID"].ColumnName = "ID";
            returnDTable.Columns["FechaComprobante"].ColumnName = "Fecha";
            returnDTable.Columns["Nro. Legal"].ColumnName = "Nro. Legal";
            returnDTable.Columns["EstadoTransaccion"].ColumnName = "Estado";
            returnDTable.Columns["NombreObjetoSalida"].ColumnName = "Nombre Archivo de Respuesta AFIP";

            return returnDTable;
        }

        static public DataTable GetLogDataTable()
        {
            DataTable returnDTable = new DataTable();

            returnDTable = MSSQLConnector.GetListOf("Comprobantes", "*", null, "ID", 0);

            returnDTable.Columns.Add("Original").SetOrdinal(11);
            returnDTable.Columns.Add("Duplicado").SetOrdinal(12);
            returnDTable.Columns.Add("Triplicado").SetOrdinal(13);
            returnDTable.Columns["Nombre Archivo de Respuesta AFIP"].SetOrdinal(14);  

            return returnDTable;
        }

        static public DataTable GetComprobanteDataTable(string CbteID)
        {
            DataTable returnDTable = new DataTable();

            returnDTable = MSSQLConnector.GetListOf("CbteCabecera", "*", "CbteID = " + CbteID, "CbteID", 0);

            return returnDTable;
        }

        static public DataTable GetComprobanteDetailDataTable(string CbteID)
        {
            DataTable returnDTable = new DataTable();

            returnDTable = MSSQLConnector.GetListOf("CbteLinea", "*", "CbteID = " + CbteID, null, 0);

            return returnDTable;
        }

        static public DataTable GetUserData(string DBServer, string DBName, string TableName, string UserName)
        {
            DataTable returnDTable = new DataTable();

            returnDTable = MSSQLConnector.GetListOf(DBServer, DBName, TableName, "*", "ETERC = '" + UserName + "'", 0);

            return returnDTable;
        }               

        static public string GetRegistryKey(string RegPath, string regKey)
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

        static public string GetDescripcionMoneda(string CodMoneda)
        {
            DataTable returnDTable = new DataTable();

            string strResult = string.Empty;

            returnDTable = MSSQLConnector.GetListOf("AFIPMoneda", "Descripcion", "CodigoAFIP = '" + CodMoneda + "'", null, 0);

            if (returnDTable != null && returnDTable.Rows.Count > 0)
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

        static public string GetDateStringValue(string DateFrom)
        {
            DateTime dTime = new DateTime();

            string strResult = string.Empty;

            string DateValue = DateFrom.Split(' ')[0];

            try
            {
                dTime = Convert.ToDateTime("15/01/2010");

                strResult = DateValue.Split('/')[0] + "/" + DateValue.Split('/')[1] + "/" + DateValue.Split('/')[2];
            }
            catch
            {
                if (DateValue.Split('/').Length > 2)
                {
                    strResult = DateValue.Split('/')[1] + "/" + DateValue.Split('/')[0] + "/" + DateValue.Split('/')[2];
                }
                else
                {
                    strResult = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                }
            }

            return strResult;
        }
    }
}

