using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using FacturaElectronica.Utils;
using FacturaElectronica.Common;

namespace FacturaElectronica.Service.Engine
{
    public class FileIntegration
    {
        DBEngine.SQLEngine sqlEngine = new DBEngine.SQLEngine();

        public RequestBatch ProcessData(FileInfo fi, Settings oSettings)
        {
            RequestBatch requestBatch = null;

            StreamReader fileInput = null;

            string line = string.Empty;

            try
            {
                //TODO: Espero que el SO libere el archivo
                while (Utils.Utils.FileIsInUse(fi))
                {
                }

                if (File.Exists(fi.FullName))
                {
                    //Determinar el tipo de comprobante basado en el nombre del archivo (left,2)
                    try
                    {
                        fileInput = new StreamReader(fi.FullName);

                        if ((line = fileInput.ReadLine()) != null)
                        {
                            if (line == "CABECERA")
                            {
                                if ((line = fileInput.ReadLine()) != null)
                                {
                                    fileInput.Close();

                                    //Identificar el tipo de archivo
                                    string tipoArchivo = line.Substring(0, 1);

                                    //Si está vacío asumo que es blanco
                                    if (tipoArchivo == ";")
                                        tipoArchivo = " ";

                                    if (tipoArchivo == " " || tipoArchivo == "1" || tipoArchivo == "2" || tipoArchivo == "3")
                                    {
                                        requestBatch = ProcesarLoteComprobantes(fi, oSettings);
                                    }
                                    else
                                    {
                                        ResponseError(null, fi, "E1", "El tipo de transacción es erróneo. Tipo de Transacción: '" + tipoArchivo + "'", oSettings);
                                    }
                                }
                                else
                                {
                                    sqlEngine.LogError("0", "0", "Identificando transacción", fi.Name + ": archivo no válido. Sin contenido de cabecera.");
                                }
                            }
                            else
                            {
                                sqlEngine.LogError("0", "0", "Identificando transacción", fi.Name + ": archivo no válido. La primera línea debe ser 'CABECERA'.");
                            }
                        }
                        else
                        {
                            sqlEngine.LogError("0", "0", "Identificando transacción", fi.Name + ": archivo no válido.");
                        }
                    }
                    catch (Exception ex)
                    {
                        sqlEngine.LogError("0", "0", "Identificando transacción", "Error no identificado 1: " + ex.Message);
                    }
                    finally
                    {
                        if (fileInput != null)
                            fileInput.Close();
                    }

                    fi.CopyTo(oSettings.PathHistorico + @"\" + fi.Name, true);

                    //Espero que el SO libere el file
                    while (Utils.Utils.FileIsInUse(fi))
                    {
                    }
                    fi.Delete();
                }
            }
            catch (Exception ex)
            {
                sqlEngine.LogError("0", "0", "Identificando transacción", "Error no identificado 2: " + ex.Message);
            }

            return requestBatch;
        }

        public void ProcessResponse(ResponseBatch rbResponse, Settings oSettings)
        {
            XmlDocument xmlResponse = new XmlDocument();

            try
            {
                xmlResponse.LoadXml(rbResponse.GetXMLString());

                //Generar para debug formato xml
                xmlResponse.Save(oSettings.PathDebug + @"\RESP_ " + rbResponse.BatchUniqueId + "_" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + ".xml");

                try
                {
                    XmlDocument respSEW = new XmlDocument();
                    XmlElement xmlElem = respSEW.CreateElement("comprobantes");
                    XmlNode rootNode = respSEW.AppendChild(xmlElem);

                    XmlNode detailsNode = null;
                    XmlNode comprobanteNode = null;
                    XmlAttribute comprobanteAttr = null;

                    string fileNameRespuesta = @"RESP_ " + rbResponse.BatchUniqueId + "_" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + ".xml";
                    string strFolderName = string.Empty;

                    switch (rbResponse.ResponseHeaders[0].TipoTransaccion)
                    {
                        case "0":
                        case "1":
                            {
                                #region Bienes y Servicios

                                detailsNode = respSEW.CreateElement("Resultado");
                                detailsNode.InnerText = rbResponse.Resultado;
                                xmlElem.AppendChild(detailsNode);

                                detailsNode = respSEW.CreateElement("Motivo");
                                detailsNode.InnerText = rbResponse.Motivo;
                                xmlElem.AppendChild(detailsNode);

                                detailsNode = respSEW.CreateElement("MotivoDescripcion");
                                detailsNode.InnerText = rbResponse.MotivoDescripcion;
                                xmlElem.AppendChild(detailsNode);

                                detailsNode = respSEW.CreateElement("Reproceso");
                                detailsNode.InnerText = rbResponse.Reproceso;
                                xmlElem.AppendChild(detailsNode);

                                detailsNode = respSEW.CreateElement("SonServicios");
                                detailsNode.InnerText = rbResponse.SonServicios;
                                xmlElem.AppendChild(detailsNode);

                                //detailsNode = respSEW.CreateElement("CodigoError");
                                //detailsNode.InnerText = rbResponse.CodigoError;
                                //xmlElem.AppendChild(detailsNode);

                                //detailsNode = respSEW.CreateElement("MensajeError");
                                //detailsNode.InnerText = rbResponse.MensajeError;
                                //xmlElem.AppendChild(detailsNode);

                                foreach (ResponseHeader thisResponse in rbResponse.ResponseHeaders)
                                {
                                    //Actualizar el documento con el nombre del archivo de respuesta
                                    sqlEngine.UpdateCabeceraNombreObjetoSalida(thisResponse.SQLID, fileNameRespuesta.Replace(".txt", string.Empty));

                                    xmlElem = respSEW.CreateElement("comprobante");
                                    comprobanteAttr = respSEW.CreateAttribute("idsolicitud");
                                    comprobanteAttr.Value = thisResponse.NroComprobanteDesde;
                                    xmlElem.Attributes.Append(comprobanteAttr);
                                    comprobanteNode = rootNode.AppendChild(xmlElem);

                                    xmlElem = respSEW.CreateElement("nro");
                                    xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.NroComprobanteDesde));
                                    comprobanteNode.AppendChild(xmlElem);

                                    xmlElem = respSEW.CreateElement("tipocodaut");
                                    xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.LetraComprobante));
                                    comprobanteNode.AppendChild(xmlElem);

                                    xmlElem = respSEW.CreateElement("codaut");
                                    xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.CAE));
                                    comprobanteNode.AppendChild(xmlElem);

                                    xmlElem = respSEW.CreateElement("vtocodaut");
                                    if (thisResponse.FechaVencimiento.Length == 8)
                                        xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.FechaVencimiento.Substring(0, 4) + "-" + thisResponse.FechaVencimiento.Substring(4, 2) + "-" + thisResponse.FechaVencimiento.Substring(6, 2)));
                                    comprobanteNode.AppendChild(xmlElem);

