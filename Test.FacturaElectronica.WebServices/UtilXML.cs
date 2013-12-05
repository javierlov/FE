using System;
using System.Linq;
using System.Xml;
using FacturaElectronica.Common;

namespace Test.FacturaElectronica.WebServices
{
    class UtilXML
    {

        public bool LoadXMLString(string xmlString)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            RequestBatch rqb = new RequestBatch();

            rqb.BatchUniqueId = ValidXMLValue(xmlDoc, "/RequestBatch/BatchUniqueId");
            rqb.SonServicios = ValidXMLValue(xmlDoc, "/RequestBatch/SonServicios");
            rqb.Periodo = ValidXMLValue(xmlDoc, "/RequestBatch/Periodo");
            rqb.CantidadComprobantes = ValidXMLValue(xmlDoc, "/RequestBatch/CantidadComprobantes");
            rqb.CUITInformante = ValidXMLValue(xmlDoc, "/RequestBatch/CUITInformante");
            rqb.Total = ValidXMLValue(xmlDoc, "/RequestBatch/Total");
            rqb.TotalComprobanteB = ValidXMLValue(xmlDoc, "/RequestBatch/TotalComprobanteB");
            rqb.TotalNoGravado = ValidXMLValue(xmlDoc, "/RequestBatch/TotalNoGravado");
            rqb.TotalGravado = ValidXMLValue(xmlDoc, "/RequestBatch/TotalGravado");
            rqb.TotalImpuestoLiquidado = ValidXMLValue(xmlDoc, "/RequestBatch/TotalImpuestoLiquidado");
            rqb.TotalRNI_Percepcion = ValidXMLValue(xmlDoc, "/RequestBatch/TotalRNI_Percepcion");
            rqb.TotalExento = ValidXMLValue(xmlDoc, "/RequestBatch/TotalExento");
            rqb.TotalPercepciones_PagosCuentaImpuestosNacionales = ValidXMLValue(xmlDoc, "/RequestBatch/TotalPercepciones_PagosCuentaImpuestosNacionales");
            rqb.TotalPercepcionIIBB = ValidXMLValue(xmlDoc, "/RequestBatch/TotalPercepcionIIBB");
            rqb.TotalPercepcionImpuestosMunicipales = ValidXMLValue(xmlDoc, "/RequestBatch/TotalPercepcionImpuestosMunicipales");
            rqb.TotalImpuestosInternos = ValidXMLValue(xmlDoc, "/RequestBatch/TotalImpuestosInternos");
            rqb.TotalMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/TotalMonedaFacturacion");
            rqb.TotalMonedaFacturacionComprobanteB = ValidXMLValue(xmlDoc, "/RequestBatch/TotalMonedaFacturacionComprobanteB");
            rqb.TotalNoGravadoMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/TotalNoGravadoMonedaFacturacion");
            rqb.TotalGravadoMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/TotalGravadoMonedaFacturacion");
            rqb.TotalImpuestoLiquidadoMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/TotalImpuestoLiquidadoMonedaFacturacion");
            rqb.TotalRNI_PercepcionMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/TotalRNI_PercepcionMonedaFacturacion");
            rqb.TotalExentoMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/TotalExentoMonedaFacturacion");
            rqb.TotalPercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/TotalPercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion");
            rqb.TotalPercepcionIIBBMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/TotalPercepcionIIBBMonedaFacturacion");
            rqb.TotalPercepcionImpuestosMunicipalesMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/TotalPercepcionImpuestosMunicipalesMonedaFacturacion");
            rqb.TotalImpuestosInternosMonedaFacturacion = ValidXMLValue(xmlDoc, "/RequestBatch/TotalImpuestosInternosMonedaFacturacion");
             
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

                rqb.RequestHeaders.Add(thisHeader);

                XmlDocument xmlLines = new XmlDocument();
                xmlLines.LoadXml(xmlDoc.SelectNodes("/RequestBatch/Comprobante")[i].OuterXml);

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
            
            return true;
        }

        private string ValidXMLValue(XmlDocument xmlDoc, string xmlPath)
        {
            string xmlPadre = xmlPath.Split('/')[1];
            string xml_Nodo = xmlPath.Split('/')[2];
            string valor = "";

            XmlNodeList xmlnl = xmlDoc.GetElementsByTagName(xmlPadre);
            

            foreach (XmlElement nodo in xmlnl)
            {
                valor = nodo.GetElementsByTagName(xml_Nodo).Item(0).InnerText;
                break;
            }
            
              return valor;
        }

        private string ValidXMLValue(XmlDocument xmlDoc, string xmlPath, string xmlTag, int nodeIndex)
        {
            if (xmlDoc.SelectNodes(xmlPath)[nodeIndex].SelectSingleNode(xmlTag).FirstChild != null)
                return xmlDoc.SelectNodes(xmlPath)[nodeIndex].SelectSingleNode(xmlTag).FirstChild.Value;
            else
                return "";
        }

        public string TransformXML(string xmlString)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlString);

                string resultado = "";

                foreach (XmlNode xn1 in xmlDoc.ChildNodes)
                {
                    resultado += " <Nodo> " + xn1.Name;
                    if (xn1.HasChildNodes)
                    {
                        foreach (XmlNode xn2 in xn1.ChildNodes)
                        {
                            resultado += " <SubNodo> " + xn2.Name;
                            if (xn2.HasChildNodes)
                            {
                                foreach (XmlNode xn3 in xn2.ChildNodes)
                                {
                                    resultado += " <SubNodo3> " + xn3.Name;
                                }
                            }
                        }
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
