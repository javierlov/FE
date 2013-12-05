using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using FacturaElectronica.Utils;
using FacturaElectronica.Common;

namespace FacturaElectronica.Service.Engine
{
    public class IntegrationEngine
    {
        public ResponseBatch ProcessRequest(RequestBatch DocumentBatchRequest, Settings oSettings, bool IsReprocessed)
        {
            FEWS.FEServices fews = new FEWS.FEServices();
            fews.Url = oSettings.UrlFEWebService;
            fews.Credentials = System.Net.CredentialCache.DefaultCredentials;
            fews.Timeout = 300000;

            ResponseBatch dbResponse = new ResponseBatch();

            string strResponse =string.Empty;
            
            try
            {
                if (DocumentBatchRequest != null && DocumentBatchRequest.RequestHeaders.Count > 0)
                {
                    // procesar comprobante
                    switch (DocumentBatchRequest.RequestHeaders[0].TipoTransaccion)
                    {
                        case "0":
                        case "1":
                            strResponse = fews.ProcesarLoteFacturasBienesServicios(oSettings.EmpresaID, DocumentBatchRequest.GetXMLString());
                            break;
                        case "2":
                            strResponse = fews.ProcesarLoteFacturasBienesCapital(oSettings.EmpresaID, DocumentBatchRequest.GetXMLString());
                            break;
                        case "3":
                            strResponse = fews.ProcesarLoteFacturasExportacion(oSettings.EmpresaID, DocumentBatchRequest.GetXMLString());
                            break;
                        default:
                            strResponse = string.Empty;
                            break;
                    }

                    dbResponse.LoadXMLString(strResponse);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Servicio Factura Electronica", "ProcessRequest.Error" + ex.Message, System.Diagnostics.EventLogEntryType.Warning);
            }

            return dbResponse;
        }

        static public string ImporteConDecimales(string importeSinDecimales, int qtyDecimales)
        {
            string result = "0";

            int count = importeSinDecimales.Length;
            if (count > qtyDecimales)
                result = importeSinDecimales.Substring(0, count - qtyDecimales) + "," + importeSinDecimales.Substring(count - qtyDecimales, qtyDecimales);
            else
                result = importeSinDecimales;

            if (result.Length == 0)
                result = "0";

            return result;
        }

        static public string ChangeDecimalPointToPoint(string number)
        {
            string result = "0";
            int commaPos = number.IndexOf(",");
            if (commaPos > 0)
            {
                result = number.Substring(0, commaPos) + "." + number.Substring(commaPos + 1, number.Length - commaPos - 1);
            }
            else
                result = number;

            return number;
        }

    }
}
