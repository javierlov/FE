using System;
using System.ComponentModel;
using System.Data;
using System.Web.Services;
using System.Xml;
using FacturaElectronica.Utils;
using FacturaElectronica.PrintEngine;

namespace FacturaElectronica.WSPrint
{
    /// <summary>
    /// Summary description for FEPrintService
    /// </summary>
    [WebService(Namespace = "http://accendo.com.ar/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class FEPrintService : System.Web.Services.WebService
    {
        #region Enums

        public enum TipoDocAfip
        {
            Factura
            ,NotaDeCredito
            ,NotaDeDebito
        }

        //public enum TipoDeCopia
        //{
        //    Original
        //    ,Duplicado
        //    ,Triplicado
        //}

        #endregion

        #region Metodos Auxiliares

        private bool EsComprobanteLocal(int codigoDocAfip)
        {
            switch (codigoDocAfip)
            {
                case 19: //Factura Export.
                case 20: //NDB Export.
                case 21: //NDC Export.
                    return false;

                default:
                    return true;
            }
        }

        private TipoDocAfip GetTipoDocByAfipCode(int codigoDocAfip)
        {
            string dummy;
            return GetTipoDocByAfipCode(codigoDocAfip, out dummy);
        }
        
        private TipoDocAfip GetTipoDocByAfipCode(int codigoDocAfip, out string codigoDocFileName) 
        {
            switch (codigoDocAfip)
            {
                case 1:  //A
                    codigoDocFileName = "FACA";
                    return TipoDocAfip.Factura;
                case 6:  //B
                    codigoDocFileName = "FACB";
                    return TipoDocAfip.Factura;
                case 19: //Exportacion
                    codigoDocFileName = "FACE";
                    return TipoDocAfip.Factura;

                case 2:  //A
                    codigoDocFileName = "NDBA";
                    return TipoDocAfip.NotaDeDebito;
                case 7:  //B
                    codigoDocFileName = "NDBB";
                    return TipoDocAfip.NotaDeDebito;
                case 20: //Exportacion
                    codigoDocFileName = "NDBE";
                    return TipoDocAfip.NotaDeDebito;

                case 3:  //A
                    codigoDocFileName = "NCRA";
                    return TipoDocAfip.NotaDeCredito;
                case 8:  //B
                    codigoDocFileName = "NCRB";
                    return TipoDocAfip.NotaDeCredito;
                case 21: //Exportacion
                    codigoDocFileName = "NCRE";
                    return TipoDocAfip.NotaDeCredito;

                default:
                    throw new Exception("Tipo de Comprobante Desconocido.");
            }
        }

        private string ValidXMLValue(XmlDocument xmlDoc, string xmlPath)
        {
            if (xmlDoc.SelectSingleNode(xmlPath).FirstChild != null)
                return xmlDoc.SelectSingleNode(xmlPath).FirstChild.Value;
            else
                return "";
        }

        //public ArrayList GetDocsIdFromXml(string xmlString)
        //{
        //    ArrayList ids = new ArrayList();
            
        //    try
        //    {
        //        XmlDocument xmlDoc = new XmlDocument();
        //        xmlDoc.LoadXml(xmlString);
        //        ids = new ArrayList();

        //        //Agrego todos los comprobantes
        //        for (int i = 0; i < xmlDoc.SelectNodes("/RespuestaLoteComprobantes/Comprobante").Count; i++)
        //            ids.Add(ValidXMLValue(xmlDoc, "/RespuestaLoteComprobantes/Comprobante"));
        //    }
        //    catch (Exception)
        //    {
        //        //ToDo: Esto es para Testing!
        //        ids.Add(1);
        //        ids.Add(2);
        //        ids.Add(3);
        //        ids.Add(4);
        //        ids.Add(5);
        //    }

        //    return ids;
        //}
             
        private static bool DebeImprimir(TipoDeCopia tipoDeCopia, string CopiasAImprimir)
        {
            return CopiasAImprimir.ToLower().IndexOf(tipoDeCopia.ToString().ToLower()) > -1; 
        }

        private static bool ComprobanteEncontrado(DataTable dtHeader, DataTable dtDetails, DataTable dtImpuestos)
        {
            return dtHeader != null && dtHeader.Rows.Count > 0 && dtDetails != null && dtDetails.Rows.Count > 0 && dtImpuestos != null && dtImpuestos.Rows.Count > 0;
        }
        
        #endregion

        [WebMethod]
        public string PrintCbte(string EmpresaID, string idsComprobantes, string FilePath)
        {
            const string CopiaComprobante = "Original|Duplicado|Triplicado|Copia";

            string[] docsId = null;
            char[] separador =  { ',' };
            string SQLID = string.Empty;
            string strResult = "OK";
            
            try
            {
                //Obtengo todos los Ids de los Docs a imprimir (vienen en xml).
                //docsId = GetDocsIdFromXml(idsComprobantes);
                docsId = idsComprobantes.Split(separador);

                //Genero todas las copias requeridas, para cada uno de los docs.
                for (int i = 0; i < docsId.Length; i++)
                {
                    //Id unico (por empresa) del Documento.
                    SQLID = Convert.ToString(docsId[i]);
                    
                    //Genero el documento!
                    if (DebeImprimir(TipoDeCopia.Original, CopiaComprobante))
                        PrintComprobante(EmpresaID, SQLID, TipoDeCopia.Original, FilePath);

                    if (DebeImprimir(TipoDeCopia.Duplicado, CopiaComprobante))
                        PrintComprobante(EmpresaID, SQLID, TipoDeCopia.Duplicado, FilePath);

                    if (DebeImprimir(TipoDeCopia.Triplicado, CopiaComprobante))
                        PrintComprobante(EmpresaID, SQLID, TipoDeCopia.Triplicado, FilePath);

                    if (DebeImprimir(TipoDeCopia.Copia, CopiaComprobante))
                        PrintComprobante(EmpresaID, SQLID, TipoDeCopia.Copia, FilePath);
                }
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }
            return strResult;
        }

        [WebMethod]
        public string SendToFileSystem(string EmpresaID, string idsComprobantes, string CopiaComprobante)
        {
            string[] docsId = null;
            char[] separador = { ',' };
            string SQLID = string.Empty;
            string strResult = string.Empty;

            try
            {
                //Obtengo todos los Ids de los Docs a imprimir (vienen en xml).
                //docsId = GetDocsIdFromXml(idsComprobantes);
                docsId = idsComprobantes.Split(separador);

                //Genero todas las copias requeridas, para cada uno de los docs.
                for (int i = 0; i < docsId.Length; i++)
                {
                    //Id unico (por empresa) del Documento.
                    SQLID = Convert.ToString(docsId[i]);

                    //Genero el documento!
                    if (DebeImprimir(TipoDeCopia.Original, CopiaComprobante))
                        strResult = PrintComprobante(EmpresaID, SQLID, TipoDeCopia.Original);

                    if (DebeImprimir(TipoDeCopia.Duplicado, CopiaComprobante))
                        strResult = PrintComprobante(EmpresaID, SQLID, TipoDeCopia.Duplicado);

                    if (DebeImprimir(TipoDeCopia.Triplicado, CopiaComprobante))
                        strResult = PrintComprobante(EmpresaID, SQLID, TipoDeCopia.Triplicado);

                    if (DebeImprimir(TipoDeCopia.Copia, CopiaComprobante))
                        strResult = PrintComprobante(EmpresaID, SQLID, TipoDeCopia.Copia);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return strResult;
        }

        [WebMethod]
        public byte[] GetCbte(string empresaID, string idCbte, TipoDeCopia eCopia)
        {
            byte[] bytesPdf = null;

            var sqlEngine = new FacturaElectronica.DBEngine.SQLEngine();

            try
            {
                bytesPdf = sqlEngine.LoadPdfFromDB(idCbte, eCopia.ToString());

                //Si no encontró el doc, primero lo genero!
                if (bytesPdf == null)
                {
                    //Genero los docs (Original, Duplicado y Triplicado) y vuelvo a buscar el pdf a la DB.
                    PrintCbte(empresaID, idCbte, string.Empty);
                    bytesPdf = sqlEngine.LoadPdfFromDB(idCbte, eCopia.ToString());
                }
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
            return bytesPdf;
        }

        private void PrintComprobante(string EmpresaID, string CbteId, TipoDeCopia tipoDeCopia, string FilePath)
        {
            var sqlEngine = new FacturaElectronica.DBEngine.SQLEngine();
            var oEmpresa = new Empresa(EmpresaID);
            var oSettings = new Settings(EmpresaID);
            var strResult = string.Empty;
            
            var cbteCodigo = 0;
            var puntoVenta = string.Empty;
            var nroComprobante = string.Empty;
            var fileName = "{0}_{1}_{2}_{3}_{4}.pdf"; //EmprCod_TipoDoc_PtoVta_NroDoc_Copia.pdf
            var codigoDocFileName = string.Empty;

            DataTable dtHeader = null;
            DataTable dtDetails = null;
            DataTable dtImpuestos= null;

            try
            {
                //Obtengo el comprobante de la DB.
                dtHeader = Common.GetComprobanteDataTable(CbteId);
                dtDetails = Common.GetComprobanteDetailDataTable(CbteId);
                dtImpuestos = Common.GetComprobanteImpuestoDataTable(CbteId);

                ////ToDo: Quitar.
                //dtDetails.TableName = "Detalle";
                //dtHeader.TableName = "Cabecera";
                //dtHeader.WriteXml(FilePath + "\\dtHeader_" + CbteId.ToString() + ".xml");
                //dtDetails.WriteXml(FilePath + "\\dtDetails_" + CbteId.ToString() + ".xml");

                var newPdf = new PrintEngine.Documento();

                if (ComprobanteEncontrado(dtHeader, dtDetails, dtImpuestos))
                {
                    cbteCodigo = Convert.ToInt32(dtHeader.Rows[0]["TipoComprobante"]);
                    puntoVenta = Convert.ToString(dtHeader.Rows[0]["PuntoVenta"]);
                    nroComprobante = Utils.Utils.FillWithCeros(Convert.ToString(dtHeader.Rows[0]["NroComprobanteDesde"]), 8);

                    //Lleno el objeto del reporte, con todos los datos necesarios.
                    newPdf.FillPdfProperties(Convert.ToString(cbteCodigo), tipoDeCopia, oEmpresa, oSettings, dtHeader);

                    //Solo si se envio un path al WS.
                    if (FilePath != null && FilePath != string.Empty)
                    {
                        //Si falta la ultima '\' al path, la agrego.
                        if (!FilePath.EndsWith(@"\")) FilePath += @"\";

                        //Preparo el nombre del Archivo, con el Nro de Comprobante y el Pto. de Venta.
                        GetTipoDocByAfipCode(cbteCodigo, out codigoDocFileName);
                        fileName = FilePath + string.Format(fileName, EmpresaID, codigoDocFileName, puntoVenta, nroComprobante, tipoDeCopia.ToString());
                    }
                    else
                        fileName = string.Empty;

                    //Llamo al metodo de newPdf que corresponde, segun el tipo de Doc y si es Local o Extranjero.
                    ImprimirComprobanteCorrespondiente(cbteCodigo, fileName, dtDetails, dtImpuestos, tipoDeCopia, newPdf);
                }
            }
            catch (Exception ex)
            {
                strResult = ex.Message + "FechaComprobante: " + dtHeader.Rows[0]["FechaComprobante"].ToString() + " | FechaVencimiento: " + dtHeader.Rows[0]["FechaVencimiento"].ToString();
                sqlEngine.LogError(dtHeader.Rows[0]["SQLID"].ToString(), "0", "Imprimiendo Factura", "Error: " + ex.Message);
            }
        }

        private string PrintComprobante(string EmpresaID, string CbteId, TipoDeCopia tipoDeCopia)
        {
            var sqlEngine = new FacturaElectronica.DBEngine.SQLEngine();
            var oEmpresa = new Empresa(EmpresaID);
            var oSettings = new Settings(EmpresaID);
            var strResult = string.Empty;

            var cbteCodigo = 0;
            var puntoVenta = string.Empty;
            var nroComprobante = string.Empty;
            var fileName = "{0}_{1}_{2}_{3}_{4}.pdf"; //EmprCod_TipoDoc_PtoVta_NroDoc_Copia.pdf
            var codigoDocFileName = string.Empty;

            DataTable dtHeader = null;
            DataTable dtDetails = null;
            DataTable dtImpuestos = null;

            try
            {
                //Obtengo el comprobante de la DB.
                dtHeader = Common.GetComprobanteDataTable(CbteId);
                dtDetails = Common.GetComprobanteDetailDataTable(CbteId);
                dtImpuestos = Common.GetComprobanteImpuestoDataTable(CbteId);

                var newPdf = new PrintEngine.Documento();

                if (ComprobanteEncontrado(dtHeader, dtDetails, dtImpuestos))
                {
                    cbteCodigo = Convert.ToInt32(dtHeader.Rows[0]["TipoComprobante"]);
                    puntoVenta = Convert.ToString(dtHeader.Rows[0]["PuntoVenta"]);
                    nroComprobante = Utils.Utils.FillWithCeros(Convert.ToString(dtHeader.Rows[0]["NroComprobanteDesde"]), 8);

                    //Lleno el objeto del reporte, con todos los datos necesarios.
                    newPdf.FillPdfProperties(Convert.ToString(cbteCodigo), tipoDeCopia, oEmpresa, oSettings, dtHeader);

                    //Preparo el nombre del Archivo, con el Nro de Comprobante y el Pto. de Venta.
                    GetTipoDocByAfipCode(cbteCodigo, out codigoDocFileName);

                    fileName = oSettings.PathTemporales + "\\" + string.Format(fileName, EmpresaID, codigoDocFileName, puntoVenta, nroComprobante, tipoDeCopia.ToString());

                    //Llamo al metodo de newPdf que corresponde, segun el tipo de Doc y si es Local o Extranjero.
                    ImprimirComprobanteCorrespondiente(cbteCodigo, fileName, dtDetails, dtImpuestos, tipoDeCopia, newPdf);
                }
                else
                {
                    fileName = string.Empty;
                }
            }
            catch (Exception ex)
            {
                strResult = ex.Message + "FechaComprobante: " + dtHeader.Rows[0]["FechaComprobante"].ToString() + " | FechaVencimiento: " + dtHeader.Rows[0]["FechaVencimiento"].ToString();
                sqlEngine.LogError(dtHeader.Rows[0]["SQLID"].ToString(), "0", "Imprimiendo Factura", "Error: " + ex.Message);
            }

            return fileName;
        }

        private void ImprimirComprobanteCorrespondiente(int cbteCodigo, string fileName, DataTable dtDetails, DataTable dtImpuestos, TipoDeCopia eCopia, PrintEngine.Documento newPdf)
        {
            if (EsComprobanteLocal(cbteCodigo))
            {
                switch (GetTipoDocByAfipCode(cbteCodigo))
                {
                    case TipoDocAfip.Factura:
                        newPdf.ImprimirArchivoFacturaLocal(fileName, dtDetails, dtImpuestos);
                        break;
                    case TipoDocAfip.NotaDeCredito:
                        newPdf.ImprimirArchivoNotaCreditoLocal(fileName, dtDetails, dtImpuestos);
                        break;
                    case TipoDocAfip.NotaDeDebito:
                        newPdf.ImprimirArchivoNotaDebitoLocal(fileName, dtDetails, dtImpuestos);
                        break;
                }
            }
            else //Es Comprobante de Exportacion.
            {
                switch (GetTipoDocByAfipCode(cbteCodigo))
                {
                    case TipoDocAfip.Factura:
                        newPdf.ImprimirArchivoFacturaExportacion(fileName, dtDetails, dtImpuestos);
                        break;
                    case TipoDocAfip.NotaDeCredito:
                        newPdf.ImprimirArchivoNotaCreditoExportacion(fileName, dtDetails, dtImpuestos);
                        break;
                    case TipoDocAfip.NotaDeDebito:
                        newPdf.ImprimirArchivoNotaDebitoExportacion(fileName, dtDetails, dtImpuestos);
                        break;
                }
            }
        }
    }
}
