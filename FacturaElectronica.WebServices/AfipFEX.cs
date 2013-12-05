using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FacturaElectronica.Common;
using FacturaElectronica.Utils;

namespace FacturaElectronica.WebServices
{
    public class AfipFEX
    {
        wsfex.Service fexService = new wsfex.Service();
        wsfex.ClsFEXAuthRequest objFEXAuthRequest = new  wsfex.ClsFEXAuthRequest();

        public AfipFEX(ref wsfex.Service afipFexService, ref wsfex.ClsFEXAuthRequest afipObjFEXAuthRequest, Settings oSettings)
        {
            fexService = afipFexService;
            fexService.Url = oSettings.UrlAFIPwsfex;
            objFEXAuthRequest = afipObjFEXAuthRequest;
        }

        public wsfex.FEXResponse_LastID GetLastBatchUniqueId()
        {
            try
            {
                wsfex.FEXResponse_LastID objFEXLastID = new wsfex.FEXResponse_LastID();
                objFEXLastID = fexService.FEXGetLast_ID(objFEXAuthRequest);
                return objFEXLastID;
            }
            catch (Exception ex)
            {
                throw (new Exception("GetLastID. Error:" + ex.Message));
            }
        }

        public wsfex.FEXResponseAuthorize FEXAuthRequest(RequestBatch docBatch, Settings oSettings )
        {           
            wsfex.ClsFEXRequest objFEXRequest = new wsfex.ClsFEXRequest();
            wsfex.FEXResponseAuthorize objFEXResponseAuthorize = new wsfex.FEXResponseAuthorize();
            wsfex.FEXResponse_LastID objFEXResponseLastID = null;

            DBEngine.SQLEngine sqlEngine = new FacturaElectronica.DBEngine.SQLEngine();

            int i = 0;

            try
            {
                //Debug Line
                Utils.Utils.DebugLine(Utils.Utils.SerializeObject(objFEXAuthRequest), oSettings.PathDebug + "\\ClsFEXAuthRequest-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + DateTime.Now.Millisecond.ToString("000") + ".xml");

                //Si no tiene identificador único hay que generarlo
                if (docBatch.BatchUniqueId == "AUTO")
                {
                    objFEXResponseLastID = GetLastBatchUniqueId();
                    if (objFEXResponseLastID.FEXResultGet == null)
                    {
                        sqlEngine.LogError(docBatch.RequestHeaders[0].SQLID, "0", "Autorización", "Error AFIP al obtener el último nro de requerimiento (" + objFEXResponseLastID.FEXErr.ErrCode + ") " + objFEXResponseLastID.FEXErr.ErrMsg);
                    }
                    else
                    {
                        objFEXResponseLastID.FEXResultGet.Id = objFEXResponseLastID.FEXResultGet.Id + 1;
                        docBatch.BatchUniqueId = (objFEXResponseLastID.FEXResultGet.Id).ToString();

                        //Guardar Unique Batch ID que luego se utilizara para reprocesos y obtener CAE
                        sqlEngine.UpdateCabeceraBatchUniqueId(docBatch.RequestHeaders[0].SQLID, docBatch.BatchUniqueId);
                    }
                }

                //Debug Line
                Utils.Utils.DebugLine(Utils.Utils.SerializeObject(docBatch), oSettings.PathDebug + "\\DocumentBatch-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + DateTime.Now.Millisecond.ToString("000") + ".xml");

                string fieldName = "";
                string seccionName = "";

                try
                {
                    seccionName = "Cabecera";
                    objFEXRequest.Id = (long)Convert.ToDouble(docBatch.BatchUniqueId);
                    objFEXRequest.Tipo_cbte = Convert.ToInt16(docBatch.RequestHeaders[0].TipoComprobante);
                    objFEXRequest.Punto_vta = Convert.ToInt16(docBatch.RequestHeaders[0].PuntoVenta);
                    objFEXRequest.Cbte_nro = (long)Convert.ToDouble(docBatch.RequestHeaders[0].NroComprobanteDesde);
                    objFEXRequest.Tipo_expo = Convert.ToInt16((docBatch.RequestHeaders[0].TipoExportacion == string.Empty) ? "1" : docBatch.RequestHeaders[0].TipoExportacion);
                    
                    if (objFEXRequest.Tipo_cbte == 19)
                        objFEXRequest.Permiso_existente = (docBatch.RequestHeaders[0].PermisoExistente == string.Empty) ? "N" : docBatch.RequestHeaders[0].PermisoExistente;
                    else
                        objFEXRequest.Permiso_existente = string.Empty;
                    
                    objFEXRequest.Dst_cmp = Convert.ToInt16(docBatch.RequestHeaders[0].CompradorPais);
                    objFEXRequest.Cliente = docBatch.RequestHeaders[0].CompradorRazonSocial;
                    objFEXRequest.Domicilio_cliente = docBatch.RequestHeaders[0].CompradorDireccion;
                    objFEXRequest.Moneda_Id = docBatch.RequestHeaders[0].CodigoMoneda;
                    objFEXRequest.Moneda_ctz = Convert.ToDouble(docBatch.RequestHeaders[0].TasaCambio);
                    objFEXRequest.Imp_total = Convert.ToDouble(docBatch.RequestHeaders[0].ImporteMonedaFacturacion);
                    objFEXRequest.Idioma_cbte = Convert.ToInt16((docBatch.RequestHeaders[0].Idioma == string.Empty) ? "01" : docBatch.RequestHeaders[0].Idioma);
                    objFEXRequest.Id_impositivo = docBatch.RequestHeaders[0].CompradorNroDocumento;
                    objFEXRequest.Fecha_cbte = Convert.ToDateTime(docBatch.RequestHeaders[0].FechaComprobante).ToString("yyyyMMdd");
                    objFEXRequest.Forma_pago = docBatch.RequestHeaders[0].FormaPagoDescripcion;
                    objFEXRequest.Obs = docBatch.RequestHeaders[0].Observaciones2;
                    objFEXRequest.Obs_comerciales = docBatch.RequestHeaders[0].Observaciones1;
                    objFEXRequest.Incoterms = docBatch.RequestHeaders[0].IncoTerms;

                    objFEXRequest.Items = new FacturaElectronica.WebServices.wsfex.Item[Convert.ToInt32(docBatch.RequestHeaders[0].CantidadRegistrosDetalle)];

                    for (i = 0; i < objFEXRequest.Items.Length; i++)
                    {
                        seccionName = "Línea " + i.ToString();
                        objFEXRequest.Items[i] = new wsfex.Item();
                        objFEXRequest.Items[i].Pro_codigo = docBatch.RequestHeaders[0].RequestLines[i].CodigoProductoEmpresa;
                        objFEXRequest.Items[i].Pro_ds = docBatch.RequestHeaders[0].RequestLines[i].Descripcion;
                        objFEXRequest.Items[i].Pro_umed = Convert.ToInt32(docBatch.RequestHeaders[0].RequestLines[i].UnidadMedida);
                        objFEXRequest.Items[i].Pro_qty = Convert.ToDouble(docBatch.RequestHeaders[0].RequestLines[i].Cantidad);
                        objFEXRequest.Items[i].Pro_precio_uni = Convert.ToDouble(docBatch.RequestHeaders[0].RequestLines[i].ImportePrecioUnitarioMonedaFacturacion);
                        objFEXRequest.Items[i].Pro_total_item = Convert.ToDouble(docBatch.RequestHeaders[0].RequestLines[i].ImporteSubtotalMonedaFacturacion);
                    }
                }
                catch (Exception ex)
                {
                    sqlEngine.LogError("0", "0", "Autorización", "Error al asignar el campo: " + seccionName + "." + fieldName + ", " + ex.Message);
                }

                //Debug Line
                Utils.Utils.DebugLine(Utils.Utils.SerializeObject(objFEXRequest), oSettings.PathDebug + "\\ClsFEXRequest-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + DateTime.Now.Millisecond.ToString("000") + ".xml");

                objFEXResponseAuthorize = fexService.FEXAuthorize(objFEXAuthRequest, objFEXRequest);
            }
            catch (Exception ex)
            {
                int iElement = 0;
                if (i > 0)
                    iElement = i - 1;
                else
                    iElement = 0;

                sqlEngine.LogError(docBatch.RequestHeaders[iElement].SQLID, "0", "Autorización", "Error no conocido: (AfipFEX) " + ex.Message);

                objFEXResponseAuthorize.FEXErr = new FacturaElectronica.WebServices.wsfex.ClsFEXErr();

                if (docBatch.BatchUniqueId == "AUTO" || objFEXResponseLastID.FEXResultGet == null)
                {
                    objFEXResponseAuthorize.FEXErr.ErrCode = 667;
                    objFEXResponseAuthorize.FEXErr.ErrMsg = ex.Message + "Error en AFIP al obtener el último nro de requerimiento (" + objFEXResponseLastID.FEXErr.ErrCode.ToString() + ") " + objFEXResponseLastID.FEXErr.ErrMsg;
                }
                else
                {
                    objFEXResponseAuthorize.FEXErr.ErrCode = 668;
                    objFEXResponseAuthorize.FEXErr.ErrMsg = ex.Message;
                    sqlEngine.LogError(docBatch.RequestHeaders[iElement].SQLID, "0", "Autorización", "Error no conocido: (AfipFEX) " + ex.Message);
                }
            }

            return objFEXResponseAuthorize;
        }

        public static string GetMotivoDescripcion(string motivo, string UltimoNroCbteUsado)
        {
            string response = "";

            if (motivo != null)
            {
                if (motivo.IndexOf("01") > -1)
                    response += "La CUIT informada no corresponde a un Responsable Inscripto en el IVA Activo;";

                if (motivo.IndexOf("02") > -1)
                    response += "La CUIT informada no se encuentra autorizada a emitir Comprobantes Electronicos Originales o el periodo de inicio autorizado es posterior al de la generacion de la solicitud;";

                if (motivo.IndexOf("03") > -1)
                    response += "La CUIT informada registra inconvenientes con el domicilio fiscal;";

                if (motivo.IndexOf("04") > -1)
                    response += "El Punto de Venta informado no se encuentra declarado para ser utilizado en el presente regimen;";

                if (motivo.IndexOf("05") > -1)
                    response += "La Fecha del comprobante indicada no puede ser anterior en mas de cinco dias, si se trata de una venta, o anterior o posterior en mas de diez dias, si se trata de una prestacion de servicios, consecutivos de la fecha de remision del archivo Art. 22 de la RG N° 2177-;";

                if (motivo.IndexOf("06") > -1)
                    response += "La CUIT informada no se encuentra autorizada a emitir Comprobantes Clase 'A';";

                if (motivo.IndexOf("07") > -1)
                    response += "Para la clase de comprobante solicitado -Comprobante Clase A- debera consignar en el campo codigo de documento identificatorio del comprador el codigo '80';";

                if (motivo.IndexOf("08") > -1)
                    response += "La CUIT indicada en el campo N° de Identificacion del Comprador es invalida;";

                if (motivo.IndexOf("09") > -1)
                    response += "La CUIT indicada en el campo N° de identificacion del Comprador no existe en el padron unico de contribuyentes;";

                if (motivo.IndexOf("10") > -1)
                    response += "La CUIT indicada en el campo n° de identificacion del comprador no corresponde a un responsable inscripto en el IVA Activo;";

                if (motivo.IndexOf("11") > -1)
                    response += "El numero de comprobante desde informado no es correlativo al ultimo n° de comprobante registrado/hasta solicitado para ese Tipo de Comprobante y Punto de Venta. Ultimo Nro:" + UltimoNroCbteUsado + " ;";

                if (motivo.IndexOf("12") > -1)
                    response += "El rango informado se encuentra autorizado con anterioridad para la misma CUIT, Tipo de Comprobante y Punto de Venta;";

                if (motivo.IndexOf("13") > -1)
                    response += "La CUIT indicada se encuentra comprendida en el regimen establecido por la resolucion general n° 2177 y/o en el titulo I de la Resolucion General N° 1361 Art. 24 de la RG N° 2177-;";
            }
            return response;
        }
    }

}
