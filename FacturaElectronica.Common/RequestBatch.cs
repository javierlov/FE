using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FacturaElectronica.Common
{
    public class RequestBatch
    {
        public string BatchUniqueId = "";
        public string SonServicios = "";
        public string Periodo = "";
        public string CantidadComprobantes = "";
        public string CUITInformante = "";
        public string Total = "0";
        public string TotalComprobanteB = "0";
        public string TotalNoGravado = "0";
        public string TotalGravado = "0";
        public string TotalImpuestoLiquidado = "0";
        public string TotalRNI_Percepcion = "0";
        public string TotalExento = "0";
        public string TotalPercepciones_PagosCuentaImpuestosNacionales = "0";
        public string TotalPercepcionIIBB = "0";
        public string TotalPercepcionImpuestosMunicipales = "0";
        public string TotalImpuestosInternos = "0";
        public string TotalMonedaFacturacion = "0";
        public string TotalMonedaFacturacionComprobanteB = "0";
        public string TotalNoGravadoMonedaFacturacion = "0";
        public string TotalGravadoMonedaFacturacion = "0";
        public string TotalImpuestoLiquidadoMonedaFacturacion = "0";
        public string TotalRNI_PercepcionMonedaFacturacion = "0";
        public string TotalExentoMonedaFacturacion = "0";
        public string TotalPercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion = "0";
        public string TotalPercepcionIIBBMonedaFacturacion = "0";
        public string TotalPercepcionImpuestosMunicipalesMonedaFacturacion = "0";
        public string TotalImpuestosInternosMonedaFacturacion = "0";

        public List<RequestHeader> RequestHeaders = new List<RequestHeader>();

        private string ValidXMLValueNO(XmlDocument xmlDoc, string xmlPath)
        {
            string xmlPadre = "RequestBatch";
            string xmlNodo = xmlPath.Replace("/RequestBatch/", "");

            string valornodo = "";

            XmlNodeList xmllist = xmlDoc.GetElementsByTagName(xmlPadre);
            foreach (XmlNode nodo in xmllist)
            {
                XmlElement elem = (XmlElement) nodo;
                valornodo = elem.GetElementsByTagName(xmlNodo)[0].InnerText;
            }
            
            return valornodo;

        }

        private string ValidXMLValue(XmlDocument xmlDoc, string xmlPath)
        {
            if (xmlDoc.SelectSingleNode(xmlPath).FirstChild != null)
                return xmlDoc.SelectSingleNode(xmlPath).FirstChild.Value;
            else
                return "";
        }

        private string ValidXMLValue(XmlDocument xmlDoc, string xmlPath, string xmlTag, int nodeIndex)
        {
            if (xmlDoc.SelectNodes(xmlPath)[nodeIndex].SelectSingleNode(xmlTag).FirstChild != null)
                return xmlDoc.SelectNodes(xmlPath)[nodeIndex].SelectSingleNode(xmlTag).FirstChild.Value;
            else
                return "";
        }

        public bool LoadXMLString(string xmlString)
        {
            try
            {
                
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlString);

                BatchUniqueId = ValidXMLValue(xmlDoc, "/LoteComprobantes/BatchUniqueId");
                SonServicios = ValidXMLValue(xmlDoc, "/LoteComprobantes/SonServicios");
                Periodo = ValidXMLValue(xmlDoc, "/LoteComprobantes/Periodo");
                CantidadComprobantes = ValidXMLValue(xmlDoc, "/LoteComprobantes/CantidadComprobantes");
                CUITInformante = ValidXMLValue(xmlDoc, "/LoteComprobantes/CUITInformante");
                Total = ValidXMLValue(xmlDoc, "/LoteComprobantes/Total");
                TotalComprobanteB = ValidXMLValue(xmlDoc, "/LoteComprobantes/TotalComprobanteB");
                TotalNoGravado = ValidXMLValue(xmlDoc, "/LoteComprobantes/TotalNoGravado");
                TotalGravado = ValidXMLValue(xmlDoc, "/LoteComprobantes/TotalGravado");
                TotalImpuestoLiquidado = ValidXMLValue(xmlDoc, "/LoteComprobantes/TotalImpuestoLiquidado");
                TotalRNI_Percepcion = ValidXMLValue(xmlDoc, "/LoteComprobantes/TotalRNI_Percepcion");
                TotalExento = ValidXMLValue(xmlDoc, "/LoteComprobantes/TotalExento");
                TotalPercepciones_PagosCuentaImpuestosNacionales = ValidXMLValue(xmlDoc, "/LoteComprobantes/TotalPercepciones_PagosCuentaImpuestosNacionales");
                TotalPercepcionIIBB = ValidXMLValue(xmlDoc, "/LoteComprobantes/TotalPercepcionIIBB");
                TotalPercepcionImpuestosMunicipales = ValidXMLValue(xmlDoc, "/LoteComprobantes/TotalPercepcionImpuestosMunicipales");
                TotalImpuestosInternos = ValidXMLValue(xmlDoc, "/LoteComprobantes/TotalImpuestosInternos");
                TotalMonedaFacturacion = ValidXMLValue(xmlDoc, "/LoteComprobantes/TotalMonedaFacturacion");
                TotalMonedaFacturacionComprobanteB = ValidXMLValue(xmlDoc, "/LoteComprobantes/TotalMonedaFacturacionComprobanteB");
                TotalNoGravadoMonedaFacturacion = ValidXMLValue(xmlDoc, "/LoteComprobantes/TotalNoGravadoMonedaFacturacion");
                TotalGravadoMonedaFacturacion = ValidXMLValue(xmlDoc, "/LoteComprobantes/TotalGravadoMonedaFacturacion");
                TotalImpuestoLiquidadoMonedaFacturacion = ValidXMLValue(xmlDoc, "/LoteComprobantes/TotalImpuestoLiquidadoMonedaFacturacion");
                TotalRNI_PercepcionMonedaFacturacion = ValidXMLValue(xmlDoc, "/LoteComprobantes/TotalRNI_PercepcionMonedaFacturacion");
                TotalExentoMonedaFacturacion = ValidXMLValue(xmlDoc, "/LoteComprobantes/TotalExentoMonedaFacturacion");
                TotalPercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion = ValidXMLValue(xmlDoc, "/LoteComprobantes/TotalPercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion");
                TotalPercepcionIIBBMonedaFacturacion = ValidXMLValue(xmlDoc, "/LoteComprobantes/TotalPercepcionIIBBMonedaFacturacion");
                TotalPercepcionImpuestosMunicipalesMonedaFacturacion = ValidXMLValue(xmlDoc, "/LoteComprobantes/TotalPercepcionImpuestosMunicipalesMonedaFacturacion");
                TotalImpuestosInternosMonedaFacturacion = ValidXMLValue(xmlDoc, "/LoteComprobantes/TotalImpuestosInternosMonedaFacturacion");

                //Agrego todos los comprobantes
                for (int i = 0; i < xmlDoc.SelectNodes("/LoteComprobantes/Comprobante").Count; i++)
                {
                    RequestHeader thisHeader = new RequestHeader();
                    thisHeader.AlicuotaIVA = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "AlicuotaIVA", i);
                    thisHeader.CantidadAlicuotasIVA = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "CantidadAlicuotasIVA", i);
                    thisHeader.CantidadRegistrosDetalle = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "CantidadRegistrosDetalle", i);
                    thisHeader.CompradorCodigoCliente = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "CompradorCodigoCliente", i);
                    thisHeader.CompradorCodigoDocumento = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "CompradorCodigoDocumento", i);
                    thisHeader.CodigoJurisdiccionIIBB = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "CodigoJurisdiccionIIBB", i);
                    thisHeader.LetraComprobante = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "LetraComprobante", i);
                    thisHeader.CodigoMecanismoDistribucion = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "CodigoMecanismoDistribucion", i);
                    thisHeader.CodigoMoneda = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "CodigoMoneda", i);
                    thisHeader.CodigoOperacion = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "CodigoOperacion", i);
                    thisHeader.CompradorCodigoPostal = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "CompradorCodigoPostal", i);
                    thisHeader.CondicionPago = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "CondicionPago", i);
                    thisHeader.CompradorDireccion = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "CompradorDireccion", i);
                    thisHeader.CompradorEmail = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "CompradorEmail", i);
                    thisHeader.FechaComprobante = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "FechaComprobante", i);
                    thisHeader.FechaDesdeServicioFacturado = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "FechaDesdeServicioFacturado", i);
                    thisHeader.FechaHastaServicioFacturado = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "FechaHastaServicioFacturado", i);
                    thisHeader.FechaVencimientoPago = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "FechaVencimientoPago", i);
                    thisHeader.FormaPagoDescripcion = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "FormaPagoDescripcion", i);
                    thisHeader.Idioma = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "Idioma", i);
                    thisHeader.Importe = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "Importe", i);
                    thisHeader.ImporteComprobanteB = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "ImporteComprobanteB", i);
                    thisHeader.ImporteEscrito = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "ImporteEscrito", i);
                    thisHeader.ImporteExento = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "ImporteExento", i);
                    thisHeader.ImporteExentoMonedaFacturacion = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "ImporteExentoMonedaFacturacion", i);
                    thisHeader.ImporteGravado = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "ImporteGravado", i);
                    thisHeader.ImporteGravadoMonedaFacturacion = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "ImporteGravadoMonedaFacturacion", i);
                    thisHeader.ImporteImpuestoLiquidado = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "ImporteImpuestoLiquidado", i);
                    thisHeader.ImporteImpuestoLiquidadoMonedaFacturacion = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "ImporteImpuestoLiquidadoMonedaFacturacion", i);
                    thisHeader.ImporteImpuestosInternos = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "ImporteImpuestosInternos", i);
                    thisHeader.ImporteImpuestosInternosMonedaFacturacion = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "ImporteImpuestosInternosMonedaFacturacion", i);
                    thisHeader.ImporteMonedaFacturacion = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "ImporteMonedaFacturacion", i);
                    thisHeader.ImporteMonedaFacturacionComprobanteB = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "ImporteMonedaFacturacionComprobanteB", i);
                    thisHeader.ImporteNoGravado = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "ImporteNoGravado", i);
                    thisHeader.ImporteNoGravadoMonedaFacturacion = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "ImporteNoGravadoMonedaFacturacion", i);
                    thisHeader.ImportePercepciones_PagosCuentaImpuestosNacionales = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "ImportePercepciones_PagosCuentaImpuestosNacionales", i);
                    thisHeader.ImportePercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "ImportePercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion", i);
                    thisHeader.ImportePercepcionImpuestosMunicipales = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "ImportePercepcionImpuestosMunicipales", i);
                    thisHeader.ImportePercepcionImpuestosMunicipalesMonedaFacturacion = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "ImportePercepcionImpuestosMunicipalesMonedaFacturacion", i);
                    thisHeader.ImportePercepcionIIBB = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "ImportePercepcionIIBB", i);
                    thisHeader.ImportePercepcionIIBBMonedaFacturacion = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "ImportePercepcionIIBBMonedaFacturacion", i);
                    thisHeader.ImporteRNI_Percepcion = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "ImporteRNI_Percepcion", i);
                    thisHeader.ImporteRNI_PercepcionMonedaFacturacion = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "ImporteRNI_PercepcionMonedaFacturacion", i);
                    thisHeader.IncoTerms = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "IncoTerms", i);
                    thisHeader.JurisdiccionImpuestosMunicipales = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "JurisdiccionImpuestosMunicipales", i);
                    thisHeader.CompradorLocalidad = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "CompradorLocalidad", i);
                    thisHeader.NroComprobanteDesde = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "NroComprobanteDesde", i);
                    thisHeader.NroComprobanteHasta = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "NroComprobanteHasta", i);
                    thisHeader.CompradorNroDocumento = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "CompradorNroDocumento", i);
                    thisHeader.CompradorNroIIBB = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "CompradorNroIIBB", i);
                    thisHeader.CompradorNroReferencia = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "CompradorNroReferencia", i);
                    thisHeader.NroRemito = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "NroRemito", i);
                    thisHeader.Observaciones1 = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "Observaciones1", i);
                    thisHeader.Observaciones2 = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "Observaciones2", i);
                    thisHeader.Observaciones3 = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "Observaciones3", i);
                    thisHeader.CompradorPais = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "CompradorPais", i);
                    thisHeader.PermisoExistente = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "PermisoExistente", i);
                    thisHeader.CompradorProvincia = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "CompradorProvincia", i);
                    thisHeader.PuntoVenta = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "PuntoVenta", i);
                    thisHeader.CompradorRazonSocial = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "CompradorRazonSocial", i);
                    thisHeader.TasaCambio = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "TasaCambio", i);
                    thisHeader.TasaIIBB = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "TasaIIBB", i);
                    thisHeader.TipoComprobante = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "TipoComprobante", i);
                    thisHeader.TipoExportacion = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "TipoExportacion", i);
                    thisHeader.CompradorTipoResponsable = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "CompradorTipoResponsable", i);
                    thisHeader.CompradorTipoResponsableDescripcion = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "CompradorTipoResponsableDescripcion", i);
                    thisHeader.TipoTransaccion = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "TipoTransaccion", i);
                    thisHeader.NroInternoERP = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "NroInternoERP", i);
                    thisHeader.EmisorDireccion = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "EmisorDireccion", i);
                    thisHeader.EmisorCalle = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "EmisorCalle", i);
                    thisHeader.EmisorCP = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "EmisorCP", i);
                    thisHeader.EmisorLocalidad = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "EmisorLocalidad", i);
                    thisHeader.EmisorProvincia = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "EmisorProvincia", i);
                    thisHeader.EmisorPais = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "EmisorPais", i);
                    thisHeader.EmisorTelefonos = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "EmisorTelefonos", i);
                    thisHeader.EmisorEMail = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "EmisorEMail", i);
                    thisHeader.OficinaVentas = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "OficinaVentas", i);
                    thisHeader.EmpresaID = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "EmpresaID", i);
                    thisHeader.SQLID = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "SQLID", i);
                    thisHeader.PagoFacil = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "PagoFacil", i);
                    thisHeader.RapiPago = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "RapiPago", i);
                    thisHeader.ObservacionRapiPago = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "ObservacionRapiPago", i);
                    thisHeader.OPER = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "OPER", i);
                    thisHeader.NOPER = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "NOPER", i);
                    thisHeader.DAGRUF = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "DAGRUF", i);
                    thisHeader.FACTORI = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "FACTORI", i);
                    thisHeader.FACTORI_FORMATEADO = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "FACTORI_FORMATEADO", i);
                    thisHeader.USUARIO = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "USUARIO", i);
                    thisHeader.FECPG1_FORMATEADO = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "FECPG1_FORMATEADO", i);
                    thisHeader.FECPG2_FORMATEADO = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "FECPG2_FORMATEADO", i);
                    
                    //thisHeader.CUOTAIVA105 = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "CUOTAIVA105", i);
                    //thisHeader.CUOTAIVA21 = ValidXMLValue(xmlDoc, "/LoteComprobantes/Comprobante", "CUOTAIVA21", i);

                    RequestHeaders.Add(thisHeader);

                    XmlDocument xmlLines = new XmlDocument();
                    xmlLines.LoadXml(xmlDoc.SelectNodes("/LoteComprobantes/Comprobante")[i].OuterXml);

                    //Agrego todas las líneas
                    for (int j = 0; j < xmlLines.SelectNodes("/Comprobante/Linea").Count; j++)
                    {
                        RequestLine thisLine = new RequestLine();

                        thisLine.AlicuotaIVA = ValidXMLValue(xmlLines, "/Comprobante/Linea", "AlicuotaIVA", j);
                        thisLine.Cantidad = ValidXMLValue(xmlLines, "/Comprobante/Linea", "Cantidad", j);
                        thisLine.CodigoProductoEmpresa = ValidXMLValue(xmlLines, "/Comprobante/Linea", "CodigoProductoEmpresa", j);
                        thisLine.CodigoProductoNCM = ValidXMLValue(xmlLines, "/Comprobante/Linea", "CodigoProductoNCM", j);
                        thisLine.CodigoProductoSecretaria = ValidXMLValue(xmlLines, "/Comprobante/Linea", "CodigoProductoSecretaria", j);
                        thisLine.Descripcion = ValidXMLValue(xmlLines, "/Comprobante/Linea", "Descripcion", j);
                        thisLine.ImporteAjuste = ValidXMLValue(xmlLines, "/Comprobante/Linea", "ImporteAjuste", j);
                        thisLine.ImporteAjusteMonedaFacturacion = ValidXMLValue(xmlLines, "/Comprobante/Linea", "ImporteAjusteMonedaFacturacion", j);
                        thisLine.ImporteBonificacion = ValidXMLValue(xmlLines, "/Comprobante/Linea", "ImporteBonificacion", j);
                        thisLine.ImporteBonificacionMonedaFacturacion = ValidXMLValue(xmlLines, "/Comprobante/Linea", "ImporteBonificacionMonedaFacturacion", j);
                        thisLine.ImportePrecioUnitario = ValidXMLValue(xmlLines, "/Comprobante/Linea", "ImportePrecioUnitario", j);
                        thisLine.ImportePrecioUnitarioMonedaFacturacion = ValidXMLValue(xmlLines, "/Comprobante/Linea", "ImportePrecioUnitarioMonedaFacturacion", j);
                        thisLine.ImporteSubtotal = ValidXMLValue(xmlLines, "/Comprobante/Linea", "ImporteSubtotal", j);
                        thisLine.ImporteSubtotalMonedaFacturacion = ValidXMLValue(xmlLines, "/Comprobante/Linea", "ImporteSubtotalMonedaFacturacion", j);
                        thisLine.ImporteSubtotalMonedaFacturacionConIVA = ValidXMLValue(xmlLines, "/Comprobante/Linea", "ImporteSubtotalMonedaFacturacionConIVA", j);
                        thisLine.IndicadorExentoGravadoNoGravado = ValidXMLValue(xmlLines, "/Comprobante/Linea", "IndicadorExentoGravadoNoGravado", j);
                        thisLine.Observaciones = ValidXMLValue(xmlLines, "/Comprobante/Linea", "Observaciones", j);
                        thisLine.MesPrestacion = ""; //ValidXMLValue(xmlLines, "/Comprobante/Linea", "MesPrestacion", j);
                        thisLine.UnidadMedida = ValidXMLValue(xmlLines, "/Comprobante/Linea", "UnidadMedida", j);
                        thisLine.SQLID = ValidXMLValue(xmlLines, "/Comprobante/Linea", "SQLID", j);

                        thisHeader.RequestLines.Add(thisLine);
                    }

                    //Agrego todas las alicuotas
                    for (int j = 0; j < xmlLines.SelectNodes("/Comprobante/Alicuota").Count; j++)
                    {
                        RequestAlicuota thisTAlicuota = new RequestAlicuota();

                        thisTAlicuota.Id = ValidXMLValue(xmlLines, "/Comprobante/Alicuota", "Id", j);
                        thisTAlicuota.Descripcion = ValidXMLValue(xmlLines, "/Comprobante/Alicuota", "Descripcion", j);
                        thisTAlicuota.Importe = ValidXMLValue(xmlLines, "/Comprobante/Alicuota", "Importe", j);
                        thisTAlicuota.BaseImp = ValidXMLValue(xmlLines, "/Comprobante/Alicuota", "BaseImp", j);
                        thisTAlicuota.Tipo = ValidXMLValue(xmlLines, "/Comprobante/Alicuota", "Tipo", j);
                        thisTAlicuota.Codigo = ValidXMLValue(xmlLines, "/Comprobante/Alicuota", "Codigo", j);
                        thisTAlicuota.CbteID = thisHeader.SQLID;

                        thisHeader.RequestAlicuotas.Add(thisTAlicuota);
                    }

                    //Agrego todos los tributos
                    for (int j = 0; j < xmlLines.SelectNodes("/Comprobante/Tributo").Count; j++)
                    {
                        RequestTributo thisTributo = new RequestTributo();

                        thisTributo.Id = ValidXMLValue(xmlLines, "/Comprobante/Tributo", "Id", j);
                        thisTributo.Descripcion = ValidXMLValue(xmlLines, "/Comprobante/Tributo", "Descripcion", j);
                        thisTributo.Importe = ValidXMLValue(xmlLines, "/Comprobante/Tributo", "Importe", j);
                        thisTributo.BaseImp = ValidXMLValue(xmlLines, "/Comprobante/Tributo", "BaseImp", j);
                        thisTributo.Tipo = ValidXMLValue(xmlLines, "/Comprobante/Tributo", "Tipo", j);
                        thisTributo.Codigo = ValidXMLValue(xmlLines, "/Comprobante/Tributo", "Codigo", j);
                        thisTributo.Alic = ValidXMLValue(xmlLines, "/Comprobante/Tributo", "Alic", j);
                        thisTributo.CbteID = thisHeader.SQLID;

                        thisHeader.RequestTributos.Add(thisTributo);
                    }
                }
                 
            }
            catch
            {
                return false;
            }

            return true;
        }


        public bool LoadXMLStringNo(string xmlString)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlString);

                BatchUniqueId = ValidXMLValue(xmlDoc, "/RequestBatch/BatchUniqueId");
                SonServicios = ValidXMLValue(xmlDoc, "/RequestBatch/SonServicios");
                Periodo = ValidXMLValue(xmlDoc, "/RequestBatch/Periodo");
                CantidadComprobantes = ValidXMLValue(xmlDoc, "/RequestBatch/CantidadComprobantes");
                CUITInformante = ValidXMLValue(xmlDoc, "/RequestBatch/CUITInformante");
                Total = ValidXMLValue(xmlDoc, "/RequestBatch/Total");
                TotalComprobanteB = ValidXMLValue(xmlDoc, "/RequestBatch/TotalComprobanteB");
                TotalNoGravado = ValidXMLValue(xmlDoc, "/RequestBatch/TotalNoGravado");
                TotalGravado = ValidXMLValue(xmlDoc, "/RequestBatch/TotalGravado");
                TotalImpuestoLiquidado = ValidXMLValue(xmlDoc, "/RequestBatch/TotalImpuestoLiquidado");
                TotalRNI_Percepcion = ValidXMLValue(xmlDoc, "/RequestBatch/TotalRNI_Percepcion");
                TotalExento = ValidXMLValue(xmlDoc, "/RequestBatch/TotalExento");
                TotalPercepciones_PagosCuentaImpuestosNacionales = ValidXMLValue(xmlDoc, "/RequestBatch/TotalPercepciones_PagosCuentaImpuestosNacionales");
                TotalPercepcionIIBB = ValidXMLValue(xmlDoc, "/RequestBatch/TotalPercepcionIIBB");
                TotalPercepcionImpuestosMunicipales = ValidXMLValue(xmlDoc, "/RequestBatch/TotalPercepcionImpuestosMunicipales");
                TotalImpuestosInternos = ValidXMLValue(xmlDoc, "/RequestBatch/TotalImpuestosInternos");
                TotalMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/TotalMonedaFacturacion");
                TotalMonedaFacturacionComprobanteB = ValidXMLValue(xmlDoc, "/RequestBatch/TotalMonedaFacturacionComprobanteB");
                TotalNoGravadoMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/TotalNoGravadoMonedaFacturacion");
                TotalGravadoMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/TotalGravadoMonedaFacturacion");
                TotalImpuestoLiquidadoMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/TotalImpuestoLiquidadoMonedaFacturacion");
                TotalRNI_PercepcionMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/TotalRNI_PercepcionMonedaFacturacion");
                TotalExentoMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/TotalExentoMonedaFacturacion");
                TotalPercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/TotalPercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion");
                TotalPercepcionIIBBMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/TotalPercepcionIIBBMonedaFacturacion");
                TotalPercepcionImpuestosMunicipalesMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/TotalPercepcionImpuestosMunicipalesMonedaFacturacion");
                TotalImpuestosInternosMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/TotalImpuestosInternosMonedaFacturacion");

                //Agrego todos los comprobantes
                for (int i = 0; i < xmlDoc.SelectNodes("/RequestBatch/Comprobante").Count; i++)
                {
                    RequestHeader thisHeader = new RequestHeader();
                    thisHeader.AlicuotaIVA = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "AlicuotaIVA", i);
                    thisHeader.CantidadAlicuotasIVA = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "CantidadAlicuotasIVA", i);
                    thisHeader.CantidadRegistrosDetalle = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "CantidadRegistrosDetalle", i);
                    thisHeader.CompradorCodigoCliente = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "CompradorCodigoCliente", i);
                    thisHeader.CompradorCodigoDocumento = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "CompradorCodigoDocumento", i);
                    thisHeader.CodigoJurisdiccionIIBB = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "CodigoJurisdiccionIIBB", i);
                    thisHeader.LetraComprobante = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "LetraComprobante", i);
                    thisHeader.CodigoMecanismoDistribucion = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "CodigoMecanismoDistribucion", i);
                    thisHeader.CodigoMoneda = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "CodigoMoneda", i);
                    thisHeader.CodigoOperacion = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "CodigoOperacion", i);
                    thisHeader.CompradorCodigoPostal = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "CompradorCodigoPostal", i);
                    thisHeader.CondicionPago = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "CondicionPago", i);
                    thisHeader.CompradorDireccion = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "CompradorDireccion", i);
                    thisHeader.CompradorEmail = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "CompradorEmail", i);
                    thisHeader.FechaComprobante = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "FechaComprobante", i);
                    thisHeader.FechaDesdeServicioFacturado = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "FechaDesdeServicioFacturado", i);
                    thisHeader.FechaHastaServicioFacturado = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "FechaHastaServicioFacturado", i);
                    thisHeader.FechaVencimientoPago = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "FechaVencimientoPago", i);
                    thisHeader.FormaPagoDescripcion = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "FormaPagoDescripcion", i);
                    thisHeader.Idioma = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "Idioma", i);
                    thisHeader.Importe = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "Importe", i);
                    thisHeader.ImporteComprobanteB = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "ImporteComprobanteB", i);
                    thisHeader.ImporteEscrito = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "ImporteEscrito", i);
                    thisHeader.ImporteExento = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "ImporteExento", i);
                    thisHeader.ImporteExentoMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "ImporteExentoMonedaFacturacion", i);
                    thisHeader.ImporteGravado = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "ImporteGravado", i);
                    thisHeader.ImporteGravadoMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "ImporteGravadoMonedaFacturacion", i);
                    thisHeader.ImporteImpuestoLiquidado = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "ImporteImpuestoLiquidado", i);
                    thisHeader.ImporteImpuestoLiquidadoMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "ImporteImpuestoLiquidadoMonedaFacturacion", i);
                    thisHeader.ImporteImpuestosInternos = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "ImporteImpuestosInternos", i);
                    thisHeader.ImporteImpuestosInternosMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "ImporteImpuestosInternosMonedaFacturacion", i);
                    thisHeader.ImporteMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "ImporteMonedaFacturacion", i);
                    thisHeader.ImporteMonedaFacturacionComprobanteB = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "ImporteMonedaFacturacionComprobanteB", i);
                    thisHeader.ImporteNoGravado = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "ImporteNoGravado", i);
                    thisHeader.ImporteNoGravadoMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "ImporteNoGravadoMonedaFacturacion", i);
                    thisHeader.ImportePercepciones_PagosCuentaImpuestosNacionales = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "ImportePercepciones_PagosCuentaImpuestosNacionales", i);
                    thisHeader.ImportePercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "ImportePercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion", i);
                    thisHeader.ImportePercepcionImpuestosMunicipales = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "ImportePercepcionImpuestosMunicipales", i);
                    thisHeader.ImportePercepcionImpuestosMunicipalesMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "ImportePercepcionImpuestosMunicipalesMonedaFacturacion", i);
                    thisHeader.ImportePercepcionIIBB = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "ImportePercepcionIIBB", i);
                    thisHeader.ImportePercepcionIIBBMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "ImportePercepcionIIBBMonedaFacturacion", i);
                    thisHeader.ImporteRNI_Percepcion = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "ImporteRNI_Percepcion", i);
                    thisHeader.ImporteRNI_PercepcionMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "ImporteRNI_PercepcionMonedaFacturacion", i);
                    thisHeader.IncoTerms = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "IncoTerms", i);
                    thisHeader.JurisdiccionImpuestosMunicipales = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "JurisdiccionImpuestosMunicipales", i);
                    thisHeader.CompradorLocalidad = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "CompradorLocalidad", i);
                    thisHeader.NroComprobanteDesde = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "NroComprobanteDesde", i);
                    thisHeader.NroComprobanteHasta = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "NroComprobanteHasta", i);
                    thisHeader.CompradorNroDocumento = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "CompradorNroDocumento", i);
                    thisHeader.CompradorNroIIBB = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "CompradorNroIIBB", i);
                    thisHeader.CompradorNroReferencia = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "CompradorNroReferencia", i);
                    thisHeader.NroRemito = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "NroRemito", i);
                    thisHeader.Observaciones1 = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "Observaciones1", i);
                    thisHeader.Observaciones2 = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "Observaciones2", i);
                    thisHeader.Observaciones3 = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "Observaciones3", i);
                    thisHeader.CompradorPais = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "CompradorPais", i);
                    thisHeader.PermisoExistente = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "PermisoExistente", i);
                    thisHeader.CompradorProvincia = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "CompradorProvincia", i);
                    thisHeader.PuntoVenta = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "PuntoVenta", i);
                    thisHeader.CompradorRazonSocial = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "CompradorRazonSocial", i);
                    thisHeader.TasaCambio = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "TasaCambio", i);
                    thisHeader.TasaIIBB = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "TasaIIBB", i);
                    thisHeader.TipoComprobante = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "TipoComprobante", i);
                    thisHeader.TipoExportacion = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "TipoExportacion", i);
                    thisHeader.CompradorTipoResponsable = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "CompradorTipoResponsable", i);
                    thisHeader.CompradorTipoResponsableDescripcion = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "CompradorTipoResponsableDescripcion", i);
                    thisHeader.TipoTransaccion = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "TipoTransaccion", i);
                    thisHeader.NroInternoERP = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "NroInternoERP", i);
                    thisHeader.EmisorDireccion = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "EmisorDireccion", i);
                    thisHeader.EmisorCalle = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "EmisorCalle", i);
                    thisHeader.EmisorCP = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "EmisorCP", i);
                    thisHeader.EmisorLocalidad = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "EmisorLocalidad", i);
                    thisHeader.EmisorProvincia = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "EmisorProvincia", i);
                    thisHeader.EmisorPais = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "EmisorPais", i);
                    thisHeader.EmisorTelefonos = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "EmisorTelefonos", i);
                    thisHeader.EmisorEMail = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "EmisorEMail", i);
                    thisHeader.OficinaVentas = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "OficinaVentas", i);
                    thisHeader.EmpresaID = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "EmpresaID", i);
                    thisHeader.SQLID = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "SQLID", i);
                    thisHeader.PagoFacil = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "PagoFacil", i);
                    thisHeader.RapiPago = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "RapiPago", i);
                    thisHeader.ObservacionRapiPago = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "ObservacionRapiPago", i);
                    thisHeader.OPER = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "OPER", i);
                    thisHeader.NOPER = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "NOPER", i);
                    thisHeader.DAGRUF = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "DAGRUF", i);
                    thisHeader.FACTORI = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "FACTORI", i);
                    thisHeader.FACTORI_FORMATEADO = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "FACTORI_FORMATEADO", i);
                    thisHeader.USUARIO = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "USUARIO", i);
                    thisHeader.FECPG1_FORMATEADO = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "FECPG1_FORMATEADO", i);
                    thisHeader.FECPG2_FORMATEADO = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "FECPG2_FORMATEADO", i);
                    //thisHeader.CUOTAIVA105 = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "CUOTAIVA105", i);
                    //thisHeader.CUOTAIVA21 = ValidXMLValue(xmlDoc, "/RequestBatch/Comprobante", "CUOTAIVA21", i);

                    RequestHeaders.Add(thisHeader);

                    XmlDocument xmlLines = new XmlDocument();
                    xmlLines.LoadXml(xmlDoc.SelectNodes("/LoteComprobantes/Comprobante")[i].OuterXml);

                    //Agrego todas las líneas
                    for (int j = 0; j < xmlLines.SelectNodes("/Comprobante/Linea").Count; j++)
                    {
                        RequestLine thisLine = new RequestLine();

                        thisLine.AlicuotaIVA = ValidXMLValue(xmlLines, "/Comprobante/Linea", "AlicuotaIVA", j);
                        thisLine.Cantidad = ValidXMLValue(xmlLines, "/Comprobante/Linea", "Cantidad", j);
                        thisLine.CodigoProductoEmpresa = ValidXMLValue(xmlLines, "/Comprobante/Linea", "CodigoProductoEmpresa", j);
                        thisLine.CodigoProductoNCM = ValidXMLValue(xmlLines, "/Comprobante/Linea", "CodigoProductoNCM", j);
                        thisLine.CodigoProductoSecretaria = ValidXMLValue(xmlLines, "/Comprobante/Linea", "CodigoProductoSecretaria", j);
                        thisLine.Descripcion = ValidXMLValue(xmlLines, "/Comprobante/Linea", "Descripcion", j);
                        thisLine.ImporteAjuste = ValidXMLValue(xmlLines, "/Comprobante/Linea", "ImporteAjuste", j);
                        thisLine.ImporteAjusteMonedaFacturacion = ValidXMLValue(xmlLines, "/Comprobante/Linea", "ImporteAjusteMonedaFacturacion", j);
                        thisLine.ImporteBonificacion = ValidXMLValue(xmlLines, "/Comprobante/Linea", "ImporteBonificacion", j);
                        thisLine.ImporteBonificacionMonedaFacturacion = ValidXMLValue(xmlLines, "/Comprobante/Linea", "ImporteBonificacionMonedaFacturacion", j);
                        thisLine.ImportePrecioUnitario = ValidXMLValue(xmlLines, "/Comprobante/Linea", "ImportePrecioUnitario", j);
                        thisLine.ImportePrecioUnitarioMonedaFacturacion = ValidXMLValue(xmlLines, "/Comprobante/Linea", "ImportePrecioUnitarioMonedaFacturacion", j);
                        thisLine.ImporteSubtotal = ValidXMLValue(xmlLines, "/Comprobante/Linea", "ImporteSubtotal", j);
                        thisLine.ImporteSubtotalMonedaFacturacion = ValidXMLValue(xmlLines, "/Comprobante/Linea", "ImporteSubtotalMonedaFacturacion", j);
                        thisLine.ImporteSubtotalMonedaFacturacionConIVA = ValidXMLValue(xmlLines, "/Comprobante/Linea", "ImporteSubtotalMonedaFacturacionConIVA", j);
                        thisLine.IndicadorExentoGravadoNoGravado = ValidXMLValue(xmlLines, "/Comprobante/Linea", "IndicadorExentoGravadoNoGravado", j);
                        thisLine.Observaciones = ValidXMLValue(xmlLines, "/Comprobante/Linea", "Observaciones", j);
                        thisLine.MesPrestacion = ""; //ValidXMLValue(xmlLines, "/Comprobante/Linea", "MesPrestacion", j);
                        thisLine.UnidadMedida = ValidXMLValue(xmlLines, "/Comprobante/Linea", "UnidadMedida", j);
                        thisLine.SQLID = ValidXMLValue(xmlLines, "/Comprobante/Linea", "SQLID", j);

                        thisHeader.RequestLines.Add(thisLine);
                    }

                    //Agrego todas las alicuotas
                    for (int j = 0; j < xmlLines.SelectNodes("/Comprobante/Alicuota").Count; j++)
                    {
                        RequestAlicuota thisTAlicuota = new RequestAlicuota();

                        thisTAlicuota.Id = ValidXMLValue(xmlLines, "/Comprobante/Alicuota", "Id", j);
                        thisTAlicuota.Descripcion = ValidXMLValue(xmlLines, "/Comprobante/Alicuota", "Descripcion", j);
                        thisTAlicuota.Importe = ValidXMLValue(xmlLines, "/Comprobante/Alicuota", "Importe", j);
                        thisTAlicuota.BaseImp = ValidXMLValue(xmlLines, "/Comprobante/Alicuota", "BaseImp", j);
                        thisTAlicuota.Tipo = ValidXMLValue(xmlLines, "/Comprobante/Alicuota", "Tipo", j);
                        thisTAlicuota.Codigo = ValidXMLValue(xmlLines, "/Comprobante/Alicuota", "Codigo", j);
                        thisTAlicuota.CbteID = thisHeader.SQLID;

                        thisHeader.RequestAlicuotas.Add(thisTAlicuota);
                    }

                    //Agrego todos los tributos
                    for (int j = 0; j < xmlLines.SelectNodes("/Comprobante/Tributo").Count; j++)
                    {
                        RequestTributo thisTributo = new RequestTributo();

                        thisTributo.Id = ValidXMLValue(xmlLines, "/Comprobante/Tributo", "Id", j);
                        thisTributo.Descripcion = ValidXMLValue(xmlLines, "/Comprobante/Tributo", "Descripcion", j);
                        thisTributo.Importe = ValidXMLValue(xmlLines, "/Comprobante/Tributo", "Importe", j);
                        thisTributo.BaseImp = ValidXMLValue(xmlLines, "/Comprobante/Tributo", "BaseImp", j);
                        thisTributo.Tipo = ValidXMLValue(xmlLines, "/Comprobante/Tributo", "Tipo", j);
                        thisTributo.Codigo = ValidXMLValue(xmlLines, "/Comprobante/Tributo", "Codigo", j);
                        thisTributo.Alic = ValidXMLValue(xmlLines, "/Comprobante/Tributo", "Alic", j);
 
                        thisTributo.CbteID = thisHeader.SQLID;

                        thisHeader.RequestTributos.Add(thisTributo);
                    }
                }
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
                return false;
            }

            return true;
        }


        public string GetXMLString()
        {
            XmlDocument xmlDoc = new XmlDocument();
            //Crear la parte del lote
            try
            {
                XmlElement xmlElem = xmlDoc.CreateElement("LoteComprobantes");
                XmlNode rootNode = xmlDoc.AppendChild(xmlElem);

                xmlElem = xmlDoc.CreateElement("BatchUniqueId");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(BatchUniqueId));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("SonServicios");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(SonServicios));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("Periodo");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(Periodo));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("CantidadComprobantes");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(CantidadComprobantes));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("CUITInformante");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(CUITInformante));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("Total");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(Total));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("TotalComprobanteB");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(TotalComprobanteB));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("TotalNoGravado");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(TotalNoGravado));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("TotalGravado");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(TotalGravado));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("TotalImpuestoLiquidado");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(TotalImpuestoLiquidado));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("TotalRNI_Percepcion");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(TotalRNI_Percepcion));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("TotalExento");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(TotalExento));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("TotalPercepciones_PagosCuentaImpuestosNacionales");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(TotalPercepciones_PagosCuentaImpuestosNacionales));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("TotalPercepcionIIBB");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(TotalPercepcionIIBB));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("TotalPercepcionImpuestosMunicipales");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(TotalPercepcionImpuestosMunicipales));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("TotalImpuestosInternos");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(TotalImpuestosInternos));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("TotalMonedaFacturacion");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(TotalMonedaFacturacion));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("TotalMonedaFacturacionComprobanteB");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(TotalMonedaFacturacionComprobanteB));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("TotalNoGravadoMonedaFacturacion");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(TotalNoGravadoMonedaFacturacion));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("TotalGravadoMonedaFacturacion");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(TotalGravadoMonedaFacturacion));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("TotalImpuestoLiquidadoMonedaFacturacion");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(TotalImpuestoLiquidadoMonedaFacturacion));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("TotalRNI_PercepcionMonedaFacturacion");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(TotalRNI_PercepcionMonedaFacturacion));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("TotalExentoMonedaFacturacion");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(TotalExentoMonedaFacturacion));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("TotalPercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(TotalPercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("TotalPercepcionIIBBMonedaFacturacion");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(TotalPercepcionIIBBMonedaFacturacion));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("TotalPercepcionImpuestosMunicipalesMonedaFacturacion");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(TotalPercepcionImpuestosMunicipalesMonedaFacturacion));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("TotalImpuestosInternosMonedaFacturacion");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(TotalImpuestosInternosMonedaFacturacion));
                rootNode.AppendChild(xmlElem);

                //Crear cada documento
                int nroComprobante = 0;
                foreach (RequestHeader thisHeader in this.RequestHeaders)
                {
                    XmlNode comprobanteNode = null;
                    XmlAttribute comprobanteAttr = null;

                    nroComprobante++;
                    xmlElem = xmlDoc.CreateElement("Comprobante");
                    comprobanteAttr = xmlDoc.CreateAttribute("NroComprobante");
                    comprobanteAttr.Value = nroComprobante.ToString();
                    xmlElem.Attributes.Append(comprobanteAttr);
                    comprobanteNode = rootNode.AppendChild(xmlElem);

                    xmlElem = xmlDoc.CreateElement("TipoTransaccion");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.TipoTransaccion));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("FechaComprobante");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.FechaComprobante));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("FechaDesdeServicioFacturado");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.FechaDesdeServicioFacturado));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("FechaHastaServicioFacturado");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.FechaHastaServicioFacturado));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("TipoComprobante");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.TipoComprobante));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("PuntoVenta");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.PuntoVenta));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("LetraComprobante");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.LetraComprobante));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("NroComprobanteDesde");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.NroComprobanteDesde));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("NroComprobanteHasta");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.NroComprobanteHasta));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("FechaVencimientoPago");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.FechaVencimientoPago));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("CondicionPago");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CondicionPago));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("CompradorCodigoDocumento");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CompradorCodigoDocumento));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("CompradorNroDocumento");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CompradorNroDocumento));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("CompradorTipoResponsable");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CompradorTipoResponsable));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("CompradorTipoResponsableDescripcion");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CompradorTipoResponsableDescripcion));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("CompradorRazonSocial");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CompradorRazonSocial));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("CompradorDireccion");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CompradorDireccion));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("CompradorLocalidad");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CompradorLocalidad));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("CompradorProvincia");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CompradorProvincia));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("CompradorPais");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CompradorPais));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("CompradorCodigoPostal");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CompradorCodigoPostal));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("CompradorNroIIBB");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CompradorNroIIBB));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("CompradorCodigoCliente");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CompradorCodigoCliente));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("CompradorNroReferencia");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CompradorNroReferencia));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("CompradorEmail");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CompradorEmail));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("NroRemito");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.NroRemito));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("Importe");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.Importe));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ImporteComprobanteB");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ImporteComprobanteB));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ImporteNoGravado");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ImporteNoGravado));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ImporteGravado");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ImporteGravado));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("AlicuotaIVA");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.AlicuotaIVA));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ImporteImpuestoLiquidado");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ImporteImpuestoLiquidado));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ImporteRNI_Percepcion");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ImporteRNI_Percepcion));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ImporteExento");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ImporteExento));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ImportePercepciones_PagosCuentaImpuestosNacionales");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ImportePercepciones_PagosCuentaImpuestosNacionales));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ImportePercepcionIIBB");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ImportePercepcionIIBB));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("TasaIIBB");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.TasaIIBB));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("CodigoJurisdiccionIIBB");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CodigoJurisdiccionIIBB));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ImportePercepcionImpuestosMunicipales");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ImportePercepcionImpuestosMunicipales));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("JurisdiccionImpuestosMunicipales");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.JurisdiccionImpuestosMunicipales));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ImporteImpuestosInternos");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ImporteImpuestosInternos));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ImporteMonedaFacturacion");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ImporteMonedaFacturacion));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ImporteMonedaFacturacionComprobanteB");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ImporteMonedaFacturacionComprobanteB));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ImporteNoGravadoMonedaFacturacion");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ImporteNoGravadoMonedaFacturacion));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ImporteGravadoMonedaFacturacion");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ImporteGravadoMonedaFacturacion));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ImporteImpuestoLiquidadoMonedaFacturacion");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ImporteImpuestoLiquidadoMonedaFacturacion));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ImporteRNI_PercepcionMonedaFacturacion");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ImporteRNI_PercepcionMonedaFacturacion));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ImporteExentoMonedaFacturacion");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ImporteExentoMonedaFacturacion));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ImportePercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ImportePercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ImportePercepcionIIBBMonedaFacturacion");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ImportePercepcionIIBBMonedaFacturacion));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ImportePercepcionImpuestosMunicipalesMonedaFacturacion");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ImportePercepcionImpuestosMunicipalesMonedaFacturacion));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ImporteImpuestosInternosMonedaFacturacion");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ImporteImpuestosInternosMonedaFacturacion));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("CantidadAlicuotasIVA");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CantidadAlicuotasIVA));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("CodigoOperacion");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CodigoOperacion));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("TasaCambio");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.TasaCambio));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("CodigoMoneda");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CodigoMoneda));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ImporteEscrito");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ImporteEscrito));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("CantidadRegistrosDetalle");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CantidadRegistrosDetalle));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("CodigoMecanismoDistribucion");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CodigoMecanismoDistribucion));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("TipoExportacion");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.TipoExportacion));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("PermisoExistente");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.PermisoExistente));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("CompradorPais");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CompradorPais));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("FormaPagoDescripcion");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.FormaPagoDescripcion));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("IncoTerms");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.IncoTerms));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("Idioma");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.Idioma));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("Observaciones1");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.Observaciones1));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("Observaciones2");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.Observaciones2));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("Observaciones3");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.Observaciones3));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("NroInternoERP");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.NroInternoERP));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("EmisorRazonSocial");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.EmisorRazonSocial));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("EmisorDireccion");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.EmisorDireccion));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("EmisorCalle");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.EmisorCalle));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("EmisorCP");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.EmisorCP));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("EmisorLocalidad");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.EmisorLocalidad));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("EmisorProvincia");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.EmisorProvincia));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("EmisorPais");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.EmisorPais));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("EmisorTelefonos");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.EmisorTelefonos));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("EmisorEMail");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.EmisorEMail));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("OficinaVentas");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.OficinaVentas));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("EmpresaID");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.EmpresaID));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("PagoFacil");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.PagoFacil));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("RapiPago");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.RapiPago));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ObservacionRapiPago");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ObservacionRapiPago));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("DAGRUF");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.DAGRUF));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("OPER");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.OPER));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("NOPER");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.NOPER));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("USUARIO");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.USUARIO));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("FACTORI");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.FACTORI));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("FACTORI_FORMATEADO");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.FACTORI_FORMATEADO));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("FECPG1_FORMATEADO");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.FECPG1_FORMATEADO));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("FECPG2_FORMATEADO");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.FECPG2_FORMATEADO));
                    comprobanteNode.AppendChild(xmlElem);
                    //xmlElem = xmlDoc.CreateElement("CUOTAIVA105");
                    //xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CUOTAIVA105));
                    //comprobanteNode.AppendChild(xmlElem);
                    //xmlElem = xmlDoc.CreateElement("CUOTAIVA21");
                    //xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CUOTAIVA21));
                    //comprobanteNode.AppendChild(xmlElem);

                    xmlElem = xmlDoc.CreateElement("SQLID");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.SQLID));
                    comprobanteNode.AppendChild(xmlElem);

                    //Agregar las líneas
                    int nroLinea = 0;

                    foreach (RequestLine thisLine in thisHeader.RequestLines)
                    {
                        XmlNode lineaNode = null;
                        XmlAttribute lineaAttr = null;

                        nroLinea++;
                        xmlElem = xmlDoc.CreateElement("Linea");
                        lineaAttr = xmlDoc.CreateAttribute("NroLinea");
                        lineaAttr.Value = nroLinea.ToString();
                        xmlElem.Attributes.Append(lineaAttr);
                        lineaNode = comprobanteNode.AppendChild(xmlElem);

                        xmlElem = xmlDoc.CreateElement("CodigoProductoEmpresa");
                        xmlElem.AppendChild(xmlDoc.CreateTextNode(thisLine.CodigoProductoEmpresa));
                        lineaNode.AppendChild(xmlElem);
                        xmlElem = xmlDoc.CreateElement("CodigoProductoNCM");
                        xmlElem.AppendChild(xmlDoc.CreateTextNode(thisLine.CodigoProductoNCM));
                        lineaNode.AppendChild(xmlElem);
                        xmlElem = xmlDoc.CreateElement("CodigoProductoSecretaria");
                        xmlElem.AppendChild(xmlDoc.CreateTextNode(thisLine.CodigoProductoSecretaria));
                        lineaNode.AppendChild(xmlElem);
                        xmlElem = xmlDoc.CreateElement("Descripcion");
                        xmlElem.AppendChild(xmlDoc.CreateTextNode(thisLine.Descripcion));
                        lineaNode.AppendChild(xmlElem);
                        xmlElem = xmlDoc.CreateElement("Cantidad");
                        xmlElem.AppendChild(xmlDoc.CreateTextNode(thisLine.Cantidad));
                        lineaNode.AppendChild(xmlElem);
                        xmlElem = xmlDoc.CreateElement("UnidadMedida");
                        xmlElem.AppendChild(xmlDoc.CreateTextNode(thisLine.UnidadMedida));
                        lineaNode.AppendChild(xmlElem);
                        xmlElem = xmlDoc.CreateElement("ImportePrecioUnitario");
                        xmlElem.AppendChild(xmlDoc.CreateTextNode(thisLine.ImportePrecioUnitario));
                        lineaNode.AppendChild(xmlElem);
                        xmlElem = xmlDoc.CreateElement("ImporteBonificacion");
                        xmlElem.AppendChild(xmlDoc.CreateTextNode(thisLine.ImporteBonificacion));
                        lineaNode.AppendChild(xmlElem);
                        xmlElem = xmlDoc.CreateElement("ImporteAjuste");
                        xmlElem.AppendChild(xmlDoc.CreateTextNode(thisLine.ImporteAjuste));
                        lineaNode.AppendChild(xmlElem);
                        xmlElem = xmlDoc.CreateElement("ImporteSubtotal");
                        xmlElem.AppendChild(xmlDoc.CreateTextNode(thisLine.ImporteSubtotal));
                        lineaNode.AppendChild(xmlElem);
                        xmlElem = xmlDoc.CreateElement("ImportePrecioUnitarioMonedaFacturacion");
                        xmlElem.AppendChild(xmlDoc.CreateTextNode(thisLine.ImportePrecioUnitarioMonedaFacturacion));
                        lineaNode.AppendChild(xmlElem);
                        xmlElem = xmlDoc.CreateElement("ImporteBonificacionMonedaFacturacion");
                        xmlElem.AppendChild(xmlDoc.CreateTextNode(thisLine.ImporteBonificacionMonedaFacturacion));
                        lineaNode.AppendChild(xmlElem);
                        xmlElem = xmlDoc.CreateElement("ImporteAjusteMonedaFacturacion");
                        xmlElem.AppendChild(xmlDoc.CreateTextNode(thisLine.ImporteAjusteMonedaFacturacion));
                        lineaNode.AppendChild(xmlElem);
                        xmlElem = xmlDoc.CreateElement("ImporteSubtotalMonedaFacturacion");
                        xmlElem.AppendChild(xmlDoc.CreateTextNode(thisLine.ImporteSubtotalMonedaFacturacion));
                        lineaNode.AppendChild(xmlElem);
                        xmlElem = xmlDoc.CreateElement("ImporteSubtotalMonedaFacturacionConIVA");
                        xmlElem.AppendChild(xmlDoc.CreateTextNode(thisLine.ImporteSubtotalMonedaFacturacionConIVA));
                        lineaNode.AppendChild(xmlElem);
                        xmlElem = xmlDoc.CreateElement("AlicuotaIVA");
                        xmlElem.AppendChild(xmlDoc.CreateTextNode(thisLine.AlicuotaIVA));
                        lineaNode.AppendChild(xmlElem);
                        xmlElem = xmlDoc.CreateElement("IndicadorExentoGravadoNoGravado");
                        xmlElem.AppendChild(xmlDoc.CreateTextNode(thisLine.IndicadorExentoGravadoNoGravado));
                        lineaNode.AppendChild(xmlElem);
                        xmlElem = xmlDoc.CreateElement("Observaciones");
                        xmlElem.AppendChild(xmlDoc.CreateTextNode(thisLine.Observaciones));
                        lineaNode.AppendChild(xmlElem);
                        xmlElem = xmlDoc.CreateElement("MesPrestacion");
                        xmlElem.AppendChild(xmlDoc.CreateTextNode(thisLine.MesPrestacion));
                        lineaNode.AppendChild(xmlElem);
                        xmlElem = xmlDoc.CreateElement("SQLID");
                        xmlElem.AppendChild(xmlDoc.CreateTextNode(thisLine.SQLID));
                        lineaNode.AppendChild(xmlElem);
                    }

                    //TODO: nuevo sprayette
                    foreach (RequestAlicuota thisAlicuota in thisHeader.RequestAlicuotas)
                    {
                        if (thisAlicuota != null)
                        {
                            XmlNode alicuotaNode = null;

                            xmlElem = xmlDoc.CreateElement("Alicuota");
                            alicuotaNode = comprobanteNode.AppendChild(xmlElem);

                            xmlElem = xmlDoc.CreateElement("CbteId");
                            xmlElem.AppendChild(xmlDoc.CreateTextNode(thisAlicuota.CbteID));
                            alicuotaNode.AppendChild(xmlElem);

                            xmlElem = xmlDoc.CreateElement("Id");
                            xmlElem.AppendChild(xmlDoc.CreateTextNode(thisAlicuota.Id));
                            alicuotaNode.AppendChild(xmlElem);

                            xmlElem = xmlDoc.CreateElement("Descripcion");
                            xmlElem.AppendChild(xmlDoc.CreateTextNode(thisAlicuota.Descripcion));
                            alicuotaNode.AppendChild(xmlElem);

                            xmlElem = xmlDoc.CreateElement("Importe");
                            xmlElem.AppendChild(xmlDoc.CreateTextNode(thisAlicuota.Importe));
                            alicuotaNode.AppendChild(xmlElem);

                            xmlElem = xmlDoc.CreateElement("BaseImp");
                            xmlElem.AppendChild(xmlDoc.CreateTextNode(thisAlicuota.BaseImp));
                            alicuotaNode.AppendChild(xmlElem);

                            xmlElem = xmlDoc.CreateElement("Tipo");
                            xmlElem.AppendChild(xmlDoc.CreateTextNode(thisAlicuota.Tipo));
                            alicuotaNode.AppendChild(xmlElem);

                            xmlElem = xmlDoc.CreateElement("Codigo");
                            xmlElem.AppendChild(xmlDoc.CreateTextNode(thisAlicuota.Codigo));
                            alicuotaNode.AppendChild(xmlElem);
                        }
                    }

                    //TODO: nuevo sprayette
                    foreach (RequestTributo thisTributo in thisHeader.RequestTributos)
                    {
                        if (thisTributo != null)
                        {
                            XmlNode tributeNode = null;

                            xmlElem = xmlDoc.CreateElement("Tributo");
                            tributeNode = comprobanteNode.AppendChild(xmlElem);

                            xmlElem = xmlDoc.CreateElement("CbteId");
                            xmlElem.AppendChild(xmlDoc.CreateTextNode(thisTributo.CbteID));
                            tributeNode.AppendChild(xmlElem);

                            xmlElem = xmlDoc.CreateElement("Id");
                            xmlElem.AppendChild(xmlDoc.CreateTextNode(thisTributo.Id));
                            tributeNode.AppendChild(xmlElem);

                            xmlElem = xmlDoc.CreateElement("Descripcion");
                            xmlElem.AppendChild(xmlDoc.CreateTextNode(thisTributo.Descripcion));
                            tributeNode.AppendChild(xmlElem);

                            xmlElem = xmlDoc.CreateElement("Importe");
                            xmlElem.AppendChild(xmlDoc.CreateTextNode(thisTributo.Importe));
                            tributeNode.AppendChild(xmlElem);

                            xmlElem = xmlDoc.CreateElement("BaseImp");
                            xmlElem.AppendChild(xmlDoc.CreateTextNode(thisTributo.BaseImp));
                            tributeNode.AppendChild(xmlElem);

                            xmlElem = xmlDoc.CreateElement("Codigo");
                            xmlElem.AppendChild(xmlDoc.CreateTextNode(thisTributo.Codigo));
                            tributeNode.AppendChild(xmlElem);

                            xmlElem = xmlDoc.CreateElement("Tipo");
                            xmlElem.AppendChild(xmlDoc.CreateTextNode(thisTributo.Tipo));
                            tributeNode.AppendChild(xmlElem);

                            xmlElem = xmlDoc.CreateElement("Alic");
                            xmlElem.AppendChild(xmlDoc.CreateTextNode(thisTributo.Alic));
                            tributeNode.AppendChild(xmlElem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Gestelec", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }

            return xmlDoc.InnerXml;
        }
    }
}
