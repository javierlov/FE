using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FacturaElectronica.Common
{
    public class ResponseBatch
    {
        public string BatchUniqueId = "";
        public string CUITInformante = "";
        public string FechaCAE = "";
        public string CantidadComprobantes = "";
        public string Resultado = "";
        public string Motivo = "";
        public string MotivoDescripcion = "";
        public string Reproceso = "";
        public string SonServicios = "";
        public string CodigoError = "";
        public string MensajeError = "";

        public List<ResponseHeader> ResponseHeaders = new List<ResponseHeader>();

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

                BatchUniqueId = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/BatchUniqueId");
                CUITInformante = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/CUITInformante");
                FechaCAE = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/FechaCAE");
                CantidadComprobantes = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/CantidadComprobantes");
                Resultado = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Resultado");
                Motivo = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Motivo");
                MotivoDescripcion = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/MotivoDescripcion");
                Reproceso = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Reproceso");
                SonServicios = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/SonServicios");

                CodigoError = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Error/CodigoError");
                MensajeError = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Error/MensajeError");

                //Agrego todos los comprobantes
                for (int i = 0; i < xmlDoc.SelectNodes("/RespuestaLoteComprobantes/Comprobante").Count; i++)
                {
                    ResponseHeader thisHeader = new ResponseHeader();
                    thisHeader.CodigoDocumentoComprador = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "CodigoDocumentoComprador", i);
                    thisHeader.NroDocumentoComprador = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "NroDocumentoComprador", i);
                    thisHeader.TipoComprobante = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "TipoComprobante", i);
                    thisHeader.PuntoVenta = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "PuntoVenta", i);
                    thisHeader.LetraComprobante = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "LetraComprobante", i);
                    thisHeader.NroComprobanteDesde = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "NroComprobanteDesde", i);
                    thisHeader.NroComprobanteHasta = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "NroComprobanteHasta", i);
                    thisHeader.Importe = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "Importe", i);
                    thisHeader.ImporteNoGravado = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "ImporteNoGravado", i);
                    thisHeader.ImporteGravado = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "ImporteGravado", i);
                    thisHeader.ImporteImpuestoLiquidado = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "ImporteImpuestoLiquidado", i);
                    thisHeader.ImporteRNI_Percepcion = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "ImporteRNI_Percepcion", i);
                    thisHeader.ImporteExento = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "ImporteExento", i);
                    thisHeader.Resultado = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "Resultado", i);
                    thisHeader.CAE = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "CAE", i);
                    thisHeader.FechaComprobante = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "FechaComprobante", i);
                    thisHeader.FechaVencimiento = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "FechaVencimiento", i);
                    thisHeader.Motivo = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "Motivo", i);
                    thisHeader.MotivoDescripcion = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "MotivoDescripcion", i);
                    thisHeader.FechaDesdeServicioFacturado = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "FechaDesdeServicioFacturado", i);
                    thisHeader.FechaHastaServicioFacturado = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "FechaHastaServicioFacturado", i);
                    thisHeader.FechaVencimientoPago = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "FechaVencimientoPago", i);
                    thisHeader.UltimoIDUsado = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "UltimoIDUsado", i);
                    thisHeader.UltimoNroComprobanteUsado = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "UltimoNroComprobanteUsado", i);
                    thisHeader.NroInternoERP = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "NroInternoERP", i);
                    thisHeader.SQLID = ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante", "SQLID", i);

                    //NUEVO DESDE LA VERSION 1 DE AFIP
                    //Agrego las observaciones por comprobante
                    foreach (XmlNode ObsNode in xmlDoc.SelectNodes("/RespuestaLoteComprobantes/Comprobante")[i].SelectNodes("Observaciones/Obs"))
                    {
                        ResponseHeaderObs Obs = new ResponseHeaderObs();
                        Obs.Codigo = ObsNode["Codigo"].InnerText;
                        Obs.Msg = ObsNode["Msg"].InnerText;

                        thisHeader.Observaciones.Add(Obs);
                    }

                    ResponseHeaders.Add(thisHeader);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("FacturaElectronica", ex.Message, System.Diagnostics.EventLogEntryType.Error);
                return false;
            }

            return true;
        }

        public string GetXMLString()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode obsNode = null;
            XmlNode lineaNode = null;

            //Crear la parte del lote
            try
            {
                XmlElement xmlElem = xmlDoc.CreateElement("RespuestaLoteComprobantes");
                XmlNode rootNode = xmlDoc.AppendChild(xmlElem);

                xmlElem = xmlDoc.CreateElement("BatchUniqueId");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(this.BatchUniqueId));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("CUITInformante");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(this.CUITInformante));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("FechaCAE");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(this.FechaCAE));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("CantidadComprobantes");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(this.CantidadComprobantes));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("Resultado");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(this.Resultado));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("Motivo");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(this.Motivo));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("MotivoDescripcion");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(this.MotivoDescripcion));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("Reproceso");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(this.Reproceso));
                rootNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("SonServicios");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(this.SonServicios));
                rootNode.AppendChild(xmlElem);

                //Crear nodo de error
                XmlNode errorNode = null;
                xmlElem = xmlDoc.CreateElement("Error");
                errorNode = rootNode.AppendChild(xmlElem);

                xmlElem = xmlDoc.CreateElement("CodigoError");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(this.CodigoError));
                errorNode.AppendChild(xmlElem);
                xmlElem = xmlDoc.CreateElement("MensajeError");
                xmlElem.AppendChild(xmlDoc.CreateTextNode(this.MensajeError));
                errorNode.AppendChild(xmlElem);


                //Por cada documento
                foreach (ResponseHeader thisHeader in ResponseHeaders)
                {
                    XmlNode comprobanteNode = null;

                    xmlElem = xmlDoc.CreateElement("Comprobante");
                    comprobanteNode = rootNode.AppendChild(xmlElem);

                    xmlElem = xmlDoc.CreateElement("CodigoDocumentoComprador");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CodigoDocumentoComprador));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("NroDocumentoComprador");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.NroDocumentoComprador));
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
                    xmlElem = xmlDoc.CreateElement("Importe");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.Importe));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ImporteNoGravado");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ImporteNoGravado));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("ImporteGravado");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.ImporteGravado));
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
                    xmlElem = xmlDoc.CreateElement("Resultado");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.Resultado));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("CAE");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.CAE));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("FechaComprobante");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.FechaComprobante));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("FechaVencimiento");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.FechaVencimiento));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("Motivo");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.Motivo));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("MotivoDescripcion");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.MotivoDescripcion));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("FechaDesdeServicioFacturado");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.FechaDesdeServicioFacturado));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("FechaHastaServicioFacturado");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.FechaHastaServicioFacturado));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("FechaVencimientoPago");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.FechaVencimientoPago));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("UltimoIDUsado");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.UltimoIDUsado));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("UltimoNroComprobanteUsado");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.UltimoNroComprobanteUsado));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("NroInternoERP");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.NroInternoERP));
                    comprobanteNode.AppendChild(xmlElem);
                    xmlElem = xmlDoc.CreateElement("SQLID");
                    xmlElem.AppendChild(xmlDoc.CreateTextNode(thisHeader.SQLID));
                    comprobanteNode.AppendChild(xmlElem);

                    //Agregar las observaciones
                    xmlElem = xmlDoc.CreateElement("Observaciones");
                    comprobanteNode.AppendChild(xmlElem);

                    foreach (ResponseHeaderObs obs in thisHeader.Observaciones)
                    {
                        obsNode = xmlDoc.CreateElement("Obs");
                        xmlElem.AppendChild(obsNode);

                        lineaNode = xmlDoc.CreateElement("Codigo");
                        lineaNode.AppendChild(xmlDoc.CreateTextNode(obs.Codigo));
                        obsNode.AppendChild(lineaNode);
                        lineaNode = xmlDoc.CreateElement("Msg");
                        lineaNode.AppendChild(xmlDoc.CreateTextNode(obs.Msg));
                        obsNode.AppendChild(lineaNode);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("FacturaElectronica", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }

            return xmlDoc.InnerXml;
        }

    }
}
