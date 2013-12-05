using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;

namespace FacturaElectronica.WebServices
{
    public class Common : Page
    {
        static DBEngine.SQLEngine sqlEngine = new DBEngine.SQLEngine();

        public Common()
        {
        }

        static public DataTable GetComprobanteDataTable(string CbteID)
        {
            try
            {
                DataTable returnDTable = new DataTable();

                returnDTable = sqlEngine.GetItems("CbteCabecera", "*", "CbteID = " + CbteID, 0);

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

                returnDTable = sqlEngine.GetItems("CbteLinea", "*", "CbteID = " + CbteID, 0);

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

            returnDTable = sqlEngine.GetItems("AFIPMoneda", "Descripcion", "CodigoAFIP = '" + CodMoneda + "'", 0);

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

            returnDTable = sqlEngine.GetItems("AFIPPais", "Descripcion", "CodigoAFIP = '" + CodPais + "'", 0);

            if (returnDTable != null && returnDTable.Rows.Count > 0)
            {
                strResult = returnDTable.Rows[0]["Descripcion"].ToString();
            }

            return strResult;
        }

        static public DateTime GetDateValue(string DateValue)
        {
            DateTime dTime = new DateTime();

            try
            {
                dTime = Convert.ToDateTime("15/01/2010");

                dTime = Convert.ToDateTime(DateValue);
            }
            catch
            {
                if (DateValue.Split('/').Length > 2)
                {
                    dTime = Convert.ToDateTime(DateValue.Split('/')[1] + "/" + DateValue.Split('/')[0] + "/" + DateValue.Split('/')[2]);
                }
                else
                {
                    dTime = DateTime.Now;
                }
            }

            return dTime;
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

        static public string GetBarCode(string CUIT, string TipoComprobante, string PuntoVenta, string CAE, string FechaVencimientoPago)
        {
            string strResult = string.Empty;
            string strAuxCode = CUIT.Trim() + TipoComprobante.Trim() + PuntoVenta.Trim() + CAE.Trim() + FechaVencimientoPago.Trim();

            int iEtapa1 = 0;
            int iEtapa2 = 0;
            int iEtapa3 = 0;
            int iEtapa4 = 0;
            int iEtapa5 = 0;

            //Digito Verificador
            //Etapa 1
            for (int i = 0; i < strAuxCode.Length; i++)
            {
                iEtapa1 += Convert.ToInt16(strAuxCode[i].ToString());

                i++;
            }

            //Etapa 2
            iEtapa2 = iEtapa1 * 3;

            //Etapa 3
            for (int i = 1; i < strAuxCode.Length - 1; i++)
            {
                iEtapa3 += Convert.ToInt16(strAuxCode[i].ToString());

                i++;
            }

            //Etapa 4
            iEtapa4 = iEtapa2 + iEtapa3;

            //Etapa 5
            iEtapa5 = 10 - (iEtapa4 - (iEtapa4 / 10 * 10));

            strResult = ((iEtapa5 == 10) ? 0 : iEtapa5).ToString();

            return strAuxCode + strResult;
        }
    }
}