                                    xmlElem = respSEW.CreateElement("Tipo_cbte");
                                    xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.TipoComprobante));
                                    comprobanteNode.AppendChild(xmlElem);

                                    xmlElem = respSEW.CreateElement("Punto_vta");
                                    xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.PuntoVenta));
                                    comprobanteNode.AppendChild(xmlElem);

                                    if (thisResponse.Resultado == "A" || thisResponse.Resultado == "R")
                                    {
                                        xmlElem = respSEW.CreateElement("codtarea");
                                        xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.Motivo));
                                        comprobanteNode.AppendChild(xmlElem);

                                        switch (thisResponse.Resultado)
                                        {
                                            case "A":
                                                xmlElem = respSEW.CreateElement("msgerror");
                                                xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.MotivoDescripcion));
                                                comprobanteNode.AppendChild(xmlElem);

                                                xmlElem = respSEW.CreateElement("estadocmp");
                                                xmlElem.AppendChild(respSEW.CreateTextNode("AC"));
                                                comprobanteNode.AppendChild(xmlElem);

                                                break;

                                            case "R":
                                                xmlElem = respSEW.CreateElement("msgerror");
                                                xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.MotivoDescripcion + " - (Ultimo Comprobante Utilizado: " + thisResponse.UltimoNroComprobanteUsado + ")"));
                                                comprobanteNode.AppendChild(xmlElem);

                                                xmlElem = respSEW.CreateElement("estadocmp");
                                                xmlElem.AppendChild(respSEW.CreateTextNode("NP"));
                                                comprobanteNode.AppendChild(xmlElem);

                                                sqlEngine.LogError(thisResponse.SQLID, "0", thisResponse.Motivo, thisResponse.MotivoDescripcion + " - (Ultimo Comprobante Utilizado: " + thisResponse.UltimoNroComprobanteUsado + ")");

                                                break;
                                        }
                                    }
                                    else 
                                    {
                                        xmlElem = respSEW.CreateElement("codtarea");
                                        xmlElem.AppendChild(respSEW.CreateTextNode(rbResponse.CodigoError));
                                        comprobanteNode.AppendChild(xmlElem);

                                        xmlElem = respSEW.CreateElement("msgerror");
                                        xmlElem.AppendChild(respSEW.CreateTextNode(rbResponse.MensajeError));
                                        comprobanteNode.AppendChild(xmlElem);

                                        if (rbResponse.CodigoError.Length > 0 && rbResponse.CodigoError != "0" && rbResponse.CodigoError != "Equivalencias")
                                        {
                                            xmlElem = respSEW.CreateElement("estadocmp");
                                            xmlElem.AppendChild(respSEW.CreateTextNode("E"));
                                            comprobanteNode.AppendChild(xmlElem);

                                            sqlEngine.LogError(thisResponse.SQLID, "0", rbResponse.CodigoError, rbResponse.MensajeError);
                                        }
                                        else
                                        {
                                            xmlElem = respSEW.CreateElement("estadocmp");
                                            xmlElem.AppendChild(respSEW.CreateTextNode("E1"));
                                            comprobanteNode.AppendChild(xmlElem);
                                        }                                                
                                    }
                                }

                                //Guardo la respuesta sino hay errores
                                if (respSEW.SelectSingleNode("comprobantes/comprobante/nro") != null && respSEW.SelectSingleNode("comprobantes/comprobante/nro").InnerText != string.Empty &&
                                    respSEW.SelectSingleNode("comprobantes/comprobante/tipocodaut") != null && respSEW.SelectSingleNode("comprobantes/comprobante/tipocodaut").InnerText != string.Empty &&
                                    respSEW.SelectSingleNode("comprobantes/comprobante/Tipo_cbte") != null && respSEW.SelectSingleNode("comprobantes/comprobante/Tipo_cbte").InnerText != string.Empty &&
                                    respSEW.SelectSingleNode("comprobantes/comprobante/Punto_vta") != null && respSEW.SelectSingleNode("comprobantes/comprobante/Punto_vta").InnerText != string.Empty)
                                {
                                    respSEW.Save(oSettings.Salida + "\\" + fileNameRespuesta.Replace(".txt", string.Empty));
                                }
                                #endregion
                            }
                            break;

                        case "2":
                            {
                                #region Bienes de Capital

                                if (rbResponse.ResponseHeaders != null)
                                {
                                    foreach (ResponseHeader thisResponse in rbResponse.ResponseHeaders)
                                    {
                                        //Actualizar el documento con el nombre del archivo de respuesta
                                        sqlEngine.UpdateCabeceraNombreObjetoSalida(thisResponse.SQLID, fileNameRespuesta.Replace(".txt", string.Empty));

                                        xmlElem = respSEW.CreateElement("comprobante");
                                        comprobanteAttr = respSEW.CreateAttribute("idsolicitud");
                                        comprobanteAttr.Value = thisResponse.NroComprobanteDesde;
                                        xmlElem.Attributes.Append(comprobanteAttr);
                                        comprobanteNode = rootNode.AppendChild(xmlElem);

                                        xmlElem = respSEW.CreateElement("nro");
                                        xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.NroComprobanteDesde));
                                        comprobanteNode.AppendChild(xmlElem);

                                        xmlElem = respSEW.CreateElement("tipocodaut");
                                        xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.LetraComprobante));
                                        comprobanteNode.AppendChild(xmlElem);

                                        xmlElem = respSEW.CreateElement("codaut");
                                        xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.CAE));
                                        comprobanteNode.AppendChild(xmlElem);

                                        xmlElem = respSEW.CreateElement("vtocodaut");
                                        if (thisResponse.FechaVencimiento.Length > 0)
                                            xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.FechaVencimiento.Substring(0, 4) + "-" + thisResponse.FechaVencimiento.Substring(4, 2) + "-" + thisResponse.FechaVencimiento.Substring(6, 2)));
                                        comprobanteNode.AppendChild(xmlElem);

                                        xmlElem = respSEW.CreateElement("Tipo_cbte");
                                        xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.TipoComprobante));
                                        comprobanteNode.AppendChild(xmlElem);

                                        xmlElem = respSEW.CreateElement("Punto_vta");
                                        xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.PuntoVenta));
                                        comprobanteNode.AppendChild(xmlElem);

                                        if (rbResponse.Resultado == "A" || rbResponse.Resultado == "R")
                                        {
                                            switch (rbResponse.Resultado)
                                            {
                                                case "A":
                                                    xmlElem = respSEW.CreateElement("estadocmp");
                                                    xmlElem.AppendChild(respSEW.CreateTextNode("AC"));
                                                    comprobanteNode.AppendChild(xmlElem);

                                                    break;

                                                case "R":

                                                    if (rbResponse.ResponseHeaders[0].Motivo != string.Empty || rbResponse.ResponseHeaders[0].MotivoDescripcion != string.Empty && rbResponse.CodigoError != "Equivalencias")
                                                    {
                                                        xmlElem = respSEW.CreateElement("estadocmp");
                                                        xmlElem.AppendChild(respSEW.CreateTextNode("NP"));
                                                        comprobanteNode.AppendChild(xmlElem);

                                                        sqlEngine.LogError(thisResponse.SQLID, "0", rbResponse.ResponseHeaders[0].Motivo, rbResponse.ResponseHeaders[0].MotivoDescripcion + " - (Ultimo Comprobante Utilizado: " + rbResponse.ResponseHeaders[0].UltimoNroComprobanteUsado + ")");
                                                    }
                                                    else
                                                    {
                                                        xmlElem = respSEW.CreateElement("estadocmp");
                                                        xmlElem.AppendChild(respSEW.CreateTextNode("E1"));
                                                        comprobanteNode.AppendChild(xmlElem);

                                                        //sqlEngine.LogError(thisResponse.SQLID, "0", responseBatch.CodigoError, responseBatch.MensajeError);
                                                    }

                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            xmlElem = respSEW.CreateElement("estadocmp");
                                            xmlElem.AppendChild(respSEW.CreateTextNode("E"));
                                            comprobanteNode.AppendChild(xmlElem);
                                        }
                                    }
                                }
                                else
                                {
                                    xmlElem = respSEW.CreateElement("comprobante");
                                    comprobanteAttr = respSEW.CreateAttribute("idsolicitud");
                                    comprobanteAttr.Value = rbResponse.ResponseHeaders[0].NroComprobanteDesde;
                                    xmlElem.Attributes.Append(comprobanteAttr);
                                    comprobanteNode = rootNode.AppendChild(xmlElem);

                                    xmlElem = respSEW.CreateElement("nro");
                                    xmlElem.AppendChild(respSEW.CreateTextNode(rbResponse.ResponseHeaders[0].NroComprobanteDesde));
                                    comprobanteNode.AppendChild(xmlElem);
                                    xmlElem = respSEW.CreateElement("tipocodaut");
                                    xmlElem.AppendChild(respSEW.CreateTextNode(rbResponse.ResponseHeaders[0].LetraComprobante));
                                    comprobanteNode.AppendChild(xmlElem);
                                    xmlElem = respSEW.CreateElement("codaut");
                                    xmlElem.AppendChild(respSEW.CreateTextNode(""));
                                    comprobanteNode.AppendChild(xmlElem);
                                    xmlElem = respSEW.CreateElement("vtocodaut");
                                    xmlElem.AppendChild(respSEW.CreateTextNode(""));
                                    comprobanteNode.AppendChild(xmlElem);
                                    xmlElem = respSEW.CreateElement("Tipo_cbte");
                                    xmlElem.AppendChild(respSEW.CreateTextNode(rbResponse.ResponseHeaders[0].TipoComprobante));
                                    comprobanteNode.AppendChild(xmlElem);
                                    xmlElem = respSEW.CreateElement("Punto_vta");
                                    xmlElem.AppendChild(respSEW.CreateTextNode(rbResponse.ResponseHeaders[0].PuntoVenta));
                                    comprobanteNode.AppendChild(xmlElem);

                                    xmlElem = respSEW.CreateElement("estadocmp");
                                    xmlElem.AppendChild(respSEW.CreateTextNode("NP"));
                                    comprobanteNode.AppendChild(xmlElem);

                                    sqlEngine.LogError(rbResponse.ResponseHeaders[0].SQLID, "0", rbResponse.ResponseHeaders[0].Motivo, rbResponse.ResponseHeaders[0].MotivoDescripcion + " - (Ultimo Comprobante Utilizado: " + rbResponse.ResponseHeaders[0].UltimoNroComprobanteUsado + ")");
                                }

                                //revisar si el motivo esta vacio o es 13, el 13 es solo informativo
                                if (rbResponse.CodigoError == "" && (rbResponse.ResponseHeaders[0].Motivo == "" || rbResponse.ResponseHeaders[0].Motivo == "13"))
                                {
                                    xmlElem = respSEW.CreateElement("codtarea");
                                    xmlElem.AppendChild(respSEW.CreateTextNode("0"));
                                    comprobanteNode.AppendChild(xmlElem);

                                    xmlElem = respSEW.CreateElement("msgerror");
                                    xmlElem.AppendChild(respSEW.CreateTextNode("OK"));
                                    comprobanteNode.AppendChild(xmlElem);
                                }
                                else
                                {
                                    if (rbResponse.ResponseHeaders[0].Motivo != string.Empty)
                                    {
                                        if (rbResponse.ResponseHeaders[0].Motivo.IndexOf("11") > -1)
                                        {
                                            xmlElem = respSEW.CreateElement("codtarea");
                                            xmlElem.AppendChild(respSEW.CreateTextNode(rbResponse.ResponseHeaders[0].Motivo));
                                            comprobanteNode.AppendChild(xmlElem);

                                            xmlElem = respSEW.CreateElement("msgerror");
                                            xmlElem.AppendChild(respSEW.CreateTextNode(rbResponse.ResponseHeaders[0].MotivoDescripcion + " - (Ultimo Comprobante Utilizado: " + rbResponse.ResponseHeaders[0].UltimoNroComprobanteUsado + ")"));
                                            comprobanteNode.AppendChild(xmlElem);
                                        }
                                    }
                                    else
                                    {
                                        xmlElem = respSEW.CreateElement("codtarea");
                                        xmlElem.AppendChild(respSEW.CreateTextNode(rbResponse.CodigoError));
                                        comprobanteNode.AppendChild(xmlElem);

                                        xmlElem = respSEW.CreateElement("msgerror");
                                        xmlElem.AppendChild(respSEW.CreateTextNode(rbResponse.MensajeError));
                                        comprobanteNode.AppendChild(xmlElem);
                                    }
                                }

                                //Guardo la respuesta sino hay errores
                                if (respSEW.SelectSingleNode("comprobantes/comprobante/nro") != null && respSEW.SelectSingleNode("comprobantes/comprobante/nro").InnerText != string.Empty &&
                                    respSEW.SelectSingleNode("comprobantes/comprobante/tipocodaut") != null && respSEW.SelectSingleNode("comprobantes/comprobante/tipocodaut").InnerText != string.Empty &&
                                    respSEW.SelectSingleNode("comprobantes/comprobante/Tipo_cbte") != null && respSEW.SelectSingleNode("comprobantes/comprobante/Tipo_cbte").InnerText != string.Empty &&
                                    respSEW.SelectSingleNode("comprobantes/comprobante/Punto_vta") != null && respSEW.SelectSingleNode("comprobantes/comprobante/Punto_vta").InnerText != string.Empty)
                                {
                                    respSEW.Save(oSettings.Salida + "\\" + fileNameRespuesta.Replace(".txt", string.Empty));
                                }

                                #endregion
                            }
                            break;

                        case "3":
                            {
                                #region Exportacion

                                //Si hay error la AFIP no devuelve valores, por lo que se agregan en base a la solicitud o vacíos
                                if (rbResponse.ResponseHeaders[0].CAE.Length > 4)
                                {
                                    foreach (ResponseHeader thisResponse in rbResponse.ResponseHeaders)
                                    {
                                        xmlElem = respSEW.CreateElement("comprobante");
                                        comprobanteAttr = respSEW.CreateAttribute("idsolicitud");
                                        comprobanteAttr.Value = thisResponse.NroComprobanteDesde;
                                        xmlElem.Attributes.Append(comprobanteAttr);
                                        comprobanteNode = rootNode.AppendChild(xmlElem);

                                        xmlElem = respSEW.CreateElement("nro");
                                        xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.NroComprobanteDesde));
                                        comprobanteNode.AppendChild(xmlElem);

                                        xmlElem = respSEW.CreateElement("tipocodaut");
                                        xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.LetraComprobante));
                                        comprobanteNode.AppendChild(xmlElem);

                                        xmlElem = respSEW.CreateElement("codaut");
                                        xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.CAE));
                                        comprobanteNode.AppendChild(xmlElem);

                                        xmlElem = respSEW.CreateElement("vtocodaut");
                                        if (thisResponse.FechaVencimiento.Length == 8)
                                            xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.FechaVencimiento.Substring(0, 4) + "-" + thisResponse.FechaVencimiento.Substring(4, 2) + "-" + thisResponse.FechaVencimiento.Substring(6, 2)));
                                        comprobanteNode.AppendChild(xmlElem);

                                        xmlElem = respSEW.CreateElement("Tipo_cbte");
                                        xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.TipoComprobante));
                                        comprobanteNode.AppendChild(xmlElem);

                                        xmlElem = respSEW.CreateElement("Punto_vta");
                                        xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.PuntoVenta));
                                        comprobanteNode.AppendChild(xmlElem);

                                        if (rbResponse.Resultado == "A" || rbResponse.Resultado == "R")
                                        {
                                            switch (rbResponse.Resultado)
                                            {
                                                case "A":
                                                    xmlElem = respSEW.CreateElement("estadocmp");
                                                    xmlElem.AppendChild(respSEW.CreateTextNode("AC"));
                                                    comprobanteNode.AppendChild(xmlElem);

                                                    break;

                                                case "R":
                                                    xmlElem = respSEW.CreateElement("estadocmp");
                                                    xmlElem.AppendChild(respSEW.CreateTextNode("NP"));
                                                    comprobanteNode.AppendChild(xmlElem);

                                                    sqlEngine.LogError(rbResponse.ResponseHeaders[0].SQLID, "0", rbResponse.ResponseHeaders[0].Motivo, rbResponse.ResponseHeaders[0].MotivoDescripcion);

                                                    break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    xmlElem = respSEW.CreateElement("comprobante");
                                    comprobanteAttr = respSEW.CreateAttribute("idsolicitud");
                                    comprobanteAttr.Value = rbResponse.ResponseHeaders[0].NroComprobanteDesde;
                                    xmlElem.Attributes.Append(comprobanteAttr);
                                    comprobanteNode = rootNode.AppendChild(xmlElem);

                                    xmlElem = respSEW.CreateElement("nro");
                                    xmlElem.AppendChild(respSEW.CreateTextNode(rbResponse.ResponseHeaders[0].NroComprobanteDesde));
                                    comprobanteNode.AppendChild(xmlElem);
                                    xmlElem = respSEW.CreateElement("tipocodaut");
                                    xmlElem.AppendChild(respSEW.CreateTextNode(rbResponse.ResponseHeaders[0].LetraComprobante));
                                    comprobanteNode.AppendChild(xmlElem);
                                    xmlElem = respSEW.CreateElement("codaut");
                                    xmlElem.AppendChild(respSEW.CreateTextNode(""));
                                    comprobanteNode.AppendChild(xmlElem);
                                    xmlElem = respSEW.CreateElement("vtocodaut");
                                    xmlElem.AppendChild(respSEW.CreateTextNode(""));
                                    comprobanteNode.AppendChild(xmlElem);
                                    xmlElem = respSEW.CreateElement("Tipo_cbte");
                                    xmlElem.AppendChild(respSEW.CreateTextNode(rbResponse.ResponseHeaders[0].TipoComprobante));
                                    comprobanteNode.AppendChild(xmlElem);
                                    xmlElem = respSEW.CreateElement("Punto_vta");
                                    xmlElem.AppendChild(respSEW.CreateTextNode(rbResponse.ResponseHeaders[0].PuntoVenta));
                                    comprobanteNode.AppendChild(xmlElem);

                                    if (rbResponse.CodigoError != "Equivalencias")
                                    {
                                        sqlEngine.LogError(rbResponse.ResponseHeaders[0].SQLID, "0", rbResponse.CodigoError, rbResponse.MensajeError);

                                        xmlElem = respSEW.CreateElement("estadocmp");
                                        xmlElem.AppendChild(respSEW.CreateTextNode("NP"));
                                        comprobanteNode.AppendChild(xmlElem);
                                    }
                                    else
                                    {
                                        xmlElem = respSEW.CreateElement("estadocmp");
                                        xmlElem.AppendChild(respSEW.CreateTextNode("E1"));
                                        comprobanteNode.AppendChild(xmlElem);
                                    }
                                }

                                //revisar si el motivo esta vacio o es 13, el 13 es solo informativo
                                if (rbResponse.ResponseHeaders[0].Motivo == string.Empty || rbResponse.CodigoError != string.Empty)
                                {
                                    xmlElem = respSEW.CreateElement("codtarea");
                                    xmlElem.AppendChild(respSEW.CreateTextNode(rbResponse.CodigoError));
                                    comprobanteNode.AppendChild(xmlElem);

                                    xmlElem = respSEW.CreateElement("msgerror");
                                    xmlElem.AppendChild(respSEW.CreateTextNode(rbResponse.MensajeError));
                                    comprobanteNode.AppendChild(xmlElem);
                                }
                                else if (rbResponse.ResponseHeaders[0].Motivo == "13")
                                {
                                    xmlElem = respSEW.CreateElement("codtarea");
                                    xmlElem.AppendChild(respSEW.CreateTextNode("0"));
                                    comprobanteNode.AppendChild(xmlElem);

                                    xmlElem = respSEW.CreateElement("msgerror");
                                    xmlElem.AppendChild(respSEW.CreateTextNode("OK"));
                                    comprobanteNode.AppendChild(xmlElem);
                                }
                                else
                                {
                                    if (rbResponse.ResponseHeaders[0].Motivo.IndexOf("11") > -1)
                                    {
                                        xmlElem = respSEW.CreateElement("codtarea");
                                        xmlElem.AppendChild(respSEW.CreateTextNode(rbResponse.ResponseHeaders[0].Motivo));
                                        comprobanteNode.AppendChild(xmlElem);

                                        xmlElem = respSEW.CreateElement("msgerror");
                                        xmlElem.AppendChild(respSEW.CreateTextNode(rbResponse.ResponseHeaders[0].MotivoDescripcion + " - (Ultimo Comprobante Utilizado: " + rbResponse.ResponseHeaders[0].UltimoNroComprobanteUsado + ")"));
                                        comprobanteNode.AppendChild(xmlElem);
                                    }
                                }

                                //Actualizar el documento con el nombre del archivo de respuesta
                                sqlEngine.UpdateCabeceraNombreObjetoSalida(rbResponse.ResponseHeaders[0].SQLID, fileNameRespuesta.Replace(".txt", string.Empty));

                                //Guardo la respuesta sino hay errores
                                if (respSEW.SelectSingleNode("comprobantes/comprobante/nro") != null && respSEW.SelectSingleNode("comprobantes/comprobante/nro").InnerText != string.Empty &&
                                    respSEW.SelectSingleNode("comprobantes/comprobante/tipocodaut") != null && respSEW.SelectSingleNode("comprobantes/comprobante/tipocodaut").InnerText != string.Empty &&
                                    respSEW.SelectSingleNode("comprobantes/comprobante/Tipo_cbte") != null && respSEW.SelectSingleNode("comprobantes/comprobante/Tipo_cbte").InnerText != string.Empty &&
                                    respSEW.SelectSingleNode("comprobantes/comprobante/Punto_vta") != null && respSEW.SelectSingleNode("comprobantes/comprobante/Punto_vta").InnerText != string.Empty)
                                {
                                    respSEW.Save(oSettings.Salida + "\\" + fileNameRespuesta.Replace(".txt", string.Empty));
                                }
                                #endregion
                            }
                            break;

                        default:
                            return;
                    }
                }
                catch(Exception ex)
                {
                    sqlEngine.LogError("0", "0", "File Engine", ex.Message);
                }
            }
            catch
            {
                sqlEngine.LogError("0", "0", "File Engine", "El resultado no es un mensaje xml.");
            }
        }

        private RequestBatch ProcesarLoteComprobantes(FileInfo fi, Settings oSettings)
        {
            RequestBatch requestBatch = new RequestBatch();

            string filePath = fi.FullName;
            string line;

            bool bProcessOK = true;

            StreamReader fileInput = null;

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
                fileInput = new StreamReader(filePath, System.Text.Encoding.GetEncoding(1252));

                line = fileInput.ReadLine();
                while (line != null)
                {
                    if (line == "CABECERA")
                    {
                        line = fileInput.ReadLine();
                        if (line != null)
                        {
                            //proceso CABECERA
                            string[] camposCabecera = line.Split(';');

                            RequestHeader thisDocument = new RequestHeader();
                            if (camposCabecera[0] == "" || camposCabecera[0] == " ")
                                thisDocument.TipoTransaccion = "0";
                            else
                                thisDocument.TipoTransaccion = camposCabecera[0];

                            thisDocument.EmpresaID = oSettings.EmpresaID;
                            thisDocument.FechaComprobante = camposCabecera[1].Substring(6, 4) + "-" + camposCabecera[1].Substring(3, 2) + "-" + camposCabecera[1].Substring(0, 2);
                            thisDocument.FechaDesdeServicioFacturado = camposCabecera[2].Substring(6, 4) + "-" + camposCabecera[2].Substring(3, 2) + "-" + camposCabecera[2].Substring(0, 2); ;
                            thisDocument.FechaHastaServicioFacturado = camposCabecera[3].Substring(6, 4) + "-" + camposCabecera[3].Substring(3, 2) + "-" + camposCabecera[3].Substring(0, 2); ;
                            thisDocument.TipoComprobante = camposCabecera[4];
                            thisDocument.PuntoVenta = camposCabecera[5];
                            thisDocument.LetraComprobante = camposCabecera[6];
                            thisDocument.NroComprobanteDesde = camposCabecera[7];
                            thisDocument.NroComprobanteHasta = camposCabecera[7];
                            thisDocument.NroInternoERP = camposCabecera[8];
                            thisDocument.FechaVencimientoPago = camposCabecera[9].Substring(6, 4) + "-" + camposCabecera[9].Substring(3, 2) + "-" + camposCabecera[9].Substring(0, 2);
                            thisDocument.CondicionPago = camposCabecera[10];
                            thisDocument.CompradorCodigoDocumento = camposCabecera[11];
                            thisDocument.CompradorNroDocumento = camposCabecera[12];
                            thisDocument.CompradorTipoResponsable = camposCabecera[13];
                            thisDocument.CompradorTipoResponsableDescripcion = camposCabecera[14];
                            thisDocument.CompradorRazonSocial = camposCabecera[15];
                            thisDocument.CompradorDireccion = camposCabecera[16];
                            thisDocument.CompradorLocalidad = camposCabecera[17];
                            thisDocument.CompradorProvincia = camposCabecera[18];
                            thisDocument.CompradorPais = camposCabecera[19];
                            thisDocument.CompradorCodigoPostal = camposCabecera[20];
                            thisDocument.CompradorNroIIBB = camposCabecera[21];
                            thisDocument.CompradorCodigoCliente = camposCabecera[22];
                            thisDocument.CompradorNroReferencia = camposCabecera[23];
                            thisDocument.CompradorEmail = camposCabecera[24];
                            thisDocument.NroRemito = camposCabecera[25];
                            thisDocument.Importe = IntegrationEngine.ImporteConDecimales(camposCabecera[26], 2);
                            thisDocument.ImporteComprobanteB = IntegrationEngine.ImporteConDecimales(camposCabecera[27], 2);
                            thisDocument.ImporteNoGravado = IntegrationEngine.ImporteConDecimales(camposCabecera[28], 2);
                            thisDocument.ImporteGravado = IntegrationEngine.ImporteConDecimales(camposCabecera[29], 2);
                            thisDocument.AlicuotaIVA = IntegrationEngine.ImporteConDecimales(camposCabecera[30], 2);
                            thisDocument.ImporteImpuestoLiquidado = IntegrationEngine.ImporteConDecimales(camposCabecera[31], 2);
                            thisDocument.ImporteRNI_Percepcion = IntegrationEngine.ImporteConDecimales(camposCabecera[32], 2);
                            thisDocument.ImporteExento = IntegrationEngine.ImporteConDecimales(camposCabecera[33], 2);
                            thisDocument.ImportePercepciones_PagosCuentaImpuestosNacionales = IntegrationEngine.ImporteConDecimales(camposCabecera[34], 2);
                            thisDocument.ImportePercepcionIIBB = IntegrationEngine.ImporteConDecimales(camposCabecera[35], 2);
                            thisDocument.TasaIIBB = IntegrationEngine.ImporteConDecimales(camposCabecera[36], 2);
                            thisDocument.CodigoJurisdiccionIIBB = camposCabecera[37];
                            thisDocument.ImportePercepcionImpuestosMunicipales = IntegrationEngine.ImporteConDecimales(camposCabecera[38], 2);
                            thisDocument.JurisdiccionImpuestosMunicipales = camposCabecera[39];
                            thisDocument.ImporteImpuestosInternos = IntegrationEngine.ImporteConDecimales(camposCabecera[40], 2);
                            thisDocument.ImporteMonedaFacturacion = IntegrationEngine.ImporteConDecimales(camposCabecera[41], 2);
                            thisDocument.ImporteMonedaFacturacionComprobanteB = IntegrationEngine.ImporteConDecimales(camposCabecera[42], 2);
                            thisDocument.ImporteNoGravadoMonedaFacturacion = IntegrationEngine.ImporteConDecimales(camposCabecera[43], 2);
                            thisDocument.ImporteGravadoMonedaFacturacion = IntegrationEngine.ImporteConDecimales(camposCabecera[44], 2);
                            thisDocument.ImporteImpuestoLiquidadoMonedaFacturacion = IntegrationEngine.ImporteConDecimales(camposCabecera[45], 2);
                            thisDocument.ImporteRNI_PercepcionMonedaFacturacion = IntegrationEngine.ImporteConDecimales(camposCabecera[46], 2);
                            thisDocument.ImporteExentoMonedaFacturacion = IntegrationEngine.ImporteConDecimales(camposCabecera[47], 2);
                            thisDocument.ImportePercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion = IntegrationEngine.ImporteConDecimales(camposCabecera[48], 2);
                            thisDocument.ImportePercepcionIIBBMonedaFacturacion = IntegrationEngine.ImporteConDecimales(camposCabecera[49], 2);
                            thisDocument.ImportePercepcionImpuestosMunicipalesMonedaFacturacion = IntegrationEngine.ImporteConDecimales(camposCabecera[50], 2);
                            thisDocument.ImporteImpuestosInternosMonedaFacturacion = IntegrationEngine.ImporteConDecimales(camposCabecera[51], 2);
                            thisDocument.CantidadAlicuotasIVA = camposCabecera[52];
                            thisDocument.CodigoOperacion = camposCabecera[53];
                            thisDocument.TasaCambio = IntegrationEngine.ImporteConDecimales(camposCabecera[54], 5);
                            thisDocument.CodigoMoneda = camposCabecera[55];
                            thisDocument.ImporteEscrito = camposCabecera[56];
                            thisDocument.CantidadRegistrosDetalle = camposCabecera[57];
                            thisDocument.CodigoMecanismoDistribucion = camposCabecera[58];
                            thisDocument.TipoExportacion = camposCabecera[59];
                            thisDocument.PermisoExistente = camposCabecera[60];
                            thisDocument.CompradorPais = camposCabecera[61];
                            thisDocument.FormaPagoDescripcion = camposCabecera[63];
                            thisDocument.IncoTerms = camposCabecera[64];
                            thisDocument.Idioma = camposCabecera[65];
                            thisDocument.Observaciones1 = camposCabecera[66];
                            thisDocument.Observaciones2 = camposCabecera[67];
                            thisDocument.Observaciones3 = camposCabecera[68];
                            thisDocument.EmisorDireccion = camposCabecera[69];
                            thisDocument.EmisorCalle = camposCabecera[70];
                            thisDocument.EmisorCP = camposCabecera[71];
                            thisDocument.EmisorLocalidad = camposCabecera[72];
                            thisDocument.EmisorProvincia = camposCabecera[73];
                            thisDocument.EmisorPais = camposCabecera[74];
                            thisDocument.EmisorTelefonos = camposCabecera[75];
                            thisDocument.EmisorEMail = camposCabecera[76];
                            thisDocument.OficinaVentas = camposCabecera[77];

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

                            line = fileInput.ReadLine();
                            if (line != null && line == "DETALLE")
                            {
                                //proceso el detalle
                                RequestLine thisLine = new RequestLine();

                                line = fileInput.ReadLine();
                                if (line != null)
                                {
                                    while (line != null && line != "CABECERA")
                                    {
                                        thisLine = new RequestLine();
                                        string[] camposDetalle = line.Split(';');

                                        if (camposDetalle.Length > 10)
                                        {
                                            thisLine.CodigoProductoEmpresa = camposDetalle[0];
                                            while (thisLine.CodigoProductoEmpresa.Substring(0, 1) == "0")
                                            {
                                                thisLine.CodigoProductoEmpresa = thisLine.CodigoProductoEmpresa.Substring(1, thisLine.CodigoProductoEmpresa.Length - 1);
                                            }
                                            thisLine.CodigoProductoNCM = camposDetalle[1]; // RUTINA PARA OBTENERLO
                                            thisLine.CodigoProductoSecretaria = camposDetalle[2];
                                            thisLine.Descripcion = camposDetalle[3];
                                            thisLine.Cantidad = IntegrationEngine.ImporteConDecimales(camposDetalle[4], 3);
                                            thisLine.UnidadMedida = camposDetalle[5];
                                            thisLine.ImportePrecioUnitario = IntegrationEngine.ImporteConDecimales(camposDetalle[6], 2);
                                            thisLine.ImporteBonificacion = IntegrationEngine.ImporteConDecimales(camposDetalle[7], 2);
                                            thisLine.ImporteAjuste = IntegrationEngine.ImporteConDecimales(camposDetalle[8], 2);
                                            thisLine.ImporteSubtotal = IntegrationEngine.ImporteConDecimales(camposDetalle[9], 2);
                                            thisLine.ImportePrecioUnitarioMonedaFacturacion = IntegrationEngine.ImporteConDecimales(camposDetalle[10], 2);
                                            thisLine.ImporteBonificacionMonedaFacturacion = IntegrationEngine.ImporteConDecimales(camposDetalle[11], 2);
                                            thisLine.ImporteAjusteMonedaFacturacion = IntegrationEngine.ImporteConDecimales(camposDetalle[12], 2);
                                            thisLine.ImporteSubtotalMonedaFacturacion = IntegrationEngine.ImporteConDecimales(camposDetalle[13], 2);
                                            thisLine.ImporteSubtotalMonedaFacturacionConIVA = IntegrationEngine.ImporteConDecimales(camposDetalle[14], 2);
                                            thisLine.AlicuotaIVA = IntegrationEngine.ImporteConDecimales(camposDetalle[15], 2);
                                            thisLine.IndicadorExentoGravadoNoGravado = camposDetalle[16];
                                            thisLine.Observaciones = camposDetalle[17];
                                            //thisLine.MesPrestacion = camposDetalle[17];

                                            thisDocument.RequestLines.Add(thisLine);
                                        }
                                        line = fileInput.ReadLine();
                                    }
                                    requestBatch.RequestHeaders.Add(thisDocument);
                                    qtyComprobantes++;
                                }
                                else
                                {
                                    //error: el detalle está vacío
                                    sqlEngine.LogError("0", "0", "Leyendo archivo", "El archivo " + fi.Name + " no responde al formato esperado. El detalle está vacío.");
                                    bProcessOK = false;
                                }
                            }
                            else
                            {
                                //error: luego de la cabecera se espera el detalle
                                sqlEngine.LogError("0", "0", "Leyendo archivo", "El archivo " + fi.Name + " no responde al formato esperado. Luego de la cabecera debe venir el detalle.");
                                bProcessOK = false;
                            }
                        }
                        else
                        {
                            //error: la cabecera está vacía
                            sqlEngine.LogError("0", "0", "Leyendo archivo", "El archivo " + fi.Name + " no responde al formato esperado. La cabecera está vacía.");
                            bProcessOK = false;
                        }
                    }
                    else
                    {
                        //error
                        sqlEngine.LogError("0", "0", "Leyendo archivo", "El archivo " + fi.Name + " no responde al formato esperado. Debe comenzar con una cabecera.");
                        bProcessOK = false;
                    }
                }

                if (bProcessOK)
                {
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

                    fileInput.Close();
                }
            }
            catch(Exception ex)
            {
                sqlEngine.LogError("0", "0", "Leyendo archivo", ex.Message);
            }

            return requestBatch;
        }

        private void ResponseError(RequestBatch docBatch, FileInfo fi, string TipoLote, string strError, Settings oSettings)
        {
            XmlDocument respSEW = new XmlDocument();
            XmlElement xmlElem = respSEW.CreateElement("comprobantes");
            XmlNode rootNode = respSEW.AppendChild(xmlElem);
            XmlNode detailsNode;

            XmlNode comprobanteNode = null;
            XmlAttribute comprobanteAttr = null;

            try
            {               
                string fileNameRespuesta = @"RESP_ " + fi.Name.Replace(".txt", string.Empty) + "_" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + ".xml";

                if (docBatch != null)
                {
                    if (TipoLote == "0" || TipoLote == "1")
                    {
                        detailsNode = respSEW.CreateElement("Resultado");
                        detailsNode.InnerText = "E1";
                        xmlElem.AppendChild(detailsNode);

                        detailsNode = respSEW.CreateElement("Motivo");
                        xmlElem.AppendChild(detailsNode);

                        detailsNode = respSEW.CreateElement("MotivoDescripcion");
                        xmlElem.AppendChild(detailsNode);

                        detailsNode = respSEW.CreateElement("Reproceso");
                        detailsNode.InnerText = "N";
                        xmlElem.AppendChild(detailsNode);

                        detailsNode = respSEW.CreateElement("SonServicios");
                        detailsNode.InnerText = docBatch.SonServicios;
                        xmlElem.AppendChild(detailsNode);

                        //detailsNode = respSEW.CreateElement("CodigoError");
                        //detailsNode.InnerText = "E1";
                        //xmlElem.AppendChild(detailsNode);

                        //detailsNode = respSEW.CreateElement("MensajeError");
                        //detailsNode.AppendChild(respSEW.CreateTextNode(strError));
                        //xmlElem.AppendChild(detailsNode);
                    }

                    foreach (RequestHeader thisResponse in docBatch.RequestHeaders)
                    {
                        xmlElem = respSEW.CreateElement("comprobante");
                        comprobanteAttr = respSEW.CreateAttribute("idsolicitud");
                        comprobanteAttr.Value = thisResponse.NroComprobanteDesde;
                        xmlElem.Attributes.Append(comprobanteAttr);
                        comprobanteNode = rootNode.AppendChild(xmlElem);

                        xmlElem = respSEW.CreateElement("nro");
                        xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.NroComprobanteDesde));
                        comprobanteNode.AppendChild(xmlElem);

                        xmlElem = respSEW.CreateElement("tipocodaut");
                        xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.LetraComprobante));
                        comprobanteNode.AppendChild(xmlElem);

                        xmlElem = respSEW.CreateElement("codaut");
                        comprobanteNode.AppendChild(xmlElem);

                        xmlElem = respSEW.CreateElement("vtocodaut");
                        comprobanteNode.AppendChild(xmlElem);

                        xmlElem = respSEW.CreateElement("Tipo_cbte");
                        xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.TipoComprobante));
                        comprobanteNode.AppendChild(xmlElem);

                        xmlElem = respSEW.CreateElement("Punto_vta");
                        xmlElem.AppendChild(respSEW.CreateTextNode(thisResponse.PuntoVenta));
                        comprobanteNode.AppendChild(xmlElem);

                        xmlElem = respSEW.CreateElement("estadocmp");
                        xmlElem.AppendChild(respSEW.CreateTextNode("E1"));
                        comprobanteNode.AppendChild(xmlElem);

                        xmlElem = respSEW.CreateElement("codtarea");
                        xmlElem.AppendChild(respSEW.CreateTextNode(""));
                        comprobanteNode.AppendChild(xmlElem);

                        xmlElem = respSEW.CreateElement("msgerror");
                        xmlElem.AppendChild(respSEW.CreateTextNode(strError));
                        comprobanteNode.AppendChild(xmlElem);

                        sqlEngine.LogError(thisResponse.SQLID, "0", "File Engine", "Error: " + strError);

                        sqlEngine.UpdateCabeceraNombreObjetoSalida(thisResponse.SQLID, fileNameRespuesta);

                        sqlEngine.LogBatchEnd(thisResponse.SQLID, "Error", "", "");
                    }
                }
                else
                {
                    xmlElem = respSEW.CreateElement("comprobante");
                    comprobanteNode = rootNode.AppendChild(xmlElem);

                    xmlElem = respSEW.CreateElement("nro");
                    comprobanteNode.AppendChild(xmlElem);

                    xmlElem = respSEW.CreateElement("tipocodaut");
                    comprobanteNode.AppendChild(xmlElem);

                    xmlElem = respSEW.CreateElement("codaut");
                    comprobanteNode.AppendChild(xmlElem);

                    xmlElem = respSEW.CreateElement("vtocodaut");
                    comprobanteNode.AppendChild(xmlElem);

                    xmlElem = respSEW.CreateElement("Tipo_cbte");
                    comprobanteNode.AppendChild(xmlElem);

                    xmlElem = respSEW.CreateElement("Punto_vta");
                    comprobanteNode.AppendChild(xmlElem);

                    xmlElem = respSEW.CreateElement("estadocmp");
                    xmlElem.AppendChild(respSEW.CreateTextNode("E1"));
                    comprobanteNode.AppendChild(xmlElem);

                    xmlElem = respSEW.CreateElement("msgerror");
                    xmlElem.AppendChild(respSEW.CreateTextNode(strError));
                    comprobanteNode.AppendChild(xmlElem);

                    sqlEngine.LogError("0", "0", "File Engine", "Error: " + strError);
                }

                //Guardo la respuesta sino hay errores
                if (respSEW.SelectSingleNode("comprobantes/comprobante/nro") != null && respSEW.SelectSingleNode("comprobantes/comprobante/nro").InnerText != string.Empty &&
                    respSEW.SelectSingleNode("comprobantes/comprobante/tipocodaut") != null && respSEW.SelectSingleNode("comprobantes/comprobante/tipocodaut").InnerText != string.Empty &&
                    respSEW.SelectSingleNode("comprobantes/comprobante/Tipo_cbte") != null && respSEW.SelectSingleNode("comprobantes/comprobante/Tipo_cbte").InnerText != string.Empty &&
                    respSEW.SelectSingleNode("comprobantes/comprobante/Punto_vta") != null && respSEW.SelectSingleNode("comprobantes/comprobante/Punto_vta").InnerText != string.Empty)
                {
                    respSEW.Save(oSettings.Salida + "\\" + fileNameRespuesta.Replace(".txt", string.Empty));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("File Engine", "Error: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
        }

    }
}
