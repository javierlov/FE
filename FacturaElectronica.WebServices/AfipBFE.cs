using System;
using FacturaElectronica.Common;
using FacturaElectronica.Utils;

namespace FacturaElectronica.WebServices
{
    public class AfipBFE
    {
        wsbfe.Service bfeService = new wsbfe.Service();
        wsbfe.ClsBFEAuthRequest objBFEAuthRequest = new wsbfe.ClsBFEAuthRequest();

        wsbfe.ClsBFE_LastCMP objLastCMP = new wsbfe.ClsBFE_LastCMP();

        public AfipBFE(ref wsbfe.Service afipBfexService, ref wsbfe.ClsBFEAuthRequest afipObjBFEAuthRequest, Settings oSettings)
        {
            bfeService = afipBfexService;
            bfeService.Url = oSettings.UrlAFIPwsbfe;
            bfeService.Timeout = 10000;

            objBFEAuthRequest = afipObjBFEAuthRequest;
            objLastCMP.Token = afipObjBFEAuthRequest.Token;
            objLastCMP.Cuit = afipObjBFEAuthRequest.Cuit;
            objLastCMP.Sign = afipObjBFEAuthRequest.Sign;            
        }

        public wsbfe.BFEResponse_LastID GetLastBatchUniqueId()
        {
            try
            {
                wsbfe.BFEResponse_LastID objBFELastID = new wsbfe.BFEResponse_LastID();
                objBFELastID = bfeService.BFEGetLast_ID(objBFEAuthRequest);

                return objBFELastID;
            }
            catch (Exception ex)
            {
                throw (new Exception("GetLastID. Error:" + ex.Message));
            }
        }

