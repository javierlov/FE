using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using FacturaElectronica.Utils;
using FacturaElectronica.Common;

namespace FacturaElectronica.Service.Engine
{
    public class DBIntegration
    {
        DBEngine.SQLEngine sqlEngine = new DBEngine.SQLEngine();

        public RequestBatch ProcessData(DataRow drCabecera, DataTable dtLineas, DataTable dtImpuestos, Settings oSettings)
        {
            RequestBatch requestBatch = new RequestBatch();
            RequestLine thisLine = new RequestLine();
            RequestAlicuota requestAlicuota = new RequestAlicuota();
            RequestTributo requestTributo = new RequestTributo();

            //Totales del lote
            double Importe = 0;
            double ImporteComprobanteB = 0;
            double ImporteNoGravado = 0;
            double ImporteGravado = 0;
            double ImporteImpuestoLiquidado = 0;
            double ImporteRNI_Percepcion = 0;
            double ImporteExento = 0;
            double ImportePercepciones_PagosCuentaImpuestosNacionales = 0;
            double ImportePercepcionIIBB = 0;
            double ImportePercepcionImpuestosMunicipales = 0;
            double ImporteImpuestosInternos = 0;
            double ImporteMonedaFacturacion = 0;
            double ImporteMonedaFacturacionComprobanteB = 0;
            double ImporteNoGravadoMonedaFacturacion = 0;
            double ImporteGravadoMonedaFacturacion = 0;
            double ImporteImpuestoLiquidadoMonedaFacturacion = 0;
            double ImporteRNI_PercepcionMonedaFacturacion = 0;
            double ImporteExentoMonedaFacturacion = 0;
            double ImportePercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion = 0;
            double ImportePercepcionIIBBMonedaFacturacion = 0;
            double ImportePercepcionImpuestosMunicipalesMonedaFacturacion = 0;
            double ImporteImpuestosInternosMonedaFacturacion = 0;

            int qtyComprobantes = 0;

            try
            {
                if(drCabecera != null)
                {
                    //DEBUG LINE
                    if (Convert.ToBoolean(oSettings.ActivarDebug))                    
                        drCabecera.Table.WriteXml(oSettings.PathDebug + "\\" + GetDataRowString(drCabecera["NROCOMPROBANTE"]) + "-Cabecera-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + DateTime.Now.Millisecond.ToString("000") + ".xml");

                    //DEBUG LINE
                    if (Convert.ToBoolean(oSettings.ActivarDebug))
                        dtLineas.WriteXml(oSettings.PathDebug + "\\" + GetDataRowString(drCabecera["NROCOMPROBANTE"]) + "-Lineas-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + DateTime.Now.Millisecond.ToString("000") + ".xml");

                    RequestHeader thisDocument = new RequestHeader();

                    thisDocument.SQLID = string.Empty; 
                    thisDocument.EmpresaID = oSettings.EmpresaID;
                    thisDocument.TipoTransaccion = GetDataRowString(drCabecera["TIPOTRANSACCION"]);
                    thisDocument.FechaComprobante = GetDataRowString(drCabecera["FECHACOMPROBANTE"]);
                    thisDocument.FechaDesdeServicioFacturado = GetDataRowString(drCabecera["FECHACOMPROBANTE"]);
                    thisDocument.FechaHastaServicioFacturado = GetDataRowString(drCabecera["FECHACOMPROBANTE"]);
                    thisDocument.TipoComprobante = GetDataRowString(drCabecera["TIPOCOMPROBANTE"]);
                    thisDocument.PuntoVenta = GetDataRowString(drCabecera["PUNTOVENTA"]);
                    thisDocument.LetraComprobante = GetDataRowString(drCabecera["LETRACOMPROBANTE"]);
                    thisDocument.NroComprobanteDesde = GetDataRowString(drCabecera["NROCOMPROBANTE"]);
                    thisDocument.NroComprobanteHasta = GetDataRowString(drCabecera["NROCOMPROBANTE"]);
                    thisDocument.NroInternoERP = GetDataRowString(drCabecera["IDREGISTROCABECERA"]);
                    thisDocument.FechaVencimientoPago = GetDataRowString(drCabecera["FECHAVENCIMIENTOPAGO"]);
                    thisDocument.CondicionPago = GetDataRowString(drCabecera["CONDICIONPAGO"]);
                    thisDocument.CompradorCodigoDocumento = GetDataRowString(drCabecera["CODIGODOCUMENTOCOMPRADOR"]);
                    thisDocument.CompradorNroDocumento = GetDataRowString(drCabecera["NRODOCUMENTOCOMPRADOR"]);
                    thisDocument.CompradorTipoResponsable = GetDataRowString(drCabecera["TIPORESPONSABLECOMPRADOR"]);
                    thisDocument.CompradorTipoResponsableDescripcion = GetDataRowString(drCabecera["TIPORESPONSABLECOMPRADORDESCRIPCION"]);
                    thisDocument.CompradorRazonSocial = GetDataRowString(drCabecera["RAZONSOCIALCOMPRADOR"]);
                    thisDocument.CompradorDireccion = GetDataRowString(drCabecera["DIRECCIONCOMPRADOR"]);
                    thisDocument.CompradorLocalidad = GetDataRowString(drCabecera["LOCALIDADCOMPRADOR"]);
                    thisDocument.CompradorProvincia = GetDataRowString(drCabecera["PROVINCIACOMPRADOR"]);
                    thisDocument.CompradorPais = GetDataRowString(drCabecera["PAISCOMPRADOR"]);
                    thisDocument.CompradorCodigoPostal = GetDataRowString(drCabecera["CODIGOPOSTALCOMPRADOR"]);
                    thisDocument.CompradorNroIIBB = GetDataRowString(drCabecera["NROIIBBCOMPRADOR"]);
                    thisDocument.CompradorCodigoCliente = GetDataRowString(drCabecera["CODIGOCLIENTECOMPRADOR"]);
                    thisDocument.CompradorNroReferencia = string.Empty;
                    thisDocument.CompradorEmail = string.Empty;
                    thisDocument.NroRemito = string.Empty;
                    thisDocument.Importe = GetDataRowString(drCabecera["IMPORTE"]);
                    thisDocument.ImporteComprobanteB = "0";
                    thisDocument.ImporteNoGravado = GetDataRowString(drCabecera["IMPORTENOGRAVADO"]);
                    thisDocument.ImporteGravado = GetDataRowString(drCabecera["IMPORTEGRAVADO"]);
                    thisDocument.AlicuotaIVA = "0";
                    thisDocument.ImporteImpuestoLiquidado = GetDataRowString(drCabecera["IMPORTEIMPUESTOLIQUIDADO"]);
                    thisDocument.ImporteRNI_Percepcion = GetDataRowString(drCabecera["IMPORTERNI_PERCEPCION"]);
                    thisDocument.ImporteExento = GetDataRowString(drCabecera["IMPORTEEXENTO"]);
                    thisDocument.ImportePercepciones_PagosCuentaImpuestosNacionales = GetDataRowString(drCabecera["IMPORTEPERCEPCIONES_PAGOSCUENTAIMPUESTOSNACIONALES"]);
                    thisDocument.ImportePercepcionIIBB = GetDataRowString(drCabecera["IMPORTEPERCEPCIONIIBB"]);
                    thisDocument.TasaIIBB = GetDataRowString(drCabecera["TASAIIBB"]);
                    thisDocument.CodigoJurisdiccionIIBB = GetDataRowString(drCabecera["CODIGOJURISDICCIONIIBB"]);
                    thisDocument.ImportePercepcionImpuestosMunicipales = GetDataRowString(drCabecera["IMPORTEPERCEPCIONIMPUESTOSMUNICIPALES"]);
                    thisDocument.JurisdiccionImpuestosMunicipales = GetDataRowString(drCabecera["JURISDICCIONIMPUESTOSMUNICIPALES"]);
                    thisDocument.ImporteImpuestosInternos = GetDataRowString(drCabecera["IMPORTEIMPUESTOSINTERNOS"]);
                    thisDocument.ImporteMonedaFacturacion = GetDataRowString(drCabecera["IMPORTEMONEDAFACTURACION"]);
                    thisDocument.ImporteMonedaFacturacionComprobanteB = "0";
                    thisDocument.ImporteNoGravadoMonedaFacturacion = GetDataRowString(drCabecera["IMPORTENOGRAVADOMONEDAFACTURACION"]);
                    thisDocument.ImporteGravadoMonedaFacturacion = GetDataRowString(drCabecera["IMPORTEGRAVADOMONEDAFACTURACION"]);
                    thisDocument.ImporteImpuestoLiquidadoMonedaFacturacion = GetDataRowString(drCabecera["IMPORTEIMPUESTOLIQUIDADOMONEDAFACTURACION"]);
                    thisDocument.ImporteRNI_PercepcionMonedaFacturacion = GetDataRowString(drCabecera["IMPORTERNI_PERCEPCIONMONEDAFACTURACION"]);
                    thisDocument.ImporteExentoMonedaFacturacion = GetDataRowString(drCabecera["IMPORTEEXENTOMONEDAFACTURACION"]);
                    thisDocument.ImportePercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion = GetDataRowString(drCabecera["IMPORTEPERCEPCIONES_PAGOSCUENTAIMPUESTOSNACIONALESMONEDAFACTURACION"]);
                    thisDocument.ImportePercepcionIIBBMonedaFacturacion = GetDataRowString(drCabecera["IMPORTEPERCEPCIONIIBBMONEDAFACTURACION"]);
                    thisDocument.ImportePercepcionImpuestosMunicipalesMonedaFacturacion = GetDataRowString(drCabecera["IMPORTEPERCEPCIONIMPUESTOSMUNICIPALESMONEDAFACTURACION"]);
                    thisDocument.ImporteImpuestosInternosMonedaFacturacion = GetDataRowString(drCabecera["IMPORTEIMPUESTOSINTERNOSMONEDAFACTURACION"]);
                    thisDocument.CantidadAlicuotasIVA = GetDataRowString(drCabecera["CANTIDADALICUOTASIVA"]);
                    thisDocument.CodigoOperacion = GetDataRowString(drCabecera["CODIGOOPERACION"]);
                    thisDocument.TasaCambio = GetDataRowString(drCabecera["TASACAMBIO"]);
                    thisDocument.CodigoMoneda = GetDataRowString(drCabecera["CODIGOMONEDA"]);
                    thisDocument.ImporteEscrito = GetDataRowString(drCabecera["IMPORTEESCRITO"]);
                    thisDocument.CantidadRegistrosDetalle = dtLineas.Rows.Count.ToString();
                    thisDocument.CodigoMecanismoDistribucion = string.Empty;
                    thisDocument.TipoExportacion = string.Empty;
                    thisDocument.PermisoExistente = string.Empty;
                    thisDocument.FormaPagoDescripcion = string.Empty;
                    thisDocument.IncoTerms = GetDataRowString(drCabecera["INCOTERMS"]);
                    thisDocument.Idioma = string.Empty;
                    thisDocument.Observaciones1 = GetDataRowString(drCabecera["OBSERVACIONCABECERA"]);
                    thisDocument.Observaciones2 = GetDataRowString(drCabecera["INFOADIC"]);
                    thisDocument.Observaciones3 = GetDataRowString(drCabecera["OBSERVACIONPIE"]);
                    thisDocument.EmisorDireccion = string.Empty;
                    thisDocument.EmisorCalle = GetDataRowString(drCabecera["EMISORCALLE"]);
                    thisDocument.EmisorCP = GetDataRowString(drCabecera["EMISORCP"]);
                    thisDocument.EmisorLocalidad = GetDataRowString(drCabecera["EMISORLOCALIDAD"]);
                    thisDocument.EmisorProvincia = GetDataRowString(drCabecera["EMISORPROVINCIA"]);
                    thisDocument.EmisorPais = GetDataRowString(drCabecera["EMISORPAIS"]);
                    thisDocument.EmisorTelefonos = GetDataRowString(drCabecera["EMISORTELEFONOS"]);
                    thisDocument.EmisorEMail = GetDataRowString(drCabecera["EMISOREMAIL"]);
                    thisDocument.OficinaVentas = string.Empty;
                    //thisDocument.PagoFacil = GetDataRowString(drCabecera["PagoFacil"]);
                    thisDocument.RapiPago = GetDataRowString(drCabecera["RapiPago"]);
                    thisDocument.ObservacionRapiPago = GetDataRowString(drCabecera["ObservacionRapiPago"]);
                    thisDocument.OPER = GetDataRowString(drCabecera["OPER"]);
                    thisDocument.NOPER = GetDataRowString(drCabecera["NOPER"]);
                    thisDocument.FACTORI = GetDataRowString(drCabecera["FACTORI"]);
                    thisDocument.FACTORI_FORMATEADO = GetDataRowString(drCabecera["FACTORI_FORMATEADO"]);
                    thisDocument.DAGRUF = GetDataRowString(drCabecera["DAGRUF"]);
                    thisDocument.USUARIO = GetDataRowString(drCabecera["USUARIO"]);

                    thisDocument.FECPG1_FORMATEADO = GetDataRowString(drCabecera["FECPG1_FORMATEADO"]);
                    thisDocument.FECPG2_FORMATEADO = GetDataRowString(drCabecera["FECPG2_FORMATEADO"]);

                    thisDocument.CUOTAIVA105 = GetDataRowString(drCabecera["CUOTAIVA105"]);
                    thisDocument.CUOTAIVA21 = GetDataRowString(drCabecera["CUOTAIVA21"]); 
                    

                    //Actualizar los importes del lote
                    Importe += Utils.Utils.DoubleFromString(thisDocument.Importe);
                    ImporteComprobanteB += Utils.Utils.DoubleFromString(thisDocument.ImporteComprobanteB);
                    ImporteNoGravado += Utils.Utils.DoubleFromString(thisDocument.ImporteNoGravado);
                    ImporteGravado += Utils.Utils.DoubleFromString(thisDocument.ImporteGravado);
                    ImporteImpuestoLiquidado += Utils.Utils.DoubleFromString(thisDocument.ImporteImpuestoLiquidado);
                    ImporteRNI_Percepcion += Utils.Utils.DoubleFromString(thisDocument.ImporteRNI_Percepcion);
                    ImporteExento += Utils.Utils.DoubleFromString(thisDocument.ImporteExento);
                    ImportePercepciones_PagosCuentaImpuestosNacionales += Utils.Utils.DoubleFromString(thisDocument.ImportePercepciones_PagosCuentaImpuestosNacionales);
                    ImportePercepcionIIBB += Utils.Utils.DoubleFromString(thisDocument.ImportePercepcionIIBB);
                    ImportePercepcionImpuestosMunicipales += Utils.Utils.DoubleFromString(thisDocument.ImportePercepcionImpuestosMunicipales);
                    ImporteImpuestosInternos += Utils.Utils.DoubleFromString(thisDocument.ImporteImpuestosInternos);
                    ImporteMonedaFacturacion += Utils.Utils.DoubleFromString(thisDocument.ImporteMonedaFacturacion);
                    ImporteMonedaFacturacionComprobanteB += Utils.Utils.DoubleFromString(thisDocument.ImporteMonedaFacturacionComprobanteB);
                    ImporteNoGravadoMonedaFacturacion += Utils.Utils.DoubleFromString(thisDocument.ImporteNoGravadoMonedaFacturacion);
                    ImporteGravadoMonedaFacturacion += Utils.Utils.DoubleFromString(thisDocument.ImporteGravadoMonedaFacturacion);
                    ImporteImpuestoLiquidadoMonedaFacturacion += Utils.Utils.DoubleFromString(thisDocument.ImporteImpuestoLiquidadoMonedaFacturacion);
                    ImporteRNI_PercepcionMonedaFacturacion += Utils.Utils.DoubleFromString(thisDocument.ImporteRNI_PercepcionMonedaFacturacion);
                    ImporteExentoMonedaFacturacion += Utils.Utils.DoubleFromString(thisDocument.ImporteExentoMonedaFacturacion);
                    ImportePercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion += Utils.Utils.DoubleFromString(thisDocument.ImportePercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion);
                    ImportePercepcionIIBBMonedaFacturacion += Utils.Utils.DoubleFromString(thisDocument.ImportePercepcionIIBBMonedaFacturacion);
                    ImportePercepcionImpuestosMunicipalesMonedaFacturacion += Utils.Utils.DoubleFromString(thisDocument.ImportePercepcionImpuestosMunicipalesMonedaFacturacion);
                    ImporteImpuestosInternosMonedaFacturacion += Utils.Utils.DoubleFromString(thisDocument.ImporteImpuestosInternosMonedaFacturacion);

                    //proceso las lineas
                    foreach (DataRow drLine in dtLineas.Rows)
                    {
                        thisLine = new RequestLine();

                        thisLine.CodigoProductoEmpresa = GetDataRowString(drLine["CODIGOPRODUCTOEMPRESA"]);
                        thisLine.CodigoProductoNCM = string.Empty;
                        thisLine.CodigoProductoSecretaria = string.Empty;
                        thisLine.Descripcion = GetDataRowString(drLine["DESCRIPCION"]);
                        thisLine.Cantidad = GetDataRowString(drLine["CANTIDAD"]);
                        thisLine.UnidadMedida = GetDataRowString(drLine["UNIDADMEDIDA"]);
                        thisLine.ImportePrecioUnitario = GetDataRowString(drLine["IMPORTEPRECIOUNITARIO"]);
                        thisLine.ImporteBonificacion = GetDataRowString(drLine["IMPORTEBONIFICACION"]);
                        thisLine.ImporteAjuste = "0";
                        thisLine.ImporteSubtotal = GetDataRowString(drLine["IMPORTESUBTOTAL"]);
                        thisLine.ImportePrecioUnitarioMonedaFacturacion = GetDataRowString(drLine["IMPORTEPRECIOUNITARIOMONEDAFACTURACION"]);
                        thisLine.ImporteBonificacionMonedaFacturacion = GetDataRowString(drLine["IMPORTEBONIFICACIONMONEDAFACTURACION"]);
                        thisLine.ImporteAjusteMonedaFacturacion = GetDataRowString(drLine["IMPORTEAJUSTEMONEDAFACTURACION"]);
                        thisLine.ImporteSubtotalMonedaFacturacion = GetDataRowString(drLine["IMPORTESUBTOTALMONEDAFACTURACION"]);
                        thisLine.ImporteSubtotalMonedaFacturacionConIVA = GetDataRowString(drLine["IMPORTESUBTOTALMONEDAFACTURACIONCONIVA"]);
                        thisLine.AlicuotaIVA = GetDataRowString(drLine["ALICUOTAIVA"]);
                        thisLine.IndicadorExentoGravadoNoGravado = GetDataRowString(drLine["INDICADOREXENTOGRAVADONOGRAVADO"]);
                        thisLine.Observaciones = GetDataRowString(drLine["OBSERVACIONES"]); ;
                        thisLine.MesPrestacion = GetDataRowString(drLine["MESPRESTACION"]); ;

                        thisDocument.RequestLines.Add(thisLine);
                    }

                    //proceso los impuestos (Alicuotas y Tributos)
                    foreach (DataRow drImpuesto in dtImpuestos.Rows)
                    {
                        switch(GetDataRowString(drImpuesto["TIPO"]))
                        {
                            case "Alicuota":
                                requestAlicuota = new RequestAlicuota();

                                requestAlicuota.Id = GetDataRowString(drImpuesto["ID"]);
                                requestAlicuota.CbteID = GetDataRowString(drImpuesto["CBTEID"]);
                                requestAlicuota.Importe = GetDataRowString(drImpuesto["IMPORTE"]);
                                requestAlicuota.BaseImp = GetDataRowString(drImpuesto["BASEIMP"]);
                                requestAlicuota.Tipo = GetDataRowString(drImpuesto["TIPO"]);
                                requestAlicuota.Descripcion = GetDataRowString(drImpuesto["DESCRIPCION"]);

                                thisDocument.RequestAlicuotas.Add(requestAlicuota);
                                break;

                            case "Tributo":
                                requestTributo = new RequestTributo();

                                requestTributo.Id = GetDataRowString(drImpuesto["ID"]);
                                requestTributo.CbteID = GetDataRowString(drImpuesto["CBTEID"]);
                                requestTributo.Importe = GetDataRowString(drImpuesto["IMPORTE"]);
                                requestTributo.BaseImp = GetDataRowString(drImpuesto["BASEIMP"]);
                                requestTributo.Tipo = GetDataRowString(drImpuesto["TIPO"]);
                                requestTributo.Descripcion = GetDataRowString(drImpuesto["DESCRIPCION"]);
                                requestTributo.Alic = GetDataRowString(drImpuesto["ALIC"]);

                                thisDocument.RequestTributos.Add(requestTributo);
                                break;
                        }
                    }

                    requestBatch.RequestHeaders.Add(thisDocument);

                    qtyComprobantes++;

                    requestBatch.CantidadComprobantes = qtyComprobantes.ToString();
                    requestBatch.Total = IntegrationEngine.ChangeDecimalPointToPoint(String.Format("{0:0.00}", Importe));
                    requestBatch.TotalComprobanteB = IntegrationEngine.ChangeDecimalPointToPoint(String.Format("{0:0.00}", ImporteComprobanteB));
                    requestBatch.TotalExento = IntegrationEngine.ChangeDecimalPointToPoint(String.Format("{0:0.00}", ImporteExento));
                    requestBatch.TotalExentoMonedaFacturacion = IntegrationEngine.ChangeDecimalPointToPoint(String.Format("{0:0.00}", ImporteExentoMonedaFacturacion));
                    requestBatch.TotalGravado = IntegrationEngine.ChangeDecimalPointToPoint(String.Format("{0:0.00}", ImporteGravado));
                    requestBatch.TotalGravadoMonedaFacturacion = IntegrationEngine.ChangeDecimalPointToPoint(String.Format("{0:0.00}", ImporteGravadoMonedaFacturacion));
                    requestBatch.TotalImpuestoLiquidado = IntegrationEngine.ChangeDecimalPointToPoint(String.Format("{0:0.00}", ImporteImpuestoLiquidado));
                    requestBatch.TotalImpuestoLiquidadoMonedaFacturacion = IntegrationEngine.ChangeDecimalPointToPoint(String.Format("{0:0.00}", ImporteImpuestoLiquidadoMonedaFacturacion));
                    requestBatch.TotalImpuestosInternos = IntegrationEngine.ChangeDecimalPointToPoint(String.Format("{0:0.00}", ImporteImpuestosInternos));
                    requestBatch.TotalImpuestosInternosMonedaFacturacion = IntegrationEngine.ChangeDecimalPointToPoint(String.Format("{0:0.00}", ImporteImpuestosInternosMonedaFacturacion));
                    requestBatch.TotalMonedaFacturacion = IntegrationEngine.ChangeDecimalPointToPoint(String.Format("{0:0.00}", ImporteMonedaFacturacion));
                    requestBatch.TotalMonedaFacturacionComprobanteB = IntegrationEngine.ChangeDecimalPointToPoint(String.Format("{0:0.00}", ImporteMonedaFacturacionComprobanteB));
                    requestBatch.TotalNoGravado = IntegrationEngine.ChangeDecimalPointToPoint(String.Format("{0:0.00}", ImporteNoGravado));
                    requestBatch.TotalNoGravadoMonedaFacturacion = IntegrationEngine.ChangeDecimalPointToPoint(String.Format("{0:0.00}", ImporteNoGravadoMonedaFacturacion));
                    requestBatch.TotalPercepciones_PagosCuentaImpuestosNacionales = IntegrationEngine.ChangeDecimalPointToPoint(String.Format("{0:0.00}", ImportePercepciones_PagosCuentaImpuestosNacionales));
                    requestBatch.TotalPercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion = IntegrationEngine.ChangeDecimalPointToPoint(String.Format("{0:0.00}", ImportePercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion));
                    requestBatch.TotalPercepcionIIBB = IntegrationEngine.ChangeDecimalPointToPoint(String.Format("{0:0.00}", ImportePercepcionIIBB));
                    requestBatch.TotalPercepcionIIBBMonedaFacturacion = IntegrationEngine.ChangeDecimalPointToPoint(String.Format("{0:0.00}", ImportePercepcionIIBBMonedaFacturacion));
                    requestBatch.TotalPercepcionImpuestosMunicipales = IntegrationEngine.ChangeDecimalPointToPoint(String.Format("{0:0.00}", ImportePercepcionImpuestosMunicipales));
                    requestBatch.TotalPercepcionImpuestosMunicipalesMonedaFacturacion = IntegrationEngine.ChangeDecimalPointToPoint(String.Format("{0:0.00}", ImportePercepcionImpuestosMunicipalesMonedaFacturacion));
                    requestBatch.TotalRNI_Percepcion = IntegrationEngine.ChangeDecimalPointToPoint(String.Format("{0:0.00}", ImporteRNI_Percepcion));
                    requestBatch.TotalRNI_PercepcionMonedaFacturacion = IntegrationEngine.ChangeDecimalPointToPoint(String.Format("{0:0.00}", ImporteRNI_PercepcionMonedaFacturacion));

                    //TODO: Lote automático donde configurarlo?
                    requestBatch.BatchUniqueId = "AUTO";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Servicio Factura Electronica", "ProcessData.Error:" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return requestBatch;
        }

        public void ProcessInitialized(string SQLID, string NroInternoERP, Settings oSettings)
        {
            string[] FieldNames = null;
            string[] FieldTypes = null;
            string[] FieldValues = null;

            try
            {
                if ( NroInternoERP != string.Empty)
                {
                    FieldNames = new string[] { "ESTADO" };

                    //TODO: usar enum del SqlType
                    FieldTypes = new string[] { "NVarChar"};

                    FieldValues = new string[] { "INICIADO"};

                    try
                    {
                        //modifico base intermedia
                        SetData(oSettings.Entrada.Split('\\')[0], oSettings.Entrada.Split('\\')[1], oSettings.Entrada.Split('\\')[2], oSettings.Entrada.Split('\\')[3], NroInternoERP, FieldNames, FieldTypes, FieldValues);
                    }
                    catch (Exception ex)
                    {
                        sqlEngine.LogError(SQLID, "0", "ProcessResponse. Guardar en Tabla Intermedia.", "Error: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void ProcessResponse(ResponseBatch responseBatch, Settings oSettings)
        {
            XmlDocument xmlResponse = new XmlDocument();
            XmlDocument xmlError = new XmlDocument();

            string[] FieldNames = null;
            string[] FieldTypes = null;
            string[] FieldValues = null;

            string ErrorCod = string.Empty;
            string ErrorMsg = string.Empty;
            
            string SqlID = "0"; 

            try
            {
                if (responseBatch != null && responseBatch.ResponseHeaders != null && responseBatch.ResponseHeaders.Count > 0)
                {
                    SqlID = responseBatch.ResponseHeaders[0].SQLID;

                    FieldNames = new string[] { "ESTADO", "CAE", "FECHAVENCCAE", "RESULTADO", "ERRCOD", "ERRMNG" };

                    //TODO: usar enum del SqlType
                    FieldTypes = new string[] { "NVarChar", "NVarChar", "SmallDateTime", "NVarChar", "NVarChar", "NVarChar" };

                    if (responseBatch != null && responseBatch.ResponseHeaders != null && responseBatch.ResponseHeaders.Count > 0)
                    {
                        if (responseBatch.Reproceso == "s" && responseBatch.ResponseHeaders[0].Resultado == "R")
                        {
                            ErrorCod = "12";
                            ErrorMsg = "El rango informado se encuentra autorizado con anterioridad para la misma CUIT, Tipo de Comprobante y Punto de Venta.";
                        }
                        else
                        {
                            if (responseBatch.ResponseHeaders[0].Observaciones.Count > 0)
                            {
                                foreach (ResponseHeaderObs obs in responseBatch.ResponseHeaders[0].Observaciones)
                                {
                                    ErrorCod += obs.Codigo + ";";
                                    ErrorMsg += obs.Msg + ";";
                                }
                            }
                            else
                            {
                                if (responseBatch.ResponseHeaders[0].Motivo != string.Empty)
                                {
                                    ErrorCod = responseBatch.ResponseHeaders[0].Motivo;
                                }
                                else
                                {
                                    ErrorCod = responseBatch.CodigoError;
                                }

                                if (responseBatch.ResponseHeaders[0].MotivoDescripcion != string.Empty)
                                {
                                    ErrorMsg = responseBatch.ResponseHeaders[0].MotivoDescripcion;
                                }
                                else
                                {
                                    ErrorMsg = responseBatch.MotivoDescripcion;
                                }
                            }
                        }
                        FieldValues = new string[] { "PROCESADO", responseBatch.ResponseHeaders[0].CAE, responseBatch.ResponseHeaders[0].FechaVencimiento, responseBatch.Resultado, ErrorCod, ErrorMsg };
                    }
                    else
                    {
                        ErrorCod = responseBatch.CodigoError;
                        ErrorMsg = responseBatch.MotivoDescripcion;

                        FieldValues = new string[] { "PROCESADO", "", "", responseBatch.Resultado, ErrorCod, ErrorMsg };
                    }

                    if (ErrorCod != string.Empty || ErrorMsg != string.Empty)
                    {
                        sqlEngine.LogError(SqlID, "0", ErrorCod, ErrorMsg);
                    }

                    try
                    {
                        //modifico base intermedia
                        SetData(oSettings.Entrada.Split('\\')[0], oSettings.Entrada.Split('\\')[1], oSettings.Entrada.Split('\\')[2], oSettings.Entrada.Split('\\')[3], responseBatch.ResponseHeaders[0].NroInternoERP, FieldNames, FieldTypes, FieldValues);

                        if (Convert.ToBoolean(oSettings.ActivarDebug))
                            Utils.Utils.DebugLine("<DBResult><Accion>MODIFICAR BASE INTERMEDIA(SetData)</Accion><Resultado>OK</Resultado></DBResult>", oSettings.PathDebug + "\\" + SqlID + "-P5.DBUPDATE-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + DateTime.Now.Millisecond.ToString("000") + ".xml");
                    }
                    catch (Exception ex)
                    {
                        if (Convert.ToBoolean(oSettings.ActivarDebug))
                            Utils.Utils.DebugLine("<DBResult><Accion>MODIFICAR BASE INTERMEDIA(SetData)</Accion><Resultado>Error: " + ex.Message + "</Resultado></DBResult>", oSettings.PathDebug + "\\" + SqlID + "-P5.DBUPDATE-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + DateTime.Now.Millisecond.ToString("000") + ".xml");

                        sqlEngine.LogError(SqlID, "0", "ProcessResponse. Modificar en Base Intermedia.", "Exeption1: " + ex.Message);
                    }
                }
                else
                {
                    if (Convert.ToBoolean(oSettings.ActivarDebug))
                        Utils.Utils.DebugLine("<DBResult><Accion>MODIFICAR BASE INTERMEDIA</Accion><Resultado>Error: responseBatch esta vacio.</Resultado></DBResult>", oSettings.PathDebug + "\\" + SqlID + "-P5.DBUPDATE-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + DateTime.Now.Millisecond.ToString("000") + ".xml");
                }
            }
            catch(Exception ex)
            {
                if (Convert.ToBoolean(oSettings.ActivarDebug))
                    Utils.Utils.DebugLine("<DBResult><Accion>MODIFICAR BASE INTERMEDIA</Accion><Resultado>Error: " + ex.Message + "</Resultado></DBResult>", oSettings.PathDebug + "\\" + SqlID + "-P5.DBUPDATE-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + DateTime.Now.Millisecond.ToString("000") + ".xml");

                sqlEngine.LogError(SqlID, "0", "ProcessResponse. Modificar en Base Intermedia.", "Exeption2: " + ex.Message);
            }
        }

        public DataTable GetData(string SQLServerName, string SQLDBName, string SQLTableName, string SQLCondition, string FieldsName)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = DBEngine.SQLEngine.GetItems(SQLServerName, SQLDBName, SQLTableName, FieldsName, SQLCondition, 0);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return dt;
        }

        public DataTable GetData(string SQLServerName, string SQLDBName, string SQLTableName, string SQLCondition, string FieldsName, int iTop)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = DBEngine.SQLEngine.GetItems(SQLServerName, SQLDBName, SQLTableName, FieldsName, SQLCondition, iTop);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return dt;
        }

        public DataTable GetData(string SQLTableName, string SQLCondition, string FieldsName)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = sqlEngine.GetItems(SQLTableName, FieldsName, SQLCondition, 0);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return dt;
        }

        public bool SetData(string SQLServerName, string SQLDBName, string SQLTableName, string KeyName, string KeyValue, string[] FieldName, string[] FieldType, string[] FieldValue )
        {
            bool bResult = false;

            try
            {
                bResult = DBEngine.SQLEngine.UpdateItem(SQLServerName, SQLDBName, SQLTableName, KeyName, SqlDbType.Int.ToString(), KeyValue, FieldName, FieldType, FieldValue);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return bResult;
        }

        private string GetDataRowString(object DataRowColumnValue)
        {
            string strResult = string.Empty;

            try
            {
                if (DataRowColumnValue != null)
                {
                    strResult = DataRowColumnValue.ToString().Trim();
                }
            }
            catch
            {
                strResult = string.Empty;
            }
            return strResult;
        }
    }
}
