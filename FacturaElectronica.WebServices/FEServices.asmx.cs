using System;
using System.ComponentModel;
using System.Web.Services;
using System.Xml;
using FacturaElectronica.Common;
using FacturaElectronica.Utils;

namespace FacturaElectronica.WebServices
{
    /// <summary>
    /// Summary description for FEServices
    /// </summary>
    [WebService(Namespace = "http://accendo.com.ar/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class FEServices : System.Web.Services.WebService
    {
        private DBEngine.SQLEngine sqlEngine = new FacturaElectronica.DBEngine.SQLEngine();
                        
        [WebMethod]
        public string TestProcesarLoteFacturas(string EmpresaID, string TipoServicio, string PathXmlDocumentBatch)
        {
            XmlDocument xmlDocumentBatch = new XmlDocument();

            string result = string.Empty;

            try
            {
                xmlDocumentBatch.Load(PathXmlDocumentBatch);

                switch (TipoServicio)
                {
                    //BIENES Y SERVICIOS
                    case "1":
                        result = ProcesarLoteFacturasBienesServicios(EmpresaID, xmlDocumentBatch.InnerXml);
                        break;

                    //BIENES DE CAPITAL
                    case "2":
                        result = ProcesarLoteFacturasBienesCapital(EmpresaID, xmlDocumentBatch.InnerXml);
                        break;

                    //EXPORTACION
                    case "3":
                        result = ProcesarLoteFacturasExportacion(EmpresaID, xmlDocumentBatch.InnerXml);
                        break;

                    default:
                        result = "<Error>No se ha seleccionado el Tipo de Servicio. 1:Bienes y Servicios; 2:Bienes de Capital; 3:Exportacion</Error>";
                        break;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return result;
        }

        [WebMethod]
        public ResponseBatch ProcesarLoteFacturasBienesServiciosObj(string EmpresaID, RequestBatch documentBatch)
        {
            ResponseBatch responseBatch = new ResponseBatch();

            try
            {
                string result = ProcesarLoteFacturasBienesServicios(EmpresaID, documentBatch.GetXMLString());
                responseBatch.LoadXMLString(result);
            }
            catch (Exception ex)
            {
                responseBatch.MensajeError = ex.Message;
            }

            return responseBatch;
        }

        [WebMethod]
        public ResponseBatch ProcesarLoteFacturasExportacionObj(string EmpresaID, RequestBatch documentBatch)
        {
            ResponseBatch responseBatch = new ResponseBatch();
            try
            {
                string result = ProcesarLoteFacturasExportacion(EmpresaID, documentBatch.GetXMLString());
                responseBatch.LoadXMLString(result);
            }
            catch (Exception ex)
            {
                responseBatch.MensajeError = ex.Message;
            }

            return responseBatch;
        }

        [WebMethod]
        public string ProcesarLoteFacturasBienesServicios(string EmpresaID, string xmlDocument)
        {
            #region Inicialización

            Settings oSettings = new Settings(EmpresaID);

            wsfe.Service afipFeService = new wsfe.Service();
            afipFeService.Url = oSettings.UrlAFIPwsfe;

            wsfe.FEAuthRequest afipObjFEAuthRequest = new wsfe.FEAuthRequest();
            wsfe.CbteTipo afipObjFELastCMType = new wsfe.CbteTipo();
            wsfe.FECAEResponse feResponse = new wsfe.FECAEResponse();           

            ResponseBatch batchResponse = new ResponseBatch();            
            RequestBatch loteDocs = new RequestBatch();

            string url = oSettings.UrlFEWebService;
            string SQLID = "0";
            string estadoDocumento = string.Empty;
            string cae = string.Empty;
            string FechaVencimiento = string.Empty;
            string strEquivalenciaErrorFields = string.Empty;

            bool bRegistrarInicio = false;
            bool bEquivalenciaError = false;

            //Cargar el lote recibido
            loteDocs.LoadXMLString(xmlDocument);

            #endregion

            #region Registrar Inicio

            try
            {
                bRegistrarInicio = sqlEngine.LogBatchStart(ref loteDocs);

                SQLID = loteDocs.RequestHeaders[0].SQLID;

                //DEBUG LINE
                if (Convert.ToBoolean( oSettings.ActivarDebug ))
                    Utils.Utils.DebugLine(Utils.Utils.SerializeObject(loteDocs), oSettings.PathDebug + "\\" + SQLID + "-P1.RequestBatch-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + DateTime.Now.Millisecond.ToString("000") + ".xml");
            }
            catch (Exception ex)
            {
                sqlEngine.LogError(SQLID, "0", "ProcesarLoteFacturasBienesServicios", ex.Message);
                bRegistrarInicio = false;
            }

            #endregion

            //Si pudo crear el registro en la base continuo
            if (bRegistrarInicio)
            {
                #region Verificar Login con AFIP

                AfipConnection afipConn = new AfipConnection("wsfe", oSettings);
                if (afipConn.ConnectionErrorDescription == string.Empty)
                {
                    //Inicializo el objeto AuthRequest de la Afip
                    afipObjFEAuthRequest.Cuit = afipConn.Cuit;
                    afipObjFEAuthRequest.Sign = afipConn.Sign;
                    afipObjFEAuthRequest.Token = afipConn.Token;
                }
                else
                {
                    try
                    {
                        sqlEngine.LogError(SQLID, "0", "AfipConnection", afipConn.ConnectionErrorDescription);
                        sqlEngine.LogBatchEnd(SQLID, "Error", cae, FechaVencimiento);
                    }
                    catch (Exception ex)
                    {
                        sqlEngine.LogError(SQLID, "0", "FEService-AFIP Login", "Error: " + ex.Message);
                    }
                }

                AfipFE afipFE = new AfipFE(ref afipFeService, ref afipObjFEAuthRequest, ref afipObjFELastCMType);

                #endregion

                if (afipConn.ConnectionErrorDescription == string.Empty)
                {
                    #region Buscar Equivalencias

                    bEquivalenciaError = BuscarEquivalencias(ref loteDocs, oSettings, ref strEquivalenciaErrorFields);

                    #endregion

                    //Si no hay errores de equivalencia continuo
                    if (!bEquivalenciaError)
                    {
                        #region Hacer el pedido a AFIP

                        try
                        {
                            if (afipConn.IsConnected)
                            {
                                feResponse = afipFE.FEAuthRequest(loteDocs, oSettings);

                                //DEBUG LINE
                                if (Convert.ToBoolean(oSettings.ActivarDebug))
                                    Utils.Utils.DebugLine(Utils.Utils.SerializeObject(feResponse), oSettings.PathDebug + "\\" + SQLID + "-P3.FEResponse-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + DateTime.Now.Millisecond.ToString("000") + ".xml");
                            }
                            else
                            {
                                sqlEngine.LogError(SQLID, "0", "Respuesta AFIP", "Sin Conexion. Descripcion: " + afipConn.ConnectionErrorDescription);
                                feResponse.Errors = new wsfe.Err[1];
                                feResponse.Errors[0] = new wsfe.Err();
                                feResponse.Errors[0].Code = 0;
                                feResponse.Errors[0].Msg = afipConn.ConnectionErrorDescription;
                            }
                        }
                        catch (Exception ex)
                        {
                            sqlEngine.LogError(SQLID, "0", "Respuesta AFIP", "Error: " + ex.Message);
                        }

                        #endregion
                    }
                }

                #region Armar y devolver respuesta

                //Armar info del lote                
                if (feResponse.FeCabResp != null)
                {
                    batchResponse.BatchUniqueId = loteDocs.BatchUniqueId;
                    batchResponse.CUITInformante = feResponse.FeCabResp.Cuit.ToString();
                    batchResponse.FechaCAE = feResponse.FeCabResp.FchProceso;
                    batchResponse.CantidadComprobantes = feResponse.FeCabResp.CantReg.ToString();
                    batchResponse.Resultado = feResponse.FeCabResp.Resultado;

                    batchResponse.Reproceso = feResponse.FeCabResp.Reproceso;

                    if (feResponse.Errors != null && feResponse.Errors.Length > 0 && feResponse.Errors[0] != null)
                    {
                        batchResponse.CodigoError = feResponse.Errors[0].Code.ToString();
                        batchResponse.MensajeError = feResponse.Errors[0].Msg.ToString();
                    }
                }
                else
                {
                    batchResponse.BatchUniqueId = "0";
                    batchResponse.Resultado = "R";
                    batchResponse.CantidadComprobantes = loteDocs.CantidadComprobantes;

                    if (feResponse.Errors != null && feResponse.Errors.Length > 0 && feResponse.Errors[0] != null)
                    {
                        batchResponse.CodigoError = feResponse.Errors[0].Code.ToString();
                        batchResponse.MensajeError = feResponse.Errors[0].Msg.ToString();
                    }
                    else if(afipConn.ConnectionErrorDescription != string.Empty)
                    {
                        batchResponse.Resultado = "E";
                        batchResponse.CodigoError = "669";
                        batchResponse.MensajeError = afipConn.ConnectionErrorDescription;
                    }
                    else if (bEquivalenciaError)
                    {
                        batchResponse.Resultado = "E";
                        batchResponse.CodigoError = "Equivalencias";
                        batchResponse.MensajeError = "No se encontró equivalencia con AFIP. Campos: " + strEquivalenciaErrorFields;
                    }
                }

                //Armar info de los documentos
                if (feResponse.FeDetResp != null)
                {
                    for (int i = 0; i < feResponse.FeDetResp.Length; i++)
                    {
                        ResponseHeader thisHeader = new ResponseHeader();
                        if (feResponse.FeDetResp[i].CAE == "NULL")
                            feResponse.FeDetResp[i].CAE = "";
                        thisHeader.CAE = feResponse.FeDetResp[i].CAE;
                        thisHeader.CodigoDocumentoComprador = feResponse.FeDetResp[i].DocTipo.ToString();
                        thisHeader.NroDocumentoComprador = feResponse.FeDetResp[i].DocNro.ToString();
                        thisHeader.TipoComprobante = feResponse.FeCabResp.CbteTipo.ToString();                       
                        thisHeader.NroComprobanteDesde = feResponse.FeDetResp[i].CbteDesde.ToString();
                        thisHeader.NroComprobanteHasta = feResponse.FeDetResp[i].CbteHasta.ToString();
                        thisHeader.Importe = loteDocs.RequestHeaders[i].Importe;
                        thisHeader.ImporteNoGravado = loteDocs.RequestHeaders[i].ImporteNoGravado;
                        thisHeader.ImporteGravado = loteDocs.RequestHeaders[i].ImporteGravado;
                        thisHeader.ImporteImpuestoLiquidado = loteDocs.RequestHeaders[i].ImporteImpuestoLiquidado;
                        thisHeader.ImporteRNI_Percepcion = loteDocs.RequestHeaders[i].ImporteRNI_Percepcion;
                        thisHeader.ImporteExento = loteDocs.RequestHeaders[i].ImporteExento;
                        thisHeader.Resultado = feResponse.FeDetResp[i].Resultado;
                        
                        thisHeader.FechaComprobante = feResponse.FeDetResp[i].CbteFch;
                        thisHeader.FechaVencimiento = feResponse.FeDetResp[i].CAEFchVto;

                        thisHeader.PuntoVenta = loteDocs.RequestHeaders[i].PuntoVenta;
                        thisHeader.LetraComprobante = loteDocs.RequestHeaders[i].LetraComprobante;
                        thisHeader.NroInternoERP = loteDocs.RequestHeaders[i].NroInternoERP;
                        thisHeader.SQLID = loteDocs.RequestHeaders[i].SQLID;

                        wsfe.FERecuperaLastCbteResponse feLastCMPRespose = afipFE.FERecuperaUltimoComprobante(thisHeader.TipoComprobante, thisHeader.PuntoVenta);
                        thisHeader.UltimoNroComprobanteUsado = feLastCMPRespose.CbteNro.ToString();

                        if (feResponse.Errors != null && feResponse.Errors.Length > 0 && feResponse.Errors[0] != null)
                        {
                            thisHeader.Motivo = feResponse.Errors[0].Code.ToString();
                            thisHeader.MotivoDescripcion = feResponse.Errors[0].Msg;

                            if (thisHeader.Motivo == "10016")                            
                                thisHeader.MotivoDescripcion += " UltimoNroComprobanteUsado: " + thisHeader.UltimoNroComprobanteUsado;
                        }

                        //NUEVO DESDE VERSION 1 DE AFIP
                        if (feResponse.FeDetResp[i].Observaciones != null)
                        {
                            foreach (wsfe.Obs wsObs in feResponse.FeDetResp[i].Observaciones)
                            {
                                ResponseHeaderObs resObs = new ResponseHeaderObs();

                                resObs.Codigo = wsObs.Code.ToString();
                                resObs.Msg = wsObs.Msg;

                                if (resObs.Codigo == "10016")
                                    resObs.Msg += " UltimoNroComprobanteUsado: " + thisHeader.UltimoNroComprobanteUsado;

                                thisHeader.Observaciones.Add(resObs);
                            }
                        }

                        thisHeader.FechaDesdeServicioFacturado = feResponse.FeDetResp[i].CbteDesde.ToString();
                        thisHeader.FechaHastaServicioFacturado = feResponse.FeDetResp[i].CbteHasta.ToString();
                        thisHeader.FechaVencimientoPago = loteDocs.RequestHeaders[i].FechaVencimientoPago;                 

                        batchResponse.ResponseHeaders.Add(thisHeader);
                    }
                }
                else
                {
                    for (int i = 0; i < loteDocs.RequestHeaders.Count; i++)
                    {
                        ResponseHeader thisHeader = new ResponseHeader();

                        thisHeader.CAE = "";
                        thisHeader.TipoComprobante = loteDocs.RequestHeaders[i].TipoComprobante;
                        thisHeader.PuntoVenta = loteDocs.RequestHeaders[i].PuntoVenta;
                        thisHeader.NroComprobanteDesde = loteDocs.RequestHeaders[i].NroComprobanteDesde;
                        thisHeader.NroComprobanteHasta = loteDocs.RequestHeaders[i].NroComprobanteHasta;
                        thisHeader.Importe = loteDocs.RequestHeaders[i].Importe;
                        thisHeader.ImporteNoGravado = loteDocs.RequestHeaders[i].ImporteNoGravado;
                        thisHeader.ImporteGravado = loteDocs.RequestHeaders[i].ImporteGravado;
                        thisHeader.ImporteImpuestoLiquidado = loteDocs.RequestHeaders[i].ImporteImpuestoLiquidado;
                        thisHeader.ImporteRNI_Percepcion = loteDocs.RequestHeaders[i].ImporteRNI_Percepcion;
                        thisHeader.ImporteExento = loteDocs.RequestHeaders[i].ImporteExento;
                        thisHeader.FechaComprobante = loteDocs.RequestHeaders[i].FechaComprobante;
                        thisHeader.Motivo = batchResponse.CodigoError;
                        thisHeader.MotivoDescripcion = batchResponse.MensajeError;
                        thisHeader.FechaDesdeServicioFacturado = loteDocs.RequestHeaders[i].FechaDesdeServicioFacturado;
                        thisHeader.FechaHastaServicioFacturado = loteDocs.RequestHeaders[i].FechaHastaServicioFacturado;
                        thisHeader.FechaVencimientoPago = loteDocs.RequestHeaders[i].FechaVencimientoPago;
                        thisHeader.NroInternoERP = loteDocs.RequestHeaders[i].NroInternoERP;
                        thisHeader.LetraComprobante = loteDocs.RequestHeaders[i].LetraComprobante;
                        thisHeader.SQLID = loteDocs.RequestHeaders[i].SQLID;                        

                        batchResponse.ResponseHeaders.Add(thisHeader);
                    }
                }

                //DEBUG LINE
                if (Convert.ToBoolean(oSettings.ActivarDebug))
                    Utils.Utils.DebugLine(Utils.Utils.SerializeObject(batchResponse), oSettings.PathDebug + "\\" + SQLID + "-P4.ResponseBatch-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + DateTime.Now.Millisecond.ToString("000") + ".xml");

                #endregion

                #region Registrar Fin

                //TODO: VERIFICAR ESTA PARTE CAPTURA MAL LOS ERRORES
                switch (batchResponse.Resultado)
                {
                    case "A":
                        estadoDocumento = "Aceptado";
                        cae = batchResponse.ResponseHeaders[0].CAE;
                        FechaVencimiento = batchResponse.ResponseHeaders[0].FechaVencimiento;

                        break;
                    case "R":
                        estadoDocumento = "Rechazado";
                        cae = "";
                        FechaVencimiento = "";
                        break;

                    default:
                        if (batchResponse.Reproceso.ToLower() == "s")
                        {
                            estadoDocumento = "Rechazado";
                            cae = batchResponse.ResponseHeaders[0].CAE;
                            FechaVencimiento = batchResponse.ResponseHeaders[0].FechaVencimiento;
                        }
                        else
                        {
                            estadoDocumento = "Error";
                            cae = "";
                            FechaVencimiento = "";
                        }
                        break;
                }

                try
                {
                    sqlEngine.LogBatchEnd(SQLID, estadoDocumento, cae, FechaVencimiento);
                }
                catch (Exception ex)
                {
                    sqlEngine.LogError(SQLID, "0", "Respuesta Recibida", "Error: " + ex.Message);
                }

                #endregion                
            }

            return batchResponse.GetXMLString();
        }

        [WebMethod]
        public string ProcesarLoteFacturasExportacion(string EmpresaID, string xmlDocument)
        {
            #region Inicialización

            Settings oSettings = new Settings(EmpresaID);

            wsfex.Service afipFexService = new wsfex.Service();
            afipFexService.Url = oSettings.UrlAFIPwsfex;

            wsfex.ClsFEXAuthRequest afipObjFEXAuthRequest = new wsfex.ClsFEXAuthRequest();
            wsfex.FEXResponseAuthorize fexResponseAuthorize = new wsfex.FEXResponseAuthorize();

            ResponseBatch batchResponse = new ResponseBatch();
            RequestBatch loteDocs = new RequestBatch();

            string SQLID = "0";
            string estadoDocumento = string.Empty;
            string cae = string.Empty;
            string FechaVencimiento = string.Empty;
            string strEquivalenciaErrorFields = string.Empty;

            bool bRegistrarInicio = false;
            bool bEquivalenciaError = false;

            //Cargar el lote recibido            
            loteDocs.LoadXMLString(xmlDocument);

            #endregion

            #region Registrar pedido

            try
            {
                bRegistrarInicio = sqlEngine.LogBatchStart(ref loteDocs);

                SQLID = loteDocs.RequestHeaders[0].SQLID;

                //DEBUG LINE
                if (Convert.ToBoolean(oSettings.ActivarDebug))
                    Utils.Utils.DebugLine(Utils.Utils.SerializeObject(loteDocs), oSettings.PathDebug + "\\" + SQLID + "-FERequestBatch-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + DateTime.Now.Millisecond.ToString("000") + ".xml");
            }
            catch (Exception ex)
            {
                bRegistrarInicio = false;
                sqlEngine.LogError(SQLID, "0", "Procesando lote", "Error: " + ex.Message);                
            }

            #endregion

            //Si pudo crear el registro en la base continuo
            if (bRegistrarInicio)
            {
                #region Verificar Login con AFIP

                AfipConnection afipConn = new AfipConnection("wsfex", oSettings);
                if (afipConn.ConnectionErrorDescription == string.Empty)
                {
                    //Inicializo el objeto AuthRequest de la Afip
                    afipObjFEXAuthRequest.Cuit = afipConn.Cuit;
                    afipObjFEXAuthRequest.Sign = afipConn.Sign;
                    afipObjFEXAuthRequest.Token = afipConn.Token;
                }
                else
                {
                    try
                    {
                        sqlEngine.LogError(SQLID, "0", "AfipConnection", afipConn.ConnectionErrorDescription);

                        sqlEngine.LogBatchEnd(SQLID, "Error", cae, FechaVencimiento);
                    }
                    catch (Exception ex)
                    {
                        sqlEngine.LogError(SQLID, "0", "FEService-AFIP Login", "Error: " + ex.Message);
                    }
                }

                AfipFEX afipFEX = new AfipFEX(ref afipFexService, ref afipObjFEXAuthRequest, oSettings);

                #endregion                

                if (afipConn.ConnectionErrorDescription == string.Empty)
                {
                    #region Buscar Equivalencias

                    bEquivalenciaError = BuscarEquivalencias(ref loteDocs, oSettings, ref strEquivalenciaErrorFields);

                    #endregion

                    //Si no hay errore de equivalencia continuo
                    if (!bEquivalenciaError)
                    {
                        #region Realizar Validaciones
                        #endregion

                        #region Hacer el pedido a AFIP

                        try
                        {
                            if (afipConn.IsConnected)
                            {
                                fexResponseAuthorize = afipFEX.FEXAuthRequest(loteDocs, oSettings);
                                if (fexResponseAuthorize.FEXErr == null)
                                {
                                    sqlEngine.LogError(SQLID, "0", "Respuesta AFIP", "AFIP no pudo procesar por un error previo. Vea el log de errores.");
                                    fexResponseAuthorize.FEXErr = new wsfex.ClsFEXErr();
                                    fexResponseAuthorize.FEXErr.ErrCode = 0;
                                    fexResponseAuthorize.FEXErr.ErrMsg = "AFIP no pudo procesar por un error previo. Vea el log de errores.";
                                }
                            }
                            else
                            {
                                sqlEngine.LogError(SQLID, "0", "Respuesta AFIP", "Sin Conexion. Descripcion: " + afipConn.ConnectionErrorDescription);
                                fexResponseAuthorize.FEXErr = new wsfex.ClsFEXErr();
                                fexResponseAuthorize.FEXErr.ErrCode = 0;
                                fexResponseAuthorize.FEXErr.ErrMsg = afipConn.ConnectionErrorDescription;
                            }
                        }
                        catch (Exception ex)
                        {
                            sqlEngine.LogError(SQLID, "0", "Respuesta AFIP", "Error: " + ex.Message);
                        }

                        #endregion
                    }
                }

                #region Armar y devolver respuesta

                //Armar info del lote                
                if (fexResponseAuthorize.FEXResultAuth != null)
                {
                    batchResponse.BatchUniqueId = loteDocs.BatchUniqueId;
                    batchResponse.BatchUniqueId = fexResponseAuthorize.FEXResultAuth.Id.ToString();
                    batchResponse.CUITInformante = fexResponseAuthorize.FEXResultAuth.Cuit.ToString();
                    batchResponse.FechaCAE = fexResponseAuthorize.FEXResultAuth.Fch_venc_Cae;
                    batchResponse.CantidadComprobantes = "1";
                    batchResponse.Resultado = fexResponseAuthorize.FEXResultAuth.Resultado;
                    batchResponse.Reproceso = fexResponseAuthorize.FEXResultAuth.Reproceso;
                    batchResponse.SonServicios = "";
                    batchResponse.CodigoError = fexResponseAuthorize.FEXErr.ErrCode.ToString();
                    batchResponse.MensajeError = fexResponseAuthorize.FEXErr.ErrMsg.ToString();

                    if (fexResponseAuthorize.FEXResultAuth.Reproceso.ToLower() == "s")
                    {
                        batchResponse.Resultado = "R";
                        batchResponse.Motivo = "12";
                        batchResponse.MotivoDescripcion = "EL RANGO INFORMADO SE ENCUENTRA AUTORIZADO CON ANTERIOIRIDAD PARA LA MISMA CUIT, TIPO DE COMPROBANTE Y PUNTO DE VENTA.";
                    }
                    else
                    {
                        batchResponse.Motivo = fexResponseAuthorize.FEXResultAuth.Motivos_Obs;
                        batchResponse.MotivoDescripcion = "";
                    }

                    //Armar info del documento
                    ResponseHeader thisHeader = new ResponseHeader();

                    thisHeader.CAE = fexResponseAuthorize.FEXResultAuth.Cae;
                    thisHeader.NroComprobanteDesde = fexResponseAuthorize.FEXResultAuth.Cbte_nro.ToString();
                    thisHeader.NroComprobanteHasta = fexResponseAuthorize.FEXResultAuth.Cbte_nro.ToString();
                    thisHeader.FechaComprobante = fexResponseAuthorize.FEXResultAuth.Fch_cbte;
                    thisHeader.FechaVencimiento = fexResponseAuthorize.FEXResultAuth.Fch_venc_Cae;
                    thisHeader.PuntoVenta = fexResponseAuthorize.FEXResultAuth.Punto_vta.ToString();
                    thisHeader.TipoComprobante = fexResponseAuthorize.FEXResultAuth.Tipo_cbte.ToString();
                    thisHeader.NroInternoERP = loteDocs.RequestHeaders[0].NroInternoERP;
                    thisHeader.LetraComprobante = loteDocs.RequestHeaders[0].LetraComprobante;
                    thisHeader.SQLID = loteDocs.RequestHeaders[0].SQLID;
                    thisHeader.Resultado = fexResponseAuthorize.FEXResultAuth.Resultado;
                    thisHeader.Importe = loteDocs.RequestHeaders[0].Importe;

                    if (fexResponseAuthorize.FEXResultAuth.Reproceso.ToLower() == "s")
                    {
                        thisHeader.Resultado = "R";
                        thisHeader.Motivo = "12";
                        thisHeader.MotivoDescripcion = AfipFEX.GetMotivoDescripcion(thisHeader.Motivo, string.Empty);
                    }
                    else
                    {
                        thisHeader.Motivo = fexResponseAuthorize.FEXResultAuth.Motivos_Obs;
                        thisHeader.MotivoDescripcion = "";
                    }

                    batchResponse.ResponseHeaders.Add(thisHeader);
                }
                else
                {
                    batchResponse.BatchUniqueId = "0";
                    batchResponse.CUITInformante = "";
                    batchResponse.FechaCAE = "";
                    batchResponse.CantidadComprobantes = "1";
                    batchResponse.Resultado = "R";
                    batchResponse.Motivo = "";
                    batchResponse.MotivoDescripcion = "";
                    batchResponse.Reproceso = "";
                    batchResponse.SonServicios = "";

                    if (fexResponseAuthorize.FEXErr != null)
                    {
                        batchResponse.CodigoError = fexResponseAuthorize.FEXErr.ErrCode.ToString();
                        batchResponse.MensajeError = fexResponseAuthorize.FEXErr.ErrMsg.ToString();
                    }
                    else if (afipConn.ConnectionErrorDescription != string.Empty)
                    {
                        batchResponse.CodigoError = "669";
                        batchResponse.MensajeError = afipConn.ConnectionErrorDescription;
                    }
                    else if (bEquivalenciaError)
                    {
                        batchResponse.Resultado = "E";
                        batchResponse.CodigoError = "Equivalencias";
                        batchResponse.MensajeError = "No se encontró equivalencia con AFIP. Campos: " + strEquivalenciaErrorFields;
                    }

                    //Devolver los documentos originales
                    ResponseHeader thisHeader = new ResponseHeader();
                    thisHeader.CAE = "";
                    thisHeader.NroComprobanteDesde = loteDocs.RequestHeaders[0].NroComprobanteDesde;
                    thisHeader.NroComprobanteHasta = loteDocs.RequestHeaders[0].NroComprobanteHasta;
                    thisHeader.FechaComprobante = loteDocs.RequestHeaders[0].FechaComprobante;
                    thisHeader.FechaVencimiento = "";
                    thisHeader.Motivo = batchResponse.CodigoError;
                    thisHeader.MotivoDescripcion = batchResponse.MensajeError;
                    thisHeader.PuntoVenta = loteDocs.RequestHeaders[0].PuntoVenta;
                    thisHeader.TipoComprobante = loteDocs.RequestHeaders[0].TipoComprobante;
                    thisHeader.NroInternoERP = loteDocs.RequestHeaders[0].NroInternoERP;
                    thisHeader.LetraComprobante = loteDocs.RequestHeaders[0].LetraComprobante;
                    thisHeader.Resultado = "R";
                    thisHeader.SQLID = loteDocs.RequestHeaders[0].SQLID;

                    batchResponse.ResponseHeaders.Add(thisHeader);
                }

                #endregion

                #region Registrar respuesta recibida

                if (batchResponse.Resultado == "A" || batchResponse.Resultado == "R")
                {
                    switch (batchResponse.Resultado)
                    {
                        case "A":
                            estadoDocumento = "Aceptado";
                            cae = batchResponse.ResponseHeaders[0].CAE;
                            FechaVencimiento = batchResponse.ResponseHeaders[0].FechaVencimiento;

                            break;
                        case "R":
                            estadoDocumento = "Rechazado";
                            cae = "";
                            FechaVencimiento = "";
                            break;
                    }
                }
                else if (batchResponse.Reproceso.ToLower() == "s")
                {
                    estadoDocumento = "Rechazado";
                    cae = batchResponse.ResponseHeaders[0].CAE;
                    FechaVencimiento = batchResponse.ResponseHeaders[0].FechaVencimiento;
                }
                else
                {
                    estadoDocumento = "Error";
                    cae = "";
                    FechaVencimiento = "";
                }

                try
                {
                    sqlEngine.LogBatchEnd(SQLID, estadoDocumento, cae, FechaVencimiento);
                }
                catch (Exception ex)
                {
                    sqlEngine.LogError(SQLID, "0", "Respuesta Recibida", "Error: " + ex.Message);
                }
                #endregion
            }
            return batchResponse.GetXMLString();
        }

        [WebMethod]
        public string ProcesarLoteFacturasBienesCapital(string EmpresaID, string xmlDocument)
        {
            #region Inicialización

            Settings oSettings = new Settings(EmpresaID);

            wsbfe.Service afipBfexService = new wsbfe.Service();
            afipBfexService.Url = oSettings.UrlAFIPwsbfe;

            wsbfe.ClsBFEAuthRequest afipObjBFEAuthRequest = new wsbfe.ClsBFEAuthRequest();
            wsbfe.BFEResponseAuthorize bfeResponseAuthorize = new wsbfe.BFEResponseAuthorize();

            ResponseBatch batchResponse = new ResponseBatch();  
            RequestBatch loteDocs = new RequestBatch();

            string SQLID = "0";
            string estadoDocumento = "";
            string cae = "";
            string FechaVencimiento = "";
            string strEquivalenciaErrorFields = string.Empty;
            
            bool bRegistrarInicio = false;
            bool bEquivalenciaError = false;

            //Cargar el lote recibido            
            loteDocs.LoadXMLString(xmlDocument);

            #endregion

            #region Registrar pedido

            try
            {
                bRegistrarInicio = sqlEngine.LogBatchStart(ref loteDocs);

                SQLID = loteDocs.RequestHeaders[0].SQLID;

                //DEBUG LINE
                if (Convert.ToBoolean(oSettings.ActivarDebug))
                    Utils.Utils.DebugLine(Utils.Utils.SerializeObject(batchResponse), oSettings.PathDebug + "\\" + SQLID + "-FERequestBatch-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + DateTime.Now.Millisecond.ToString("000") + ".xml");          
            }
            catch (Exception ex)
            {
                bRegistrarInicio = false;
                sqlEngine.LogError(SQLID, "0", "Procesando lote", "Error: " + ex.Message);
            }

            #endregion

            if (bRegistrarInicio)
            {
                #region Verificar Login con AFIP

                AfipConnection afipConn = new AfipConnection("wsbfe", oSettings);
                if (afipConn.ConnectionErrorDescription == string.Empty)
                {
                    //Inicializo el objeto AuthRequest de la Afip
                    afipObjBFEAuthRequest.Cuit = afipConn.Cuit;
                    afipObjBFEAuthRequest.Sign = afipConn.Sign;
                    afipObjBFEAuthRequest.Token = afipConn.Token;
                }
                else
                {
                    try
                    {
                        sqlEngine.LogError(SQLID, "0", "AfipConnection", afipConn.ConnectionErrorDescription);

                        sqlEngine.LogBatchEnd(SQLID, "Error", cae, FechaVencimiento);
                    }
                    catch (Exception ex)
                    {
                        sqlEngine.LogError(SQLID, "0", "FEService-AFIP Login", "Error: " + ex.Message);
                    }
                }

                AfipBFE afipBFE = new AfipBFE(ref afipBfexService, ref afipObjBFEAuthRequest, oSettings);

                #endregion

                if (afipConn.ConnectionErrorDescription == string.Empty)
                {
                    #region Buscar Equivalencias

                    bEquivalenciaError = BuscarEquivalencias(ref loteDocs, oSettings, ref strEquivalenciaErrorFields);

                    #endregion

                    if (!bEquivalenciaError)
                    {
                        #region Realizar Validaciones
                        #endregion

                        #region Hacer el pedido a AFIP

                        try
                        {
                            if (afipConn.IsConnected)
                            {
                                bfeResponseAuthorize = afipBFE.BFEAuthorize(loteDocs, oSettings);

                                if (bfeResponseAuthorize.BFEErr == null)
                                {
                                    sqlEngine.LogError(SQLID, "0", "Respuesta AFIP", "AFIP no pudo procesar por un error previo. Vea el log de errores.");
                                    bfeResponseAuthorize.BFEErr = new wsbfe.ClsBFEErr();
                                    bfeResponseAuthorize.BFEErr.ErrCode = 0;
                                    bfeResponseAuthorize.BFEErr.ErrMsg = "AFIP no pudo procesar por un error previo. Vea el log de errores.";
                                }
                            }
                            else
                            {
                                sqlEngine.LogError(SQLID, "0", "Respuesta AFIP", "Sin Conexion. Descripcion: " + afipConn.ConnectionErrorDescription);
                                bfeResponseAuthorize.BFEErr = new wsbfe.ClsBFEErr();
                                bfeResponseAuthorize.BFEErr.ErrCode = 0;
                                bfeResponseAuthorize.BFEErr.ErrMsg = afipConn.ConnectionErrorDescription;
                            }
                        }
                        catch (Exception ex)
                        {
                            sqlEngine.LogError(SQLID, "0", "Respuesta AFIP", "Error: " + ex.Message);
                        }

                        #endregion
                    }
                }

                #region Armar y devolver respuesta

                //Armar info del lote                            
                if (bfeResponseAuthorize.BFEResultAuth != null && bfeResponseAuthorize.BFEResultAuth.Cae != null )
                {
                    batchResponse.BatchUniqueId = loteDocs.BatchUniqueId;
                    batchResponse.BatchUniqueId = bfeResponseAuthorize.BFEResultAuth.Id.ToString();
                    batchResponse.Resultado = bfeResponseAuthorize.BFEResultAuth.Resultado;
                    batchResponse.Reproceso = bfeResponseAuthorize.BFEResultAuth.Reproceso;
                    batchResponse.CUITInformante = bfeResponseAuthorize.BFEResultAuth.Cuit.ToString();
                    batchResponse.CantidadComprobantes = "1";

                    //Armar info del documento
                    ResponseHeader thisHeader = new ResponseHeader();

                    thisHeader.CAE = bfeResponseAuthorize.BFEResultAuth.Cae;
                    thisHeader.NroComprobanteDesde = loteDocs.RequestHeaders[0].NroComprobanteDesde.ToString();
                    thisHeader.NroComprobanteHasta = loteDocs.RequestHeaders[0].NroComprobanteHasta.ToString();
                    thisHeader.FechaComprobante = bfeResponseAuthorize.BFEResultAuth.Fch_cbte;
                    thisHeader.FechaVencimiento = bfeResponseAuthorize.BFEResultAuth.Fch_venc_Cae;
                    
                    thisHeader.PuntoVenta = loteDocs.RequestHeaders[0].PuntoVenta.ToString();
                    thisHeader.TipoComprobante = loteDocs.RequestHeaders[0].TipoComprobante.ToString();
                    thisHeader.NroInternoERP = loteDocs.RequestHeaders[0].NroInternoERP;
                    thisHeader.LetraComprobante = loteDocs.RequestHeaders[0].LetraComprobante;
                    thisHeader.SQLID = loteDocs.RequestHeaders[0].SQLID;
                    thisHeader.UltimoIDUsado = "";

                    //Obtengo el último último comprobante
                    wsbfe.BFEResponseLast_CMP bfeLastCMPRespose = afipBFE.BFERecuperaLastCMPRequest(thisHeader.TipoComprobante, thisHeader.PuntoVenta);
                    thisHeader.UltimoNroComprobanteUsado = bfeLastCMPRespose.BFEResult_LastCMP.Cbte_nro.ToString();

                    batchResponse.ResponseHeaders.Add(thisHeader);

                    if (bfeResponseAuthorize.BFEResultAuth.Reproceso.ToLower() == "s")
                    {
                        thisHeader.Resultado = "R";
                        thisHeader.Motivo = "12";
                        thisHeader.MotivoDescripcion = AfipBFE.GetMotivoDescripcion(thisHeader.Motivo);
                    }
                    else
                    {
                        thisHeader.Motivo = bfeResponseAuthorize.BFEResultAuth.Obs;
                        thisHeader.MotivoDescripcion = AfipBFE.GetMotivoDescripcion(bfeResponseAuthorize.BFEResultAuth.Obs);
                    }
                }
                else
                {
                    batchResponse.BatchUniqueId = "0";
                    batchResponse.Resultado = "R";
                    batchResponse.Reproceso = "";
                    batchResponse.CUITInformante = "";
                    batchResponse.CantidadComprobantes = "1";
                    
                    if (bfeResponseAuthorize.BFEErr != null)
                    {
                        batchResponse.CodigoError = bfeResponseAuthorize.BFEErr.ErrCode.ToString();
                        batchResponse.MensajeError = bfeResponseAuthorize.BFEErr.ErrMsg.ToString();
                    }
                    else if (afipConn.ConnectionErrorDescription != string.Empty)
                    {
                        batchResponse.CodigoError = "669";
                        batchResponse.MensajeError = afipConn.ConnectionErrorDescription;
                    }
                    else if (bEquivalenciaError)
                    {
                        batchResponse.Resultado = "E";
                        batchResponse.CodigoError = "Equivalencias";
                        batchResponse.MensajeError = "No se encontró equivalencia con AFIP. Campos: " + strEquivalenciaErrorFields;
                    }

                    //Devolver los documentos originales
                    ResponseHeader thisHeader = new ResponseHeader();
                    thisHeader.CAE = "";
                    thisHeader.NroComprobanteDesde = loteDocs.RequestHeaders[0].NroComprobanteDesde;
                    thisHeader.FechaComprobante = loteDocs.RequestHeaders[0].FechaComprobante;
                    thisHeader.FechaVencimiento = "";
                    thisHeader.Motivo = "";
                    thisHeader.MotivoDescripcion = "";
                    thisHeader.PuntoVenta = loteDocs.RequestHeaders[0].PuntoVenta;
                    thisHeader.TipoComprobante = loteDocs.RequestHeaders[0].TipoComprobante;
                    thisHeader.NroInternoERP = loteDocs.RequestHeaders[0].NroInternoERP;
                    thisHeader.LetraComprobante = loteDocs.RequestHeaders[0].LetraComprobante;
                    thisHeader.SQLID = loteDocs.RequestHeaders[0].SQLID;

                    //Obtengo el último último comprobante
                    if (afipConn.ConnectionErrorDescription == string.Empty)
                    {
                        wsbfe.BFEResponseLast_CMP bfeLastCMPRespose = afipBFE.BFERecuperaLastCMPRequest(thisHeader.TipoComprobante, thisHeader.PuntoVenta);
                        thisHeader.UltimoNroComprobanteUsado = bfeLastCMPRespose.BFEResult_LastCMP.Cbte_nro.ToString();
                    }

                    batchResponse.ResponseHeaders.Add(thisHeader);
                }

                #endregion

                #region Registrar respuesta recibida

                if (batchResponse.Resultado == "A" || batchResponse.Resultado == "R")
                {
                    switch (batchResponse.Resultado)
                    {
                        case "A":
                            estadoDocumento = "Aceptado";
                            cae = batchResponse.ResponseHeaders[0].CAE;
                            FechaVencimiento = batchResponse.ResponseHeaders[0].FechaVencimiento;

                            break;
                        case "R":
                            estadoDocumento = "Rechazado";
                            cae = "";
                            FechaVencimiento = "";
                            break;
                    }
                }
                else if (bfeResponseAuthorize.BFEResultAuth.Reproceso.ToLower() == "s")
                {
                    estadoDocumento = "Rechazado";
                    cae = batchResponse.ResponseHeaders[0].CAE;
                    FechaVencimiento = batchResponse.ResponseHeaders[0].FechaVencimiento;
                }
                else
                {
                    estadoDocumento = "Error";
                    cae = "";
                    FechaVencimiento = "";
                }

                try
                {
                    sqlEngine.LogBatchEnd(SQLID, estadoDocumento, cae, FechaVencimiento);
                }
                catch (Exception ex)
                {
                    sqlEngine.LogError(SQLID, "0", "Respuesta Recibida", "Error: " + ex.Message);
                }
                #endregion
            }
            return batchResponse.GetXMLString();
        }

        [WebMethod]
        public string ReprocesarLoteFacturasBienesServicios(string EmpresaID, RequestBatch documentBatch)
        {
            ResponseBatch responseBatch = new ResponseBatch();
            try
            {
                string result = ProcesarLoteFacturasBienesServicios(EmpresaID, documentBatch.GetXMLString());
                responseBatch.LoadXMLString(result);
            }
            catch (Exception ex)
            {
                responseBatch.MensajeError = ex.Message;
            }

            return string.Empty;
        }

        [WebMethod]
        public string ReprocesarLoteFacturasExportacion(string EmpresaID, RequestBatch documentBatch)
        {
            ResponseBatch responseBatch = new ResponseBatch();
            try
            {
                string result = ProcesarLoteFacturasBienesServicios(EmpresaID, documentBatch.GetXMLString());
                responseBatch.LoadXMLString(result);
            }
            catch (Exception ex)
            {
                responseBatch.MensajeError = ex.Message;
            }

            return string.Empty;
        }

        [WebMethod]
        public string ReprocesarLoteFacturasBienesCapital(string EmpresaID, RequestBatch documentBatch)
        {
            ResponseBatch responseBatch = new ResponseBatch();
            try
            {
                string result = ProcesarLoteFacturasBienesServicios(EmpresaID, documentBatch.GetXMLString());
                responseBatch.LoadXMLString(result);
            }
            catch (Exception ex)
            {
                responseBatch.MensajeError = ex.Message;
            }

            return string.Empty;
        }

        private bool BuscarEquivalencias(ref RequestBatch loteDocs, Settings oSettings, ref string strEquivalenciaErrorFields)
        {
            string resultado = string.Empty;
            string strSQLID = string.Empty;

            bool bEquivalenciaError = false;

            try
            {
                foreach (RequestHeader thisHeader in loteDocs.RequestHeaders)
                {
                    strSQLID = thisHeader.SQLID;

                    resultado = sqlEngine.ObtenerEquivalencia("EquivAFIPTipoComprobante", oSettings.EmpresaID, thisHeader.TipoComprobante);
                    if (resultado == "")
                    {
                        bEquivalenciaError = true;
                        strEquivalenciaErrorFields += "Tipo Comprobante: " + thisHeader.TipoComprobante + "; ";
                        sqlEngine.LogError(thisHeader.SQLID, "0", "Equivalencias", "No se encontró la equivalencia con AFIP del tipo de comprobante (" + thisHeader.TipoComprobante + ")");
                    }
                    else
                    {
                        thisHeader.TipoComprobante = resultado;
                    }

                    //PAIS TRANSACCIONES 0, 1, 2 y 3
                    resultado = sqlEngine.ObtenerEquivalencia("EquivAFIPPais", oSettings.EmpresaID, thisHeader.CompradorPais);
                    if (resultado == "")
                    {
                        bEquivalenciaError = true;
                        strEquivalenciaErrorFields += "Pais Comprador(" + thisHeader.CompradorPais + "); ";
                        sqlEngine.LogError(thisHeader.SQLID, "0", "Equivalencias", "No se encontró la equivalencia con AFIP del país (" + thisHeader.CompradorPais + ")");
                    }
                    else
                    {
                        thisHeader.CompradorPais = resultado;
                    }

                    //MONEDA TRANSACCIONES 0, 1, 2 y 3
                    resultado = sqlEngine.ObtenerEquivalencia("EquivAFIPMoneda",oSettings.EmpresaID, thisHeader.CodigoMoneda);
                    if (resultado == "")
                    {
                        bEquivalenciaError = true;
                        strEquivalenciaErrorFields += "Codigo Moneda(" + thisHeader.CodigoMoneda + "); ";
                        sqlEngine.LogError(thisHeader.SQLID, "0", "Equivalencias", "No se encontró la equivalencia con AFIP de la moneda (" + thisHeader.CodigoMoneda + ")");
                    }
                    else
                    {
                        thisHeader.CodigoMoneda = resultado;
                    }

                    //TIPO RESPONSABLE TRANSACCIONES 0, 1, 2 y 3
                    resultado = sqlEngine.ObtenerEquivalencia("EquivAFIPTipoResponsable", oSettings.EmpresaID, thisHeader.CompradorTipoResponsable);
                    if (resultado == "")
                    {
                        bEquivalenciaError = true;
                        strEquivalenciaErrorFields += "Tipo Responsable Comprador(" + thisHeader.CompradorTipoResponsable + "); ";
                        sqlEngine.LogError(thisHeader.SQLID, "0", "Equivalencias", "No se encontró la equivalencia con AFIP del tipo de responsable (" + thisHeader.CompradorTipoResponsable + ")");
                    }
                    else
                    {
                        thisHeader.CompradorTipoResponsable = resultado;
                    }

                    //SOLO PARA TRANSACCIONES 0 y 1 (BIENES Y SERVICIOS)
                    if (thisHeader.TipoTransaccion == "0" || thisHeader.TipoTransaccion == "1")
                    {                   
                        //CODIGO DOCUMENTO
                        resultado = sqlEngine.ObtenerEquivalencia("EquivAFIPCodigoDocumento", oSettings.EmpresaID, thisHeader.CompradorCodigoDocumento);
                        if (resultado == "")
                        {
                            bEquivalenciaError = true;
                            strEquivalenciaErrorFields += "Codigo Documento Comprador(" + thisHeader.CompradorCodigoDocumento + "); ";
                            sqlEngine.LogError(thisHeader.SQLID, "0", "Equivalencias", "No se encontró la equivalencia con AFIP del Código de documento (" + thisHeader.CompradorCodigoDocumento + ")");
                        }
                        else
                        {
                            thisHeader.CompradorCodigoDocumento = resultado;
                        }
                    }

                    //SOLO PARA TRANSACCIONES 3 (EXPORTACION)
                    if (thisHeader.TipoTransaccion == "3")
                    {
                        resultado = sqlEngine.ObtenerEquivalencia("EquivAFIPIncoterms", oSettings.EmpresaID, thisHeader.IncoTerms);
                        if (resultado == "")
                        {
                            bEquivalenciaError = true;
                            strEquivalenciaErrorFields += "Incoterms(" + thisHeader.IncoTerms + "); ";
                            sqlEngine.LogError(thisHeader.SQLID, "0", "Equivalencias", "No se encontró la equivalencia con AFIP del incoterms (" + thisHeader.IncoTerms + ")");
                        }
                        else
                        {
                            thisHeader.IncoTerms = resultado;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sqlEngine.LogError(strSQLID, "0", "Equivalencias", "Error no identificado: " + ex.Message);
                bEquivalenciaError = true;
            }
            return bEquivalenciaError;
        }
    }


}
