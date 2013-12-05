using System;
using FacturaElectronica.Common;
using FacturaElectronica.Utils;

namespace FacturaElectronica.WebServices
{
    public class AfipFE
    {
        wsfe.Service feService = new wsfe.Service();
        wsfe.FEAuthRequest objFEAuthRequest = new wsfe.FEAuthRequest();
        wsfe.CbteTipo objFELastCMType = new wsfe.CbteTipo();

        public AfipFE(ref wsfe.Service afipFeService, ref wsfe.FEAuthRequest afipObjFEAuthRequest, ref wsfe.CbteTipo afipObjFELastCMType)
        {
            feService = afipFeService;
            objFEAuthRequest = afipObjFEAuthRequest;
            objFELastCMType = afipObjFELastCMType;
        }

        public wsfe.FERecuperaLastCbteResponse FERecuperaUltimoComprobante(string tipoComprobante, string puntoVenta)
        {
            wsfe.FERecuperaLastCbteResponse objFERecuperaLastCMPResponse = new wsfe.FERecuperaLastCbteResponse();

            try
            {
                objFERecuperaLastCMPResponse = feService.FECompUltimoAutorizado(objFEAuthRequest, Convert.ToInt16(puntoVenta), Convert.ToInt16(tipoComprobante));
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("FERecuperaUltimoComprobante", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return objFERecuperaLastCMPResponse;
        }

        public wsfe.FECAEResponse FEAuthRequest(RequestBatch docBatch, Settings oSettings)
        {
            wsfe.FECAECabRequest objFECabeceraRequest = new wsfe.FECAECabRequest();
            wsfe.FECAEDetRequest objFEDetalleRequest = new wsfe.FECAEDetRequest();
            wsfe.FECAERequest objFERequest = new wsfe.FECAERequest();
            wsfe.FECAEResponse objFEResponse = new wsfe.FECAEResponse();
            wsfe.AlicIva objIva = null;
            wsfe.Tributo objTributo = null;

            DBEngine.SQLEngine sqlEngine = new FacturaElectronica.DBEngine.SQLEngine();

            int i = 0;
            int iIvaItems = 0;
            int iTributosItems = 0;

            try
            {
                objFECabeceraRequest.CantReg = Convert.ToInt16(docBatch.CantidadComprobantes);
                objFECabeceraRequest.PtoVta = Convert.ToInt16(docBatch.RequestHeaders[0].PuntoVenta);
                objFECabeceraRequest.CbteTipo = Convert.ToInt16(docBatch.RequestHeaders[0].TipoComprobante);

                wsfe.FECAEDetRequest[] aObjFEDetalleRequest = new wsfe.FECAEDetRequest[Convert.ToInt16(objFECabeceraRequest.CantReg)];
                
                int arrayIndex = 0;
                for (i = 0; i < objFECabeceraRequest.CantReg; i++)
                {
                    objFEDetalleRequest = new wsfe.FECAEDetRequest();

                    //Si el numero de documento viene vacio asumo que es un consumidor final 99
                    if (docBatch.RequestHeaders[i].CompradorNroDocumento == string.Empty)
                    {
                        docBatch.RequestHeaders[i].CompradorCodigoDocumento = "99";
                        docBatch.RequestHeaders[i].CompradorNroDocumento = "0";
                    }

                    objFEDetalleRequest.DocTipo = Convert.ToInt16(docBatch.RequestHeaders[i].CompradorCodigoDocumento);
                    objFEDetalleRequest.DocNro = (long)Convert.ToDouble(docBatch.RequestHeaders[i].CompradorNroDocumento);
                    
                    objFEDetalleRequest.CbteDesde = (long)Convert.ToDouble(docBatch.RequestHeaders[i].NroComprobanteDesde);
                    objFEDetalleRequest.CbteHasta = (long)Convert.ToDouble(docBatch.RequestHeaders[i].NroComprobanteHasta);
                    objFEDetalleRequest.ImpTotal = Convert.ToDouble(docBatch.RequestHeaders[i].Importe);
                    objFEDetalleRequest.ImpTotConc = Convert.ToDouble(docBatch.RequestHeaders[i].ImporteNoGravado);
                    objFEDetalleRequest.ImpNeto = Convert.ToDouble(docBatch.RequestHeaders[i].ImporteGravado);
                    //objFEDetalleRequest.impto_liq_rni = Convert.ToDouble(docBatch.RequestHeaders[i].ImporteRNI_Percepcion);????
                    //objFEDetalleRequest.ImpIVA = Convert.ToDouble(docBatch.RequestHeaders[i].ImporteImpuestoLiquidado);
                    
                    objFEDetalleRequest.ImpOpEx = Convert.ToDouble(docBatch.RequestHeaders[i].ImporteExento);

                    //Agrego todas las alicuotas en array
                    //1 No gravado
                    //2 Exento
                    //3 0%
                    //4 10.5%
                    //5 21%
                    //6 27%                  
                    if (docBatch.RequestHeaders[i].RequestAlicuotas.Count > 0)
                    {
                        objFEDetalleRequest.Iva = new wsfe.AlicIva[docBatch.RequestHeaders[i].RequestAlicuotas.Count];
                        iIvaItems = 0;
                        foreach (RequestAlicuota alic in docBatch.RequestHeaders[i].RequestAlicuotas)
                        {
                            if (alic != null)
                            {
                                objIva = new wsfe.AlicIva();
                                objIva.Id = Convert.ToInt32(alic.Id);
                                objIva.BaseImp = Convert.ToDouble(String.Format("{0:0.00}", alic.BaseImp));
                                objIva.Importe = Convert.ToDouble(String.Format("{0:0.00}", alic.Importe));

                                objFEDetalleRequest.Iva[iIvaItems] = objIva;

                                iIvaItems++;

                                //Agrego importe al total de alicuotas de IVA
                                objFEDetalleRequest.ImpIVA += objIva.Importe;
                            }
                        }
                    }
                    objFEDetalleRequest.ImpIVA = Convert.ToDouble(String.Format("{0:0.00}", objFEDetalleRequest.ImpIVA));

                    //Agrego todos los tributos en array
                    //1	Impuestos nacionales
                    //2	Impuestos provinciales
                    //3	Impuestos municipales
                    //4	Impuestos internos
                    //99 Otros      
                    if (docBatch.RequestHeaders[i].RequestTributos.Count > 0)
                    {
                        objFEDetalleRequest.Tributos = new wsfe.Tributo[docBatch.RequestHeaders[i].RequestTributos.Count];
                        iTributosItems = 0;
                        foreach (RequestTributo trib in docBatch.RequestHeaders[i].RequestTributos)
                        {
                            if (trib != null)
                            {
                                objTributo = new wsfe.Tributo();
                                objTributo.Id = Convert.ToInt16(trib.Id);
                                objTributo.BaseImp = Convert.ToDouble(String.Format("{0:0.00}", trib.BaseImp));
                                objTributo.Importe = Convert.ToDouble(String.Format("{0:0.00}", trib.Importe));
                                objTributo.Alic = Convert.ToDouble(String.Format("{0:#,##0.00}", trib.Alic)); 

                                objFEDetalleRequest.Tributos[iTributosItems] = objTributo;

                                iTributosItems++;

                                //Agrego importe al total de tributos
                                objFEDetalleRequest.ImpTrib += objTributo.Importe;
                            }
                        }
                    }

                    // Las fechas deben venir en formato "YYYY-MM-DD"
                    objFEDetalleRequest.CbteFch = Convert.ToDateTime(docBatch.RequestHeaders[i].FechaComprobante).ToString("yyyyMMdd");

                    //Es 1 si son productos 2 servicios, 3 productos y servicios
                    if (docBatch.SonServicios == "1")
                        objFEDetalleRequest.Concepto = 3;
                    else
                        objFEDetalleRequest.Concepto = 1;

                    if (objFEDetalleRequest.Concepto == 3)
                    {
                        objFEDetalleRequest.FchServDesde = Convert.ToDateTime(docBatch.RequestHeaders[i].FechaDesdeServicioFacturado).ToString("yyyyMMdd");
                        objFEDetalleRequest.FchServHasta = Convert.ToDateTime(docBatch.RequestHeaders[i].FechaHastaServicioFacturado).ToString("yyyyMMdd");
                        objFEDetalleRequest.FchVtoPago = Convert.ToDateTime(docBatch.RequestHeaders[i].FechaVencimientoPago).ToString("yyyyMMdd");
                    }

                    objFEDetalleRequest.MonId = docBatch.RequestHeaders[i].CodigoMoneda;
                    objFEDetalleRequest.MonCotiz = 1;

                    aObjFEDetalleRequest[arrayIndex] = objFEDetalleRequest;
                    arrayIndex += 1;
                }

                objFERequest.FeCabReq = objFECabeceraRequest;
                objFERequest.FeDetReq = aObjFEDetalleRequest;

                //DEBUG LINE
                if (Convert.ToBoolean(oSettings.ActivarDebug))
                    Utils.Utils.DebugLine(Utils.Utils.SerializeObject(objFERequest), oSettings.PathDebug + "\\" + docBatch.RequestHeaders[0].SQLID + "-P2.FERequest-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + DateTime.Now.Millisecond.ToString("000") + ".xml");

                objFEResponse = feService.FECAESolicitar(objFEAuthRequest, objFERequest);
            }
            catch (Exception ex)
            {
                int iElement = 0;
                if(i > 0) 
                    iElement = i - 1;
                else
                    iElement = 0;

                objFEResponse.Errors = new wsfe.Err[1];
                objFEResponse.Errors[0] = new wsfe.Err();
                objFEResponse.Errors[0].Code = 668;
                objFEResponse.Errors[0].Msg = ex.Message;

                string url = oSettings.UrlFEWebService;
                sqlEngine.LogError(docBatch.RequestHeaders[iElement].SQLID, "0", "Autorización", "Error no conocido: (AfipFE) " + ex.Message);
            }

            return objFEResponse;
        }
    }
}