        public wsbfe.BFEResponseLast_CMP BFERecuperaLastCMPRequest(string tipoComprobante, string puntoVenta)
        {
            wsbfe.BFEResponseLast_CMP objBFERecuperaLastCMPResponse = new wsbfe.BFEResponseLast_CMP();
            objLastCMP.Pto_venta = Convert.ToInt16(puntoVenta);
            objLastCMP.Tipo_cbte = Convert.ToInt16(tipoComprobante);

            try
            {
                objBFERecuperaLastCMPResponse = bfeService.BFEGetLast_CMP(objLastCMP);
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("FERecuperaLastCMPRequest", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return objBFERecuperaLastCMPResponse;
        }

        public wsbfe.BFEResponseAuthorize BFEAuthorize(RequestBatch docBatch, Settings oSettings)
        {
            wsbfe.ClsBFERequest objBFERequest = new wsbfe.ClsBFERequest();
            wsbfe.BFEResponseAuthorize objBFEResponseAuthorize = new wsbfe.BFEResponseAuthorize();
            wsbfe.BFEResponse_LastID objBFEResponseLastID = null;

            DBEngine.SQLEngine sqlEngine = new FacturaElectronica.DBEngine.SQLEngine();

            int i = 0;

            try
            {
                //DEBUG LINE
                if (Convert.ToBoolean(oSettings.ActivarDebug))
                    Utils.Utils.DebugLine(Utils.Utils.SerializeObject(objBFEAuthRequest), oSettings.PathDebug + "\\ClsBFEAuthRequest-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + DateTime.Now.Millisecond.ToString("000") + ".xml");

                //Si no tiene identificador único hay que generarlo
                if (docBatch.BatchUniqueId == "AUTO")
                {
                    objBFEResponseLastID = GetLastBatchUniqueId();
                    if (objBFEResponseLastID.BFEResultGet == null)
                    {
                        sqlEngine.LogError(docBatch.RequestHeaders[0].SQLID, "0", "Autorización", "Error AFIP al obtener el último nro de requerimiento (" + objBFEResponseLastID.BFEErr.ErrCode + ") " + objBFEResponseLastID.BFEErr.ErrMsg);
                    }
                    else
                    {
                        objBFEResponseLastID.BFEResultGet.Id = objBFEResponseLastID.BFEResultGet.Id + 1;
                        docBatch.BatchUniqueId = (objBFEResponseLastID.BFEResultGet.Id).ToString();

                        //Guardar Unique Batch ID que luego se utilizara para reprocesos y obtener CAE
                        sqlEngine.UpdateCabeceraBatchUniqueId(docBatch.RequestHeaders[0].SQLID, docBatch.BatchUniqueId);
                    }
                }

                //DEBUG LINE
                if (Convert.ToBoolean(oSettings.ActivarDebug))
                    Utils.Utils.DebugLine(Utils.Utils.SerializeObject(docBatch), oSettings.PathDebug + "\\DocumentBatch-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + DateTime.Now.Millisecond.ToString("000") + ".xml");

                string fieldName = "";
                string seccionName = "";

                try
                {
                    seccionName = "Cabecera";
                    fieldName = "BatchUniqueId";
                    objBFERequest.Id = (long)Convert.ToDouble(docBatch.BatchUniqueId);
                    fieldName = "TipoComprobante";
                    objBFERequest.Tipo_cbte = Convert.ToInt16(docBatch.RequestHeaders[0].TipoComprobante);
                    fieldName = "PuntoVenta";
                    objBFERequest.Punto_vta = Convert.ToInt16(docBatch.RequestHeaders[0].PuntoVenta);
                    fieldName = "NroComprobanteDesde";
                    objBFERequest.Cbte_nro = (long)Convert.ToDouble(docBatch.RequestHeaders[0].NroComprobanteDesde);
                    fieldName = "ImporteMonedaFacturacion";
                    objBFERequest.Imp_total = Convert.ToDouble(docBatch.RequestHeaders[0].ImporteMonedaFacturacion);
                    fieldName = "FechaComprobante";
                    objBFERequest.Fecha_cbte = Convert.ToDateTime(docBatch.RequestHeaders[0].FechaComprobante).ToString("yyyyMMdd");
                    fieldName = "ImportePercepcionIIBBMonedaFacturacion";
                    objBFERequest.Imp_iibb = Convert.ToDouble(docBatch.RequestHeaders[0].ImportePercepcionIIBBMonedaFacturacion);
                    fieldName = "ImporteImpuestosInternosMonedaFacturacion";
                    objBFERequest.Imp_internos = Convert.ToDouble(docBatch.RequestHeaders[0].ImporteImpuestosInternosMonedaFacturacion);
                    fieldName = "ImporteExentoMonedaFacturacion";
                    objBFERequest.Imp_op_ex = Convert.ToDouble(docBatch.RequestHeaders[0].ImporteExentoMonedaFacturacion);
                    fieldName = "ImportePercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion";
                    objBFERequest.Imp_perc = Convert.ToDouble(docBatch.RequestHeaders[0].ImportePercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion);
                    fieldName = "ImportePercepcionImpuestosMunicipalesMonedaFacturacion";
                    objBFERequest.Imp_perc_mun = Convert.ToDouble(docBatch.RequestHeaders[0].ImportePercepcionImpuestosMunicipalesMonedaFacturacion);
                    fieldName = "ImporteNoGravadoMonedaFacturacion";
                    objBFERequest.Imp_tot_conc = Convert.ToDouble(docBatch.RequestHeaders[0].ImporteNoGravadoMonedaFacturacion);
                    fieldName = "ImporteImpuestoLiquidadoMonedaFacturacion";
                    objBFERequest.Impto_liq = Convert.ToDouble(docBatch.RequestHeaders[0].ImporteImpuestoLiquidadoMonedaFacturacion);
                    fieldName = "CodigoDocumentoComprador";
                    objBFERequest.Tipo_doc = Convert.ToInt16(docBatch.RequestHeaders[0].CompradorCodigoDocumento);
                    fieldName = "NroDocumentoComprador";
                    objBFERequest.Nro_doc = Convert.ToInt64(docBatch.RequestHeaders[0].CompradorNroDocumento);
                    fieldName = "CodigoMoneda";
                    objBFERequest.Imp_moneda_Id = docBatch.RequestHeaders[0].CodigoMoneda;
                    fieldName = "TasaCambio";
                    objBFERequest.Imp_moneda_ctz = Convert.ToDouble(docBatch.RequestHeaders[0].TasaCambio);
                    fieldName = "ImporteRNI_PercepcionMonedaFacturacion";
                    objBFERequest.Impto_liq_rni = Convert.ToDouble(docBatch.RequestHeaders[0].ImporteRNI_PercepcionMonedaFacturacion);

                    objBFERequest.Zona = 1;

                    objBFERequest.Items = new wsbfe.Item[Convert.ToInt32(docBatch.RequestHeaders[0].CantidadRegistrosDetalle)];

                    for (i = 0; i < objBFERequest.Items.Length; i++)
                    {
                        seccionName = "Línea " + i.ToString();
                        objBFERequest.Items[i] = new wsbfe.Item();
                        fieldName = "CodigoProductoEmpresa";
                        objBFERequest.Items[i].Pro_codigo_sec = docBatch.RequestHeaders[0].RequestLines[i].CodigoProductoEmpresa;
                        fieldName = "CodigoProductoNCM";
                        objBFERequest.Items[i].Pro_codigo_ncm = docBatch.RequestHeaders[0].RequestLines[i].CodigoProductoNCM;
                        fieldName = "Descripcion";
                        objBFERequest.Items[i].Pro_ds = docBatch.RequestHeaders[0].RequestLines[i].Descripcion;
                        fieldName = "UnidadMedida";
                        objBFERequest.Items[i].Pro_umed = Convert.ToInt32(docBatch.RequestHeaders[0].RequestLines[i].UnidadMedida);
                        fieldName = "Cantidad";
                        objBFERequest.Items[i].Pro_qty = Convert.ToDouble(docBatch.RequestHeaders[0].RequestLines[i].Cantidad);
                        fieldName = "ImportePrecioUnitarioMonedaFacturacion";
                        objBFERequest.Items[i].Pro_precio_uni = Convert.ToDouble(docBatch.RequestHeaders[0].RequestLines[i].ImportePrecioUnitarioMonedaFacturacion);
                        fieldName = "ImporteSubtotalMonedaFacturacion";
                        objBFERequest.Items[i].Imp_total = Convert.ToDouble(docBatch.RequestHeaders[0].RequestLines[i].ImporteSubtotalMonedaFacturacion);
                        fieldName = "ImporteBonificacionMonedaFacturacion";
                        objBFERequest.Items[i].Imp_bonif = Convert.ToDouble(docBatch.RequestHeaders[0].RequestLines[i].ImporteBonificacionMonedaFacturacion);
                        fieldName = "AlicuotaIVA";
                        objBFERequest.Items[i].Iva_id = Convert.ToInt16(docBatch.RequestHeaders[0].RequestLines[i].AlicuotaIVA);
                    }
                }
                catch (Exception ex)
                {
                    sqlEngine.LogError("0", "0", "Autorización", "Error al asignar el campo: " + seccionName + "." + fieldName + ", " + ex.Message);
                }

                //DEBUG LINE
                if (Convert.ToBoolean(oSettings.ActivarDebug))
                    Utils.Utils.DebugLine(Utils.Utils.SerializeObject(objBFERequest), oSettings.PathDebug + "\\ClsBFERequest-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + DateTime.Now.Millisecond.ToString("000") + ".xml");

                try
                {
                    objBFEResponseAuthorize = bfeService.BFEAuthorize(objBFEAuthRequest, objBFERequest);

                    ////WORKAROUND FE BIENES DE CAPITAL
                    if ((objBFEResponseAuthorize == null || objBFEResponseAuthorize.BFEResultAuth == null || objBFEResponseAuthorize.BFEResultAuth.Fch_venc_Cae == null || objBFEResponseAuthorize.BFEResultAuth.Fch_venc_Cae == string.Empty) &&
                        (objBFEResponseAuthorize.BFEResultAuth.Obs == string.Empty) &&
                        (objBFEResponseAuthorize.BFEErr == null ))
                    {

                        //WORKAROUND FE BIENES DE CAPITAL, REINTENTO 3 VECES EN LAPSOS DE 40 SEGUNDOS
                        for (int iw = 0; iw < 5; iw++)
                        {
                            //espero 40 seg
                            System.Threading.Thread.Sleep(60000);

                            //consulto el cae
                            objBFEResponseAuthorize = ReprocessOnError(objBFEAuthRequest, objBFERequest);

                            //verifico si me devuelve el cae
                            if (objBFEResponseAuthorize != null && objBFEResponseAuthorize.BFEResultAuth != null && objBFEResponseAuthorize.BFEResultAuth.Fch_venc_Cae != null && objBFEResponseAuthorize.BFEResultAuth.Fch_venc_Cae != string.Empty)
                                break;
                        }
                    }
                }
                catch
                {
                    //WORKAROUND FE BIENES DE CAPITAL, REINTENTO 3 VECES EN LAPSOS DE 40 SEGUNDOS
                    for (int iw = 0; iw < 5; iw++)
                    {
                        //espero 40 seg
                        System.Threading.Thread.Sleep(60000);

                        //consulto el cae
                        objBFEResponseAuthorize = ReprocessOnError(objBFEAuthRequest, objBFERequest);

                        //verifico si me devuelve el cae
                        if (objBFEResponseAuthorize != null && objBFEResponseAuthorize.BFEResultAuth != null && objBFEResponseAuthorize.BFEResultAuth.Fch_venc_Cae != null && objBFEResponseAuthorize.BFEResultAuth.Fch_venc_Cae != string.Empty)
                            break;
                    }
                }

                //DEBUG LINE
                if (Convert.ToBoolean(oSettings.ActivarDebug))
                    Utils.Utils.DebugLine(Utils.Utils.SerializeObject(objBFEResponseAuthorize), oSettings.PathDebug + "\\BFEResponseAuthorize-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + DateTime.Now.Millisecond.ToString("000") + ".xml");
            }
            catch (Exception ex)
            {
                int iElement = 0;
                if (i > 0)
                    iElement = i - 1;
                else
                    iElement = 0;

                if (docBatch == null || (docBatch.RequestHeaders == null && docBatch.RequestHeaders.Count == 0))
                {
                    sqlEngine.LogError("0", "0", "Autorización", "Error: " + ex.Message);
                }
                else
                {
                    if(docBatch.RequestHeaders[iElement] != null)
                        sqlEngine.LogError(docBatch.RequestHeaders[iElement].SQLID, "0", "Autorización", "Error: " + ex.Message);
                    else
                        sqlEngine.LogError("0", "0", "Autorización", "Error: " + ex.Message);
                }

                objBFEResponseAuthorize.BFEErr = new FacturaElectronica.WebServices.wsbfe.ClsBFEErr();

                if (docBatch.BatchUniqueId == "AUTO" || objBFEResponseLastID.BFEResultGet == null)
                {
                    objBFEResponseAuthorize.BFEErr.ErrCode = 667;
                    objBFEResponseAuthorize.BFEErr.ErrMsg = ex.Message + "Error en AFIP al obtener el último nro de requerimiento (" + objBFEResponseLastID.BFEErr.ErrCode.ToString() + ") " + objBFEResponseLastID.BFEErr.ErrMsg;
                }
                else
                {
                    objBFEResponseAuthorize.BFEErr.ErrCode = 668;
                    objBFEResponseAuthorize.BFEErr.ErrMsg = ex.Message;
                    sqlEngine.LogError(docBatch.RequestHeaders[iElement].SQLID, "0", "Autorización", "Error no conocido: (AfipBFE) " + ex.Message);
                }
            }

            return objBFEResponseAuthorize;

        }

        public static string GetMotivoDescripcion(string motivo)
        {
            string response = "";

            if (motivo != null)
            {
                if (motivo.IndexOf("01") > -1)
                    response += "La CUIT informada no corresponde a un Responsable Inscripto en el IVA activo;";

                if (motivo.IndexOf("02") > -1)
                    response += "La CUIT informada no se encuentra autorizada a emitir comprobantes Electronicos Originales o el Periodo de inicio de autorizado es posterior al de la generacion de la solicitud;";

                if (motivo.IndexOf("03") > -1)
                    response += "La CUIT informada registra inconvenientes con el domicilio fiscal;";

                if (motivo.IndexOf("04") > -1)
                    response += "El Punto de Venta informado no se encuentra declarado para ser utilizado en el presente regimen;";

                if (motivo.IndexOf("05") > -1)
                    response += "La Fecha del comprobante indicada no puede ser anterior en mas de cinco dias, si se trata de una venta, o anterior o posterior en mas de diez dias, si se trata de una prestacion de servicios, consecutivos de la fecha de remision del archivo Art. 22 de la rg N° 2177-;";

                if (motivo.IndexOf("06") > -1)
                    response += "La CUIT informada no se encuentra autorizada a emitir comprobantes clase 'A';";

                if (motivo.IndexOf("07") > -1)
                    response += "Para la clase de comprobante solicitado -comprobante clase A- debera consignar en el campo codigo de documento identificatorio del comprador el codigo '80';";

                if (motivo.IndexOf("08") > -1)
                    response += "La CUIT indicada en el campo N° de Identificacion del Comprador es invalida;";

                if (motivo.IndexOf("09") > -1)
                    response += "La CUIT indicada en el campo N° de Identificacion del Comprador no existe en el padron unico de contribuyentes;";

                if (motivo.IndexOf("10") > -1)
                    response += "La CUIT indicada en el campo N° de Identificacion del Comprador no corresponde a un responsable inscripto en el IVA activo;";

                if (motivo.IndexOf("11") > -1)
                    response += "El numero de comprobante desde informado no es correlativo al ultimo N° de comprobante registrado/hasta solicitado para ese tipo de comprobante y punto de venta;";

                if (motivo.IndexOf("12") > -1)
                    response += "El rango informado se encuentra autorizado con anterioridad para la misma CUIT, Tipo de Comprobante y Punto de Venta;";

                if (motivo.IndexOf("13") > -1)
                    response += "La CUIT indicada se encuentra comprendida en el regimen establecido por la resolucion general N° 2177 y/o en el Titulo I de la Resolucion General N° 1361 Art. 24 de la rg N° 2177-;";
            }

            return response;
        }

        private wsbfe.BFEResponseAuthorize ReprocessOnError(wsbfe.ClsBFEAuthRequest objBFEAuthRequest, wsbfe.ClsBFERequest objBFERequest)
        {
            //consulto el cae
            wsbfe.BFEGetCMPResponse objBFEGetCMPResponse = new wsbfe.BFEGetCMPResponse();
            wsbfe.ClsBFEGetCMP objCMP = new wsbfe.ClsBFEGetCMP();
            wsbfe.BFEResponseAuthorize objBFEResponseAuthorize = new wsbfe.BFEResponseAuthorize();

            objCMP.Punto_vta = Convert.ToInt16(objBFERequest.Punto_vta);
            objCMP.Tipo_cbte = Convert.ToInt16(objBFERequest.Tipo_cbte);
            objCMP.Cbte_nro = Convert.ToInt64(objBFERequest.Cbte_nro);

            try
            {
                objBFEGetCMPResponse = bfeService.BFEGetCMP(objBFEAuthRequest, objCMP);

                if (objBFEGetCMPResponse.BFEResultGet != null && objBFEGetCMPResponse.BFEResultGet.Fch_venc_Cae != null)
                {
                    if (objBFEResponseAuthorize.BFEErr == null)
                    {
                        objBFEResponseAuthorize.BFEErr = new wsbfe.ClsBFEErr();
                    }
                    objBFEResponseAuthorize.BFEErr.ErrCode = objBFEGetCMPResponse.BFEErr.ErrCode;
                    objBFEResponseAuthorize.BFEErr.ErrMsg = objBFEGetCMPResponse.BFEErr.ErrMsg;

                    if (objBFEResponseAuthorize.BFEEvents == null)
                    {
                        objBFEResponseAuthorize.BFEEvents = new wsbfe.ClsBFEEvents();
                    }
                    objBFEResponseAuthorize.BFEEvents.EventCode = objBFEGetCMPResponse.BFEEvents.EventCode;
                    objBFEResponseAuthorize.BFEEvents.EventMsg = objBFEGetCMPResponse.BFEEvents.EventMsg;

                    if (objBFEResponseAuthorize.BFEResultAuth == null)
                    {
                        objBFEResponseAuthorize.BFEResultAuth = new wsbfe.ClsBFEOutAuthorize();
                    }
                    objBFEResponseAuthorize.BFEResultAuth.Resultado = "A";
                    objBFEResponseAuthorize.BFEResultAuth.Cae = objBFEGetCMPResponse.BFEResultGet.Cae;
                    objBFEResponseAuthorize.BFEResultAuth.Cuit = objBFEGetCMPResponse.BFEResultGet.Cuit;
                    objBFEResponseAuthorize.BFEResultAuth.Fch_cbte = objBFEGetCMPResponse.BFEResultGet.Fecha_cbte_cae;
                    objBFEResponseAuthorize.BFEResultAuth.Fch_venc_Cae = objBFEGetCMPResponse.BFEResultGet.Fch_venc_Cae;
                    objBFEResponseAuthorize.BFEResultAuth.Id = objBFEGetCMPResponse.BFEResultGet.Id;
                    objBFEResponseAuthorize.BFEResultAuth.Obs = objBFEGetCMPResponse.BFEResultGet.Obs;
                    objBFEResponseAuthorize.BFEResultAuth.Resultado = objBFEGetCMPResponse.BFEResultGet.Resultado;
                    objBFEResponseAuthorize.BFEResultAuth.Reproceso = "S";
                }
            }
            catch
            {
                objBFEResponseAuthorize = null;
            }

            return objBFEResponseAuthorize;
        }    
    }
}
